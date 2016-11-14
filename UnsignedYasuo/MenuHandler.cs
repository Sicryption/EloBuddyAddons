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

namespace UnsignedYasuo
{
    class MenuHandler
    {
        public static Menu mainMenu, Combo, Harass, AutoHarass, Killsteal, LaneClear, JungleClear, LastHit, Flee, Ult, Items, Drawing;

        public static void Initialize()
        {
            #region CreateMenus
            mainMenu = MainMenu.AddMenu("Unsigned Yasuo", "UnsignedYasuo");
            Combo = AddSubMenu(mainMenu, "Combo");
            Harass = AddSubMenu(mainMenu, "Harass");
            AutoHarass = AddSubMenu(mainMenu, "Auto Harass");
            LaneClear = AddSubMenu(mainMenu, "Lane Clear");
            JungleClear = AddSubMenu(mainMenu, "Jungle Clear");
            LastHit = AddSubMenu(mainMenu, "Last Hit");
            Killsteal = AddSubMenu(mainMenu, "Kill Steal");
            Flee = AddSubMenu(mainMenu, "Flee");
            Ult = AddSubMenu(mainMenu, "Ult");
            Drawing = AddSubMenu(mainMenu, "Drawing");
            Items = AddSubMenu(mainMenu, "Items");
            #endregion

            #region Set Menu Values
            AddCheckboxes(ref Combo, "Use Q", "Use Q3", "Use E_false", "Use EQ_false", "Use E Under Tower_false", "Use R", "Use Items", "Beyblade");
            AddComboBox(Combo, "Dash Mode: ", 0, "Gapclose", "To Mouse", "Disable");
            AddCheckboxes(ref Harass, "Use Q", "Use Q3", "Use E_false", "Use EQ_false", "Use E Under Tower_false", "Use R_false", "Use Items");
            AddCheckboxes(ref AutoHarass, "Use Q", "Use Q3", "Use E_false", "Use EQ_false", "Use E Under Tower_false", "Use Items");
            AddCheckboxes(ref LaneClear, "Use Q", "Use Q3", "Use E_false", "Use E only for Last Hit", "Use EQ_false",  "Use E Under Tower_false", "Use Items");
            AddCheckboxes(ref JungleClear, "Use Q", "Use Q3", "Use E", "Use E only for Last Hit_false", "Use EQ_false", "Use Items");
            AddCheckboxes(ref LastHit, "Use Q", "Use Q3", "Use E", "Use EQ_false", "Use E Under Tower_false", "Use Items");
            AddCheckboxes(ref Killsteal, "Activate Killsteal", "Use Q", "Use Q3", "Use E", "Use EQ", "Use E Under Tower", "Use Items", "Use Ignite");
            AddCheckboxes(ref Flee, "Use E", "Use E Under Tower", "Stack Q", "Wall Dash");
            AddSlider(Flee, "Wall Dash Extra Space", 20, 10, 200);
            AddCheckboxes(ref Ult, "Use R at Last Second", "Use R on All Enemies in Range", "Use R for Flow_false", "Use R at 10% HP_false");
            AddSlider(Ult, "Use R on x Enemies or more:", 3, 1, 5);
            AddCheckboxes(ref Drawing, "Draw Q", "Draw W_false", "Draw E", "Draw E End Position on Target_false", "Draw E End Position on Target - Detailed_false", "Draw EQ_false", "Draw EQ on Target_false", "Draw R", "Draw Beyblade", "Draw Wall Dashes", "Draw Turret Range");
            AddCheckboxes(ref Items, "Use Quick Silver Sash", "Use Mercurials Scimitar", "Use Tiamat", "Use Ravenous Hydra", "Use Titanic Hydra", "Use Youmuus", "Use Bilgewater Cutlass", "Use Hextech Gunblade", "Use Blade of the Ruined King");
            WindWall.Initialize();
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
