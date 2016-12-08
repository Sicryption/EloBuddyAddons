using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Spells;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UnsignedEvade
{
    class CustomPolygon
    {
        public Geometry.Polygon polygon;
        public SpellInfo info;
        
        public CustomPolygon(Geometry.Polygon poly, SpellInfo spellInfo)
        {
            polygon = poly;
            info = spellInfo;
        }

        public float TimeUntilHitsChampion(AIHeroClient champion)
        {
            if (SpellDatabase.GetSpellInfo(info.SpellName).MissileName == "")
                return Math.Max((info.TimeOfCast + info.Delay) - Game.Time, 0);
            else
            {

                float distance = 0;
                if (info.missile == null)
                    distance = info.startPosition.Distance(champion);
                else
                    distance = info.missile.Distance(champion);
                
                float time = 1000 * distance / info.MissileSpeed;
                //Spell.Delay + 1000 * (StartPosition.Distance(EndPosition) / Spell.Speed)
                if (info.missile == null)
                    time += Math.Max((info.TimeOfCast + info.Delay) - Game.Time, 0) * 1000;

                return time;// divide by 1000 to make it in miliseconds
            }
        }
    }
}
