using Il2Cpp;

namespace Randomizer
{
    internal class UnbreakablePatches
    {

        [HarmonyPatch(typeof(SaveGameSystem), nameof(SaveGameSystem.LoadSceneData))] // replace with something that fires after asset load not save game
        private static class ReplaceTransitionPointers
        {
            internal static void Postfix(string name, string sceneSaveName) // name = sandbox name; sceneSaveName = scene name with guid appended (if used) via _ 
            {
                string sceneName = GameManager.m_ActiveScene;

                if (IsScenePlayable(sceneName) && Main.rolledPairs.TryGetValue(sceneName, out var pairs))
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
                                if (Settings.options.hideTransitionLabels) comp.m_SceneLocationLocIDToShow = "? ? ?";
                                break;
                            }

                        }
                        if (i == 0)
                        {
                            Log(System.ConsoleColor.Gray, $"┌--{comp.m_SceneToLoad}: {comp.m_ExitPointName}");
                            Log(System.ConsoleColor.Red, $"└▷ 𐌢");
                        }
                    }
                }
            }
        }
    }
}
 