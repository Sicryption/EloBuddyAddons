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

//Once .Direction is fixed, fix GetConePosition to work with Corki E

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
            GameObject.OnCreate += GameObject_OnCreate;
            Obj_AI_Base.OnSpellCast += AIHeroClient_OnSpellCast;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
            Obj_AI_Base.OnBuffLose += Obj_AI_Base_OnBuffLose;
        }

        private static void Obj_AI_Base_OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
            if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show Buff Losses"))
                Console.WriteLine(args.Buff.Name);
        }

        private static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show Buff Gains"))
                Console.WriteLine(args.Buff.Name);

            foreach (SpellInfo info in activeSpells.Where(a => a.BuffName != ""))
                if (args.Buff.Name == info.BuffName && args.Buff.Caster.Name == info.caster.Name)
                    info.target = sender;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            HandleSpellCast(sender, args, SpellInfo.SpellCreationLocation.OnProcessSpell);
        }

        private static void AIHeroClient_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            //HandleSpellCast(sender, args, SpellInfo.SpellCreationLocation.OnSpellCast);
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
                if ((!info.canVaryInLength || args.Start.Distance(args.End) >= info.Range) && info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot)
                    newSpellInstance.endPosition = Geometry.CalculateEndPosition(args.Start, args.End, info.Range);
                else
                    newSpellInstance.endPosition = args.End;

                newSpellInstance.target = args.Target;
                newSpellInstance.caster = sender;
                newSpellInstance.CreationType = location;
                newSpellInstance.TimeOfCast = Game.Time;
                activeSpells.Add(newSpellInstance);
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
        
        private static void GameObject_OnCreate(GameObject sender, EventArgs args)  
        {
            if (sender == null)
                return;

            //projectile
            if (sender.Name == "missile")
            {
                MissileClient projectile = sender as MissileClient;

                if (projectile == null || projectile.SpellCaster == null || projectile.SData == null || projectile.SpellCaster.Type != GameObjectType.AIHeroClient)
                    return;

                SpellInfo info = SpellDatabase.GetSpellInfo(projectile.SData.Name);
                if (info != null)
                {
                    SpellInfo newSpellInstance = SpellDatabase.CreateInstancedSpellInfo(info);
                    
                    newSpellInstance.startPosition = projectile.StartPosition;
                    if ((!info.canVaryInLength || projectile.StartPosition.Distance(projectile.EndPosition) >= info.Range) && info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot)
                        newSpellInstance.endPosition = Geometry.CalculateEndPosition(projectile.StartPosition, projectile.EndPosition, info.Range);
                    else
                        newSpellInstance.endPosition = projectile.EndPosition;

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
            else
            {
                //handles traps
                if (sender.Type == GameObjectType.obj_AI_Minion)
                    return;
                if (sender.Type == GameObjectType.obj_GeneralParticleEmitter)
                    HandleParticle(sender);
            }
        }

        public static void HandleParticle(GameObject ob)
        {
            Obj_GeneralParticleEmitter particle = ob as Obj_GeneralParticleEmitter;
            ParticleInfo info = ParticleDatabase.GetParticleInfo(ob.Name);
            if (info == null && MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show Particles"))
                Console.WriteLine("\"" + ob.Name + "\",");
            else
            {
                //known particle
                //this is where the particle handler should be

            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (_Player.IsDead)
                return;
            
            if(MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Draw, "Draw Player Direction"))
                Drawing.DrawText(_Player.Position.WorldToScreen(), Geometry.drawColor, _Player.Direction.ToString(), 15);

            if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show All Object Names"))
                foreach (GameObject ob in ObjectManager.Get<GameObject>())
                    Drawing.DrawText(ob.Position.WorldToScreen(), Geometry.drawColor, ob.Name, 15);

            #region clear screen when object is destroyed
            //when removing object from below add it to this list then cross reference in another method and reset.
            List<SpellInfo> KeepList = new List<SpellInfo>();

            foreach (SpellInfo info in activeSpells)
            {
                switch (info.CreationType)
                {
                    //Before Cast Delay
                    case SpellInfo.SpellCreationLocation.OnProcessSpell:
                        //if is dash and dashtype is linear
                        if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot)
                        {
                            if (info.DashType == SpellInfo.Dashtype.Linear)
                            {
                                if (info.caster.IsDashing() || Game.Time - info.TimeOfCast <= info.Delay)
                                    KeepList.Add(info);
                            }
                            else if (info.DashType == SpellInfo.Dashtype.None)
                            {
                                if (Game.Time - info.TimeOfCast <= info.Delay)
                                    KeepList.Add(info);
                            }
                        }
                        else if (info.SpellType == SpellInfo.SpellTypeInfo.Targeted)
                        {
                            if (info.DashType == SpellInfo.Dashtype.Linear)
                                if (info.caster.IsDashing() || Game.Time - info.TimeOfCast <= info.Delay)
                                    KeepList.Add(info);
                        }
                        else if (info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot)
                        {
                            float timeSinceCast = Game.Time - info.TimeOfCast;
                            float timeItTakesToCast = info.Delay + info.TravelTime;
                            if (timeSinceCast <= timeItTakesToCast || Math.Max(0, info.GetChampionSpell().CooldownExpires - Game.Time) == 0)
                                KeepList.Add(info);
                        }
                        else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSkillshot)
                        {
                            if (((info.BuffName == "" || info.caster.HasBuff(info.BuffName)) || Math.Max(0, info.GetChampionSpell().CooldownExpires - Game.Time) == 0))
                                KeepList.Add(info);
                        }
                        else if (info.SpellType == SpellInfo.SpellTypeInfo.SelfActive)
                        {
                            float timeSinceCast = Game.Time - info.TimeOfCast;
                            float timeItTakesToCast = info.Delay + info.TravelTime;

                            if (timeSinceCast <= timeItTakesToCast || Math.Max(0, info.GetChampionSpell().CooldownExpires - Game.Time) == 0)
                                KeepList.Add(info);
                        }
                        break;
                    //After Cast Delay
                    case SpellInfo.SpellCreationLocation.OnSpellCast:
                        break;
                    //Projectile
                    case SpellInfo.SpellCreationLocation.OnObjectCreate:
                        //linear skillshot
                        if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot)
                        {
                            if (info.missile != null && info.missile.StartPosition != Vector3.Zero && info.missile.EndPosition != Vector3.Zero && info.missile.SpellCaster != null && info.missile.SData != null && (info.missile.SData.Name == info.MissileName || info.OtherMissileNames.Contains(info.missile.SData.Name)))
                                KeepList.Add(info);
                        }
                        if (info.SpellType == SpellInfo.SpellTypeInfo.Targeted)
                        {
                            if (info.missile != null && info.missile.StartPosition != Vector3.Zero && info.missile.EndPosition != Vector3.Zero && info.missile.Name != null && info.missile.SData != null && (info.missile.SData.Name == info.MissileName || info.OtherMissileNames.Contains(info.missile.SData.Name)))
                                KeepList.Add(info);
                        }
                        if (info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot)
                        {
                            if (info.missile != null && info.missile.StartPosition != Vector3.Zero && info.missile.EndPosition != Vector3.Zero && info.missile.Name != null && info.missile.SData != null && (info.missile.SData.Name == info.MissileName || info.OtherMissileNames.Contains(info.missile.SData.Name)))
                                KeepList.Add(info);
                        }   
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
                    {
                        if (info.DashType == SpellInfo.Dashtype.None)
                        {
                            if (info.missile != null)
                            {
                                //sivir Q
                                if (info.MissileName.ToLower().Contains("return"))
                                    Geometry.DrawLinearSkillshot(info.missile.Position, info.missile.SpellCaster.Position, info.Width, info.MissileSpeed, info.Range, info.CollisionCount);
                                else
                                    Geometry.DrawLinearSkillshot(info.missile.Position, info.endPosition, info.Width, info.MissileSpeed, info.Range, info.CollisionCount);
                            }
                            else
                                Geometry.DrawLinearSkillshot(info.caster.Position, info.endPosition, info.Width, info.MissileSpeed, info.Range, info.CollisionCount);
                        }
                        else if (info.DashType == SpellInfo.Dashtype.Linear)
                            Geometry.DrawLinearSkillshot(info.caster.Position, info.endPosition, info.Width, info.MissileSpeed, info.Range, info.CollisionCount);
                    }
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.Targeted && (info.target.Name == Player.Instance.Name || MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show Friendly Targeted Spells")))
                    {
                        //lee sin does not have a target set. the target set is the one hovered over.
                        if (info.DashType == SpellInfo.Dashtype.Linear && info.target != null)
                            Geometry.DrawTargetedSpell(info.caster.Position, info.target);
                        else if (info.DashType == SpellInfo.Dashtype.None && (info.target.Name == Player.Instance.Name || MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show Friendly Targeted Spells")))
                            Geometry.DrawTargetedSpell(info.missile.Position, info.target);
                    }
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot)
                    {
                        if (info.missile != null)
                            Geometry.DrawCircularSkillshot(info.missile.EndPosition, info.Radius, info.SecondRadius);
                        else
                            Geometry.DrawCircularSkillshot(info.endPosition, info.Radius, info.SecondRadius);
                    }
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSkillshot)
                        Geometry.DrawConeSkillshot(info.caster.Position, info.endPosition, info.ConeDegrees);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.SelfActive)
                        Geometry.DrawCircularSkillshot(info.caster.Position, info.Radius, info.SecondRadius);
                }
            }

            foreach(Obj_GeneralParticleEmitter particle in ObjectManager.Get<Obj_GeneralParticleEmitter>())
            {
                ParticleInfo info = ParticleDatabase.GetParticleInfo(particle.Name);

                if (info != null)
                {
                    if (info.SpellType == ParticleInfo.SpellTypeInfo.Wall)
                        Geometry.DrawRectangle(info.Length, info.Width, particle.Position, info.XOffset, info.YOffset);
                    else if (info.SpellType == ParticleInfo.SpellTypeInfo.CircularSkillshot)
                        Geometry.DrawCircularSkillshot(particle.Position, info.Radius);
                }
            }

            foreach(Obj_AI_Minion trap in ObjectManager.Get<Obj_AI_Minion>().Where(a=>(a.IsEnemy || MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Draw, "Draw Friendly Projectiles")) && !a.IsDead && TrapDatabase.AllTrapNames().Contains(a.Name)))
            {
                Drawing.DrawCircle(trap.Position, TrapDatabase.getTrap(trap.Name).Radius, Geometry.drawColor);
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
