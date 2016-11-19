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

namespace UnsignedGangplank
{

    class Calculations
    {
        public static AIHeroClient GP = Program.Gangplank;
        
        public static float Q(Obj_AI_Base target)
        {
            float qdmg = (-5 + (25 * Program.Q.Level)) + GP.TotalAttackDamage;
            bool hasSheenBuff = GP.HasBuff("sheen");
            bool canUseSheenItem = GP.InventoryItems.Any(a => a.CanUseItem() && (a.Id == ItemId.Trinity_Force || a.Id == ItemId.Lich_Bane || a.Id == ItemId.Iceborn_Gauntlet || a.Id == ItemId.Sheen));

            if (canUseSheenItem || hasSheenBuff)
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
        public static float WHeal()
        {
            return 25 + (25 * Program.W.Level) + (0.9f * GP.TotalMagicalDamage) + (0.15f * GP.MissingHealth());
        }
        public static float RDamagePerWave(Obj_AI_Base target)
        {
            return GP.CalculateDamageOnUnit(target, DamageType.Magical,
                10 + (25 * Program.R.Level)
                + (0.10f * GP.TotalMagicalDamage)
                );
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
        public static float TotalRDamage(Obj_AI_Base target)
        {
            return RDamagePerWave(target) * 12;
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
        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * GP.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
        public static List<Barrel> ChainedBarrels(Barrel barrel)
        {
            return Program.barrels.Where(a => barrel != null && a.barrel.MeetsCriteria() && a.barrel.IsInRange(barrel.barrel, Program.barrelDiameter)).ToList();
        }
        public static int EnemiesHitByBarrel(Barrel barrel, List<Obj_AI_Base> enemies, bool usingQ)
        {
            Menu menu = MenuHandler.mainMenu;
            if (menu.GetComboBoxText("Prediction Type:") == "EloBuddy")
            {
                if (usingQ)
                    return enemies.Where(a => a.Position((int)(CalculateQTimeToTarget(barrel.barrel) * 1000)).IsInRangeOfBarrels(ChainedBarrels(barrel))).Count();
                else
                    return enemies.Where(a => a.Position(250).IsInRangeOfBarrels(ChainedBarrels(barrel))).Count();
            }
            else if (menu.GetComboBoxText("Prediction Type:") == "Current Position")
                return enemies.Where(a => a.IsInRangeOfBarrels(ChainedBarrels(barrel))).Count();

            Chat.Print("This type of prediction doesn't exist!");

            return 0;
        }
        // add accurate Auto-Attack delay time
        public static Barrel getBestBarrel(List<Obj_AI_Base> enemies, bool usingQ, int hitCount)
        {
            List<Barrel> barrelsToAttack = Program.barrels;

            if (usingQ)
                barrelsToAttack = barrelsToAttack.Where(a => a.barrel.IsInRange(GP, Program.Q.Range) && a.TimeAt1HP - Game.Time <= CalculateQTimeToTarget(a.barrel)).ToList();
            else
                barrelsToAttack = barrelsToAttack.Where(a => a.barrel.IsInRange(GP, GP.GetAutoAttackRange(a.barrel)) && a.TimeAt1HP - Game.Time <= 0.25f).ToList();

            return barrelsToAttack.OrderBy(a => EnemiesHitByBarrel(a, enemies, usingQ) >= hitCount).FirstOrDefault();
        }
    }
}
