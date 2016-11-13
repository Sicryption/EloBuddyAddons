using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace UnsignedJarvanIV
{
    class Calculations
    {
        public static AIHeroClient JarvanIV = Player.Instance;

        public static float Q(Obj_AI_Base target)
        {
            float dmg = 25 + 45 * Program.Q.Level;
            dmg += 1.2f * (JarvanIV.TotalAttackDamage - JarvanIV.BaseAttackDamage);

            if (target.Type == GameObjectType.AIHeroClient)
                dmg *= 1.33f;

            return JarvanIV.CalculateDamageOnUnit(target, DamageType.Physical, dmg);
        }
        public static float E(Obj_AI_Base target)
        {
            return JarvanIV.CalculateDamageOnUnit(target, DamageType.Physical,
                15 + 45 * Program.E.Level 
                + (JarvanIV.TotalMagicalDamage * 0.8f));
        }
        public static float R(Obj_AI_Base target, bool primaryTarget = true)
        {
            float dmg = 75 + 125 * Program.R.Level;
            dmg += 1.5f * (JarvanIV.TotalAttackDamage - JarvanIV.BaseAttackDamage);
            return JarvanIV.CalculateDamageOnUnit(target, DamageType.Physical, dmg);
        }
        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * JarvanIV.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
        public static float Smite()
        {
            return new int[] { 0, 390, 410, 430, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 850, 900, 950, 1000 }[JarvanIV.Level];
        }
    }
}
