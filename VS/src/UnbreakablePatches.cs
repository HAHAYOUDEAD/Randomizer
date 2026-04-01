namespace Randomizer
{
    internal class UnbreakablePatches
    {

        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.Awake))]
        private static class ExamplePatch
        {
            internal static void Prefix(ref PlayerManager __instance)
            {

            }
        }
    }
}
 