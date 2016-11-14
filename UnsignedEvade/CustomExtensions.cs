using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;

namespace UnsignedEvade
{
    static class CustomExtensions
    {
        public static bool IsOffCooldown(this SpellInfo info)
        {
            return info.GetChampionSpell() != null && !info.IsOnCooldown() && info.SpellName == info.GetChampionSpell().Name;
        }

        public static bool IsOnCooldown(this SpellInfo info)
        {
            return (Game.Time - info.GetChampionSpell().CooldownExpires) < 0;
        }
    }
}
