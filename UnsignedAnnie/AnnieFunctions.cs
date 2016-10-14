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
            Ignite
        };
        
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
                range = Program.R.Range + (Program.R.Width /2);
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
            bool QCHECK = Program.LastHit["Q"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.LastHit["W"].Cast<CheckBox>().CurrentValue;
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
            bool QCHECK = Program.Killsteal["Q"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.Killsteal["W"].Cast<CheckBox>().CurrentValue;
            bool RCHECK = Program.Killsteal["R"].Cast<CheckBox>().CurrentValue;
            bool ICHECK = Program.Killsteal["Ignite"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool WREADY = Program.W.IsReady();
            bool RREADY = Program.R.IsReady();
            bool IREADY = false;
            if (Program.Ignite != null && Program.Ignite.IsReady())
                IREADY = true;

            if (ObjectManager.Get<AIHeroClient>().Where(a => a.IsEnemy && a.Distance(Annie) <= Program.R.Range + (Program.R.Width /2)).OrderBy(a => a.Health).FirstOrDefault() != null)
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
            bool QCHECK = Program.LaneClear["Q"].Cast<CheckBox>().CurrentValue;
            bool QForLastHit = Program.LaneClear["QForLastHit"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.LaneClear["W"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool WREADY = Program.W.IsReady();

            if (QCHECK && QREADY)
            {
                Obj_AI_Minion enemy;
                if (QForLastHit)
                    enemy = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.Q);
                else
                    enemy = (Obj_AI_Minion)GetEnemy(Program.Q.Range, GameObjectType.obj_AI_Minion);
                if (enemy != null)
                    Program.Q.Cast(enemy);
            }
                
            if (WCHECK && WREADY)
            {
                Vector3 pos;
                Chat.Print("ELLO2");
                if (GetBestWLocationUnits(GameObjectType.obj_AI_Minion, out pos) >= 3)
                {
                    Chat.Print("ELLO");
                    if (pos != Vector3.Zero)
                        Program.W.Cast(pos);
                }
            }
        }
       
        public static void Harrass()
        {
            bool QCHECK = Program.Harass["Q"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.Harass["W"].Cast<CheckBox>().CurrentValue;
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
                Vector3 pos;
                GetBestWLocationUnits(GameObjectType.AIHeroClient, out pos);

                if (pos != Vector3.Zero)
                    Program.W.Cast(pos);
            }
        }

        public static void Combo()
        {
            bool QCHECK = Program.ComboMenu["Q"].Cast<CheckBox>().CurrentValue;
            bool WCHECK = Program.ComboMenu["W"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.ComboMenu["E"].Cast<CheckBox>().CurrentValue;
            bool RCHECK = Program.ComboMenu["R"].Cast<CheckBox>().CurrentValue;
            bool ItemsCHECK = Program.ComboMenu["Items"].Cast<CheckBox>().CurrentValue;
            bool IgniteCHECK = Program.ComboMenu["Ignite"].Cast<CheckBox>().CurrentValue;
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

            if (QCHECK && QREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    Program.Q.Cast(enemy);
            }

            if (WCHECK && WREADY)
            {
                Vector3 pos;
                GetBestWLocationUnits(GameObjectType.AIHeroClient, out pos);

                if (pos != Vector3.Zero)
                    Program.W.Cast(pos);
                else
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.W.Range, GameObjectType.AIHeroClient);

                    if (enemy != null)
                        Program.W.Cast(enemy.Position);
                }
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
                if (Program.W.IsLearned && Program.W.IsReady())
                    Program.W.Cast(Annie.Position);
                if (Program.E.IsLearned && Program.E.IsReady())
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
                }
            }
        }
        
        public static void ControlTibbers()
        {
            if(Program.R.Name == "infernalguardianguide")
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

        public static void CastR(Vector3 position)
        {
            //enemy is not in annie r range
            if (Annie.Distance(position) > Program.R.Range + (Program.R.Width / 2) || !Program.R.IsReady())
                return;
            //enemy is in the range
            else if (Annie.Distance(position) <= Program.R.Range)
                Program.R.Cast(position);
            //enemy can be hit by tibbers AOE spawn size
            else
                Program.R.Cast(Annie.Position.Extend(position, Program.R.Range).To3D());
        }

        public static void AutoUlt()
        {
            Vector3 ultPos = Vector3.Zero;
            if(Program.R.IsLearned && Program.R.IsReady() && Program.R.Name == "InfernalGuardian" && GetBestRLocationUnits(GameObjectType.AIHeroClient, out ultPos) >= 3)
            {
                if (ultPos != Vector3.Zero)
                {
                    if (Annie.HasBuff("pyromania_particle"))
                        CastR(ultPos);
                    else if (Program.PassiveStacks == 3 && Program.E.IsReady())
                        Program.E.Cast();
                    else if (Program.PassiveStacks == 3 && Program.W.IsReady())
                    {
                        Vector3 position;
                        GetBestWLocationUnits(GameObjectType.obj_AI_Base, out position);
                        if (position != Vector3.Zero)
                            Program.W.Cast(position);
                        else
                            Program.W.Cast(Annie.Position);
                    }
                    else if (Program.PassiveStacks == 3 && Program.Q.IsReady())
                    {
                        AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);
                        if (enemy != null)
                            Program.Q.Cast(enemy);
                    }
                }
            }
        }

        //credits to whoever made it
        public static int GetBestWLocationUnits(GameObjectType type, out Vector3 pos)
        {
            var sectorList = new List<Geometry.Polygon.Sector>();
            pos = Vector3.Zero;

            List<Obj_AI_Minion> minionList = EntityManager.MinionsAndMonsters.EnemyMinions.Where(it => !it.IsDead && it.IsValidTarget(Program.W.Range)).OrderByDescending(it => it.Distance(Annie)).ToList();
            List<AIHeroClient> championList = EntityManager.Heroes.Enemies.Where(it => !it.IsDead && it.IsValidTarget(Program.W.Range)).OrderByDescending(it => it.Distance(Annie)).ToList();

            Obj_AI_Base enemy = (type == GameObjectType.AIHeroClient) ?
                (Obj_AI_Base)championList.FirstOrDefault() : (Obj_AI_Base)minionList.FirstOrDefault();

            if (enemy == null)
                return 0;

            var Vectors = new List<Vector3>()
            {
                new Vector3(enemy.ServerPosition.X + 550, enemy.ServerPosition.Y, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X - 550, enemy.ServerPosition.Y, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X, enemy.ServerPosition.Y + 550, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X, enemy.ServerPosition.Y - 550, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X + 230, enemy.ServerPosition.Y, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X - 230, enemy.ServerPosition.Y, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X, enemy.ServerPosition.Y + 230, enemy.ServerPosition.Z),
                new Vector3(enemy.ServerPosition.X, enemy.ServerPosition.Y - 230, enemy.ServerPosition.Z),
                enemy.ServerPosition
            };

            float ANGLE = (float)(5 * Math.PI / 18);

            var sector1 = new Geometry.Polygon.Sector(Annie.Position, Vectors[0], ANGLE, 585);
            var sector2 = new Geometry.Polygon.Sector(Annie.Position, Vectors[1], ANGLE, 585);
            var sector3 = new Geometry.Polygon.Sector(Annie.Position, Vectors[2], ANGLE, 585);
            var sector4 = new Geometry.Polygon.Sector(Annie.Position, Vectors[3], ANGLE, 585);
            var sector5 = new Geometry.Polygon.Sector(Annie.Position, Vectors[4], ANGLE, 585);
            var sector6 = new Geometry.Polygon.Sector(Annie.Position, Vectors[5], ANGLE, 585);
            var sector7 = new Geometry.Polygon.Sector(Annie.Position, Vectors[6], ANGLE, 585);
            var sector8 = new Geometry.Polygon.Sector(Annie.Position, Vectors[7], ANGLE, 585);
            var sector9 = new Geometry.Polygon.Sector(Annie.Position, Vectors[8], ANGLE, 585);

            sectorList.Add(sector1);
            sectorList.Add(sector2);
            sectorList.Add(sector3);
            sectorList.Add(sector4);
            sectorList.Add(sector5);
            sectorList.Add(sector6);
            sectorList.Add(sector7);
            sectorList.Add(sector8);
            sectorList.Add(sector9);

            var CSHits = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int count = 0; count < 9; count++)
                if (type == GameObjectType.AIHeroClient)
                {
                    foreach (Obj_AI_Base champion in championList)
                        if (sectorList.ElementAt(count).IsInside(champion))
                            CSHits[count]++;
                }
                else
                {
                    foreach (Obj_AI_Base minion in minionList)
                        if (sectorList.ElementAt(count).IsInside(minion))
                            CSHits[count]++;
                }

            int i = CSHits.Select((value, index) => new { Value = value, Index = index }).Aggregate((a, b) => (a.Value > b.Value) ? a : b).Index;

            pos = Vectors[i];
            return CSHits[i];
        }

        public static Vector3 GetBestRLocation(GameObjectType type)
        {
            Spell.Skillshot.BestPosition pos = Program.R.GetBestCircularCastPosition(EntityManager.Enemies.Where(a => a.Type == type && a.Distance(Annie) <= Program.R.Range + (Program.R.Width / 2)));
            if (pos.HitNumber >= 1)
                return pos.CastPosition;
            else
                return Vector3.Zero;
        }
        public static int GetBestRLocationUnits(GameObjectType type, out Vector3 pos)
        {
            Spell.Skillshot.BestPosition position = Program.R.GetBestCircularCastPosition(EntityManager.Enemies.Where(a => a.Type == type && a.Distance(Annie) <= Program.R.Range + (Program.R.Width / 2)));
            pos = position.CastPosition;
            if (position.HitNumber >= 1)
                return position.HitNumber;
            else
                return 0;
        }
    }
}
