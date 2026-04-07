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
global using LocalizationUtilities;
using System.Text.Json.Serialization;

namespace Randomizer
{
    internal class Utility
    {
        public const string modVersion = "0.2.0";
        public const string modName = "Randomizer";
        public const string modAuthor = "Waltz";

        public static string modsPath;

        public const string resourcesFolder = "Randomizer.Resources."; // root is project name
        public const string mainFolder = "Randomizer";
        public const string addressableRefHack = "RANDOMIZER";

        public static Texture2D survivorSettingsTexture = new(1, 1);


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
        public static JsonSerializerOptions GetDefaultJsonOptions()
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.Never,
                //UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement
            };

            return options;
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
    }
}
