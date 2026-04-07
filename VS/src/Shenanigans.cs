#if !SHENANIGANS
using HarmonyLib;
using Il2Cpp;
using Il2CppSystem.Data;
using Il2CppTLD.Scenes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler;

[HarmonyPatch]
static class Shenanigans
{
    public static string _before;
    public static string _lastValue;


    static IEnumerable<MethodBase> TargetMethods()
    {
        List<MethodBase> methods = [];

        //methods.AddRange(GetSafeMethods(typeof(LoadScene)));

        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.Activate), [typeof(bool)]));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.Activate), []));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.Deserialize)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.CompleteActivate)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.Serialize)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.PerformSceneLoad)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.LoadLevelWhenFadeOutComplete)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.GetSceneToLoad)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.InitializeInteraction)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.Awake)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.Start)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.OnTriggerEnter)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.AssignBindingOverrides)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.ExitToMainMenu)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.HolsterItemInHands)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.PerformInteraction)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.PerformHold)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.EndHold)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.AnimatedInteractionDone)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.MaybeAddGUIDToPrevSceneSave)));
        methods.Add(AccessTools.Method(typeof(LoadScene), nameof(LoadScene.MarkExplored)));


        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.LoadScene)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.LoadSceneAsynchronously)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.LoadSceneWithLoadingScreen)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.SetActiveScene)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.StripOptFromSceneName)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.LoadSlotOnStart)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.LoadSaveGameSlot), [typeof(SaveSlotInfo)]));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.LoadSaveGameSlot), [typeof(string), typeof(int)]));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.LoadGame))); 
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.Deserialize)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.Serialize)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.OnLoadGameCallback)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.LaunchSandbox)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.DoExitToMainMenu)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.ResetGameState)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.AllScenesLoaded)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.AsyncLoadMainMenu)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.IsOutDoorsScene)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.IsWellKnownScene)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.SetupTransferData)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.ApplyTransferData)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.InstantiatePlayerObject)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.InstantiatePlayerObject)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.Awake)));
        methods.Add(AccessTools.Method(typeof(GameManager), nameof(GameManager.Start)));

        methods.Add(AccessTools.Method(typeof(SaveGameSlots), nameof(SaveGameSlots.CopyData)));
        methods.Add(AccessTools.Method(typeof(SaveGameSlots), nameof(SaveGameSlots.GetSaveSlotFromName)));
        methods.Add(AccessTools.Method(typeof(SaveGameSlots), nameof(SaveGameSlots.FillSaveSlotInfo)));
        methods.Add(AccessTools.Method(typeof(SaveGameSlots), nameof(SaveGameSlots.CreateSaveSlotInfo)));
        methods.Add(AccessTools.Method(typeof(SaveGameSlots), nameof(SaveGameSlots.CreateSaveSlotInfo)));
        methods.Add(AccessTools.Method(typeof(SaveGameSlots), nameof(SaveGameSlots.PrepareSaveSlotForLoad)));
        //methods.Add(AccessTools.Method(typeof(SaveGameSlots), nameof(SaveGameSlots.TryLoadDataFromSlot)));
        //methods.Add(AccessTools.Method(typeof(SaveGameSlots), nameof(SaveGameSlots.TryLoadDataFromSlotUsingGuid)));

        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.RestoreGlobalDataPostSceneRestore)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.RestoreGlobalDataPreSceneRestore)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.LoadSceneData)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.LoadSceneDataAdditive)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.ResetForSceneLoad)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.RestoreGame)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.RestoreGlobalData)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.ConfigureRestoreFromSlot)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.SaveGlobalData)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.SaveGame)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.SaveGeneralData)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.SaveInfoData)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.SaveSettings)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.OnSaveCompleted)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.PrepareSaveData)));
        methods.Add(AccessTools.Method(typeof(SaveGameSystem), nameof(SaveGameSystem.SaveBootData)));

        methods.Add(AccessTools.Method(typeof(SceneManager), nameof(SceneManager.Deserialize)));
        methods.Add(AccessTools.Method(typeof(SceneManager), nameof(SceneManager.Deserialize)));
        methods.Add(AccessTools.Method(typeof(Utils), nameof(Utils.InferOutdoorSceneName)));


        methods.Add(AccessTools.Method(typeof(SaveGameSlotHelper), nameof(SaveGameSlotHelper.GetSaveSlotInfo), [typeof(SaveSlotType), typeof(int)]));
        methods.Add(AccessTools.Method(typeof(SaveGameSlotHelper), nameof(SaveGameSlotHelper.GetSaveSlotInfo), [typeof(SaveSlotType), typeof(string)]));
        methods.Add(AccessTools.Method(typeof(SaveGameSlotHelper), nameof(SaveGameSlotHelper.GetCurrentSaveSlotInfo)));

        methods.Add(AccessTools.Method(typeof(Il2CppTLD.Scenes.SceneSetManager), nameof(Il2CppTLD.Scenes.SceneSetManager.FindSceneSetForSceneName), [typeof(string), typeof(bool)]));
        methods.Add(AccessTools.Method(typeof(OutdoorSceneRoot), nameof(OutdoorSceneRoot.OnSceneUnload)));
        methods.Add(AccessTools.Method(typeof(OutdoorSceneRoot), nameof(OutdoorSceneRoot.GetSceneName)));
        //methods.Add(AccessTools.Method(typeof(UnityEngine.SceneManagement.SceneManager), nameof(UnityEngine.SceneManagement.SceneManager.GetActiveScene)));
        methods.Add(AccessTools.PropertyGetter(typeof(SceneSet), nameof(SceneSet.RegionGroupNameLocId)));

        methods.Add(AccessTools.Method(typeof(Panel_MainMenu), nameof(Panel_MainMenu.OnLoadSaveSlot)));
        methods.Add(AccessTools.Method(typeof(Panel_MainMenu), nameof(Panel_MainMenu.OnLoadGame)));


        return methods;
    }

    static IEnumerable<MethodBase> GetSafeMethods(Type type)
    {
        foreach (var m in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            // ❌ skip property accessors / backing fields
            if (m.Name.StartsWith("get_") || m.Name.StartsWith("set_"))
                continue;

            // ❌ skip generics (IL2CPP hates these)
            if (m.IsGenericMethod || m.ContainsGenericParameters)
                continue;

            // ❌ skip abstract / extern
            if (m.IsAbstract)
                continue;

            // ❌ skip special/internal junk
            if (m.MethodImplementationFlags != MethodImplAttributes.IL)
                continue;

            yield return m;
        }
    }

    static void Prefix()
    {
        _before = GetValueSafe();
    }

    static void Postfix(MethodBase __originalMethod)
    {
        Log("--raw " + $"[METHOD CALL] {__originalMethod.DeclaringType.Name}.{__originalMethod.Name}");
        var after = GetValueSafe();

        if (_before != after)
        {
            Log(CC.Green, "--raw " + $"[FIELD CHANGE] {__originalMethod.DeclaringType.Name}.{__originalMethod.Name}");
            Log("--raw " + $"    {_before ?? "null"} → {after ?? "null"}");

            // optional
            // Log(Environment.StackTrace);
        }
    }

    // -------- SAFE ACCESS --------
    public static string GetValueSafe()
    {
        try
        {
            var data = GameManager.m_SceneTransitionData;
            if (data == null) return null;

            return data.m_ForceNextSceneLoadTriggerScene;
        }
        catch
        {
            return null;
        }
    }
}
#endif