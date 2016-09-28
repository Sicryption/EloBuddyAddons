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

namespace UnsignedRyze
{
    class RyzeFunctions
    {
        public enum AttackSpell
        {
            Q,
            W,
            E,
            ENearEnemy,
            Ignite
        };

        //get enemy non last hit
        public static Obj_AI_Base GetEnemy(float range, GameObjectType type)
        {
            return ObjectManager.Get<Obj_AI_Base>().OrderBy(a => a.Health).Where(a => a.IsEnemy
            && a.Type == type
            && a.Distance(Ryze) <= range
            && !a.IsDead
            && !a.IsInvulnerable
            && a.IsValidTarget(range)).FirstOrDefault();
        }

        public static Obj_AI_Base GetEnemyKS(GameObjectType type, AttackSpell spell)
        {
            float range = 0;
            if (spell == AttackSpell.Q)
                range = Program.Q.Range;
            else if (spell == AttackSpell.E)
                range = Program.E.Range;
            else if (spell == AttackSpell.W)
                range = Program.W.Range;
            else if (spell == AttackSpell.Ignite)
                range = Program.Ignite.Range;
            //ksing
            return ObjectManager.Get<Obj_AI_Base>().OrderBy(a => a.Health).Where(a => a.IsEnemy
                && a.Type == type
                && a.Distance(Ryze) <= range
                && !a.IsDead
                && !a.IsInvulnerable
                && a.IsValidTarget(range)
                &&
                (
                (a.Health <= RyzeCalcs.W(a) && AttackSpell.W == spell) ||
                (a.Health <= RyzeCalcs.Q(a) && Program.Q.GetPrediction(a).HitChance >= HitChance.Low && AttackSpell.Q == spell) ||
                (a.Health <= RyzeCalcs.E(a) && AttackSpell.E == spell) ||
                (a.Health <= RyzeCalcs.Ignite(a) && AttackSpell.Ignite == spell)
                )).FirstOrDefault();
        }

        public static AIHeroClient Ryze { get { return ObjectManager.Player; } }

        public static void LastHit()
        {
            bool QCHECK = Program.LastHit["LHQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.LastHit["LHE"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();

            if (!Ryze.CanAttack)
                return;

            if (QCHECK && QREADY)
            {
                Obj_AI_Minion enemy = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.Q);

                if (enemy != null && Program.Q.GetPrediction(enemy).HitChance >= HitChance.Low)
                    Program.Q.Cast(enemy.Position);
            }

            if (ECHECK && EREADY)
            {
                Obj_AI_Minion enemy = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.E);

                if (enemy != null)
                    Program.E.Cast(enemy);
            }
        }

        public static void KillSteal()
        {
            bool QCHECK = Program.Killsteal["KSQ"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.Killsteal["KSW"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.Killsteal["KSE"].Cast<CheckBox>().CurrentValue;
            bool ICHECK = Program.Killsteal["KSI"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool WREADY = Program.W.IsReady();
            bool EREADY = Program.E.IsReady();
            //problem lies here. probably a summoner name change
            bool IREADY = (Program.Ignite != null && Program.Ignite.IsReady()) ? true : false;

            if (Ryze.CountEnemiesInRange(Program.Q.Range) >= 1)
            {
                if (QCHECK && QREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.Q);

                    if (enemy != null)
                        Program.Q.Cast(enemy.Position);
                }
                if (WCHECK && WREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.W);

                    if (enemy != null)
                        Program.W.Cast(enemy);
                }
                if (ECHECK && EREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.E);

                    if (enemy != null)
                        Program.E.Cast(enemy);
                }
                
                if (ICHECK && IREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.Ignite);

                    if (enemy != null)
                        Program.Ignite.Cast(enemy);
                }
            }
        }

        //only q if it will last hit and they have spell flux 
        //OR last hit if no other minions are nearby
        // use e to spread and q to kill if spreading and q will do enough damage to clear all 3 minions
        public static void LaneClear()
        {
            bool QCHECK = Program.LaneClear["LCQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.LaneClear["LCE"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();


            if (QCHECK && QREADY)
            {
                Obj_AI_Minion enemy = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.Q);

                if (enemy != null)
                    Program.Q.Cast(enemy.Position);
            }

            if (ECHECK && EREADY)
            {
                Obj_AI_Minion enemy = (Obj_AI_Minion)GetEnemy(Program.E.Range, GameObjectType.obj_AI_Minion);

                if (enemy != null)
                    Program.E.Cast(enemy);
            }
        }

        //e minions with e stack that will spread to champion then q the minion
        public static void Harrass()
        {
            bool QCHECK = Program.Harass["HQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.Harass["HE"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();

            if (QCHECK && QREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null && Program.Q.GetPrediction(enemy).HitChance >= HitChance.Low)
                    Program.Q.Cast(enemy);
            }

            if (ECHECK && EREADY && !QREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.E.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    Program.E.Cast(enemy);
            }
        }

        //do q-e-q-w-q-e-q OR q-ew-q-e-q OR q-e-e-q-w-q-e-q OR -e-e-w-q-e-q
        public static void Combo()
        {
            bool QCHECK = Program.ComboMenu["QU"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.ComboMenu["WU"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.ComboMenu["EU"].Cast<CheckBox>().CurrentValue;
            bool ItemsCHECK = Program.ComboMenu["IU"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool WREADY = Program.W.IsReady();
            bool EREADY = Program.E.IsReady();

            if (QCHECK && QREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null && Program.Q.GetPrediction(enemy).HitChance >= HitChance.Low)
                    Program.Q.Cast(enemy.Position);
            }

            if (WCHECK && WREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.W.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    Program.W.Cast(enemy);
            }

            if (ECHECK && EREADY)
            {
                //if e kills minion then it will spread. if the spread kills a minion it will continue to spread.
                Obj_AI_Base enemy = enemy = (AIHeroClient)GetEnemy(Program.E.Range, GameObjectType.AIHeroClient);
                Obj_AI_Base bouceTarg = EBounceTarget(enemy);
                enemy = (bouceTarg == null) ? enemy : bouceTarg;

                if (enemy != null)
                    Program.E.Cast(enemy);
            }

            if (ItemsCHECK)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(2500, GameObjectType.AIHeroClient);

                if (enemy != null)
                    UseItems();
            }
        }

        public static void StackMode()
        {
            if (Ryze.IsInShopRange() && Ryze.HasItem(ItemId.Tear_of_the_Goddess))
            {
                if (Program.Q.IsReady())
                    Program.Q.Cast(Ryze.Position);
            }
        }

        //do ryze shield + movespeed combo
        public static void Flee()
        {
            Orbwalker.MoveTo(Game.CursorPos);
        }

        //add zhonyas with ult, use sephrahs, add refillable/hunters/corruption potion
        public static void UseItems()
        {
            InventorySlot[] items = Ryze.InventoryItems;

            foreach (InventorySlot item in items)
            {
                if (item.CanUseItem())
                {
                    if (item.Id == ItemId.Health_Potion
                        && Ryze.Health <= (Ryze.MaxHealth * 0.45)
                        && !Ryze.IsRecalling()
                        && !Ryze.IsInShopRange()
                        && !Ryze.HasBuff("RegenerationPotion"))
                    {
                        item.Cast();
                    }
                }
            }
        }

        public static Obj_AI_Base EBounceTarget(Obj_AI_Base target)
        {
            if (target == null)
                return null;
            foreach(Obj_AI_Base enemyUnit in ObjectManager.Get<Obj_AI_Base>().Where(a=>a.IsEnemy&&!a.IsDead&&a.IsInRange(target, 250)&&a.IsInRange(Ryze, Program.E.Range)&&RyzeCalcs.E(a) >= a.Health))
            {
                return enemyUnit;
            }
            return null;
        }
    }
}
