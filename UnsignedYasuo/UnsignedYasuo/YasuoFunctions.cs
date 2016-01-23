using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace UnsignedYasuo
{
    class YasuoFunctions
    {
        public static bool dashing;

        public enum AttackSpell
        {
            Q,
            E,
            EQ,
            Ignite
        };

        //get enemy non last hit)
        public static Obj_AI_Base GetEnemy(float range, GameObjectType type)
        {
            return ObjectManager.Get<Obj_AI_Base>().Where(a => a.IsEnemy
            && a.Type == type
            && a.Distance(_Player) <= range
            && !a.IsDead
            && !a.IsInvulnerable
            && a.IsValidTarget(range)).FirstOrDefault();
        }

        public static Obj_AI_Base GetEnemy(GameObjectType type, AttackSpell spell)
        {
            float range = 0;
            if (spell == AttackSpell.E)
                range = Program.E.Range;
            if (spell == AttackSpell.Q)
                range = Program.Q.Range;
            if (spell == AttackSpell.EQ)
                range = Program.E.Range;
            if (spell == AttackSpell.Ignite)
                range = Program.Ignite.Range;

            return ObjectManager.Get<Obj_AI_Base>().Where(a => a.IsEnemy
                && a.Type == type
                && a.Distance(_Player) <= Program.E.Range
                && !a.IsDead
                && !a.IsInvulnerable
                && a.IsValidTarget(Program.E.Range)
                && !a.HasBuff("YasuoDashWrapper")
                &&
                ((spell == AttackSpell.E && a.Health <= YasuoCalcs.E(a)) ||
                (spell == AttackSpell.Q && a.Health <= YasuoCalcs.Q(a)) ||
                (spell == AttackSpell.EQ && a.Health <= (YasuoCalcs.Q(a) + YasuoCalcs.E(a))) ||
                (spell == AttackSpell.Ignite && a.Health <= YasuoCalcs.Ignite(a)))).FirstOrDefault();
        }

        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
        
        public static void LastHit()
        {
            bool QCHECK = Program.LastHit["LHQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.LastHit["LHE"].Cast<CheckBox>().CurrentValue;
            bool EQCHECK = Program.LastHit["LHEQ"].Cast<CheckBox>().CurrentValue;
            bool EUNDERTURRET = Program.LastHit["LHEUT"].Cast<CheckBox>().CurrentValue;

            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();
            
            if (ECHECK && EREADY)
            {
                Obj_AI_Minion target = null;
                Obj_AI_Turret closestTurret = null;
                foreach (Obj_AI_Minion minion in ObjectManager.Get<Obj_AI_Minion>())
                {
                    if (minion.IsEnemy
                       && minion.Distance(_Player) <= Program.E.Range
                       && !minion.IsDead
                       && minion.IsValidTarget()
                       && minion.Health + 10 <= YasuoCalcs.E(minion)
                       && !minion.HasBuff("YasuoDashWrapper"))
                    {
                        foreach (Obj_AI_Turret tur in ObjectManager.Get<Obj_AI_Turret>())
                        {
                            if ((closestTurret == null ||
                                tur.Distance(minion) < closestTurret.Distance(minion)) && tur.IsEnemy)
                                closestTurret = tur;
                        }

                        target = minion;
                        break;
                    }
                }

                if (target != null)
                {
                    if (EUNDERTURRET)
                    {
                        Program.E.Cast(target);
                        dashing = true;
                        if (EQCHECK && QREADY && !target.IsDead && target.Health <= YasuoCalcs.E(target))
                            Program.Q.Cast(target.Position);

                    }
                    else if (!EUNDERTURRET && closestTurret != null)
                    {
                        if (Extensions.Distance(closestTurret, YasuoCalcs.GetDashingEnd(target)) > 900
                            || closestTurret == null)
                        {
                            Program.E.Cast(target);
                            dashing = true;
                            if (EQCHECK && QREADY && !target.IsDead && target.Health <= YasuoCalcs.E(target))
                                Program.Q.Cast(target.Position);
                        }
                    }
                }
            }

            if (QCHECK && QREADY && !dashing && !_Player.IsDashing())
            {
                Obj_AI_Minion minion = (Obj_AI_Minion)GetEnemy(GameObjectType.obj_AI_Minion, AttackSpell.Q);

                if (minion != null)
                    Program.Q.Cast(minion.Position);
            }
            dashing = false;
        }

        public static void LaneClear()
        {
            bool QCHECK = Program.LaneClear["LCQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.LaneClear["LCE"].Cast<CheckBox>().CurrentValue;
            bool EQCHECK = Program.LaneClear["LCEQ"].Cast<CheckBox>().CurrentValue;
            bool EUNDERTURRET = Program.LaneClear["LCEUT"].Cast<CheckBox>().CurrentValue;

            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();
        
            if (ECHECK && EREADY)
            {
                Obj_AI_Minion target = null;
                Obj_AI_Turret closestTurret = null;
                foreach (Obj_AI_Minion minion in ObjectManager.Get<Obj_AI_Minion>())
                {
                    if (minion.IsEnemy
                       && minion.Distance(_Player) <= Program.E.Range
                       && !minion.IsDead
                       && minion.IsValidTarget()
                       && !minion.HasBuff("YasuoDashWrapper"))
                    {
                        foreach(Obj_AI_Turret tur in ObjectManager.Get<Obj_AI_Turret>())
                        {
                            if ((closestTurret == null || tur.Distance(minion) < closestTurret.Distance(minion))
                                && tur.IsEnemy)
                                closestTurret = tur;
                        }

                        target = minion;
                        break;
                    }
                }

                if (target != null && !target.IsDead && !target.IsInvulnerable)
                {
                    if (EUNDERTURRET)
                    {
                        Program.E.Cast(target);
                        dashing = true;
                        if (EQCHECK && QREADY && !target.IsDead)
                            Program.Q.Cast(target.Position);
                        
                    }
                    else if(!EUNDERTURRET)
                    {
                        if(Extensions.Distance(closestTurret, YasuoCalcs.GetDashingEnd(target)) > 900 
                            || closestTurret == null)
                        {
                            Program.E.Cast(target);
                            dashing = true;
                            if (EQCHECK && QREADY && !target.IsDead)
                                Program.Q.Cast(target.Position);
                        }
                    }
                    //UseItemsAndIgnite(target);
                }
            }

            if (QCHECK && QREADY && !_Player.IsDashing() && !dashing)
            {
                Obj_AI_Minion minion = (Obj_AI_Minion)GetEnemy(Program.Q.Range, GameObjectType.obj_AI_Minion);

                if (minion != null)
                    Program.Q.Cast(minion.Position);
            }

            if (Program.LaneClear["LCI"].Cast<CheckBox>().CurrentValue)
            {
                Obj_AI_Minion minion = (Obj_AI_Minion)GetEnemy(400, GameObjectType.obj_AI_Minion);

                if (minion != null)
                    UseItemsAndIgnite(minion);
            }

            dashing = false;
        }

        public static void KS()
        {
            foreach (AIHeroClient target in HeroManager.Enemies.Where(hero => hero != null && hero.Health <= YasuoCalcs.Q(hero)))
            if (Program.KSMenu["KSQ"].Cast<CheckBox>().CurrentValue 
                    && Program.Q.IsReady() && !target.IsDead && Program.Q.Range != 1000)
            {
                Program.Q.Cast(target.Position);
            }

            foreach (AIHeroClient target in HeroManager.Enemies.Where(hero => hero != null && hero.Health <= YasuoCalcs.Q(hero)))
            if (Program.KSMenu["KS3Q"].Cast<CheckBox>().CurrentValue 
                    && Program.Q.IsReady() && !target.IsDead && Program.Q.Range == 1000)
            {
                Program.Q.Cast(target.Position);
            }

            foreach (AIHeroClient target in HeroManager.Enemies.Where(hero => hero != null && hero.Health <= YasuoCalcs.E(hero)))
            if (Program.KSMenu["KSE"].Cast<CheckBox>().CurrentValue
                    && Program.E.IsReady() && !target.IsDead && !target.HasBuff("YasuoDashWrapper"))
            {
                Program.E.Cast(target);
            }

            foreach (AIHeroClient target in HeroManager.Enemies.Where(hero => hero != null && hero.Health <= (YasuoCalcs.E(hero) + YasuoCalcs.Q(hero))))
            if (Program.KSMenu["KSEQ"].Cast<CheckBox>().CurrentValue 
                    && Program.E.IsReady() && !target.IsDead && !target.HasBuff("YasuoDashWrapper"))
            {
                Program.E.Cast(target);
                Program.Q.Cast(target.Position);
            }

            var igniteEnemy = GetEnemy(GameObjectType.AIHeroClient, AttackSpell.Ignite);
            if (Program.Ignite != null && igniteEnemy != null)//get correct value
            {
                Program.Ignite.Cast(igniteEnemy);
            }
        }
        
        public static void Harrass()
        {
            bool QCHECK = Program.Harass["HQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.Harass["HE"].Cast<CheckBox>().CurrentValue;
            bool EQCHECK = Program.Harass["HEQ"].Cast<CheckBox>().CurrentValue;

            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();

            if (ECHECK && EREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.E.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                {
                    Program.E.Cast(enemy);
                    dashing = true;
                    if (EQCHECK && QREADY)
                        Program.Q.Cast(enemy.Position);
                }
            }

            if (QCHECK && QREADY && !dashing && !_Player.IsDashing())
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    Program.Q.Cast(enemy.Position);
            }
            dashing = false;
        }

        public static void Combo()
        {
            bool QCHECK = Program.ComboMenu["QU"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.ComboMenu["EU"].Cast<CheckBox>().CurrentValue;
            bool EQCHECK = Program.ComboMenu["RU"].Cast<CheckBox>().CurrentValue;

            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();
            bool RREADY = Program.R.IsReady();


            int enemiesKU = 0;
            int enemiesIR = 0;
            List<AIHeroClient> enemies = HeroManager.Enemies;
            foreach (AIHeroClient enemy in enemies)
            {
                if (_Player.Distance(enemy) <= 1200)
                {
                    enemiesIR++;
                    if (enemy.HasBuffOfType(BuffType.Knockup))
                        enemiesKU++;
                }
            }

            if(enemiesKU >= 3)
                Program.R.Cast();

            if (ECHECK && EREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.E.Range, GameObjectType.AIHeroClient);

                if (enemy != null
                    && Extensions.Distance(YasuoCalcs.GetDashingEnd(enemy), enemy) <= _Player.GetAutoAttackRange()//wont e unless in AA range after
                    && enemy.Distance(_Player) <= Program.E.Range
                    && enemy.Distance(_Player) >= _Player.GetAutoAttackRange())
                {
                    Program.E.Cast(enemy);
                    dashing = true;
                    if (EQCHECK && QREADY)
                        Program.Q.Cast(enemy.Position);
                }
                else if (enemy != null
                    && enemy.Distance(_Player) > Program.E.Range)//use minions to get to champ
                {
                    Obj_AI_Minion furthestMinion = null;
                    foreach (Obj_AI_Minion minion in ObjectManager.Get<Obj_AI_Minion>())
                    {
                        if (furthestMinion == null
                            && Extensions.Distance(YasuoCalcs.GetDashingEnd(minion), enemy) <= _Player.Distance(enemy)
                            && minion.IsEnemy
                            && !minion.HasBuff("YasuoDashWrapper")
                            && minion.Distance(_Player) <= Program.E.Range)
                            furthestMinion = minion;
                        else if (_Player.IsFacing(minion)
                            && Extensions.Distance(YasuoCalcs.GetDashingEnd(minion), enemy) <= _Player.Distance(enemy)
                            && minion.IsEnemy
                            && !minion.HasBuff("YasuoDashWrapper")
                            && minion.Distance(_Player) <= Program.E.Range)
                            furthestMinion = minion;
                    }

                    if (furthestMinion != null)
                        Program.E.Cast(furthestMinion);
                }
            }

            if (QCHECK && QREADY && !dashing && !_Player.IsDashing())
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                {
                    if (Program.Q.Range == 1000 && Program.Q.GetPrediction(enemy).HitChance >= HitChance.Medium)
                        Program.Q.Cast(enemy.Position);
                    else if (Program.Q.Range == 475)
                        Program.Q.Cast(enemy.Position);
                }
            }

            if (Program.ComboMenu["IU"].Cast<CheckBox>().CurrentValue == true)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(2500, GameObjectType.AIHeroClient);

                if (enemy != null)
                    UseItemsAndIgnite(enemy);
            }

            dashing = false;
        }

        public static void Flee()
        {
            Obj_AI_Minion furthestMinion = null;
            foreach(Obj_AI_Minion minion in ObjectManager.Get<Obj_AI_Minion>())
            {
                if (furthestMinion == null &&
                    _Player.IsFacing(minion) &&
                    minion.IsEnemy &&
                    minion.Distance(_Player) <= Program.E.Range)
                    furthestMinion = minion;

                if(_Player.IsFacing(minion) &&
                    minion.IsEnemy &&
                    minion.Distance(_Player) <= Program.E.Range &&
                    minion.Distance(_Player) > furthestMinion.Distance(_Player))
                    furthestMinion = minion;
            }

            if(furthestMinion != null && Program.E.IsReady())
                Program.E.Cast(furthestMinion);
        }

        public static void UseItemsAndIgnite(Obj_AI_Base unit)
        {
            if (unit == null)
                return;

            InventorySlot[] items = _Player.InventoryItems;

            foreach(InventorySlot item in items)
            {
                if(item.CanUseItem())
                {
                    if ((item.Id == ItemId.Blade_of_the_Ruined_King || item.Id == ItemId.Bilgewater_Cutlass)
                        && _Player.Distance(unit) <= 550
                        && unit.Type == GameObjectType.AIHeroClient
                        && unit.IsEnemy)
                        item.Cast(unit);

                    if ((item.Id == ItemId.Ravenous_Hydra_Melee_Only || item.Id == ItemId.Tiamat_Melee_Only)
                        && (unit.Type == GameObjectType.AIHeroClient || unit.Type == GameObjectType.obj_AI_Minion)
                        && _Player.Distance(unit) <= 400
                        && unit.IsEnemy)
                        item.Cast();

                    if (item.Id == ItemId.Youmuus_Ghostblade
                        && unit.Type == GameObjectType.AIHeroClient
                        && unit.IsEnemy)
                        item.Cast();
                }
            }
        }

        public static Spell.Skillshot GetQType()
        {
            if (_Player.HasBuff("yasuoq3w"))
                return new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear);
            else
                return new Spell.Skillshot(SpellSlot.Q, 475, SkillShotType.Linear);
        }
    }
}
