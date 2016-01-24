using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UnsignedEvade
{
    internal class Program
    {
        public static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public static Menu mainMenu, DodgeMenu;
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            mainMenu = MainMenu.AddMenu("Unsigned Evade", "Unsigned Evade");

            DodgeMenu = DodgeMenu.AddSubMenu("Dodge Menu", "DodgeMenu");
            DodgeMenu.AddGroupLabel("Dodge Settings");
            
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (_Player.IsDead)
                return;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (_Player.IsDead)
                return;
        }
    }
}
