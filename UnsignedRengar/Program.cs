using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace UnsignedRengar
{
    internal class Program
    {
        public static AIHeroClient Rengar = Player.Instance;
        public static Spell.Active W,
            EmpW;
        public static Spell.Skillshot E,
            EmpE,
            Q,
            EmpQ;
        public static Spell.Active R1;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Rengar.ChampionName != "Rengar")
                return;

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
        }
        
        private static void Drawing_OnDraw(EventArgs args)
        {

        }

        private static void Game_OnTick(EventArgs args)
        {

        }
    }
}