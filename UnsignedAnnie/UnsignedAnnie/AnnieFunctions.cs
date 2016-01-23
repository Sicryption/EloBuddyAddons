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

namespace UnsignedAnnie
{
    class AnnieFunctions
    {
        public enum AttackSpell
        {
            Q,
            W,
            R,
            RPet,
            Ignite
        };

        //get enemy non last hit)
        public static Obj_AI_Base GetEnemy(float range, GameObjectType type)
        {
            return ObjectManager.Get<Obj_AI_Base>().OrderBy(a => a.Health).Where(a => a.IsEnemy
            && a.Type == type
            && a.Distance(Annie) <= range
            && !a.IsDead
            && !a.IsInvulnerable
            && a.IsValidTarget(range)).FirstOrDefault();
        }

        public static Obj_AI_Base GetEnemyKS(GameObjectType type, AttackSpell spell)
        {
            float range = 0;
            if (spell == AttackSpell.W)
                range = Program.W.Range;
            else if (spell == AttackSpell.R)
                range = Program.R.Range;
            else if (spell == AttackSpell.Q)
                range = Program.Q.Range;
            else if (spell == AttackSpell.Ignite)
                range = Program.Ignite.Range;
            //ksing
            return ObjectManager.Get<Obj_AI_Base>().OrderBy(a => a.Health).Where(a => a.IsEnemy
                && a.Type == type
                && a.Distance(Annie) <= range
                && !a.IsDead
                && !a.IsInvulnerable
                && a.IsValidTarget(range)
                && 
                (
                (a.Health <= AnnieCalcs.W(a) && AttackSpell.W == spell) ||
                (a.Health <= AnnieCalcs.Q(a) && AttackSpell.Q == spell) ||
                (a.Health <= AnnieCalcs.R(a) && AttackSpell.R == spell) ||
                (a.Health <= AnnieCalcs.Ignite(a) && AttackSpell.Ignite == spell)
                )).FirstOrDefault();
        }

        public static AIHeroClient Annie { get { return ObjectManager.Player; } }

        public static void LastHit()
        {
            bool QCHECK = Program.LastHit["LHQ"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.LastHit["LHW"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool WREADY = Program.W.IsReady();

            if (QCHECK && QREADY)
            {
                Obj_AI_Minion enemy = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.Q);

                if (enemy != null)
                    Program.Q.Cast(enemy);
            }

            if (WCHECK && WREADY)
            {
                Obj_AI_Minion enemy = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.W);

                if (enemy != null)
                    Program.W.Cast(enemy.Position);
            }
        }

        public static void KillSteal()
        {
            bool QCHECK = Program.Killsteal["KSQ"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.Killsteal["KSW"].Cast<CheckBox>().CurrentValue;
            bool RCHECK = Program.Killsteal["KSR"].Cast<CheckBox>().CurrentValue;
            bool ICHECK = Program.Killsteal["KSI"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool WREADY = Program.W.IsReady();
            bool RREADY = Program.R.IsReady();
            bool IREADY = false;
            if (Program.Ignite != null && Program.Ignite.IsReady())
                IREADY = true;

            if (ObjectManager.Get<AIHeroClient>().Where(a => a.IsEnemy && a.Distance(Annie) <= Program.R.Range).OrderBy(a => a.Health).FirstOrDefault() != null)
            {
                if (QCHECK && QREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.Q);

                    if (enemy != null)
                        Program.Q.Cast(enemy);
                }
                if (WCHECK && WREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.W);

                    if (enemy != null)
                        Program.W.Cast(enemy.Position);
                }
                if (RCHECK && RREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.R);

                    if (enemy != null)
                        Program.R.Cast(enemy.Position);
                }
                
                if (ICHECK && IREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.Ignite);

                    if (enemy != null)
                        Program.Ignite.Cast(enemy);
                }
            }
        }

        public static void LaneClear()
        {
            bool QCHECK = Program.LaneClear["LCQ"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.LaneClear["LCW"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool WREADY = Program.W.IsReady();

            if (QCHECK && QREADY)
            {
                Obj_AI_Minion enemy = (Obj_AI_Minion)GetEnemy(Program.Q.Range, GameObjectType.obj_AI_Minion);

                if (enemy != null)
                    Program.Q.Cast(enemy);
            }

            if (WCHECK && WREADY)
            {
                Vector3 pos = GetBestWLocation(GameObjectType.obj_AI_Minion);

                if (pos != Vector3.Zero)
                    Program.W.Cast(pos);
            }
        }
       
        public static void Harrass()
        {
            bool QCHECK = Program.Harass["HQ"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.Harass["HW"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool WREADY = Program.W.IsReady();

            if (QCHECK && QREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    Program.Q.Cast(enemy);
            }

            if (WCHECK && WREADY)
            {
                Vector3 pos = GetBestWLocation(GameObjectType.AIHeroClient);

                if (pos != Vector3.Zero)
                    Program.W.Cast(pos);
            }
        }

        public static void Combo()
        {
            bool QCHECK = Program.ComboMenu["QU"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.ComboMenu["WU"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.ComboMenu["EU"].Cast<CheckBox>().CurrentValue;
            bool RCHECK = Program.ComboMenu["RU"].Cast<CheckBox>().CurrentValue;
            bool ItemsCHECK = Program.ComboMenu["IU"].Cast<CheckBox>().CurrentValue;
            bool IgniteCHECK = Program.ComboMenu["IgU"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool WREADY = Program.W.IsReady();
            bool EREADY = Program.E.IsReady();
            bool RREADY = Program.R.IsReady();

            if (!Annie.HasBuff("pyromania_particle") && ECHECK && EREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    Program.E.Cast();
            }

            if (RCHECK && RREADY)
            {
                Vector3 pos = GetBestRLocation(GameObjectType.AIHeroClient);

                if (pos != Vector3.Zero)
                    Program.R.Cast(pos);
            }
            /*if (RCHECK && RREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.R.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    Program.R.Cast(enemy);
            }*/

            if (QCHECK && QREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    Program.Q.Cast(enemy);
            }

            if (WCHECK && WREADY)
            {
                Vector3 pos = GetBestWLocation(GameObjectType.AIHeroClient);

                if (pos != Vector3.Zero)
                    Program.W.Cast(pos);
            }

            if (ItemsCHECK)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(2500, GameObjectType.AIHeroClient);

                if (enemy != null)
                    UseItems();
            }

            if (IgniteCHECK && Program.Ignite != null && Program.Ignite.IsReady())
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Ignite.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    Program.Ignite.Cast(enemy);
            }
        }

        public static void StackMode()
        {
            if(Annie.IsInShopRange()
                && !Annie.HasBuff("pyromania_particle"))
            {
                if (Program.W.IsReady())
                    Program.W.Cast(Annie.Position);
                if (Program.E.IsReady())
                    Program.E.Cast();
            }
        }

        public static void Flee()
        {
            Orbwalker.MoveTo(Game.CursorPos);
        }

        public static void UseItems()
        {
            InventorySlot[] items = Annie.InventoryItems;

            foreach (InventorySlot item in items)
            {
                if (item.CanUseItem())
                {
                    if (item.Id == ItemId.Health_Potion
                        && Annie.Health <= (Annie.MaxHealth * 0.45)
                        && !Annie.IsRecalling()
                        && Annie.CountEnemiesInRange(2000) <= 1
                        && !Annie.IsInShopRange()
                        && !Annie.HasBuff("RegenerationPotion"))
                    {
                        item.Cast();
                    }
                    if (item.Id == ItemId.Mana_Potion
                        && Annie.Mana <= (Annie.MaxMana * 0.45)
                        && !Annie.IsRecalling()
                        && Annie.CountEnemiesInRange(2000) <= 1
                        && !Annie.IsInShopRange()
                        && !Annie.HasBuff("FlaskOfCrystalWater"))
                    {
                        item.Cast();
                    }
                    if (item.Id == ItemId.Crystalline_Flask
                        && (Annie.Health <= (Annie.MaxHealth * 0.45) || Annie.Mana <= (Annie.MaxMana * 0.45))
                        && !Annie.IsRecalling()
                        && Annie.CountEnemiesInRange(2000) <= 1
                        && !Annie.IsInShopRange()
                        && !Annie.HasBuff("ItemCrystalFlask"))
                    {
                        item.Cast();
                    }
                }
            }
        }
        
        public static void ControlTibbers()
        {
            if(Annie.Pet != null)
            {
                AIHeroClient enemy = ObjectManager.Get<AIHeroClient>()
                    .OrderBy(a => a.Health)
                    .Where(a => !a.IsDead
                    && !a.IsInvulnerable
                    && a.IsEnemy
                    && a.Distance(Annie.Pet) <= 1500).FirstOrDefault();

                if (enemy != null)
                    Player.IssueOrder(GameObjectOrder.AutoAttackPet, enemy);
                else
                {
                    Obj_AI_Turret turret = ObjectManager.Get<Obj_AI_Turret>()
                        .OrderBy(a => a.Health)
                        .Where(a => !a.IsDead
                        && !a.IsInvulnerable
                        && a.IsEnemy
                        && a.Distance(Annie.Pet) <= 1500).FirstOrDefault();

                    if (turret != null)
                        Player.IssueOrder(GameObjectOrder.AutoAttackPet, turret);
                    else
                    {
                        Obj_AI_Minion minion = ObjectManager.Get<Obj_AI_Minion>()
                            .OrderBy(a => a.Health)
                            .Where(a => !a.IsDead
                            && !a.IsInvulnerable
                            && a.IsEnemy
                            && a.Distance(Annie.Pet) <= 1500).FirstOrDefault();

                        if (minion != null)
                            Player.IssueOrder(GameObjectOrder.AutoAttackPet, minion);
                    }
                }
            }
        }

        public static void AutoUlt()
        {
            if(GetBestRLocationUnits(GameObjectType.AIHeroClient) >= 4)
            {
                Vector3 pos = GetBestRLocation(GameObjectType.AIHeroClient);
                if(pos != Vector3.Zero)
                {

                }
            }
        }

        public static Vector3 GetBestWLocation(GameObjectType type)
        {
            int mostUnits = 0;
            Vector3 bestPos = Vector3.Zero;
            PredictionResult[] prediction = Prediction.Position.PredictConeSpellAoe(ObjectManager.Get<Obj_AI_Base>()
                    .Where(a => !a.IsDead
                    && !a.IsInvulnerable
                    && a.Type == type)
                    .ToArray(), Program.W.Range, 50, 1, 0, Annie.Position);
            foreach(PredictionResult pr in prediction)
            {
                if(pr.CollisionObjects.Length > mostUnits)
                {
                    mostUnits = pr.CollisionObjects.Length;
                    bestPos = pr.CastPosition;
                }
            }

            //Chat.Print("Units: " + mostUnits);

            return bestPos;
        }
        
        public static Vector3 GetBestRLocation(GameObjectType type)
        {
            int mostUnits = 0;
            Vector3 bestPos = Vector3.Zero;
            PredictionResult[] prediction = Prediction.Position.PredictCircularMissileAoe(ObjectManager.Get<Obj_AI_Base>()
                    .Where(a => !a.IsDead
                    && !a.IsInvulnerable
                    && a.Type == type)
                    .ToArray(), Program.R.Range, 290, 0, 0, Annie.Position);
            foreach (PredictionResult pr in prediction)
            {
                if (pr.CollisionObjects.Length > mostUnits)
                {
                    mostUnits = pr.CollisionObjects.Length;
                    bestPos = pr.CastPosition;
                }
            }

            //Chat.Print("Units: " + mostUnits);

            return bestPos;
        }
        public static int GetBestRLocationUnits(GameObjectType type)
        {
            int mostUnits = 0;
            PredictionResult[] prediction = Prediction.Position.PredictCircularMissileAoe(ObjectManager.Get<Obj_AI_Base>()
                    .Where(a => !a.IsDead
                    && !a.IsInvulnerable
                    && a.Type == type)
                    .ToArray(), Program.R.Range, 290, 0, 0, Annie.Position);
            foreach (PredictionResult pr in prediction)
            {
                if (pr.CollisionObjects.Length > mostUnits)
                    mostUnits = pr.CollisionObjects.Length;
            }

            //Chat.Print("Units: " + mostUnits);

            return mostUnits;
        }
    }
}
