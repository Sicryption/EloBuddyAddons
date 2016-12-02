using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UnsignedEvade
{
    class MenuHandler
    {
        public static Menu mainEvadeMenu, mainChampionEvadeMenu, DebugMenu, DrawMenu;
        public static List<Menu> championMenus = new List<Menu>();
        
        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            mainEvadeMenu = MainMenu.AddMenu("Unsigned Evade", "Unsigned Evade");
            mainChampionEvadeMenu = MainMenu.AddMenu("UE - Dodging", "UE - Champions");
            
            DrawMenu = AddSubMenu(mainEvadeMenu, "Draw Menu");
            AddCheckboxes(ref DrawMenu, "Draw Friendly Spells/Missiles_false", "Draw Active Spells/Missiles", "Draw Passive Spell Text");
            DrawMenu.AddSeparator(4);

            DebugMenu = AddSubMenu(mainEvadeMenu, "Debug Menu");
            AddCheckboxes(ref DebugMenu, "Debug Spell/Missile Creation", "Debug Spell/Missile Deletion",
                "Show only Enemy Spells/Missiles", "Show Particles_false", "Show All Object Names_false", 
                "Show Buff Gains_false", "Show Buff Losses_false", "Draw Player Direction_false");

            foreach(string s in EntityManager.Heroes.AllHeroes.GetChampionNames())
            {
                Menu champMenu = AddSubMenu(mainChampionEvadeMenu, s);

                foreach(SpellInfo info in SpellDatabase.SpellList.Where(a=>a.ChampionName == s))
                {
                    string name = info.Slot.ToString();
                    if (name == "None")
                        name = info.SpellName;
                    if (name == "")
                        name = info.MissileName;

                    CheckBox cb = AddCheckbox(ref champMenu, "Dodge " + name);
                }

                championMenus.Add(champMenu);
            }
        }
        
        public static void AddCheckboxes(ref Menu menu, params string[] checkBoxValues)
        {
            foreach (string s in checkBoxValues)
            {
                if (s.Length > "_false".Length && s.Contains("_false"))
                    AddCheckbox(ref menu, s.Remove(s.IndexOf("_false"), 6), false);
                else
                    AddCheckbox(ref menu, s, true);
            }
        }
        public static Menu AddSubMenu(Menu startingMenu, string text)
        {
            Menu menu = startingMenu.AddSubMenu(text, text + ".");
            menu.AddGroupLabel(text + " Settings");
            return menu;
        }
        public static CheckBox AddCheckbox(ref Menu menu, string text, bool defaultValue = true)
        {
            return menu.Add(menu.UniqueMenuId + text, new CheckBox(text, defaultValue));
        }
        public static CheckBox GetCheckbox(Menu menu, string text)
        {
            return menu.Get<CheckBox>(menu.UniqueMenuId + text);
        }
        public static bool GetCheckboxValue(Menu menu, string text)
        {
            CheckBox checkbox = GetCheckbox(menu, text);

            if (checkbox == null)
                Console.WriteLine("Checkbox (" + text + ") not found under menu (" + menu.DisplayName + "). Unique ID (" + menu.UniqueMenuId + text + ")");

            return checkbox.CurrentValue;
        }
        public static ComboBox AddComboBox(Menu menu, string text, int defaultValue = 0, params string[] values)
        {
            return menu.Add(menu.UniqueMenuId + text, new ComboBox(text, defaultValue, values));
        }
        public static ComboBox GetComboBox(Menu menu, string text)
        {
            return menu.Get<ComboBox>(menu.UniqueMenuId + text);
        }
        public static string GetComboBoxText(Menu menu, string text)
        {
            return menu.Get<ComboBox>(menu.UniqueMenuId + text).SelectedText;
        }
        public static Slider GetSlider(Menu menu, string text)
        {
            return menu.Get<Slider>(menu.UniqueMenuId + text);
        }
        public static int GetSliderValue(Menu menu, string text)
        {
            return menu.Get<Slider>(menu.UniqueMenuId + text).CurrentValue;
        }
        public static Slider AddSlider(Menu menu, string text, int defaultValue, int minimumValue, int maximumValue)
        {
            return menu.Add(menu.UniqueMenuId + text, new Slider(text, defaultValue, minimumValue, maximumValue));
        }
    }
}
