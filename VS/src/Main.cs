global using static Randomizer.Utility;
using Harmony;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using static UnityEngine.UI.Image;
using static UnityEngine.UI.Selectable;
using Random = System.Random;

namespace Randomizer
{
    public enum TransitionType
    {
        Irrelevant = 0,
        ToCave,
        ToIndoors,
        ToBunker,
        ToOutdoorsFromCave,
        ToOutdoorsFromIndoors,
        ToOutdoorsFromBunker,
        ToOutdoorsFromOutdoors
    }
    public record TransitionDefinition
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransitionType type;
        public string fromScene;
        public string exitPoint;
        public string toScene;
        public string linkedPoint;
        public bool unique = true;
    }

    public class Main : MelonMod
    {
        public bool isLoaded = false;

        public static string modsPath;

        private static int chanceRollSameTransitionType = 12;

        public static Dictionary<string, TransitionDefinition[]> transitions = new();
        public static Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> rolledPairs = new(); // origonal scene, <transition info > replacer transition info>

        public override void OnInitializeMelon()
        {
            modsPath = Path.GetFullPath(typeof(MelonMod).Assembly.Location + "/../../../Mods/");

            Settings.OnLoad();

            transitions = JsonSerializer.Deserialize<Dictionary<string, TransitionDefinition[]>>(LoadEmbeddedJSON("Transitions.json"), GetDefaultJsonOptions()) ?? new();

            rolledPairs = RollPairs(Settings.options.seed);

            //RunTransitionDictionaryIntegrityCheck();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {

        }

        public static void RunTransitionDictionaryIntegrityCheck()
        {
            Dictionary<string, int> countExit = new();
            Dictionary<string, int> shortCountLink = new();
            Dictionary<string, int> countLink = new();
            Dictionary<string, string> pairs = new();
            Dictionary<string, int> linkScene = new();
            foreach (var scene in transitions)
            {
                foreach (var transition in scene.Value)
                {
                    if (string.IsNullOrEmpty(transition.exitPoint))
                    {
                        Log(System.ConsoleColor.Red, $"Transition in scene {scene.Key} is missing an EXIT point");
                        continue;
                    }
                    else if (string.IsNullOrEmpty(transition.linkedPoint))
                    {
                        Log(System.ConsoleColor.Red, $"Transition to {transition.exitPoint} in scene {scene.Key} is missing a LINKED point");
                        continue;
                    }
                    if (!pairs.ContainsKey(transition.exitPoint))
                    { 
                        pairs[transition.exitPoint] = transition.linkedPoint;
                    }
                    if (!linkScene.ContainsKey(transition.exitPoint + "|" + transition.toScene))
                    {
                        linkScene[transition.exitPoint + "|" + transition.toScene] = 1;
                    }
                    else
                    {
                        linkScene[transition.exitPoint + "|" + transition.toScene]++;
                    }

                    if (!countExit.ContainsKey(transition.exitPoint))
                    {
                        countExit[transition.exitPoint] = 1;
                    }
                    else
                    {
                        countExit[transition.exitPoint]++;
                    }                    
                    
                    if (!shortCountLink.ContainsKey(transition.linkedPoint))
                    {
                        shortCountLink[transition.linkedPoint] = 1;
                    }
                    else
                    {
                        shortCountLink[transition.linkedPoint]++;
                    }

                    if (!countLink.ContainsKey(transition.linkedPoint))
                    {
                        countLink[transition.linkedPoint] = 1;
                    }
                    else
                    {
                        countLink[transition.linkedPoint]++;
                    }
                }

                foreach (var kvp in shortCountLink)
                {
                    if (kvp.Value > 1)
                    {
                        Log(System.ConsoleColor.Red, $"Scene {scene.Key} has {kvp.Value} {kvp.Key} points");
                    }
                }

                shortCountLink.Clear();

            }
            var sortedDict = new SortedDictionary<string, int>(linkScene);
            
            foreach (var kvp in sortedDict)
            {
                if (kvp.Value > 1)
                {
                    Log(System.ConsoleColor.Yellow, $"Point {kvp.Key.Split("|")[0]} leading to scene {kvp.Key.Split("|")[1]} is linked to multiple exit points");
                }
            }
            int count = 0;
            foreach (var kvp in countExit)
            {

                count++;
                if (countLink.TryGetValue(kvp.Key, out var _))
                {
                    if (pairs.TryGetValue(kvp.Key, out string link))
                    {
                        if (((kvp.Value + countLink[kvp.Key]) + (countExit[link] + countLink[link])) % 2 != 0)
                        {
                            Log(System.ConsoleColor.Blue, $"Uneven pairs for {kvp.Key}");
                        }
                    }
                    else
                    {
                        Log(System.ConsoleColor.Magenta, $"Exit point {kvp.Key} is missing a link pair");
                    }

                    if (kvp.Value > 1)
                    {
                        //Log(System.ConsoleColor.Yellow, $"Exit point {kvp.Key} is not unique");
                    }
                }
                else
                {
                    Log(System.ConsoleColor.Red, $"Exit point {kvp.Key} is not linked to anywhere");
                }


            }

            Log(System.ConsoleColor.Gray, $"Check complete for {count} transitions");
            Log(System.ConsoleColor.Gray);
        }

        public static Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> RollPairs(int seed)
        {
            // flatten dict and create new one sorted by transition type for easier pairing logic
            List<TransitionDefinition> allTransitions = new();

            foreach (var scenes in transitions)
            {
                foreach (var transition in scenes.Value)
                {
                    transition.fromScene = scenes.Key; // add original scene info to transition definition for later reference
                    allTransitions.Add(transition);
                }
            }

            // one-way pair transitions to their linked counterparts (exits to enters)
            List<(TransitionDefinition In, TransitionDefinition Out)> pairs = [];
            Dictionary<TransitionType, List<(TransitionDefinition In, TransitionDefinition Out)>> pairsByType = new();

            foreach (var transition in allTransitions)
            {
                // Skip if we already added this pair
                if (pairs.Any(p => p.In == transition)) continue;

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

            // pair transition pairs to randomized pairs
            var pairedPairs = new Dictionary<(TransitionDefinition AIn, TransitionDefinition AOut), (TransitionDefinition BIn, TransitionDefinition BOut)>();
            var rnd = new Random(seed);

            if (Settings.options.shuffleMode == 0) // no logic, just shuffle everything
            {
                var pairsClone = new List<(TransitionDefinition InClone, TransitionDefinition OutClone)>(pairs);

                foreach (var pair in pairs)
                {
                    int index = rnd.Next(pairsClone.Count);
                    var randomPair = pairsClone[index];
                    pairsClone.RemoveAt(index);

                    pairedPairs[pair] = randomPair;
                }
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
                
                foreach (var pair in pairs)
                {
                    var validTransitions = pairsByType[validMatches[pair.Out.type]];
                    bool canUseSameType = pair.Out.type == TransitionType.ToIndoors || pair.Out.type == TransitionType.ToCave;

                    if (canUseSameType && (validTransitions.Count == 0 || UnityEngine.Random.Range(0, 100) < chanceRollSameTransitionType))
                    {
                        pairsByType.TryGetValue(pair.Out.type, out var fallback);
                        if (fallback?.Count > 0)
                        {
                            validTransitions = fallback;
                            Log(CC.Yellow, $"Choosing same type transition for {pair.Out.type} when pairing {pair.Out.linkedPoint} in scene {pair.Out.fromScene}");
                        }
                    }

                    if (validTransitions.Count == 0)
                    {
                        Log(CC.Red, $"No valid transitions available for pairing {pair.Out.linkedPoint} in scene {pair.Out.fromScene}, skipping linking");
                        continue;
                    }

                    int index = rnd.Next(validTransitions.Count);
                    var picked = validTransitions[index];

                    validTransitions.RemoveAt(index); // remove from available pool to prevent reuse

                    pairedPairs[pair] = picked;
                }
            }

            // swap A and B points to get final transition replacement map
            Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> result = new();

            foreach (var pairOfPairs in pairedPairs)
            {
                var A = pairOfPairs.Key;
                var B = pairOfPairs.Value;

                // A's In remapped to B's Out
                result.TryAdd(A.AIn.fromScene, new());
                if (!result[A.AIn.fromScene].ContainsKey(A.AIn))
                {
                    result[A.AIn.fromScene][A.AIn] = B.BIn;
                }


                // B's In remapped to A's Out
                result.TryAdd(B.BOut.fromScene, new());
                if (!result[B.BOut.fromScene].ContainsKey(B.BOut))
                {
                    result[B.BOut.fromScene][B.BOut] = A.AOut;
                }

                Log(System.ConsoleColor.Green, $"Linking  {A.AIn.linkedPoint} in {A.AIn.fromScene} with {B.BIn.exitPoint} in {B.BIn.toScene}");
                Log(System.ConsoleColor.Gray, $"Mirroring  {A.AOut.exitPoint} in {A.AOut.toScene} to {B.BOut.linkedPoint} in {B.BOut.fromScene}");
            }
            int total = result.Sum(r => r.Value.Count);

            Log(CC.Red, $"{total} pairs vs {allTransitions.Count}");
            Log();

            return result;

           
        }

        /*

        public static Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> RollPairs(int seed)
        {






            // look through al exit points, and if there are duplicates AND the lead to the same scene - do something smart ok
            // this is for stuff like trailers, where there are multiple of them
            // move them to mltiregion category
            // if more than 2 of exit point pairs in dict ^









            List<(TransitionDefinition transition, string originalScene)> allTransitions = new(); // store original scene in tuple for later reference

            foreach (var scenes in transitions)
            {
                foreach (var transition in scenes.Value)
                {
                    allTransitions.Add((transition, scenes.Key));
                }
            }

            // Deterministic shuffle
            var rng = new Random(seed);

            for (int i = allTransitions.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                var temp = allTransitions[i];
                allTransitions[i] = allTransitions[j];
                allTransitions[j] = temp;
            }

            
            // graym > caveb
            //
            // pair elements into dictionary
            var pairsPerRegion = new Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>>();

            for (int i = 0; i < allTransitions.Count - 1; i += 2)
            {
                var v = allTransitions[i]; // vanilla transition identifier   milton graym tuple
                var m = allTransitions[i + 1]; // modded transition identifier   rural caveb tuple

                // A scene
                TransitionDefinition originV = v.transition;// into graym
                TransitionDefinition originM = m.transition;// into caveb

                // B scene
                TransitionDefinition destinationV = new TransitionDefinition() { // out of caveb to rural
                    toScene = m.originalScene, 
                    exitPoint = m.transition.linkedPoint, 
                    linkedPoint = m.transition.exitPoint };
                TransitionDefinition destinationM = new TransitionDefinition() { // out of graym to milton
                    toScene = v.originalScene, 
                    exitPoint = v.transition.linkedPoint, 
                    linkedPoint = v.transition.exitPoint };

                // scenes that were the original destinations of A and B
                TransitionDefinition originPantomV = destinationM; // out of graym to milton
                TransitionDefinition destinationPhantomV = originM; // into caveb

                // mod destination in A scene
                pairsPerRegion.TryAdd(v.originalScene, new()); // milton
                if (pairsPerRegion[v.originalScene].ContainsKey(originV))
                {
                    Log(CC.Red,$"Duplicate key detected: {originV.exitPoint} in {v.originalScene}" );
                }
                pairsPerRegion[v.originalScene][originV] = originM; // into graym > into caveb
                //Log(System.ConsoleColor.White, $"____{v.originalScene}____");
                //Log(System.ConsoleColor.DarkMagenta, $"┌--{originV.toScene}: {originV.exitPoint}");
                //Log(System.ConsoleColor.Magenta, $"└▷ {originM.toScene}: {originM.exitPoint}");

                // mod destination in B scene
                pairsPerRegion.TryAdd(m.transition.toScene, new()); // caveb
                if (pairsPerRegion[m.transition.toScene].ContainsKey(destinationV))
                {
                    Log(CC.Red, $"Duplicate key detected: {destinationV.exitPoint} in {m.transition.toScene}");
                }
                pairsPerRegion[m.transition.toScene][destinationV] = destinationM; // out of caveb to rural > out of graym to milton
                //Log(System.ConsoleColor.White, $"____{m.transition.toScene}____");
                //Log(System.ConsoleColor.DarkMagenta, $"┌--{destinationV.toScene}: {destinationV.exitPoint}");
                //Log(System.ConsoleColor.Magenta, $"└▷ {destinationM.toScene}: {destinationM.exitPoint}");

                // mod destination in scene that A led to iriginally
                //pairsPerRegion.TryAdd(v.transition.toScene, new()); // graym
                //pairsPerRegion[v.transition.toScene][originPantomV] = destinationV; // out of graym to milton > out of caveb to rural
                //Log(System.ConsoleColor.White, $"____{v.transition.toScene}____");
                //Log(System.ConsoleColor.DarkMagenta, $"┌--{originPantomV.toScene}: {originPantomV.exitPoint}");
                //Log(System.ConsoleColor.Magenta, $"└▷ {destinationV.toScene}: {destinationV.exitPoint}");

                // mod destination in scene that B led to originally
                //pairsPerRegion.TryAdd(m.originalScene, new()); // rural
                //pairsPerRegion[m.originalScene][destinationPhantomV] = originV; // into caveb > into graym
                //Log(System.ConsoleColor.White, $"____{m.originalScene}____");
                //Log(System.ConsoleColor.DarkMagenta, $"┌--{destinationPhantomV.toScene}: {destinationPhantomV.exitPoint}");
                //Log(System.ConsoleColor.Magenta, $"└▷ {originV.toScene}: {originV.exitPoint}");
                //Log();
            }

            // Optional: if odd number of elements, last one points to itself
            if (allTransitions.Count % 2 != 0)
            {
                //dict[allTransitions.Last()] = allTransitions.Last();
            }
            int total = pairsPerRegion.Sum(r => r.Value.Count);
            Log(CC.Red,$"{total} pairs vs {allTransitions.Count}");
            Log();

            return pairsPerRegion;
        }

        
        */




        public static void Dump() // to use directly in UE
        {


            var b = new StringBuilder();
            b.AppendLine($"  \"{GameManager.m_ActiveScene}\": [");
            string c = "";
            var collection = UnityEngine.Object.FindObjectsOfType<LoadScene>(true); 
            var last = collection.Last();

            foreach (var a in collection)
            {
                string link = "";

                if (a.m_SceneToLoad.ToLower().Contains("cave") || a.m_SceneToLoad.ToLower().Contains("mine"))
                {
                    c = "ToCave";
                }
                else if (a.m_SceneToLoad.ToLower().Contains("cache") || a.m_SceneToLoad.ToLower().Contains("bunker"))
                {
                    c = "ToBunker";
                }
                else if (a.m_SceneToLoad.ToLower().Contains("region") || a.m_SceneToLoad.ToLower().Contains("transition"))
                {
                    if (GameManager.m_ActiveScene.ToLower().Contains("cave") || GameManager.m_ActiveScene.ToLower().Contains("mine"))
                    {
                        c = "ToOutdoorsFromCave";
                    }
                    else if (GameManager.m_ActiveScene.ToLower().Contains("region") || GameManager.m_ActiveScene.ToLower().Contains("transition"))
                    {
                        c = "ToOutdoorsFromOutdoors";
                    }
                    else if (GameManager.m_ActiveScene.ToLower().Contains("bunker") || GameManager.m_ActiveScene.ToLower().Contains("cache"))
                    {
                        c = "ToOutdoorsFromBunker";
                    }
                    else
                    {
                        c = "ToOutdoorsFromIndoors";
                    }
                }
                else
                {
                    c = "ToIndoors";
                }
                int i = 0;
                if (Randomizer.Main.transitions.TryGetValue(GameManager.m_ActiveScene, out var t1))
                {
                    foreach (var transition in t1)
                    {
                        if (transition.exitPoint == a.m_ExitPointName)
                        {
                            i++;
                        }
                    }
                }

                if (i > 0) // skip existing
                {
                    continue;
                }

                if (Randomizer.Main.transitions.TryGetValue(a.m_SceneToLoad, out var t))
                {
                    foreach (var transition in t)
                    {

                        //if (transition.toScene.ToLower().Contains(GameManager.m_ActiveScene.ToLower()))
                        //{
                            if (transition.exitPoint.ToLower().Replace("exitpoint", "").Replace("enterpoint", "")
                                .Contains(a.m_ExitPointName.ToLower().Replace("exitpoint", "").Replace("enterpoint", "")))
                            {
                                link = transition.exitPoint;
                                break;
                            }
                        //}
                    }
                }

                b.AppendLine($"    {{");
                b.AppendLine($"      \"exitPoint\": \"{a.m_ExitPointName}\",");
                b.AppendLine($"      \"toScene\": \"{a.m_SceneToLoad}\",");
                b.AppendLine($"      \"type\": \"{c}\",");
                b.AppendLine($"      \"linkedPoint\": \"{link}\"");
                if (a == last)
                {
                    b.AppendLine($"    }}");
                }
                else
                {
                    b.AppendLine($"    }},");
                }
            }

            GUIUtility.systemCopyBuffer = b.AppendLine($"  ]").ToString();


        }
    }


     
}




