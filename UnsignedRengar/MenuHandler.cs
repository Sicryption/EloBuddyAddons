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

namespace UnsignedRengar
{
    class MenuHandler
    {
        public static Menu mainMenu, Combo, Harass, Killsteal, LaneClear, JungleClear, LastHit, Flee, Items, Drawing;

        public static void Initialize()
        {
            #region CreateMenus
            mainMenu = MainMenu.AddMenu("Unsigned Rengar", "Main Menu");
            Combo = AddSubMenu(mainMenu, "Combo");
            Harass = AddSubMenu(mainMenu, "Harass");
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
            AddComboBox(mainMenu, "Prediction Type:", 0, "EloBuddy", "Current Position");

            AddCheckboxes(ref Combo, "Use Q", "Use Empowered Q", "Use W", "Use Empowered W", "Use E", "Use Empowered E",
                "Use W for fourth ferocity stack", "Use W for damage_false", "Use Empowered W for damage_false", "Use Empowered W to stop CC",
                "Use Items", "Use Ignite", "Use Smite");
            AddSlider(Combo, "Use W at % black health", 15, 1, 100);
            AddSlider(Combo, "Use Empowered W at % black health", 15, 1, 100);

            AddCheckboxes(ref Harass, "Use Q", "Use Empowered Q", "Use W", "Use Empowered W", "Use E", "Use Empowered E",
                "Use W for fourth ferocity stack", "Use W for damage_false", "Use Empowered W for damage_false", "Use Empowered W to stop CC",
                "Use Items", "Use Smite");
            AddSlider(Harass, "Use W at % black health", 8, 1, 100);
            AddSlider(Harass, "Use Empowered W at % black health", 8, 1, 100);

            AddCheckboxes(ref JungleClear, "Use Q", "Use Empowered Q", "Use W", "Use Empowered W_false", "Use E", "Use Empowered E_false",
                "Use W for damage_false", "Use Empowered W for damage_false", "Use W for fourth ferocity stack",  
                "Use Items", "Use Smite", "Use Smite for HP");
            AddSlider(JungleClear, "Use W at % black health", 8, 1, 100);
            AddSlider(JungleClear, "Use Empowered W at % black health", 8, 1, 100);

            AddCheckboxes(ref LaneClear, "Use Q", "Use Empowered Q", "Use W", "Use Empowered W", "Use E", "Use Empowered E_false",
                "Use W for damage_false", "Use Empowered W for damage_false", "Use W for fourth ferocity stack",  "Save Ferocity", 
                "Use Items_false");
            AddSlider(LaneClear, "Use W at % black health", 8, 1, 100);
            AddSlider(LaneClear, "Use Empowered W at % black health", 8, 1, 100);

            AddCheckboxes(ref LastHit, "Use Q", "Use Empowered Q", "Use W_false", "Use Empowered W_false", "Use E", "Use Empowered E_false", 
                "Save Ferocity", 
                "Use Items");
            AddSlider(LastHit, "Minions to use W", 2, 0, 5);
            AddSlider(LastHit, "Minions to use Empowered W", 2, 0, 5);

            AddCheckboxes(ref Killsteal, "Killsteal", "Use Q", "Use Empowered Q", "Use W", "Use Empowered W",
                "Use E", "Use Empowered E", "Use Items", "Use Ignite", "Use Smite");

            AddCheckboxes(ref Flee, "Use Empowered W", "Use E", "Use Empowered E", "Use Empowered W to stop CC", "Jump from Brush");

            AddCheckboxes(ref Items, "Use Quick Silver Sash", "Use Mercurials Scimitar", "Use Tiamat", "Use Ravenous Hydra", "Use Titanic Hydra", "Use Youmuus", "Use Bilgewater Cutlass", "Use Hextech Gunblade", "Use Blade of the Ruined King");
            AddSlider(Items, "Minions to use Tiamat/Ravenous Hydra On", 2, 1, 10);
            AddSlider(Items, "Champions to use Tiamat/Ravenous Hydra on", 2, 1, 10);

            AddCheckboxes(ref Drawing, "Draw Q_false", "Draw Q radius", "Draw W", "Draw E", "Draw R Detection Range", "Draw Arrow to R Target_false", "Draw Killable Text", "Draw Enemy Health after Combo");
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
