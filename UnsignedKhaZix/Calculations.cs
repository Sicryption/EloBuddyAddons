using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace KhaZix
{
    class Calculations
    {
        public static AIHeroClient Kha = Player.Instance;

        public static float Q(Obj_AI_Base target)
        {
            float dmg = 45 + 25 * Program.Q.Level;
            dmg += 1.4f * (Kha.TotalAttackDamage - Kha.BaseAttackDamage);

            if (target.IsIsolated())
                dmg *= 1.5f;

            return Kha.CalculateDamageOnUnit(target, DamageType.Physical, dmg);
        }
        public static float W(Obj_AI_Base target)
        {
            return Kha.CalculateDamageOnUnit(target, DamageType.Physical,
                50 + 30 * Program.W.Level
                + (Kha.TotalAttackDamage - Kha.BaseAttackDamage));
        }

        public static float WHeal()
        {
            return 45 + 15 * Program.W.Level
                + (0.5f * Kha.TotalMagicalDamage);
        }
        public static float E(Obj_AI_Base target)
        {
            return Kha.CalculateDamageOnUnit(target, DamageType.Physical,
                30 + 35 * Program.E.Level
                + (0.2f * (Kha.TotalAttackDamage - Kha.BaseAttackDamage)));
        }
        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * Kha.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
        public static float Smite(Obj_AI_Base target, string type)
        {
            if (target.Type == GameObjectType.AIHeroClient)
            {
                if (type == "red")
                    return 54 + 6 * Kha.Level;
                else if (type == "blue")
                    return 20 + 8 * Kha.Level;
                else
                {
                    Console.WriteLine("Smite type: " + type + " does not exist!");
                    return 0;
                }
            }
            else
                return new int[] { 0, 390, 410, 430, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 850, 900, 950, 1000 }[Kha.Level];
        }
        public static float SmiteHeal()
        {
            return 100 + (Kha.MaxHealth / 10);
        }
    }
}