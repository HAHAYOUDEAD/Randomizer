using Il2Cpp;
using Il2CppNodeCanvas.Tasks.Conditions;

namespace Randomizer
{
    internal class UnbreakablePatches
    {
        /*
        [HarmonyPatch(typeof(SaveGameSystem), nameof(SaveGameSystem.LoadSceneData))] // replace with something that fires after asset load not save game
        private static class ReplaceTransitionPointers
        {
            internal static void Postfix(string name, string sceneSaveName) // name = sandbox name; sceneSaveName = scene name with guid appended (if used) via _ 
            {
                string sceneName = GameManager.m_ActiveScene;

                if (IsScenePlayable(sceneName) && rolledPairs.TryGetValue(sceneName, out var pairs))
                {
                    Log(System.ConsoleColor.Gray, $"Replacing transitions in scene {sceneName}:");
                    foreach (var comp in UnityEngine.Object.FindObjectsOfType<LoadScene>())
                    {
                        // check guid, and if already replaced - reroll and add to dict with guid


                        // preselect some inconsistent buildings to be always present, defined by the seed. This is for stuff like prepper caches and non-unique houses/basements

                        int i = 0;
                        foreach (var pair in pairs)
                        {
                            if (pair.Key.toScene == comp.m_SceneToLoad && pair.Key.exitPoint == comp.m_ExitPointName)
                            {
                                i++;


                                Log(System.ConsoleColor.Gray, $"┌--{comp.m_SceneToLoad}: {comp.m_ExitPointName}");
                                Log(System.ConsoleColor.Blue, $"└▷ {pair.Value.toScene}: {pair.Value.exitPoint}");
                                comp.m_SceneToLoad = pair.Value.toScene;
                                comp.m_ExitPointName = pair.Value.exitPoint;
                                comp.m_SceneCanBeInstanced = allScenesData[pair.Value.toScene].instantiable;
                                if (Settings.options.hideTransitionLabels) comp.m_SceneLocationLocIDToShow = "? ? ?";
                                break;
                            }

                        }
                        if (i == 0)
                        {
                            Log(System.ConsoleColor.Gray, $"┌--{comp.m_SceneToLoad}: {comp.m_ExitPointName}");
                            Log(System.ConsoleColor.Red,  $"└▷ 𐌢");
                        }
                    }
                }
            }
        }

        */
        
        
        [HarmonyPatch(typeof(LoadScene), nameof(LoadScene.Start))]
        private static class RandomizeTransition
        {
            internal static void Postfix(LoadScene __instance)
            {
                string sceneName = GameManager.m_ActiveScene;

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
                            Log(System.ConsoleColor.Gray,  $"┌--{__instance.m_SceneToLoad}: {__instance.m_ExitPointName}");
                            Log(System.ConsoleColor.Green, $"└▷ {pair.Value.toScene}: {pair.Value.exitPoint}");
                            __instance.m_SceneToLoad = pair.Value.toScene;
                            __instance.m_ExitPointName = pair.Value.exitPoint;
                            __instance.m_SceneCanBeInstanced = allScenesData[pair.Value.toScene].instantiable;
                            if (Settings.options.hideTransitionLabels) __instance.m_SceneLocationLocIDToShow = "? ? ?";
                            break;
                        }
                    }
                    if (i == 0)
                    {
                        Log(System.ConsoleColor.Gray, $"┌--{__instance.m_SceneToLoad}: {__instance.m_ExitPointName}");
                        Log(System.ConsoleColor.Red,  $"└▷ 𐌢");
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

                if (!__instance.m_SceneCanBeInstanced) 
                {
                    GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene = ""; // it's not used for anything meaningful, but we keep it somewhat consistent with vvanilla anyways
                }
                else if (GameManager.GetWeatherComponent().IsIndoorScene())
                {
                    GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene = "";
                }
                else 
                {
                    // handled by vanilla
                }

                if (allScenesData[currentScene].instantiable && allScenesData[sceneToLoad].region) // fix inconsistency when instantiable interior can act as transition between 2 regions
                {
                    GameManager.m_SceneTransitionData.m_ForceNextSceneLoadTriggerScene = sceneToLoad;
                }

                string a = __instance.m_SceneCanBeInstanced ? "can be instanced" : "cannot be instanced";
                Log(System.ConsoleColor.Yellow, $"Transitioning to {sceneToLoad}: {__instance.m_ExitPointName}, scene " + a);
            }
        }      
    }
}
 