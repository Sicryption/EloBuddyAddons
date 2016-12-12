using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Collections.Generic;
using System;
using SharpDX;

namespace UnsignedEvade
{
    class SpellInfo
    {
        public float Delay = 0.25f,
            Duration,
            MissileMaxSpeed,
            MissileMinSpeed,
            MissileSpeed,
            Radius,
            SecondRadius,
            Width = 10,
            Range,
            CollisionCount = 0,
            ConeDegrees,
            TimeOfCast,
            DangerValue,
            startingAmmoCount = -1f;
        private float travelTime = 0;
        public string ChampionName = "All",
            FriendlyName = "",
            MissileName = "",
            SpellName = "",
            BuffName = "";
        public bool CanVaryInLength = false;
        public string[] OtherMissileNames = new string[0];
        public SpellSlot Slot = SpellSlot.None;
        public CrowdControlType CCtype = CrowdControlType.None;
        public SpellCreationLocation CreationLocation = SpellCreationLocation.None;
        public SpellTypeInfo SpellType = SpellTypeInfo.none;
        public Buff BuffType = Buff.None;
        public Dashtype DashType = Dashtype.None;

        public Vector3 startPosition = Vector3.Zero,
            endPosition = Vector3.Zero,
            startingDirection;
        public GameObject target = null;
        public Obj_AI_Base caster = null;
        public MissileClient missile = null;

        public float TravelTime
        {
            get
            {
                if (travelTime == -1f || MissileSpeed == 0)
                    return 0f;
                else if (travelTime == 0)
                    travelTime = startPosition.Distance(endPosition) / MissileSpeed;
                return travelTime;
            }
            set
            {
                travelTime = value;
            }
        }

        public enum SpellCreationLocation
        {
            OnObjectCreate,
            OnProcessSpell,
            None
        }
        public enum SpellTypeInfo
        {
            none,
            ArcSkillshot,
            LinearSkillshot,
            LinearSkillshotNoDamage,
            LinearMissile,
            LinearSpellWithDuration,
            LinearSpellWithBuff,
            LinearDash,
            TargetedMissile,
            TargetedPassiveSpell,
            TargetedSpell,
            TargetedSpellWithDuration,
            TargetedChannel,
            TargetedDash,
            CircularSkillshot,
            CircularSkillshotDash,
            CircularSpell,
            CircularSpellWithDuration,
            CircularSpellWithBuff,
            BlinkDash,
            PassiveSpell,
            PassiveSpellWithDuration,
            PassiveSpellWithBuff,
            ConeSpell,
            ConeSpellWithBuff,
            SelfActive,
            SelfActiveNoDamage,
            SelfActiveNoDamageWithBuff,
            SelfActiveWithBuff,
            SelfActiveToggleable,
            TargetedActive,
            Wall,
            CircularWall,
            PentaWall,
            AutoAttack,
            AutoAttackWithSplashDamage,
        }

        public enum Dashtype
        {
            Linear,//lucian e, gragas e
            Blink,//ez e, flash
            FixedDistance,//Nid W
            TargetedLinear,//akali r, lee sin w
            Targeted,//lee sin q2, vi r
            None
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
            HeavySlow,//Nasus W
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
            Auto,
            None
        }

        public enum Buff
        {
            AttackDamageIncrease,
            AttackSpeedIncrease,
            AutoAttackRangeIncrease,
            AutoAttackImmune,
            CCRemoval,
            CCImmunity,
            DamageReduction,
            Heal,
            HeavySpeedUp,
            LifestealSpellVamp,
            Invulnerability,
            Pet,
            Shield,
            SpeedUp,
            Slow,
            SpellShield,
            GuardianAngel,
            None
        }

        public string Name()
        {
            string name = FriendlyName;

            if (name == "")
                name = SpellName;
            if (name == "")
                name = MissileName;
            return name;
        }

        public EloBuddy.SpellSlot GetEBSpellSlot()
        {
            if (Slot == SpellSlot.Q)
                return EloBuddy.SpellSlot.Q;
            else if (Slot == SpellSlot.W)
                return EloBuddy.SpellSlot.W;
            else if (Slot == SpellSlot.E)
                return EloBuddy.SpellSlot.E;
            else if (Slot == SpellSlot.R)
                return EloBuddy.SpellSlot.R;
            else
                return EloBuddy.SpellSlot.Unknown;
        }
        public SpellDataInst GetChampionSpell()
        {
            EloBuddy.SpellSlot slot = GetEBSpellSlot();
            if (slot != EloBuddy.SpellSlot.Unknown)
                return caster.Spellbook.GetSpell(slot);

            Console.WriteLine("Champion Spell was null: " + SpellName);
            return null;
        }
    }
}
