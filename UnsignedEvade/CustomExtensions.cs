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
            /*if (info.ChampionName == "Rengar")
            {
                if (info.SpellName != info.GetChampionSpell().Name)
                    return false;
            }
            */
            //empowered abilities -  Rengar
            if (info.ChampionName == "Rengar" && info.GetChampionSpell() != null)
            {
                if (info.SpellName.Contains("Emp") && info.caster.Mana != 4)
                    return false;
                else if (!info.SpellName.Contains("Emp") && Game.Time - (info.TimeOfCast + info.Delay + info.TravelTime) >= 0)
                    return false;
            }
            
            if (info.GetChampionSpell() != null &&
                info.startingAmmoCount != -1)
                return true;

            return info.GetChampionSpell() != null && (!info.IsOnCooldown() || (info.BuffName != "" && info.caster.HasBuff(info.BuffName)));// && info.SpellName == info.GetChampionSpell().Name;
        }
        public static bool IsOnCooldown(this SpellInfo info)
        {
            return info.GetChampionSpell() != null && (Game.Time - info.GetChampionSpell().CooldownExpires) < 0;
        }
        public static bool ContainsSpellName(this List<SpellInfo> info, string name, bool blankMissileName = false)
        {
            foreach (SpellInfo inf in info)
                if (inf.SpellName == name && (!blankMissileName || inf.MissileName == ""))
                    return true;
            return false;
        }
        public static SpellInfo GetSpellFromSpellName(this List<SpellInfo> info, string name, bool blankMissileName = false)
        {
            foreach (SpellInfo inf in info)
                if (inf.SpellName == name && (!blankMissileName || inf.MissileName == ""))
                    return inf;
            return null;
        }
    }
}
