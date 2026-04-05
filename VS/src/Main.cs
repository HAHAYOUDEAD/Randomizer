global using static Randomizer.Utility;
using Harmony;
using JetBrains.Annotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using static UnityEngine.UI.Image;
using static UnityEngine.UI.Selectable;
using Random = System.Random;
using System.Diagnostics;
using Il2CppAmazingAssets.TerrainToMesh;
using static Randomizer.Debug;

namespace Randomizer
{


    public class Main : MelonMod
    {
        public bool isLoaded = false;



        private static int chanceRollSameTransitionType = 5;



        public override void OnInitializeMelon()
        {
            modsPath = Path.GetFullPath(typeof(MelonMod).Assembly.Location + "/../../../Mods/");

            Settings.OnLoad();

            transitions = JsonSerializer.Deserialize<Dictionary<string, TransitionDefinition[]>>(LoadEmbeddedJSON("Transitions.json"), GetDefaultJsonOptions()) ?? [];
            inconsistentTransitions = JsonSerializer.Deserialize<Dictionary<string, TransitionDefinition[]>>(LoadEmbeddedJSON("InconsistentTransitions.json"), GetDefaultJsonOptions()) ?? [];

            rolledPairs = RollPairs(Settings.options.seed);

            //RunTransitionDictionaryIntegrityCheck();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            isLoaded = true;
        }

        public override void OnUpdate()
        {
            if (isLoaded && InputManager.GetPauseMenuTogglePressed(InputManager.m_CurrentContext))
            {
                Debug.breakCoroutines = true;
            }

#if !SHENANIGANS
            var current = Shenanigans.GetValueSafe();

            if (Shenanigans._lastValue != current)
            {
                Log(CC.Red,
                    "--raw "+$"[FIELD CHANGE] {Shenanigans._lastValue ?? "null"} → {current ?? "null"}"
                );

                // optional: stack trace (heavy, but useful)
                UnityEngine.Debug.Log(Environment.StackTrace);

                Shenanigans._lastValue = current;
            }
#endif
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
            List<(TransitionDefinition In, TransitionDefinition Out)> pairs = [];
            Dictionary<TransitionType, List<(TransitionDefinition In, TransitionDefinition Out)>> pairsByType = [];

            foreach (var transition in allTransitions)
            {
                // Skip if we already added this pair
                if (pairs.Any(p => p.In == transition || p.Out == transition)) continue;

                var counterpart = allTransitions.FirstOrDefault(link =>
                    link.exitPoint == transition.linkedPoint &&
                    link.toScene == transition.fromScene);

                if (counterpart != null)
                {
                    pairs.Add((transition, counterpart));
                }
                else
                {
                    pairs.Add((transition, transition));
                    Log(System.ConsoleColor.Red, $"No pair found for {transition.exitPoint} in scene {transition.fromScene}");
                }

                if (Settings.options.shuffleMode != 0) // only make by type dict when needed
                {
                    pairsByType.TryAdd(transition.type, new());
                    pairsByType[transition.type].Add((transition, counterpart ?? transition));
                }
            }

            sw.Stop();
            Log(CC.Cyan, $"Generated {pairs.Count} transition pairs in {sw.Elapsed.TotalMilliseconds:F2} ms");
            sw.Restart();

            // pair transition pairs to randomized pairs
            Dictionary<(TransitionDefinition AIn, TransitionDefinition AOut), (TransitionDefinition BIn, TransitionDefinition BOut)> pairedPairs = [];

            if (Settings.options.shuffleMode == 0) // no logic, just shuffle everything
            {
                /*
                List<(TransitionDefinition InClone, TransitionDefinition OutClone)> pairsClone = new(pairs);

                foreach (var pair in pairs)
                {
                    int index = rng.Next(pairsClone.Count);
                    var randomPair = pairsClone[index];
                    pairsClone.RemoveAt(index);

                    pairedPairs[pair] = randomPair;
                }
                */

                List<(TransitionDefinition InClone, TransitionDefinition OutClone)> pairsClone = new(pairs); // made by Claude

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

                if (pairsClone.Count % 2 != 0)
                {
                    Log(CC.Yellow, $"Odd pair count. Transition to {pairsClone[^1].InClone.toScene}: {pairsClone[^1].InClone.exitPoint} in {pairsClone[^1].InClone.fromScene} left upaired");
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
                foreach (var pair in pairs)
                {
                    var validTransitions = pairsByType[pair.In.type]; // look for same type transition to replace with

                    bool isIndoorsToIndoors = pair.Out.type == TransitionType.ToIndoors && pair.In.type == TransitionType.ToIndoors;
                    bool isCaveToCave = pair.Out.type == TransitionType.ToCave && pair.In.type == TransitionType.ToCave;
                    bool canUseCounterpart = pair.In.type == TransitionType.ToOutdoorsFromIndoors || pair.In.type == TransitionType.ToOutdoorsFromCave;

                    // if same type transition both ways, switch to counterparts, otherwise there's not enough pairs for normal transitions
                    if ((isIndoorsToIndoors || isCaveToCave)) 
                    {
                        //Log(CC.DarkGray, $"Switching to counterpart type for {pair.In.type} when pairing {pair.In.linkedPoint} in scene {pair.In.fromScene}");
                        validTransitions = pairsByType[validMatches[pair.In.type]]; 
                    }

                    bool poolEmpty = validTransitions.Count == 0 || (validTransitions.Count == 1 && validTransitions[0] == pair);

                    // chance switch in counterpart for types that can be harmlessly switched to counterparts
                    if (canUseCounterpart && (poolEmpty || rng.Next(100) < chanceRollSameTransitionType))
                    {
                        //Log(CC.DarkGray, $"Rolling for counterpart for {pair.In.type} when pairing {pair.In.linkedPoint} in scene {pair.In.fromScene}");
                        validTransitions = pairsByType[validMatches[pair.In.type]];
                        poolEmpty = validTransitions.Count == 0 || (validTransitions.Count == 1 && validTransitions[0] == pair);
                    }

                    // still no valid transitions for the picked type left
                    if (poolEmpty)
                    {
                        n++;
                        //Log(CC.Red, $"No valid transitions available for pairing {pair.Out.linkedPoint} in scene {pair.Out.fromScene}, skipping linking");
                        continue;
                    }

                    int selfIndex = validTransitions.IndexOf(pair);
                    int index = 0;

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

                    var picked = validTransitions[index];

                    validTransitions.RemoveAt(index); // remove from available pool to prevent reuse
                    pairedPairs[pair] = picked;
                    //Log(CC.Green, $"Paired {pair.In.type} -> {picked.In.type} ({pair.In.linkedPoint} in scene {pair.In.fromScene} with {picked.In.linkedPoint} in scene {picked.In.fromScene})");
                }

                if (n > 0)
                {
                    Log(CC.Yellow, $"{n} pairs had no valid matches and were left unchanged");
                }

                sw.Stop();
                Log(CC.Cyan, $"Shuffled pairs for 'Reasonable' preset in {sw.Elapsed.TotalMilliseconds:F2} ms");
            }

            //RunOddTransitionLookup(pairedPairs);
            sw.Restart();

            // swap A and B points to get final transition replacement map
            Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> result = [];

            foreach (var pairOfPairs in pairedPairs)
            {
                var A = pairOfPairs.Key;
                var B = pairOfPairs.Value;

                // A's In remapped to B's In
                result.TryAdd(A.AIn.fromScene, new());
                if (!(result[A.AIn.fromScene].ContainsKey(A.AIn)))
                {
                    result[A.AIn.fromScene][A.AIn] = B.BIn;
                    //Log(System.ConsoleColor.Green, $"Linking  {A.AIn.linkedPoint} in {A.AIn.fromScene} with {B.BIn.exitPoint} in {B.BIn.toScene}");
                }

                // B's Out mirrored back to A's Out
                result.TryAdd(B.BOut.fromScene, new());
                if (!(result[B.BOut.fromScene].ContainsKey(B.BOut)))
                {
                    result[B.BOut.fromScene][B.BOut] = A.AOut;
                    //Log(System.ConsoleColor.Blue, $"Mirroring  {B.BOut.exitPoint} in {B.BOut.toScene} to {A.AOut.linkedPoint} in {A.AOut.fromScene}");
                }
            }

            sw.Stop();
            Log(CC.Cyan, $"Finalizing result dictionary in {sw.Elapsed.TotalMilliseconds:F2} ms");

            int total = result.Sum(r => r.Value.Count);

            Log(total == allTransitions.Count ? CC.Green : CC.Red, $"{total} pairs vs {allTransitions.Count} total transitions ({allTransitions.Count - total} went MIA)");
            Log();

            RunSymmetryDiagnostics(result);

            return result;

           
        }
    }     
}




