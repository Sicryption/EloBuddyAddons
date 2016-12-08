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
    class DrawingManager
    {
        public static AIHeroClient _Player => Player.Instance;
        public static System.Drawing.Color drawColor = System.Drawing.Color.Blue;
        public static void Drawing_OnDraw(EventArgs args)
        {
            //Spells should be refreshing even when dead
            RefreshSpellList();

            //passive spells should be refreshing even when dead
            Utilities.ResetPassiveSpellCounter();

            if (_Player.IsDead || !MenuHandler.DrawMenu.GetCheckboxValue("Draw Spells/Missiles"))
                return;

            try
            {
                //draws player direction if checked in debug menu
                DrawDirection();

                //draws object names if checked in debug menu
                DrawObjectNames();

                //Create Polygons for Every Spell
                CreatePolygons();

                //method that calls drawing method for each polygon
                DrawSpells();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void CreatePolygons()
        {
            //reset polygon list to have no entities
            SpellDatabase.Polygons = new List<CustomPolygon>();

            #region Spells
            foreach(SpellInfo info in SpellDatabase.activeSpells)
            {
                if (info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpell 
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpellWithBuff 
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpellWithDuration
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshotDash)
                {
                    if (info.missile != null)
                        SpellDatabase.Polygons.Add(PolygonCreater.CreateCircularSkillshot(info, info.missile.EndPosition, info.Radius));
                    else
                        SpellDatabase.Polygons.Add(PolygonCreater.CreateCircularSkillshot(info, info.endPosition, info.Radius));
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.SelfActive || info.SpellType == SpellInfo.SpellTypeInfo.SelfActiveWithBuff)
                    SpellDatabase.Polygons.Add(PolygonCreater.CreateCircularSkillshot(info, info.caster.Position, info.Radius));
                else if (info.SpellType == SpellInfo.SpellTypeInfo.CircularWall)
                    SpellDatabase.Polygons.Add(PolygonCreater.CreateCircularWall(info, info.endPosition, info.Radius, info.SecondRadius));
                else if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSpellWithBuff
                    || info.SpellType == SpellInfo.SpellTypeInfo.LinearSpellWithDuration)
                    SpellDatabase.Polygons.Add(PolygonCreater.CreateLinearSkillshot(info, info.caster.Position, info.caster.Position.Extend(info.caster.Position + info.caster.Direction, info.Range).To3D((int)info.caster.Position.Z), info.Width));
                else if (info.SpellType == SpellInfo.SpellTypeInfo.LinearDash)
                    SpellDatabase.Polygons.Add(PolygonCreater.CreateLinearSkillshot(info, info.caster.Position, info.endPosition, info.Width));
                else if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot
                    || info.SpellType == SpellInfo.SpellTypeInfo.LinearMissile
                    || info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshotNoDamage)
                {
                    if (info.missile != null)
                    {
                        //sivir Q and draven R return 
                        //Graves Q Does not work likes this
                        if ((info.MissileName.ToLower().Contains("return") && info.MissileName != "GravesQReturn") ||
                            ((info.MissileName == "DravenR" || info.MissileName == "TalonWMissileTwo" || info.MissileName == "TalonRMisTwo")
                            && info.missile.EndPosition.Distance(info.caster.Position) <= 50))
                        {
                            SpellDatabase.Polygons.Add(PolygonCreater.CreateLinearSkillshot(info, info.missile.Position, info.missile.SpellCaster.Position, info.Width));
                        }
                        //ahri rotating w orbs
                        else if (info.MissileName == "AhriFoxFireMissile" || info.MissileName == "AhriFoxFireMissileTwo")
                        {
                            if (info.missile.Position.Distance(info.caster.Position) > 200)
                                SpellDatabase.Polygons.Add(PolygonCreater.CreateLinearSkillshot(info, info.missile.Position, info.endPosition, info.Width));
                        }
                        else
                            SpellDatabase.Polygons.Add(PolygonCreater.CreateLinearSkillshot(info, info.missile.Position, info.endPosition, info.Width));
                    }
                    //for on spell cast spells that dont have their missiles created yet.
                    else if (info.MissileName == "" && info.BuffName == "")
                        SpellDatabase.Polygons.Add(PolygonCreater.CreateLinearSkillshot(info, info.caster.Position, info.endPosition, info.Width));
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.TargetedMissile || info.SpellType == SpellInfo.SpellTypeInfo.AutoAttack)
                {
                    if (info.missile != null)
                        SpellDatabase.Polygons.Add(PolygonCreater.CreateTargetedSpell(info, info.missile.Position, info.target));
                    else
                        SpellDatabase.Polygons.Add(PolygonCreater.CreateTargetedSpell(info, info.caster.Position, info.target));
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.TargetedDash)
                {
                    if (info.DashType == SpellInfo.Dashtype.Targeted && info.target != null)
                        SpellDatabase.Polygons.Add(PolygonCreater.CreateTargetedSpell(info, info.caster.Position, info.target));
                    else if (info.DashType == SpellInfo.Dashtype.TargetedLinear && info.target != null)
                        SpellDatabase.Polygons.Add(PolygonCreater.CreateTargetedSpell(info, info.caster.Position, info.endPosition));
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSpell)
                    SpellDatabase.Polygons.Add(PolygonCreater.CreateCone(info, info.startPosition, info.endPosition, info.ConeDegrees, info.Range));
                else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSpellWithBuff)
                    SpellDatabase.Polygons.Add(PolygonCreater.CreateCone(info, info.caster.Position, info.caster.Position.Extend(info.caster.Position + info.caster.Direction, info.Range).To3D((int)info.caster.Position.Z), info.ConeDegrees, info.Range));
                else if (info.SpellType == SpellInfo.SpellTypeInfo.Wall)
                    SpellDatabase.Polygons.Add(PolygonCreater.CreateWall(info, info.startPosition, info.endPosition, info.Width, info.Radius));
                else if (info.SpellType == SpellInfo.SpellTypeInfo.ArcSkillshot)
                    SpellDatabase.Polygons.Add(PolygonCreater.CreateArc(info, info.startPosition, info.endPosition, info.Width));
            }
            #endregion

            #region Particles
            foreach (Obj_GeneralParticleEmitter particle in ObjectManager.Get<Obj_GeneralParticleEmitter>())
            {
                ParticleInfo info = ParticleDatabase.GetParticleInfo(particle.Name);

                if (info != null)
                {
                    if (info.CreationTime == -1f)
                        info.CreationTime = Game.Time;

                    if (info.SpellType == ParticleInfo.SpellTypeInfo.Wall)
                        SpellDatabase.Polygons.Add(PolygonCreater.CreateRectangleAroundPoint(info.Length, info.Width, particle.Position, info.XOffset, info.YOffset));
                    else if (info.SpellType == ParticleInfo.SpellTypeInfo.CircularSkillshot)
                        SpellDatabase.Polygons.Add(PolygonCreater.CreateCircularSkillshot(null, particle.Position, info.Radius));
                    else if (info.SpellType == ParticleInfo.SpellTypeInfo.LinearSkillshot)
                    {
                        if (info.ParticleName == "Illaoi_Base_Q_IndicatorBLU.troy")
                        {
                            Obj_AI_Base tentacle = ObjectManager.Get<Obj_AI_Base>().Where(a => a.Name == "God" && a.Position.Distance(particle.Position) <= 10).OrderBy(a => a.Distance(particle)).FirstOrDefault();

                            if (tentacle != null && (tentacle.BaseSkinName == "Illaoi" || info.CanDraw()))
                                SpellDatabase.Polygons.Add(PolygonCreater.CreateLinearSkillshot(null, tentacle.Position, tentacle.Position.Extend(tentacle.Position + tentacle.Direction, info.Length).To3D((int)tentacle.Position.Z), info.Width));
                        }
                        else
                            SpellDatabase.Polygons.Add(PolygonCreater.CreateRectangleAroundPoint(info.Length, info.Width, particle.Position, info.XOffset, info.YOffset));
                    }
                }
            }
            #endregion

            #region Traps
            foreach (Obj_AI_Minion trap in ObjectManager.Get<Obj_AI_Minion>().Where(a => a != null && (a.IsEnemy || MenuHandler.DrawMenu.GetCheckboxValue("Draw Friendly Spells/Missiles")) && !a.IsDead && TrapDatabase.AllTrapNames().Contains(a.Name)))
                SpellDatabase.Polygons.Add(PolygonCreater.CreateCircularSkillshot(null, trap.Position, TrapDatabase.getTrap(trap.Name).Radius));
            #endregion

            #region Singed Poisons
            List<ParticleInfo> replacementPoisonList = new List<ParticleInfo>();
            foreach (ParticleInfo info in ParticleDatabase.SingedPoisonTrails)
            {
                if (Game.Time - (info.CreationTime + info.Delay) <= 0)
                {
                    SpellDatabase.Polygons.Add(PolygonCreater.CreateCircularSkillshot(null, info.Position, info.Radius));
                    replacementPoisonList.Add(info);
                }
            }
            ParticleDatabase.SingedPoisonTrails = replacementPoisonList;
            #endregion
        }

        private static void RefreshSpellList()
        {
            //when removing object from below add it to this list then cross reference in another method and reset.
            List<SpellInfo> KeepList = new List<SpellInfo>();

            foreach (SpellInfo info in SpellDatabase.activeSpells)
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

            ExtraSpellOverides.OnRefreshSpellList();
            SpellDatabase.activeSpells = KeepList;
        }

        private static void HandleSpells(SpellInfo info, List<SpellInfo> KeepList)
        {
            //we check if the spell is off cooldown because that is when the spell shouldn't be drawing, and the missile should be.
            try
            {
                //if is dash and dashtype is linear
                if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot
                    || info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshotNoDamage
                    || info.SpellType == SpellInfo.SpellTypeInfo.LinearMissile
                    || info.SpellType == SpellInfo.SpellTypeInfo.Wall
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularWall
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshot
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpell
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSpellWithDuration
                    || info.SpellType == SpellInfo.SpellTypeInfo.SelfActive
                    || info.SpellType == SpellInfo.SpellTypeInfo.ConeSpell
                    || info.SpellType == SpellInfo.SpellTypeInfo.ArcSkillshot)
                {
                    float timeSinceCast = Game.Time - info.TimeOfCast;
                    float timeItTakesToCast = info.Delay + info.Duration + info.TravelTime;

                    if (info.SpellName.Contains("RekSaiQAttack"))
                    {
                        if (timeSinceCast <= timeItTakesToCast)
                            KeepList.Add(info);
                    }
                    else if (timeSinceCast <= timeItTakesToCast || info.IsOffCooldown())
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSpellWithDuration
                    || info.SpellType == SpellInfo.SpellTypeInfo.PassiveSpellWithDuration)
                {
                    if ((Game.Time - info.TimeOfCast <= info.Delay + info.Duration || info.IsOffCooldown()) && info.caster.IsFacing(info.endPosition))
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.TargetedMissile
                    || info.SpellType == SpellInfo.SpellTypeInfo.TargetedSpell
                    || info.SpellType == SpellInfo.SpellTypeInfo.TargetedSpellWithDuration)
                {
                    //targeted spells dont have missiles if they are cast on themseleves. IE: nami w
                    if (info.target != null && info.target != info.caster)
                        if (Game.Time - info.TimeOfCast <= info.Delay + info.Duration || info.IsOffCooldown())
                            KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.LinearDash
                    || info.SpellType == SpellInfo.SpellTypeInfo.TargetedDash
                    || info.SpellType == SpellInfo.SpellTypeInfo.CircularSkillshotDash)
                {
                    if (info.caster.IsDashing() || Game.Time - info.TimeOfCast <= info.Delay + info.Duration)
                        KeepList.Add(info);
                }
                else if (info.SpellType == SpellInfo.SpellTypeInfo.ConeSpellWithBuff
                    || info.SpellType == SpellInfo.SpellTypeInfo.LinearSpellWithBuff
                    || info.SpellType == SpellInfo.SpellTypeInfo.PassiveSpellWithBuff
                    || info.SpellType == SpellInfo.SpellTypeInfo.SelfActiveWithBuff
                    || info.SpellType == SpellInfo.SpellTypeInfo.TargetedPassiveSpell)
                {
                    float timeItTakesToCast = info.Delay + info.Duration + info.TravelTime;
                    float timeSinceCast = Game.Time - info.TimeOfCast;
                    if (info.caster.HasBuff(info.BuffName)
                        || timeSinceCast <= timeItTakesToCast
                        //this is so mf's ult isn't angled away from where it was casted
                        && (info.startingDirection == null || info.startingDirection == info.caster.Direction))
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
            //basic attacks are handled under HandleBasicAttacks
            try
            {
                if (info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshot || info.SpellType == SpellInfo.SpellTypeInfo.ArcSkillshot || info.SpellType == SpellInfo.SpellTypeInfo.LinearMissile || info.SpellType == SpellInfo.SpellTypeInfo.LinearSkillshotNoDamage)
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
            catch (Exception ex)
            {
                if (info.missile != null)
                    Console.WriteLine(info.ChampionName + "|" + info.SpellName + "|" + info.MissileName + "| MISSILE | HAS ERRORS");
                else
                    Console.WriteLine(info.ChampionName + "|" + info.SpellName + "|" + info.MissileName + "| SPELL | HAS ERRORS");
                Console.WriteLine(ex);
            }
        }

        public static void HandleParticle(GameObject ob)
        {
            Obj_GeneralParticleEmitter particle = ob as Obj_GeneralParticleEmitter;
            ParticleInfo info = ParticleDatabase.GetParticleInfo(ob.Name);
            if (info == null && MenuHandler.DebugMenu.GetCheckboxValue("Show Particles"))
                Console.WriteLine("\"" + ob.Name + "\",");
            else
            {
                //known particle
                //this is where the particle handler should be

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
            }
        }

        private static void DrawSpells()
        {
            int count = 0;
            foreach (SpellInfo info in SpellDatabase.activeSpells)
            {
                count++;

                if (info.caster.IsEnemy || MenuHandler.DrawMenu.GetCheckboxValue("Draw Friendly Spells/Missiles"))
                {
                    if (MenuHandler.DrawMenu.GetCheckboxValue("Draw Active Spells/Missiles"))
                    {
                        if (info.SpellName != "")
                            Drawing.DrawText(Vector2.Zero + new Vector2(0, 15 * count), drawColor, info.SpellName, 15);
                        else if (info.MissileName != "")
                            Drawing.DrawText(Vector2.Zero + new Vector2(0, 15 * count), drawColor, info.MissileName, 15);
                        else
                            Drawing.DrawText(Vector2.Zero + new Vector2(0, 15 * count), drawColor, "I shoudn't exist 0.0", 15);
                    }


                    if ((info.SpellType == SpellInfo.SpellTypeInfo.PassiveSpellWithBuff ||
                        info.SpellType == SpellInfo.SpellTypeInfo.PassiveSpellWithDuration ||
                        info.SpellType == SpellInfo.SpellTypeInfo.PassiveSpell)
                        && MenuHandler.DrawMenu.GetCheckboxValue("Draw Passive Spell Text"))
                        DrawPassiveSpell(info.caster, info);
                    else if (info.SpellType == SpellInfo.SpellTypeInfo.TargetedPassiveSpell
                        && MenuHandler.DrawMenu.GetCheckboxValue("Draw Passive Spell Text"))
                        DrawPassiveSpell(info.target as Obj_AI_Base, info);
                }
            }

            foreach (CustomPolygon polygon in SpellDatabase.Polygons)
                polygon.polygon.Draw(drawColor, 5);
        }
        
        private static void DrawPassiveSpell(Obj_AI_Base playerBeingDrawnOn, SpellInfo info)
        {
            int spellCount = SpellDatabase.championSpellsDrawnOnChampion.Where(a => a.Item1 == playerBeingDrawnOn.Name).FirstOrDefault().Item2;

            for (int i = 0; i < SpellDatabase.championSpellsDrawnOnChampion.Count; i++)
                if (SpellDatabase.championSpellsDrawnOnChampion[i].Item1 == playerBeingDrawnOn.Name)
                    SpellDatabase.championSpellsDrawnOnChampion[i] = new Tuple<string, int>(SpellDatabase.championSpellsDrawnOnChampion[i].Item1, SpellDatabase.championSpellsDrawnOnChampion[i].Item2 + 1);

            string name = info.SpellName;
            if (name == "")
                name = info.MissileName;

            Drawing.DrawText(info.target.Position.WorldToScreen() - new Vector2(0, 15 * spellCount), drawColor, name + " Buff", 15);
        }
        
        private static void DrawObjectNames()
        {
            if (MenuHandler.DebugMenu.GetCheckboxValue("Show All Object Names"))
            {
                int index = 0;
                foreach (GameObject ob in ObjectManager.Get<GameObject>().Where(a => a.Position.Distance(Game.CursorPos) <= 500))
                {
                    Drawing.DrawText(ob.Position.WorldToScreen() + (new Vector2(0, -15f) * index), drawColor, ob.Name, 15);
                    index++; ;
                }
            }
        }

        private static void DrawDirection()
        {
            if (MenuHandler.DebugMenu.GetCheckboxValue("Draw Player Direction"))
                Drawing.DrawText(_Player.Position.WorldToScreen(), drawColor, _Player.Direction.ToString(), 15);
        }
    }
}
