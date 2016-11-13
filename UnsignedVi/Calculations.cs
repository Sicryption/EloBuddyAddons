using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace UnsignedVi
{
    class Calculations
    {
        public static AIHeroClient Vi = Player.Instance;

        public static float Q(Obj_AI_Base target, float chargeTime)
        {
            float dmg = 20 + 20 * Program.Q.Level;
            dmg += 0.6f * (Vi.TotalAttackDamage - Vi.BaseAttackDamage);
            dmg += (Math.Min(chargeTime, 1.25f) / 1.25f) * (20 + 20 * Program.Q.Level);

            if (target.Type == GameObjectType.AIHeroClient)
                dmg *= 1.33f;

            return Vi.CalculateDamageOnUnit(target, DamageType.Physical, dmg);
        }
        public static float W(Obj_AI_Base target)
        {
            return Vi.CalculateDamageOnUnit(target, DamageType.Physical,
                (0.025f + (0.015f * Program.W.Level) + (0.01f * (int)Math.Floor((Vi.TotalAttackDamage - Vi.BaseAttackDamage) / 35))) * target.MaxHealth);
        }
        public static float E(Obj_AI_Base target)
        {
            return Vi.CalculateDamageOnUnit(target, DamageType.Physical,
                -10 + 20 * Program.E.Level 
                + (Vi.TotalAttackDamage * 1.15f)
                + (Vi.TotalMagicalDamage * 0.7f));
        }
        public static float R(Obj_AI_Base target, bool primaryTarget = true)
        {
            float dmg = 150 * Program.R.Level + (1.4f * (Vi.TotalAttackDamage - Vi.BaseAttackDamage));
            if (!primaryTarget)
                dmg *= 0.75f;
            return Vi.CalculateDamageOnUnit(target, DamageType.Physical, dmg);
        }
        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * Vi.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
        public static float Smite()
        {
            return new int[] { 0, 390, 410, 430, 450, 480, 510, 540, 570, 600, 640, 680, 720, 760, 800, 850, 900, 950, 1000 }[Vi.Level];
        }
    }
}
