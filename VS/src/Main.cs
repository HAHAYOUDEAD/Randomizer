global using static Randomizer.Utility;
using AssetsTools.NET.Extra;
using Harmony;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNewtonsoft.Json.Utilities;
using Il2CppTLD.AddressableAssets;
using Il2CppTLD.OptionalContent;
using Il2CppTLD.Trader;
using JetBrains.Annotations;
using MelonLoader.Utils;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using UnityEngine;
using static Randomizer.UnbreakablePatches;
using static UnityEngine.ParticleSystem.PlaybackState;
using static UnityEngine.UI.Image;
using static UnityEngine.UI.Selectable;
using AssetHelper = Il2CppTLD.AddressableAssets.AssetHelper;


namespace Randomizer
{


    public class Main : MelonMod
    {
        public static bool gameStarted = false;
        public static bool oncePerScene = true;
        public static AssetBundle? mainBundle;

        public static bool TFTFTInstalled = false;

        public override void OnInitializeMelon()
        {
            modsPath = MelonEnvironment.ModsDirectory;

            string dir = Path.Combine(modsPath, mainFolder);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            Settings.OnLoad();

            LocalizationManager.LoadJsonLocalization(LoadEmbeddedJSON("Localization.json"));

            transitions = JsonSerializer.Deserialize<Dictionary<string, TransitionDefinition[]>>(LoadEmbeddedJSON("Transitions.json"), Jsoning.GetDefaultOptions()) ?? [];
            inconsistentTransitions = JsonSerializer.Deserialize<Dictionary<string, TransitionDefinition[]>>(LoadEmbeddedJSON("InconsistentTransitions.json"), Jsoning.GetDefaultOptions()) ?? [];
            queDoorPositionPool = JsonSerializer.Deserialize<Dictionary<string, QueDoorPosition[]>>(LoadEmbeddedJSON("MysteryDoorPotitions.json"), Jsoning.GetDefaultOptions()) ?? [];
            queDoorDestinationPool = JsonSerializer.Deserialize<Dictionary<string, QueDoorDestination[]>>(LoadEmbeddedJSON("MysteryDoorDestinations.json"), Jsoning.GetDefaultOptions()) ?? [];

            string sandboxDataFile = Path.Combine(dir, sandboxDataFilename + ".json");

            if (File.Exists(sandboxDataFile))
            {
                globalSandboxToggleStatus = JsonSerializer.Deserialize<Dictionary<string, bool>>(File.ReadAllText(sandboxDataFile), Jsoning.GetDefaultOptions()) ?? [];
            }

            mainBundle = LoadEmbeddedAssetBundle("randomizer");

            //playerHasTFTFT = OptionalContentManager.Instance.InstalledContent.ContainsKey("2091330");
            OptionalContentManager.Instance.TryFindConfigFromName("DLC01", out var config);
            TFTFTInstalled = OptionalContentManager.Instance.CanUseContent(config);

            //RunTransitionDictionaryIntegrityCheck();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (IsMainMenu(sceneName))
            {
                gameStarted = true;
                Settings.OnInitialize();
                Settings.SwitchTransitionsLabel(-1);
                Dementia.Reset();
            }
        }
        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            oncePerScene = true;

        }

        public override void OnUpdate()
        {

#if DEBUG
            if (gameStarted && InputManager.GetPauseMenuTogglePressed(InputManager.m_CurrentContext))
            {
                Debug.breakCoroutines = true;
            }

            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.O))
            {

                GameManager.GetPlayerManagerComponent().StartPlaceMesh(QueDoors.PrepareQueDoor(false, null), PlaceMeshFlags.DestroyOnCancel);

            } 
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.Alpha0))
            {

                GameManager.GetPlayerManagerComponent().StartPlaceMesh(QueDoors.PrepareQueDoor(true, null), PlaceMeshFlags.DestroyOnCancel);
            } 
            
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.P))
            {
                GameObject que = Debug.GetInteractiveGameObjectUnderCrosshair();
                if (que == null || !que.name.StartsWith("RandomizerDoor_A"))
                {
                    HUDMessage.AddMessage("Nope", true, true);
                    return;
                }
                Debug.DumpQueDoorPos(que.transform);
                HUDMessage.AddMessage("Writing Que door pos to file", true, true);
            }           
            
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.L))
            {
                if (InterfaceManager.DetermineIfOverlayIsActive()) return;
                if (uConsole.IsOn()) return;
                Debug.DumpCurrentPlayerPos();
                HUDMessage.AddMessage("Writing current player pos to file", true, true);
            }

#endif

#if SHENANIGANS
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

        public static void OnSceneStart(string sceneName)
        {
            if (!IsScenePlayable(sceneName)) return;

            if (Settings.options.seedMode == 2 && Dementia.lockout)
            {
                Log(CC.Magenta, "Dementia lockout, randomization skipped");
                return;
            }

            /* moved to late init because of async addressable load fail 
            if (Settings.options.shuffleMode != 2) // skip for outdoor only
            {
                if (QueDoors.SpawnQueDoor()) Log(CC.Magenta, "Creating a mystery door...");
                QueDoors.CreateHelperObjects(sceneName);
            }
            */

            UnlockDoors();

            string name = SaveGameSlots.GetUserDefinedSlotName(SaveGameSystem.GetCurrentSaveName());
            if (string.IsNullOrEmpty(name)) name = GetSlotDisplayNameEarly.slotDisplayName;
            string seed = (globalSeed == 42 && Settings.options.seedMode == 2) ? "?" : globalSeed.ToString();

            LogAlways(CC.Blue, $"Shuffling transitions in {sceneName}:");
            LogAlways(CC.Blue, $"Seed: {seed}/ {Settings.options.seedMode}. Algorithm: {Settings.options.shuffleMode}. " +
                $"Slot: {SaveGameSystem.GetCurrentSaveName().ToLower().Replace("sandbox", "")}. Slotname: '{name}'");
            Log(" ·?··?·· ·?··  ·  ·?· ·   · ··?·  ·     ·");

        }

        public static void UnlockDoors()
        {
            if (!GameManager.GetWeatherComponent().IsIndoorScene()) return;
            var doors = GameObject.FindObjectsOfType<LoadScene>();
            List<Lock> locks = new(); 
            foreach (var door in doors)
            {
                if(door.TryGetComponent<Lock>(out Lock l))
                {
                    if (l.m_LockState == LockState.Locked)
                    {
                        locks.Add(l);
                    }
                }
            }
            if (doors.Count == locks.Count)
            {
                if (GameManager.m_ActiveScene == "ConvenienceStoreA")
                {
                    var prybar = GameObject.Instantiate(GearItem.LoadGearItemPrefab("GEAR_Prybar"));
                    prybar.CurrentHP = 26f; // just past breaking threshold
                    prybar.transform.localPosition = new(0.435f, 2.28f, 8.78f);
                    prybar.transform.localEulerAngles = new(63.34f, 208.98f, 301.59f);
                }
                else
                {
                    locks[UnityEngine.Random.Range(0, locks.Count)].m_LockState = LockState.Unlocked;
                }
            }

        }


        public static bool IsRandomizerEnabledForSaveslot()
        {
            bool enabled = false;
            string saveslot = SaveGameSystem.GetCurrentSaveName();
            if (saveslot == null) return false;
            if (!globalSandboxToggleStatus.TryGetValue(saveslot, out enabled))
            {
                globalSandboxToggleStatus.Add(saveslot, false);
            }
            return enabled;
        }

        /// <summary>
        /// ⚠ Only call when saveslot is loaded ⚠
        /// </summary>
        public static int AcquireSeed(bool newGame)
        {
            if (Settings.options.seedMode == 0) // random
            {
                if (newGame)
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
                if (newGame)
                {
                    MelonLogger.Msg("seed newgame " + SaveGameSystem.GetCurrentSaveName());
                    return GetSeedFromSandboxName(UnbreakablePatches.GetSlotDisplayNameEarly.slotDisplayName);

                }
                else
                {
                    return GetSeedFromSandboxName(SaveGameSlots.GetUserDefinedSlotName(SaveGameSystem.GetCurrentSaveName())); // create from saveslot name
                }

            }
            else if (Settings.options.seedMode == 3) // debug
            {
                return 42;
            }

            return 42;
        }
    }     
}




