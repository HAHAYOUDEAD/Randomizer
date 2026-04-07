using UnityEngine.AddressableAssets;
using static UnityEngine.GraphicsBuffer;

namespace Randomizer
{
    internal class UnbreakablePatches
    {
        [HarmonyPatch(typeof(GameManager), nameof(GameManager.LoadSaveGameSlot), [typeof(string), typeof(int)])] // is not fired on new game
        private static class InitLoadingSandbox
        {
            internal static void Postfix()
            {


                // check if sandbox? to accidentally not randomize challenges



                // read from ini, if doesn't exist obv false



                globalSeed = Function.AcquireSeed(true);

                rolledPairs = Function.RollPairs(globalSeed);
            }
        }        
        [HarmonyPatch(typeof(GameManager), nameof(GameManager.LaunchSandbox))] // is only fired on new game
        private static class InitLoadingSandbox2
        {
            internal static void Postfix()
            {
                if (!GetRandomizerEnabledFromSurvivalSettings.enableRandomizer) return;
                else
                {
                    globalSeed = Function.AcquireSeed(false);

                    rolledPairs = Function.RollPairs(globalSeed);

                    // write/overwrite bool per sandbox to ini, both true and false
                }

            }
        }        

        [HarmonyPatch(typeof(LoadScene), nameof(LoadScene.Awake))]
        private static class RandomizeTransition
        {
            internal static void Postfix(LoadScene __instance)
            {
                if (!__instance.gameObject.activeInHierarchy) return;
                //if (__instance.m_StartHasBeenCalled) return;

                string sceneName = GameManager.m_ActiveScene;

                if (Main.oncePerScene)
                {
                    Main.oncePerScene = false;
                    LogAlways(CC.Blue, $"Shuffling transitions in {sceneName}. Seed: {globalSeed}. Algorithm: {Settings.options.shuffleMode}. " +
                        $"Slot: {SaveGameSystem.GetCurrentSaveName().ToLower().Replace("sandbox", "")}. Slotname: {SaveGameSlots.GetUserDefinedSlotName(SaveGameSystem.GetCurrentSaveName())}");
                    Log($"       ┄┅ ✧   ?  ←→  ?   ✧ ┅┄");
                }
                

                // check guid, and if already replaced - reroll and add to dict with guid

                // preselect some inconsistent buildings to be always present, defined by the seed. This is for stuff like prepper caches and non-unique houses/basements


                if (IsScenePlayable(sceneName) && rolledPairs.TryGetValue(sceneName, out var pairs))
                {
                    int i = 0;
                    foreach (var pair in pairs)
                    {
                        if (pair.Key.toScene == __instance.m_SceneToLoad && pair.Key.exitPoint == __instance.m_ExitPointName)
                        {
                            i++;
                            Log(CC.Gray,  $"┌--{__instance.m_SceneToLoad}: {__instance.m_ExitPointName}");
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
                        Log(CC.Red,  $"└▷ 𐌢");
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
                string currentScene = GameManager.m_ActiveScene;
                string sceneToLoad = __instance.m_SceneToLoad;

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
                /*


                if (!__instance.m_SceneCanBeInstanced) 
                {
                    GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene = ""; // this is used with instantiable interiors that have predefined toScene that can be incorrect
                }
                else if (GameManager.GetWeatherComponent().IsIndoorScene() && allScenesData[currentScene].unique) // load trigger will have correct transition data, handled by mod
                {
                    GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene = "";
                }
                else 
                {
                    // handled by vanilla
                }

                if (allScenesData[currentScene].instantiable && allScenesData[sceneToLoad].region && allScenesData[currentScene].unique) // fix inconsistency when instantiable interior can act as transition between 2 regions
                {
                    GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene = sceneToLoad;
                }
                */

                string a = __instance.m_SceneCanBeInstanced ? "can be instanced" : "cannot be instanced";
                Log(System.ConsoleColor.Yellow, $"Transitioning to {sceneToLoad}: {__instance.m_ExitPointName}, scene " + a);
            }
        }

        [HarmonyPatch(typeof(Panel_SurvivalSettings), nameof(Panel_SurvivalSettings.Enable))]
        private static class AddRandomizerToSurvivalSettings
        {
            public static Panel_SurvivalSettings.SurvivalSetting randomizerPanel = null;
            
            internal static void Prefix(Panel_SurvivalSettings __instance)
            {
                randomizerPanel = new()
                {
                    m_FeatureTexture = new AssetReferenceTexture2D(addressableRefHack),
                    m_FeatureTitle = new LocalizedString() { m_LocalizationID = "RNZ_NewGameTitle" },
                    m_FeatureDescription = new LocalizedString() { m_LocalizationID = "RNZ_NewGameDescription" }
                };
                Func<bool> yesShowIt = () => true;

                __instance.AddSurvivalSettingIfValid(randomizerPanel, yesShowIt);

                //move to be 1st so players don't skip over accidentally
                for (int i = 0; i < __instance.m_ActiveSurvivalSettings.Count; i++)
                {
                    var item = __instance.m_ActiveSurvivalSettings[i];

                    if (item.m_FeatureTexture.m_AssetGUID == addressableRefHack) 
                    {
                        if (item.m_TextureHandle.Result == null)
                        {
                            item.m_TextureHandle = Addressables.ResourceManager.CreateCompletedOperation(Main.mainBundle.LoadAsset<Texture2D>("RandomizerSelectionBG3.png"), null); // fake handle bacuse I can't be arsed with addressables for a single texture
                        }
                        item.m_Toggled = false;
                        __instance.m_ActiveSurvivalSettings.RemoveAt(i);
                        __instance.m_ActiveSurvivalSettings.Insert(0, item);
                        break;
                    }
                }
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
    }
}
 