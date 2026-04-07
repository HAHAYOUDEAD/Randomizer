using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.src
{
    internal class MysteryDoors
    {
        private static bool enableGlow = true;

        public static GameObject PrepareMysteryDoor()
        {
            GameObject door = GameObject.Instantiate(Main.mainBundle.LoadAsset<GameObject>("RandomizerDoor_A" + UnityEngine.Random.RandomRange(1, 4).ToString())); // 1 2 or 3
            door.layer = vp_Layer.InteractivePropNoCollidePlayer;
            var r = door.GetComponent<Renderer>();
            foreach (var m in r.materials)
            {
                if (m.name.StartsWith("dor"))
                {
                    m.shader = Main.standardShader;
                    m.SetFloat("_EnableTerrainMaterialBlending", 1f);
                    m.SetFloat("_TerrainTextureBlendOffset", -0.85f);
                }
                else if (m.name.StartsWith("que") && !enableGlow) // glow is by default in material on prefab
                {
                    //var emissive = m.GetTexture("_EmissionMap"); // doesn't work with HL transparent shader
                    //Color eColor = m.GetColor("_EmissionColor");
                    //float eStrength = (eColor.r + eColor.g + eColor.b) / 3f;
                    m.shader = Main.transparentShader;
                    //m.SetTexture("_Emissive", emissive);
                    //m.SetFloat("_EmissiveStrength", eStrength);
                }
            }
            return door;
        }
    }
}
