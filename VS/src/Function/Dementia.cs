using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer
{
    internal class Dementia
    {
        public static Queue<float> timestamps = new Queue<float>();
        public static float gameHoursTimestamp = 0f; 

        private static readonly float gracePeriod = 90f; // seconds
        public static int threshold = 5; // num events within grace period to trigger lockout

        public static bool lockout = false;
        private static readonly float lockoutCooldown = 2f; // ingame hours

        public static void Reset()
        {
            gameHoursTimestamp = 0;
            timestamps.Clear();
            lockout = false;
        }

        public static bool ShouldLockoutDementia() 
        {

            float now = Time.time;

            if (lockout)
            {
                float nowGH = GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused();
                if (nowGH < gameHoursTimestamp) // for scene command, to reset
                {
                    gameHoursTimestamp = 0;
                    lockout = false;
                    return false;
                }

                if (nowGH - gameHoursTimestamp > lockoutCooldown)  
                {
                    lockout = false;
                    return false;
                }
                else
                {
                    return true;
                }
            }

            // write new event
            timestamps.Enqueue(now);
            gameHoursTimestamp = GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused();

            // remove old events
            while (timestamps.Count > 0 && now - timestamps.Peek() > gracePeriod) // peek = oldest event
            {
                timestamps.Dequeue();
            }

            // check threshold
            return timestamps.Count >= threshold;
        }

        public static TransitionDefinition? DementiaRoll(TransitionDefinition from)
        {
            List<TransitionDefinition> allTransitions = [];

            foreach (var scenes in transitions)
            {
                foreach (var transition in scenes.Value)
                {
                    allTransitions.Add(transition);
                }
            }
            if (Settings.options.shuffleMode == 0) // no logic, just pick something
            {
                return allTransitions[UnityEngine.Random.Range(0, allTransitions.Count)];
            }
            else if (Settings.options.shuffleMode == 1) // reasonable, pick within types
            {
                Shuffle(allTransitions);
                return allTransitions.FirstOrDefault(pick => pick.type == from.type);

            }
            else if (Settings.options.shuffleMode == 2) //outdoors only, pick within outdoors including caves
            {
                Dictionary<TransitionType, TransitionType> validMatches = new()
                {
                    { TransitionType.ToCave, TransitionType.ToOutdoorsFromCave },
                    { TransitionType.ToOutdoorsFromOutdoors, TransitionType.ToOutdoorsFromOutdoors },
                    { TransitionType.ToOutdoorsFromCave, TransitionType.ToCave }
                };

                Shuffle(allTransitions);
                if (!validMatches.ContainsKey(from.type)) return null;
                return allTransitions.FirstOrDefault(pick => pick.type == from.type);
            }

            return null; // unreachable
        }

    }
}
