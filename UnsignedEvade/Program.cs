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
        public static List<SpellInfo> activeSpells = new List<SpellInfo>();

        public static readonly Random Random = new Random(DateTime.Now.Millisecond);
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        private static void Main(string[] args)
        {
            DodgeManager.Initialize();
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
            Loading.OnLoadingComplete += MenuHandler.Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            SpellDatabase.Initialize();
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            GameObject.OnCreate += MissileClient_OnCreate;
            Obj_AI_Base.OnSpellCast += AIHeroClient_OnSpellCast;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            HandleSpellCast(sender, args, SpellInfo.SpellCreationLocation.OnProcessSpell);
        }

        private static void AIHeroClient_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            HandleSpellCast(sender, args, SpellInfo.SpellCreationLocation.OnSpellCast);
        }

        private static void HandleSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args, SpellInfo.SpellCreationLocation location)
        {
            if (sender.Type != GameObjectType.AIHeroClient)
                return;

            if (args.SData.Name.ToLower().Contains("basicattack") ||
                args.SData.Name.ToLower().Contains("critattack"))
                return;

            //Console.WriteLine("SpellCast at: " + Game.Time);
            SpellInfo info = SpellDatabase.GetSpellInfo(args.SData.Name);
            if (info != null && (location == SpellInfo.SpellCreationLocation.OnProcessSpell || info.TravelTime == -1f))
            {
                SpellInfo newSpellInstance = SpellDatabase.CreateInstancedSpellInfo(info);

                newSpellInstance.startPosition = args.Start;
                newSpellInstance.endPosition = args.End;
                //have to set newSpellInstance.endPosition in order for Geometry to calculate it correctly
                //extends it to max range, 
                if ((!info.canVaryInLength || args.Start.Distance(args.End) >= info.Range) && info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot)
                    newSpellInstance.endPosition = Geometry.CalculateEndPosition(newSpellInstance);

                newSpellInstance.target = args.Target;
                newSpellInstance.caster = sender;
                newSpellInstance.CreationType = location;
                newSpellInstance.TimeOfCast = Game.Time;
                activeSpells.Add(newSpellInstance);

                //might remove this later. pretty sloppy imo.
                if (info.SpellName == "CarpetBomb" || info.SpellName == "CarpetBombMega")
                {
                    SpellInfo newSpellInstance2 = SpellDatabase.CreateInstancedSpellInfo(SpellDatabase.GetSpellInfo(args.SData.Name + "2"));

                    newSpellInstance2.startPosition = args.Start;
                    newSpellInstance2.endPosition = args.End;

                    if (!info.canVaryInLength || args.Start.Distance(args.End) >= info.Range)
                        newSpellInstance2.endPosition = Geometry.CalculateEndPosition(newSpellInstance2);
                    else
                        newSpellInstance2.endPosition = args.End;

                    newSpellInstance2.endPosition = newSpellInstance2.startPosition.Extend(newSpellInstance2.endPosition, newSpellInstance2.startPosition.Distance(newSpellInstance2.endPosition) + 50f).To3DWorld();
                    newSpellInstance2.startPosition = newSpellInstance2.endPosition.Extend(newSpellInstance2.startPosition, newSpellInstance2.startPosition.Distance(newSpellInstance2.endPosition) + 100f).To3DWorld();

                    newSpellInstance2.caster = sender;
                    newSpellInstance2.CreationType = location;
                    newSpellInstance2.TimeOfCast = Game.Time;
                    activeSpells.Add(newSpellInstance2);
                }
            }

            #region Print Out Spell Information
            if ((!MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show only Enemy Projectiles") || sender.IsEnemy)
                && info == null)
            {
                //spells
                if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Debug Projectile Creation"))
                {
                    string debugText = "";
                    debugText += "SpellName = \"" + args.SData.Name + "\",\r\n";
                    debugText += "ChampionName = \"" + sender.BaseSkinName + "\",\r\n";
                    debugText += "Range = " + sender.Spellbook.GetSpell(args.Slot).SData.CastRange + "f,\r\n";
                    debugText += "MissileSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileSpeed + "f,\r\n";
                    if (sender.Spellbook.GetSpell(args.Slot).SData.MissileMinSpeed.ToString() != "3.402823E+38")
                        debugText += "MissileMinSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileMinSpeed + "f,\r\n";
                    else
                        debugText += "MissileMinSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileSpeed + "f,\r\n";
                    if (sender.Spellbook.GetSpell(args.Slot).SData.MissileMaxSpeed.ToString() != "3.402823E+38")
                        debugText += "MissileMaxSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileMaxSpeed + "f,\r\n";
                    else
                        debugText += "MissileMaxSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileSpeed + "f,\r\n";
                    debugText += "Width = " + sender.Spellbook.GetSpell(args.Slot).SData.LineWidth + "f,\r\n";
                    debugText += "Radius = " + sender.Spellbook.GetSpell(args.Slot).SData.CastRadius + "f,\r\n";
                    debugText += "ConeDegrees = " + sender.Spellbook.GetSpell(args.Slot).SData.CastConeAngle + "f,\r\n";

                    Console.WriteLine(debugText);
                }
            }
            #endregion
        }

        private static void MissileClient_OnCreate(GameObject sender, EventArgs args)
        {
            if (sender == null)
                return;

            MissileClient projectile = sender as MissileClient;

            if (projectile == null || projectile.SpellCaster == null || projectile.SData == null || projectile.SpellCaster.Type != GameObjectType.AIHeroClient)
                return;

            SpellInfo info = SpellDatabase.GetSpellInfo(projectile.SData.Name);
            if (info != null)
            {
                SpellInfo newSpellInstance = SpellDatabase.CreateInstancedSpellInfo(info);

                newSpellInstance.startPosition = projectile.StartPosition;
                newSpellInstance.endPosition = projectile.EndPosition;
                newSpellInstance.endPosition = Geometry.CalculateEndPosition(newSpellInstance);
                newSpellInstance.target = projectile.Target;
                newSpellInstance.caster = projectile.SpellCaster;
                newSpellInstance.missile = projectile;
                newSpellInstance.CreationType = SpellInfo.SpellCreationLocation.OnObjectCreate;
                newSpellInstance.TimeOfCast = Game.Time;
                activeSpells.Add(newSpellInstance);
            }
            else
            {
                #region Print Out Spell Information
                if ((!MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show only Enemy Projectiles")
                        || projectile.SpellCaster.IsEnemy))
                    if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Debug Projectile Creation"))
                    {
                        string debugText = "";
                        debugText += "MissileName = \"" + projectile.SData.Name + "\",\r\n";
                        debugText += "ChampionName = \"" + projectile.SpellCaster.BaseSkinName + "\",\r\n";
                        debugText += "MissileSpeed = " + projectile.SData.MissileSpeed + "f,\r\n";


                        if (projectile.SData.MissileMinSpeed.ToString() != "3.402823E+38")
                            debugText += "MissileMinSpeed = " + projectile.SData.MissileMinSpeed + "f,\r\n";
                        else
                            debugText += "MissileMinSpeed = " + projectile.SData.MissileSpeed + "f,\r\n";
                        if (projectile.SData.MissileMaxSpeed.ToString() != "3.402823E+38")
                            debugText += "MissileMaxSpeed = " + projectile.SData.MissileMaxSpeed + "f,\r\n";
                        else
                            debugText += "MissileMaxSpeed = " + projectile.SData.MissileSpeed + "f,\r\n";


                        if (projectile.SData.Name.ToLower().Contains("basicattack") ||
                            projectile.SData.Name.ToLower().Contains("critattack"))
                            debugText += "Range = " + projectile.SpellCaster.GetAutoAttackRange() + "f,\r\n";
                        else
                        {
                            debugText += "Range = " + projectile.SData.CastRange + "f,\r\n";
                            debugText += "Width = " + projectile.SData.LineWidth + "f,\r\n";
                        }

                        Console.WriteLine(debugText);
                    }
                #endregion
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            #region clear screen when object is destroyed
            //when removing object from below add it to this list then cross reference in another method and reset.
            List<SpellInfo> KeepList = new List<SpellInfo>();

            foreach (SpellInfo info in activeSpells)
            {
                switch(info.CreationType)
                {
                    //Before Cast Delay
                    case SpellInfo.SpellCreationLocation.OnProcessSpell:
                        //if is dash and dashtype is linear
                        if (info.DashType == SpellInfo.Dashtype.Linear && (info.caster.IsDashing() || Game.Time - info.TimeOfCast <= info.Delay))   
                            KeepList.Add(info);
                        //name would be different if it can be toggled ie: Gragas Q
                        if (info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot 
                            && (Game.Time - info.TimeOfCast <= info.Delay + info.TravelTime  || Math.Max(0, info.GetChampionSpell().CooldownExpires - Game.Time) == 0))
                            KeepList.Add(info);
                        break;
                    //After Cast Delay
                    case SpellInfo.SpellCreationLocation.OnSpellCast:
                        //if (info.startPosition != Vector3.Zero && info.endPosition != Vector3.Zero && info.startPosition.Distance(info.endPosition) <= 2* info.Range
                        //    && !info.caster.Spellbook.GetSpell(info.GetEBSpellSlot()).IsOnCooldown)
                        //    KeepList.Add(info);
                        break;
                    //Projectile
                    case SpellInfo.SpellCreationLocation.OnObjectCreate:
                        //linear skillshot
                        if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot && info.missile != null && info.missile.StartPosition != Vector3.Zero && info.missile.EndPosition != Vector3.Zero && info.missile.Name != null)
                            KeepList.Add(info);
                        break;
                }
            }

            activeSpells = KeepList;
            #endregion
            
            foreach (SpellInfo info in activeSpells)
            {
                if (info.caster.IsEnemy || MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Draw, "Draw Friendly Projectiles"))
                {
                    if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot)
                        Geometry.DrawLinearSkillshot(info);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.Targeted && (info.target.Name == Player.Instance.Name || MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show Friendly Targeted Spells")))
                        Geometry.DrawTargetedSpell(info);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot)
                        Geometry.DrawCircularSkillshot(info);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSkillshot)
                        Geometry.DrawConeSkillshot(info);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.SelfActive)
                        Geometry.DrawCircularSkillshot(info, true);
                }
            }
        }
       
        private static void Game_OnTick(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            DodgeManager.HandleDodging();
        }
    }
}
