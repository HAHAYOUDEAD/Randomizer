using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = System.Random;
using System.Diagnostics;
using Il2CppAmazingAssets.TerrainToMesh;
using static Randomizer.Debug;
using UnityEngine.AddressableAssets;
using Il2CppTLD.AddressableAssets;

namespace Randomizer
{
    internal class Function
    {
        private static int chanceRollSameTransitionType = 5;


        /// <summary>
        /// ⚠ Only call when saveslot is loaded ⚠
        /// </summary>
        public static int AcquireSeed(bool early)
        {
            if (Settings.options.seedMode == 0) // random
            {
                if (!early)
                {
                    return GameManager.m_SceneTransitionData.m_GameRandomSeed;
                }

                int seed = 0;

                SaveGameSlots.GetSaveSlotFromName(SaveGameSystem.GetCurrentSaveName()).TryGetData("global", out GlobalSaveGameFormat gsgf); // pull from savegame data because it's deserialized too late
                if (gsgf != null)
                {
                    string json = gsgf.m_GameManagerSerialized;
                    try
                    {
                        using var outer = JsonDocument.Parse(json); // way faster than deserializing into objects

                        if (outer.RootElement.TryGetProperty("m_SceneTransitionDataSerialized", out var innerProp) && innerProp.ValueKind == JsonValueKind.String)
                        {
                            string innerJson = innerProp.GetString();

                            if (!string.IsNullOrEmpty(innerJson))
                            {
                                using var inner = JsonDocument.Parse(innerJson);

                                if (inner.RootElement.TryGetProperty("m_GameRandomSeed", out var seedProp)) seedProp.TryGetInt32(out seed);
                            }
                        }
                    }
                    catch (JsonException ex)
                    {
                        Log(CC.Red, "FAILURE: can't retrieve seed from saveslot: " + ex.Message);
                    }
                }
                return seed;
            }
            else if (Settings.options.seedMode == 1) // controlled
            {
                return GetSeedFromSandboxName(SaveGameSlots.GetUserDefinedSlotName(SaveGameSystem.GetCurrentSaveName())); // create from saveslot name
            }
            else if (Settings.options.seedMode == 2) // debug
            {
                return 42;
            }

            return 42;
        }

        public static bool PickPairByTypeAndRemoveFromList(
            int seed,
            Pair original,
            Dictionary<TransitionType, TransitionType> validMatches,
            ref Dictionary<TransitionType, List<Pair>> pool,
            out Pair? rolled )
        {
            if (!validMatches.ContainsKey(original.In.type)) // skip if filtered out by shuffle mode settings
            {
                rolled = null;
                return false;
            }

            var rng = new Random(seed);

            var validTransitions = pool[original.In.type]; // look for same type transition to replace with

            bool isIndoorsToIndoors = original.Out.type == TransitionType.ToIndoors && original.In.type == TransitionType.ToIndoors;
            bool isCaveToCave = original.Out.type == TransitionType.ToCave && original.In.type == TransitionType.ToCave;
            bool canUseCounterpart = original.In.type == TransitionType.ToOutdoorsFromIndoors || original.In.type == TransitionType.ToOutdoorsFromCave;

            // if same type transition both ways, switch to counterparts, otherwise there's not enough pairs for normal transitions
            if ((isIndoorsToIndoors || isCaveToCave))
            {
                if (pool.TryGetValue(validMatches[original.In.type], out var counterparts))
                {
                    validTransitions = counterparts;
                }
                //Log(CC.DarkGray, $"Switching to counterpart type for {pair.In.type} when pairing {pair.In.linkedPoint} in scene {pair.In.fromScene}");    
            }

            bool poolEmpty = validTransitions.Count == 0 || (validTransitions.Count == 1 && validTransitions[0] == original);

            // chance switch in counterpart for types that can be harmlessly switched to counterparts
            if (canUseCounterpart && (poolEmpty || rng.Next(100) < chanceRollSameTransitionType))
            {
                if (pool.TryGetValue(validMatches[original.In.type], out var counterparts))
                {
                    validTransitions = counterparts;
                    poolEmpty = validTransitions.Count == 0 || (validTransitions.Count == 1 && validTransitions[0] == original);
                }
                //Log(CC.DarkGray, $"Rolling for counterpart for {pair.In.type} when pairing {pair.In.linkedPoint} in scene {pair.In.fromScene}");
            }

            // still no valid transitions for the picked type left
            if (poolEmpty)
            {
                rolled = null;
                //Log(CC.Red, $"No valid transitions available for pairing {pair.Out.linkedPoint} in scene {pair.Out.fromScene}, skipping linking");
                return false;
            }

            int selfIndex = validTransitions.IndexOf(original);
            int index;

            if (selfIndex == -1) // self not in pool, just pick random
            {
                index = rng.Next(validTransitions.Count);
            }
            else
            {
                int count = validTransitions.Count;

                index = rng.Next(count - 1); // reserve 1 for self

                if (index >= selfIndex) index++; // skip self (or next to prevent choice bias)
            }

            rolled = validTransitions[index];

            validTransitions.RemoveAt(index); // remove from available pool to prevent reuse

            return true;
        }

        public static Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> RollPairs(int seed)
        {
            var sw = Stopwatch.StartNew();
            var rng = new Random(seed);

            // flatten dict and create new one sorted by transition type for easier pairing logic
            List<TransitionDefinition> priority = [];
            List<TransitionDefinition> regular = [];

            foreach (var scenes in transitions)
            {
                foreach (var transition in scenes.Value)
                {
                    transition.fromScene = scenes.Key; // add original scene info to transition definition for later reference

                    // lookup num scenes, if > 5(most main zones), have corresponding chance of priopitizing it for pairing start
                    if (scenes.Value.Length > 5 && rng.Next(100) < scenes.Value.Length / 2)
                    {
                        priority.Add(transition);
                    }
                    else
                    {
                        regular.Add(transition);
                    }
                }
            }

            List<TransitionDefinition> allTransitions = [];
            allTransitions.AddRange(priority);
            allTransitions.AddRange(regular);

            sw.Stop();
            Log(CC.Cyan, $"Flattened transitions in {sw.Elapsed.TotalMilliseconds:F2} ms, prioritized {allTransitions[0].fromScene} to begin pairing");
            sw.Restart();

            // one-way pair transitions to their linked counterparts (exits to enters)
            List<Pair> pairs = [];
            Dictionary<TransitionType, List<Pair>> pairsByType = [];

            foreach (var transition in allTransitions)
            {
                // Skip if we already added this pair
                if (pairs.Any(p => p.In == transition || p.Out == transition)) continue;

                var counterpart = allTransitions.FirstOrDefault(link =>
                    link.exitPoint == transition.linkedPoint &&
                    link.toScene == transition.fromScene);

                if (counterpart != null)
                {
                    pairs.Add(new Pair(transition, counterpart));
                }
                else
                {
                    pairs.Add(new Pair(transition, transition));
                    Log(System.ConsoleColor.Red, $"No pair found for {transition.exitPoint} in scene {transition.fromScene}");
                }

                if (Settings.options.shuffleMode != 0) // only make by type dict when needed
                {
                    pairsByType.TryAdd(transition.type, new());
                    pairsByType[transition.type].Add(new Pair(transition, counterpart ?? transition));
                }
            }

            sw.Stop();
            Log(CC.Cyan, $"Generated {pairs.Count} transition pairs in {sw.Elapsed.TotalMilliseconds:F2} ms");
            sw.Restart();

            // pair transition pairs to randomized pairs
            Dictionary<Pair, Pair> pairedPairs = [];

            if (Settings.options.shuffleMode == 0) // no logic, just shuffle everything
            {

                List<Pair> pairsClone = new(pairs);

                foreach (var pair in pairs)
                {
                    int index = rng.Next(pairsClone.Count);
                    var randomPair = pairsClone[index];
                    pairsClone.RemoveAt(index);

                    pairedPairs[pair] = randomPair;
                }

                /*
                List<Pair> pairsClone = new(pairs); // made by Claude

                // Fisher-Yates shuffle for uniform randomness
                for (int i = pairsClone.Count - 1; i > 0; i--)
                {
                    int j = rng.Next(i + 1);
                    (pairsClone[i], pairsClone[j]) = (pairsClone[j], pairsClone[i]);
                }

                // pair adjacent elements (guaranteed 2-cycles)
                for (int i = 0; i + 1 < pairsClone.Count; i += 2)
                {
                    pairedPairs[pairsClone[i]] = pairsClone[i + 1];
                    pairedPairs[pairsClone[i + 1]] = pairsClone[i];
                }
                */
                if (pairsClone.Count % 2 != 0)
                {
                    Log(CC.Yellow, $"Odd pair count. Transition to {pairsClone[^1].In.toScene}: {pairsClone[^1].In.exitPoint} in {pairsClone[^1].In.fromScene} left upaired");
                }

                sw.Stop();
                Log(CC.Cyan, $"Shuffled pairs for 'No logic' preset in {sw.Elapsed.TotalMilliseconds:F2} ms");
                sw.Restart();
            }
            else if (Settings.options.shuffleMode == 1) // reasonable, shuffle within types
            {
                Dictionary<TransitionType, TransitionType> validMatches = new()
                {
                    { TransitionType.ToIndoors, TransitionType.ToOutdoorsFromIndoors },
                    { TransitionType.ToCave, TransitionType.ToOutdoorsFromCave },
                    { TransitionType.ToOutdoorsFromIndoors, TransitionType.ToIndoors },
                    { TransitionType.ToOutdoorsFromOutdoors, TransitionType.ToOutdoorsFromOutdoors },
                    { TransitionType.ToOutdoorsFromCave, TransitionType.ToCave },
                    { TransitionType.ToOutdoorsFromBunker, TransitionType.ToBunker },
                    { TransitionType.ToBunker, TransitionType.ToOutdoorsFromBunker }
                };

                int n = 0;
                foreach (Pair pair in pairs)
                {
                    if (PickPairByTypeAndRemoveFromList(seed, pair, validMatches, ref pairsByType, out Pair? picked))
                    {
                        pairedPairs[pair] = picked;
                    }
                    else
                    {
                        n++;
                    }
                }

                if (n > 0)
                {
                    Log(CC.Yellow, $"{n} pairs had no valid matches and were left unchanged");
                }

                sw.Stop();
                Log(CC.Cyan, $"Shuffled pairs for 'Reasonable' preset in {sw.Elapsed.TotalMilliseconds:F2} ms");
            }

            else if (Settings.options.shuffleMode == 2) // outdoors only, shuffle within outdoors including caves
            {
                Dictionary<TransitionType, TransitionType> validMatches = new()
                {
                    { TransitionType.ToCave, TransitionType.ToOutdoorsFromCave },
                    { TransitionType.ToOutdoorsFromOutdoors, TransitionType.ToOutdoorsFromOutdoors },
                    { TransitionType.ToOutdoorsFromCave, TransitionType.ToCave }
                };

                int n = 0;
                foreach (Pair pair in pairs)
                {
                    if (PickPairByTypeAndRemoveFromList(seed, pair, validMatches, ref pairsByType, out Pair? picked))
                    {
                        pairedPairs[pair] = picked;
                    }
                    else
                    {
                        n++;
                    }
                }

                if (n > 0)
                {
                    Log(CC.Yellow, $"{n} pairs had no valid matches and were left unchanged");
                }

                sw.Stop();
                Log(CC.Cyan, $"Shuffled pairs for 'Outdoors Only' preset in {sw.Elapsed.TotalMilliseconds:F2} ms");
            }
            /*
            else if (Settings.options.shuffleMode == 3) // region lock, shuffle within same region except transitions leading out of the region
            {
                int n = 0;



                // need list that would match all interiors to exteriors (already exists hardcoded fro HL, scenenamemapping or something like that)

                // need function that would start at a region, go through every transition in it that doesn't lead ToOutdoorsFromOutdoors or to transition cave (find those),
                // and lead them all through back to the same region. Then move to next region, until all Regions are parsed. Finally, process remaining regions and cave/transition zones to interlink between each other





                
                Dictionary<TransitionType, TransitionType> validMatches = new()
                {
                    { TransitionType.ToIndoors, TransitionType.ToOutdoorsFromIndoors },
                    { TransitionType.ToCave, TransitionType.ToOutdoorsFromCave },
                    { TransitionType.ToBunker, TransitionType.ToOutdoorsFromBunker }
                };

                // 1. for ToIndoors match the vanilaa scene that's connected to it and extract number or exits
                // 2. create a loop that has interior scenes with same amount of exits
                // 3. map interiors exits to the exterior building entrances

                foreach (Pair pair in pairs)
                {
                    if (PickPairByTypeAndRemoveFromList(seed, pair, validMatches, ref pairsByType, out Pair? picked))
                    {
                        pairedPairs[pair] = picked;
                    }
                    else
                    {
                        n++;
                    }
                }

                // 

                Dictionary<TransitionType, TransitionType> validMatches2 = new()
                {
                    { TransitionType.ToOutdoorsFromIndoors, TransitionType.ToIndoors },
                    { TransitionType.ToOutdoorsFromCave, TransitionType.ToCave },
                    { TransitionType.ToOutdoorsFromBunker, TransitionType.ToBunker }
                };

                foreach (Pair pair in pairs) 
                {
                    if (PickPairByTypeAndRemoveFromList(seed, pair, validMatches2, ref pairsByType, out Pair? picked))
                    {
                        pairedPairs[pair] = picked;
                    }
                    else
                    {
                        n++;
                    }
                }

                Dictionary<TransitionType, TransitionType> validMatches3 = new()
                {
                    { TransitionType.ToOutdoorsFromOutdoors, TransitionType.ToOutdoorsFromOutdoors }
                };

                foreach (Pair pair in pairs)
                {
                    if (PickPairByTypeAndRemoveFromList(seed, pair, validMatches3, ref pairsByType, out Pair? picked))
                    {
                        pairedPairs[pair] = picked;
                    }
                    else
                    {
                        n++;
                    }
                }

                // make new pool that would uhh
                // first roll the ToIndoors and ToCave transitions, omit all ToOutdoors 


                // then roll all ToOutdoors except ToOutdoorsFromOutdoors, only withib the same region
                // finally roll all ToOutdoorsFromOutdoors








                if (n > 0)
                {
                    Log(CC.Yellow, $"{n} pairs had no valid matches and were left unchanged");
                }

                sw.Stop();
                Log(CC.Cyan, $"Shuffled pairs for 'Outdoors Only' preset in {sw.Elapsed.TotalMilliseconds:F2} ms");
            }
            */


            //RunOddTransitionLookup(pairedPairs);
            sw.Restart();

            // swap A and B points to get final transition replacement map
            Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> result = [];

            foreach (var pairOfPairs in pairedPairs)
            {
                var A = pairOfPairs.Key;
                var B = pairOfPairs.Value;

                if (A == B) continue; // skip unchanged pairs

                // A's In remapped to B's In
                result.TryAdd(A.In.fromScene, []);
                if (!(result[A.In.fromScene].ContainsKey(A.In)))
                {
                    result[A.In.fromScene][A.In] = B.In;
                    //Log(System.ConsoleColor.Green, $"Linking  {A.AIn.linkedPoint} in {A.AIn.fromScene} with {B.BIn.exitPoint} in {B.BIn.toScene}");
                }

                // B's Out mirrored back to A's Out
                result.TryAdd(B.Out.fromScene, []);
                if (!(result[B.Out.fromScene].ContainsKey(B.Out)))
                {
                    result[B.Out.fromScene][B.Out] = A.Out;
                    //Log(System.ConsoleColor.Blue, $"Mirroring  {B.BOut.exitPoint} in {B.BOut.toScene} to {A.AOut.linkedPoint} in {A.AOut.fromScene}");
                }
            }

            sw.Stop();
            Log(CC.Cyan, $"Finalizing result dictionary in {sw.Elapsed.TotalMilliseconds:F2} ms");

            int total = result.Sum(r => r.Value.Count);

            Log(total == allTransitions.Count ? CC.Green : CC.Red, $"{total} pairs vs {allTransitions.Count} total transitions ({allTransitions.Count - total} went MIA)");
            Log("");

            RunSymmetryDiagnostics(result);

            return result;


        }
    }
}
