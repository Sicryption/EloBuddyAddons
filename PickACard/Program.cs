using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace PickACard
{
    internal class Program
    {
        public static Menu menu;
        public static Spell.Active W;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.Name != "TwistedFate")
                return;

            W = new Spell.Active(SpellSlot.W);

            menu = MainMenu.AddMenu("Shroom Tracker", "ShroomTracker");
            //U key
            menu.Add("Gold Card", new KeyBind("Gold Card", false, KeyBind.BindTypes.HoldActive, 'u'));
            //I key
            menu.Add("Blue Card", new KeyBind("Blue Card", false, KeyBind.BindTypes.HoldActive, 'i'));
            //O key
            menu.Add("Red Card", new KeyBind("Red Card", false, KeyBind.BindTypes.HoldActive, 'o'));
            menu.Add("Draw", new CheckBox("Draw Shrooms", true));

            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (menu.Get<KeyBind>("Gold Card").CurrentValue == true && W.Name == "goldcardlock")
                W.Cast();
            else if (menu.Get<KeyBind>("Blue Card").CurrentValue == true && W.Name == "bluecardlock")
                W.Cast();
            else if (menu.Get<KeyBind>("Red Card").CurrentValue == true && W.Name == "redcardlock")
                W.Cast();
        }
    }
}