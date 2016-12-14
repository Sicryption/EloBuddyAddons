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

namespace UnsignedEvade
{
    class SpellCreation
    {
        //After Cast Time
        public static void AIHeroClient_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            //HandleSpellCast(sender, args, SpellInfo.SpellCreationLocation.OnSpellCast);
            //actually canceling of spells if this isnt casted
        }

        //Before Cast Time
        public static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            HandleSpellCast(sender, args, SpellInfo.SpellCreationLocation.OnProcessSpell);
        }

        //Add Spells to Active Spell List
        public static void HandleSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args, SpellInfo.SpellCreationLocation location)
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
                    && info.ShouldBeAccountedFor()
                    && (location == SpellInfo.SpellCreationLocation.OnProcessSpell || info.TravelTime == -1f)
                    && info.SpellName != "")
                {
                    SpellInfo newSpellInstance = SpellDatabase.CreateInstancedSpellInfo(info);

                    newSpellInstance.startPosition = new Vector3(args.Start.X, args.Start.Y, NavMesh.GetHeightForPosition(args.Start.X, args.Start.Y));

                    //this needs to be here to let Lee Q override it.
                    newSpellInstance.target = args.Target;

                    if ((!info.CanVaryInLength || args.Start.Distance(args.End) >= info.Range)
                        && (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot || info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshotDash || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpell || info.SpellType == SpellInfo.SpellTypeInfo.LinearDash || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpellWithDuration))
                        newSpellInstance.endPosition = CalculateEndPosition(args.Start, args.End, info.Range);
                    else if (info.DashType == SpellInfo.Dashtype.TargetedLinear && info.target != null)
                        newSpellInstance.endPosition = info.target.Position;
                    else if (info.DashType == SpellInfo.Dashtype.Targeted && info.SpellName == "BlindMonkQTwo")
                        newSpellInstance.target = ParticleDatabase.LeeSinQTargets.Where(a => a.Item1 == (AIHeroClient)sender).FirstOrDefault().Item2;
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.ArcSkillshot)
                    {
                        if (args.Start.Distance(args.End) >= info.Range)
                            newSpellInstance.endPosition = CalculateEndPosition(args.Start, args.End, info.Range);
                        else
                            newSpellInstance.endPosition = args.End.To2D().To3D((int)NavMesh.GetHeightForPosition(args.End.X, args.End.Y));
                    }
                    else if (info.DashType == SpellInfo.Dashtype.FixedDistance)
                        newSpellInstance.endPosition = info.caster.Position.Extend(args.End, info.Range).To3D((int)info.caster.Position.Z);
                    else
                        newSpellInstance.endPosition = args.End.To2D().To3D((int)NavMesh.GetHeightForPosition(args.End.X, args.End.Y));

                    ExtraSpellOverides.OnSpellCreation(sender, args, info, ref newSpellInstance);

                    newSpellInstance.MissileName = "";
                    newSpellInstance.caster = sender;
                    newSpellInstance.CreationLocation = location;
                    newSpellInstance.TimeOfCast = Game.Time;

                    if (newSpellInstance.GetChampionSpell() != null && newSpellInstance.GetChampionSpell().SData.MaxAmmo != -1)
                        newSpellInstance.startingAmmoCount = newSpellInstance.GetChampionSpell().Ammo;

                    //Console.WriteLine("Added Spell " + newSpellInstance.SpellName + " - " + Game.Time);
                    SpellDatabase.activeSpells.Add(newSpellInstance);
                }
            }
            #endregion

            #region Print Out Spell Information
            if ((!MenuHandler.DebugMenu.GetCheckboxValue("Show only Enemy Spells/Missiles") || sender.IsEnemy)
                && spells.Count == 0)
            {
                //spells
                if (MenuHandler.DebugMenu.GetCheckboxValue("Debug Spell/Missile Creation"))
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

                    if (!SpellDatabase.SpellsNotInDatabase.Contains(debugText))
                    {
                        SpellDatabase.SpellsNotInDatabase.Add(debugText);
                        Console.WriteLine(debugText);
                    }
                }
            }
            #endregion
        }

        //Adds Missiles to Active Spell List
        public static void GameObject_OnCreate(GameObject sender, EventArgs args)
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
                if (info != null
                    && info.ShouldBeAccountedFor())
                {
                    SpellInfo newSpellInstance = SpellDatabase.CreateInstancedSpellInfo(info);

                    //dont draw a spell if its missile was created  
                    if (SpellDatabase.activeSpells.ContainsSpellName(info.SpellName, true) && info.SpellType != SpellInfo.SpellTypeInfo.ArcSkillshot)
                        SpellDatabase.activeSpells.Remove(SpellDatabase.activeSpells.GetSpellFromSpellName(info.SpellName, true));
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.ArcSkillshot)
                    {
                        //diana Q
                        if (info.MissileName.Contains("Diana"))
                        {
                            SpellInfo DianaQSpellCast = SpellDatabase.activeSpells.Where(a => a.SpellName == "DianaArc" && projectile.SpellCaster.Name == a.caster.Name).FirstOrDefault();
                            if (DianaQSpellCast != null)
                                newSpellInstance.endPosition = DianaQSpellCast.endPosition;
                            if (info.MissileName.Contains("Outer"))
                                SpellDatabase.activeSpells.Remove(SpellDatabase.activeSpells.GetSpellFromSpellName("DianaArc", true));
                        }
                    }

                    if (info.SpellType == SpellInfo.SpellTypeInfo.ArcSkillshot)
                        newSpellInstance.startPosition = sender.Position;
                    else
                        newSpellInstance.startPosition = projectile.StartPosition;

                    if ((!info.CanVaryInLength || projectile.StartPosition.Distance(projectile.EndPosition) >= info.Range) && info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot)
                        newSpellInstance.endPosition = CalculateEndPosition(projectile.StartPosition, projectile.EndPosition, info.Range);
                    //spells that dont hav to be extended. The only exception is Dianas Q which was taken care of above
                    else if (info.SpellType != SpellInfo.SpellTypeInfo.ArcSkillshot)
                        newSpellInstance.endPosition = projectile.EndPosition;

                    //overrides
                    if (info.MissileName == "GravesQReturn")
                        newSpellInstance.endPosition = ParticleDatabase.GravesQRewind.Where(a => a.Item1 == (AIHeroClient)projectile.SpellCaster).FirstOrDefault().Item2;
                    
                    newSpellInstance.target = projectile.Target;
                    newSpellInstance.caster = projectile.SpellCaster;
                    newSpellInstance.missile = projectile;
                    newSpellInstance.CreationLocation = SpellInfo.SpellCreationLocation.OnObjectCreate;
                    newSpellInstance.TimeOfCast = Game.Time;

                    //Console.WriteLine("Added Missile " + newSpellInstance.MissileName + " - " + Game.Time);

                    if (!SpellDatabase.activeSpells.Contains(newSpellInstance))
                        SpellDatabase.activeSpells.Add(newSpellInstance);

                }
                else
                {
                    #region Print Out Spell Information
                    if ((!MenuHandler.DebugMenu.GetCheckboxValue("Show only Enemy Spells/Missiles")
                            || projectile.SpellCaster.IsEnemy))
                        if (MenuHandler.DebugMenu.GetCheckboxValue("Debug Spell/Missile Creation"))
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

                            if (!SpellDatabase.SpellsNotInDatabase.Contains(debugText))
                            {
                                SpellDatabase.SpellsNotInDatabase.Add(debugText);
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
                    DrawingManager.HandleParticle(sender);
            }
        }

        public static Vector3 CalculateEndPosition(Vector3 position, Vector3 endPosition, float range)
        {
            return Extensions.Extend(position, endPosition, range).To3D((int)position.Z);
        }
    }
}