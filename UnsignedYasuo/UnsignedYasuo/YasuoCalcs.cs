using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UnsignedYasuo
{

    class YasuoCalcs
    {
        private static AIHeroClient yo { get { return ObjectManager.Player; } }
        public static double Q(Obj_AI_Base target)
        {
            return yo.CalculateDamageOnUnit(target, DamageType.Physical,
                (float)(new double[] { 0, 20, 40, 60, 80, 100 }[Program.Q.Level] + yo.TotalAttackDamage));
        }
        public static float E(Obj_AI_Base target)
        {
            return yo.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)(new[] { 0, 70, 90, 110, 130, 150 }[Program.E.Level] + (0.6 * yo.FlatMagicDamageMod)));
        }
        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * Program._Player.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
        public static bool ShouldEQ(Obj_AI_Base target)
        {
            if (Q(target) >= target.Health || E(target) >= target.Health)
                return false;
            if (Q(target) + E(target) >= target.Health)
                return true;
            return false;
        }
        public static bool IsUnderTurret(Vector2 position)
        {
            Obj_AI_Turret closestTurret = ObjectManager.Get<Obj_AI_Turret>().Where(a =>
                a.Distance(position) <= 875).FirstOrDefault();

            if (closestTurret == null)
                return false;
            return true;
        }
        public static int GetEnemiesKnockedUp()
        {
            int enemiesKU = 0;
            int enemiesIR = 0;
            List<AIHeroClient> enemies = EntityManager.Heroes.Enemies;
            foreach (AIHeroClient enemy in enemies)
            {
                if (yo.Distance(enemy) <= 1200)
                {
                    enemiesIR++;
                    if (enemy.HasBuffOfType(BuffType.Knockup))
                        enemiesKU++;
                }
            }
            return enemiesKU;
        }
        public static Obj_AI_Base GetBestDashMinionToChampion(Obj_AI_Base target, bool EUNDERTURRET)
        {
            Obj_AI_Base minion = ObjectManager.Get<Obj_AI_Base>().Where(a =>
                a.Distance(yo) <= Program.E.Range
                && GetDashingEnd(a).Distance(target) < yo.Distance(target)
                && ((IsUnderTurret(GetDashingEnd(a)) && EUNDERTURRET) || !EUNDERTURRET)
                ).OrderBy(a => GetDashingEnd(a).Distance(target)).FirstOrDefault();

            return minion;
        }

        public static Vector2 GetDashingEnd(Obj_AI_Base target)
        {
            if (!target.IsValidTarget())
            {
                return Vector2.Zero;
            }

            var baseX = Program._Player.Position.X;
            var baseY = Program._Player.Position.Y;
            var targetX = target.Position.X;
            var targetY = target.Position.Y;

            var vector = new Vector2(targetX - baseX, targetY - baseY);
            var sqrt = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

            var x = (float)(baseX + (Program.E.Range * (vector.X / sqrt)));
            var y = (float)(baseY + (Program.E.Range * (vector.Y / sqrt)));

            return new Vector2(x, y);
        }
    }
}
