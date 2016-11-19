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

namespace UnsignedGangplank
{
    class MenuHandler
    {
        public static Menu mainMenu, Combo, Harass, AutoHarass, Killsteal, LaneClear, JungleClear, LastHit, Flee, Items, Drawing, Settings;

        public static void Initialize()
        {
            #region CreateMenus
            mainMenu = MainMenu.AddMenu("Unsigned Gangplank", "UnsignedGangplank");
            Combo = AddSubMenu(mainMenu, "Combo");
            Harass = AddSubMenu(mainMenu, "Harass");
            AutoHarass = AddSubMenu(mainMenu, "Auto Harass");
            LaneClear = AddSubMenu(mainMenu, "Lane Clear");
            JungleClear = AddSubMenu(mainMenu, "Jungle Clear");
            LastHit = AddSubMenu(mainMenu, "Last Hit");
            Killsteal = AddSubMenu(mainMenu, "Killsteal");
            Flee = AddSubMenu(mainMenu, "Flee");
            Drawing = AddSubMenu(mainMenu, "Drawing");
            Items = AddSubMenu(mainMenu, "Items and Auto W");
            Settings = AddSubMenu(mainMenu, "Settings");
            #endregion

            #region Set Menu Values
            mainMenu.Add("Creator", new Label("This script is apart of the Unsigned Series made by Chaos"));
            mainMenu.AddLabel("In the case of FPS problems: Disabled Barrel Chaining in Killsteal and Auto-Harass.");
            AddComboBox(mainMenu, "Prediction Type:", 1, "EloBuddy", "Current Position");

            AddCheckboxes(ref Combo, "Use Q", "Use W", "Use E", "Use R", "Auto-Attack Barrels if Q on cooldown", "Use Q on enemies", "Use Q on enemies only if no barrels around", "Use Q to kill barrels", "Use W to remove CC", "Use W to remove slows_false", "Create First Barrel", "Chain Barrels", "Use Items", "Use Ignite_false");
            //AddComboBox(Combo, "First Barrel Usage:", 0, "None", "EloBuddy Prediction", "On Closest Enemy", "On Lowest HP Enemy", "On Lowest % HP Enemy", "Between Enemy and Me");
            AddSlider(Combo, "Enemies to use R", 3, 1, 5);
            AddSlider(Combo, "Enemies to use Barrel", 1, 1, 5);
            AddSlider(Combo, "Enemies to use Q on Barrel", 1, 1, 5);
            AddSlider(Combo, "Enemies to Auto-Attack Barrel", 1, 1, 5);

            AddCheckboxes(ref Harass, "Use Q", "Use E", "Auto-Attack Barrels if Q on cooldown", "Use Q on enemies", "Use Q on enemies only if no barrels around", "Use Q to kill barrels", "Create First Barrel", "Chain Barrels", "Use R_false", "Use Items_false");
            AddSlider(Harass, "Enemies to use R", 5, 1, 5);
            AddSlider(Harass, "Enemies to use Barrel", 1, 1, 5);
            AddSlider(Harass, "Enemies to use Q on Barrel", 1, 1, 5);
            AddSlider(Harass, "Enemies to Auto-Attack Barrel", 1, 1, 5);

            AddCheckboxes(ref AutoHarass, "Use Q_false", "Use E_false", "Auto-Attack Barrels if Q on cooldown_false", "Use Q on enemies", "Use Q on enemies only if no barrels around", "Use Q to kill barrels", "Create First Barrel_false", "Chain Barrels");
            AddCheckboxes(ref LaneClear, "Use Q", "Use E", "Auto-Attack Barrels if Q on cooldown", "Use Q on enemies", "Use Q on enemies only if no barrels around", "Use Q to kill barrels", "Create First Barrel", "Chain Barrels", "Use Items");
            AddSlider(LaneClear, "Minions to use Barrel", 3, 1, 5);
            AddSlider(LaneClear, "Minions to use Q on Barrel", 1, 1, 5);
            AddSlider(LaneClear, "Minions to Auto-Attack Barrel", 1, 1, 5);

            AddCheckboxes(ref JungleClear, "Use Q", "Use E", "Auto-Attack Barrels if Q on cooldown", "Use Q on enemies", "Use Q on enemies only if no barrels around", "Use Q to kill barrels", "Create First Barrel", "Chain Barrels", "Use Items");
            AddSlider(JungleClear, "Minions to use Barrel", 1, 1, 5);
            AddSlider(JungleClear, "Minions to use Q on Barrel", 1, 1, 5);
            AddSlider(JungleClear, "Minions to Auto-Attack Barrel", 1, 1, 5);

            AddCheckboxes(ref LastHit, "Use Q", "Use E", "Auto-Attack Barrels if Q on cooldown", "Use Q on enemies", "Use Q on enemies only if no barrels around", "Use Q to kill barrels", "Create First Barrel", "Chain Barrels", "Use Items");
            AddSlider(LastHit, "Minions to use Barrel", 3, 1, 5);
            AddSlider(LastHit, "Minions to use Q on Barrel", 1, 1, 5);
            AddSlider(LastHit, "Minions to Auto-Attack Barrel", 1, 1, 5);

            AddCheckboxes(ref Killsteal, "Activate Killsteal", "Auto-Attack Barrels if Q on cooldown", "Use Q", "Use Q on enemies", "Use Q on enemies only if no barrels around", "Use Q to kill barrels", "Use E", "Create First Barrel", "Chain Barrels", "Use R", "Use Items", "Use Ignite");
            AddCheckboxes(ref Flee, "Use Passive", "Slow Enemies with Barrels", "Ult for slow_false");
            AddCheckboxes(ref Drawing, "Draw Q", "Draw Health after W", "Draw E", "Draw E Circle on Mouse_false", "Draw E Chains", "Draw Enemies Killable with E", "Draw Enemies Killable with E + Q", "Draw Killable Text", "Draw Enemy Health after Combo", "Draw Silver Serpent Notifier", "Draw Shiny Barrels_false");
            AddSlider(Drawing, "Autos in Combo", 2, 0, 5);

            AddCheckboxes(ref Items, "Use Quick Silver Sash", "Use Mercurials Scimitar", "Use QSS to remove slows_false", "Use Mercurials Scimitar to remove slows_false", "Use Tiamat", "Use Ravenous Hydra", "Use Titanic Hydra", "Use Youmuus", "Use Bilgewater Cutlass", "Use Hextech Gunblade", "Use Blade of the Ruined King");
            AddSlider(Items, "HP to use Potions", 40, 1, 99);

            Items.AddSeparator();
            AddCheckboxes(ref Items, "Auto W", "Use W to remove CC", "Use W to remove slows_false");
            AddSlider(Items, "HP to use W", 30, 1, 99);
            AddSlider(Items, "Mana to use W", 80, 1, 99);

            AddCheckboxes(ref Settings, "Auto-Place Barrels with 3 stacks");
            AddComboBox(Settings, "Barrel Mode:", 2, "Best Prediction", "Best FPS", "Middle Ground");
            AddComboBox(Settings, "Barrel Range Mode:", 0, "Max Range", "Middle Ground", "Any Position");
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
