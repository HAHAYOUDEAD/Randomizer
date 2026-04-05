using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.UI.Selectable;

namespace Randomizer
{
    internal class Debug
    {

        //       ___       __         ____                     _          __  _         
        //      / _ \___ _/ /____ _  / __ \_______ ____ ____  (_)__ ___ _/ /_(_)__  ___ 
        //     / // / _ `/ __/ _ `/ / /_/ / __/ _ `/ _ `/ _ \/ /_ // _ `/ __/ / _ \/ _ \
        //    /____/\_,_/\__/\_,_/  \____/_/  \_, /\_,_/_//_/_//__/\_,_/\__/_/\___/_//_/
        //                                   /___/                                      

        public static readonly string[] InstantiatibleRegions = {
            "AshWoodCabinA",
            "BankA",
            "BarnHouseB",
            "CoastalHouseA",
            "CoastalHouseB",
            "CoastalHouseC",
            "CoastalHouseD",
            "CoastalHouseE",
            "CoastalHouseF",
            "CoastalHouseH",
            "DamTrailerB",
            "FishingCabinA",
            "FishingCabinC",
            "FishingCabinD",
            "HouseBasementC",
            "LakeCabinA",
            "LakeCabinB",
            "LakeCabinC",
            "LakeCabinD",
            "LakeCabinE",
            "LakeCabinF",
            "LakeRegion",
            "MiltonHouseA",
            "MiltonHouseF1",
            "MiltonHouseF2",
            "MiltonHouseF3",
            "TrailerA",
            "TrailerB",
            "TrailerC",
            "TrailerD",
            "TrailerE",
            "WoodCabinA",
            "WoodCabinB",
            "WoodCabinC"
        };

        public static readonly string[] TFTFTRegions = {
            "AFHangar",
            "AirfieldRegion",
            "AirfieldTrailerB",
            "AirfieldWoodCabinA",
            "BunkerA",
            "BunkerB",
            "BunkerC",
            "BunkerXL",
            "HubCave",
            "HubRegion",
            "MineConcentratorBuilding",
            "MiningRegion",
            "MiningRegionMine",
            "MountainPassBasement",
            "MountainPassBuriedCabin",
            "MountainPassCabinA",
            "MountainPassCaveA",
            "MountainPassCaveB",
            "MountainPassRegion",
            "LongRailTransitionZone",
            "LongTransitionCave",
            "PumpHouse",
            "RadarBuilding"
        };

        public static readonly string[] VanillaRegions =
        {
            "AshCabinD",                  "AshCabinF",                   "AshCanyonRegion",           "AshCaveA",
            "AshCaveB",                   "AshMine",                     "AshWoodCabinA",             "BankA",
            "BarnHouseA",                 "BarnHouseB",                  "BlackRockTrailerB",         "BlackrockCaveA",
            "BlackrockInteriorASurvival", "BlackrockMineA",              "BlackrockPowerplantA",      "BlackrockPrisonSurvivalZone",
            "BlackrockRegion",            "BlackrockTransitionZone",     "CampOffice",                "BlackrockSteamTunnelsASurvival",
            "CanneryMarshTransitionCave", "CanneryRegion",               "CanneryTrailerA",           "CanyonRoadCave",
            "CanyonRoadTransitionZone",   "CaveB",                       "CaveC",                     "CaveD",
            "ChurchB",                    "ChurchC",                     "CoastalHouseA",             "CoastalHouseB",
            "CoastalHouseC",              "CoastalHouseD",               "CoastalHouseE",             "CoastalHouseF",
            "CoastalHouseH",              "CoastalRegion",               "CommunityHallA",            "ConvenienceStoreA",
            "CrashMountainRegion",        "Dam",                         "DamCaveTransitionZone",     "DamRiverTransitionZoneB",
            "DamTrailerB",                "DamTransitionZone",           "FarmHouseA",                "FarmHouseABasement",
            "FarmHouseB",                 "FishingCabinA",               "FishingCabinC",             "FishingCabinD",
            "GreyMothersHouseA",          "HighwayMineTransitionZone",   "HighwayTransitionZone",     "HouseBasementC",
            "HouseBasementPV",            "HuntingLodgeA",               "IceCaveA",                  "IceCaveB",
            "LakeCabinA",                 "LakeCabinB",                  "LakeCabinC",                "LakeCabinD",
            "LakeCabinE",                 "LakeCabinF",                  "LakeRegion",                "LighthouseA",
            "MaintenanceShedA",           "MaintenanceShedB",            "MarshRegion",               "MiltonHouseA",
            "MiltonHouseC",               "MiltonHouseD",                "MiltonHouseF1",             "MiltonHouseF2",
            "MiltonHouseF3",              "MiltonHouseH1",               "MiltonHouseH2",             "MiltonHouseH3",
            "MiltonTrailerB",             "MineTransitionZone",          "MountainCaveA",             "MountainCaveB",
            "MountainTownCaveA",          "MountainTownCaveB",           "MountainTownRegion",        "PostOfficeA",
            "PrepperCacheA",              "PrepperCacheAEmpty",          "PrepperCacheB",             "PrepperCacheBEmpty",
            "PrepperCacheBInterloper",    "PrepperCacheC",               "PrepperCacheCEmpty",        "PrepperCacheD",
            "PrepperCacheDEmpty",         "PrepperCacheE",               "PrepperCacheEEmpty",        "PrepperCacheEmpty",
            "PrepperCacheF",              "PrepperCacheFEmpty",          "QuonsetGasStation",         "RadioControlHut",
            "RadioControlHutB",           "RadioControlHutC",            "RavineTransitionZone",      "RiverValleyRegion",
            "RiverValleyTransitionCave",  "RuralRegion",                 "RuralStoreA",               "SafeHouseA",
            "TracksRegion",               "TrailerA",                    "TrailerB",                  "TrailerC",
            "TrailerD",                   "TrailerE",                    "TrailerSShape",             "WhalingMine",
            "WhalingShipA",               "WhalingStationRegion",        "WhalingWarehouseA",         "WoodCabinA",
            "WoodCabinB",                 "WoodCabinC"

            };

        public static readonly string[] RegionSizeByNumUniqueTransitions =
        {
            "MountainTownRegion",              // 26
            "RuralRegion",                     // 21
            "MiningRegion",                    // 14
            "WhalingStationRegion",            // 13
            "MountainPassRegion",              // 12
            "CoastalRegion",                   // 11
            "MineConcentratorBuilding",        // 11
            "AshCanyonRegion",                 // 10
            "BlackrockRegion",                 // 10
            "AirfieldRegion",                  // 8
            "CanneryRegion",                   // 8
            "CrashMountainRegion",             // 8
            "LakeRegion",                      // 8
            "RiverValleyRegion",               // 8
            "MiningRegionMine",                // 7
            "TracksRegion",                    // 7
            "MarshRegion",                     // 6
            "MineTransitionZone",              // 5
            "FarmHouseA",                      // 4
            "HubRegion",                       // 4
            "LongRailTransitionZone",          // 4
            "MountainPassCaveA",               // 4
            "AFHangar",                        // 3
            "BlackrockPrisonSurvivalZone",     // 3
            "HubCave",                         // 3
            "IceCaveA",                        // 3
            "IceCaveB",                        // 3
            "MaintenanceShedA",                // 3
            "RavineTransitionZone",            // 3
            "TrailerSShape",                   // 3
            "AshCaveA",                        // 2
            "AshCaveB",                        // 2
            "AshMine",                         // 2
            "BarnHouseA",                      // 2
            "BlackrockCaveA",                  // 2
            "BlackrockMineA",                  // 2
            "BlackrockPowerplantA",            // 2
            "BlackrockSteamTunnelsASurvival",  // 2
            "BlackrockTransitionZone",         // 2
            "CampOffice",                      // 2
            "CanneryMarshTransitionCave",      // 2
            "CanyonRoadCave",                  // 2
            "CanyonRoadTransitionZone",        // 2
            "CaveD",                           // 2
            "CoastalHouseB",                   // 2
            "CoastalHouseC",                   // 2
            "CommunityHallA",                  // 2
            "ConvenienceStoreA",               // 2
            "Dam",                             // 2
            "DamCaveTransitionZone",           // 2
            "DamRiverTransitionZoneB",         // 2
            "DamTransitionZone",               // 2
            "FarmHouseABasement",              // 2
            "HighwayMineTransitionZone",       // 2
            "HighwayTransitionZone",           // 2
            "HuntingLodgeA",                   // 2
            "LighthouseA",                     // 2
            "LongTransitionCave",              // 2
            "MiltonHouseC",                    // 2
            "MiltonHouseD",                    // 2
            "MiltonHouseH1",                   // 2
            "MiltonHouseH2",                   // 2
            "MiltonHouseH3",                   // 2
            "MountainCaveA",                   // 2
            "MountainCaveB",                   // 2
            "MountainPassCaveB",               // 2
            "MountainTownCaveA",               // 2
            "MountainTownCaveB",               // 2
            "PumpHouse",                       // 2
            "QuonsetGasStation",               // 2
            "RiverValleyTransitionCave",       // 2
            "RuralStoreA",                     // 2
            "WhalingMine",                     // 2
            "WhalingShipA",                    // 2
            "WhalingWarehouseA"                // 2
        };

        public static void BuildData()
        {
            List<string> regions = [];
            regions.AddRange(VanillaRegions);
            regions.AddRange(TFTFTRegions);
            regions.Sort();

            Dictionary<string, int> sizes = [];

            foreach (var kvp in transitions)
            {
                sizes.TryAdd(kvp.Key, 0);
                sizes[kvp.Key] += kvp.Value.Count();
            }
            foreach (var kvp in inconsistentTransitions)
            {
                sizes.TryAdd(kvp.Key, 0);
                sizes[kvp.Key] += kvp.Value.Count();
            }

            foreach (var region in regions)
            {
                string name = region;
                Version version = TFTFTRegions.Contains(region) ? Version.TFTFT : Version.Vanilla;
                int size = sizes.TryGetValue(region, out var s) ? s : 0;
                bool instantiable = !InstantiatibleRegions.Contains(region);
                bool unique = transitions.ContainsKey(region);
                string spaces = new string(' ', 31 - name.Length);
                int numspacesAfterVersion = version == Version.TFTFT ? 3 : 1;
                string moreSpaces = new string(' ', numspacesAfterVersion);
                int numspacesAfterSize = size < 10 ? 2 : 1;
                string moreMoreSpaces = new string(' ', numspacesAfterSize);
                int numspacesAfterUnique = unique ? 2 : 1;
                string evenMoreSpaces = new string(' ', numspacesAfterUnique);
                int numspacesAfterInstantiable = instantiable ? 2 : 1;
                string evenEvenMoreSpaces = new string(' ', numspacesAfterInstantiable);

                Log(CC.Gray, "--raw " + $"{{ \"{name}\",{spaces}new SceneData {{ version = Version.{version},{moreSpaces}size = {size},{moreMoreSpaces}unique = {unique.ToString().ToLower()},{evenMoreSpaces}instantiable = {instantiable.ToString().ToLower()}{evenEvenMoreSpaces}}} }},");
            }

        }

        //       ___                           __ 
        //      / _ \___ ___ ___ ___ _________/ / 
        //     / , _/ -_|_-</ -_) _ `/ __/ __/ _ \
        //    /_/|_|\__/___/\__/\_,_/_/  \__/_//_/
        //                                        

        public static bool breakCoroutines = false;

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
                if (Randomizer.Data.transitions.TryGetValue(GameManager.m_ActiveScene, out var t1))
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

                if (Randomizer.Data.transitions.TryGetValue(a.m_SceneToLoad, out var t))
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

        public static void Run_PrintDataFromAllTransitions() => MelonCoroutines.Start(RunThroughAllScenesAndOperateOnTransitions(PrintDataFromTransition));

        internal static IEnumerator RunThroughAllScenesAndOperateOnTransitions(Action<LoadScene[]> operation)
        {
            
            foreach (string scene in VanillaRegions)
            {
                GameManager.LoadSceneWithLoadingScreen(scene);

                yield return new WaitForSecondsRealtime(10f);
                operation(UnityEngine.Object.FindObjectsOfType<LoadScene>());
                yield return new WaitForSecondsRealtime(1f);

                if (breakCoroutines)
                {
                    breakCoroutines = false;
                    break;
                }
            }
            
            foreach (string scene in TFTFTRegions)
            {
                GameManager.LoadSceneWithLoadingScreen(scene);

                yield return new WaitForSecondsRealtime(10f);
                operation(UnityEngine.Object.FindObjectsOfType<LoadScene>());
                yield return new WaitForSecondsRealtime(1f);

                if (breakCoroutines)
                {
                    breakCoroutines = false;
                    break;
                }
            }
            yield break;
        }



        class DebugData 
        {
            public string toScene;
            public string m_SceneCanBeInstanced;
        }

        internal static void PrintDataFromTransition(LoadScene[] transition)
        {
            if (transition.Length == 0) return;

            string dir = modsPath + "RandomizerDebugOutput/";
            string file = dir + "AutomatedDataFromTransitions.txt";

            Dictionary<string, DebugData[]> dataDict = [];
            
            string scene = transition[0].gameObject.scene.name;

            List<DebugData> data = [];

            foreach (var t in transition)
            {
                data.Add(new DebugData
                {
                    toScene = t.m_SceneToLoad,
                    m_SceneCanBeInstanced = t.m_SceneCanBeInstanced.ToString()
                });
            }

            dataDict[scene] = data.ToArray();

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.AppendAllText(file, JsonSerializer.Serialize(dataDict, GetDefaultJsonOptions()));
            File.AppendAllText(file, "\n");
        }


        //       ___  _                         __  _       
        //      / _ \(_)__ ____ ____  ___  ___ / /_(_)______
        //     / // / / _ `/ _ `/ _ \/ _ \(_-</ __/ / __(_-<
        //    /____/_/\_,_/\_, /_//_/\___/___/\__/_/\__/___/
        //                /___/                             

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

        public static void RunOddTransitionLookup(Dictionary<(TransitionDefinition AIn, TransitionDefinition AOut), (TransitionDefinition BIn, TransitionDefinition BOut)> preResult)
        {
            // list odd transitions
            foreach (var pair in preResult)
            {
                if (pair.Key.AIn.type != pair.Value.BIn.type)
                {
                    Log(CC.Yellow, $"Found odd pair of ┌▷{pair.Key.AIn.type} from {pair.Key.AIn.fromScene} through {pair.Key.AIn.linkedPoint}");
                    Log(CC.Yellow, $"       leading to └▷{pair.Value.BIn.type}");
                }
            }
        }

        public static void RunSymmetryDiagnostics(Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> result) // Claude
        {



            // Build directed graph: for each remapping, track where each transition's "other side" leads
            // A correct swap means: if A.In -> B.In, then B.Out -> A.Out (and vice versa)
            Dictionary<string, (string destPoint, string destScene, string srcScene)> graph = new();
            int totalMappings = 0;

            foreach (var scene in result)
            {
                foreach (var mapping in scene.Value)
                {
                    string key = $"{mapping.Key.exitPoint}|{scene.Key}";
                    string val = $"{mapping.Value.exitPoint}|{mapping.Value.toScene}";
                    graph[key] = (mapping.Value.exitPoint, mapping.Value.toScene, scene.Key);
                    totalMappings++;
                }
            }

            // Check round-trips: for each mapping A->B, find B's linkedPoint and check it maps back to A's linkedPoint
            int symmetricCount = 0;
            int brokenCount = 0;
            int missingCount = 0;

            foreach (var scene in result)
            {
                foreach (var mapping in scene.Value)
                {
                    TransitionDefinition src = mapping.Key;
                    TransitionDefinition dst = mapping.Value;

                    // We went from src.fromScene via src.exitPoint -> dst.toScene via dst.exitPoint
                    // The "return door" is dst.linkedPoint in dst.toScene
                    // It should map back to src.linkedPoint in src.fromScene (which is src.toScene for the original)
                    string returnKey = $"{dst.linkedPoint}|{dst.toScene}";

                    if (graph.TryGetValue(returnKey, out var returnDest))
                    {
                        // The return door should lead to src.linkedPoint
                        if (returnDest.destPoint == src.linkedPoint)
                        {
                            symmetricCount++;
                        }
                        else
                        {
                            brokenCount++;
                            if (brokenCount <= 5) // limit spam
                            {
                                //Log(CC.Red, $"BROKEN ROUND-TRIP: {src.exitPoint}({scene.Key}) -> {dst.exitPoint}({dst.toScene}) but return goes to {returnDest.destPoint}({returnDest.destScene}) instead of {src.linkedPoint}");
                            }
                        }
                    }
                    else
                    {
                        missingCount++;
                        if (missingCount <= 5)
                        {
                            Log(CC.Yellow, $"MISSING RETURN: {src.exitPoint}({scene.Key}) -> {dst.exitPoint}({dst.toScene}), no mapping for return door {dst.linkedPoint}");
                        }
                    }
                }
            }

            //Log(CC.Cyan, $"=== SYMMETRY DIAGNOSTICS ===");
            //Log(CC.Cyan, $"Total mappings: {totalMappings}");
            Log(symmetricCount == totalMappings ? CC.Green : CC.Red, $"Symmetric round-trips: {symmetricCount}/{totalMappings}");
            if (brokenCount > 0) Log(CC.Red, $"Broken round-trips: {brokenCount}");
            if (missingCount > 0) Log(CC.Yellow, $"Missing return doors: {missingCount}");
            if (symmetricCount == totalMappings) Log(CC.Green, $"ALL ROUND-TRIPS VERIFIED OK");

            // === GRAPH CONNECTIVITY: check for disconnected subgraphs and dead ends ===
            // Build undirected scene adjacency graph from the remapped transitions
            Dictionary<string, HashSet<string>> sceneGraph = new();

            foreach (var scene in result)
            {
                string fromScene = scene.Key;
                sceneGraph.TryAdd(fromScene, new());

                foreach (var mapping in scene.Value)
                {
                    string toScene = mapping.Value.toScene;
                    sceneGraph.TryAdd(toScene, new());
                    sceneGraph[fromScene].Add(toScene);
                    sceneGraph[toScene].Add(fromScene);
                }
            }

            // BFS from any starting scene to find connected components
            HashSet<string> visited = new();
            List<HashSet<string>> components = new();

            foreach (string startScene in sceneGraph.Keys)
            {
                if (visited.Contains(startScene)) continue;

                HashSet<string> component = new();
                Queue<string> queue = new();
                queue.Enqueue(startScene);
                visited.Add(startScene);

                while (queue.Count > 0)
                {
                    string current = queue.Dequeue();
                    component.Add(current);

                    foreach (string neighbor in sceneGraph[current])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                }

                components.Add(component);
            }

            //Log(CC.Cyan, $"=== CONNECTIVITY DIAGNOSTICS ===");
            Log(CC.Cyan, $"Total scenes in graph: {sceneGraph.Count}");
            //Log(components.Count == 1 ? CC.Green : CC.Red, $"Connected components: {components.Count}");

            if (components.Count == 1)
            {
                Log(CC.Green, $"WORLD IS FULLY CONNECTED -- all {sceneGraph.Count} scenes reachable");
            }
            else
            {
                // Sort components by size descending
                components.Sort((a, b) => b.Count.CompareTo(a.Count));
                for (int ci = 0; ci < components.Count; ci++)
                {
                    HashSet<string> comp = components[ci];
                    string label = ci == 0 ? "MAIN" : "ISLAND";
                    CC color = ci == 0 ? CC.Green : CC.Red;
                    Log(color, $"  Component {ci + 1} ({label}): {comp.Count} scenes");
                    if (ci > 0) // show isolated scenes
                    {
                        foreach (string scene in comp)
                        {
                            Log(CC.Red, $"    - {scene} (connections: {sceneGraph[scene].Count})");
                        }
                    }
                }
            }
        }
    }
}
