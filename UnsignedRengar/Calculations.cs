using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace UnsignedRengar
{
    class Calculations
    {
        public static AIHeroClient Rengar = Player.Instance;

        public static float Q(Obj_AI_Base target)
        {
            return Rengar.CalculateDamageOnUnit(target, DamageType.Physical,
                50 + 40 * Program.Q.Level
                + (0.4f + 0.2f * Program.Q.Level) * (Rengar.BaseAttackDamage - Rengar.TotalAttackDamage));
        }
        public static float EmpQ(Obj_AI_Base target)
        {
            return Rengar.CalculateDamageOnUnit(target, DamageType.Physical,
                104 + 16 * Program.Q.Level
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
                40 + 10 * Program.W.Level
                + 0.8f * Rengar.TotalMagicalDamage);
        }
    }
}
