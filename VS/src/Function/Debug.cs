#if DEBUG

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

        public static void BuildQueDoorData1()
        {
            Dictionary<string, List<QueDoorDestination>> data = new();

            List<(string scene, Vector3 pos, float pitch, float yaw)> values = new(){
                new() { scene = "BlackrockPrisonSurvivalZone", pos = new Vector3(-143.09f, 233.48f, 109.62f), pitch = -5.71f, yaw = 229.03f },
                new() { scene = "AshCanyonRegion", pos = new Vector3(296.23f, 232.1f, 3.94f), pitch = -10.06f, yaw = 78.57f },
                new() { scene = "LakeRegion", pos = new Vector3(1214.34f, 92.59f, 877.54f), pitch = 9.37f, yaw = 195.61f },
                new() { scene = "LakeRegion", pos = new Vector3(453.47f, 160.15f, 1002.6f), pitch = -65.81f, yaw = 200.31f },
                new() { scene = "MountainTownRegion", pos = new Vector3(1986.28f, 448.53f, 1482.22f), pitch = -30.97f, yaw = 52.90f },
                new() { scene = "CanneryRegion", pos = new Vector3(76.17f, 95.85f, -917.39f), pitch = -8.89f, yaw = 338.96f },
                new() { scene = "WhalingStationRegion", pos = new Vector3(734.41f, 60.94f, 766.34f), pitch = 2.09f, yaw = 31.78f },
                new() { scene = "RuralRegion", pos = new Vector3(587.56f, 157.39f, 1795.76f), pitch = 0.56f, yaw = 94.46f },

                new() { scene = "AshCanyonRegion", pos = new Vector3(469.85f, 247.94f, 413.65f), pitch = 2.64f, yaw = 115.47f },
                new() { scene = "CoastalRegion", pos = new Vector3(327.87f, 26.41f, 118.35f), pitch = -16.59f, yaw = 265.64f },
                new() { scene = "WhalingStationRegion", pos = new Vector3(566.62f, 63.29f, 1105.17f), pitch = 26.31f, yaw = 4.06f },
                new() { scene = "WhalingStationRegion", pos = new Vector3(220.78f, 76.8f, 947.23f), pitch = 22.74f, yaw = 264.05f },
                new() { scene = "CanneryRegion", pos = new Vector3(-387.45f, 31.9f, -590.74f), pitch = 9.83f, yaw = 180.77f },
                new() { scene = "RuralRegion", pos = new Vector3(2508.2f, 111.26f, 1209.96f), pitch = -45.80f, yaw = 284.62f },
                new() { scene = "RuralRegion", pos = new Vector3(2624.69f, 94.44f, 1459.81f), pitch = -8.28f, yaw = 286.05f },
                new() { scene = "CoastalRegion", pos = new Vector3(-705.337f, 23.046f, 633.581f), pitch = -19.03f, yaw = 108.93f },
                new() { scene = "CanneryRegion", pos = new Vector3(-573.351f, 84.779f, 709.559f), pitch = -4.82f, yaw = 154.01f },
                new() { scene = "LakeRegion", pos = new Vector3(574.227f, -1.308f, 263.231f), pitch = -5.78f, yaw = 293.81f },
                new() { scene = "MarshRegion", pos = new Vector3(1068.903f, -125.521f, 390.653f), pitch = 3.79f, yaw = 231.77f },
                new() { scene = "MarshRegion", pos = new Vector3(1110.914f, -130.039f, 975.339f), pitch = -9.34f, yaw = 99.22f },
                new() { scene = "MountainTownRegion", pos = new Vector3(441.783f, 7.159f, 705.086f), pitch = -27.19f, yaw = 150.61f },
                new() { scene = "MountainTownRegion", pos = new Vector3(687.388f, 289.364f, 2106.912f), pitch = -6.63f, yaw = 321.76f },
                new() { scene = "TracksRegion", pos = new Vector3(253.219f, 193.03f, 698.471f), pitch = 6.27f, yaw = 232.10f },
                new() { scene = "RiverValleyRegion", pos = new Vector3(1153.08f, 197.749f, 762.208f), pitch = -14.85f, yaw = 326.54f },
                new() { scene = "RiverValleyRegion", pos = new Vector3(939.847f, 192.813f, 1294.657f), pitch = 5.91f, yaw = 170.13f },

                new() { scene = "HighwayTransitionZone", pos = new Vector3(456.005f, 49.984f, 240.458f), pitch = -3.54f, yaw = 338.05f },
                new() { scene = "DamRiverTransitionZoneB", pos = new Vector3(475.473f, 93.065f, 272.878f), pitch = -17.81f, yaw = 38.72f },
                new() { scene = "RavineTransitionZone", pos = new Vector3(-89.327f, 133.569f, -4.813f), pitch = -1.42f, yaw = 49.91f },
                new() { scene = "RavineTransitionZone", pos = new Vector3(-680.48f, 19.896f, -185.567f), pitch = -11.80f, yaw = 328.07f },
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(758.16f, 244.54f, -164.331f), pitch = -37.65f, yaw = 23.90f },
                new() { scene = "AshCanyonRegion", pos = new Vector3(654.908f, 51.52f, -419.82f), pitch = -21.35f, yaw = 314.55f },
                new() { scene = "AshCanyonRegion", pos = new Vector3(-367.109f, 160.6f, 734.99f), pitch = 50.24f, yaw = 255.37f },
                new() { scene = "CrashMountainRegion", pos = new Vector3(1575.28f, 127.053f, 486.847f), pitch = -17.44f, yaw = 249.73f },
                new() { scene = "CrashMountainRegion", pos = new Vector3(1006.28f, 525.593f, 1289.064f), pitch = -12.19f, yaw = 197.84f },
                new() { scene = "BlackrockRegion", pos = new Vector3(-339.708f, 76.605f, 441.534f), pitch = -33.56f, yaw = 346.88f },
                new() { scene = "BlackrockRegion", pos = new Vector3(484.555f, 362.037f, 920.44f), pitch = -4.80f, yaw = 105.20f },
                new() { scene = "TracksRegion", pos = new Vector3(-185.983f, 257.662f, 812.721f), pitch = 1.23f, yaw = 94.92f }, // rabbit grove
                new() { scene = "RiverValleyRegion", pos = new Vector3(1004.8f, 19.109f, 1080.165f), pitch = 28.85f, yaw = 267.73f }, // facing rainbow
                new() { scene = "RiverValleyRegion", pos = new Vector3(1718.403f, 18.711f, 1021.358f), pitch = 27.73f, yaw = 310.83f }, // landslide cave
                new() { scene = "RiverValleyRegion", pos = new Vector3(454.011f, 227.846f, 259.994f), pitch = 89.86f, yaw = 20.56f }, // facing up into trees near exit
                new() { scene = "RiverValleyRegion", pos = new Vector3(341.916f, 246.119f, 422.849f), pitch = -14.73f, yaw = 328.60f }, // plane
                new() { scene = "RiverValleyRegion", pos = new Vector3(376.032f, 266.642f, 373.449f), pitch = -25.62f, yaw = 263.81f }, // dangerous broken tree near plane
                new() { scene = "LakeRegion", pos = new Vector3(-71.553f, 144.711f, 1407.41f), pitch = -9.93f, yaw = 102.05f }, // most n/w point, high altitude
                new() { scene = "LakeRegion", pos = new Vector3(1652.228f, 52.124f, 1313.553f), pitch = -18.74f, yaw = 54.57f }, // tall pipe near dam
                new() { scene = "RavineTransitionZone", pos = new Vector3(-606.864f, 142.495f, -15.722f), pitch = 1.88f, yaw = 153.87f }, // end of track
                new() { scene = "RavineTransitionZone", pos = new Vector3(-801.471f, 136.817f, -134.115f), pitch = 0.91f, yaw = 84.57f }, // on dangerous fallen tree in leaves
                new() { scene = "RavineTransitionZone", pos = new Vector3(-763.075f, 145.8712f, -147.6877f), pitch = -33.96f, yaw = 157.18f }, //walk the plank
                new() { scene = "CanneryRegion", pos = new Vector3(-396.458f, 38.461f, -470.637f), pitch = -10.69f, yaw = 239.13f }, // looking at cannery through bars
                new() { scene = "CanneryRegion", pos = new Vector3(-1030.98f, 24.998f, -249.208f), pitch = 60.34f, yaw = 29.33f }, // under broken bridge
                new() { scene = "CanneryRegion", pos = new Vector3(-730.017f, 104.894f, -215.53f), pitch = 5.10f, yaw = 104.27f }, // far hill with snowshelter
                new() { scene = "CanneryRegion", pos = new Vector3(491.287f, 73.659f, 3.765f), pitch = -40.22f, yaw = 311.58f }, // food easter egg
                new() { scene = "BlackrockPrisonSurvivalZone", pos = new Vector3(-219.79f, 225.834f, 128.768f), pitch = -4.33f, yaw = 55.45f }, //jail, inaccessible
                new() { scene = "AshCanyonRegion", pos = new Vector3(23.066f, 124.392f, 48.479f), pitch = -2.91f, yaw = 8.97f }, // some rare cliff
                new() { scene = "CrashMountainRegion", pos = new Vector3(520.46f, 236.058f, 122.794f), pitch = 13.39f, yaw = 170.37f }, // moss
                new() { scene = "CrashMountainRegion", pos = new Vector3(489.2017f, 246.8081f, 721.4768f), pitch = 16.15f, yaw = 126.04f }, // dangerous tree top
                new() { scene = "CrashMountainRegion", pos = new Vector3(1380.933f, 148.232f, 1210.677f), pitch = -15.29f, yaw = 240.62f }, // cave
                new() { scene = "RuralRegion", pos = new Vector3(1681.311f, 51.55f, 1835.4f), pitch = 9.07f, yaw = 192.60f }, // cock view
                new() { scene = "RuralRegion", pos = new Vector3(2433.663f, 54.309f, 2270.072f), pitch = 13.72f, yaw = 358.79f }, // scenic bridge
                new() { scene = "HighwayTransitionZone", pos = new Vector3(502.368f, 88.822f, 255.065f), pitch = -0.23f, yaw = 53.26f }, // overview
                new() { scene = "HighwayTransitionZone", pos = new Vector3(126.542f, 61.945f, 483.861f), pitch = 4.19f, yaw = 100.81f }, // entrance
                new() { scene = "HighwayTransitionZone", pos = new Vector3(647.284f, 73.592f, 500.332f), pitch = 16.12f, yaw = 19.77f }, // exit
                new() { scene = "DamRiverTransitionZoneB", pos = new Vector3(745.33f, 27.823f, 656.045f), pitch = -22.19f, yaw = 91.42f }, // entrance
                new() { scene = "DamRiverTransitionZoneB", pos = new Vector3(553.122f, 33.496f, 275.668f), pitch = 69.09f, yaw = 161.76f }, // dam fr4om below
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(800.427f, 287.13f, 416.312f), pitch = 14.25f, yaw = 267.20f }, // exit
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(103.963f, 253.72f, -79.038f), pitch = -1.48f, yaw = 74.43f }, // entrance
                new() { scene = "CoastalRegion", pos = new Vector3(-1142.277f, 98.344f, 826.436f), pitch = 19.51f, yaw = 269.96f }, // log gate in park
                new() { scene = "CoastalRegion", pos = new Vector3(-430.547f, 140.991f, 1123.304f), pitch = -27.58f, yaw = 260.96f }, // scenic on lonely cabin
                new() { scene = "CoastalRegion", pos = new Vector3(945.393f, 48.598f, 496.429f), pitch = 59.84f, yaw = 281.21f }, // flag
                new() { scene = "CoastalRegion", pos = new Vector3(50.389f, 23.126f, 846.809f), pitch = 90.03f, yaw = 10.31f }, // inside logs glitchy
                new() { scene = "WhalingStationRegion", pos = new Vector3(1008.353f, 55.666f, 1575.312f), pitch = 0.02f, yaw = 196.66f }, // top of bro0ken bridge
                new() { scene = "WhalingStationRegion", pos = new Vector3(1106.693f, 16.129f, 1285.403f), pitch = 1.91f, yaw = 89.65f }, // under pier
                new() { scene = "RuralRegion", pos = new Vector3(351.351f, 179.384f, 2257.338f), pitch = -23.78f, yaw = 224.08f }, // looking into toilet
                new() { scene = "RuralRegion", pos = new Vector3(1246.903f, 42.272f, 2113.071f), pitch = 0.43f, yaw = 89.48f }, // birch bear den

                new() { scene = "MarshRegion", pos = new Vector3(1603.469f, -44.201f, 301.222f), pitch = -20.56f, yaw = 286.77f }, // helicopter
                new() { scene = "MarshRegion", pos = new Vector3(72.195f, -94.598f, 212.465f), pitch = 30.50f, yaw = 53.32f },// fallen tower
                new() { scene = "MarshRegion", pos = new Vector3(134.645f, -126.668f, 834.134f), pitch = -0.44f, yaw = 90.18f }, // top of log car
                new() { scene = "MarshRegion", pos = new Vector3(191.738f, -55.043f, 1554.603f), pitch = -2.62f, yaw = 186.36f }, // cave
                new() { scene = "TracksRegion", pos = new Vector3(579.726f, 199.046f, 562.947f), pitch = 6.68f, yaw = 268.52f }, // they hate the light
                new() { scene = "TracksRegion", pos = new Vector3(327.64f, 200.325f, 429.784f), pitch = -4.15f, yaw = 14.33f }, // lake overlook
                new() { scene = "TracksRegion", pos = new Vector3(773.007f, 244.377f, 1298.345f), pitch = 10.86f, yaw = 333.60f }, // grove at entrance
                new() { scene = "BlackrockRegion", pos = new Vector3(-584.721f, 110.114f, -472.083f), pitch = 21.20f, yaw = 90.95f }, // substation
                new() { scene = "BlackrockRegion", pos = new Vector3(-39.022f, 127.267f, -758.214f), pitch = -3.43f, yaw = 318.02f }, // old substation
                new() { scene = "BlackrockRegion", pos = new Vector3(-652.048f, 117.772f, 6.31f), pitch = 68.61f, yaw = 187.16f }, // powerline
                new() { scene = "BlackrockRegion", pos = new Vector3(44.186f, 251.773f, 119.613f), pitch = 29.40f, yaw = 235.29f }, // scenic trees
                new() { scene = "MountainTownRegion", pos = new Vector3(1106.718f, 275.144f, 1781.028f), pitch = -1.81f, yaw = 176.40f }, //graym
                new() { scene = "MountainTownRegion", pos = new Vector3(1491.239f, 369.413f, 1998.092f), pitch = 1.48f, yaw = 318.18f }, // cave
                new() { scene = "MountainTownRegion", pos = new Vector3(1201.987f, 264.604f, 1371.261f), pitch = 42.31f, yaw = 184.08f }, // gas prices
                new() { scene = "MountainTownRegion", pos = new Vector3(883.823f, 59.398f, 895.875f), pitch = 1.11f, yaw = 215.98f }, // rocky refuge

                new() { scene = "MountainPassRegion", pos = new Vector3(521.14f, 582.97f, -546.59f), pitch = -19.85f, yaw = 315.51f },
                new() { scene = "MountainPassRegion", pos = new Vector3(-334.119f, 599.822f, -995.962f), pitch = -3.99f, yaw = 240.91f },
                new() { scene = "MountainPassRegion", pos = new Vector3(352.742f, 259.177f, 469.675f), pitch = 1.12f, yaw = 137.96f },
                new() { scene = "MountainPassRegion", pos = new Vector3(944.661f, 291.833f, -497.316f), pitch = 70.62f, yaw = 100.63f }, // orge teardrop
                new() { scene = "MountainPassRegion", pos = new Vector3(332.419f, 134.448f, 1039.575f), pitch = -12.64f, yaw = 351.01f }, // tunnel view


                new() { scene = "MiningRegion", pos = new Vector3(-366.14f, 147.71f, -866.55f), pitch = -1.24f, yaw = 105.80f },
                new() { scene = "MiningRegion", pos = new Vector3(-246.436f, 148.88f, -85.744f), pitch = -55.18f, yaw = 166.23f },
                new() { scene = "MiningRegion", pos = new Vector3(-337.405f, 244.553f, 129.423f), pitch = -2.17f, yaw = 117.28f },
                new() { scene = "MiningRegion", pos = new Vector3(-4.396f, 182.603f, -0.606f), pitch = -1.55f, yaw = 101.44f }, // overlooking chem pond
                new() { scene = "MiningRegion", pos = new Vector3(474.609f, 247.864f, 185.431f), pitch = 14.56f, yaw = 271.73f }, // idle camp roof
                new() { scene = "HubRegion", pos = new Vector3(289.052f, 266.941f, 653.328f), pitch = -3.02f, yaw = 324.24f }, // exit
                new() { scene = "HubRegion", pos = new Vector3(-40.031f, 326.772f, 161.802f), pitch = -14.57f, yaw = 86.36f },//big area overview
                new() { scene = "HubRegion", pos = new Vector3(213.836f, 256.166f, 16.638f), pitch = -8.22f, yaw = 77.46f },

                new() { scene = "AirfieldRegion", pos = new Vector3(-509.44f, 153.39f, 296.61f), pitch = -25.65f, yaw = 285.50f },
                new() { scene = "AirfieldRegion", pos = new Vector3(-234.559f, 160.509f, -151.336f), pitch = -8.08f, yaw = 96.94f },
                new() { scene = "AirfieldRegion", pos = new Vector3(544.756f, 152.622f, 825.09f), pitch = -19.51f, yaw = 49.52f },
                new() { scene = "MiningRegion", pos = new Vector3(-4.396f, 182.603f, -0.606f), pitch = -1.55f, yaw = 101.44f }, // overlooking chem pond
                new() { scene = "MiningRegion", pos = new Vector3(474.609f, 247.864f, 185.431f), pitch = 14.56f, yaw = 271.73f }, // idle camp roof
                new() { scene = "HubRegion", pos = new Vector3(289.052f, 266.941f, 653.328f), pitch = -3.02f, yaw = 324.24f }, // exit
                new() { scene = "HubRegion", pos = new Vector3(-40.031f, 326.772f, 161.802f), pitch = -14.57f, yaw = 86.36f },//big area overview
                new() { scene = "HubRegion", pos = new Vector3(213.836f, 256.166f, 16.638f), pitch = -8.22f, yaw = 77.46f },
                new() { scene = "AirfieldRegion", pos = new Vector3(595.014f, 156.462f, 364.992f), pitch = -27.59f, yaw = 358.21f }, // crashed plane wing
                new() { scene = "AirfieldRegion", pos = new Vector3(-1050.585f, 236.924f, -1068.258f), pitch = -19.99f, yaw = 286.74f }, // junkers paddock


                new() { scene = "HubRegion", pos = new Vector3(190.574f, 262.79f, 447.354f), pitch = -35.02f, yaw = 21.27f },


            };

            foreach (var e in values)
            {
                data.TryAdd(e.scene, []);
                QueDoorDestination q = new() { 
                position = e.pos,
                pitch = e.pitch,
                yaw = e.yaw,

                };
                data[e.scene].Add(q);
            }

            // serialize
            // print

            string dir = Path.Combine(modsPath, "RandomizerDebugOutput");
            dir = Path.Combine(dir, "Processed");
            string file = Path.Combine(dir, "MysteryDoorDestinations.json");

            string dataText = JsonSerializer.Serialize(data, Jsoning.GetDefaultOptions());

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(file, dataText);

        }
        public static void BuildQueDoorData2()
        {
            Dictionary<string, List<QueDoorPosition>> data = new();

            List<(string scene, Vector3 pos, Quaternion rot, Vector3 sca, bool que)> values = new()
            {
                new() { scene = "TracksRegion", pos = new Vector3(667.912f, 201.041f, 566.376f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) }, //nosnow
                new() { scene = "TracksRegion", pos = new Vector3(883.991f, 241.254f, 674.06f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(829.602f, 238.396f, 403.803f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(793.272f, 236.858f, 205.011f), rot = new Quaternion(0f, 0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(782.316f, 236.298f, 945.113f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(680.414f, 240.121f, 959.436f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(528.967f, 184.5f, 1241.454f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(-144.771f, 248.036f, 506.739f), rot = new Quaternion(0.2671f, -0.537f, 0.1404f, 0.7878f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(68.108f, 243.471f, 439.276f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(311.477f, 184.808f, 553.23f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(312.515f, 184.908f, 552.843f), rot = new Quaternion(-0.2988f, 0.6409f, -0.6409f, -0.2988f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(806.417f, 232.192f, 366.633f), rot = new Quaternion(0.342f, 0f, 0.9397f, -0f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "TracksRegion", pos = new Vector3(578.757f, 200.238f, 581.028f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(0.7f, 0.7f, 0.7f) },
                new() { scene = "MarshRegion", pos = new Vector3(1075.498f, -119.894f, -179.318f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MarshRegion", pos = new Vector3(801.546f, -132.935f, 352.779f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MarshRegion", pos = new Vector3(1120.192f, -124.849f, 977.953f), rot = new Quaternion(0f, 0f, -0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MarshRegion", pos = new Vector3(1287.888f, -83.455f, 1549.996f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MarshRegion", pos = new Vector3(652.514f, -27.031f, 1491.858f), rot = new Quaternion(0.231f, -0.6302f, 0.712f, 0.2064f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MarshRegion", pos = new Vector3(93.202f, -133.246f, 616.732f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MarshRegion", pos = new Vector3(731.63f, -130.502f, -17.657f), rot = new Quaternion(0.2466f, -0.1681f, -0.0435f, -0.9534f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MarshRegion", pos = new Vector3(474.594f, -124.742f, 50.474f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(0.65f, 0.65f, 0.65f) },
                new() { scene = "MarshRegion", pos = new Vector3(198.827f, -133.695f, 811.222f), rot = new Quaternion(-0.5258f, 0.3874f, 0.1611f, 0.74f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MarshRegion", pos = new Vector3(1640.164f, -133.125f, 1351.135f), rot = new Quaternion(-0f, 0.9397f, -0f, -0.342f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(1172.491f, 267.057f, 1336.481f), rot = new Quaternion(0.3464f, 0.6246f, -0.5999f, 0.3606f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(1111.357f, 269.456f, 1793.02f), rot = new Quaternion(-0f, 0.5736f, -0f, -0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(598.686f, 220.271f, 1568.634f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(623.003f, 219.816f, 1555.818f), rot = new Quaternion(0.6749f, -0.2338f, 0.2399f, 0.6575f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(1022.111f, 261.554f, 1627.491f), rot = new Quaternion(0.2601f, -0.3799f, -0.0237f, 0.8874f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(924.612f, 315.297f, 1136.39f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(1262.419f, 245f, 990.478f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(629.729f, 18.939f, 594.84f), rot = new Quaternion(0.25f, -0.25f, -0.067f, -0.933f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(408.73f, 11.911f, 664.81f), rot = new Quaternion(-0.383f, -0.5567f, -0.3214f, 0.6634f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(537.719f, 11.273f, 745.153f), rot = new Quaternion(-0.029f, -0.7653f, -0.0346f, 0.6421f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(773.481f, 225.789f, 1631.889f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(1097.786f, 262.215f, 1699.978f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(1.2f, 1.2f, 1.2f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(1176.814f, 261.063f, 1670.072f), rot = new Quaternion(0.113f, 0.8586f, -0.0653f, -0.4957f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(1141.54f, 263.376f, 1721.577f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(856.168f, 273.817f, 2031.452f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(680.916f, 288.857f, 2098.832f), rot = new Quaternion(0f, 0.7071f, -0f, 0.7071f), sca = new Vector3(1f, 1f, 1f) },
                
                new() { scene = "RiverValleyRegion", pos = new Vector3(1512.03f, 16.191f, 1106.252f), rot = new Quaternion(-0.0511f, -0.2585f, -0.0137f, 0.9646f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RiverValleyRegion", pos = new Vector3(908.385f, 80.534f, 1022.176f), rot = new Quaternion(-0f, 0.6428f, -0f, -0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RiverValleyRegion", pos = new Vector3(444.258f, 148.596f, 1343.378f), rot = new Quaternion(0.1284f, -0.9305f, -0.0161f, 0.3426f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RiverValleyRegion", pos = new Vector3(906.252f, 89.853f, 787.054f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RiverValleyRegion", pos = new Vector3(315.799f, 103.513f, 1074.55f), rot = new Quaternion(0.5f, 0.5f, -0.5f, 0.5f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RiverValleyRegion", pos = new Vector3(613.039f, 103.639f, 1148.51f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RiverValleyRegion", pos = new Vector3(232.727f, 170.999f, 479.864f), rot = new Quaternion(-0.1124f, 0.4189f, 0.4911f, -0.7554f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RiverValleyRegion", pos = new Vector3(622.312f, 108.926f, 699.266f), rot = new Quaternion(-0.0222f, 0.2588f, -0.0059f, -0.9657f), sca = new Vector3(0.95f, 1f, 1f) },
                new() { scene = "RiverValleyRegion", pos = new Vector3(1084.736f, 119.15f, 829.08f), rot = new Quaternion(-0.2241f, -0.483f, -0.1294f, 0.8365f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(-76.521f, 145.761f, 1404.759f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(327.27f, 18.999f, 1123.215f), rot = new Quaternion(0f, 0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(489.687f, 1.223f, 299.61f), rot = new Quaternion(-0f, 0.5736f, -0f, -0.8192f), sca = new Vector3(0.8f, 0.8f, 0.8f) },
                new() { scene = "LakeRegion", pos = new Vector3(149.128f, 4.199f, 53.027f), rot = new Quaternion(0f, 0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(44.612f, 14.221f, 33.179f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(1530.585f, 17.027f, 225.589f), rot = new Quaternion(-0f, 0.7071f, -0f, -0.7071f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(1449.177f, 26.96f, 226.147f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(1538.186f, 28.646f, 1271.006f), rot = new Quaternion(-0f, 0.866f, -0f, -0.5f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(1632.408f, 38.246f, 1271.787f), rot = new Quaternion(-0.3522f, 0.6132f, -0.3522f, -0.6132f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(777.45f, 191.9f, 1005.007f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(190.261f, 9.7f, 700.101f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(213.748f, 4.084f, -16.09f), rot = new Quaternion(-0f, 0.4226f, -0f, -0.9063f), sca = new Vector3(5f, 5f, 5f) },
                new() { scene = "LakeRegion", pos = new Vector3(398.615f, 78.312f, 152.148f), rot = new Quaternion(-0f, 0.0872f, -0f, -0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(232.175f, 75.99f, 231.589f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(1441.253f, 16.404f, 971.013f), rot = new Quaternion(0.5517f, -0.3353f, -0.2573f, -0.719f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "LakeRegion", pos = new Vector3(1666.544f, 38.245f, 1398.338f), rot = new Quaternion(0.049f, 0.0871f, -0.0043f, 0.995f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RavineTransitionZone", pos = new Vector3(-1072.141f, 131.513f, -81.807f), rot = new Quaternion(0f, 0.6428f, -0f, 0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RavineTransitionZone", pos = new Vector3(-832.526f, 126.442f, -145.31f), rot = new Quaternion(-0.1485f, 0.7912f, -0.212f, -0.554f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RavineTransitionZone", pos = new Vector3(-310.298f, 136.065f, 26.765f), rot = new Quaternion(-0.1039f, -0.6994f, -0.1039f, 0.6994f), sca = new Vector3(1f, 1f, 0.1f) },
                new() { scene = "RavineTransitionZone", pos = new Vector3(-521.195f, 134.2f, -62.494f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RavineTransitionZone", pos = new Vector3(-485.788f, 21.201f, -185.811f), rot = new Quaternion(0.1f, -0.7595f, -0.0839f, 0.6373f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RavineTransitionZone", pos = new Vector3(-568.182f, 134.041f, -3.362f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RavineTransitionZone", pos = new Vector3(-1159.427f, 127.723f, -14.044f), rot = new Quaternion(0f, 0.3338f, -0f, 0.9426f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-403.264f, 24.296f, -534.895f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-461.157f, 32.657f, -468.103f), rot = new Quaternion(0.0552f, 0.419f, 0.1183f, 0.8986f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-430.08f, 35.803f, -409.554f), rot = new Quaternion(-0f, 0.7071f, -0f, -0.7071f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-842.277f, 90.281f, 675.357f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-693.625f, 57.399f, 396.728f), rot = new Quaternion(-0f, 0.0872f, -0f, -0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-539.229f, 25.098f, -114.497f), rot = new Quaternion(-0f, 0.8192f, -0f, -0.5736f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-633.752f, 35.471f, -73.175f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(0.4f, 0.4f, 0.4f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-534.98f, 38.011f, -373.768f), rot = new Quaternion(0.1f, 0.7595f, -0.0839f, -0.6373f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-194.131f, 50.125f, -365.968f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(531.974f, 93.708f, 86.595f), rot = new Quaternion(-0.1576f, -0.7663f, -0.588f, 0.2053f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(636.107f, 92.027f, 392.05f), rot = new Quaternion(0.3696f, 0.2391f, -0.099f, 0.8924f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(299.323f, 238.805f, 351.841f), rot = new Quaternion(0.866f, -0f, -0.5f, -0f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(772.671f, 122.139f, 476.033f), rot = new Quaternion(0.1227f, -0.3391f, -0.0446f, -0.9317f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(237.372f, 296.982f, 859.978f), rot = new Quaternion(-0f, 0.0872f, -0f, 0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(115.099f, 215.616f, 523.57f), rot = new Quaternion(0f, 0.0872f, -0f, 0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-68.765f, 139.837f, 14.104f), rot = new Quaternion(0.1305f, -0f, -0f, -0.9914f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(-23.681f, 139.142f, 144.409f), rot = new Quaternion(0.0449f, -0.1677f, 0.2549f, -0.9513f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(152.921f, 141.55f, 230.728f), rot = new Quaternion(0.1f, 0.7595f, -0.0839f, -0.6373f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CanneryRegion", pos = new Vector3(282.566f, 41.092f, -263.993f), rot = new Quaternion(0f, 0.6428f, -0f, 0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(194.71f, 175.399f, -475.452f), rot = new Quaternion(0.1f, -0.7595f, -0.0839f, 0.6373f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(182.693f, 179.148f, -249.751f), rot = new Quaternion(0f, 0.7071f, -0f, 0.7071f), sca = new Vector3(0.9f, 0.9f, 0.9f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(29.043f, 172.127f, -276.941f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1.2f, 1.2f, 1.2f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(-64.48f, 199.071f, -461.434f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(-187.835f, 172.308f, -484.187f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(11.296f, 238.571f, -23.82f), rot = new Quaternion(-0f, 0.0085f, -0f, -1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(410.822f, 185.654f, 515.481f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(-192.646f, 184.221f, -549.928f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(-812.584f, 85.61f, -450.274f), rot = new Quaternion(0f, 0.6428f, -0f, 0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(-976.597f, 100.215f, -380.45f), rot = new Quaternion(0.0227f, -0.1722f, -0.1285f, 0.9764f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(-850.883f, 76.043f, -186.634f), rot = new Quaternion(-0f, 0.4226f, -0f, -0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(-376.267f, 108.927f, -583.316f), rot = new Quaternion(0f, 0.0872f, -0f, 0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockRegion", pos = new Vector3(-103.969f, 120.426f, -683.354f), rot = new Quaternion(-0f, 0.6428f, -0f, -0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockPrisonSurvivalZone", pos = new Vector3(-234.22f, 225.738f, 154.35f), rot = new Quaternion(0f, 0.6428f, -0f, 0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockPrisonSurvivalZone", pos = new Vector3(-85.629f, 231.657f, 154.751f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockPrisonSurvivalZone", pos = new Vector3(-75.092f, 225.673f, 42.89f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockPrisonSurvivalZone", pos = new Vector3(-104.19f, 225.319f, 105.216f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockPrisonSurvivalZone", pos = new Vector3(-230.729f, 226.015f, 98.692f), rot = new Quaternion(-0.2432f, 0.3304f, -0.0885f, -0.9077f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockPrisonSurvivalZone", pos = new Vector3(-216.579f, 224.641f, 59.886f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(2365.218f, 54.2f, 2269.936f), rot = new Quaternion(0.3008f, -0.7685f, 0.3879f, 0.4104f), sca = new Vector3(1f, 1f, 1f), que = true }, // only ?
                new() { scene = "BlackrockPrisonSurvivalZone", pos = new Vector3(-124.738f, 229.564f, 125.45f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f), que = true }, // only ?
                new() { scene = "CanneryRegion", pos = new Vector3(-337.176f, 30.922f, -518.069f), rot = new Quaternion(-0f, 0.5736f, -0f, -0.8192f), sca = new Vector3(1f, 1f, 1f), que = true }, // only ?
                new() { scene = "LakeRegion", pos = new Vector3(896.196f, 77.848f, 963.597f), rot = new Quaternion(-0f, 0.5736f, -0f, -0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(883.693f, 53.811f, -726.487f), rot = new Quaternion(-0f, 0.866f, -0f, -0.5f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(615.371f, 55.924f, -499.819f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(309.931f, 51.314f, -129.582f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(598.885f, 190.747f, -23.322f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(312.7f, 73.915f, -14.876f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(-233.887f, 70.242f, 417.506f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(-237.076f, 156.797f, 478.804f), rot = new Quaternion(-0.588f, -0.2053f, -0.1576f, 0.7663f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(-120.699f, 195.255f, 775.676f), rot = new Quaternion(-0.0664f, 0.4956f, -0.1124f, 0.8587f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(183.131f, 251.795f, 813.941f), rot = new Quaternion(0.0858f, -0.5704f, -0.0601f, -0.8146f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(338.384f, 233.557f, 593.038f), rot = new Quaternion(-0.6124f, -0.3536f, -0f, 0.7071f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(216.836f, 265.681f, 406.645f), rot = new Quaternion(-0f, 0.766f, -0f, -0.6428f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(93.189f, 183.247f, 31.054f), rot = new Quaternion(-0f, 0.0872f, -0f, -0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(-824.975f, 75.335f, 458.677f), rot = new Quaternion(0.4698f, 0.2962f, -0.171f, 0.8138f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(-33.809f, 70.242f, 248.732f), rot = new Quaternion(-0f, 0.8192f, -0f, -0.5736f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AshCanyonRegion", pos = new Vector3(-70.86f, 106.677f, 612.416f), rot = new Quaternion(-0.1026f, 0.1176f, 0.9777f, 0.1402f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(575.439f, 166.452f, 327.216f), rot = new Quaternion(0.1176f, 0.1026f, 0.1402f, -0.9777f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(912.642f, 177.615f, 232.67f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(1472.367f, 150.846f, 1330f), rot = new Quaternion(0f, 0.0872f, -0f, 0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(648.371f, 211.169f, 600.063f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(757.998f, 156.896f, 615.508f), rot = new Quaternion(-0.0691f, 0.6064f, 0.7903f, 0.0531f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(988.4f, 166.153f, 769.768f), rot = new Quaternion(0.1485f, 0.554f, -0.212f, -0.7912f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(1260.252f, 165.816f, 713.907f), rot = new Quaternion(-0f, 0.5f, -0f, -0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(843.831f, 148.982f, 553.312f), rot = new Quaternion(-0.6392f, -0.224f, -0.2538f, 0.6906f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(1083.082f, 148.808f, 528.482f), rot = new Quaternion(-0.3044f, -0.6871f, -0.5272f, 0.3967f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(1137.355f, 50.982f, 943.595f), rot = new Quaternion(-0.2191f, -0.0361f, 0.1583f, -0.9621f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(1613.231f, 217.929f, 1500.086f), rot = new Quaternion(-0.2566f, 0.1261f, 0.9577f, 0.0338f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(286.895f, 360.564f, 1704.71f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(852.289f, 289.365f, 952.951f), rot = new Quaternion(-0.0336f, 0.2587f, -0.009f, -0.9653f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(757.737f, 494.941f, 1251.385f), rot = new Quaternion(-0f, 0.4226f, -0f, -0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(927.396f, 465.459f, 1180.158f), rot = new Quaternion(-0f, 0.4226f, -0f, -0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(926.769f, 469.941f, 1181.259f), rot = new Quaternion(-0.021f, 0.2056f, -0.0044f, -0.9784f), sca = new Vector3(1f, 1f, 1f), que = true }, // ? only
                new() { scene = "CrashMountainRegion", pos = new Vector3(846.124f, 504.208f, 1312.186f), rot = new Quaternion(0.0066f, 0.1735f, -0.0372f, -0.9841f), sca = new Vector3(4f, 4f, 4f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(985.048f, 487.84f, 1428.116f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CrashMountainRegion", pos = new Vector3(1344.958f, 292.24f, 1682.806f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1096.081f, 42.005f, 2245.96f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1097.488f, 42.005f, 2244.122f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1015.879f, 89.01f, 1548.791f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(7f, 7f, 7f) },
                new() { scene = "RuralRegion", pos = new Vector3(1684.406f, 44.378f, 1631.724f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1503.842f, 41.609f, 1553.452f), rot = new Quaternion(-0f, 0.4226f, -0f, -0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1646.067f, 44.225f, 1437.967f), rot = new Quaternion(-0.1664f, -0.7399f, -0.1983f, 0.6209f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1396.579f, 38.4f, 1708.299f), rot = new Quaternion(-0.5995f, 0.1378f, -0.1057f, -0.7813f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1744.228f, 42.52f, 1882.67f), rot = new Quaternion(-0f, 0.4226f, -0f, -0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(2035.915f, 46.142f, 2190.09f), rot = new Quaternion(0f, 0.0872f, -0f, 0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(2329.28f, 52.875f, 2265.134f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(2261.846f, 55.146f, 2285.558f), rot = new Quaternion(0f, 0.6428f, -0f, 0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1450.051f, 41.015f, 2196.719f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1389.835f, 42.99f, 2124.475f), rot = new Quaternion(0f, 0f, -0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1304.021f, 40.942f, 2149.293f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1397.926f, 89.611f, 2701.7f), rot = new Quaternion(-0f, 0.0872f, -0f, -0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(429.849f, 190.691f, 1617.943f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(589.033f, 159f, 1805.091f), rot = new Quaternion(0.0783f, -0.1691f, -0.2012f, -0.9617f), sca = new Vector3(1f, 1f, 5f), que = true }, // ? only
                new() { scene = "HighwayTransitionZone", pos = new Vector3(347.158f, 59.587f, 521.44f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HighwayTransitionZone", pos = new Vector3(661.463f, 77.845f, 442.243f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HighwayTransitionZone", pos = new Vector3(644.736f, 74.021f, 517.345f), rot = new Quaternion(-0f, 0.5736f, -0f, -0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HighwayTransitionZone", pos = new Vector3(161.74f, 49.153f, 485.177f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "DamRiverTransitionZoneB", pos = new Vector3(500.597f, 95.331f, 231.09f), rot = new Quaternion(-0f, 0.1268f, -0f, -0.9919f), sca = new Vector3(1f, 1f, 1f), que = true }, // ? only
                new() { scene = "DamRiverTransitionZoneB", pos = new Vector3(442.55f, 30.233f, 662.815f), rot = new Quaternion(0.1294f, -0.017f, 0.1294f, 0.983f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "DamRiverTransitionZoneB", pos = new Vector3(457.824f, 35.198f, 620.491f), rot = new Quaternion(0.1227f, -0.3391f, -0.0446f, -0.9317f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "DamRiverTransitionZoneB", pos = new Vector3(733.919f, 5.586f, 595.934f), rot = new Quaternion(-0.6696f, -0.1147f, -0.1058f, 0.7262f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(862.11f, 251.016f, 427.826f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(855.564f, 258.846f, 465.999f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(644.73f, 259.228f, -178.951f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(560.325f, 262.201f, 62.126f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(409.499f, 254.381f, 15.779f), rot = new Quaternion(-0f, 0.5f, -0f, -0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(221.764f, 253.247f, -64.885f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "BlackrockTransitionZone", pos = new Vector3(423.874f, 188.096f, -152.798f), rot = new Quaternion(-0f, 0f, 0.1305f, 0.9914f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-930.302f, 23.447f, 348.907f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-1116.622f, 67.234f, 590.382f), rot = new Quaternion(0.1294f, -0.8365f, -0.2241f, -0.483f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-1109.535f, 116.12f, 309.45f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-1048.047f, 104.071f, 370.317f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-1117.038f, 60.102f, 523.539f), rot = new Quaternion(-0f, 0.5f, -0f, -0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-916.621f, 57.673f, 822.138f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-681.975f, 27.056f, 675.264f), rot = new Quaternion(-0.5f, -0.5f, -0.5f, 0.5f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-715.934f, 27.653f, 687.87f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-306.626f, 23.664f, 922.007f), rot = new Quaternion(-0.1261f, -0.2566f, -0.0338f, 0.9577f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-6.093f, 31.849f, 1031.023f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(30.208f, 23.174f, 708.692f), rot = new Quaternion(0.2241f, -0.483f, -0.1294f, -0.8365f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-83.892f, 25.541f, 895.195f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(697.302f, 30.467f, 626.543f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(1041.2f, 49.237f, 513.02f), rot = new Quaternion(-0f, 0.766f, -0f, -0.6428f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(933.122f, 24.462f, 188.288f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(796.72f, 40.163f, -127.675f), rot = new Quaternion(-0f, 0.7071f, -0f, -0.7071f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(653.694f, 35.498f, -182.313f), rot = new Quaternion(0f, 0.0872f, -0f, 0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(678.963f, 35.748f, -427.246f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(361.48f, 60.297f, 223.07f), rot = new Quaternion(-0f, 0.3829f, -0f, -0.9238f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "CoastalRegion", pos = new Vector3(-484.275f, 100.22f, 1137.8f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(1134.332f, 24.362f, 893.882f), rot = new Quaternion(-0f, 0.5f, -0f, -0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(1103.214f, 15.837f, 1006.801f), rot = new Quaternion(-0.246f, 0.7077f, -0.2932f, -0.5939f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(799.523f, 15.739f, 1054.2f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(989.978f, 38.668f, 1553.287f), rot = new Quaternion(-0f, 0.7071f, -0f, -0.7071f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(909.264f, 15.739f, 1666.791f), rot = new Quaternion(-0f, 0.5736f, -0f, -0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(1028.93f, 19.495f, 1261.396f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(1104.185f, 16.007f, 1288.813f), rot = new Quaternion(-0f, 0.5736f, -0f, -0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(945.461f, 16.677f, 1311.542f), rot = new Quaternion(0.1485f, -0.7912f, -0.212f, -0.554f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(853.335f, 41.975f, 1081.206f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(639.043f, 33.391f, 795.175f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(525.637f, 15.739f, 983.58f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(227.436f, 53.689f, 1155.351f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(385.826f, 72.965f, 1134.584f), rot = new Quaternion(-0.0361f, -0.1193f, -0.0043f, 0.9922f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "WhalingStationRegion", pos = new Vector3(858.296f, 38.145f, 1475.775f), rot = new Quaternion(-0f, 0.5f, -0f, -0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(718.994f, 144.589f, 2291.787f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "RuralRegion", pos = new Vector3(1724.827f, 49.016f, 923.712f), rot = new Quaternion(-0.3696f, -0.2391f, -0.099f, 0.8924f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainTownRegion", pos = new Vector3(676.149f, 223.348f, 1707.84f), rot = new Quaternion(-0f, 0.0872f, -0f, -0.9962f), sca = new Vector3(1f, 1f, 1f), que = true }, // ? only
                new() { scene = "MountainPassRegion", pos = new Vector3(984.424f, 386.979f, 405.219f), rot = new Quaternion(-0f, 0.7071f, -0f, -0.7071f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(307.729f, 262.127f, 427.605f), rot = new Quaternion(0.0338f, 0.2566f, 0.1261f, 0.9577f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(299.478f, 362.397f, 0.286f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(80.619f, 370.352f, -172.268f), rot = new Quaternion(-0f, 0.0872f, -0f, -0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(-326.376f, 491.509f, -478.669f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(-524.055f, 624.957f, -885.182f), rot = new Quaternion(0f, 0.0872f, -0f, 0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(-101.142f, 510.2f, -1023.745f), rot = new Quaternion(-0f, 0.8192f, -0f, -0.5736f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(625.585f, 450.166f, -879.614f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(939.474f, 433.287f, -654.389f), rot = new Quaternion(0.0446f, 0.3391f, -0.1227f, -0.9317f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(774.366f, 192.182f, -246.008f), rot = new Quaternion(0.429f, -0f, -0f, -0.9033f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(472.168f, 83.885f, -27.012f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(1010.128f, 175.989f, -167.287f), rot = new Quaternion(0.0446f, -0.3391f, -0.1227f, 0.9317f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(501.119f, 67.906f, 820.674f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(619.399f, 188.837f, 585.023f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(412.849f, 116.27f, -412.673f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MountainPassRegion", pos = new Vector3(169.684f, 451.811f, -511.108f), rot = new Quaternion(-0.2241f, 0.483f, -0.1294f, -0.8365f), sca = new Vector3(1f, 1f, 1f) },

                new() { scene = "MiningRegion", pos = new Vector3(828.965f, 275.726f, -899.285f), rot = new Quaternion(0.0324f, 0.172f, 0.0562f, 0.983f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(303.14f, 152.694f, -952.693f), rot = new Quaternion(0.3489f, 0.5632f, -0.1814f, 0.7267f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-168.438f, 132.075f, -750.969f), rot = new Quaternion(0.0338f, -0.2566f, 0.1261f, -0.9577f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(96.01f, 87.007f, -972.823f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-136.936f, 193.977f, -515.084f), rot = new Quaternion(0.483f, 0.2241f, -0.1294f, 0.8365f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-127.58f, 195.37f, -485.545f), rot = new Quaternion(-0.5272f, 0.3967f, -0.3044f, -0.6871f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-349.162f, 220.144f, -446.471f), rot = new Quaternion(-0.0839f, 0.7595f, -0.1f, -0.6373f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-253.497f, 160.594f, -320.138f), rot = new Quaternion(0f, -0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-307.485f, 160.785f, -74.963f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-264.697f, 146.867f, -93.528f), rot = new Quaternion(-0.1227f, -0.3391f, -0.0446f, 0.9317f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-21.139f, 182.617f, 1.215f), rot = new Quaternion(-0f, 0.5f, -0f, -0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(26.192f, 177.887f, -388.888f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(233.972f, 215.504f, 234.448f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-299.5f, 200.032f, 117.273f), rot = new Quaternion(-0f, 0.5f, -0f, -0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-203.583f, 198.599f, 206.973f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-224.772f, 199.266f, 293.18f), rot = new Quaternion(-0.183f, 0.683f, -0.183f, -0.683f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(432.077f, 235.001f, 182.34f), rot = new Quaternion(0f, 0f, -0f, 1f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(433.21f, 239.104f, 223.661f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-171.38f, 198.394f, 94.307f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-303.146f, 198.451f, 339.282f), rot = new Quaternion(0f, 0.2588f, -0f, 0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "MiningRegion", pos = new Vector3(-226.4f, 216.325f, 430.083f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },

                new() { scene = "AirfieldRegion", pos = new Vector3(-1084.082f, 235.049f, -1065.887f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(131.124f, 160.896f, -498.364f), rot = new Quaternion(0f, 0.5486f, -0f, 0.8361f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(94.868f, 172.544f, -443.456f), rot = new Quaternion(0f, 0.2002f, -0f, 0.9798f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-237.91f, 160.003f, -152.782f), rot = new Quaternion(0f, 0.9397f, 0f, 0.342f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(666.583f, 151.043f, 1136.51f), rot = new Quaternion(-0f, 0.3015f, -0f, -0.9535f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-1067.119f, 229.451f, 819.135f), rot = new Quaternion(-0f, 0.5736f, -0f, -0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-451.246f, 153.836f, 159.483f), rot = new Quaternion(0.029f, -0.3408f, -0.0658f, 0.9374f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(259.161f, 160.034f, -47.67f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(627.551f, 151.004f, 488.17f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(1132.208f, 260.147f, -636.145f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(792.555f, 177.098f, -321.063f), rot = new Quaternion(0f, 0.6428f, -0f, 0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(99.947f, 151.004f, 1065.815f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-163.51f, 153.468f, 186.966f), rot = new Quaternion(-0f, 0.0872f, -0f, -0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-1111.34f, 183.802f, 198.366f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-103.441f, 155.901f, -409.138f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(13.294f, 158.011f, -664.429f), rot = new Quaternion(-0f, 0.1736f, -0f, -0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(331.058f, 206.303f, 1047.687f), rot = new Quaternion(-0f, 0.0872f, -0f, -0.9962f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(386.495f, 178.412f, 981.148f), rot = new Quaternion(0f, 0.7071f, -0f, 0.7071f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-255.624f, 156.631f, 872.571f), rot = new Quaternion(-0f, 0.342f, -0f, -0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-576.922f, 171.142f, -401.025f), rot = new Quaternion(0f, 0.4226f, -0f, 0.9063f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-1166.968f, 271.551f, -277.325f), rot = new Quaternion(-0f, 0.2588f, -0f, -0.9659f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-733.202f, 297.077f, 1228.533f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(-231.3f, 160.867f, 527.745f), rot = new Quaternion(0f, 0.6428f, -0f, 0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "AirfieldRegion", pos = new Vector3(80.317f, 160.562f, 790.733f), rot = new Quaternion(0f, 0f, 0f, 1f), sca = new Vector3(1f, 1f, 1f) },

                new() { scene = "HubRegion", pos = new Vector3(136.927f, 250.155f, -97.56f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(109.938f, 250.122f, 327.982f), rot = new Quaternion(-0f, 0.6428f, -0f, -0.766f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(-116.558f, 286.303f, 296.325f), rot = new Quaternion(0f, 0.766f, -0f, 0.6428f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(151.699f, 259.221f, 508.821f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(232.787f, 251.049f, 194.794f), rot = new Quaternion(-0f, 0.5736f, -0f, -0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(-29.911f, 261.008f, 190.334f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(-78.94f, 333.545f, 666.549f), rot = new Quaternion(0f, 0.5f, -0f, 0.866f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(253.997f, 253.699f, 502.155f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(170.091f, 251.925f, -383.785f), rot = new Quaternion(0f, 0.1736f, -0f, 0.9848f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(161.591f, 253.561f, -213.156f), rot = new Quaternion(0f, 0.342f, -0f, 0.9397f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(-1.643f, 262.87f, -45.366f), rot = new Quaternion(0f, 0.5736f, -0f, 0.8192f), sca = new Vector3(1f, 1f, 1f) },
                new() { scene = "HubRegion", pos = new Vector3(57.644f, 256.597f, 368.46f), rot = new Quaternion(-0.1203f, 0.4126f, -0.055f, -0.9013f), sca = new Vector3(1f, 1f, 1f), que = true },
            };

            foreach (var e in values)
            {
                data.TryAdd(e.scene, []);
                QueDoorPosition q = new()
                {
                    position = e.pos,
                    rotation = e.rot,
                    scale = e.sca == Vector3.one ? null : e.sca,
                    doorless = e.que

                };
                data[e.scene].Add(q);
            }

            // serialize
            // print

            string dir = Path.Combine(modsPath, "RandomizerDebugOutput");
            dir = Path.Combine(dir, "Processed");
            string file = Path.Combine(dir, "MysteryDoorPotitions.json");

            string dataText = JsonSerializer.Serialize(data, Jsoning.GetDefaultOptions());

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(file, dataText);

        }



        //       ___                           __ 
        //      / _ \___ ___ ___ ___ _________/ / 
        //     / , _/ -_|_-</ -_) _ `/ __/ __/ _ \
        //    /_/|_|\__/___/\__/\_,_/_/  \__/_//_/
        //                                        

        public static bool breakCoroutines = false;

        public static string ToStringWithF(Vector3 v, string format = "0.###")
        {
            return $"({v.x.ToString(format)}f, {v.y.ToString(format)}f, {v.z.ToString(format)}f)"; // format vector3 string to (1.23f, 2.3f, 7.45f)
        }

        public static string ToStringWithF(Quaternion q, string format = "0.####")
        {
            return $"({q.x.ToString(format)}f, {q.y.ToString(format)}f, {q.z.ToString(format)}f, {q.w.ToString(format)}f)";
        }

        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.ExitMeshPlacement))]
        private static class ManagePostPlacement
        {
            internal static void Prefix(PlayerManager __instance)
            {
                if (__instance.GetObjectToPlace()?.name.StartsWith("RandomizerDoor_A") == true)
                {
                    if (Vector3.Distance(__instance.GetObjectToPlace().transform.position, Vector3.zero) < 0.01f)
                    {
                        UnityEngine.Object.Destroy(__instance.GetObjectToPlace());
                        return;
                    }


                    __instance.GetObjectToPlace().layer = vp_Layer.InteractiveProp;
                }
            }
        }

        public static GameObject GetInteractiveGameObjectUnderCrosshair()
        {
            if (!Main.gameStarted) return null;
            GameObject go = null;
            PlayerManager pm = GameManager.GetPlayerManagerComponent();

            float maxPickupRange = GameManager.GetGlobalParameters().m_MaxPickupRange;
            float maxRange = pm.ComputeModifiedPickupRange(maxPickupRange);
            if (pm.GetControlMode() == PlayerControlMode.InFPCinematic)
            {
                maxRange = 50f;
            }

            go = GameManager.GetPlayerManagerComponent().GetInteractiveObjectUnderCrosshairs(maxRange); // breaks here when going to main menu

            return go;
        }


        public static void DumpCurrentPlayerPos() // for position gathering
        { 



            // set pitch with GameManager.GetVpFPSCamera()?.m_Pitch

            string dir = Path.Combine(modsPath, "RandomizerDebugOutput");
            string file = Path.Combine(dir , "MysteryDoorDestinations.txt");

            string data = $"new() {{ scene = \"{GameManager.m_ActiveScene}\", pos = new Vector3{ToStringWithF(GameManager.GetPlayerTransform().position)}, pitch = {GameManager.GetVpFPSCamera()?.m_CurrentPitch.ToString("0.00")}f, yaw = {GameManager.GetVpFPSCamera()?.m_CurrentYaw.ToString("0.00")}f }},";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.AppendAllText(file, data);
            File.AppendAllText(file, "\n");
            
        }

        public static void DumpQueDoorPos(Transform t) // call after door been placed
        {
            List<(string scene, Vector3 pos, Quaternion rot, Vector3 sca)> values = new(){
                new() { scene = "RuralRegion", pos = new Vector3(1146.62f, 89.17f, 1778.19f), rot = new Quaternion(1f, 1f, 1f, 1f), sca = new Vector3(1f, 1f, 1f) }, // dump as this, then convert to dict by scene and to static json
            new() { scene = "RuralRegion", pos = new Vector3(2372.162f, 54.797f, 2272.896f), rot = new Quaternion(-0f, 0.766f, -0f, -0.6428f), sca = new Vector3(1f, 1f, 1f) },
            };

            // set pitch with GameManager.GetVpFPSCamera()?.m_Pitch

            string dir = Path.Combine(modsPath, "RandomizerDebugOutput");
            string file = Path.Combine(dir, "MysteryDoorPositions.txt");

            string data = $"new() {{ scene = \"{GameManager.m_ActiveScene}\", pos = new Vector3{ToStringWithF(t.position)}, rot = new Quaternion{ToStringWithF(t.rotation)}, sca = new Vector3{ToStringWithF(t.localScale)} }},";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.AppendAllText(file, data);
            File.AppendAllText(file, "\n");
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

            File.AppendAllText(file, JsonSerializer.Serialize(dataDict, Jsoning.GetDefaultOptions()));
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
#endif