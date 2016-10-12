using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UnsignedRyze
{

    class RyzeCalcs
    {
        private static AIHeroClient Ryze { get { return ObjectManager.Player; } }
        public static float Q(Obj_AI_Base target)
        {
            float qdmg = new float[] { 0, 60, 85, 110, 135, 160, 185 }[Program.Q.Level]
                + (0.45f * Ryze.FlatMagicDamageMod)
                + (0.03f * BonusMana());

            if (target.HasBuff("RyzeE"))
                qdmg *= new float[] { 1, 1.40f, 1.55f, 1.70f, 1.85f, 2f }[Program.Q.Level];

            return Ryze.CalculateDamageOnUnit(target, DamageType.Magical, qdmg);
        }

        public static float W(Obj_AI_Base target)
        {
           return Ryze.CalculateDamageOnUnit(target, DamageType.Magical,
               (new float[] { 0, 80, 100, 120, 140, 160 }[Program.W.Level]
               + (0.20f * Ryze.FlatMagicDamageMod)
               + (0.01f * BonusMana())
               ));
        }
        public static float E(Obj_AI_Base target)
        {
            return Ryze.CalculateDamageOnUnit(target, DamageType.Magical,
                (new float[] { 0, 50, 75, 100, 125, 150 }[Program.E.Level]
                + (0.30f * Ryze.FlatMagicDamageMod)
                + (0.02f * BonusMana())
                ));
        }

        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * Ryze.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }

        public static float BonusMana()
        {
            int[] ryzeBaseMana = new int[] { 0, 400, 436, 474, 513, 555, 598, 642, 689, 737, 787, 839, 892, 948, 1005, 1063, 1124, 1186, 1250 };

            return Ryze.MaxMana - ryzeBaseMana[Ryze.Level];
        }
    }
}
