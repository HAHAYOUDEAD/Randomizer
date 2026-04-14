using UnityEngine.AddressableAssets;
using ModSettings;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Rendering.RenderGraphModule;
using static Randomizer.Main;

namespace Randomizer
{
    internal class UnbreakablePatches
    {
        [HarmonyPatch(typeof(GameManager), nameof(GameManager.LoadSaveGameSlot), [typeof(string), typeof(int)])] // is not fired on new game
        private static class InitLoadingSandbox
        {
            internal static void Postfix()
            {
                bool enable = IsRandomizerEnabledForSaveslot();
                if (enable)
                {
                    if (Settings.options.seedMode != 2) // not dementia
                    {
                        globalSeed = AcquireSeed(false);

                        rolledPairs = Shuffle.RollRegularPairs(globalSeed);
                    }

                }

                Settings.SwitchTransitionsLabel(enable ? 1 : 0);
            }
        }

        [HarmonyPatch(typeof(SaveGameSlots), nameof(SaveGameSlots.SetSlotDisplayName))]
        public static class GetSlotDisplayNameEarly
        {
            public static string slotDisplayName = "";
            internal static void Prefix(string slotName, string displayName)
            {
                slotDisplayName = displayName.ToUpper();
            }
        }

        [HarmonyPatch(typeof(GameManager), nameof(GameManager.LaunchSandbox))] // is only fired on new game
        private static class InitStartingSandbox
        {
            internal static void Postfix()
            {
                bool enable = GetRandomizerEnabledFromSurvivalSettings.enableRandomizer;

                if (enable)
                {
                    if (Settings.options.seedMode != 2) // not dementia
                    {
                        globalSeed = AcquireSeed(true);

                        rolledPairs = Shuffle.RollRegularPairs(globalSeed);
                    }

                }

                // write/overwrite bool per sandbox to json, both true and false
                if (!globalSandboxToggleStatus.ContainsKey(SaveGameSystem.GetCurrentSaveName()))
                {
                    globalSandboxToggleStatus.Add(SaveGameSystem.GetCurrentSaveName(), enable);
                }
                else
                {
                    globalSandboxToggleStatus[SaveGameSystem.GetCurrentSaveName()] = enable;
                }

                Settings.SwitchTransitionsLabel(enable ? 1 : 0);

                File.WriteAllText(sandboxDataFilePath, JsonSerializer.Serialize(globalSandboxToggleStatus, Jsoning.GetDefaultOptions()));

            }
        }

        [HarmonyPatch(typeof(LoadScene), nameof(LoadScene.Awake))]
        private static class RandomizeTransition
        {
            internal static void Postfix(LoadScene __instance)
            {
                if (!__instance.gameObject.activeInHierarchy) return;
                if (!IsRandomizerEnabledForSaveslot()) return;

                string sceneName = GameManager.m_ActiveScene;

                if (Main.oncePerScene)
                {
                    OnSceneStart(sceneName);
                    Main.oncePerScene = false;
                }


                //SCRIPT_InterfaceManager/_GUI_Common/Camera/Anchor/Panel_ChooseSandbox/Details /Texts/ThumbnailTexture



                // check guid, and if already replaced - reroll and add to dict with guid

                // preselect some inconsistent buildings to be always present, defined by the seed. This is for stuff like prepper caches and non-unique houses/basements
                if (Settings.options.seedMode == 2) // dementia
                {

                    if (Dementia.lockout)
                    {
                        return;
                    }

                    if (IsScenePlayable(sceneName) && transitions.TryGetValue(sceneName, out var list))
                    {

                        TransitionDefinition? origin = list.FirstOrDefault(find => find.toScene == __instance.m_SceneToLoad && find.exitPoint == __instance.m_ExitPointName) ?? null;
                        
                        if (origin == null)
                        {
                            Log(CC.Gray, $"┌--{__instance.m_SceneToLoad}: {__instance.m_ExitPointName}");
                            Log(CC.Yellow, $"└▷ 𐌢 ");
                            return;
                        }

                        var pair = Dementia.DementiaRoll(origin);

                        if (pair == null)
                        {
                            Log(CC.Gray, $"┌--{__instance.m_SceneToLoad}: {__instance.m_ExitPointName}");
                            Log(CC.Yellow, $"└▷ 𐌢 ");
                            return;
                        }

                        Log(CC.Gray, $"┌--{__instance.m_SceneToLoad}: {__instance.m_ExitPointName}");
                        Log(CC.Green, $"└▷ {pair.toScene}: {pair.exitPoint}");
                        __instance.m_SceneToLoad = pair.toScene;
                        __instance.m_ExitPointName = pair.exitPoint;
                        __instance.m_SceneCanBeInstanced = allScenesData[pair.toScene].instantiable;
                        if (Settings.options.hideTransitionLabels) __instance.m_SceneLocationLocIDToShow = "? ? ?";
                    }

                }
                else
                {
                    if (IsScenePlayable(sceneName) && rolledPairs.TryGetValue(sceneName, out var pairs))
                    {
                        int i = 0;
                        foreach (var pair in pairs)
                        {
                            if (pair.Key.toScene == __instance.m_SceneToLoad && pair.Key.exitPoint == __instance.m_ExitPointName)
                            {
                                i++;
                                Log(CC.Gray, $"┌--{__instance.m_SceneToLoad}: {__instance.m_ExitPointName}");
                                Log(CC.Green, $"└▷ {pair.Value.toScene}: {pair.Value.exitPoint}");
                                __instance.m_SceneToLoad = pair.Value.toScene;
                                __instance.m_ExitPointName = pair.Value.exitPoint;
                                __instance.m_SceneCanBeInstanced = allScenesData[pair.Value.toScene].instantiable;
                                if (Settings.options.hideTransitionLabels) __instance.m_SceneLocationLocIDToShow = "? ? ?";
                                break;
                            }
                        }
                        if (i == 0)
                        {
                            Log(CC.Gray, $"┌--{__instance.m_SceneToLoad}: {__instance.m_ExitPointName}");
                            Log(CC.Yellow, $"└▷ 𐌢 ");
                        }
                    }
                    else
                    {
                        Log(CC.Gray, $"┌--{__instance.m_SceneToLoad}: {__instance.m_ExitPointName}");
                        Log(CC.Yellow, $"└▷ 𐌢 ");
                    }
                }

            }
        }

        //GameManager.m_SceneTransitionData.m_LastOutdoorScene is inconsistent with actual lasst outdoor scene; seems to always defailt to LakeRegion


        [HarmonyPatch(typeof(LoadScene), nameof(LoadScene.CompleteActivate), [typeof(bool)])]
        private static class TrackRegionForInterior
        {
            internal static void Postfix(LoadScene __instance)
            {
                if (!IsRandomizerEnabledForSaveslot()) return;
                string currentScene = GameManager.m_ActiveScene;
                string sceneToLoad = __instance.m_SceneToLoad;

                if (Settings.options.seedMode == 2) // dementia
                { 
                    if (Dementia.ShouldLockoutDementia())
                    {
                        if (!Dementia.lockout) Log(CC.Magenta, "Overstimulated by dementia");
                        Dementia.lockout = true;
                        if (!GameManager.GetHeadacheComponent().HasHeadache())
                        {
                            HeadacheData hd = GameManager.GetHeadacheComponent().m_LegacyHeadacheData;
                            hd.m_CausedByLocalizedId = new LocalizedString() { m_LocalizationID = "RNZ_Dementia_Affliction" };
                            hd.m_HeadacheDescription = new LocalizedString() { m_LocalizationID = Utils.RollChance(2) ? "RNZ_Dementia_AfflictionDescriptionRare" : "RNZ_Dementia_AfflictionDescription" };
                            /*
                            HeadacheData hd = new()
                            {
                                m_CausedByLocalizedId = new LocalizedString() { m_LocalizationID = "RNZ_Dementia_Affliction" },
                                m_HeadacheAfflictionIcoName = "ico_injury_headache",
                                m_HeadacheDescription = new LocalizedString() { m_LocalizationID = "GAMEPLAY_HeadacheEnergyBoostDescription" },
                                m_HeadacheLocalizedId = new LocalizedString() { m_LocalizationID = "GAMEPLAY_HeadacheEnergyBoost" },
                                m_HeadachePulseEvent = "",
                                m_HeadachePulseFrequencyEnd = 2.5f,
                                m_HeadachePulseFrequencyStart = 1.7f,
                                m_HeadacheStartAudio = "Play_Headache_EnergyBoost",
                                m_HealedAfflictionLocalizedId = new LocalizedString() { m_LocalizationID = "GAMEPLAY_HeadacheEnergyBoostHealed" },
                                m_TreatmentRequiredDescription = new LocalizedString() { m_LocalizationID = ""},
                                m_HeadacheDurationHours = 0.5f
                            };
                            */
                            GameManager.GetHeadacheComponent().ApplyHeadache(hd);
                        }

                        Dementia.threshold = UnityEngine.Random.Range(4, 7);
                    }
                    else
                    {
                        if (Dementia.lockout) Log(CC.Blue, "Dementia is back baby");
                        Dementia.lockout = false;
                    }

                }
                else
                {
                    Dementia.Reset();
                }

                if (GameManager.GetWeatherComponent().IsIndoorScene() && allScenesData[currentScene].unique) // going out from from unique scene, transition data is correct
                {
                    if (allScenesData[sceneToLoad].region)
                    {
                        GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene = sceneToLoad;
                    }
                    else
                    {
                        GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene = "";
                    }
                }

                if (GameManager.GetWeatherComponent().IsIndoorScene() && !allScenesData[currentScene].unique) // going out from from non-unique scene, transition data might be incorrect since scene is instantiated
                {
                    // handle when implementing InconsistentTransitions
                }

                string a = __instance.m_SceneCanBeInstanced ? "can be instanced" : "cannot be instanced";
                Log(System.ConsoleColor.Yellow, $"Transitioning to {sceneToLoad}: {__instance.m_ExitPointName}, scene " + a);
            }
        }

        [HarmonyPatch(typeof(Panel_SurvivalSettings), nameof(Panel_SurvivalSettings.Enable))]
        private static class AddRandomizerToSurvivalSettings
        {
            public static Panel_SurvivalSettings.SurvivalSetting? randomizerPanel = null;
            static Texture2D tex;

            internal static void Prefix(Panel_SurvivalSettings __instance, bool enable)
            {
                if (!enable) return;
                if (tex == null) tex = Main.mainBundle.LoadAsset<Texture2D>(survivorSettingsTexture);
                if (randomizerPanel == null)
                {
                    randomizerPanel = new()
                    {
                        m_FeatureTexture = new AssetReferenceTexture2D(addressableRefHack),
                        m_FeatureTitle = new LocalizedString() { m_LocalizationID = "RNZ_NewGameTitle" },
                        m_FeatureDescription = new LocalizedString() { m_LocalizationID = "RNZ_NewGameDescription" }
                    };
                }

                Func<bool> yesShowIt = () => true;
                __instance.AddSurvivalSettingIfValid(randomizerPanel, yesShowIt);
                //move to be 1st so players don't skip over accidentally
                for (int i = 0; i < __instance.m_ActiveSurvivalSettings.Count; i++)
                {
                    var item = __instance.m_ActiveSurvivalSettings[i];
                    if (item.m_FeatureTexture.m_AssetGUID == addressableRefHack)
                    {
                        item.m_Toggled = false;
                        item.m_TextureHandle = Addressables.ResourceManager.CreateCompletedOperation(tex, null);
                        __instance.m_ActiveSurvivalSettings.RemoveAt(i);
                        __instance.m_ActiveSurvivalSettings.Insert(0, item);
                        break;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Panel_ChooseSandbox), nameof(Panel_ChooseSandbox.RefreshDetails))]
        private static class MarkRandomizedSaveSlots
        {
            static GameObject overlay = null;
            static Texture2D tex;
            internal static void Postfix(Panel_ChooseSandbox __instance)
            {

                GameObject thumbnail = __instance.m_DetailObjects.m_ThumbnailTexture.gameObject;

                globalSandboxToggleStatus.TryGetValue(__instance.GetSelectedSaveSlotInfo().m_SaveSlotName, out bool isRandomized);

                if (tex == null) tex = Main.mainBundle.LoadAsset<Texture2D>(saveSlotOverlayTexture);

                if (!overlay)
                {
                    overlay = new GameObject("RandomizerOverlay");
                    UITexture uitex = overlay.AddComponent<UITexture>();
                    tex = Main.mainBundle.LoadAsset<Texture2D>(saveSlotOverlayTexture);
                    uitex.mainTexture = tex;

                    overlay.transform.localScale = Vector3.one * 0.94f;
                    overlay.transform.parent = thumbnail.transform.parent;
                    overlay.transform.SetAsFirstSibling();

                    overlay.transform.localPosition = Vector3.zero;
                    overlay.transform.position = new Vector3(1.07f, 0.365f, 0f);

                    overlay.transform.localRotation = Quaternion.identity;
                    overlay.transform.eulerAngles = new Vector3(0f, 0f, 1.4f);

                    uitex.depth = 1000;
                }

                overlay.active = isRandomized;
            }
        }

        [HarmonyPatch(typeof(Panel_SurvivalSettings), nameof(Panel_SurvivalSettings.HandleOnClickContinue))]
        private static class GetRandomizerEnabledFromSurvivalSettings
        {
            public static bool enableRandomizer = false;
            internal static void Postfix(Panel_SurvivalSettings __instance)
            {
                if (AddRandomizerToSurvivalSettings.randomizerPanel != null)
                {
                   
                    enableRandomizer = AddRandomizerToSurvivalSettings.randomizerPanel.m_Toggled;
                }
            }
        }

        [HarmonyPatch(typeof(Panel_SurvivalSettings.SurvivalSetting), nameof(Panel_SurvivalSettings.SurvivalSetting.LoadTextureIfNeeded))]
        private static class PreventAddressableOperation
        {
            internal static bool Prefix(Panel_SurvivalSettings.SurvivalSetting __instance)
            {
                if (__instance.m_FeatureTexture.m_AssetGUID == addressableRefHack)
                {
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(GameManager), nameof(GameManager.ResetLists))]
        private static class ReseetLists
        {
            internal static void Postfix()
            {
                QueDoors.spawnedDoors.Clear();
                foreach (var d in QueDoors.spawnedDecorations)
                {
                    if (d != null) UnityEngine.Object.Destroy(d);
                }    
                QueDoors.spawnedDecorations.Clear();
                ConsoleCommands.lastDestIndex = -1;
                
            }
        }

        [HarmonyPatch] // ModSettings patch to add warning
        class InjectWarning
        {
            public static MethodBase TargetMethod()
            {
                var type = AccessTools.TypeByName("ModSettings.ModSettingsGUI");
                return AccessTools.FirstMethod(type, method => method.Name.Contains("SelectMod"));
            }
            public static void Postfix(string modName)
            {
                Settings.ToggleWarning(modName == settingsName);
            }
        }
    }
}
 