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
using EloBuddy.Sandbox;
using System.IO;
using SharpDX;

namespace UnsignedMinigames
{
    class MenuHandler
    {
        public static Menu mainMenu, Settings, SnakeMenu, TetrisMenu;

        public static void Initialize()
        {
            #region CreateMenus
            mainMenu = MainMenu.AddMenu("UnsignedMinigames", "UnsignedMinigames");
            mainMenu.AddGroupLabel("UnsignedMinigames");
            mainMenu.AddLabel("This Addon was developed by Chaos.");
            mainMenu.AddLabel("This Addon currently has: Snake and Tetris");
            SnakeMenu = AddSubMenu(mainMenu, "Snake");
            TetrisMenu = AddSubMenu(mainMenu, "Tetris");
            Settings = AddSubMenu(mainMenu, "Settings");
            #endregion

            #region Set Menu Values
            AddCheckboxes(ref Settings, "Draw when Alive_false", "Draw Element Names_false", "Debug Click Actions_false", "Debug Hover Actions_false");
            AddSlider(SnakeMenu, "Game Speed", 10, 1, 100);
            KeyBind up = AddKeybind(ref SnakeMenu, "Up", KeyBind.BindTypes.HoldActive, 87, 38);
            KeyBind down = AddKeybind(ref SnakeMenu, "Down", KeyBind.BindTypes.HoldActive, 83, 40);
            KeyBind left = AddKeybind(ref SnakeMenu, "Left", KeyBind.BindTypes.HoldActive, 65, 37);
            KeyBind right = AddKeybind(ref SnakeMenu, "Right", KeyBind.BindTypes.HoldActive, 68, 39);

            up.OnValueChange += delegate { if(!up.CurrentValue) Snake.SetDirection(Snake.Direction.North); };
            down.OnValueChange += delegate { if (!down.CurrentValue) Snake.SetDirection(Snake.Direction.South); };
            left.OnValueChange += delegate { if (!left.CurrentValue) Snake.SetDirection(Snake.Direction.West); };
            right.OnValueChange += delegate { if (!right.CurrentValue) Snake.SetDirection(Snake.Direction.East); };
            AddSlider(TetrisMenu, "Game Speed", 10, 1, 100);
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
        public static KeyBind AddKeybind(ref Menu menu, string text, KeyBind.BindTypes bindType, uint key1, uint key2, bool defaultValue = false)
        {
            return menu.Add(menu.UniqueMenuId + text, new KeyBind(text, defaultValue, bindType, key1, key2));
        }
        public static KeyBind GetKeybind(Menu menu, string text)
        {
            return menu.Get<KeyBind>(menu.UniqueMenuId + text);
        }
        public static bool GetKeybindValue(Menu menu, string text)
        {
            KeyBind keybind = GetKeybind(menu, text);

            if (keybind == null)
                Console.WriteLine("Keybind (" + text + ") not found under menu (" + menu.DisplayName + "). Unique ID (" + menu.UniqueMenuId + text + ")");

            return keybind.CurrentValue;
        }
    }
}
