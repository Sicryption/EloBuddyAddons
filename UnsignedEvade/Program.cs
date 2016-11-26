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
using EloBuddy.Sandbox;

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
        public static List<string> SpellsNotInDatabase = new List<string>();

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

            #region Adding Spell to Active Spells
            List<SpellInfo> spells = SpellDatabase.GetSpells(args.SData.Name);
            foreach (SpellInfo info in spells)
            {
                if (info != null
                    && (location == SpellInfo.SpellCreationLocation.OnProcessSpell || info.TravelTime == -1f)
                    && info.SpellName != "")
                {
                    SpellInfo newSpellInstance = SpellDatabase.CreateInstancedSpellInfo(info);

                    newSpellInstance.startPosition = args.Start;
                    if ((!info.CanVaryInLength || args.Start.Distance(args.End) >= info.Range) && info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot)
                        newSpellInstance.endPosition = Geometry.CalculateEndPosition(args.Start, args.End, info.Range);
                    else
                        newSpellInstance.endPosition = args.End;

                    newSpellInstance.MissileName = "";
                    newSpellInstance.startingDirection = sender.Direction;
                    newSpellInstance.target = args.Target;
                    newSpellInstance.caster = sender;
                    newSpellInstance.CreationLocation = location;
                    newSpellInstance.TimeOfCast = Game.Time;

                    if (newSpellInstance.GetChampionSpell().SData.MaxAmmo != -1)
                        newSpellInstance.startingAmmoCount = newSpellInstance.GetChampionSpell().Ammo;

                    //Console.WriteLine("Added Spell " + newSpellInstance.SpellName + " - " + Game.Time);
                    activeSpells.Add(newSpellInstance);
                }
            }
            #endregion

            #region Print Out Spell Information
            if ((!MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show only Enemy Projectiles") || sender.IsEnemy)
                && spells.Count == 0)
            {
                //spells
                if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Debug Projectile Creation"))
                {
                    string debugText = "";
                    debugText += "                SpellName = \"" + args.SData.Name + "\",\r\n";
                    debugText += "                ChampionName = \"" + sender.BaseSkinName + "\",\r\n";
                    debugText += "                Range = " + sender.Spellbook.GetSpell(args.Slot).SData.CastRange + "f,\r\n";
                    debugText += "                MissileSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileSpeed + "f,\r\n";
                    if (sender.Spellbook.GetSpell(args.Slot).SData.MissileMinSpeed.ToString() != "3.402823E+38")
                        debugText += "                MissileMinSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileMinSpeed + "f,\r\n";
                    else
                        debugText += "                MissileMinSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileSpeed + "f,\r\n";
                    if (sender.Spellbook.GetSpell(args.Slot).SData.MissileMaxSpeed.ToString() != "3.402823E+38")
                        debugText += "                MissileMaxSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileMaxSpeed + "f,\r\n";
                    else
                        debugText += "                MissileMaxSpeed = " + sender.Spellbook.GetSpell(args.Slot).SData.MissileSpeed + "f,\r\n";
                    debugText += "                Width = " + sender.Spellbook.GetSpell(args.Slot).SData.LineWidth + "f,\r\n";
                    debugText += "                Radius = " + sender.Spellbook.GetSpell(args.Slot).SData.CastRadius + "f,\r\n";
                    debugText += "                ConeDegrees = " + sender.Spellbook.GetSpell(args.Slot).SData.CastConeAngle + "f,\r\n";

                    if (!SpellsNotInDatabase.Contains(debugText))
                    {
                        SpellsNotInDatabase.Add(debugText);
                        Console.WriteLine(debugText);
                    }
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
                    //dont draw a spell if its missile was created  
                    if (activeSpells.ContainsSpellName(info.SpellName, true))
                        activeSpells.Remove(activeSpells.GetSpellFromSpellName(info.SpellName, true));

                    SpellInfo newSpellInstance = SpellDatabase.CreateInstancedSpellInfo(info);
                    
                    newSpellInstance.startPosition = projectile.StartPosition;
                    if ((!info.CanVaryInLength || projectile.StartPosition.Distance(projectile.EndPosition) >= info.Range) && info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot)
                        newSpellInstance.endPosition = Geometry.CalculateEndPosition(projectile.StartPosition, projectile.EndPosition, info.Range);
                    else
                        newSpellInstance.endPosition = projectile.EndPosition;

                    newSpellInstance.target = projectile.Target;
                    newSpellInstance.caster = projectile.SpellCaster;
                    newSpellInstance.missile = projectile;
                    newSpellInstance.CreationLocation = SpellInfo.SpellCreationLocation.OnObjectCreate;
                    newSpellInstance.TimeOfCast = Game.Time;

                    //Console.WriteLine("Added Missile " + newSpellInstance.MissileName + " - " + Game.Time);

                    if (!activeSpells.Contains(newSpellInstance))
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
                            debugText += "                MissileName = \"" + projectile.SData.Name + "\",\r\n";
                            debugText += "                ChampionName = \"" + projectile.SpellCaster.BaseSkinName + "\",\r\n";
                            debugText += "                MissileSpeed = " + projectile.SData.MissileSpeed + "f,\r\n";


                            if (projectile.SData.MissileMinSpeed.ToString() != "3.402823E+38")
                                debugText += "                MissileMinSpeed = " + projectile.SData.MissileMinSpeed + "f,\r\n";
                            else
                                debugText += "                MissileMinSpeed = " + projectile.SData.MissileSpeed + "f,\r\n";
                            if (projectile.SData.MissileMaxSpeed.ToString() != "3.402823E+38")
                                debugText += "                MissileMaxSpeed = " + projectile.SData.MissileMaxSpeed + "f,\r\n";
                            else
                                debugText += "                MissileMaxSpeed = " + projectile.SData.MissileSpeed + "f,\r\n";


                            if (projectile.SData.Name.ToLower().Contains("basicattack") ||
                                projectile.SData.Name.ToLower().Contains("critattack"))
                                debugText += "                Range = " + projectile.SpellCaster.GetAutoAttackRange() + "f,\r\n";
                            else
                            {
                                debugText += "                Range = " + projectile.SData.CastRange + "f,\r\n";
                                debugText += "                Width = " + projectile.SData.LineWidth + "f,\r\n";
                            }

                            if (!SpellsNotInDatabase.Contains(debugText))
                            {
                                SpellsNotInDatabase.Add(debugText);
                                Console.WriteLine(debugText);
                            }
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
            RefreshSpellList();
            if (_Player.IsDead)
                return;
            
            try
            {
                DrawDirection();
                DrawObjectNames();
                DrawSpells();
                DrawParticles();
                DrawTraps();
                DrawIllaoiTentacles();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
       
        private static void RefreshSpellList()
        {
            //when removing object from below add it to this list then cross reference in another method and reset.
            List<SpellInfo> KeepList = new List<SpellInfo>();

            foreach (SpellInfo info in activeSpells)
            {
                if (info.Slot == SpellInfo.SpellSlot.Auto)
                    HandleBasicAttacks(info, KeepList);
                else
                {
                    switch (info.CreationLocation)
                    {
                        //Before Cast Delay
                        case SpellInfo.SpellCreationLocation.OnProcessSpell:
                            HandleSpells(info, KeepList);
                            break;
                        //Projectile
                        case SpellInfo.SpellCreationLocation.OnObjectCreate:
                            //linear skillshot
                            HandleMissiles(info, KeepList);
                            break;
                    }
                }
            }

            activeSpells = KeepList;
        }

        private static void HandleSpells(SpellInfo info, List<SpellInfo> KeepList)
        {
            //we check if the spell is off cooldown because that is when the spell shouldn't be drawing, and the missile should be.
            try
            {
                //if is dash and dashtype is linear
                if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot ||
                    info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshotNoDamage ||
                    info.SpellType == SpellInfo.SpellTypeInfo.LinearMissile ||
                    info.SpellType == SpellInfo.SpellTypeInfo.LinearSpellWithDuration)
                {
                    if (Game.Time - info.TimeOfCast <= info.Delay || info.IsOffCooldown())
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.TargetedMissile ||
                    info.SpellType == SpellInfo.SpellTypeInfo.TargetedSpell ||
                    info.SpellType == SpellInfo.SpellTypeInfo.TargetedSpellWithDuration)
                {
                    //targeted spells dont have missiles if they are cast on themseleves. IE: nami w
                    if(info.target != info.caster)
                        if (Game.Time - info.TimeOfCast <= info.Delay || info.IsOffCooldown())
                            KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSpellWithBuff)
                {
                    if (Game.Time - info.TimeOfCast <= info.Delay || info.IsOffCooldown() && info.caster.HasBuff(info.BuffName))
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.LinearDash)
                {
                    if (info.caster.IsDashing() || Game.Time - info.TimeOfCast <= info.Delay || info.IsOffCooldown())
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.TargetedDash)
                {
                    if (info.DashType == SpellInfo.Dashtype.TargetedLinear)
                        if (info.caster.IsDashing() || Game.Time - info.TimeOfCast <= info.Delay || info.IsOffCooldown())
                            KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSpell)
                {
                    float timeItTakesToCast = info.Delay + info.TravelTime;
                    float timeSinceCast = Game.Time - info.TimeOfCast;
                    if (timeSinceCast <= timeItTakesToCast
                        || info.IsOffCooldown())
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSpellWithBuff)
                {
                    float timeItTakesToCast = info.Delay + info.TravelTime;
                    float timeSinceCast = Game.Time - info.TimeOfCast;
                    if (info.caster.HasBuff(info.BuffName)
                        || timeSinceCast <= timeItTakesToCast
                        || info.IsOffCooldown()
                        //this is so mf's ult isn't angled away from where it was casted
                        && (info.startingDirection == null || info.startingDirection == info.caster.Direction))
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.Wall
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularWall
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpell
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpellWithDuration)
                {
                    float timeSinceCast = Game.Time - info.TimeOfCast;
                    float timeItTakesToCast = info.Delay + info.TravelTime;

                    if (timeSinceCast <= timeItTakesToCast || info.IsOffCooldown())
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.SelfActive)
                {
                    float timeSinceCast = Game.Time - info.TimeOfCast;
                    float timeItTakesToCast = info.Delay + info.TravelTime;

                    if (timeSinceCast <= timeItTakesToCast || info.IsOffCooldown())
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.SelfActiveWithBuff)
                {
                    float timeSinceCast = Game.Time - info.TimeOfCast;
                    float timeItTakesToCast = info.Delay + info.TravelTime;

                    if (timeSinceCast <= timeItTakesToCast || info.IsOffCooldown() || info.caster.HasBuff(info.BuffName))
                        KeepList.Add(info);
                }
            }
            catch (Exception ex)
            {
                if (info.missile != null)
                    Console.WriteLine(info.ChampionName + "|" + info.SpellName + "|" + info.MissileName + "| MISSILE | HAS ERRORS");
                else
                    Console.WriteLine(info.ChampionName + "|" + info.SpellName + "|" + info.MissileName + "| SPELL | HAS ERRORS");
                Console.WriteLine(ex);
            }
        }

        private static void HandleMissiles(SpellInfo info, List<SpellInfo> KeepList)
        {
            try
            {
                if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot || info.SpellType == SpellInfo.SpellTypeInfo.LinearMissile || info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshotNoDamage)
                {
                    if (info.missile != null && info.missile.StartPosition != Vector3.Zero
                        && info.missile.EndPosition != Vector3.Zero && info.missile.SpellCaster != null
                        && info.missile.SData != null
                        && info.missile.SData.Name != null
                        && (info.missile.SData.Name == info.MissileName || info.OtherMissileNames.Contains(info.missile.SData.Name)))
                        KeepList.Add(info);
                }
                if (info.SpellType == SpellInfo.SpellTypeInfo.TargetedMissile)
                {
                    if (info.missile != null && info.missile.StartPosition != Vector3.Zero
                        && info.missile.EndPosition != Vector3.Zero && info.missile.Name != null
                            && ((info.missile.SData != null && info.missile.SData.Name != null
                            && (info.missile.SData.Name == info.MissileName || info.OtherMissileNames.Contains(info.missile.SData.Name)))
                        ||
                            info.Slot == SpellInfo.SpellSlot.Auto))
                        KeepList.Add(info);
                }
                if (info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot)
                {
                    if (info.missile != null && info.missile.StartPosition != Vector3.Zero
                        && info.missile.EndPosition != Vector3.Zero && info.missile.Name != null
                        && info.missile.SData != null
                        && info.missile.SData.Name != null
                        && (info.missile.SData.Name == info.MissileName || info.OtherMissileNames.Contains(info.missile.SData.Name)))
                        KeepList.Add(info);
                }
            }
            catch(Exception ex)
            {
                if(info.missile != null)
                    Console.WriteLine(info.ChampionName + "|" + info.SpellName + "|" + info.MissileName + "| MISSILE | HAS ERRORS");
                else
                    Console.WriteLine(info.ChampionName + "|" + info.SpellName + "|" + info.MissileName + "| SPELL | HAS ERRORS");
                Console.WriteLine(ex);
            }
        }

        private static void DrawSpells()
        {
            int count = 0;
            foreach (SpellInfo info in activeSpells)
            {
                count++;

                if (info.caster.IsEnemy || MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Draw, "Draw Friendly Projectiles"))
                {
                    if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Draw Active Spells"))
                    {
                        if (info.SpellName != "")
                            Drawing.DrawText(Vector2.Zero + new Vector2(0, 15 * count), Geometry.drawColor, info.SpellName, 15);
                        else if (info.MissileName != "")
                            Drawing.DrawText(Vector2.Zero + new Vector2(0, 15 * count), Geometry.drawColor, info.MissileName, 15);
                        else
                            Drawing.DrawText(Vector2.Zero + new Vector2(0, 15 * count), Geometry.drawColor, "I shoudn't exist 0.0", 15);
                    }

                    if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot || info.SpellType == SpellInfo.SpellTypeInfo.LinearMissile || info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshotNoDamage)
                    {
                        if (info.DashType == SpellInfo.Dashtype.None)
                        {
                            if (info.missile != null)
                            {
                                //sivir Q and draven R return 
                                if (info.MissileName.ToLower().Contains("return") ||
                                    (info.MissileName == "DravenR" && info.missile.EndPosition.Distance(info.caster.Position) <= 50))
                                    Geometry.DrawLinearSkillshot(info.missile.Position, info.missile.SpellCaster.Position, info.Width, info.MissileSpeed, info.Range, info.CollisionCount);
                                else
                                    Geometry.DrawLinearSkillshot(info.missile.Position, info.endPosition, info.Width, info.MissileSpeed, info.Range, info.CollisionCount);
                            }
                            //for on spell cast spells that dont have missiles
                            else if (info.MissileName == "" && info.BuffName == "")
                                Geometry.DrawLinearSkillshot(info.caster.Position, info.endPosition, info.Width, info.MissileSpeed, info.Range, info.CollisionCount);
                            else if (info.MissileName == "" && info.BuffName != "")
                                Geometry.DrawLinearSkillshot(info.caster.Position, info.caster.Position.Extend(info.caster.Position + info.caster.Direction, info.Range).To3D((int)info.caster.Position.Z), info.Width, info.MissileSpeed, info.Range, info.CollisionCount);
                        }
                     }

                    else if (info.SpellType == SpellInfo.SpellTypeInfo.LinearDash)
                        Geometry.DrawLinearSkillshot(info.caster.Position, info.endPosition, info.Width, info.MissileSpeed, info.Range, info.CollisionCount);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.TargetedMissile)
                    {
                        Geometry.DrawTargetedSpell(info.missile.Position, info.target);
                    }
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.TargetedDash)
                    {
                        //lee sin does not have a target set. the target set is the one hovered over.
                        if (info.DashType == SpellInfo.Dashtype.Targeted && info.target != null)
                            Geometry.DrawTargetedSpell(info.caster.Position, info.target);
                    }
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot)
                        Geometry.DrawCircularSkillshot(info.missile.EndPosition, info.Radius, info.SecondRadius);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.CircularSpell || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpellWithBuff || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpellWithDuration)
                        Geometry.DrawCircularSkillshot(info.endPosition, info.Radius, info.SecondRadius);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSpell)
                        Geometry.DrawConeSkillshot(info.startPosition, info.endPosition, info.ConeDegrees, info.Range);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSpellWithBuff)
                        Geometry.DrawConeSkillshot(info.caster.Position, info.caster.Position.Extend(info.caster.Position + info.caster.Direction, info.Range).To3D((int)info.caster.Position.Z), info.ConeDegrees, info.Range);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.SelfActive || info.SpellType == SpellInfo.SpellTypeInfo.SelfActiveWithBuff)
                        Geometry.DrawCircularSkillshot(info.caster.Position, info.Radius, info.SecondRadius);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.Wall)
                        Geometry.DrawWall(info.startPosition, info.endPosition, info.Width, info.Radius);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.CircularWall)
                        Geometry.DrawCircularWall(info.endPosition, info.Radius, info.SecondRadius);
                }
            }
        }

        private static void HandleBasicAttacks(SpellInfo info, List<SpellInfo> KeepList)
        {
            if (info.SpellType == SpellInfo.SpellTypeInfo.AutoAttack)
            {
                if (info.missile != null && info.missile.StartPosition != Vector3.Zero
                    && info.missile.EndPosition != Vector3.Zero && info.missile.Name != null
                    && info.missile.EndPosition.Distance(info.missile) <= info.caster.GetAutoAttackRange() * 2)
                    KeepList.Add(info);

                //Console.WriteLine(info.missile.Position + "|" + info.missile.EndPosition);
            }
        }

        private static void DrawParticles()
        {
            foreach (Obj_GeneralParticleEmitter particle in ObjectManager.Get<Obj_GeneralParticleEmitter>())
            {
                ParticleInfo info = ParticleDatabase.GetParticleInfo(particle.Name);

                if (info != null)
                {
                    if (info.CreationTime == -1f)
                        info.CreationTime = Game.Time;

                    if (info.SpellType == ParticleInfo.SpellTypeInfo.Wall)
                        Geometry.DrawRectangle(info.Length, info.Width, particle.Position, info.XOffset, info.YOffset);
                    else if (info.SpellType == ParticleInfo.SpellTypeInfo.CircularSkillshot)
                        Geometry.DrawCircularSkillshot(particle.Position, info.Radius);
                    else if (info.SpellType == ParticleInfo.SpellTypeInfo.LinearSkillshot)
                    {
                        if (info.ParticleName == "Illaoi_Base_Q_IndicatorBLU.troy")
                        {
                            Obj_AI_Base tentacle = ObjectManager.Get<Obj_AI_Base>().Where(a => a.Name == "God" && a.Position.Distance(particle.Position) <= 10).OrderBy(a => a.Distance(particle)).FirstOrDefault();

                            if (tentacle != null && (tentacle.BaseSkinName == "Illaoi" || info.CanDraw()))
                                Geometry.DrawLinearSkillshot(tentacle.Position, tentacle.Position.Extend(tentacle.Position + tentacle.Direction, info.Length).To3D((int)tentacle.Position.Z), info.Width, 500, info.Length, 0);
                        }
                        else
                            Geometry.DrawRectangle(info.Length, info.Width, particle.Position, info.XOffset, info.YOffset);
                    }
                }
            }
        }

        private static void DrawTraps()
        {
            foreach (Obj_AI_Minion trap in ObjectManager.Get<Obj_AI_Minion>().Where(a => a != null && (a.IsEnemy || MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Draw, "Draw Friendly Projectiles")) && !a.IsDead && TrapDatabase.AllTrapNames().Contains(a.Name)))
            {
                Drawing.DrawCircle(trap.Position, TrapDatabase.getTrap(trap.Name).Radius, Geometry.drawColor);
            }
        }

        private static void DrawIllaoiTentacles()
        {
            foreach (Obj_AI_Minion tentacle in ObjectManager.Get<Obj_AI_Minion>().Where(a => a.Name == "God"))
            {
                Drawing.DrawCircle(tentacle.Position, 50, Geometry.drawColor);
            }
        }

        private static void DrawObjectNames()
        {
            if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Debug, "Show All Object Names"))
            {
                int index = 0;
                foreach (GameObject ob in ObjectManager.Get<GameObject>().Where(a => a.Position.Distance(Game.CursorPos) <= 500))
                {
                    Drawing.DrawText(ob.Position.WorldToScreen() + (new Vector2(0, -15f) * index), Geometry.drawColor, ob.Name, 15);
                    index++; ;
                }
            }
        }
        
        private static void DrawDirection()
        {
            if (MenuHandler.GetCheckboxValue(MenuHandler.MenuType.Draw, "Draw Player Direction"))
                Drawing.DrawText(_Player.Position.WorldToScreen(), Geometry.drawColor, _Player.Direction.ToString(), 15);
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (_Player.IsDead)
                return;

            DodgeManager.HandleDodging();
        }
    }
}
