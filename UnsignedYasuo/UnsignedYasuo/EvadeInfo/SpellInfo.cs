using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UnsignedEvade
{
    class SpellInfo
    {
        public int Delay,
            MissileSpeed,
            Width,
            Range,
            CollisionCount;
        public string ChampionName,
            MissileName,
            SpellName;
        public SpellSlot Slot;
        public SpellDatabase.ProjectileType ProjectileType;
        public SpellDatabase.ChannelType ChannelType = SpellDatabase.ChannelType.None;
        public BuffType CCType = BuffType.Internal;
    }
}
