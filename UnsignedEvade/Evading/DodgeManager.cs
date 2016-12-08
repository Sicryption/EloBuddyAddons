using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Spells;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UnsignedEvade
{
    class DodgeManager
    {
        public static void Initialize()
        {

        }

        public static void HandleDodging()
        {
            if(!Player.Instance.IsSafe())
            {
                if(!TryToMoveToSafePosition())
                {
                    //use spells to evade
                }
            }
        }

        public static bool TryToMoveToSafePosition()
        {
            //timeUntilHit in form of MS
            float timeUntilHit = Player.Instance.FindSpellInfoWithClosestTime().TimeUntilHitsChampion(Player.Instance);

            //Console.WriteLine(timeUntilHit + Player.Instance.FindSpellInfoWithClosestTime().info.SpellName);

            return false;
        }
    }
}
