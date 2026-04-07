using Il2CppEasyRoads3Dv3;
using ModSettings;
using static Il2Cpp.PlayerVoice;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Randomizer
{
    internal static class Settings
    {
        public static RandomizerSettings options;

        public static void OnLoad()
        {
            Settings.options = new RandomizerSettings(Path.Combine(mainFolder, "Settings")); // put settings under folder
            Settings.options.AddToModSettings("Randomizer");
        }
    }

    // ideas

    // + Mystery doors
    // standalone doors rarely spawned in predfefined locations(should be a lot) that one-way lead to a random pre defined location 
    // Pathfinders Ascent extension: if player has the mod, expand the list of possible locations including plcaces from the mod
    // only for outdoors

    // ? custom exit points
    // for uninteractable doors and other weird places. Not sure how to connect it to the pair system though, would have to add entrances as well
    // ask moose to find good locations

    // + on brutal dementia mode have a chance to spawn player in a middle of random scene half dead after just being mauled by bear, looking how it walks away

    // ? simulate 30days passing, which would spoil the food/ ruin clothes accoirdingly
    // would have to manually simulate it anyways, if I don't want player to just die 😄 so remove food/water/material, degrade tools, and so on



    // from moose:
    // +  Oh, another fun idea.  Every once in a while, have maybe 5 transitions in a row lead to the exact same place.  So for a while the player will think they can't leave whatever scene they are in.

    // + Build in a few "hard-coded" transitions that are especially brutal or fun.  Example: when you go to leave DP you always end up in ZOC.

    // ? This might be a good mod to make use of the second Riken in BI.  When you enter it you enter the Riken in DP, so when you exit you are in DP, instead of BI where you entered it.

    // + Weighted randomness.Favor nearby regions more often (so it feels semi-coherent). Maybe rare chance to get zoomed across the island.

    // + Maybe caves always(and only) lead to other cave transitions. Not to regular caves

    // ? Region-based targeting.If you have discovered a region, higher chance to land there.

    // x Region Pool - let players choose which regions the mod will be in effect for.

    // ? I just remembered the front door to the Blackrock prison has a second disabled transition for story mode.
    // It is set to load a scene that doesn’t exist in survival so nothing happens if you enable it and then click on the transition.
    // Could maybe do something special with it.  Not sure what exactly, but it’s a little hidden thing you could have fun with.

    // ? Just thought of another fun one.  Hardcode a transition that sends the player to inside one of the cells in the Blackrock prison.
    // but spawn a mystery door after timer atleast? or hide a key to the door

    internal class RandomizerSettings : JsonModSettings
    {
        public RandomizerSettings(string path) : base(path) { }

        [Section("Transitions")]

        [Name("Seed")]
        [Description("- Random: uses random seed per save" +
            "\n  - Controlled: uses seed derived from saveslot name, can be changed along with the saveslot name" +
            "\n  - Debug: uses fixed seed 42 for all savegames, for testing purposes only" +
            "\n\n!! WARNING: changing this will affect all savesfiles that have randomizer enabled. The seed however is always fixed, so this option is revertible" +
            "\n\nDefault: Random")]
        [Choice(new string[]
        {
            "Random",
            "Controlled",
            "Debug"
        })]
#if DEBUG
        public int seedMode = 2;
#else
        public int seedMode = 0;
#endif
        [Name("Hide transition labels")]
        [Description("For additional confusion\n\nDefault: True")]
        public bool hideTransitionLabels = true;

        [Name("Shuffle mode")]
        [Description("- No logic: indoors can lead to caves, outdoor transitions to huts, full chaos" +
            "\n  - Reasonable: keep cave/outdoor/indoor relations" +
            "\n  - Outdoors only: only shuffle outdoors including caves. Somewhat preserves immersion (not really)" +
            //"\n  - Region lock: only shuffle interiors within current region, if you want to leave the region - go to region transition" +
            "\n\nDefault: Reasonable")]
        [Choice(new string[]
        {
            "No logic",
            "Reasonable",
            "Outdoors only",
            //"Region lock"

        })]
        public int shuffleMode = 1;

        [Name("Enable debug")]
        [Description("")]
        public bool debug = true;
        /*
        [Name("Dementia mode")]
        [Description("Links between locations are severed, you will not return to your cozy house until you accidentally stumble on it again\n\nGoes well with randomized weather/time/afflictions\n\nDefault: false")]
        public bool rerollAfterTransition = false;

        // how often to reroll after transition

        [Name("Randomize weather")]
        [Description("Set random weather on transition\n\nDefault: false")]
        public bool randomizeWeather = false;

        [Name("Randomize time of day")]
        [Description("Set random time of day on transition\n\nDefault: false")]
        public bool randomizeTime = false;

        [Name("Random affliction")]
        [Description("Get random affliction on transition\n\nDefault: false")]
        public bool randoimizeAffliction = false;

        [Name("RNG cruelty")]
        [Description("How severe you want your challenge. Affects how far you transition to from current location, and how severe the weather/time/afflictions are, if enabled" +
            "\n\n  - Lenient: transition within 1 nearby region, rarely get mild afflictions, small time skips, mild to no weather changes" +
            "\n  - Moderate: transition within 2 nearby regions, occasionally get mild to moderate afflictions, larger time skips, mild weather changes" +
            "\n  - Brutal: transition within whole islasnd, often get mild to severe afflictions, even larger time skips, wild weather changes" +
            "\n  - Uhhh: well" +
            "\n\nDefault: Lenient")]
        [Choice(new string[]
        {
            "Lenient",
            "Moderate",
            "Brutal",
            "Uhhh"
        })]
        public int severity = 1;


        [Name("Orchestrated Random")]
        [Description("- No: create you own adventure" +
            "\n- Cabin Fever: spawn indoors and never leave. Forget about clothes and hunting, just chill and watch TV" +
            "\n- Viola's Dream: cave mode, ho idea how you would survive here but here you go" +
            "\n- Locked out: you're locked to the region you start in" +
            "\n\nDefault: No")]
        [Choice(new string[]
        {
            "No",
            "Cabin Fever",
            "Viola's Dream",
            "locked out"
        })]
        public int preset = 1;
        */

        protected override void OnConfirm()
        {
            base.OnConfirm();
        }
    }
}
