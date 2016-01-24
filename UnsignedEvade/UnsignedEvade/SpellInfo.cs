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
            Range;
        public bool CanHitMultipleMinions,
            CanHitMultipleEnemies;
        public string ChampionName,
            MissileName,
            SpellName;
        public SpellSlot Slot;
        public SkillShotType SkillshotType;

        //Targeted
        public SpellInfo()
        {

        }

        //Skillshot
        public SpellInfo(string championName, string spellName, SkillShotType SkillShotType, SpellSlot slot, int range, int delay = 0, 
            int missileMaxSpeed = 0, int missileMinSpeed = 0, int missileSpeed = 0, int radius = 0)
        {

        }
    }
}
