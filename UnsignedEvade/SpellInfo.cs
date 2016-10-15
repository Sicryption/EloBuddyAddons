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
        public string ChampionName = "",
            MissileName = "",
            SpellName = "";
        public SpellSlot Slot;
        public SpellTypeInfo SpellType;
        public CrowdControlType CCtype;

        public enum SpellTypeInfo
        {
            CircularSkillshot,
            LinearSkillshot,
            ArcSkillshot,
            SelfActive,
            PassiveActive,
            Targeted,
            Wall,
            MovingWall,
            ConeSkillshot,
        }
        public enum CrowdControlType
        {
            KnockAside,//Velkoz E/Dragon
            KnockUp,//Aatrox Q, Malphite Ult
            KnockBack,//Tristana R
            Pull,//Thresh Q/Blitz Q
            Blind,//Teemo Q
            Entangle,//Amumu R
            Polymorph,//Lulu
            Charm,//Ahri E
            Fear,//Fiddlesticks Q
            Taunt,//Rammus E/Galio R
            Ground,//Cassiopeia W
            Nearsight,//Quinn Q/ Graves E
            Root,//Cait E, Rengar E, Lux Q
            Silence,//Soraka E, Fidds E
            Slow,//Janna W
            Statis,//Aatrox Passive
            Stun,//Alistar Q, Xerath E, Riven W
            Suspension,//Yasuo R
            Suppression,//Malzahar R, Warwick R
            None
        }

        public enum SpellSlot
        {
            Q,
            W,
            E,
            R,
            Auto
        } 
    }
}
