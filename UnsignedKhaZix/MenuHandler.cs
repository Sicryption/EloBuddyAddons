using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace KhaZix
{
    class MenuHandler
    {
        public static Menu mainMenu, Combo, Harass, AutoHarass, Killsteal, LaneClear, JungleClear, LastHit, Flee, Items, Drawing;

        public static void Initialize()
        {
            #region CreateMenus
            mainMenu = MainMenu.AddMenu("Unsigned Kha'Zix", "Main Menu");
            Combo = AddSubMenu(mainMenu, "Combo");
            Harass = AddSubMenu(mainMenu, "Harass");
            AutoHarass = AddSubMenu(mainMenu, "Auto Harass");
            Killsteal = AddSubMenu(mainMenu, "Killsteal");
            LaneClear = AddSubMenu(mainMenu, "Lane Clear");
            JungleClear = AddSubMenu(mainMenu, "Jungle Clear");
            LastHit = AddSubMenu(mainMenu, "Last Hit");
            Flee = AddSubMenu(mainMenu, "Flee");
            Items = AddSubMenu(mainMenu, "Items");
            Drawing = AddSubMenu(mainMenu, "Drawing");
            #endregion

            #region Set Menu Values
            mainMenu.Add("Creator", new Label("This script is apart of the Unsigned Series made by Chaos"));

            AddCheckboxes(ref Combo, "Use Q", "Use W", "Use E", "Use R_false", "Use Items", "Use Smite", "Use Ignite_false");
            AddSlider(Combo, "Q Mana %", 0, 0, 100);
            AddSlider(Combo, "W Mana %", 0, 0, 100);
            AddSlider(Combo, "E Mana %", 0, 0, 100);
            AddCheckboxes(ref Harass, "Use Q", "Use W", "Use E_false", "Use Items");
            AddSlider(Harass, "Q Mana %", 0, 0, 100);
            AddSlider(Harass, "W Mana %", 50, 0, 100);
            AddSlider(Harass, "E Mana %", 80, 0, 100);
            AddCheckboxes(ref AutoHarass, "Use Q", "Use W", "Use E_false", "Use Items");
            AddSlider(AutoHarass, "Q Mana %", 0, 0, 100);
            AddSlider(AutoHarass, "W Mana %", 80, 0, 100);
            AddSlider(AutoHarass, "E Mana %", 90, 0, 100);

            AddCheckboxes(ref JungleClear, "Use Q", "Use W", "Use W only for heal", "Use E", "Use R_false", "Use Items", "Use Smite", "Use Smite for HP", "Use Items");
            AddSlider(JungleClear, "Q Mana %", 0, 0, 100);
            AddSlider(JungleClear, "W Mana %", 80, 0, 100);
            AddSlider(JungleClear, "E Mana %", 90, 0, 100);

            AddCheckboxes(ref LaneClear, "Use Q", "Use Q for Last Hit", "Use W", "Use E", "Use E on X enemies", "Use R_false", "Use Items");
            AddSlider(LaneClear, "Q Mana %", 0, 0, 100);
            AddSlider(LaneClear, "W Mana %", 50, 0, 100);
            AddSlider(LaneClear, "E Mana %", 90, 0, 100);
            AddSlider(LaneClear, "Units to E on", 3, 1, 10);
            AddCheckboxes(ref LastHit, "Use Q", "Use W_false", "Use E_false", "Use R_false", "Use Items");
            AddSlider(LastHit, "Q Mana %", 0, 0, 100);
            AddSlider(LastHit, "W Mana %", 50, 0, 100);
            AddSlider(LastHit, "E Mana %", 90, 0, 100);
            AddCheckboxes(ref Killsteal, "Killsteal", "Use Q", "Use W", "Use E", "Use R_false", "Use Items", "Use Smite", "Use Ignite");
            AddCheckboxes(ref Flee, "Use W", "E to Cursor", "Use R");

            AddCheckboxes(ref Items, "Use Quick Silver Sash", "Use Mercurials Scimitar", "Use Tiamat", "Use Ravenous Hydra", "Use Titanic Hydra", "Use Youmuus", "Use Bilgewater Cutlass", "Use Hextech Gunblade", "Use Blade of the Ruined King");
            AddSlider(Items, "Minions to use Tiamat/Ravenous Hydra On", 2, 1, 10);
            AddSlider(Items, "Champions to use Tiamat/Ravenous Hydra on", 1, 1, 10);

            AddCheckboxes(ref Drawing, "Draw Q Range", "Draw W Range", "Draw E Range", "Draw R Run Range", "Draw Combo Damage");
            AddSlider(Drawing, "Autos in Combo", 2, 0, 5);
            #endregion
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
