global using static Randomizer.Utility;
using Harmony;
using Il2Cpp;
using Il2CppNewtonsoft.Json.Utilities;
using JetBrains.Annotations;
using MelonLoader.Utils;
using Randomizer.src;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using UnityEngine;
using static UnityEngine.ParticleSystem.PlaybackState;
using static UnityEngine.UI.Image;
using static UnityEngine.UI.Selectable;


namespace Randomizer
{


    public class Main : MelonMod
    {
        public static bool gameStarted = false;
        public static bool oncePerScene = true;
        public static AssetBundle? mainBundle;

        public static Shader transparentShader = Shader.Find("Shader Forge/TLD_StandardTransparent");
        public static Shader standardShader = Shader.Find("Shader Forge/TLD_StandardDiffuse");

        public override void OnInitializeMelon()
        {
            modsPath = MelonEnvironment.ModsDirectory;

            string dir = Path.Combine(modsPath + mainFolder);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            Settings.OnLoad();

            LocalizationManager.LoadJsonLocalization(LoadEmbeddedJSON("Localization.json"));

            transitions = JsonSerializer.Deserialize<Dictionary<string, TransitionDefinition[]>>(LoadEmbeddedJSON("Transitions.json"), GetDefaultJsonOptions()) ?? [];
            inconsistentTransitions = JsonSerializer.Deserialize<Dictionary<string, TransitionDefinition[]>>(LoadEmbeddedJSON("InconsistentTransitions.json"), GetDefaultJsonOptions()) ?? [];

            mainBundle = LoadEmbeddedAssetBundle("randomizer");

            survivorSettingsTexture = mainBundle.LoadAsset<Texture2D>("RandomizerSelectionBG3.png");

            //RunTransitionDictionaryIntegrityCheck();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            gameStarted = true;
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

                GameManager.GetPlayerManagerComponent().StartPlaceMesh(MysteryDoors.PrepareMysteryDoor(), PlaceMeshFlags.DestroyOnCancel);
            }           
            
            if (InputManager.GetKeyDown(InputManager.m_CurrentContext, KeyCode.L))
            {
                if (InterfaceManager.DetermineIfOverlayIsActive()) return;
                if (uConsole.IsOn()) return;
                Debug.DumpCurrentPlayerPos();
                HUDMessage.AddMessage("Writing current player pos to file", true, true);
            }

#endif

#if !SHENANIGANS
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
    }     
}




