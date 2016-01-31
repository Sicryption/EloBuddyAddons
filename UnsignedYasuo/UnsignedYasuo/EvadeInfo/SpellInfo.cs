using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UnsignedEvade
{
    class SpellInfo
    {
        public int Delay,
            MissileMaxSpeed,
            MissileMinSpeed,
            MissileSpeed,
            Radius,
            Width,
            Range,
            CollisionCount;
        public string ChampionName,
            MissileName,
            SpellName;
        public SpellSlot Slot;
        public SkillShotType SkillshotType;
    }
}
