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
        private static Menu mainMenu, DodgeMenu, DebugMenu, DrawMenu;

        public enum MenuType
        {
            Main,
            Dodge,
            Debug,
            Draw
        }

        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            mainMenu = MainMenu.AddMenu("Unsigned Evade", "Unsigned Evade");

            DodgeMenu = mainMenu.AddSubMenu("Dodge Menu");
            DodgeMenu.AddGroupLabel("Dodge Settings");
            
            DrawMenu = mainMenu.AddSubMenu("Draw Menu");
            DrawMenu.AddGroupLabel("Draw Settings");
            CreateCheckbox(ref DrawMenu, "Draw Friendly Projectiles", false);
            CreateCheckbox(ref DrawMenu, "Draw Player Direction", false);

            DebugMenu = mainMenu.AddSubMenu("Debug Menu");
            DebugMenu.AddGroupLabel("Debug Settings");
            CreateCheckbox(ref DebugMenu, "Debug Projectile Creation");
            CreateCheckbox(ref DebugMenu, "Debug Projectile Deletion");
            CreateCheckbox(ref DebugMenu, "Show only Enemy Projectiles");
            CreateCheckbox(ref DebugMenu, "Show Animation Names", false);
            CreateCheckbox(ref DebugMenu, "Show Friendly Targeted Spells", false);
            CreateCheckbox(ref DebugMenu, "Show Particles", false);
            CreateCheckbox(ref DebugMenu, "Show All Object Names", false);
        }

        private static CheckBox CreateCheckbox(ref Menu menu, string name, bool defaultValue = true)
        {
            return menu.Add(name, new CheckBox(name, defaultValue));
        }

        private static Menu getMenu(MenuType type)
        {
            switch(type)
            {
                case MenuType.Debug:
                    return DebugMenu;
                case MenuType.Main:
                    return mainMenu;
                case MenuType.Dodge:
                    return DodgeMenu;
                case MenuType.Draw:
                    return DrawMenu;
            }
            return null;
        }

        public static CheckBox GetCheckbox(MenuType menuType, string uniqueName)
        {
            CheckBox checkbox = getMenu(menuType).Get<CheckBox>(uniqueName);
            if (checkbox == null)
                Console.WriteLine("Checkbox " + uniqueName + " does not exist under this menu type: " + menuType.ToString());
            return checkbox;
        }

        public static bool GetCheckboxValue(MenuType menuType, string uniqueName)
        {
            return GetCheckbox(menuType, uniqueName).CurrentValue;
        }
    }
}
