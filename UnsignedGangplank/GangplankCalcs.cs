using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

//Exploding a Keg resets Passive CD
//q damage also procs on hit effects, so check trinity force/sheen damage in q calculations
//e ignores 40% of enemies armor. need to add that into calculations
//add deaths daughter and fire at will to gp enhancements to r calculations
//calculate total time needed between Q cast time and Q air time until it reaches bomb and compare it to when the bomb goes on the last tick.
//r if enemy is sieging tower 

namespace UnsignedGP
{
    class GPCalcs
    {
        private static AIHeroClient GP { get { return ObjectManager.Player; } }

        public static float Passive(Obj_AI_Base target)
        {
            return GP.CalculateDamageOnUnit(target, DamageType.True, 20 + (10 * GP.Level) + (GP.TotalAttackDamage - GP.BaseAttackDamage));
        }

        public static float Q(Obj_AI_Base target)
        {
            float qdmg = (-5 + (25 * Program.Q.Level)) + GP.TotalAttackDamage;

            if (GP.HasBuff("sheen") 
                        || (GP.InventoryItems.Where(a => 
                    (a.Id == ItemId.Trinity_Force 
                    || a.Id == ItemId.Lich_Bane 
                    || a.Id == ItemId.Iceborn_Gauntlet 
                    || a.Id == ItemId.Sheen)).FirstOrDefault() != null
                        && GP.InventoryItems.Where(a =>
                   (a.Id == ItemId.Trinity_Force
                   || a.Id == ItemId.Lich_Bane
                   || a.Id == ItemId.Iceborn_Gauntlet
                   || a.Id == ItemId.Sheen)).FirstOrDefault().CanUseItem()))
            {
                if (GP.HasItem(ItemId.Lich_Bane))
                    qdmg += (0.75f * GP.BaseAttackDamage) + (0.5f * GP.FlatMagicDamageMod);
                else if (GP.HasItem(ItemId.Trinity_Force))
                    qdmg += (2f * GP.BaseAttackDamage);
                else if (GP.HasItem(ItemId.Iceborn_Gauntlet))
                    qdmg += GP.BaseAttackDamage;
                else if (GP.HasItem(ItemId.Sheen))
                    qdmg += GP.BaseAttackDamage;
            }

            return GP.CalculateDamageOnUnit(target, DamageType.Physical, qdmg);
        }

        public static float W()
        {
            return (25 + (25 * Program.W.Level) + (0.9f * GP.TotalMagicalDamage) + (0.15f * (GP.MaxHealth - GP.Health)));
        }

        public static float E(Obj_AI_Base target, bool usedQ)
        {
            if (target.Type == GameObjectType.AIHeroClient)
            {
                if (usedQ)
                    return GP.CalculateDamageOnUnit(target, DamageType.Physical, Q(target) + (30 + (30 * Program.E.Level)));
                else
                    return GP.CalculateDamageOnUnit(target, DamageType.Physical, GP.GetAutoAttackDamage(target) + (30 + (30 * Program.E.Level)));
            }
            else
            {
                if (usedQ)
                    return GP.CalculateDamageOnUnit(target, DamageType.Physical, Q(target));
                else
                    return GP.CalculateDamageOnUnit(target, DamageType.Physical, GP.GetAutoAttackDamage(target));
            }
        }

        public static float RDamagePerWave(Obj_AI_Base target)
        {
            return GP.CalculateDamageOnUnit(target, DamageType.Magical,
                10 + (25 * Program.R.Level)
                + (0.10f * GP.TotalMagicalDamage)
                );
        }

        public static float TotalRDamage(Obj_AI_Base target)
        {
            return RDamagePerWave(target) * 12;
        }

        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * GP.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }

        public static bool CastQOnBarrel(Obj_AI_Base barrel)
        {
            if (barrel.Health == 1)
                return Program.Q.Cast(barrel);
            else if (Program.GetBarrelAtPosition(barrel.Position).TimeAt1HP - Game.Time <= CalculateQTimeToTarget(barrel) && Program.GetBarrelAtPosition(barrel.Position).decayRate != 0)
            {
                //Chat.Print("Q Time was: " + CalculateQTimeToTarget(barrel));
                //Chat.Print("Time til bomb explosion was: " + (Program.GetBarrelAtPosition(barrel.Position).TimeAt1HP - Game.Time));
                return Program.Q.Cast(barrel);
            }
            return false;
        }

        public static float CalculateQTimeToTarget(Obj_AI_Base target)
        {
            //float unitspersecond = 3380 / 1;
            float distance = GP.Distance(target);
            float manipulative = 3380 / distance;
            float time = 1 / manipulative;
            time += 0.25f;

            return time;// divide by 1000 to make it in miliseconds
        }
    }
}
