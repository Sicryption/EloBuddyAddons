using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

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
        public static List<MissileClient> DrawingArray = new List<MissileClient>();

        public static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
            Loading.OnLoadingComplete += MenuHandler.Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            SpellDatabase.Initialize();
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            GameObject.OnCreate += MissileClient_OnCreate;
            GameObject.OnDelete += MissileClient_OnDelete;
            Obj_AI_Base.OnSpellCast += AIHeroClient_OnSpellCast;
        }

        private static void AIHeroClient_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            #region Spells Not Loaded Into List
            if (sender.Type == GameObjectType.AIHeroClient)
            {
                if(!SpellDatabase.HasSpell(sender, args))
                {
                    Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>> obj = SpellDatabase.GetNewProjectileListing(sender.BaseSkinName, args.Start);

                    if (obj != null)
                        SpellDatabase.PrintOutNewProjectileInformation(new Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>>(obj.Item1, new Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>(sender, args)));
                    else
                        SpellDatabase.newProjectiles.Add(new Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>>(null, new Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>(sender, args)));
                }
            }
            #endregion
        }

        private static void MissileClient_OnDelete(GameObject sender, EventArgs args)
        {
            /*MissileClient projectile = sender as MissileClient;
            if(!MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show only Enemy Projectiles")
                || (projectile.SpellCaster.IsEnemy && MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show only Enemy Projectiles")))
                if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Debug Projectile Deletion"))
                    Console.WriteLine("Projectile Deleted. Originally Created by: "
                        + projectile.SpellCaster.Name + ", Projectile Name: "
                        + projectile.Name + ", Projectile SData Name: "
                        + projectile.SData.Name + ", Spell Name: "
                        + projectile.SpellCaster.Spellbook.GetSpell(projectile.Slot).Name + ", Spell Data Name: "
                        + projectile.SpellCaster.Spellbook.GetSpell(projectile.Slot).SData.Name);
                        */
        }

        private static void MissileClient_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender == null)
                return;

            MissileClient projectile = sender as MissileClient;

            if (projectile == null || projectile.SpellCaster == null || projectile.SData == null)
                return;

            #region Debug Code
            if ((!MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show only Enemy Projectiles")
                    || projectile.SpellCaster.IsEnemy)
                && (!MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show only Champion Projectiles")
                    || projectile.SpellCaster.Type == GameObjectType.AIHeroClient))
                if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Debug Projectile Creation") && SpellDatabase.HasSpell(projectile))
                {
                    SpellInfo spellInfo = SpellDatabase.GetSpellInfo(projectile);
                    string debugText = "";
                    debugText += "Projectile Created. Created by: " + spellInfo.ChampionName;
                    debugText += ", Projectile Missile Name: " + spellInfo.MissileName;

                    Console.WriteLine(debugText);
                }
            #endregion

            #region Spells Not Loaded Into List
            //checks if the database knows what this spell is, if the database doesn't know, print out the stuff needed to add it
            if (projectile.SpellCaster.Type == GameObjectType.AIHeroClient && !SpellDatabase.HasSpell(projectile))
            {
                Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>> obj = SpellDatabase.GetNewProjectileListing(projectile.SpellCaster.BaseSkinName, projectile.StartPosition);

                if (obj != null)
                    SpellDatabase.PrintOutNewProjectileInformation(new Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>>(projectile, obj.Item2));
                else
                    SpellDatabase.newProjectiles.Add(new Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>>(projectile, new Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>(null, null)));
            }
            #endregion
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
