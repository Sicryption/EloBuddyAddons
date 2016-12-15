using System;
using System.Collections.Generic;
using System.Reflection;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Spells;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using System.Linq;
using System.IO;

/*
Types of Spells:
Targeted Projectiles - Annie Q, Ryze E
Linear Skillshot Projectiles - Ezreal Q/E/R, Ryze Q
Cone Skillshot Spells - Annie W, Nidalee E, Corki E,
Circular Skillshot Spells - Annie R, Singed W, Lee Sin E
Active Circular Spells - Maokai R, Sona Q, Aatrox R
Splash Effect - Ashe R/Jinx R/Kayle Empowered AA/Zilean Bomb
*/

namespace UnsignedEvade
{
    internal class Program
    {
        public static AIHeroClient _Player => Player.Instance;
        private static void Main(string[] args)
        {
            DodgeManager.Initialize();
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
            Loading.OnLoadingComplete += MenuHandler.Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            SpellDatabase.Initialize();
            //game on tick too slow for dodging.
            //Game.OnTick += Game_OnTick;
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += DrawingManager.Drawing_OnDraw;
            GameObject.OnCreate += SpellCreation.GameObject_OnCreate;
            Obj_AI_Base.OnSpellCast += SpellCreation.AIHeroClient_OnSpellCast;
            Obj_AI_Base.OnProcessSpellCast += SpellCreation.Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnBuffLose += Obj_AI_Base_OnBuffLose;
            ExtraSpellOverides.OnGameLoad();
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            /*foreach(SpellInfo inf in SpellDatabase.activeSpells)
            {
                if(inf.missile != null)
                {
                    if(inf.missilePosTest.Distance(inf.missile) <= 50)
                    {
                        inf.missilePosTest = inf.missile.Position;
                    }
                    else
                    {
                        Chat.Print(inf.oldTarget.Name + "|" + inf.missile.Target.Name);
                    }
                }
            }*/

            DodgeManager.HandleDodging();
        }

        private static void Obj_AI_Base_OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
            if (MenuHandler.DebugMenu.GetCheckboxValue("Show Buff Losses"))
                Console.WriteLine(args.Buff.Name);
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (MenuHandler.DebugMenu.GetCheckboxValue("Show Buff Gains"))
                Console.WriteLine(args.Buff.Name);
        }
    }
}
