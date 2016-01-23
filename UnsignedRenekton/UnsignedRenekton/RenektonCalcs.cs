using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UnsignedRenekton
{
    class RenektonCalcs
    {
        private static AIHeroClient Renekton { get { return ObjectManager.Player; } }
        public static float Q(Obj_AI_Base target)
        {
            float qdmg = Renekton.CalculateDamageOnUnit(target, DamageType.Physical,
                30 + (30 * Program.Q.Level) + (0.8f * (Renekton.TotalAttackDamage - Renekton.BaseAttackDamage)));

            if (Renekton.Mana >= 50)
                qdmg = qdmg * 1.5f;

            return qdmg;
        }
        public static float W(Obj_AI_Base target)
        {
            float wdmg = Renekton.CalculateDamageOnUnit(target, DamageType.Physical,
                -10 + (20 * Program.Q.Level) + (1.5f * Renekton.TotalAttackDamage));

            if (Renekton.Mana >= 50)
                wdmg = wdmg * 1.5f;

            return wdmg;
        }

        public static float Slice(Obj_AI_Base target)
        {
            return Renekton.CalculateDamageOnUnit(target, DamageType.Physical,
                (30 * Program.E.Level) + (0.8f * (Renekton.TotalAttackDamage - Renekton.BaseAttackDamage)));
        }
        public static float Dice(Obj_AI_Base target)
        {
            float edmg = Slice(target);

            if (Renekton.Mana >= 50)
                edmg = edmg * 1.5f;

            return edmg;
        }

        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * Program._Player.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
    }
}
