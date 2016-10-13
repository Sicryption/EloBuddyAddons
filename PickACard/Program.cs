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
            if (Player.Instance.ChampionName != "TwistedFate")
                return;

            W = new Spell.Active(SpellSlot.W);

            menu = MainMenu.AddMenu("Pick a Card", "PickACard");
            CheckBox Toggle = menu.Add("Toggle", new CheckBox("Toggle Card Selection Type", true));
            //Toggle key
            KeyBind toggleKey = menu.Add("Toggle Key", new KeyBind("Toggle Key", false, KeyBind.BindTypes.HoldActive, 85));
            
            menu.Add("Draw Card Type", new CheckBox("Draw Card Type", true));
            //U key
            menu.Add("Gold Card", new KeyBind("Gold Card", false, KeyBind.BindTypes.HoldActive, 85));
            //I key
            menu.Add("Blue Card", new KeyBind("Blue Card", false, KeyBind.BindTypes.HoldActive, 73));
            //O key
            menu.Add("Red Card", new KeyBind("Red Card", false, KeyBind.BindTypes.HoldActive, 79));

            Slider slider = menu.Add("Card Slider", new Slider("Active Card: ", 0, 0, 2));
            
            string[] cards = new string[] { "Gold Card", "Blue Card", "Red Card" };
            slider.DisplayName = "Active Card: " + cards[slider.CurrentValue];
            slider.OnValueChange += Program_OnValueChange;

            toggleKey.OnValueChange += ToggleKey_OnValueChange;
            Toggle.OnValueChange += Toggle_OnValueChange;
            Toggle_OnValueChange(Toggle, null);

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Toggle_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            if (menu.Get<CheckBox>("Toggle").CurrentValue)
            {
                menu.Get<KeyBind>("Gold Card").IsVisible = false;
                menu.Get<KeyBind>("Blue Card").IsVisible = false;
                menu.Get<KeyBind>("Red Card").IsVisible = false;
                menu.Get<KeyBind>("Toggle Key").IsVisible = true;
                menu.Get<Slider>("Card Slider").IsVisible = true;
                menu.Get<CheckBox>("Draw Card Type").IsVisible = true;
            }
            else
            {
                menu.Get<KeyBind>("Gold Card").IsVisible = true;
                menu.Get<KeyBind>("Blue Card").IsVisible = true;
                menu.Get<KeyBind>("Red Card").IsVisible = true;
                menu.Get<KeyBind>("Toggle Key").IsVisible = false;
                menu.Get<Slider>("Card Slider").IsVisible = false;
                menu.Get<CheckBox>("Draw Card Type").IsVisible = false;
            }
        }

        private static void ToggleKey_OnValueChange(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            KeyBind keybind = sender as KeyBind;
            if(keybind.CurrentValue == true)
            {
                Slider slider = menu.Get<Slider>("Card Slider");

                if (slider.CurrentValue == 2)
                    slider.CurrentValue = 0;
                else
                    slider.CurrentValue = slider.CurrentValue + 1;
            }
        }

        private static void Program_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            string[] cards = new string[] { "Gold Card", "Blue Card", "Red Card" };
            Slider slider = menu.Get<Slider>("Card Slider");
            slider.DisplayName = "Active Card: " + cards[slider.CurrentValue];
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (menu.Get<CheckBox>("Toggle").CurrentValue)
            {
                string[] cards = new string[] { "Gold Card", "Blue Card", "Red Card" };
                Slider slider = menu.Get<Slider>("Card Slider");
                if (menu.Get<CheckBox>("Draw Card Type").CurrentValue)
                    Drawing.DrawText(Player.Instance.Position.WorldToScreen(), System.Drawing.Color.Red, cards[slider.CurrentValue], 12);
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (menu.Get<CheckBox>("Toggle").CurrentValue)
            {
                if (menu.Get<Slider>("Card Slider").CurrentValue == 0 && W.Name == "GoldCardLock")
                    W.Cast();
                else if (menu.Get<Slider>("Card Slider").CurrentValue == 1 && W.Name == "BlueCardLock")
                    W.Cast();
                else if (menu.Get<Slider>("Card Slider").CurrentValue == 2 && W.Name == "RedCardLock")
                    W.Cast();
            }
            else
            {
                if (menu.Get<KeyBind>("Gold Card").CurrentValue == true && W.Name == "GoldCardLock")
                    W.Cast();
                else if (menu.Get<KeyBind>("Blue Card").CurrentValue == true && W.Name == "BlueCardLock")
                    W.Cast();
                else if (menu.Get<KeyBind>("Red Card").CurrentValue == true && W.Name == "RedCardLock")
                    W.Cast();
            }
        }
    }
}