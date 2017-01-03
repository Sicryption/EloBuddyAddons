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

namespace UnsignedPolygonPing
{
    class MenuHandler
    {
        public static Menu mainMenu, PingMenu;

        public static void Initialize()
        {
            #region CreateMenus
            mainMenu = MainMenu.AddMenu("Polygon Ping", "Polygon Ping");
            PingMenu = AddSubMenu(mainMenu, "Ping Menu");
            #endregion
            
            #region Set Menu Values
            mainMenu.AddLabel("This Addon was created by Chaos.");
            mainMenu.AddLabel("The original idea and some source code was made by Trees on the L# Platform.");
            mainMenu.AddSeparator(10);
            mainMenu.AddLabel("Use this to annoy your allies/enemies just a little bit more.");

            AddCheckbox(ref PingMenu, "Enable", true);
            AddComboBox(PingMenu, "Ping Type:", 4, PingType.Normal.ToString(), PingType.Danger.ToString(), PingType.EnemyMissing.ToString(), PingType.OnMyWay.ToString(), PingType.Fallback.ToString(),PingType.AssistMe.ToString());
            AddKeybind(ref PingMenu, "Ping Key", 'G', 'G');
            AddComboBox(PingMenu, "Ping Shape:", 0, "Single", "Pentagon", "Square", "Diamond", "Triangle", "Cross", "Plus Sign", "Vertical Line", "Horizontal Line", "Arrow to Player", "Arrow away from Player");
            AddSlider(PingMenu, "Distance", 400, 10, 1000);

            List<string> playerNames = new List<string>() { "None" };

            foreach (AIHeroClient client in EntityManager.Heroes.AllHeroes)
                playerNames.Add(client.Name);
                        
            AddComboBox(PingMenu, "Spam Ping Player:", playerNames);
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
        public static KeyBind AddKeybind(ref Menu menu, string text, char key1, char key2)
        {
            return menu.Add(menu.UniqueMenuId + text, new KeyBind(text, false, KeyBind.BindTypes.HoldActive, char.ToUpper(key1), char.ToUpper(key2)));
        }
        public static KeyBind GetKeybind(Menu menu, string text)
        {
            return menu.Get<KeyBind>(menu.UniqueMenuId + text);
        }
        public static bool GetKeybindValue(Menu menu, string text)
        {
            return menu.Get<KeyBind>(menu.UniqueMenuId + text).CurrentValue;
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
        public static ComboBox AddComboBox(Menu menu, string text, List<string> values, int defaultValue = 0)
        {
            return menu.Add(menu.UniqueMenuId + text, new ComboBox(text, values.AsEnumerable(), defaultValue));
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
