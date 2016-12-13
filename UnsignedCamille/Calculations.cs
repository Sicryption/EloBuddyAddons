using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace UnsignedCamille
{
    class Calculations
    {
        public static AIHeroClient Camille => Player.Instance;

        public static float Q1(Obj_AI_Base target)
        {
            float damage = Camille.TotalAttackDamage * (0.15f + (0.05f * Program.Q.Level));
            damage += Camille.GetAutoAttackDamage(target);

            return Camille.CalculateDamageOnUnit(target, DamageType.Physical, damage);
        }
        public static float Q2(Obj_AI_Base target, bool chargedQ)
        {
            float percentOfDamageAsTrueDamage = 0.55f + (0.03f * Program.W.Level),
                percentOfDamageAsRegularDamage = 1 - percentOfDamageAsTrueDamage,
                damage = Camille.TotalAttackDamage * 0.2f;

            if (chargedQ)
                damage *= 2;

            float regularDamage = Camille.CalculateDamageOnUnit(target, DamageType.Physical, Camille.GetAutoAttackDamage(target) + damage * percentOfDamageAsRegularDamage),
                trueDamage = Camille.CalculateDamageOnUnit(target, DamageType.True, damage * percentOfDamageAsTrueDamage);

            return regularDamage + trueDamage;
        }
        public static float W(Obj_AI_Base target)
        {
            float damage = 35 + (30 * Program.W.Level) + Camille.BonusAttackDamage() * 0.62f;
            
            damage += target.MaxHealth * (0.055f + (Program.W.Level * 0.005f) + (Camille.BonusAttackDamage() / 2500f));

            return Camille.CalculateDamageOnUnit(target, DamageType.Physical, damage);
        }
        public static float E2(Obj_AI_Base target)
        {
            float damage = 25 + (45 * Program.E.Level) + Camille.BonusAttackDamage() * 0.75f;

            return Camille.CalculateDamageOnUnit(target, DamageType.Physical, damage);
        }
        public static float RBasicAttack(Obj_AI_Base target)
        {
            float bonusDamage = 5 + (0.04f * target.Health);

            return Camille.CalculateDamageOnUnit(target, DamageType.Physical, bonusDamage);
        }
        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * Camille.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
        public static float Smite(Obj_AI_Base target, string type)
        {
            if (target.Type == GameObjectType.AIHeroClient)
            {
                if (type == "red")
                    return 54 + 6 * Camille.Level;
                else if (type == "blue")
                    return 20 + 8 * Camille.Level;
                else
                {
                    Console.WriteLine("Smite type: " + type + " does not exist!");
                    return 0;
                }
            }
            else
                return new int[] { 0, 390, 410, 430, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 850, 900, 950, 1000 }[Camille.Level];
        }
        public static float SmiteHeal()
        {
            return 100 + (Camille.MaxHealth / 10);
        }
    }
}
