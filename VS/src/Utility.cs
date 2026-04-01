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
using Il2CppTLD.Serialization;
using System.Text.Json.Serialization;

namespace Randomizer
{
    internal class Utility
    {
        public const string modVersion = "0.2.0";
        public const string modName = "Randomizer";
        public const string modAuthor = "Waltz";

        public const string resourcesFolder = "Randomizer.Resources."; // root is project name

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
            MelonLogger.Msg(color, message);
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
    }
}
