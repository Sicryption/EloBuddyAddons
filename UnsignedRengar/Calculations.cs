using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using System.Linq;

namespace UnsignedRengar
{
    class Calculations
    {
        public static AIHeroClient Rengar = Player.Instance;

        public static float Q(Obj_AI_Base target)
        {
            return Rengar.CalculateDamageOnUnit(target, DamageType.Physical,
                50 + 40 * Program.Q.Level
                + (0.4f + (0.2f * Program.Q.Level)) * (Rengar.BaseAttackDamage - Rengar.TotalAttackDamage));
        }
        public static float EmpQ(Obj_AI_Base target)
        {
            return Rengar.CalculateDamageOnUnit(target, DamageType.Physical,
                104 + 16 * Rengar.Level
                + 2.4f * (Rengar.BaseAttackDamage - Rengar.TotalAttackDamage));
        }
        public static float W(Obj_AI_Base target)
        {
            return Rengar.CalculateDamageOnUnit(target, DamageType.Physical,
                30 + 20 * Program.W.Level
                + 0.8f * Rengar.TotalMagicalDamage);
        }
        public static float EmpW(Obj_AI_Base target)
        {
            return Rengar.CalculateDamageOnUnit(target, DamageType.Physical,
                40 + 10 * Rengar.Level
                + 0.8f * Rengar.TotalMagicalDamage);
        }
        public static float E(Obj_AI_Base target)
        {
            return Rengar.CalculateDamageOnUnit(target, DamageType.Physical,
                50 * Program.E.Level
                + 0.7f * Rengar.TotalAttackDamage - Rengar.BaseAttackDamage);
        }
        public static float EmpE(Obj_AI_Base target)
        {
            return Rengar.CalculateDamageOnUnit(target, DamageType.Physical,
                35 + (15 * Rengar.Level)
                + 0.7f * Rengar.TotalAttackDamage - Rengar.BaseAttackDamage);
        }
        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * Rengar.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
        public static float Smite(Obj_AI_Base target, string type)
        {
            if (target.Type == GameObjectType.AIHeroClient)
            {
                if (type == "red")
                    return 54 + 6 * Rengar.Level;
                else if (type == "blue")
                    return 20 + 8 * Rengar.Level;
                else
                {
                    Console.WriteLine("Smite type: " + type + " does not exist!");
                    return 0;
                }
            }
            else
                return new int[] { 0, 390, 410, 430, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 850, 900, 950, 1000 }[Rengar.Level];
        }
        public static float SmiteHeal()
        {
            return 100 + (Rengar.MaxHealth / 10);
        }
    }
}
