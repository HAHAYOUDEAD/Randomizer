using ModSettings;

namespace Randomizer
{
    internal static class Settings
    {
        public static void OnLoad()
        {
            Settings.options = new RandomizerSettings();
            Settings.options.AddToModSettings("Randomizer");
        }

        public static RandomizerSettings options;
    }

    internal class RandomizerSettings : JsonModSettings
    {
        [Section("Scenes")]

        [Name("Seed")]
        [Description("")]
        [Slider(0, 99, 1, NumberFormat = "{00}")]
        public int seed = 42;

        [Name("Hide transition labels")]
        [Description("For additional confusion\n\nDefault: true")]
        public bool hideTransitionLabels = true;

        [Name("Shuffle mode")]
        [Description("- No logic: indoors can lead to caves, outdoor transitions to huts, full chaos" +
            "\n- Reasonable: keep cave/outdoor/indoor relations" +
            "\n- Outdoors only: only shuffle outdoors including caves. Somewhat preserves immersion (not really)" +
            "\n- Region lock: only shuffle inteeriors within current region, if you want to leave the region - go to region transition" +
            "\n\nDefault: Consistent")]
        [Choice(new string[]
        {
            "No logic",
            "Reasonable",
            "Outdoors only",
            "Region lock"

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
            "\n\n- Lenient: transition within 1 nearby region, rarely get mild afflictions, small time skips, mild to no weather changes" +
            "\n- Moderate: transition within 2 nearby regions, occasionally get mild to moderate afflictions, larger time skips, mild weather changes" +
            "\n- Brutal: transition within whole islasnd, often get mild to severe afflictions, even larger time skips, wild weather changes" +
            "\n- Uhhh: well" +
            "\n\nDefault: Lenient")]
        [Choice(new string[]
        {
            "Lenient",
            "Moderate",
            "Brutal",
            "Uhhh"
        })]
        public int severity = 1;


        [Name("Special Mode")]
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
            rolledPairs = Main.RollPairs(Settings.options.seed);
        }
    }
}
