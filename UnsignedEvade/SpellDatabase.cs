using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Linq;
using System.Collections.Generic;
using SharpDX;

namespace UnsignedEvade
{
    static class SpellDatabase
    {
        public static List<SpellInfo> SpellList = new List<SpellInfo>();

        #region Existing Spell List
        static List<SpellInfo> SpellInfoList = new List<SpellInfo>()
        {
            #region AllChampions
            //Ludens Echo
            new SpellInfo()
            {
                MissileName = "ItemMagicShankMis",
                MissileSpeed = 1400f,
                MissileMinSpeed = 1400f,
                MissileMaxSpeed = 1400f,
                Range = 725f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Tiamat
            new SpellInfo()
            {
                SpellName = "ItemTiamatCleave",
                Range = 325f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Protobelt
            new SpellInfo()
            {
                SpellName = "ItemSoFBoltSpellBase",
                MissileName = "ItemSoFBoltSpellMissile",
                Range = 850f,
                MissileSpeed = 1600f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
                Width = 75f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                DashType = SpellInfo.Dashtype.Linear,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Edge of Night
            new SpellInfo()
            {
                SpellName = "ItemVeilChannel",
                Range = 500f,
                MissileSpeed = 828.5f,
                MissileMinSpeed = 2.079758E-34f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 500f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //BOTRK
            new SpellInfo()
            {
                SpellName = "ItemSwordOfFeastAndFamine",
                MissileName = "ItemSwordOfFeastAndFamineTransfuse",
                MissileSpeed = 1200f,
                MissileMinSpeed = 1200f,
                MissileMaxSpeed = 1200f,
                Range = 550f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
            },
            //Seraphs Embrace Shield
            new SpellInfo()
            {
                SpellName = "ItemSeraphsEmbrace",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Pink Vision Item to give enemies of invis traps on acensions
            new SpellInfo()
            {
                SpellName = "OracleExtractSight",
                Range = 400f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Zhonyas 
            new SpellInfo()
            {
                SpellName = "ZhonyasHourglass",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Zhonyas
            new SpellInfo()
            {
                SpellName = "BilgewaterCutlass",
                Range = 550f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
            },
            //BOTRK
            new SpellInfo()
            {
                SpellName = "RanduinsOmen",
                Range = 500f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Slow,
            },
            //Redemption
            new SpellInfo()
            {
                SpellName = "ItemRedemption",
                Range = 5500f,//not sure on this. have to actually use the item to test
                Radius = 550f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //recall
            new SpellInfo()
            {
                SpellName = "recall",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Teleport
            new SpellInfo()
            {
                SpellName = "SummonerTeleport",
                Range = int.MaxValue,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Smite
            new SpellInfo()
            {
                SpellName = "SummonerSmite",
                Range = 500f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Flash
            new SpellInfo()
            {
                SpellName = "SummonerFlash",
                Range = 425,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                DashType = SpellInfo.Dashtype.Blink,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Ignite
            new SpellInfo()
            {
                SpellName = "SummonerDot",
                Range = 600,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Heal
            new SpellInfo()
            {
                SpellName = "SummonerHeal",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Exhaust
            new SpellInfo()
            {
                SpellName = "SummonerExhaust",
                Range = 650f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Heal
            new SpellInfo()
            {
                SpellName = "SummonerBoost",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Youmuus
            new SpellInfo()
            {
                SpellName = "YoumusBlade",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Ghost
            new SpellInfo()
            {
                SpellName = "SummonerHaste",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Barrier
            new SpellInfo()
            {
                SpellName = "SummonerBarrier",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Blue Buff something?
            new SpellInfo()
            {
                MissileName = "CrestOfTheAncientGolemLines",
                MissileSpeed = 2200f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
                Range = 1200f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Hextech GLP
            new SpellInfo()
            {
                MissileName = "ItemWillBoltSpellMissile",
                MissileSpeed = 4000f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 4000f,
                Range = 1000f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
            },
            #endregion
            //fix aatrox e and give W buff name
            #region Aatrox
            //q
            CreateCircularSkillshotDash("AatroxQ", "Aatrox", 650f, 285f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp),
            //w
            CreatePassiveSpell("AatroxW", "Aatrox", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            CreatePassiveSpell("AatroxW2", "Aatrox", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //e
            
            new SpellInfo()
            {
                /*ChampionName = "Aatrox",
                Range = 1050f,
                MissileSpeed = 1200f,
                MissileMinSpeed = 1200f,
                MissileMaxSpeed = 1200f,
                Width = 15f,
                ConeDegrees = 45f,
                OtherMissileNames = new string[] { "AatroxEConeMissile", "AatroxEConeMissile2" },
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,*/
            },
            //r
            CreateSelfActive("AatroxR", "Aatrox", 550f, SpellInfo.SpellSlot.R),
            CreatePassiveSpell("AatroxR", "Aatrox", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            CreatePassiveSpell("AatroxR", "Aatrox", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AutoAttackRangeIncrease),
            #endregion
            #region Ahri
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Charm,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r (bolts, not he dash itself)
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //get akali q projectile. Add new Akali W
            #region Akali
            //q
            CreateTargetedMissile("AkaliMota", "null", "Akali", 600f, 1000f, 1000f, 1000f, SpellInfo.SpellSlot.Q),
            //w
            CreateCircularSpell("AkaliSmokeBomb", "Akali", 8f, 250f, 475f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.SpeedUp, SpellInfo.CrowdControlType.Slow),
            //e
            CreateSelfActive("AkaliShadowSwipe", "Akali", 325f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedDash("AkaliShadowDance", "Akali", 400f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.TargetedLinear),
            #endregion
            #region Alistar
            //q
            CreateSelfActive("Pulverize", "Alistar", 365f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp),
            //w
            CreateTargetedDash("Headbutt", "Alistar", 650f, SpellInfo.SpellSlot.W, SpellInfo.Dashtype.TargetedLinear, SpellInfo.CrowdControlType.KnockBack),
            //e
            CreateSelfActiveNoDamage("AlistarE", "Alistar", 350f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //r
            CreatePassiveSpell("FerociousHowl", "Alistar", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.CCRemoval),
            CreatePassiveSpell("FerociousHowl", "Alistar", 7, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            #endregion
            #region Amumu
            //q
            CreateLinearSkillshot("BandageToss", "SadMummyBandageToss", "Amumu", 1100f, 2000f, 2000f, 2000f, 80f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateToggleableSelfActive("AuraofDespair", "Amumu", "AuraofDespair", 300f, SpellInfo.SpellSlot.W),
            //e
            CreateSelfActive("Tantrum", "Amumu", 350f, SpellInfo.SpellSlot.E),
            //r
            CreateSelfActive("CurseoftheSadMummy", "Amumu", 550f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Entangle),
            #endregion
            //anivia r scaling size (needs buff name and to be removed from particle database
            #region Anivia
            //q
            CreateLinearSkillshot("FlashFrost", "FlashFrostSpell", "Anivia", 1100f, 850f, 850f, 850f, 110f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w Handled Under Particle Database
            /*new SpellInfo()
            {
                SpellName = "Crystallize",
                ChampionName = "Anivia",
                Range = 1000f,
                MissileSpeed = 1600f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
                Width = 0f,
                Radius = 100f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.W,
            },*/
            //e
            CreateTargetedMissile("Frostbite", "Frostbite", "Anivia", 600f, 1600f, 1600f, 1600f, SpellInfo.SpellSlot.E),
            //r Handled under Particle Darabase
            CreateCircularSpell("GlacialStorm", "Anivia", "null", 750f, 200f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Slow),
            #endregion
            //do annie Q/W/R stun with buff. Add Annie E
            #region Annie
            //aa
            CreateAutoAttack("AnnieBasicAttack", "Annie", 1200f, 1200f, 1200f),
            CreateAutoAttack("AnnieBasicAttack2", "Annie", 1200f, 1200f, 1200f),
            //q
            CreateTargetedMissile("Disintegrate", "Disintegrate", "Annie", 625f, 1400f, 1400f, 1400f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateConeSpell("Incinerate", "Annie", 600f, 24.76f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e
            CreatePassiveSpell("null", "Annie", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            //r
            CreateCircularSpell("InfernalGuardian", "Annie", 600f, 250f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.Pet, SpellInfo.CrowdControlType.Stun),
            #endregion
            //add ashe basic attack having a slow, change E to not draw or dodge
            #region Ashe
            //q
            CreatePassiveSpell("AsheQ", "Ashe", SpellInfo.SpellSlot.Q),
            //w
            CreateLinearSkillshot("Volley", "VolleyAttack", "Ashe", 1150f, 1500f, 1500f, 1500f, 20f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("Volley", "VolleyCenterAttack", "Ashe", 1150f, 1500f, 1500f, 1500f, 20f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("Volley", "VolleyRightAttack", "Ashe", 1150f, 1500f, 1500f, 1500f, 20f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("Volley", "VolleyAttackWithSound", "Ashe", 1150f, 1500f, 1500f, 1500f, 20f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            //e
            CreateLinearSkillshot("AsheSpiritOfTheHawk", "AsheSpiritOfTheHawk", "Ashe", int.MaxValue, 1400f, 1400f, 1400f, 5f, SpellInfo.SpellSlot.E),            //r
            //r
            CreateLinearSkillshot("EnchantedCrystalArrow", "EnchantedCrystalArrow", "Ashe", int.MaxValue, 1600f, 1600f, 1600f, 130f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
            #endregion
            #region Aurelion Sol
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //completely change Azir
            #region Azir
            //q
            new SpellInfo()
            {
                SpellName = "AzirQ",
                ChampionName = "Azir",
                Range = 875f,
                MissileSpeed = 500f,
                MissileMinSpeed = 2.305022E-38f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 0f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "AzirW",
                ChampionName = "Azir",
                Range = 450f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "AzirE",
                ChampionName = "Azir",
                Range = 1100f,
                MissileSpeed = 500f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "AzirR",
                ChampionName = "Azir",
                Range = 250f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                SpellType = SpellInfo.SpellTypeInfo.MovingWall,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //bard Q stun conditions, treat W as projectile, and E escape, and R land time
            #region Bard
            //q
            CreateLinearSkillshot("BardQ", "BardQ", "Bard", 950f, 1500f, 1500f, 1500f, 60f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            CreateLinearSkillshot("BardQ", "BardQMissile2", "Bard", 950f, 1500f, 1500f, 1500f, 60f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w on champion
            new SpellInfo()
            {
                SpellName = "BardWDirectHeal",
                ChampionName = "Bard",
                Range = 800f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w on ground
            new SpellInfo()
            {
                SpellName = "BardW",
                ChampionName = "Bard",
                Range = 800f,
                Radius = 100f,
                OtherMissileNames = new string[] { "BardWHealthPack", },
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e Not sure how to handle this
            new SpellInfo()
            {
                SpellName = "BardE",
                ChampionName = "Bard",
                //Range = 900f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            CreateCircularSpell("BardR", "Bard", 3400f, 350f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Statis),
            #endregion
            #region Blitzcrank
            //q
            CreateLinearSkillshot("RocketGrab", "RocketGrabMissile", "Blitzcrank", 1050f, 1800f, 1800f, 1800f, 70f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Pull),
            //w
            CreatePassiveSpell("Overdrive", "Blitzcrank", 5f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            CreatePassiveSpell("Overdrive", "Blitzcrank", 5f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            //e
            CreatePassiveSpell("PowerFist", "Blizcrank", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockUp),
            //r
            CreateSelfActive("StaticField", "Blitzcrank", 600f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Silence),
            #endregion
            //add brand passive. Brand Q stun calculations
            #region Brand
            //q
            CreateLinearSkillshot("BrandQ", "BrandQMissile", "Brand", 1100f, 1600f, 2000f, 2000f, 60f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateCircularSpell("BrandW", "Brand", 0.625f, 900f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.None),
            //e
            CreateTargetedSpell("BrandE", "Brand", 675f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedMissile("BrandR", "BrandRMissile", "Brand", 750f, 750f, 250f, 3000f, SpellInfo.SpellSlot.R),
            CreateTargetedMissile("BrandR", "BrandR", "Brand", 750f, 750f, 250f, 3000f, SpellInfo.SpellSlot.R),
            #endregion
            //add braum aa passive stun
            #region Braum
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //fix caitlyn's Q's end, not all cait traps being deleted when dead (might have fixed traps), R has scaling range 2000/2500/3000
            #region Caitlyn
            //aa
            CreateAutoAttack("CaitlynBasicAttack", "Caitlyn", 2500f, 2500f, 2500f),
            CreateAutoAttack("CaitlynHeadshotMissile", "Caitlyn", 3000f, 2500f, 2500f),
            //q
            CreateLinearSkillshot("CaitlynPiltoverPeacemaker", "CaitlynPiltoverPeacemaker", "Caitlyn", 1250f, 2000f, 2000f, 2000f, 60f, SpellInfo.SpellSlot.Q),
            CreateLinearSkillshot("CaitlynPiltoverPeacemaker", "CaitlynPiltoverPeacemaker2", "Caitlyn", 1250f, 2000f, 2000f, 2000f, 90f, SpellInfo.SpellSlot.Q),
            //w handled as trap
            /*new SpellInfo()
            {
                SpellName = "CaitlynYordleTrap",
                ChampionName = "Caitlyn",
                Range = 800f,
                MissileSpeed = 1450f,
                MissileMinSpeed = 1450f,
                MissileMaxSpeed = 1450f,
                Width = 0f,
                Radius = 75f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },*/
            //e
            CreateLinearSkillshot("CaitlynEntrapment", "CaitlynEntrapmentMissile", "Caitlyn", 800f, 1600f, 2000f, 2000f, 70f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateTargetedMissile("CaitlynAceintheHole", "CaitlynAceintheHoleMissile", "Caitlyn", 2000f, 3200f, 2200f, 2200f, SpellInfo.SpellSlot.R),
            #endregion
            #region Cassiopeia
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Ground,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //chogath scaling R range
            #region ChoGath
            //q
            CreateCircularSpell("Rupture", "Chogath", 0.625f, 950f, 250f, true, SpellInfo.SpellSlot.Q, SpellInfo.Buff.None, SpellInfo.CrowdControlType.KnockUp),
            //w
            CreateConeSpell("FeralScream", "Chogath", 300f, 28f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Silence),
            //e
            CreateAutoAttack("VorpalSpikesMissle", "Chogath", 1475f, 1475f, 1475f),
            CreateAutoAttack("VorpalSpikesMissle2", "Chogath", 1475f, 1475f, 1475f),
            CreateAutoAttack("VorpalSpikesMissle3", "Chogath", 1475f, 1475f, 1475f),
            CreateAutoAttack("VorpalSpikesMissle4", "Chogath", 1475f, 1475f, 1475f),
            CreateAutoAttack("VorpalSpikesMissle5", "Chogath", 1475f, 1475f, 1475f),
            CreateAutoAttack("VorpalSpikesMissle6", "Chogath", 1475f, 1475f, 1475f),
            CreateAutoAttack("VorpalSpikesMissle7", "Chogath", 1475f, 1475f, 1475f),
            CreateAutoAttack("VorpalSpikesMissle8", "Chogath", 1475f, 1475f, 1475f),
            //r
            CreateTargetedSpell("Feast", "Chogath", 175f, SpellInfo.SpellSlot.R),
            #endregion
            #region Corki
            //aa
            CreateAutoAttack("CorkiBasicAttack", "Corki", 2000f, 2000f, 2000f),
            CreateAutoAttack("CorkiBasicAttack2", "Corki", 2000f, 2000f, 2000f),
            //q
            CreateCircularSkillshot("PhosphorusBomb", "PhosphorusBombMissile", "Corki", 825f, 1000f, 1000f, 1000f, 250f, true, SpellInfo.SpellSlot.Q),
            CreateCircularSkillshot("PhosphorusBomb", "PhosphorusBombMissileMin", "Corki", 825f, 1000f, 1000f, 1000f, 250f, true, SpellInfo.SpellSlot.Q),
            //w,  burn handled by particle database
            CreateLinearDash("CarpetBomb", "Corki", 600f, 200f, SpellInfo.SpellSlot.W),
            CreateLinearDash("CarpetBombMega", "Corki", 600f, 200f, SpellInfo.SpellSlot.W),
            CreateLinearDash("CarpetBomb2", "Corki", 600f, 200f, SpellInfo.SpellSlot.W),
            CreateLinearDash("CarpetBombMega2", "Corki", 600f, 200f, SpellInfo.SpellSlot.W),
            //e
            CreateConeSpell("GGun", "Corki", "GGun", 600f, 28f, SpellInfo.SpellSlot.E),
            //r
            CreateLinearSkillshot("MissileBarrageMissile", "MissileBarrageMissile", "Corki", 1300f, 2000f, 2000f, 2000f, 40f, SpellInfo.SpellSlot.R),
            CreateLinearSkillshot("MissileBarrageMissile2", "MissileBarrageMissile2", "Corki", 1500f, 2000f, 2000f, 2000f, 40f, SpellInfo.SpellSlot.R),
            #endregion
            //add darius Q/W
            #region Darius
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            CreateConeSpell("DariuxAxeGrabCone", "Darius", 550f, 25f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Pull),
            //r
            CreateTargetedSpell("DariusExecute", "Darius", 475f, SpellInfo.SpellSlot.R),
            #endregion
            #region Diana
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ArcSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Pull,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region DrMundo
            //q
            CreateLinearSkillshot("InfectedCleaverMissile", "InfectedCleaverMissile", "DrMundo", 1050f, 2000f, 2000f, 2000f, 60f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateToggleableSelfActive("BurningAgony", "DrMundo", "BurningAgony", 325f, SpellInfo.SpellSlot.W),
            //e
            CreatePassiveSpell("Masochism", "DrMundo", SpellInfo.SpellSlot.E),
            //r
            CreatePassiveSpell("Sadism", "DrMundo", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            #endregion
            #region Draven
            //q
            CreatePassiveSpell("DravenSpinning", "Draven", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //w
            CreatePassiveSpell("DravenFury", "Draven", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //e
            CreateLinearSkillshot("DravenDoubleShot", "DravenDoubleShotMissile", "Draven", 1400f, 1400f, 1400f, 1400f, 130f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockBack),
            //r
            CreateLinearSkillshot("DravenRCast", "DravenR", "Draven", int.MaxValue, 2000f, 2000f, 2000f, 160f, SpellInfo.SpellSlot.R),
            #endregion  
            //Ekko R location and W delay
            #region Ekko
            //q
            CreateLinearSkillshot("EkkoQ", "EkkoQMis", "Ekko", 1075f, 1650f, 1650f, 1650f, 60f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("EkkoQ", "EkkoQReturn", "Ekko", 1075f, 1650f, 1650f, 1650f, 60f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("EkkoQ", "EkkoQReturnDead", "Ekko", 1075f, 1650f, 1650f, 1650f, 60f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateCircularSkillshot("EkkoW", "EkkoWMis", "Ekko", 1600f, 1500f, 1500f, 1500f, 400f, true, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e
            CreateLinearDash("EkkoE", "Ekko", 325f, 25, SpellInfo.SpellSlot.E),
            CreateTargetedDash("EkkoEAttack", "Ekko", 425f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Targeted),
            //r
            CreateCircularSpell("EkkoR", "Ekko", int.MaxValue, 375f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.Heal),
            #endregion
            #region Elise
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q spider
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w spider
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e spider
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Evelynn
            //q
            CreateTargetedSpell("HateSpike", "Evelynn", 500f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("ShadowWalk", "Evelynn", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //e
            CreateTargetedSpell("Ravage", "Evelynn", 225f, SpellInfo.SpellSlot.Q),
            //r
            CreateCircularSpell("MaliceandSpite", "Evelynn", 650f, 250f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.Shield, SpellInfo.CrowdControlType.HeavySlow),
            #endregion
            #region Ezreal
            //q
            CreateLinearSkillshot("EzrealMysticShot", "EzrealMysticShotMissile", "Ezreal", 1200f, 2000f, 2000f, 2000f, 80f, SpellInfo.SpellSlot.Q),
            //w
            CreateLinearSkillshot("EzrealEssenceFluxMissile", "EzrealEssenceFluxMissile", "Ezreal", 1050f, 1600f, 1600f, 1600f, 80f, SpellInfo.SpellSlot.W),
            //e
            CreateBlinkDash("EzrealArcaneShift", "Ezreal", 475f, SpellInfo.SpellSlot.E),
            //r
            CreateLinearSkillshot("EzrealTrueshotBarrage", "EzrealTrueshotBarrage", "Ezreal", int.MaxValue, 2000f, 2000f, 2000f, 160f, SpellInfo.SpellSlot.W),
            #endregion
            //fiddlesticks buff name
            #region Fiddlesticks
            //q
            CreateTargetedSpell("Terrify", "FiddleSticks", 525f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Fear),
            //w
            CreateTargetedSpell("Drain", "FiddleSticks", 575f, SpellInfo.SpellSlot.W),
            CreateTargetedChannel("DrainChannel", "FiddleSticks", "null", 650f, SpellInfo.SpellSlot.W),
            //e
            CreateTargetedMissile("FiddlesticksDarkWind", "FiddlesticksDarkWind", "FiddleSticks", 750f, 1100f, 1100f, 1100f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Silence),
            CreateTargetedMissile("FiddlesticksDarkWind", "FiddleSticksDarkWindMissile", "FiddleSticks", 750f, 1100f, 1100f, 1100f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Silence),
            //r
            CreateSelfActive("Crowstorm", "FiddleSticks", 600f, SpellInfo.SpellSlot.R),
            CreateBlinkDash("Crowstorm", "FiddleSticks", 800f, SpellInfo.SpellSlot.R),
            #endregion
            //do fiora w cc calculations
            #region Fiora
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Fizz
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //get galio R buff name
            #region Galio
            //q
            CreateLinearSkillshot("GalioResoluteSmite", "GalioResoluteSmite", "Galio", 900f, 1300f, 1300f, 1300f, 120f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreatePassiveSpell("GalioBulwark", "Galio", SpellInfo.SpellSlot.W),
            //e
            CreateLinearSpell("GalioRighteousGust", "Galio", 5f, 1180f, 120f, SpellInfo.SpellSlot.E),
            //r
            CreateSelfActive("GalioIdolOfDurange", "Galio", 550f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Taunt),
            #endregion
            #region Gangplank
            //q
            CreateTargetedMissile("GangplankQWrapper", "GangplankQProceed", "Gangplank", 625f, 2600f, 2600f, 2600f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("GangplankW", "Gangplank", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            CreatePassiveSpell("GangplankW", "Gangplank", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.CCRemoval),
            //e in Trap Database
            /*new SpellInfo()
            {
                SpellName = "GangplankE",
                ChampionName = "Gangplank",
                Range = 1000f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                Width = 0f,
                Radius = 325f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },*/
            //r
            CreateCircularSpell("GangplankR", "Gangplank", 7f, int.MaxValue, 525f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Slow),
            #endregion
            //fix garen E by adding buff name and garen w buff name, add garen R
            #region Garen
            //q charge
            CreatePassiveSpell("GarenQ", "Garen", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None),
            CreateTargetedSpell("GarenQAttack", "Garen", 300f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Silence),
            //w
            CreatePassiveSpell("GarenW", "Garen", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None),
            //e
            CreateToggleableSelfActive("GarenE", "Garen", "null", 330f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedSpell("GarenR", "Garen", 100f, SpellInfo.SpellSlot.R),
            #endregion
            //gnar R stun logic
            #region Gnar
            //q
            CreateLinearSkillshot("GnarQMissile", "GnarQMissile", "Gnar", 1125f, 2500f, 2500f, 2500f, 55f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("GnarQMissile", "GnarQMissileReturn", "Gnar", 1125f, 2500f, 2500f, 2500f, 75f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //q mega
            CreateLinearSkillshot("GnarBigQMissile", "GnarBigQMissile", "Gnar", 1150f, 2100f, 2100f, 2100f, 90f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w passive. nothing to do here (maybe stacks)
            //w mega
            CreateLinearSpell("GnarBigW", "Gnar", 1.25f, 900f, 80f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e
            CreateCircularSkillshotDash("GnarE", "Gnar", 475f, 150f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //e form
            CreateCircularSkillshotDash("GnarBigE", "Gnar", 475f, 350f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateLinearSpell("GnarR", "Gnar", 590f, 590f, 200f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
            #endregion
            //gragas Q extra damage logic
            #region Gragas
            //q
            CreateCircularSkillshot("GragasQ", "GragasQMissile", "Gragas", 1100f, 800f, 800f, 800f, 250f, true, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateTargetedSpell("GragasWAttack", "Gragas", 400f, SpellInfo.SpellSlot.W),
            //e
            CreateLinearDash("GragasE", "Gragas", 600f, 200f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockBack),
            //r
            CreateCircularSkillshot("GragasR", "GragasRBoom", "Gragas", 1050f, 1800f, 1800f, 1800f, 350, 0.55f, true, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack),
            #endregion
            //add graves auto
            #region Graves
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Nearsight,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            //r cone split
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Hecarim
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Fear,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add turret empowered hits
            #region Heimerdinger
            //turret attacks
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Illaoi
            //q
            CreateLinearSpell("IllaoiQ", "Illaoi", 0.5f, 800f, 250f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("IllaoiW", "Illaoi", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //e
            CreateLinearSkillshot("IllaoiE", "IllaoiEMis", "Illaoi", 950f, 1900f, 1900f, 1900f, 50f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateSelfActive("IllaoiR", "Illaoi", 450f, SpellInfo.SpellSlot.R),
            #endregion
            //irelia stun/slow logic
            #region Irelia
            //q
            CreateTargetedDash("IreliaGatotsu", "Irelia", 650f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.Linear),
            //w
            CreatePassiveSpell("IreliaHittenStyle", "Irelia", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //e
            CreateTargetedSpell("IreliaEquilibriumStrike", "Irelia", 425f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            //r
            CreateLinearSkillshot("IreliaTranscendentBlades", "IreliaTranscendentBladesSpell", "Irelia",  1200f, 1600f, 1600f, 1600f, 120f, SpellInfo.SpellSlot.R),
            #endregion
            #region Ivern
            //q
            CreateLinearSkillshot("IvernQ", "IvernQ", "Ivern", 1100f, 1300f, 1300f, 1300f, 65f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Root),
            //w
            CreateWall("IvernW", "Ivern", 1600f, 300f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None),
            //e
            CreateTargetedActive("IvernE", "Ivern", 750f, 525f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //r
            CreatePassiveSpell("IvernR", "Ivern", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            CreatePassiveSpell("IvernRRecast", "Ivern", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.None),
            //daisy
            CreateLinearMissile("IvernRMissile", "Ivern", 800f, 1400f, 1600f, 1600f, 80f, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            #region Janna
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //j4 knockup logic, J4 w buff name
            #region JarvanIV
            //q
            CreateLinearSpell("JarvanIVDragonStrike", "JarvanIV", 0, 770f, 70f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp,
            //w
            CreateSelfActive("JarvanIVGoldenAegis", "JarvanIV", "null", 625f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow, SpellInfo.Buff.Shield),
            //e
            CreateCircularSpell("JarvanIVDemacianStandard", "JarvanIV", 830f, 75f, true, SpellInfo.SpellSlot.E, SpellInfo.Buff.AttackSpeedIncrease),
            //r
            CreateTargetedActive("JarvanIVCataclysm", "JarvanIV", 650f, 210f, SpellInfo.SpellSlot.R),
            #endregion
            #region Jax
            //q
            new SpellInfo()
            {
                SpellName = "JaxLeapStrike",
                ChampionName = "Jax",
                Range = 700f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "JaxEmpowerTwo",
                ChampionName = "Jax",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "JaxCounterStrike",
                ChampionName = "Jax",
                Range = 187.5f,
                Radius = 187.5f,
                Delay = 2.25f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "JaxRelentlessAssault",
                ChampionName = "Jax",
                Delay = 8.25f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add longrange q
            #region Jayce
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q ranged
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w ranged
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockAside,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e ranged
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Jhin
            //q
            new SpellInfo()
            {
                SpellName = "JhinQ",
                MissileName = "JhinQ",
                ChampionName = "Jhin",
                Range = 550f,
                MissileSpeed = 1800f,
                MissileMinSpeed = 1800f,
                MissileMaxSpeed = 1800f,
                OtherMissileNames = new string[] { "JhinQMisBounce", },
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "JhinW",
                ChampionName = "Jhin",
                Range = 2500f,
                MissileSpeed = 5000f,
                MissileMinSpeed = 5000f,
                MissileMaxSpeed = 5000f,
                Width = 40f,
                Delay = 1f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e handled as a trap
            new SpellInfo()
            {
                SpellName = "JhinE",
                MissileName = "JhinETrap",
                ChampionName = "Jhin",
                MissileSpeed = 1600f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
                Range = 750f,
                Width = 120f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                //original cast spell name is "JhinR"
                SpellName = "JhinRShot",
                MissileName = "JhinRShotMis",
                ChampionName = "Jhin",
                Range = 3500f,
                MissileSpeed = 1200f,
                MissileMinSpeed = 1200f,
                MissileMaxSpeed = 1200f,
                Width = 80f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Jinx
            //aa
            new SpellInfo()
            {
                MissileName = "JinxBasicAttack",
                ChampionName = "Jinx",
                MissileSpeed = 2750f,
                MissileMinSpeed = 2750f,
                MissileMaxSpeed = 2750f,
                Range = 625f,
                Radius = 100f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //rocket aa
            new SpellInfo()
            {
                MissileName = "JinxQAttack",
                ChampionName = "Jinx",
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Width = 20f,
                Radius = 100f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
                OtherMissileNames = new string[]
                {
                    "JinxQAttack2",
                },
            },
            //q
            new SpellInfo()
            {
                SpellName = "JinxQ",
                ChampionName = "Jinx",
                Range = 600f,
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Width = 0f,
                Radius = 100f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "JinxW",
                ChampionName = "Jinx",
                MissileName = "JinxWMissile",
                Range = 1600f,
                MissileSpeed = 1200f,
                MissileMinSpeed = 1200f,
                MissileMaxSpeed = 1200f,
                Width = 60f,
                Delay = 0.5f,
                OtherMissileNames = new string[]
                {
                    "JinxWMissile",
                },
                CollisionCount = 1,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "JinxE",
                MissileName = "JinxEHit",
                ChampionName = "Jinx",
                MissileSpeed = 1100f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Range = 1200f,
                Radius = 50f,
                //handled under the trap handler
                //SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                //CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "JinxR",
                ChampionName = "Jinx",
                MissileName = "JinxR",
                Range = 250000f,
                MissileSpeed = 1700f,
                MissileMinSpeed = 1700f,
                MissileMaxSpeed = 1700f,
                Width = 140f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Kalista
            //q
            new SpellInfo()
            {
                SpellName = "KalistaMysticShot",
                MissileName = "KalistaMysticShotMisTrue",
                ChampionName = "Kalista",
                Range = 1200f,
                MissileSpeed = 2400f,
                MissileMinSpeed = 2400f,
                MissileMaxSpeed = 2400f,
                Width = 40f,
                //OtherMissileNames = new string[] { "KalistaMysticShotMis" },
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "KalistaW",
                ChampionName = "Kalista",
                Range = int.MaxValue,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "KalistaExpunge",
                MissileName = "KalistaExpungeParticle",
                ChampionName = "Kalista",
                Range = 3000f,
                MissileSpeed = 3000f,
                MissileMinSpeed = 3000f,
                MissileMaxSpeed = 3000f,
                Radius = 1200f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "KalistaRx",
                MissileName = "KalistaRMis",
                ChampionName = "Kalista",
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Range = 1450f,
                Width = 80f,
                //R cast range = 1100
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Karma
            //q
            new SpellInfo()
            {
                SpellName = "KarmaQ",
                ChampionName = "Karma",
                MissileName = "KarmaQMissile",
                MissileSpeed = 1700f,
                MissileMinSpeed = 1700f,
                MissileMaxSpeed = 1700f,
                Range = 1050f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //rq
            new SpellInfo()
            {
                MissileName = "KarmaQMissileMantra",
                ChampionName = "Karma",
                MissileSpeed = 1700f,
                MissileMinSpeed = 1700f,
                MissileMaxSpeed = 1700f,
                Range = 950f,
                Width = 80f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "KarmaSpiritBind",
                ChampionName = "Karma",
                Range = 700f,
                MissileSpeed = 2200f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Width = 60f,
                Radius = 175f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Root,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "KarmaSolKimShield",
                ChampionName = "Karma",
                Range = 800f,
                MissileSpeed = 20f,
                MissileMinSpeed = 20f,
                MissileMaxSpeed = 20f,
                Width = 0f,
                Radius = 100f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "KarmaMantra",
                ChampionName = "Karma",
                Range = 1100f,
                MissileSpeed = 1300f,
                MissileMinSpeed = 1300f,
                MissileMaxSpeed = 1300f,
                Width = 0f,
                Radius = 275f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Karthus
            //q handled by particle databaseq
            /*new SpellInfo()
            {
                SpellName = "KarthusLayWasteA3",
                ChampionName = "Karthus",
                MissileSpeed = 10000f,
                Range = 875f,
                Radius = 160f,
                Delay = 0.25f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
                OtherMissileNames = new string[] { "KarthusLayWasteA1", "KarthusLayWasteA2",},
            },*/
            //w
            new SpellInfo()
            {
                SpellName = "KarthusWallOfPain",
                ChampionName = "Karthus",
                Range = 1000f,
                Width = 5f,
                Radius = 850f,
                ConeDegrees = 45f,
                Delay = 5.25f,
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "KarthusDefile",
                BuffName = "KarthusDefile",
                ChampionName = "Karthus",
                Range = 550f,
                MissileSpeed = 1000f,
                MissileMinSpeed = 1000f,
                MissileMaxSpeed = 1000f,
                Width = 150f,
                Radius = 550f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "KarthusFallenOne",
                ChampionName = "Karthus",
                Range = int.MaxValue,
                Delay = 3.25f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Kassadin
            //q
            new SpellInfo()
            {
                SpellName = "NullLance",
                ChampionName = "Kassadin",
                Range = 650f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "NetherBlade",
                ChampionName = "Kassadin",
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "ForcePulse",
                ChampionName = "Kassadin",
                Range = 700f,
                ConeDegrees = 80f,
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "RiftWalk",
                ChampionName = "Kassadin",
                Range = 500f,
                Radius = 150f,
                DashType = SpellInfo.Dashtype.Blink,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Katarina
            //q
            new SpellInfo()
            {
                SpellName = "KatarinaQ",
                MissileName = "KatarinaQ",
                ChampionName = "Katarina",
                Range = 675f,
                MissileSpeed = 1800f,
                MissileMinSpeed = 1100f,
                MissileMaxSpeed = 1100f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,

                // kat q bounce
                OtherMissileNames = new string[] { "KatarinaQMis", },
            },
            //w
            new SpellInfo()
            {
                SpellName = "KatarinaW",
                ChampionName = "Katarina",
                Range = 0f,
                Radius = 60f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "KatarinaE",
                ChampionName = "Katarina",
                Range = 725f,
                DashType = SpellInfo.Dashtype.Blink,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "KatarinaR",
                MissileName = "KatarinaRMis",
                ChampionName = "Katarina",
                Range = 550f,
                MissileSpeed = 2400f,
                MissileMinSpeed = 2400f,
                MissileMaxSpeed = 2400f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Kayle
            //q
            new SpellInfo()
            {
                SpellName = "JudicatorReckoning",
                MissileName = "JudicatorReckoning",
                ChampionName = "Kayle",
                Range = 650f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1300f,
                MissileMaxSpeed = 1300f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "JudicatorDivineBlessing",
                ChampionName = "Kayle",
                Range = 900f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "JudicatorRighteousFury",
                ChampionName = "Kayle",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "JudicatorIntervention",
                ChampionName = "Kayle",
                Range = 900f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Kennen
            //aa
            new SpellInfo()
            {
                MissileName = "KennenMegaProc",
                ChampionName = "Kennen",
                MissileSpeed = 1700f,
                MissileMinSpeed = 1700f,
                MissileMaxSpeed = 1700f,
                Range = 200f,
                Width = 0f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //q
            new SpellInfo()
            {
                SpellName = "KennenShurikenHurlMissile1",
                MissileName = "KennenShurikenHurlMissile1",
                ChampionName = "Kennen",
                Range = 1050f,
                MissileSpeed = 1700f,
                MissileMinSpeed = 1700f,
                MissileMaxSpeed = 1700f,
                Width = 50f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "KennenBringTheLight",
                ChampionName = "Kennen",
                Range = 725f,
                MissileSpeed = 20f,
                MissileMinSpeed = 20f,
                MissileMaxSpeed = 20f,
                Width = 0f,
                Radius = 900f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "KennenLightningRush",
                ChampionName = "Kennen",
                Range = 200f,
                MissileSpeed = 20f,
                MissileMinSpeed = 20f,
                MissileMaxSpeed = 20f,
                Radius = 100f,
                Delay = 2.25f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "KennenShurikenStorm",
                ChampionName = "Kennen",
                MissileSpeed = 779.9f,
                MissileMinSpeed = 779.9f,
                MissileMaxSpeed = 779.9f,
                Radius = 550f,
                Delay = 3.25f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region KhaZix
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Kindred
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Kled
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q skarrl
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w skarrl
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e skarrl
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region KogMaw
            //q
            new SpellInfo()
            {
                SpellName = "KogMawQ",
                MissileName = "KogMawQ",
                ChampionName = "KogMaw",
                Range = 1200f,
                MissileSpeed = 1650f,
                MissileMinSpeed = 1650f,
                MissileMaxSpeed = 1650f,
                Width = 70f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "KogMawBioArcaneBarrage",
                ChampionName = "KogMaw",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "KogMawVoidOozeMissile",
                MissileName = "KogMawVoidOozeMissile",
                ChampionName = "KogMaw",
                Range = 1500f,
                MissileSpeed = 1400f,
                MissileMinSpeed = 1400f,
                MissileMaxSpeed = 1400f,
                Width = 120f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //R
            new SpellInfo()
            {
                SpellName = "KogMawLivingArtillery",
                ChampionName = "KogMaw",
                Range = 1200f,
                Radius = 240f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R
            },
            #endregion
            #region LeBlanc
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //rq
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            //rw
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            //re
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //fix lee sin second q
            #region LeeSin
            //q
            new SpellInfo()
            {
                //SpellName = "BlindMonkQOne",
                MissileName = "BlindMonkQOne",
                BuffName = "BlindMonkQOne",
                ChampionName = "LeeSin",
                MissileSpeed = 1800f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Range = 1100f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q2
            new SpellInfo()
            {
                //SpellName = "BlindMonkQTwo",
                ChampionName = "LeeSin",
                //BuffName = "BlindMonkQOne",
                Range = 1300f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                DashType = SpellInfo.Dashtype.Linear,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                //SpellName = "BlindMonkWOne",
                ChampionName = "LeeSin",
                Range = 700f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Radius = 750f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                DashType = SpellInfo.Dashtype.Linear,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w2
            new SpellInfo()
            {
                SpellName = "BlindMonkWTwo",
                ChampionName = "LeeSin",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                //SpellName = "BlindMonkEOne",
                ChampionName = "LeeSin",
                Radius = 425f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e2
            new SpellInfo()
            {
                SpellName = "BlindMonkETwo",
                MissileName = "BlindMonkETwoMissile",
                ChampionName = "LeeSin",
                MissileSpeed = 1600f,
                MissileMinSpeed = 1400f,
                MissileMaxSpeed = 1400f,
                Range = 575f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "BlindMonkRKick",
                ChampionName = "LeeSin",
                Range = 375f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Width = 0f,
                Radius = 375f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Leona
            //q
            new SpellInfo()
            {
                SpellName = "LeonaShieldOfDaybreak",
                ChampionName = "Leona",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "LeonaSolarBarrier",
                ChampionName = "Leona",
                Radius = 500f,
                Delay = 3.25f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "LeonaZenithBlade",
                MissileName = "LeonaZenithBladeMissile",
                ChampionName = "Leona",
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Range = 900f,
                Width = 70f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Root,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "LeonaSolarFlare",
                ChampionName = "Leona",
                Range = 1200f,
                Radius = 120f,
                SecondRadius = 225f,
                Delay = 0.625f + 0.25f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Lissandra
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Lucian
            //q
            new SpellInfo()
            {
                SpellName = "LucianQ",
                ChampionName = "Lucian",
                Range = 500f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                Width = 65f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "LucianW",
                MissileName = "LucianWMissile",
                ChampionName = "Lucian",
                Range = 900f,
                MissileSpeed = 1600f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
                Width = 55f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "LucianE",
                ChampionName = "Lucian",
                Range = 425f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                Width = 50f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "LucianR",
                MissileName = "LucianRMissile",
                ChampionName = "Lucian",
                Range = 1200f,
                MissileSpeed = 2800f,
                MissileMinSpeed = 2800f,
                MissileMaxSpeed = 2800f,
                Width = 110f,//possible 60 width
                OtherMissileNames = new string[] { "LucianRMissileOffhand", },
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Lulu
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Polymorph,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Lux
            //q
            new SpellInfo()
            {
                SpellName = "LuxLightBinding",
                MissileName = "LuxLightBindingMis",
                ChampionName = "Lux",
                Range = 1200f,
                MissileSpeed = 1200f,
                MissileMinSpeed = 1200f,
                MissileMaxSpeed = 1200f,
                Width = 70f,
                OtherMissileNames = new string[] { "LuxLightBindingDummy" },
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "LuxPrismaticWave",
                MissileName = "LuxPrismaticWaveMissile",
                ChampionName = "Lux",
                Range = 1175f,
                MissileSpeed = 2200f,
                MissileMinSpeed = 2200f,
                MissileMaxSpeed = 2200f,
                Width = 110f,
                OtherMissileNames = new string[] { "LuxPrismaticWaveReturn" },
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            //this is the missile. The ball is handled by Particle Database
            new SpellInfo()
            {
                SpellName = "LuxLightStrikeKugel",
                MissileName = "LuxLightStrikeKugel",
                ChampionName = "Lux",
                Range = 1100f,
                MissileSpeed = 1300f,
                MissileMinSpeed = 1300f,
                MissileMaxSpeed = 1300f,
                CanVaryInLength = true,
                Radius = 330f,
                Width = 55f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "LuxMaliceCannon",
                ChampionName = "Lux",
                Range = 3500f,
                MissileSpeed = 3000f,
                MissileMinSpeed = 3000f,
                MissileMaxSpeed = 3000f,
                Width = 250f,
                Delay = 0.75f,
                Radius = 250f,
                OtherMissileNames = new string[] { "LuxMaliceCannonMis", "LuxRVfxMis" },
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Malphite
            //q
            new SpellInfo()
            {
                SpellName = "SeismicShard",
                MissileName = "SeismicShard",
                ChampionName = "Malphite",
                Range = 625f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "Obduracy",
                ChampionName = "Malphite",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "Landslide",
                ChampionName = "Malphite",
                Range = 400f,
                Radius = 400f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "UFSlash",
                ChampionName = "Malphite",
                Range = 1000f,
                MissileSpeed = 700f,
                MissileMinSpeed = 700f,
                MissileMaxSpeed = 700f,
                Width = 160f,
                Radius = 300f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Malzahar
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.Silence,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Suppression,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Maokai
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Root,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region MasterYi
            //q
            new SpellInfo()
            {
                SpellName = "AlphaStrike",
                MissileName = "AlphaStrikeMissile",
                ChampionName = "MasterYi",
                Range = 600f,
                MissileSpeed = 4000f,
                MissileMinSpeed = 4000f,
                MissileMaxSpeed = 4000f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
                DashType = SpellInfo.Dashtype.Blink,
            },
            //w
            new SpellInfo()
            {
                SpellName = "Meditate",
                ChampionName = "MasterYi",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "WujuStyle",
                ChampionName = "MasterYi",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "Highlander",
                ChampionName = "MasterYi",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region MissFortune
            //q
            new SpellInfo()
            {
                SpellName = "MissFortuneRicochetShot",
                MissileName = "MissFortuneRicochetShot",
                ChampionName = "MissFortune",
                Range = 550f,
                MissileSpeed = 1400f,
                MissileMinSpeed = 1400f,
                MissileMaxSpeed = 1400f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q richochet bounce - most damage
            new SpellInfo()
            {
                MissileName = "MissFortuneRShotExtra",
                ChampionName = "MissFortune",
                MissileSpeed = 1400f,
                MissileMinSpeed = 1400f,
                MissileMaxSpeed = 1400f,
                Range = 575f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "MissFortuneViciousStrikes",
                ChampionName = "MissFortune",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "MissFortuneScattershot",
                ChampionName = "MissFortune",
                Range = 1000f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                Radius = 350f,
                Delay = 2.25f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "MissFortuneBulletTime",
                //MissileName = "MissFortuneBullets",
                ChampionName = "MissFortune",
                BuffName = "missfortunebulletsound",
                Range = 1450f,
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                //Delay = 3.25f,
                Width = 40f,
                Radius = 210f,
                ConeDegrees = 17f,
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Mordekaiser
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Morgana
            //q
            new SpellInfo()
            {
                SpellName = "DarkBindingMissile",
                MissileName = "DarkBindingMissile",
                ChampionName = "Morgana",
                MissileSpeed = 1200f,
                MissileMinSpeed = 1200f,
                MissileMaxSpeed = 1200f,
                Range = 1300f,
                Width = 90f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Root,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                //handled as particle
                //SpellName = "TormentedSoil",
                ChampionName = "Morgana",
                Range = 900f,
                MissileSpeed = 20f,
                MissileMinSpeed = 20f,
                MissileMaxSpeed = 20f,
                Width = 0f,
                Radius = 280f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "BlackShield",
                ChampionName = "Morgana",
                Range = 800f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "SoulShackles",
                ChampionName = "Morgana",
                Range = 625f,
                Radius = 625f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Nami
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.MovingWall,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Nasus
            //q
            new SpellInfo()
            {
                SpellName = "NasusQ",
                ChampionName = "Nasus",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "NasusW",
                ChampionName = "Nasus",
                Range = 600f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "NasusE",
                ChampionName = "Nasus",
                Range = 650f,
                Radius = 380f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "NasusR",
                ChampionName = "Nasus",
                Delay = 15.25f,
                Radius = 175f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Nautilus
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Pull,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Nidalee
            //q
            new SpellInfo()
            {
                SpellName = "JavelinToss",
                MissileName = "JavelinToss",
                ChampionName = "Nidalee",
                Range = 1500f,
                MissileSpeed = 1300f,
                MissileMinSpeed = 1300f,
                MissileMaxSpeed = 1300f,
                Width = 40f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q tiger
            new SpellInfo()
            {
                SpellName = "Takedown",
                ChampionName = "Nidalee",
                Range = 500f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                Width = 0f,
                Radius = 20f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w handled as trap
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w tiger
            new SpellInfo()
            {
                SpellName = "Pounce",
                ChampionName = "Nidalee",
                Range = 200f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Radius = 75f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "PrimalSurge",
                ChampionName = "Nidalee",
                Range = 600f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 350f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e tiger
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "AspectOfTheCougar",
                ChampionName = "Nidalee",
                Range = 20f,
                MissileSpeed = 943.8f,
                MissileMinSpeed = 943.8f,
                MissileMaxSpeed = 943.8f,
                Width = 0f,
                Radius = 100f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Nocturne
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Fear,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Nearsight,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Nunu
            //q
            new SpellInfo()
            {
                SpellName = "Consume",
                ChampionName = "Nunu",
                Range = 125f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "BloodBoil",
                ChampionName = "Nunu",
                Range = 700f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "IceBlast",
                MissileName = "IceBlast",
                ChampionName = "Nunu",
                Range = 550f,
                MissileSpeed = 1000f,
                MissileMinSpeed = 1000f,
                MissileMaxSpeed = 1000f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "AbsoluteZero",
                ChampionName = "Nunu",
                Range = 650f,
                MissileSpeed = 828.5f,
                MissileMinSpeed = 828.5f,
                MissileMaxSpeed = 828.5f,
                Width = 0f,
                Radius = 650f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Olaf
            //q
            new SpellInfo()
            {
                SpellName = "OlafAxeThrowCast",
                MissileName = "OlafAxeThrow",
                ChampionName = "Olaf",
                MissileSpeed = 1550f,
                MissileMinSpeed = 1550f,
                MissileMaxSpeed = 1550f,
                Range = 1000f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "OlafFrenziedStrikes",
                ChampionName = "Olaf",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "OlafRecklessStrike",
                ChampionName = "Olaf",
                Range = 325f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "OlafRagnarok",
                ChampionName = "Olaf",
                Delay = 6.25f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Orianna
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Pantheon
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Poppy
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Quinn
            //aa
            new SpellInfo()
            {
                MissileName = "QuinnBasicAttack",
                ChampionName = "Quinn",
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Range = 625f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //aa enhanced
            new SpellInfo()
            {
                MissileName = "QuinnWEnhanced",
                ChampionName = "Quinn",
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Range = 525f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //q
            new SpellInfo()
            {
                SpellName = "QuinnQ",
                MissileName = "QuinnQ",
                ChampionName = "Quinn",
                MissileSpeed = 1550f,
                MissileMinSpeed = 1550f,
                MissileMaxSpeed = 1550f,
                Range = 1050f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Nearsight,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                //gives vision
                SpellName = "QuinnW",
                ChampionName = "Quinn",
                Range = 2500f,
                MissileSpeed = 20f,
                MissileMinSpeed = 20f,
                MissileMaxSpeed = 20f,
                Width = 0f,
                Radius = 1200f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
                OtherMissileNames = new string[] { "QuinnWTimingMissile", },
            },
            //e
            new SpellInfo()
            {
                SpellName = "QuinnE",
                ChampionName = "Quinn",
                Range = 600f,
                MissileSpeed = 20f,
                MissileMinSpeed = 20f,
                MissileMaxSpeed = 20f,
                Width = 0f,
                Radius = 250f,
                ConeDegrees = 45f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            //the shards when leaving ult form
            new SpellInfo()
            {
                SpellName = "QuinnR",
                ChampionName = "Quinn",
                Range = 650f,
                MissileSpeed = 2200f,
                MissileMinSpeed = 2200f,
                MissileMaxSpeed = 2200f,
                Width = 200f,
                Radius = 300f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            //quinn r dmg
            new SpellInfo()
            {
                SpellName = "QuinnRFinale",
                ChampionName = "Quinn",
                Range = 700f,
                Radius = 700f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Rammus
            //q
            new SpellInfo()
            {
                SpellName = "PowerBall",
                ChampionName = "Rammus",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "DefensiveBallCurl",
                ChampionName = "Rammus",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "PuncturingTaunt",
                ChampionName = "Rammus",
                Range = 325f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Taunt,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "Tremors2",
                ChampionName = "Rammus",
                Radius = 375f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add reksai unburrow
            #region RekSai
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q burrowed
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w burrowed
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e burrowed
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Renekton
            //q
            new SpellInfo()
            {
                SpellName = "RenektonCleave",
                ChampionName = "Renekton",
                Range = 325f,
                Radius = 325f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w just casting
            new SpellInfo()
            {
                SpellName = "RenektonPreExecute",
                ChampionName = "Renekton",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w during auto
            new SpellInfo()
            {
                SpellName = "RenektonExecute",
                ChampionName = "Renekton",
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w during auto with enhanced w 
            new SpellInfo()
            {
                SpellName = "RenektonSuperExecute",
                ChampionName = "Renekton",
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //slice
            new SpellInfo()
            {
                SpellName = "RenektonSliceAndDice",
                ChampionName = "Renekton",
                Range = 450,
                Width = 50f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //dice
            new SpellInfo()
            {
                SpellName = "RenektonDice",
                ChampionName = "Renekton",
                Range = 450,
                Width = 50f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "RenektonReignOfTheTyrant",
                ChampionName = "Renekton",
                Range = 175f,
                Radius = 175f,
                Delay = 15.25f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Rengar
            //q
            new SpellInfo()
            {
                SpellName = "RengarQ",
                ChampionName = "Rengar",
                Range = 325f,
                ConeAndLinearRange = 450f,
                MissileSpeed = 3000f,
                MissileMinSpeed = 3000f,
                MissileMaxSpeed = 3000f,
                Width = 55f,
                Radius = 325f,
                ConeDegrees = 90f,
                SpellType = SpellInfo.SpellTypeInfo.ConeAndLinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q emp
            new SpellInfo()
            {
                SpellName = "RengarQEmp",
                ChampionName = "Rengar",
                Range = 300f,
                ConeAndLinearRange = 450f,
                MissileSpeed = 3000f,
                MissileMinSpeed = 3000f,
                MissileMaxSpeed = 3000f,
                Width = 55f,
                Radius = 300f,
                ConeDegrees = 90f,
                SpellType = SpellInfo.SpellTypeInfo.ConeAndLinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "RengarW",
                ChampionName = "Rengar",
                Range = 450f,
                Radius = 450f,
                ConeDegrees = 45f,

                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w emp
            new SpellInfo()
            {
                SpellName = "RengarWEmp",
                ChampionName = "Rengar",
                Range = 450f,
                Radius = 450f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "RengarE",
                MissileName = "RengarEMis",
                ChampionName = "Rengar",
                Range = 1000f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Width = 70f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e emp
            new SpellInfo()
            {
                SpellName = "RengarEEmp",
                MissileName = "RengarEEmpMis",
                ChampionName = "Rengar",
                Range = 1000f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Width = 70f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "RengarR",
                ChampionName = "Rengar",
                Range = 2000f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Riven
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion  
            #region Rumble
            //q
            new SpellInfo()
            {
                ChampionName = "Rumble",
                SpellName = "RumbleFlameThrower",
                BuffName = "RumbleFlameThrower",
                Range = 600f,
                MissileSpeed = 5000f,
                MissileMinSpeed = 5000f,
                MissileMaxSpeed = 5000f,
                Width = 500f,
                Radius = 500f,
                ConeDegrees = 32f,
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "RumbleShield",
                ChampionName = "Rumble",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                MissileName = "RumbleGrenadeMissile",
                ChampionName = "Rumble",
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Range = 950f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r Handled by Particle Manager
            /*new SpellInfo()
            {
                SpellName = "RumbleCarpetBomb",
                //MissileName = "RumbleCarpetBombMissile",
                ChampionName = "Rumble",
                Range = 1700f,
                Width = 200f,
                Delay = 5.25f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },*/
            #endregion
            //add Ryze E bounce "RyzeEMissile" slot 47
            #region Ryze
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Sejuani
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add shaco jack in the box to trap database
            #region Shaco
            //q
            new SpellInfo()
            {
                SpellName = "Deceive",
                ChampionName = "Shaco",
                Range = 400f,
                DashType = SpellInfo.Dashtype.Blink,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Fear,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "TwoShivPoison",
                MissileName = "TwoShivPoison",
                ChampionName = "Shaco",
                Range = 625f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "HallucinateFull",
                ChampionName = "Shaco",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            //r movement
            new SpellInfo()
            {
                SpellName = "HallucinateGuide",
                ChampionName = "Shaco",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Shen
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Taunt,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Shyvana
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q drag
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w drag
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e drag
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Singed
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "MegaAdhesive",
                ChampionName = "Singed",
                Range = 1000f,
                MissileSpeed = 700f,
                MissileMinSpeed = 700f,
                MissileMaxSpeed = 700f,
                Width = 0f,
                Delay = 5.25f,
                Radius = 265f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "Fling",
                ChampionName = "Singed",
                Range = 125f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 100f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Sion
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Sivir
            //aa
            new SpellInfo()
            {
                MissileName = "SivirBasicAttack",
                ChampionName = "Sivir",
                MissileSpeed = 1750f,
                MissileMinSpeed = 1400f,
                MissileMaxSpeed = 1400f,
                Range = 600f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //aa
            new SpellInfo()
            {
                MissileName = "SivirCritAttack",
                ChampionName = "Sivir",
                MissileSpeed = 1750f,
                MissileMinSpeed = 1750f,
                MissileMaxSpeed = 1750f,
                Range = 600f,
                Width = 0f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //q
            new SpellInfo()
            {
                SpellName = "SivirQ",
                MissileName = "SivirQMissile",
                ChampionName = "Sivir",
                Range = 1250f,
                MissileSpeed = 1350f,
                MissileMinSpeed = 1350f,
                MissileMaxSpeed = 1350f,
                Width = 90f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q
            new SpellInfo()
            {
                SpellName = "SivirQ",
                MissileName = "SivirQMissileReturn",
                ChampionName = "Sivir",
                Range = 25000f,
                MissileSpeed = 1350f,
                MissileMinSpeed = 1350f,
                MissileMaxSpeed = 1350f,
                Width = 100f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "SivirW",
                MissileName = "SivirWAttackBounce",
                ChampionName = "Sivir",
                MissileSpeed = 700f,
                MissileMinSpeed = 700f,
                MissileMaxSpeed = 700f,
                Range = 25000f,
                Width = 10f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //w
            new SpellInfo()
            {
                MissileName = "SivirWAttack",
                ChampionName = "Sivir",
                MissileSpeed = 1750f,
                MissileMinSpeed = 1750f,
                MissileMaxSpeed = 1750f,
                Range = 25000f,
                Width = 10f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //e
            new SpellInfo()
            {
                SpellName = "SivirE",
                ChampionName = "Sivir",
                Delay = 1.75f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "SivirR",
                ChampionName = "Sivir",
                Radius = 1000f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Skarner
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Suppression,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Sona
            //q
            new SpellInfo()
            {
                SpellName = "SonaQ",
                MissileName = "SonaQMissile",
                ChampionName = "Sona",
                Range = 825f,
                MissileSpeed = 1300f,
                MissileMinSpeed = 1300f,
                MissileMaxSpeed = 1300f,
                OtherMissileNames = new string[] { "SonaQAttackUpgrade" },
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                ChampionName = "Sona",
                SpellName = "SonaW",
                Range = 1000f,
                Radius = 1000f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w chord
            new SpellInfo()
            {
                ChampionName = "Sona",
                MissileName = "SonaWMissile",
                MissileSpeed = 1300f,
                MissileMinSpeed = 1300f,
                MissileMaxSpeed = 1300f,
                Range = 1000f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "SonaE",
                ChampionName = "Sona",
                Range = 1000f,
                Radius = 1000f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e chord
            new SpellInfo()
            {
                MissileName = "SonaEAttackUpgrade",
                ChampionName = "Sona",
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "SonaR",
                MissileName = "SonaR",
                ChampionName = "Sona",
                Range = 1000f,
                MissileSpeed = 2400f,
                MissileMinSpeed = 2400f,
                MissileMaxSpeed = 2400f,
                Width = 140f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Soraka
            //q
            new SpellInfo()
            {
                SpellName = "SorakaQ",
                MissileName = "SorakaQMissile",
                ChampionName = "Soraka",
                Range = 800f,
                MissileSpeed = 1100f,
                MissileMinSpeed = 1100f,
                MissileMaxSpeed = 1100f,
                Radius = 230f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "SorakaW",
                ChampionName = "Soraka",
                Range = 550f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "SorakaE",
                ChampionName = "Soraka",
                Range = 875f,
                Radius = 250f,
                Delay = 1.75f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Silence,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "SorakaR",
                ChampionName = "Soraka",
                Range = int.MaxValue,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Swain
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Syndra
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region TahmKench
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Suppression,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Talon
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Taric
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "TaricW",
                ChampionName = "Taric",
                Range = 800f,
                MissileSpeed = 1700f,
                MissileMinSpeed = 1700f,
                MissileMaxSpeed = 1700f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "TaricR",
                ChampionName = "Taric",
                Range = 400f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                Width = 0f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Teemo
            //aa
            new SpellInfo()
            {
                MissileName = "ToxicShotAttack",
                ChampionName = "Teemo",
                Range = 600f,
                MissileSpeed = 1300f,
                MissileMinSpeed = 1300f,
                MissileMaxSpeed = 1300f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //q
            new SpellInfo()
            {
                SpellName = "BlindingDart",
                MissileName = "BlindingDart",
                ChampionName = "Teemo",
                Range = 680f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Blind,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                //SpellName = "TeemoRCast",
                //MissileName = "BantamTrap",
                ChampionName = "Teemo",
                Range = 400f,
                MissileSpeed = 1000f,
                MissileMinSpeed = 1000f,
                MissileMaxSpeed = 1000f,
                Width = 120f,
                Radius = 135f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            //r2
            new SpellInfo()
            {
                //SpellName = "TeemoRCast",
                //aaaaaaaMissileName = "BantamTrapBounceSpell",
                ChampionName = "Teemo",
                Range = 400f,
                MissileSpeed = 1000f,
                MissileMinSpeed = 1000f,
                MissileMaxSpeed = 1000f,
                Width = 120f,
                Radius = 135f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Thresh
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Pull,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Tristana
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Trundle
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Tryndamere
            //q
            new SpellInfo()
            {
                SpellName = "TryndamereQ",
                ChampionName = "Tryndamere",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "TryndamereW",
                ChampionName = "Tryndamere",
                Range = 850f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "TryndamereE",
                ChampionName = "Tryndamere",
                Range = 660f,
                MissileSpeed = 700f,
                MissileMinSpeed = 700f,
                MissileMaxSpeed = 700f,
                Width = 160f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "UndyingRage",
                ChampionName = "Tryndamere",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region TwistedFate
            //q
            new SpellInfo()
            {
                SpellName = "WildCards",
                MissileName = "SealFateMissile",
                ChampionName = "TwistedFate",
                Range = 1450f,
                MissileSpeed = 1000f,
                MissileMinSpeed = 1000f,
                MissileMaxSpeed = 1000f,
                Width = 40f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "PickACard",
                ChampionName = "TwistedFate",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //blue card
            new SpellInfo()
            {
                SpellName = "BlueCardPreAttack",
                MissileName = "BlueCardAttack",
                ChampionName = "TwistedFate",
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //red card
            new SpellInfo()
            {
                SpellName = "PickACard",
                ChampionName = "TwistedFate",
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //red card aoe
            new SpellInfo()
            {
                SpellName = "PickACard",
                ChampionName = "TwistedFate",
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //yellow card
            new SpellInfo()
            {
                SpellName = "GoldCardPreAttack",
                MissileName = "GoldCardAttack",
                ChampionName = "TwistedFate",
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                MissileName = "TwistedFateBasicAttack4",
                ChampionName = "TwistedFate",
                MissileSpeed = 1500f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r vision
            new SpellInfo()
            {
                SpellName = "Destiny",
                ChampionName = "TwistedFate",
                Range = 5500f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            //r teleport
            new SpellInfo()
            {
                SpellName = "Gate",
                ChampionName = "TwistedFate",
                Range = 5500f,
                DashType = SpellInfo.Dashtype.Blink,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Twitch
            //q
            new SpellInfo()
            {
                SpellName = "TwitchHideInShadows",
                ChampionName = "Twitch",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "TwitchVenomCask",
                MissileName = "TwitchVenomCaskMissile",
                ChampionName = "Twitch",
                Range = 1100f,//900?
                MissileSpeed = 1400f,
                Delay = 3.25f,
                Radius = 275f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "TwitchExpunge",
                MissileName = "TwitchEParticle",
                ChampionName = "Twitch",
                MissileSpeed = 3000f,
                MissileMinSpeed = 3000f,
                MissileMaxSpeed = 3000f,
                Range = 1200f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "TwitchFullAutomatic",
                ChampionName = "Twitch",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Udyr
            //q
            new SpellInfo()
            {
                SpellName = "UdyrTigerStance",
                ChampionName = "Udyr",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "UdyrTurtleStance",
                ChampionName = "Udyr",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "UdyrBearStance",
                ChampionName = "Udyr",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "UdyrPheonixStance",
                ChampionName = "Udyr",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Urgot
            //q
            new SpellInfo()
            {
                SpellName = "UrgotHeatseekingLineMissile",
                MissileName = "UrgotHeatseekingLineMissile",
                ChampionName = "Urgot",
                Range = 1000f,
                MissileSpeed = 1600f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //lock on Q
            new SpellInfo()
            {
                SpellName = "UrgotHeatseekingHomeMissile",
                MissileName = "UrgotHeatseekingHomeMissile",
                ChampionName = "Urgot",
                Range = 1200f,
                MissileSpeed = 1800f,
                MissileMinSpeed = 1800f,
                MissileMaxSpeed = 1800f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "UrgotTerrorCapacitorActive2",
                ChampionName = "Urgot",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "UrgotPlasmaGrenade",
                MissileName = "UrgotPlasmaGrenadeBoom",
                ChampionName = "Urgot",
                Range = 1100f,
                Width = 120f,
                Radius = 250f,
                Delay = 0.25f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "UrgotSwap2",
                ChampionName = "Urgot",
                Range = 550f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Suppression,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Varus
            //q
            new SpellInfo()
            {
                MissileName = "VarusQMissile",
                //SpellName = "VarusQ",
                ChampionName = "Varus",
                MissileSpeed = 1900f,
                MissileMinSpeed = 1900f,
                MissileMaxSpeed = 1900f,
                Range = 2000f,
                Width = 70f,
                CanVaryInLength = true,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "VarusEMissile",
                MissileName = "VarusEMissile",
                ChampionName = "Varus",
                Range = 1100f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Width = 120f,
                Radius = 210f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "VarusR",
                MissileName = "VarusRMissile",
                ChampionName = "Varus",
                Range = 1250f,
                MissileSpeed = 1950f,
                MissileMinSpeed = 1950f,
                MissileMaxSpeed = 1950f,
                Width = 120f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Vayne
            //q
            new SpellInfo()
            {
                SpellName = "VayneTumble",
                ChampionName = "Vayne",
                Range = 300f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                DashType = SpellInfo.Dashtype.Linear,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                //passive
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "VayneCondemnMissile",
                MissileName = "VayneCondemnMissile",
                ChampionName = "Vayne",
                Range = 550f,
                MissileSpeed = 2200f,
                MissileMinSpeed = 2200f,
                MissileMaxSpeed = 2200f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "VayneInquisition",
                ChampionName = "Vayne",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Veigar
            //q
            new SpellInfo()
            {
                SpellName = "VeigarBalefulStrike",
                MissileName = "VeigarBalefulStrikeMis",
                ChampionName = "Veigar",
                Range = 950f,
                MissileSpeed = 2200f,
                MissileMinSpeed = 2200f,
                MissileMaxSpeed = 2200f,
                Width = 70f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "VeigarDarkMatter",
                ChampionName = "Veigar",
                CanVaryInLength = true,
                Range = 900f,
                Delay = 1.5f,
                Radius = 225f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "VeigarEventHorizon",
                ChampionName = "Veigar",
                Range = 700f,
                Radius = 390f,
                Delay = 3.75f,
                SecondRadius = 350f,
                SpellType = SpellInfo.SpellTypeInfo.CircularWall,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "VeigarR",
                MissileName = "VeigarR",
                ChampionName = "Veigar",
                Range = 650f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region VelKoz
            //q
            new SpellInfo()
            {
                SpellName = "VelkozQ",
                ChampionName = "Velkoz",
                MissileName = "VelkozQMissile",
                MissileSpeed = 1300f,
                MissileMinSpeed = 1300f,
                MissileMaxSpeed = 1300f,
                Range = 1100f,
                Width = 50f,
                CanVaryInLength = true,
                CollisionCount = 1,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q split
            new SpellInfo()
            {
                SpellName = "VelkozQ",
                MissileName = "VelkozQMissileSplit",
                ChampionName = "Velkoz",
                MissileSpeed = 2100f,
                MissileMinSpeed = 2100f,
                MissileMaxSpeed = 2100f,
                Range = 1100f,
                Width = 45f,
                CanVaryInLength = true,
                CollisionCount = 1,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "VelkozW",
                MissileName = "VelkozWMissile",
                ChampionName = "Velkoz",
                MissileSpeed = 1700f,
                MissileMinSpeed = 1700f,
                MissileMaxSpeed = 1700f,
                Range = 1200f,
                Width = 225f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "VelkozE",
                MissileName = "VelkozEMissile",
                ChampionName = "Velkoz",
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Range = 1100f,
                Width = 120f,
                Radius = 200f,
                TravelTime = -1f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "VelkozR",
                ChampionName = "Velkoz",
                BuffName = "VelkozR",
                Range = 1550f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Width = 100f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Vi
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Viktor
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Vladimir
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Volibear
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Warwick
            //q
            new SpellInfo()
            {
                SpellName = "HungeringStrike",
                ChampionName = "Warwick",
                Range = 400f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "HuntersCall",
                ChampionName = "Warwick",
                Range = 1200f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "BloodScent",
                ChampionName = "Warwick",
                Range = 1600f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "InfiniteDuress",
                ChampionName = "Warwick",
                Range = 700f,
                DashType = SpellInfo.Dashtype.Blink,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Suppression,
                Slot = SpellInfo.SpellSlot.R,
            },
            //r channel
            new SpellInfo()
            {
                SpellName = "InfiniteDuressChannel",
                ChampionName = "Warwick",
                Range = 700f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Suppression,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //get ult buff name
            #region Wukong
            //q
            new SpellInfo()
            {
                SpellName = "MonkeyKingDoubleAttack",
                ChampionName = "MonkeyKing",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "MonkeyKingDecoy",
                ChampionName = "MonkeyKing",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "MonkeyKingNimbus",
                ChampionName = "MonkeyKing",
                Range = 650f,
                MissileSpeed = 2200f,
                MissileMinSpeed = 2200f,
                MissileMaxSpeed = 2200f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "MonkeyKingSpinToWin",
                ChampionName = "MonkeyKing",
                Range = 162.5f,
                MissileSpeed = 700f,
                MissileMinSpeed = 700f,
                MissileMaxSpeed = 700f,
                Width = 162.5f,
                Radius = 162.5f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Xerath
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region XinZhao
            //q
            new SpellInfo()
            {
                SpellName = "XenZhaoComboTarget",
                ChampionName = "XinZhao",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "XenZhaoBattleCry",
                ChampionName = "XinZhao",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "XenZhaoSweep",
                ChampionName = "XinZhao",
                Range = 600f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "XenZhaoParry",
                ChampionName = "XinZhao",
                Range = 187.5f,
                Radius = 187.5f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //On Q cast, if Yasuo casted E is dashing (Yasuo.DummySpell) Q has a circular radius of 3.75f
            // Need to completely fix yasuo dash
            #region Yasuo
            //q
            new SpellInfo()
            {
                //SpellName = "YasuoQ",
                ChampionName = "Yasuo",
                Range = 520f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 55f,
                //OtherMissileNames = new string[] { "YasuoQ2", "YasuoQW", "YasuoQ2W", },
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q3
            new SpellInfo()
            {
                //SpellName = "YasuoQ3",
                MissileName = "YasuoQ3Mis",
                ChampionName = "Yasuo",
                Range = 1000f,
                MissileSpeed = 1200f,
                MissileMinSpeed = 1200f,
                MissileMaxSpeed = 1200f,
                Width = 90f,
                Radius = 100f,
                ConeDegrees = 45f,
                //OtherMissileNames = new string[] { "YasuoQ3W", },
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "YasuoWMovingWall",
                MissileName = "YasuoWMovingWallMisL",
                ChampionName = "Yasuo",
                Range = 6000f,
                MissileSpeed = 850f,
                MissileMinSpeed = 850f,
                MissileMaxSpeed = 850f,
                Width = 350f,
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                // SpellName = "YasuoDashWrapper",
                ChampionName = "Yasuo",
                Range = 475f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //IS EING
            new SpellInfo()
            {
                //SpellName = "YasuoDummySpell",
                ChampionName = "Yasuo",
                Range = 400f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "YasuoRKnockUpComboW",
                MissileName = "TempYasuoRMissile",
                ChampionName = "Yasuo",
                Range = 1200f,
                OtherMissileNames = new string[] { "YasuoRDummySpell" },
                DashType = SpellInfo.Dashtype.Blink,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.Suspension,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Yorick
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Zac
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //zeds spells will originate from shadows
            #region Zed
            //q
            new SpellInfo()
            {
                SpellName = "ZedQ",
                MissileName = "ZedQMissile",
                ChampionName = "Zed",
                Range = 925f,
                MissileSpeed = 1700f,
                MissileMinSpeed = 1700f,
                MissileMaxSpeed = 1700f,
                Width = 50f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "ZedW",
                MissileName = "ZedWMissile",
                ChampionName = "Zed",
                Range = 400f,
                MissileSpeed = 1750f,
                MissileMinSpeed = 1750f,
                MissileMaxSpeed = 1750f,
                Width = 60f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w second cast to blink
            new SpellInfo()
            {
                SpellName = "ZedW2",
                ChampionName = "Zed",
                DashType = SpellInfo.Dashtype.Blink,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "ZedE",
                ChampionName = "Zed",
                Radius = 290f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "ZedR",
                ChampionName = "Zed",
                Range = 625f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            //r second cast to blink
            new SpellInfo()
            {
                SpellName = "ZedR2",
                ChampionName = "Zed",
                Range = 400f,
                DashType = SpellInfo.Dashtype.Blink,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Ziggs
            //q
            new SpellInfo()
            {
                SpellName = "ZiggsQ",
                MissileName = "ZiggsQSpell",
                ChampionName = "Ziggs",
                Range = 1625f,
                MissileSpeed = 1750f,
                MissileMinSpeed = 1750f,
                MissileMaxSpeed = 1750f,
                Width = 120f,
                Radius = 125f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
                CanVaryInLength = true,
                OtherMissileNames = new string[]
                {
                    "ZiggsQSpell2",
                    "ZiggsQSpell3",
                },
            },
            //w
            new SpellInfo()
            {
                SpellName = "ZiggsW",
                MissileName = "ZiggsW",
                ChampionName = "Ziggs",
                Range = 1000f,
                MissileSpeed = 1750f,
                MissileMinSpeed = 1750f,
                MissileMaxSpeed = 1750f,
                Width = 0f,
                Radius = 275f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                MissileName = "ZiggsE",
                ChampionName = "Ziggs",
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Range = 900f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
                OtherMissileNames = new string[] { "ZiggsE2", "ZiggsE3" }
            },
            //r
            new SpellInfo()
            {
                SpellName = "ZiggsR",
                ChampionName = "Ziggs",
                MissileName = "ZiggsRBoom",
                Range = 5000f,
                MissileSpeed = 1750f,
                MissileMinSpeed = 1750f,
                MissileMaxSpeed = 1750f,
                Width = 0f,
                Radius = 250f,
                SecondRadius = 500f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Zilean
            //q
            new SpellInfo()
            {
                SpellName = "ZileanQ",
                MissileName = "ZileanQMissile",
                ChampionName = "Zilean",
                Range = 900f,
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Radius = 150f,
                OtherMissileNames = new string[] { "ZileanQAttachMissileMinion" },
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "ZileanW",
                ChampionName = "Zilean",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e speed up
            new SpellInfo()
            {
                SpellName = "TimeWarp",
                ChampionName = "Zilean",
                Range = 550f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e slow down
            new SpellInfo()
            {
                SpellName = "Rewind",
                ChampionName = "Zilean",
                Range = 550f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "ChronoShift",
                ChampionName = "Zilean",
                Range = 900f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add zyra plants
            #region Zyra
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Root,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
        };
        #endregion
        
        public static SpellInfo CreateLinearSkillshot(string spellName, string missileName, string championName,
            float range, float missileSpeed, float missileMinSpeed, float missileMaxSpeed, float width, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                MissileName = missileName,
                ChampionName = championName,
                Range = range,
                MissileSpeed = missileSpeed,
                MissileMinSpeed = missileMinSpeed,
                MissileMaxSpeed = missileMaxSpeed,
                Width = width,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateLinearMissile(string missileName, string championName,
            float range, float missileSpeed, float missileMinSpeed, float missileMaxSpeed, float width,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                MissileName = missileName,
                ChampionName = championName,
                Range = range,
                MissileSpeed = missileSpeed,
                MissileMinSpeed = missileMinSpeed,
                MissileMaxSpeed = missileMaxSpeed,
                Width = width,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = ccType,
            };
        }

        public static SpellInfo CreateLinearSpell(string spellName, string championName, float duration,
            float range, float width, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                Delay = duration + 0.25f,
                Width = width,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }
        
        public static SpellInfo CreateLinearDash(string spellName, string championName, float range, float width, 
            SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                Width = width,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedMissile(string spellName, string missileName, string championName,
            float range, float missileSpeed, float missileMinSpeed, float missileMaxSpeed, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                MissileName = missileName,
                ChampionName = championName,
                Range = range,
                MissileSpeed = missileSpeed,
                MissileMinSpeed = missileMinSpeed,
                MissileMaxSpeed = missileMaxSpeed,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedSpell(string spellName, string championName,
            float range, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedChannel(string spellName, string championName, string buffName,
            float range, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                BuffName = buffName,
                Range = range,
                SpellType = SpellInfo.SpellTypeInfo.TargetedChannel,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedDash(string spellName, string championName, float range,
            SpellInfo.SpellSlot slot, SpellInfo.Dashtype dashType, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                DashType = dashType,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateCircularSkillshot(string spellName, string missileName, string championName,
            float range, float missileSpeed, float missileMinSpeed, float missileMaxSpeed, float radius, bool CanVaryInLength, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                MissileName = missileName,
                ChampionName = championName,
                Range = range,
                MissileSpeed = missileSpeed,
                MissileMinSpeed = missileMinSpeed,
                MissileMaxSpeed = missileMaxSpeed,
                Radius = radius,
                CanVaryInLength = CanVaryInLength,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateCircularSkillshot(string spellName, string missileName, string championName,
            float range, float missileSpeed, float missileMinSpeed, float missileMaxSpeed, float radius, 
            float travelTime, bool CanVaryInLength, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                MissileName = missileName,
                ChampionName = championName,
                Range = range,
                MissileSpeed = missileSpeed,
                MissileMinSpeed = missileMinSpeed,
                MissileMaxSpeed = missileMaxSpeed,
                TravelTime = travelTime,
                Radius = radius,
                CanVaryInLength = CanVaryInLength,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateCircularSpell(string spellName, string championName, float duration,
            float range, float radius, bool canVaryInLength, SpellInfo.SpellSlot slot, SpellInfo.Buff buffType,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                Radius = radius,
                Delay = duration + 0.25f,
                CanVaryInLength = canVaryInLength,
                BuffType = buffType,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateCircularSpell(string spellName, string championName,
            float range, float radius, bool canVaryInLength, SpellInfo.SpellSlot slot, SpellInfo.Buff buffType,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                Radius = radius,
                CanVaryInLength = canVaryInLength,
                BuffType = buffType,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateCircularSpell(string spellName, string championName, string buffName,
            float range, float radius, bool canVaryInLength, SpellInfo.SpellSlot slot, SpellInfo.Buff buffType,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                BuffName = buffName,
                Range = range,
                Radius = radius,
                CanVaryInLength = canVaryInLength,
                BuffType = buffType,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateCircularSkillshotDash(string spellName, string championName,
            float range, float impactRadius, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                CanVaryInLength = true,
                Radius = impactRadius,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateBlinkDash(string spellName, string championName,
            float range, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                CanVaryInLength = true,
                DashType = SpellInfo.Dashtype.Blink,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreatePassiveSpell(string spellName,string championName, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None, SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                BuffType = buffType,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreatePassiveSpell(string spellName, string championName, float extraDelay, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None, SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                Delay = extraDelay + 0.25f,
                BuffType = buffType,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateConeSpell(string spellName, string championName, float range, float coneAngleInDegrees,
            SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                ConeDegrees = coneAngleInDegrees,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateConeSpell(string spellName, string championName, string buffName, float range, float coneAngleInDegrees,
            SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                BuffName = buffName,
                Range = range,
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                ConeDegrees = coneAngleInDegrees,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateSelfActive(string spellName, string championName, 
            float radius, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None, 
            SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                Radius = radius,
                CCtype = ccType,
                BuffType = buffType,
                Slot = slot,
            };
        }
        
        public static SpellInfo CreateSelfActive(string spellName, string championName, string buffName,
            float radius, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None,
            SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                BuffName = buffName,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                Radius = radius,
                CCtype = ccType,
                BuffType = buffType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedActive(string spellName, string championName, float range,
            float radius, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None,
            SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                SpellType = SpellInfo.SpellTypeInfo.TargetedActive,
                Range = range,
                Radius = radius,
                CCtype = ccType,
                BuffType = buffType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateWall(string spellName, string championName, float range, float width,
            SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                Width = width,
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateAutoAttack(string missileName, string championName,
            float missileSpeed, float missileMinSpeed, float missileMaxSpeed, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                ChampionName = championName,
                MissileName = missileName,
                MissileSpeed = missileSpeed,
                MissileMinSpeed = missileMinSpeed,
                MissileMaxSpeed = missileMaxSpeed,
                CCtype = ccType,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                Slot = SpellInfo.SpellSlot.Auto,
            };
        }

        public static SpellInfo CreateSelfActiveNoDamage(string spellName, string championName,
            float radius, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None,
            SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                SpellType = SpellInfo.SpellTypeInfo.SelfActiveNoDamage,
                CCtype = ccType,
                BuffType = buffType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateToggleableSelfActive(string spellName, string championName, string buffName,
            float radius, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                BuffName = buffName,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo GetSpellInfo(string spellOrMissileName)
        {
            foreach (SpellInfo info in SpellList)
            {
                if (info.SpellName == spellOrMissileName || info.MissileName == spellOrMissileName)
                    return info;
                foreach (string otherName in info.OtherMissileNames)
                    if (otherName == spellOrMissileName)
                        return info;
            }
            return null;
        }

        public static SpellInfo CreateInstancedSpellInfo(SpellInfo info)
        {
            if (info == null)
                return null;

            SpellInfo newSpellInstance = new SpellInfo();

            var type = typeof(SpellInfo);
            foreach (var sourceProperty in type.GetProperties().ToList())
            {
                var targetProperty = type.GetProperty(sourceProperty.Name);
                targetProperty.SetValue(newSpellInstance, sourceProperty.GetValue(info, null), null);
            }
            foreach (var sourceField in type.GetFields().ToList())
            {
                try
                {
                    var targetField = type.GetField(sourceField.Name);
                    targetField.SetValue(newSpellInstance, sourceField.GetValue(info));
                }
                catch
                {
                    Console.WriteLine("failed with field " + sourceField.Name);
                }
            }
            return newSpellInstance;
        }

        public static void Initialize()
        {
            List<string> ChampionNames = new List<string>();
            foreach (AIHeroClient hero in EntityManager.Heroes.AllHeroes)
                if (!ChampionNames.Contains(hero.ChampionName))
                    ChampionNames.Add(hero.ChampionName);

            foreach (SpellInfo spell in SpellInfoList.Where(a=>ChampionNames.Contains(a.ChampionName)))
                SpellList.Add(spell);
        }
    }
}
