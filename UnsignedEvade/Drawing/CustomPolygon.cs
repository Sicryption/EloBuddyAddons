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
using System.Linq;

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
            return TimeUntilHitsPosition(champion.Position);
        }
        public float TimeUntilHitsPosition(Vector3 pos)
        {
            if (info == null)
            {
                //particles fall into play here
                Chat.Print("This Spell Does Not Have A Spell Info | TimeUntilHitsPos");
                return 0;
            }

            if (SpellDatabase.GetSpellInfo(info.SpellName).MissileName == "")
                return Math.Max((info.TimeOfCast + info.Delay) - Game.Time, 0) * 1000;
            else
            {
                float distance = 0;
                if (info.missile == null)
                    distance = info.startPosition.Distance(pos);
                else
                    distance = info.missile.Distance(pos);

                float time = 1000 * distance / info.MissileSpeed;
                //Spell.Delay + 1000 * (StartPosition.Distance(EndPosition) / Spell.Speed)
                if (info.missile == null)
                    time += Math.Max((info.TimeOfCast + info.Delay) - Game.Time, 0) * 1000;

                return time;// divide by 1000 to make it in miliseconds
            }
        }
        public bool ShouldBeEvaded()
        {
            return info.ShouldBeAccountedFor();
        }
        public Vector3 PositionInTime(float time)
        {
            if(info == null)
            {
                //particles fall into play here
                Chat.Print("This Spell Does Not Have A Spell Info | PosInTime");
                return Vector3.Zero;
            }

            if (SpellDatabase.GetSpellInfo(info.SpellName).MissileName == "")
            {
                //if time of spell is after the spell delay, then the position is the end
                //this needs to be done
                return info.endPosition;
            }
            else
            {
                Vector3 pos = info.missile.Position;
                Vector3 direction = info.endPosition;

                float distance = time * info.MissileSpeed / 1000;
                return pos.Extend(direction, distance).To3DFromNavMesh();
            }
        }
        public float GetDangerValue()
        {
            Menu menu = MenuHandler.championMenus.Where(a => a.UniqueMenuId.Contains(info.ChampionName)).FirstOrDefault();

            //spells like recall/tp/items
            if (menu == null)
                return -1;

            return menu.GetSliderValue(info.Name() + " Danger Value");
        }
    }
}
