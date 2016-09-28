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
        private static AIHeroClient _Player = Program._Player;
        public static double Q(Obj_AI_Base target)
        {
            return _Player.CalculateDamageOnUnit(target, DamageType.Physical,
                (float)(new double[] { 0, 20, 40, 60, 80, 100 }[Program.Q.Level] + _Player.TotalAttackDamage));
        }
        public static double E(Obj_AI_Base target)
        {
            double dmgModifier = 1;
            if (_Player.HasBuff("yasuodashscalar"))
                dmgModifier += _Player.GetBuff("yasuodashscalar").Count * 0.25f;

            return _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (50 + (20 * Program.E.Level)) + (0.6f * _Player.FlatMagicDamageMod));
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
        public static bool IsUnderTurret(Vector3 position)
        {
            Obj_AI_Turret closestTurret = EntityManager.Turrets.Enemies.Where(a =>
                a.IsInRange(position, Program.TurretRange)
                && !a.IsDead).OrderBy(a => a.Distance(position)).FirstOrDefault();

            if (closestTurret == null)
                return false;
            return true;
        }
        public static bool IsInFountain(Vector3 position, GameObjectTeam team)
        {
            float fountainRange = 1050;
            Vector3 vec3 = (team == GameObjectTeam.Order) ? new Vector3(363, 426, 182) : new Vector3(14340, 14390, 172);

            return position.IsInRange(vec3, fountainRange);
        }
        public static bool ERequirements(Obj_AI_Base unit, bool EUNDERTURRET)
        {
            //not in fountain
            if (!IsInFountain(GetDashingEnd(unit), unit.Team) &&
                //can be e'd
                !unit.HasBuff("YasuoDashWrapper") &&
                ((!IsUnderTurret(GetDashingEnd(unit)) && !EUNDERTURRET) || EUNDERTURRET)
                )
                return true;

            return false;
        }
        public static int GetNumEnemiesKnockedUp()
        {
            int enemiesKU = 0;
            List<AIHeroClient> enemies = EntityManager.Heroes.Enemies;
            foreach (AIHeroClient enemy in enemies)
                if (_Player.IsInRange(enemy, Program.R.Range))
                    if (enemy.HasBuffOfType(BuffType.Knockup))
                        enemiesKU++;
            return enemiesKU;
        }
        public static List<AIHeroClient> GetEnemiesKnockedUp()
        {
            List<AIHeroClient> enemiesKnockedUp = new List<AIHeroClient>();
            List<AIHeroClient> enemies = EntityManager.Heroes.Enemies;
                foreach (AIHeroClient enemy in enemies)
                    if (_Player.IsInRange(enemy, Program.R.Range))
                        if (enemy.HasBuffOfType(BuffType.Knockup))
                            enemiesKnockedUp.Add(enemy);
            return enemiesKnockedUp;
        }
        public static bool IsLastKnockUpSecond(AIHeroClient enemy)
        {
            if (enemy == null)
                return false;

            BuffInstance KnockUp = enemy.Buffs.Where(a => a.IsKnockup
            //if the end time is less than or equal to the current time + 0.25 of a second
                && a.EndTime <= Game.Time + 0.25).FirstOrDefault();

            if (KnockUp != null)
                return true;
            return false;
        }
        public static int RadiansToDegrees(float angle)
        {
            return (int)(angle * (180 / Math.PI));
        }
        public static int GetEnemyHeroesInRange(float range)
        {
            int enemiesIR = 0;
            List<AIHeroClient> enemies = EntityManager.Heroes.Enemies;
            foreach (AIHeroClient enemy in enemies)
            {
                if (_Player.IsInRange(enemy, range))
                    enemiesIR++;
            }
            return enemiesIR;
        }
        public static Obj_AI_Base GetBestDashMinionToChampion(Obj_AI_Base target, bool EUNDERTURRET)
        {
            Obj_AI_Base minion = ObjectManager.Get<Obj_AI_Base>().Where(a =>
                a.IsInRange(_Player, Program.E.Range)
                && a != target
                && GetDashingEnd(a).Distance(target) < _Player.Distance(target)
                && ((!IsUnderTurret(GetDashingEnd(a)) && !EUNDERTURRET) || EUNDERTURRET)
                ).OrderBy(a => GetDashingEnd(a).Distance(target)).FirstOrDefault();

            return minion;
        }
        public static Obj_AI_Base GetBestDashEnemyToChampionWithinAARange(Obj_AI_Base target, bool EUNDERTURRET)
        {
            if (target == null)
                return null;
            Obj_AI_Base minion = ObjectManager.Get<Obj_AI_Base>().Where(a =>
                a.IsInRange(_Player, Program.E.Range)
                && a != target
                && GetDashingEnd(a).IsInRange(target, _Player.GetAutoAttackRange())
                && ((!IsUnderTurret(GetDashingEnd(a)) && !EUNDERTURRET) || EUNDERTURRET)
                ).OrderBy(a => GetDashingEnd(a).Distance(target)).FirstOrDefault();

            return minion;
        }
        public static bool WillQBeReady()
        {
            if (Math.Max(0, _Player.Spellbook.GetSpell(SpellSlot.Q).CooldownExpires - Game.Time) <= 0.40f)
                return true;
            else
                return false;
        }
        public static Vector3 GetDashingEnd(Obj_AI_Base target)
        {
            if (!target.IsValidTarget())
                return Vector3.Zero;

            return _Player.Position.Extend(target, Program.E.Range).To3D();
        }
        public static int FindNumEnemyUnitsHitByQ()
        {
            //Prediction.Position.PredictLinearMissile(null, Program.Q.Range, )
            return 0;
       }
        public static int GetAngleBetween(Vector3 a, Vector3 b)
        {
            var dotProd = Vector3.Dot(a, b);
            var lenProd = a.Length() * b.Length();
            var divOperation = dotProd / lenProd;
            return (int)(Math.Acos(divOperation) * (180.0 / Math.PI));
        }
    }
}
