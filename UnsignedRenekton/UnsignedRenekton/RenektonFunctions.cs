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
using SharpDX;

namespace UnsignedRenekton
{
    class RenektonFunctions
    {
        static bool inFullCombo = false;
        static AIHeroClient comboEnemy;
        public static Vector3 beginningComboPosition = Vector3.Zero;
        public enum Abilities
        {
            Q,
            W,
            Slice,
            Dice,
            R,
            Ignite
        }
        public enum Mode
        {
            Combo,
            LaneClear,
            Harass
        }

        //renekton e buff name: renektonsliceanddicedelay
        public static AIHeroClient Renekton { get { return ObjectManager.Player; } }
        
        public static Obj_AI_Base GetEnemy(Abilities ability, GameObjectType type)
        {
            float range = 0;
            if (ability == Abilities.Q)
                range = Program.QRange;
            else if (ability == Abilities.W)
                range = Program.WRange;
            else if (ability == Abilities.Slice || ability == Abilities.Dice)
                range = Program.E.Range;

            return ObjectManager.Get<Obj_AI_Base>().OrderBy(a => a.Health).Where(a => a.IsEnemy
            && a.Type == type
            && a.Distance(Renekton) <= range
            && !a.IsDead
            && !a.IsInvulnerable
            && a.IsValidTarget(range)).FirstOrDefault();
        }
        public static Obj_AI_Base GetEnemy(float range, GameObjectType type)
        {
            return ObjectManager.Get<Obj_AI_Base>().OrderBy(a => a.Health).Where(a => a.IsEnemy
            && a.Type == type
            && a.Distance(Renekton) <= range
            && !a.IsDead
            && !a.IsInvulnerable
            && a.IsValidTarget(range)).FirstOrDefault();
        }
        public static Obj_AI_Base GetEnemyKS(Abilities ability, GameObjectType type)
        {
            float range = 0;
            if (ability == Abilities.Q)
                range = Program.QRange;
            else if (ability == Abilities.W)
                range = Program.WRange;
            else if (ability == Abilities.Slice || ability == Abilities.Dice)
                range = Program.E.Range;
            else if (ability == Abilities.Ignite)
                range = Program.Ignite.Range;

            return ObjectManager.Get<Obj_AI_Base>().OrderBy(a => a.Health).Where(a => a.IsEnemy
            && ((ability == Abilities.Q && RenektonCalcs.Q(a) >= a.Health) ||
            (ability == Abilities.W && RenektonCalcs.W(a) >= a.Health) ||
            (ability == Abilities.Slice && RenektonCalcs.Slice(a) >= a.Health) ||
            (ability == Abilities.Dice && RenektonCalcs.Dice(a) >= a.Health) ||
            (ability == Abilities.Ignite && RenektonCalcs.Ignite(a) >= a.Health))
            && a.Type == type
            && a.Distance(Renekton) <= range
            && !a.IsDead
            && !a.IsInvulnerable
            && a.IsValidTarget(range)).FirstOrDefault();
        }
        
        public static void LastHit()
        {
            bool QCheck = Program.LastHit["LHQ"].Cast<CheckBox>().CurrentValue;
            bool ECheck = Program.LastHit["LHE"].Cast<CheckBox>().CurrentValue;
            bool SaveFury = Program.LastHit["LHSF"].Cast<CheckBox>().CurrentValue;
            bool QReady = Program.Q.IsReady();
            bool EReady = Program.E.IsReady();
            
            if(!SaveFury || (SaveFury && Renekton.Mana < 50))
            {
                if(QReady && QCheck)
                {
                    Obj_AI_Base minion = GetEnemyKS(Abilities.Q, GameObjectType.obj_AI_Minion);
                    if (minion != null)
                        Program.Q.Cast();
                }
                if (EReady && ECheck)
                {
                    if (Program.E.Name == "RenektonSliceAndDice")
                    {
                        Obj_AI_Base minion = GetEnemyKS(Abilities.Slice, GameObjectType.obj_AI_Minion);
                        if (minion != null)
                            Program.E.Cast(minion.Position);
                    }
                    if (Program.E.Name == "renektondice")
                    {
                        Obj_AI_Base minion = GetEnemyKS(Abilities.Dice, GameObjectType.obj_AI_Minion);
                        if (minion != null)
                            Program.E.Cast(minion.Position);
                    }
                }
            }
        }

        public static void KillSteal()
        {
            bool QCheck = Program.Killsteal["KSQ"].Cast<CheckBox>().CurrentValue;
            bool WCheck = Program.Killsteal["KSW"].Cast<CheckBox>().CurrentValue;
            bool ECheck = Program.Killsteal["KSE"].Cast<CheckBox>().CurrentValue;
            bool IgCheck = Program.Killsteal["KSI"].Cast<CheckBox>().CurrentValue;
            bool QReady = Program.Q.IsReady();
            bool WReady = Program.W.IsReady();
            bool EReady = Program.E.IsReady();
            
            if (QReady && QCheck)
            {
                Obj_AI_Base enemy = GetEnemyKS(Abilities.Q, GameObjectType.AIHeroClient);
                if (enemy != null)
                    Program.Q.Cast();
            }
            if (Program.Ignite != null && IgCheck && Program.Ignite.IsReady())
            {
                Obj_AI_Base enemy = GetEnemyKS(Abilities.Ignite, GameObjectType.AIHeroClient);
                if (enemy != null)
                    Program.Ignite.Cast(enemy);
            }
            if (WReady && WCheck && Orbwalker.CanAutoAttack)
            {
                Obj_AI_Base enemy = GetEnemyKS(Abilities.W, GameObjectType.AIHeroClient);
                if (enemy != null && Renekton.IsInAutoAttackRange(enemy))
                {
                    Program.W.Cast();
                    Orbwalker.ForcedTarget = enemy;
                }
            }
            if (EReady && ECheck)
            {
                if (Program.E.Name == "RenektonSliceAndDice")
                {
                    Obj_AI_Base enemy = GetEnemyKS(Abilities.Slice, GameObjectType.AIHeroClient);
                    if (enemy != null)
                    {
                        Vector2 dashPos = Renekton.ServerPosition.Extend(enemy, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                        Program.E.Cast(dashPos.To3DWorld());
                    }
                }
                if (Program.E.Name == "renektondice")
                {
                    Obj_AI_Base enemy = GetEnemyKS(Abilities.Dice, GameObjectType.AIHeroClient);
                    if (enemy != null)
                    {
                        Vector2 dashPos = Renekton.ServerPosition.Extend(enemy, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                        Program.E.Cast(dashPos.To3DWorld());
                    }
                }
            }
        }

        public static void LaneClear()
        {
            bool QCheck = Program.LaneClear["LCQ"].Cast<CheckBox>().CurrentValue;
            bool ECheck = Program.LaneClear["LCE"].Cast<CheckBox>().CurrentValue;
            bool SaveFury = Program.LaneClear["LCSF"].Cast<CheckBox>().CurrentValue;
            bool QReady = Program.Q.IsReady();
            bool EReady = Program.E.IsReady();

            if (Program.LaneClear["LCI"].Cast<CheckBox>().CurrentValue)
                UseItems(Mode.Harass);

            if (!SaveFury || (SaveFury && Renekton.Mana < 50))
            {
                if (QReady && QCheck)
                {
                    Obj_AI_Base minion = GetEnemy(Abilities.Q, GameObjectType.obj_AI_Minion);
                    if (minion != null)
                        Program.Q.Cast();
                }
                if (EReady && ECheck)
                {
                    if (Program.E.Name == "RenektonSliceAndDice")
                    {
                        Obj_AI_Base minion = GetEnemyKS(Abilities.Slice, GameObjectType.obj_AI_Minion);
                        if (minion != null)
                        {
                            Vector2 dashPos = Renekton.ServerPosition.Extend(minion, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                            Program.E.Cast(dashPos.To3DWorld());
                        }
                        else
                        {
                            Obj_AI_Base minion2 = GetEnemy(Abilities.Slice, GameObjectType.obj_AI_Minion);
                            if (minion != null)
                            {
                                Vector2 dashPos = Renekton.ServerPosition.Extend(minion, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                                Program.E.Cast(dashPos.To3DWorld());
                            }
                        }
                    }
                    if (Program.E.Name == "renektondice")
                    {
                        Obj_AI_Base minion = GetEnemyKS(Abilities.Dice, GameObjectType.obj_AI_Minion);
                        if (minion != null)
                        {
                            Vector2 dashPos = Renekton.ServerPosition.Extend(minion, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                            Program.E.Cast(dashPos.To3DWorld());
                        }
                        else
                        {
                            Obj_AI_Base minion2 = GetEnemy(Abilities.Dice, GameObjectType.obj_AI_Minion);
                            if (minion != null)
                            {
                                Vector2 dashPos = Renekton.ServerPosition.Extend(minion, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                                Program.E.Cast(dashPos.To3DWorld());
                            }
                        }
                    }
                }
            }
        }
       
        public static void Harrass()
        {
            bool QCheck = Program.Harass["HQ"].Cast<CheckBox>().CurrentValue;
            bool WCheck = Program.Harass["HW"].Cast<CheckBox>().CurrentValue;
            bool ECheck = Program.Harass["HE"].Cast<CheckBox>().CurrentValue;
            bool QReady = Program.Q.IsReady();
            bool WReady = Program.W.IsReady();
            bool EReady = Program.E.IsReady();
            
            if (Program.Harass["HI"].Cast<CheckBox>().CurrentValue)
                UseItems(Mode.Harass);

            if (inFullCombo || 
                (QReady && WReady && EReady && QCheck && WCheck && ECheck))
            {
                if (comboEnemy == null)
                {
                    comboEnemy = (AIHeroClient)GetEnemy(Abilities.Slice, GameObjectType.AIHeroClient);
                    if (comboEnemy == null)
                    {
                        inFullCombo = false;
                        return;
                    }
                    inFullCombo = true;
                }
                //engage
                if (Program.E.Name == "RenektonSliceAndDice" && EReady)
                {
                    beginningComboPosition = Renekton.Position;
                    if (comboEnemy != null)
                    {
                        Vector2 dashPos = Renekton.ServerPosition.Extend(comboEnemy, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                        Program.E.Cast(dashPos.To3DWorld());
                    }
                }
                if (WReady && !Renekton.HasBuffOfType(BuffType.Blind))
                {
                    Program.W.Cast();
                    Orbwalker.ForcedTarget = comboEnemy;
                }
                else if(QReady && comboEnemy.Distance(Renekton) <= Program.QRange)
                    Program.Q.Cast();
                else if (Program.E.Name == "renektondice" && EReady && !WReady && Renekton.CanMove)
                {
                    if (beginningComboPosition.Distance(Renekton.Position) >= Program.E.Range)
                        Renekton.Position.Extend(beginningComboPosition, Program.E.Range);
                    else
                    {
                        Vector2 dashPos = Renekton.ServerPosition.Extend(beginningComboPosition, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                        Program.E.Cast(dashPos.To3DWorld());
                    }
                    inFullCombo = false;
                    beginningComboPosition = Vector3.Zero;
                    comboEnemy = null;
                }

                if (comboEnemy == null || comboEnemy.IsDead || comboEnemy.IsInvulnerable || comboEnemy.Distance(Renekton) >= Program.E.Range)
                {
                    beginningComboPosition = Vector3.Zero;
                    inFullCombo = false;
                    comboEnemy = null;
                }
            }
            else
            {
                if(QCheck && QReady)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemy(Abilities.Q, GameObjectType.AIHeroClient);
                    if (enemy != null)
                        Program.Q.Cast();
                }
                if (WCheck && WReady && Renekton.CanAttack)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemy(Abilities.W, GameObjectType.AIHeroClient);
                    if (enemy != null && !Renekton.HasBuffOfType(BuffType.Blind))
                    {
                        Program.W.Cast();
                        Orbwalker.ForcedTarget = enemy;
                    }
                }
                if (ECheck && EReady)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemy(Abilities.Slice, GameObjectType.AIHeroClient);
                    if (enemy != null)
                    {
                        Vector2 dashPos = Renekton.ServerPosition.Extend(enemy, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                        Program.E.Cast(dashPos.To3DWorld());
                    }
                }
            }
        }

        public static void Combo()
        {
            bool QCheck = Program.ComboMenu["CQ"].Cast<CheckBox>().CurrentValue;
            bool WCheck = Program.ComboMenu["CW"].Cast<CheckBox>().CurrentValue;
            bool ECheck = Program.ComboMenu["CE"].Cast<CheckBox>().CurrentValue;
            bool RCheck = Program.ComboMenu["CR"].Cast<CheckBox>().CurrentValue;
            bool QReady = Program.Q.IsReady();
            bool WReady = Program.W.IsReady();
            bool EReady = Program.E.IsReady();
            bool RReady = Program.R.IsReady();
            
            if (Program.ComboMenu["CI"].Cast<CheckBox>().CurrentValue)
                UseItems(Mode.Combo);

            if (Renekton.CountEnemiesInRange(Renekton.GetAutoAttackRange()) >= 3
                || Renekton.Health <= Renekton.MaxHealth * 0.2f
                && RCheck && RReady)
                Program.R.Cast();


            if (inFullCombo ||
                (QReady && WReady && EReady && QCheck && WCheck && ECheck))
            {
                if (comboEnemy == null)
                {
                    comboEnemy = (AIHeroClient)GetEnemy(Abilities.Slice, GameObjectType.AIHeroClient);
                    if (comboEnemy == null)
                    {
                        inFullCombo = false;
                        return;
                    }
                    inFullCombo = true;
                }
                //engage
                if (Program.E.Name == "RenektonSliceAndDice" && EReady)
                {
                    beginningComboPosition = Renekton.Position;

                    Vector2 dashPos = Renekton.ServerPosition.Extend(comboEnemy, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                    Program.E.Cast(dashPos.To3DWorld());
                }
                if (WReady && !Renekton.HasBuffOfType(BuffType.Blind))
                {
                    Program.W.Cast();
                    Orbwalker.ForcedTarget = comboEnemy;
                }
                else if (QReady && comboEnemy.Distance(Renekton) <= Program.QRange)
                {
                    Program.Q.Cast();
                }
                else if (Program.E.Name == "renektondice" && EReady && !WReady && Renekton.CanMove)
                {
                    Vector2 dashPos = Renekton.ServerPosition.Extend(comboEnemy, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                    Program.E.Cast(dashPos.To3DWorld());
                    inFullCombo = false;
                    beginningComboPosition = Vector3.Zero;
                    comboEnemy = null;
                }

                if (comboEnemy == null || comboEnemy.IsDead || comboEnemy.IsInvulnerable || comboEnemy.Distance(Renekton) >= Program.E.Range)
                {
                    beginningComboPosition = Vector3.Zero;
                    inFullCombo = false;
                    comboEnemy = null;
                }
            }
            else
            {
                if (QCheck && QReady)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemy(Abilities.Q, GameObjectType.AIHeroClient);
                    if (enemy != null)
                        Program.Q.Cast();
                }
                if (WCheck && WReady && Renekton.CanAttack && !Renekton.HasBuffOfType(BuffType.Blind))
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemy(Abilities.W, GameObjectType.AIHeroClient);
                    if (enemy != null)
                    {
                        Program.W.Cast();
                        Orbwalker.ForcedTarget = enemy;
                    }
                }
                if (ECheck && EReady)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemy(Abilities.Slice, GameObjectType.AIHeroClient);
                    
                    if (enemy != null)
                    {
                        Vector2 dashPos = Renekton.ServerPosition.Extend(enemy, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                        Program.E.Cast(dashPos.To3DWorld());
                    }
                }
            }
        }

        public static void Flee()
        {
            if (Program.E.IsReady())
            {
                Vector2 dashPos = Renekton.ServerPosition.Extend(Game.CursorPos, Program.E.Range * Program.Random.NextFloat(0, 0.05f));
                Program.E.Cast(dashPos.To3DWorld());
            }
        }

        public static void UseItems(Mode mode)
        {
            InventorySlot[] items = Renekton.InventoryItems;

            foreach (InventorySlot item in items)
            {
                if (item.CanUseItem())
                {
                    if (mode == Mode.Combo || mode == Mode.Harass)
                    {
                        if (item.Id == ItemId.Ravenous_Hydra_Melee_Only || item.Id == ItemId.Tiamat_Melee_Only)
                            if (GetEnemy(400, GameObjectType.AIHeroClient) != null)
                                item.Cast();
                        if (item.Id == ItemId.Youmuus_Ghostblade)
                            if (GetEnemy(Program.EmpERange, GameObjectType.AIHeroClient) != null)
                                item.Cast();
                        if ((int)item.Id == 3053)//ItemId.TitanicHydra)
                            if (GetEnemy(Renekton.GetAutoAttackRange(), GameObjectType.AIHeroClient) != null && Renekton.CanAttack)
                                item.Cast();
                        if (item.Id == ItemId.Quicksilver_Sash || item.Id == ItemId.Mercurial_Scimitar)
                            if (Renekton.HasBuffOfType(BuffType.Stun) ||
                                Renekton.HasBuffOfType(BuffType.Snare) ||
                                Renekton.HasBuffOfType(BuffType.Blind) ||
                                Renekton.HasBuffOfType(BuffType.Charm) ||
                                Renekton.HasBuffOfType(BuffType.Fear) ||
                                Renekton.HasBuffOfType(BuffType.Taunt) ||
                                Renekton.HasBuffOfType(BuffType.Slow))
                                item.Cast();
                    }
                    if (mode == Mode.LaneClear)
                    {
                        if (item.Id == ItemId.Ravenous_Hydra_Melee_Only || item.Id == ItemId.Tiamat_Melee_Only)
                            if (GetEnemy(400, GameObjectType.obj_AI_Minion) != null)
                                item.Cast();
                        if ((int)item.Id == 3053)//ItemId.TitanicHydra)
                            if (GetEnemy(Renekton.GetAutoAttackRange(), GameObjectType.obj_AI_Minion) != null && Renekton.CanAttack)
                                item.Cast();
                    }
                }
            }
        }
    }
}
