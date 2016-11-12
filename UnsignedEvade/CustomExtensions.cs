using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsignedEvade
{
    static class CustomExtensions
    {
        public static bool IsOffCooldown(this SpellInfo info)
        {
            return !info.GetChampionSpell().IsOnCooldown && info.SpellName == info.GetChampionSpell().Name;
        }
    }
}
