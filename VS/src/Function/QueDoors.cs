using Il2CppRewired;
using Il2CppTLD.AddressableAssets;
using Il2CppTLD.Interactions;
using Il2CppTLD.Placement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Randomizer
{
    internal class QueDoors
    {
        private static bool enableGlow = true;
        public static HashSet<GameObject> spawnedDoors = [];
        public static HashSet<GameObject> spawnedDecorations = [];

        public static void CreateHelperObjects(string scene) // additional objects for ? door access
        {
            switch (scene)
            {
                case "BlackrockPrisonSurvivalZone":
                    SpawnDecoration("Assets/ArtAssets/Env/Objects/OBJ_Pallets/OBJ_PalletA_Prefab.prefab", new(-220.9f, 226.8f, 140.63f), new(330f, 353.56f, 0f));
                    break;
            }
        }

        public static void SpawnDecoration(string name, Vector3 pos, Vector3 rot)
        {
            string sanitizedName = name.Split('/').Last().Replace(".prefab", "");
            GameObject go = AssetHelper.SafeInstantiateAssetAsync(name, PlaceableManager.FindOrCreateCategoryRoot().transform).WaitForCompletion();
            go.name = sanitizedName;
            go.layer = vp_Layer.TerrainObject;
            go.transform.localPosition = pos;
            go.transform.localEulerAngles = rot;
            spawnedDecorations.Add(go);
        }

        public static bool SpawnQueDoor()
        {
            string scene = GameManager.m_ActiveScene;
            if (queDoorPositionPool.TryGetValue(scene, out var doorPool))
            {
                if (queDoorDestinationPool.TryGetValue(scene, out var destPool))
                {
                    var pick = doorPool[Random.Range(0, doorPool.Length)];
                    var destination = PickPoint(destPool, pick.position);
                    var newDoor = PrepareQueDoor(pick.doorless, destination);
                    newDoor.transform.position = pick.position;
                    newDoor.transform.rotation = pick.rotation;
                    newDoor.transform.localScale = pick.scale ?? Vector3.one;
                    spawnedDoors.Add(newDoor);
                    return true;
                }
                else 
                {
                    Log(CC.Red, $"No que door destinations for {scene}");
                }

            }
            return false;
        }

        public static void RandomAdvanceTime()
        {
            float numHoursToSkip = Mathf.Pow(Random.value, 3f) * 24f;
            numHoursToSkip = Mathf.Round(numHoursToSkip * 100f) / 100f;
            MelonLogger.Msg("skip " + numHoursToSkip);
            float played = GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused();
            float currentTime = GameManager.GetTimeOfDayComponent().GetNormalizedTime();
            float threeHours = 0.125f;
            float oneHour = threeHours / 3f;
            MelonLogger.Msg("before " + played + " | " + currentTime);
            GameManager.GetTimeOfDayComponent().SetNormalizedTime(Mathf.Repeat(currentTime + oneHour * numHoursToSkip, 1f));
            GameManager.GetTimeOfDayComponent().SetHoursPlayedNotPaused(played + numHoursToSkip);
            MelonLogger.Msg("after " + GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused() + " | " + GameManager.GetTimeOfDayComponent().GetNormalizedTime());
        }
        
        public static QueDoorDestination PickPoint(QueDoorDestination[] points, Vector3 origin) // GPT
        {
            if (points == null || points.Length == 0)
                return new();

            // Step 1: find min/max distance
            float minDist = float.MaxValue;
            float maxDist = 0f;

            float[] distances = new float[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                float d = Vector3.Distance(origin, points[i].position);
                distances[i] = d;

                if (d < minDist) minDist = d;
                if (d > maxDist) maxDist = d;
            }

            float range = maxDist - minDist;
            if (range <= 0f)
                return points[Random.Range(0, points.Length)];

            // Step 2: build weights
            float totalWeight = 0f;
            float[] weights = new float[points.Length];

            for (int i = 0; i < points.Length; i++)
            {
                // Normalize (0 = closest, 1 = furthest)
                float t = (distances[i] - minDist) / range;

                // Step 3: shape the curve
                // Squaring makes close points near 0 weight, far ones stronger
                float weight = Mathf.Pow(t, 2f);

                // Cap max influence
                weight *= 0.7f;

                weights[i] = weight;
                totalWeight += weight;
            }

            // Step 4: weighted pick
            float random = Random.Range(0f, totalWeight);

            for (int i = 0; i < points.Length; i++)
            {
                random -= weights[i];
                if (random <= 0f)
                    return points[i];
            }

            return points[points.Length - 1];
        }

        public static GameObject PrepareQueDoor(bool onlyQue, QueDoorDestination d)
        {
            GameObject door = GameObject.Instantiate(Main.mainBundle.LoadAsset<GameObject>("RandomizerDoor_A" + UnityEngine.Random.RandomRange(1, 4).ToString())); // 1 2 or 3
            door.layer = vp_Layer.InteractiveProp;
            var r = door.GetComponent<Renderer>();
            var rQ = door.transform.GetChild(0).GetComponent<Renderer>();

            if (onlyQue)
            {
                r.forceRenderingOff = true;
                rQ.castShadows = false;
            }
            else
            {
                r.material.shader = standardShader;
                r.material.SetFloat("_EnableTerrainMaterialBlending", 1f);
                r.material.SetFloat("_TerrainTextureBlendOffset", -0.85f);
            }

            if (!enableGlow) // glow is by default in material on prefab
            {
                //var emissive = m.GetTexture("_EmissionMap"); // doesn't work with HL transparent shader
                //Color eColor = m.GetColor("_EmissionColor");
                //float eStrength = (eColor.r + eColor.g + eColor.b) / 3f;
                //m.SetTexture("_Emissive", emissive);
                //m.SetFloat("_EmissiveStrength", eStrength);
                r.material.shader = transparentShader;
            }

            var si = door.AddComponent<SimpleInteraction>();
            Action action = () => TeleportToDestination(d);
            si.AddEventCallback(InteractionEventType.PerformInteraction, (UnityAction<BaseInteraction>)(tp => CameraFade.FadeOut(1f, 0f, action)));
            si.m_DefaultHoverText.m_LocalizationID = "RNZ_QueDoor_Hover";
            si.InitializeInteraction();

            return door;
        }

        public static void TeleportToDestination(QueDoorDestination d)
        {
            if (d != null)
            {
                GameManager.GetPlayerManagerComponent().TeleportPlayer(d.position, Quaternion.identity);
                GameManager.GetVpFPSCamera().m_Pitch = d.pitch;
                GameManager.GetVpFPSCamera().m_Yaw = d.yaw;
                RandomAdvanceTime();
            }
            CameraFade.FadeIn(1f, 0f);

            foreach (var q in spawnedDoors)
            { 
                if (q != null) UnityEngine.Object.Destroy(q.gameObject);
            }
            spawnedDoors.Clear();

            SpawnQueDoor();
        }
    }
}
