using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UnsignedEvade
{
    class Geometry
    {
        public static void DrawRectangle(MissileClient missile, SpellInfo info)
        {
            Drawing.DrawLine(missile.StartPosition.WorldToScreen(), CalculateEndPosition(missile, info).WorldToScreen(), 3, System.Drawing.Color.White);
        }

        public static void DrawCircle(MissileClient missile, SpellInfo info)
        {
            Drawing.DrawCircle(missile.StartPosition, info.Radius, System.Drawing.Color.White);
        }

        public static Vector3 CalculateEndPosition(MissileClient missile, SpellInfo info)
        {
            return Extensions.Extend(missile.StartPosition, missile.EndPosition, info.Range).To3DWorld();
        }
    }
}
