global using CC = System.ConsoleColor;
global using System;
global using MelonLoader;
global using HarmonyLib;
global using UnityEngine;
global using System.Reflection;
global using System.Collections;
global using System.Collections.Generic;
global using Il2Cpp;
global using System.Text.Json;
global using System.Text;
global using static Randomizer.Data;
global using static Randomizer.Shuffle;
global using LocalizationUtilities;
using System.Text.Json.Serialization;
using MelonLoader.Utils;

namespace Randomizer
{
    internal class Utility
    {
        public const string modVersion = "0.3.0";
        public const string modName = "Randomizer-Transitions";
        public const string modAuthor = "Waltz";

        public const string settingsName = "Randomizer";

        public static string modsPath = MelonEnvironment.ModsDirectory;

        public const string resourcesFolder = "Randomizer.Resources."; // root is default namespace
        public const string mainFolder = "Randomizer";
        public const string addressableRefHack = "RANDOMIZER";
        public const string sandboxDataFilename = "SandboxData";

        public static string sandboxDataFilePath = Path.Combine(modsPath, mainFolder, sandboxDataFilename + ".json");

        public static Shader transparentShader = Shader.Find("Shader Forge/TLD_StandardTransparent");
        public static Shader standardShader = Shader.Find("Shader Forge/TLD_StandardDiffuse");



        public static bool IsScenePlayable()
        {
            return !(string.IsNullOrEmpty(GameManager.m_ActiveScene) || GameManager.m_ActiveScene.Contains("MainMenu") || GameManager.m_ActiveScene == "Boot" || GameManager.m_ActiveScene == "Empty");
        }

        public static bool IsScenePlayable(string scene)
        {
            return !(string.IsNullOrEmpty(scene) || scene.Contains("MainMenu") || scene == "Boot" || scene == "Empty");
        }

        public static bool IsMainMenu(string scene)
        {
            return !string.IsNullOrEmpty(scene) && scene.Contains("MainMenu");
        }

        public static void Log(CC color = CC.White, string message = "🌚")
        {
            if (Settings.options.debug) MelonLogger.Msg(color, message);
        }

        public static void Log(string message = "🌚")
        {
            CC color = CC.White;
            if (Settings.options.debug) MelonLogger.Msg(color, message);
        }

        public static void LogAlways(CC color = CC.White, string message = "🌚")
        {
            MelonLogger.Msg(color, message);
        }

        public static AssetBundle? LoadEmbeddedAssetBundle(string name)
        {
            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcesFolder + name))
            {
                MemoryStream? memory = new((int)stream.Length);
                stream!.CopyTo(memory);

                Il2CppSystem.IO.MemoryStream memoryStream = new(memory.ToArray());
                return AssetBundle.LoadFromStream(memoryStream);
            };
        }

        public static string? LoadEmbeddedJSON(string name)
        {
            string? result = null;

            Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcesFolder + name);
            if (stream != null)
            {
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd();
            }

            return result;
        }



        public static int GetSeedFromSandboxName(string name) // FNV1a
        {
            unchecked
            {
                int hash = (int)2166136261;

                foreach (char c in name)
                {
                    hash ^= c;
                    hash *= 16777619;
                }

                return hash;
            }
        }

        public static Vector3 GetPointOnCircle(Vector3 center, float radius = 1f) // GPT
        {
            float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            return new Vector3(center.x + x, center.y, center.z + z);
        }


        public static void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
