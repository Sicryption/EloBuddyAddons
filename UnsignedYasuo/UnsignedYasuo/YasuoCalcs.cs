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
        public static double E(Obj_AI_Base target)
        {
            double dmgModifier = 1;
            if (yo.HasBuff("yasuodashscalar"))
                dmgModifier += yo.GetBuff("yasuodashscalar").Count * 0.25f;

            return yo.CalculateDamageOnUnit(target, DamageType.Magical,
                (50 + (20 * Program.E.Level)) + (0.6f * yo.FlatMagicDamageMod));
        }
        public static double Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * yo.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
        public static bool ShouldEQ(Obj_AI_Base target)
        {
            if (Q(target) >= target.Health || E(target) >= target.Health)
                return false;
            if (Q(target) + E(target) >= target.Health)
                return true;
            return false;
        }
        public static bool IsUnderTurret(Vector3 position)
        {
            Obj_AI_Turret closestTurret = EntityManager.Turrets.Enemies.Where(a =>
                a.IsInRange(position, Program.TurretRange)
                && !a.IsDead).OrderBy(a => a.Distance(position)).FirstOrDefault();

            if (closestTurret == null)
                return false;
            return true;
        }
        public static bool IsInFountain(Obj_AI_Base self)
        {
            float fountainRange = 1050;
            Vector3 vec3 = (self.Team == GameObjectTeam.Order) ? new Vector3(363, 426, 182) : new Vector3(14340, 14390, 172);
            return self.IsVisible && self.IsInRange(vec3, fountainRange);
        }
        public static bool ERequirements(Obj_AI_Base unit, bool EUNDERTURRET)
        {
            return !IsInFountain(unit) && !unit.HasBuff("YasuoDashWrapper") && (!IsUnderTurret(GetDashingEnd(unit)) || !EUNDERTURRET);
        }
        public static int GetEnemiesKnockedUp()
        {
            int enemiesKU = 0;
            int enemiesIR = 0;
            List<AIHeroClient> enemies = EntityManager.Heroes.Enemies;
            foreach (AIHeroClient enemy in enemies)
            {
                if (yo.IsInRange(enemy, Program.R.Range))
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
                a.IsInRange(yo, Program.E.Range)
                && GetDashingEnd(a).Distance(target) < yo.Distance(target)
                && ((!IsUnderTurret(GetDashingEnd(a)) && !EUNDERTURRET) || EUNDERTURRET)
                ).OrderBy(a => GetDashingEnd(a).Distance(target)).FirstOrDefault();

            return minion;
        }
        public static Vector3 GetDashingEnd(Obj_AI_Base target)
        {
            if (!target.IsValidTarget())
                return Vector3.Zero;

            return yo.Position.Extend(target, Program.E.Range).To3D();
        }
    }
}
