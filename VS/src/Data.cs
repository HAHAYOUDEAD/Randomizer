using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler;
using UnityEngine.UIElements;
using static Il2CppSystem.Globalization.TimeSpanParse;

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
    public enum Version
    {
        Vanilla,
        TFTFT
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

    public class SceneData 
    { 
        public Version version;
        public int size;
        public bool unique;
        public bool instantiable;
        public bool region;

    }

    public class QueDoorPosition
    {
        public Vector3 position;
        public Quaternion rotation;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Vector3? scale = null;
        public bool doorless = false;
    }
    public class QueDoorDestination
    {
        public Vector3 position;
        public float pitch;
        public float yaw;
    }

    public class Pair
    {
        public TransitionDefinition In { get; set; }
        public TransitionDefinition Out { get; set; }

        public Pair(TransitionDefinition inDef, TransitionDefinition outDef)
        {
            In = inDef;
            Out = outDef;
        }
    }
    public class Data
    {
        public static Dictionary<string, TransitionDefinition[]> transitions = [];
        public static Dictionary<string, TransitionDefinition[]> inconsistentTransitions = [];
        public static Dictionary<string, Dictionary<TransitionDefinition, TransitionDefinition>> rolledPairs = []; // origonal scene, <transition info > replacer transition info>
        public static Dictionary<string, QueDoorPosition[]> queDoorPositionPool = [];
        public static Dictionary<string, QueDoorDestination[]> queDoorDestinationPool = [];
        public static Dictionary<string, bool> globalSandboxToggleStatus = [];

        public const string survivorSettingsTexture = "RandomizerSelectionBG3.png";
        public const string saveSlotOverlayTexture = "SaveSlotOverlay.png";

        public static int globalSeed = 42;

        public static Dictionary<string, SceneData> allScenesData = new() {
            { "AFHangar",                       new SceneData { version = Version.TFTFT,   size = 3,  unique = true,  instantiable = false, region = false } },
            { "AirfieldRegion",                 new SceneData { version = Version.TFTFT,   size = 8,  unique = true,  instantiable = false, region = true  } },
            { "AirfieldTrailerB",               new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = true,  region = false } },
            { "AirfieldWoodCabinA",             new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = true,  region = false } },
            { "AshCabinD",                      new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "AshCabinF",                      new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "AshCanyonRegion",                new SceneData { version = Version.Vanilla, size = 10, unique = true,  instantiable = false, region = true  } },
            { "AshCaveA",                       new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "AshCaveB",                       new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "AshMine",                        new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "AshWoodCabinA",                  new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "BankA",                          new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "BarnHouseA",                     new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "BarnHouseB",                     new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "BlackRockTrailerB",              new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "BlackrockCaveA",                 new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "BlackrockInteriorASurvival",     new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "BlackrockMineA",                 new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "BlackrockPowerplantA",           new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "BlackrockPrisonSurvivalZone",    new SceneData { version = Version.Vanilla, size = 3,  unique = true,  instantiable = false, region = true  } },
            { "BlackrockRegion",                new SceneData { version = Version.Vanilla, size = 10, unique = true,  instantiable = false, region = true  } },
            { "BlackrockSteamTunnelsASurvival", new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "BlackrockTransitionZone",        new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = true  } },
            { "BunkerA",                        new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = false, region = false } },
            { "BunkerB",                        new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = false, region = false } },
            { "BunkerC",                        new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = false, region = false } },
            { "BunkerXL",                       new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = false, region = false } },
            { "CampOffice",                     new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "CanneryMarshTransitionCave",     new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "CanneryRegion",                  new SceneData { version = Version.Vanilla, size = 8,  unique = true,  instantiable = false, region = true  } },
            { "CanneryTrailerA",                new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "CanyonRoadCave",                 new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "CanyonRoadTransitionZone",       new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = true  } },
            { "CaveB",                          new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "CaveC",                          new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "CaveD",                          new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "ChurchB",                        new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "ChurchC",                        new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "CoastalHouseA",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "CoastalHouseB",                  new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = true,  region = false } },
            { "CoastalHouseC",                  new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = true,  region = false } },
            { "CoastalHouseD",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "CoastalHouseE",                  new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "CoastalHouseF",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "CoastalHouseH",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "CoastalRegion",                  new SceneData { version = Version.Vanilla, size = 11, unique = true,  instantiable = false, region = true  } },
            { "CommunityHallA",                 new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "ConvenienceStoreA",              new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "CrashMountainRegion",            new SceneData { version = Version.Vanilla, size = 8,  unique = true,  instantiable = false, region = true  } },
            { "Dam",                            new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "DamCaveTransitionZone",          new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "DamRiverTransitionZoneB",        new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = true  } },
            { "DamTrailerB",                    new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "DamTransitionZone",              new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "FarmHouseA",                     new SceneData { version = Version.Vanilla, size = 4,  unique = true,  instantiable = false, region = false } },
            { "FarmHouseABasement",             new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "FarmHouseB",                     new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "FishingCabinA",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "FishingCabinC",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "FishingCabinD",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "GreyMothersHouseA",              new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "HighwayMineTransitionZone",      new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "HighwayTransitionZone",          new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = true  } },
            { "HouseBasementC",                 new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "HouseBasementPV",                new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "HubCave",                        new SceneData { version = Version.TFTFT,   size = 3,  unique = true,  instantiable = false, region = false } },
            { "HubRegion",                      new SceneData { version = Version.TFTFT,   size = 4,  unique = true,  instantiable = false, region = true  } },
            { "HuntingLodgeA",                  new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "IceCaveA",                       new SceneData { version = Version.Vanilla, size = 3,  unique = true,  instantiable = false, region = false } },
            { "IceCaveB",                       new SceneData { version = Version.Vanilla, size = 3,  unique = true,  instantiable = false, region = false } },
            { "LakeCabinA",                     new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "LakeCabinB",                     new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "LakeCabinC",                     new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "LakeCabinD",                     new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "LakeCabinE",                     new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "LakeCabinF",                     new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "LakeRegion",                     new SceneData { version = Version.Vanilla, size = 8,  unique = true,  instantiable = false, region = true  } },
            { "LighthouseA",                    new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "LongRailTransitionZone",         new SceneData { version = Version.TFTFT,   size = 4,  unique = true,  instantiable = false, region = true  } },
            { "LongTransitionCave",             new SceneData { version = Version.TFTFT,   size = 2,  unique = true,  instantiable = false, region = false } },
            { "MaintenanceShedA",               new SceneData { version = Version.Vanilla, size = 3,  unique = true,  instantiable = false, region = false } },
            { "MaintenanceShedB",               new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "MarshRegion",                    new SceneData { version = Version.Vanilla, size = 6,  unique = true,  instantiable = false, region = true  } },
            { "MiltonHouseA",                   new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "MiltonHouseC",                   new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "MiltonHouseD",                   new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "MiltonHouseF1",                  new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "MiltonHouseF2",                  new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "MiltonHouseF3",                  new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "MiltonHouseH1",                  new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "MiltonHouseH2",                  new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "MiltonHouseH3",                  new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "MiltonTrailerB",                 new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "MineConcentratorBuilding",       new SceneData { version = Version.TFTFT,   size = 11, unique = true,  instantiable = false, region = false } },
            { "MineTransitionZone",             new SceneData { version = Version.Vanilla, size = 5,  unique = true,  instantiable = false, region = false } },
            { "MiningRegion",                   new SceneData { version = Version.TFTFT,   size = 14, unique = true,  instantiable = false, region = true  } },
            { "MiningRegionMine",               new SceneData { version = Version.TFTFT,   size = 7,  unique = true,  instantiable = false, region = false } },
            { "MountainCaveA",                  new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "MountainCaveB",                  new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "MountainPassBasement",           new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = false, region = false } },
            { "MountainPassBuriedCabin",        new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = false, region = false } },
            { "MountainPassCabinA",             new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = false, region = false } },
            { "MountainPassCaveA",              new SceneData { version = Version.TFTFT,   size = 4,  unique = true,  instantiable = false, region = false } },
            { "MountainPassCaveB",              new SceneData { version = Version.TFTFT,   size = 2,  unique = true,  instantiable = false, region = false } },
            { "MountainPassRegion",             new SceneData { version = Version.TFTFT,   size = 12, unique = true,  instantiable = false, region = true  } },
            { "MountainTownCaveA",              new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "MountainTownCaveB",              new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "MountainTownRegion",             new SceneData { version = Version.Vanilla, size = 26, unique = true,  instantiable = false, region = true  } },
            { "PostOfficeA",                    new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "PrepperCacheA",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheAEmpty",             new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheB",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheBEmpty",             new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheBInterloper",        new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheC",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheCEmpty",             new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheD",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheDEmpty",             new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheE",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheEEmpty",             new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheEmpty",              new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "PrepperCacheF",                  new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PrepperCacheFEmpty",             new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = false, region = false } },
            { "PumpHouse",                      new SceneData { version = Version.TFTFT,   size = 2,  unique = true,  instantiable = false, region = false } },
            { "QuonsetGasStation",              new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "RadarBuilding",                  new SceneData { version = Version.TFTFT,   size = 1,  unique = true,  instantiable = false, region = false } },
            { "RadioControlHut",                new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "RadioControlHutB",               new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "RadioControlHutC",               new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "RavineTransitionZone",           new SceneData { version = Version.Vanilla, size = 3,  unique = true,  instantiable = false, region = true  } },
            { "RiverValleyRegion",              new SceneData { version = Version.Vanilla, size = 8,  unique = true,  instantiable = false, region = true  } },
            { "RiverValleyTransitionCave",      new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "RuralRegion",                    new SceneData { version = Version.Vanilla, size = 21, unique = true,  instantiable = false, region = true  } },
            { "RuralStoreA",                    new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "SafeHouseA",                     new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = false, region = false } },
            { "TracksRegion",                   new SceneData { version = Version.Vanilla, size = 7,  unique = true,  instantiable = false, region = true  } },
            { "TrailerA",                       new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "TrailerB",                       new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "TrailerC",                       new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "TrailerD",                       new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "TrailerE",                       new SceneData { version = Version.Vanilla, size = 0,  unique = false, instantiable = true,  region = false } },
            { "TrailerSShape",                  new SceneData { version = Version.Vanilla, size = 3,  unique = true,  instantiable = false, region = false } },
            { "WhalingMine",                    new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "WhalingShipA",                   new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "WhalingStationRegion",           new SceneData { version = Version.Vanilla, size = 13, unique = true,  instantiable = false, region = true  } },
            { "WhalingWarehouseA",              new SceneData { version = Version.Vanilla, size = 2,  unique = true,  instantiable = false, region = false } },
            { "WoodCabinA",                     new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "WoodCabinB",                     new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
            { "WoodCabinC",                     new SceneData { version = Version.Vanilla, size = 1,  unique = true,  instantiable = true,  region = false } },
        };
    }
}
