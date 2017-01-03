using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using SharpDX;

namespace UnsignedPolygonPing
{
    static class CustomExtensions
    {
        public static bool GetCheckboxValue(this Menu self, string text)
        {
            return MenuHandler.GetCheckboxValue(self, text);
        }
        public static int GetSliderValue(this Menu self, string text)
        {
            return MenuHandler.GetSliderValue(self, text);
        }
        public static string GetComboBoxText(this Menu self, string text)
        {
            return MenuHandler.GetComboBoxText(self, text);
        }
        public static int GetComboBoxValue(this Menu self, string text)
        {
            return MenuHandler.GetComboBox(self, text).CurrentValue;
        }
        public static bool GetKeybindValue(this Menu self, string text)
        {
            return MenuHandler.GetKeybindValue(self, text);
        }
    }
}