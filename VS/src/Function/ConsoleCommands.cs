using Il2CppTLD.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Randomizer
{
    internal class ConsoleCommands
    {

        public static int lastDestIndex = -1;

        [HarmonyPatch(typeof(ConsoleManager), nameof(ConsoleManager.Initialize))]
        private static class AddCommands
        {
            internal static void Postfix()
            {
                if (!uConsole.CommandAlreadyRegistered("randomizer_gotoquedoor"))
                {
                    uConsole.RegisterCommand("randomizer_findquedoor", new Action(CONSOLE_GotoNextQueDoor));
                    uConsole.RegisterCommand("randomizer_nextquedoordestination", new Action(CONSOLE_GotoNextQueDoorDestination));

                }
            }
        }

        private static void CONSOLE_GotoNextQueDoor()
        {
            if (IsScenePlayable(GameManager.m_ActiveScene))
            {
                if (QueDoors.spawnedDoors.Count == 0)
                {
                    uConsoleLog.Add("No que doors found for this scene");
                    return;
                }
                GameObject? door = QueDoors.spawnedDoors.ElementAt(UnityEngine.Random.Range(0, QueDoors.spawnedDoors.Count));
                if (door == null)
                {
                    uConsoleLog.Add("Que door was destroyed");
                    return;
                }
                Vector3 origin = door.transform.position;
                Vector3 pos = GetPointOnCircle(origin, 1f);
                Vector3 direction = (origin - pos).normalized;
                Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);

                GameManager.GetPlayerManagerComponent().TeleportPlayer(pos, rot);

                return;
            }
            

            uConsoleLog.Add("You have to be in a playable scene to use this command");
            
        }
        private static void CONSOLE_GotoNextQueDoorDestination()
        {
            string scene = GameManager.m_ActiveScene;
            if (IsScenePlayable(scene))
            {
                QueDoorDestination? pick = null;
                if (queDoorDestinationPool.TryGetValue(scene, out var pool))
                {
                    if (lastDestIndex == -1)
                    {
                        pick = pool[0];
                        lastDestIndex = 0;
                    }
                    else
                    {
                        lastDestIndex = (lastDestIndex + 1) % pool.Length; // increase until reaches length then 0
                        pick = pool[lastDestIndex];
                    }
                }
                else
                {
                    uConsoleLog.Add("No que door destinations found for this scene");
                    return;
                }

                if (pick != null)
                {
                    GameManager.GetPlayerManagerComponent().TeleportPlayer(pick.position, Quaternion.identity);
                    GameManager.GetVpFPSCamera().m_Pitch = pick.pitch;
                    GameManager.GetVpFPSCamera().m_Yaw = pick.yaw;
                }
                else
                {
                    uConsoleLog.Add("Failed to get next destination (shouldn't happen)");
                    return;
                }

                

                return;
            }
            

            uConsoleLog.Add("You have to be in a playable scene to use this command");
            
        }
    }
}
