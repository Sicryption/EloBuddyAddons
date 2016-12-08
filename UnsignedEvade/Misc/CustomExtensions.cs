using System;
using System.Collections.Generic;
using System.Reflection;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Spells;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using System.Linq;
using System.IO;
using EloBuddy.Sandbox;

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

            if (info.ChampionName == "LeeSin" && info.GetChampionSpell() != null)
            {
                if (info.SpellName.Contains("One") && info.GetChampionSpell().Name.Contains("Two"))
                    return false;
                else
                    return true;
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
        public static SpellInfo GetSpellFromMissileName(this List<SpellInfo> info, string name, bool blankSpellName = false)
        {
            foreach (SpellInfo inf in info)
                if (inf.MissileName == name && (!blankSpellName || inf.SpellName == ""))
                    return inf;
            return null;
        }
        public static bool IsInRangeFromSingedPoison(this Vector3 pos, float range)
        {
            foreach (ParticleInfo info in ParticleDatabase.SingedPoisonTrails)
                if (pos.IsInRange(info.Position, range))
                    return true;
            return false;
        }
        public static bool GetCheckboxValue(this Menu self, string text)
        {
            return MenuHandler.GetCheckboxValue(self, text);
        }
        public static CheckBox GetCheckbox(this Menu self, string text)
        {
            return MenuHandler.GetCheckbox(self, text);
        }
        public static int GetSliderValue(this Menu self, string text)
        {
            return MenuHandler.GetSliderValue(self, text);
        }
        public static string GetComboBoxText(this Menu self, string text)
        {
            return MenuHandler.GetComboBoxText(self, text);
        }
        public static ComboBox GetComboBox(this Menu self, string text)
        {
            return MenuHandler.GetComboBox(self, text);
        }
        public static List<string> GetChampionNames(this List<AIHeroClient> list)
        {
            List<string> names = new List<string>();
            foreach (AIHeroClient cl in list)
                if (!names.Contains(cl.ChampionName))
                    names.Add(cl.ChampionName);
            return names;
        }
        public static List<string> GetNames(this List<AIHeroClient> list)
        {
            List<string> names = new List<string>();
            foreach (AIHeroClient cl in list)
                if (!names.Contains(cl.Name))
                    names.Add(cl.Name);
            return names;
        }
        public static bool ShouldBeAccountedFor(this SpellInfo info)
        {
            Menu menu = MenuHandler.championMenus.Where(a => a.UniqueMenuId.Contains(info.ChampionName)).FirstOrDefault();
            
            //spells like recall/tp/items
            if (menu == null)
                return true;

            string name = info.SpellName;
            if (name == "")
                name = info.MissileName;
            return menu.GetCheckboxValue("Dodge " + name);
        }
    }
}
