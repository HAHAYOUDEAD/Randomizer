using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer
{
    public enum UnfamiliarizeMode
    {
        MirrorX,
        MirrorZ,
        MirrorXZ
    }
    internal class Unfamiliarizer
    {

        private static void UnfamiliarizeCampOffice()
        {
            // get all GEAR from main scene (MC) and move under sandbox root
            // scale sandbox root, normal root and dlc root
            // scale Design root (gear)
            // scale Design_Placeable root (decorations)
            // check where dropped items end up

            // check if spawnpoint is also mirrored
            // maybe rotate exterior as well

            // mirror back objects with text on them (posters) mostly
        }

        private static void UnfamiliarizeCommon()
        {
            //
        }
    }
}
