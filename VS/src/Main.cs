global using static Randomizer.Utility;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        public string? exitPoint;
        public string? toScene;
        public string? linkedPoint;
        public bool unique = true;
    }



    public class Main : MelonMod
    {
        public bool isLoaded = false;

        public int theSeed = 42;
        
        public static string modsPath;

        public static Dictionary<string, TransitionDefinition[]> transitions = new();
        public static Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> rolledPairs = new();

        public override void OnInitializeMelon()
        {
            modsPath = Path.GetFullPath(typeof(MelonMod).Assembly.Location + "/../../../Mods/");

            Settings.OnLoad();

            transitions = JsonSerializer.Deserialize<Dictionary<string, TransitionDefinition[]>>(LoadEmbeddedJSON("Transitions.json"), GetDefaultJsonOptions()) ?? new();

            //rolledPairs = RollPairs(theSeed);

            RunTransitionDictionaryIntegrityCheck();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (IsScenePlayable(sceneName) && rolledPairs.TryGetValue(sceneName, out var kvp))
            {
                foreach (var comp in UnityEngine.Object.FindObjectsOfType<LoadScene>())
                {
                    // check guid, and if already replaced - reroll and add to dict with guid


                    // preselect some inconsistent buildings to be always present, defined by the seed. This is for stuff like prepper caches and non-unique houses/basements


                    if (kvp.TryGetValue(new TransitionDefinition { toScene = comp.m_SceneToLoad, exitPoint = comp.m_ExitPointName }, out var newTransition))
                    {
                        Log(System.ConsoleColor.Blue, $"Replaced transition {comp.m_SceneToLoad} in scene {sceneName} to {newTransition.toScene}");
                        comp.m_SceneToLoad = newTransition.toScene;
                        comp.m_ExitPointName = newTransition.exitPoint;
                        
                    }
                }
            }
        }

        public static void RunTransitionDictionaryIntegrityCheck()
        {
            Dictionary<string, int> countExit = new();
            Dictionary<string, int> shortCountLink = new();
            Dictionary<string, int> countLink = new();
            foreach (var scene in transitions)
            {
                foreach (var transition in scene.Value)
                {
                    if (string.IsNullOrEmpty(transition.exitPoint))
                    {
                        Log(System.ConsoleColor.Red, $"Transition in scene {scene.Key} is missing an EXIT point");
                        continue;
                    }
                    if (string.IsNullOrEmpty(transition.linkedPoint))
                    {
                        Log(System.ConsoleColor.Red, $"Transition in scene {scene.Key} is missing a LINKED point");
                        continue;
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

            int count = 0;
            foreach (var kvp in countExit)
            {
                count++;
                if (countLink.TryGetValue(kvp.Key, out var _))
                { 
                    if (kvp.Value != countLink[kvp.Key])
                    {
                        Log(System.ConsoleColor.Red, $"Exit point {kvp.Key} has missmatched links, there are {kvp.Value} exits vs {countLink[kvp.Key]} links");
                    }
                    if (kvp.Value > 1)
                    {
                        Log(System.ConsoleColor.Yellow, $"Exit point {kvp.Key} is not unique");
                    }
                }
                else
                {
                    Log(System.ConsoleColor.Red, $"Exit point {kvp.Key} is missing a corresponding linked point");
                }


            }
            Log(System.ConsoleColor.Gray, $"Check complete for {count} transitions");
            Log(System.ConsoleColor.Gray);
        }

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



            // pair elements into dictionary (jfc my head)
            var dict = new Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>>();
            for (int i = 0; i < allTransitions.Count - 1; i += 2)
            {
                TransitionDefinition popo = allTransitions[i].transition;
                popo.type = TransitionType.Irrelevant;
                TransitionDefinition pepe = allTransitions[i + 1].transition;
                pepe.type = TransitionType.Irrelevant;

                if (!dict.ContainsKey(allTransitions[i].originalScene))
                {
                    dict[allTransitions[i].originalScene] = new();
                }
                dict[allTransitions[i].originalScene][popo] = pepe;

                if (!dict.ContainsKey(allTransitions[i + 1].originalScene))
                {
                    dict[allTransitions[i + 1].originalScene] = new();
                }
                dict[allTransitions[i + 1].originalScene][pepe] = popo;
            }

            // Optional: if odd number of elements, last one points to itself
            if (allTransitions.Count % 2 != 0)
            {
                //dict[allTransitions.Last()] = allTransitions.Last();
            }

            return dict;
        }

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




