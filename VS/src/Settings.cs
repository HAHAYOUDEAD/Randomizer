using Il2CppEasyRoads3Dv3;
using ModSettings;
using static Il2Cpp.PlayerVoice;
using System.Runtime.CompilerServices;
using UnityEngine;
using Il2CppRewired;
using System;

namespace Randomizer
{
    internal static class Settings
    {
        public static RandomizerSettings options;
        public static UILabel transitionLabel;
        public static Transform? root;
        public static GameObject warning;

        public static void OnLoad()
        {

            Settings.options = new RandomizerSettings(Path.Combine(mainFolder, "Settings")); // put settings under folder
            Settings.options.AddToModSettings(settingsName);
        }

        public static void OnInitialize()
        {
            root = InterfaceManager.GetPanel<Panel_OptionsMenu>().transform.Find("Pages/ModSettings/GameObject/ScrollPanel/Offset/Mod settings grid (Randomizer)");
            //New Game Object/Custom Header (Transitions)
            if (root != null)
            {
                transitionLabel = root.GetComponentsInChildren<UILabel>(true).Where(label => label.gameObject.name == "Custom Header (RNZ_Settings_Transition)").FirstOrDefault();
                SetupWarning();
            }
        }

        public static void SwitchTransitionsLabel(int isOnForSaveslot)
        {
            if (transitionLabel != null)
            {
                switch (isOnForSaveslot)
                {
                    default:
                        //transitionLabel.text = Localization.Get("RNZ_Settings_Transition");
                        transitionLabel.GetComponent<UILocalize>().key = "RNZ_Settings_Transition";
                        transitionLabel.color = new(0.94f, 0.94f, 0.94f);
                        break;
                    case 0:
                        transitionLabel.text = Localization.Get("RNZ_Settings_Transition_Off");
                        transitionLabel.GetComponent<UILocalize>().key = "RNZ_Settings_Transition_Off";
                        transitionLabel.color = new(0.95f, 0.667f, 0.7f);
                        break;
                    case 1:
                        transitionLabel.text = Localization.Get("RNZ_Settings_Transition_On");
                        transitionLabel.GetComponent<UILocalize>().key = "RNZ_Settings_Transition_On";
                        transitionLabel.color = new(0.667f, 0.95f, 0.8f);
                        break;
                }
                
            }
        }

        public static void SetupWarning()
        {
            if (warning != null) return;
            warning = GameObject.Instantiate(transitionLabel.gameObject, root.GetParent().GetParent().GetParent());
            warning.name = "RandomizerWarning";
            GameObject warningContent = GameObject.Instantiate(transitionLabel.gameObject, warning.transform);
            warningContent.name = "Text";

            warning.transform.position = Vector3.zero;
            warning.transform.localPosition = new(656f, 101f, 0f);

            warningContent.transform.localPosition = new(0f, -25f, 0f);


            UILabel labelTitle = warning.GetComponent<UILabel>();
            UILabel labelText = warningContent.GetComponent<UILabel>();


            labelText.alignment = NGUIText.Alignment.Left;
            labelText.color = new(0.94f, 0.94f, 0.94f);
            labelText.capsLock = false;
            labelText.fontSize = 14;
            labelText.pivot = UIWidget.Pivot.TopLeft;
            labelText.overflowMethod = UILabel.Overflow.ResizeHeight;
            labelText.width = 400;
            

            labelTitle.color = new(1f, 0.75f, 0.2f);


            warning.GetComponent<UILocalize>().key = "RNZ_Settings_Warning";
            warningContent.GetComponent<UILocalize>().key = "RNZ_Settings_WarningText";

        }

        public static void ToggleWarning(bool show) => warning.SetActive(show);

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

    // + add console command to force toggle randomizer per saveslot

    // add mod regions



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

    // ? Maybe there could be just two or three doors with upside down ?  and for those the time of day switches to the opposite (so night to day and day to night) and the weather changes  too.  😆

    // ? Would it work to spawn a player like half way up a climbing rope?

    // + when dementia is turned on, the mod is sometimes actually enabled and other times it isn’t.  So it works for a while and then for a while it doesn’t. Sort of plays into the dementia feel.


    internal class RandomizerSettings : JsonModSettings
    {
        public RandomizerSettings(string path) : base(path) { }

        [Section("RNZ_Settings_Transition", Localize = true)]

        [Name("Hide transition labels")]
        [Description("For additional confusion\n\nDefault: True")]
        public bool hideTransitionLabels = true;

        [Name("Seed mode")]
        [Description("  - Random: uses random fixed seed per save" +
            "\n  - Controlled: uses seed derived from saveslot name, can be changed along with the saveslot name" +
            "\n  - Dementia: links between locations are severed, you will not return to your cozy house until you accidentally stumble upon it again" +
            "\n\nDefault: Random")]
        [Choice(new string[]
        {
            "Random",
            "Controlled",
            "Dementia",
            "[DEBUG] 42"
        })]
#if DEBUG
        public int seedMode = 3;
#else
        public int seedMode = 0;
#endif

        [Name("Shuffle mode")]
        [Description("  - No logic: indoors can lead to caves, outdoor transitions to huts, full chaos" +
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
        [Description("Toggle debug logging in console. Verbose, might affect performance")]
#if DEBUG
        public bool debug = true;
#else
        public bool debug = false;
#endif
        /*
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
