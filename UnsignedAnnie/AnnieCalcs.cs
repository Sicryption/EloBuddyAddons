using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UnsignedAnnie
{

    class AnnieCalcs
    {
        private static AIHeroClient Annie { get { return ObjectManager.Player; } }
        public static float Q(Obj_AI_Base target)
        {
            return Annie.CalculateDamageOnUnit(target, DamageType.Magical,
                (new float[] { 0, 80, 115, 150, 185, 220 }[Program.Q.Level] + ( 0.8f * Annie.FlatMagicDamageMod)));
        }

        public static float W(Obj_AI_Base target)
        {
            return Annie.CalculateDamageOnUnit(target, DamageType.Magical,
                (new float[] { 0, 70, 115, 160, 205, 250 }[Program.W.Level] + (0.85f * Annie.FlatMagicDamageMod)));
        }
        public static float E(Obj_AI_Base target)
        {
            return Annie.CalculateDamageOnUnit(target, DamageType.Magical,
                (new float[] { 0, 20, 30, 40, 50, 60 }[Program.E.Level] + (0.20f * Annie.FlatMagicDamageMod)));
        }
        public static float R(Obj_AI_Base target)
        {
            return Annie.CalculateDamageOnUnit(target, DamageType.Magical,
                (new float[] { 0, 150, 275, 400 }[Program.R.Level] + (0.65f * Annie.FlatMagicDamageMod)));
        }

        public static float Ignite(Obj_AI_Base target)
        {
            return ((10 + (4 * Program._Player.Level)) * 5) - ((target.HPRegenRate / 2) * 5);
        }
    }
}
