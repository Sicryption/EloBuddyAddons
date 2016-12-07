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
            CreateTargetedMissile("none", "ItemMagicShankMis", "All", 725f, 1400f, 1400f, 1400f, SpellInfo.SpellSlot.None),
            //recall
            CreatePassiveSpell("recall", "All", "recall", 1f, SpellInfo.SpellSlot.None),
            //Teleport
            CreatePassiveSpell("SummonerTeleport", "All", "SummonerTeleport", 2f, SpellInfo.SpellSlot.None),
            //Tiamat
            CreateSelfActive("ItemTiamatCleave", "All", 325f, SpellInfo.SpellSlot.None),
            //Health Potion
            CreatePassiveSpell("RegenerationPotion", "All", "RegenerationPotion", SpellInfo.SpellSlot.None),
            //trinket
            CreateCircularSpell("TrinketTotemLvl1", "All", 625f, 5f, true, SpellInfo.SpellSlot.None, SpellInfo.Buff.None),
            //blue trinket
            CreateCircularSpell("TrinketOrbLvl3", "All", 4000f, 5f, true, SpellInfo.SpellSlot.None, SpellInfo.Buff.None),
            //Regen Potion
            CreatePassiveSpell("ItemCrystalFlask", "All", "ItemCrystalFlask", SpellInfo.SpellSlot.None),
            //Corruption Potion
            CreatePassiveSpell("ItemDarkCrystalFlask", "All", "ItemDarkCrystalFlask", SpellInfo.SpellSlot.None),
            //Bisket
            CreatePassiveSpell("ItemMiniRegenPotion", "All", "ItemMiniRegenPotion", SpellInfo.SpellSlot.None),
            //Shurelyas
            CreateSelfActiveNoDamage("ShurelyasCrest", "All", 700f, SpellInfo.SpellSlot.None, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //Runaans
            CreateAutoAttack("ItemHurricaneAttack", "All", 2000f, 2000f, 2000f),
            //ZZRot
            CreateCircularWall("ItemVoidGate", "All", 400f, 0f, 150f, 0f, SpellInfo.SpellSlot.None, SpellInfo.CrowdControlType.None),
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithBuff,
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
                SpellType = SpellInfo.SpellTypeInfo.TargetedSpell,
                CCtype = SpellInfo.CrowdControlType.Slow,
            },
            //Seraphs Embrace Shield
            new SpellInfo()
            {
                SpellName = "ItemSeraphsEmbrace",
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithBuff,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithBuff,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Bilgewater Cutlass
            new SpellInfo()
            {
                SpellName = "BilgewaterCutlass",
                Range = 550f,
                SpellType = SpellInfo.SpellTypeInfo.TargetedSpell,
                CCtype = SpellInfo.CrowdControlType.Slow,
            },
            //Randuins Omen
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
            //Smite
            new SpellInfo()
            {
                SpellName = "SummonerSmite",
                Range = 500f,
                SpellType = SpellInfo.SpellTypeInfo.TargetedSpell,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Flash
            new SpellInfo()
            {
                SpellName = "SummonerFlash",
                Range = 425,
                SpellType = SpellInfo.SpellTypeInfo.LinearDash,
                DashType = SpellInfo.Dashtype.Blink,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Ignite
            new SpellInfo()
            {
                SpellName = "SummonerDot",
                Range = 600,
                SpellType = SpellInfo.SpellTypeInfo.TargetedSpellWithDuration,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Heal
            CreateSelfActiveNoDamage("SummonerHeal", "All", 600f, SpellInfo.SpellSlot.None),
            //Clarity
            CreateSelfActiveNoDamage("SummonerMana", "All", 600f, SpellInfo.SpellSlot.None),
            //Exhaust
            new SpellInfo()
            {
                SpellName = "SummonerExhaust",
                Range = 650f,
                SpellType = SpellInfo.SpellTypeInfo.TargetedSpell,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Heal speed up
            new SpellInfo()
            {
                SpellName = "SummonerBoost",
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithBuff,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Youmuus
            new SpellInfo()
            {
                SpellName = "YoumusBlade",
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithBuff,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Ghost
            new SpellInfo()
            {
                SpellName = "SummonerHaste",
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithBuff,
                CCtype = SpellInfo.CrowdControlType.None,
            },
            //Barrier
            new SpellInfo()
            {
                SpellName = "SummonerBarrier",
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithBuff,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpell,
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
            CreateCircularSkillshotDash("AatroxQ", "Aatrox", 650f, 285f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.Linear, SpellInfo.CrowdControlType.KnockUp),
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
            CreateLinearSkillshot("AhriOrbofDeception", "AhriOrbMissile", "Ahri", 1000f, 1100f, 1100f, 1100f, 100f, false, SpellInfo.SpellSlot.Q),
            CreateLinearSkillshot("AhriOrbofDeception", "AhriOrbReturn", "Ahri", 1000f, 1100f, 1100f, 1100f, 100f, false, SpellInfo.SpellSlot.Q),
            //w
            CreateSelfActive("AhriFoxFire", "Ahri", "AhriFoxFire", 700, SpellInfo.SpellSlot.W),
            CreateTargetedMissile("null", "AhriFoxFireMissile", "Ahri", 600f, 1800f, 1800f, 1800f, SpellInfo.SpellSlot.W),
            CreateTargetedMissile("null", "AhriFoxFireMissileTwo", "Ahri", 600f, 1800f, 1800f, 1800f, SpellInfo.SpellSlot.W),
            //e
            CreateLinearSkillshot("AhriSeduce", "AhriSeduceMissile", "Ahri", 1000f, 1550f, 1550f, 1550f, 60f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Charm),
            //r (bolts, not he dash itself)
            CreateLinearDash("AhriTumble", "Ahri", 400f, 0f, SpellInfo.SpellSlot.R),
            CreateLinearMissile("AhriTumbleMissile", "Ahri", 800f, 1400f, 1400f, 1400f, 0f),
            #endregion
            #region Akali
            //q
            CreateTargetedMissile("AkaliMota", "AkaliMota", "Akali", 600f, 1000f, 1000f, 1000f, SpellInfo.SpellSlot.Q),
            //w
            //CreateCircularSpell("AkaliSmokeBomb", "Akali", 8f, 250f, 475f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.SpeedUp, SpellInfo.CrowdControlType.Slow),
            CreateCircularSpell("AkaliSmokeBomb", "Akali", 8f, 0f, 400f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.SpeedUp, SpellInfo.CrowdControlType.Slow),
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
            CreateLinearSkillshot("BandageToss", "SadMummyBandageToss", "Amumu", 1100f, 2000f, 2000f, 2000f, 80f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
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
            CreateLinearSkillshot("FlashFrost", "FlashFrostSpell", "Anivia", 1100f, 850f, 850f, 850f, 110f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
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
            //should be handled with buff
            CreateCircularSpell("GlacialStorm", "Anivia", "null", 750f, 200f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Slow),
            #endregion
            //do annie Q/W/R stun with buff
            #region Annie
            //aa
            CreateAutoAttack("AnnieBasicAttack", "Annie", 1200f, 1200f, 1200f),
            CreateAutoAttack("AnnieBasicAttack2", "Annie", 1200f, 1200f, 1200f),
            //q
            CreateTargetedMissile("Disintegrate", "Disintegrate", "Annie", 625f, 1400f, 1400f, 1400f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateConeSpell("Incinerate", "Annie", 600f, 24.76f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e
            CreatePassiveSpell("MoltenShield", "Annie", "MoltenShield", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            //r
            CreateCircularSpell("InfernalGuardian", "Annie", 600f, 250f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.Pet, SpellInfo.CrowdControlType.Stun),
            #endregion
            #region Ashe
            //aa
            CreateAutoAttack("AsheBasicAttack", "Ashe", 2500f, 2500f, 2500f),
            CreateAutoAttack("AsheBasicAttack2", "Ashe", 2500f, 2500f, 2500f),
            CreateAutoAttack("AsheCritAttack", "Ashe", 2500f, 2500f, 2500f),
            CreateAutoAttack("AsheQAttack", "Ashe", 2500f, 2500f, 2500f, SpellInfo.CrowdControlType.Slow),
            CreateAutoAttack("AsheQAttackNoOnHit", "Ashe", 2500f, 2500f, 2500f, SpellInfo.CrowdControlType.Slow),
            CreateAutoAttack("ItemHurricaneAsheAttack", "Ashe", 2500f, 2500f, 2500f, SpellInfo.CrowdControlType.Slow),
            CreateAutoAttack("ItemHurricaneAsheAttackNoOnHit", "Ashe", 2500f, 2500f, 2500f, SpellInfo.CrowdControlType.Slow),
            //q
            CreatePassiveSpell("AsheQ", "Ashe", SpellInfo.SpellSlot.Q),
            //w
            CreateLinearSkillshot("Volley", "VolleyAttack", "Ashe", 1150f, 1500f, 1500f, 1500f, 20f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("Volley", "VolleyCenterAttack", "Ashe", 1150f, 1500f, 1500f, 1500f, 20f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("Volley", "VolleyRightAttack", "Ashe", 1150f, 1500f, 1500f, 1500f, 20f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("Volley", "VolleyAttackWithSound", "Ashe", 1150f, 1500f, 1500f, 1500f, 20f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            //e
            CreateLinearSkillshotNoDamage("AsheSpiritOfTheHawk", "AsheSpiritOfTheHawk", "Ashe", int.MaxValue, 1400f, 1400f, 1400f, 5f, true, SpellInfo.SpellSlot.E),            //r
            //r
            CreateLinearSkillshot("EnchantedCrystalArrow", "EnchantedCrystalArrow", "Ashe", int.MaxValue, 1600f, 1600f, 1600f, 130f, false, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
            #endregion
            //Do Sol
            #region Aurelion Sol
            //passive
            /*
                MissileName = "AurelionSolStarMissile",
                ChampionName = "AurelionSol",
                MissileSpeed = 500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 2500f,
                Width = 0f,*/
            //q
            /*
                MissileName = "AurelionSolBasicAttack",
                ChampionName = "AurelionSol",
                MissileSpeed = 4000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 650f,

                SpellName = "AurelionSolQ",
                ChampionName = "AurelionSol",
                Range = 2000f,
                MissileSpeed = 850f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 110f,
                Radius = 210f,
                ConeDegrees = 45f,

aurelionsolqhaste
                MissileName = "AurelionSolQMissile",
                ChampionName = "AurelionSol",
                MissileSpeed = 850f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 110f,

                SpellName = "AurelionSolQCancelButton",
                ChampionName = "AurelionSol",
                Range = 25000f,
                MissileSpeed = 850f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 110f,
                Radius = 210f,
                ConeDegrees = 45f,
                */
            //w
            /*
                SpellName = "AurelionSolW",
                ChampionName = "AurelionSol",
                Range = 600f,
                MissileSpeed = 300f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 60f,
                Radius = 0f,
                ConeDegrees = 45f,

aurelionsolwactive
turretshield
                SpellName = "AurelionSolWToggleOff",
                ChampionName = "AurelionSol",
                Range = 660f,
                MissileSpeed = 500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 0f,
                ConeDegrees = 45f,
            */
            //e
            /*
                SpellName = "AurelionSolE",
                ChampionName = "AurelionSol",
                Range = 3000f,
                MissileSpeed = 20f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 350f,
                ConeDegrees = 45f,
                SpellName = "AurelionSolECancelButton",
                ChampionName = "AurelionSol",
                Range = 25000f,
                MissileSpeed = 850f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 110f,
                Radius = 210f,
                ConeDegrees = 45f,
            */
            //r
            /*
                SpellName = "AurelionSolR",
                ChampionName = "AurelionSol",
                Range = 1500f,
                MissileSpeed = 20f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 350f,
                ConeDegrees = 45f,

                MissileName = "AurelionSolRBeamMissile",
                ChampionName = "AurelionSol",
                MissileSpeed = 4500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 120f,
            */
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpell,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSpellWithDuration,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //bard Q stun conditions, treat W as projectile, and E escape, and R land time
            #region Bard
            //q
            CreateLinearSkillshot("BardQ", "BardQ", "Bard", 950f, 1500f, 1500f, 1500f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            CreateLinearSkillshot("BardQ", "BardQMissile2", "Bard", 950f, 1500f, 1500f, 1500f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w on champion
            new SpellInfo()
            {
                SpellName = "BardWDirectHeal",
                ChampionName = "Bard",
                Range = 800f,
                SpellType = SpellInfo.SpellTypeInfo.TargetedMissile,
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
            CreateLinearSkillshot("RocketGrab", "RocketGrabMissile", "Blitzcrank", 1050f, 1800f, 1800f, 1800f, 70f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Pull),
            //w
            CreatePassiveSpell("Overdrive", "Blitzcrank", "Overdrive", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //e
            CreatePassiveSpell("PowerFist", "Blitzcrank", "PowerFist", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockUp),
            //r
            CreateSelfActive("StaticField", "Blitzcrank", 600f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Silence),
            #endregion
            //add brand passive. Brand Q stun calculations
            #region Brand
            //q
            CreateLinearSkillshot("BrandQ", "BrandQMissile", "Brand", 1100f, 1600f, 2000f, 2000f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateCircularSpell("BrandW", "Brand", 0.625f, 900f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.None),
            //e
            CreateTargetedSpell("BrandE", "Brand", 675f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedMissile("BrandR", "BrandRMissile", "Brand", 750f, 750f, 250f, 3000f, SpellInfo.SpellSlot.R),
            CreateTargetedMissile("BrandR", "BrandR", "Brand", 750f, 750f, 250f, 3000f, SpellInfo.SpellSlot.R),
            #endregion
            //add braum aa passive stun
            //q stun on 4 stacks
            #region Braum
            //q
            CreateLinearSkillshot("BraumQ", "BraumQMissile", "Braum", 1050f, 1700f, 1700f, 1700f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("BraumQMissile", "BraumQMissile", "Braum", 1050f, 1700f, 1700f, 1700f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateLinearDash("BraumW", "Braum", 650f, 0f, SpellInfo.SpellSlot.W),
            //e
            CreatePassiveSpell("BraumE", "Braum", "braumeshieldbuff", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            //r
            CreateLinearSkillshot("BraumRWrapper", "BraumRMissile", "Braum", 1200f, 1400f, 1400f, 1400f, 115f, false, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            //fix caitlyn's Q's end, not all cait traps being deleted when dead (might have fixed traps), R has scaling range 2000/2500/3000
            #region Caitlyn
            //aa
            CreateAutoAttack("CaitlynBasicAttack", "Caitlyn", 2500f, 2500f, 2500f),
            CreateAutoAttack("CaitlynHeadshotMissile", "Caitlyn", 3000f, 3000f, 3000f),
            CreateAutoAttack("CaitlynCritAttack", "Caitlyn", 2500f, 2500f, 2500f),
            //q
            CreateLinearSkillshot("CaitlynPiltoverPeacemaker", "CaitlynPiltoverPeacemaker", "Caitlyn", 1250f, 2000f, 2000f, 2000f, 60f, false, SpellInfo.SpellSlot.Q),
            CreateLinearSkillshot("CaitlynPiltoverPeacemaker", "CaitlynPiltoverPeacemaker2", "Caitlyn", 1250f, 2000f, 2000f, 2000f, 90f, false, SpellInfo.SpellSlot.Q),
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
            CreateLinearSkillshot("CaitlynEntrapment", "CaitlynEntrapmentMissile", "Caitlyn", 800f, 1600f, 2000f, 2000f, 70f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateTargetedMissile("CaitlynAceintheHole", "CaitlynAceintheHoleMissile", "Caitlyn", 2000f, 3200f, 2200f, 2200f, SpellInfo.SpellSlot.R),
            #endregion
            //code camille
            #region Camille

            #endregion
            //cassiopeia stun logic 
            //cassiopeia w? data below
            #region Cassiopeia
            //q
            CreateCircularSpell("CassiopeiaQ", "Cassiopeia", 850f, 160f, true, SpellInfo.SpellSlot.Q, SpellInfo.Buff.None),
            //w
            /*
            SpellName = "CassiopeiaW",
                ChampionName = "Cassiopeia",
                Range = 25000f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 180f,
                ConeDegrees = 45f,

                MissileName = "CassiopeiaWMissile",
                ChampionName = "Cassiopeia",
                MissileSpeed = 3000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 25f,
                */
            //e
            CreateTargetedMissile("CassiopeiaE", "CassiopeiaE", "Cassiopeia", 700f, 2500f, 2500f, 2500f, SpellInfo.SpellSlot.E),
            //r
            CreateConeSpell("CassiopeiaR", "Cassiopeia", 825f, 40f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
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
            CreateLinearSkillshot("MissileBarrageMissile", "MissileBarrageMissile", "Corki", 1300f, 2000f, 2000f, 2000f, 40f, false, SpellInfo.SpellSlot.R),
            CreateLinearSkillshot("MissileBarrageMissile2", "MissileBarrageMissile2", "Corki", 1500f, 2000f, 2000f, 2000f, 40f, false, SpellInfo.SpellSlot.R),
            #endregion
            //add darius Q inner range dodging
            #region Darius
            //q
            CreateSelfActive("DariusCleave", "Darius", "dariusqcast", 285f, SpellInfo.SpellSlot.Q),
            CreateSelfActive("DariusCleave", "Darius", "dariusqcast", 400f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("DariusNoxianTacticsONH", "Darius", "DariusNoxianTacticsONH", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            //e
            CreateConeSpell("DariusAxeGrabCone", "Darius", 550f, 25f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Pull),
            //r
            CreateTargetedSpell("DariusExecute", "Darius", 475f, SpellInfo.SpellSlot.R),
            #endregion
            #region Diana
            //q
            CreateArcSkillshot("DianaArc", "null", "Diana", 1000f, 5000f, 5000f, 5000f, 0f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrow", "Diana", 1000f, 5000f, 5000f, 5000f, 5f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowOuter", "Diana", 1000f, 5000f, 5000f, 5000f, 50f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowInner", "Diana", 1000f, 5000f, 5000f, 5000f, 0f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowOuter5", "Diana", 1000f, 5000f, 5000f, 5000f, 40f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowInner5", "Diana", 1000f, 5000f, 5000f, 5000f, 0f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowOuter4", "Diana", 1000f, 5000f, 5000f, 5000f, 30f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowInner4", "Diana", 1000f, 5000f, 5000f, 5000f, 0f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowOuter3", "Diana", 1000f, 5000f, 5000f, 5000f, 20f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowInner3", "Diana", 1000f, 5000f, 5000f, 5000f, 0f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowOuter2", "Diana", 1000f, 5000f, 5000f, 5000f, 0f, SpellInfo.SpellSlot.Q),
            CreateArcSkillshot("", "DianaArcThrowInner2", "Diana", 1000f, 5000f, 5000f, 5000f, 0f, SpellInfo.SpellSlot.Q),
            //w
            //could be 400 range
            CreateSelfActive("DianaOrbs", "Diana", "DianaOrbs", 750f, SpellInfo.SpellSlot.W),
            CreateTargetedMissile("null", "DianaOrbsMissile", "Diana", 750f, 900f, 900f, 900f, SpellInfo.SpellSlot.W),
            //e
            CreateSelfActive("DianaVortex", "Diana", 450f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Pull),
            //r
            CreateTargetedDash("DianaTeleport", "Diana", 825f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.TargetedLinear),
            #endregion
            #region DrMundo
            //q
            CreateLinearSpell("InfectedCleaverMissile", "DrMundo", 0f, 1050f, 60f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("InfectedCleaverMissileCast", "InfectedCleaverMissile", "DrMundo", 1050f, 2000f, 2000f, 2000f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateToggleableSelfActive("BurningAgony", "DrMundo", "BurningAgony", 325f, SpellInfo.SpellSlot.W),
            //e
            CreatePassiveSpell("Masochism", "DrMundo", "Masochism", SpellInfo.SpellSlot.E),
            //r
            CreatePassiveSpell("Sadism", "DrMundo", "Sadism", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            #endregion
            #region Draven
            //q
            CreatePassiveSpell("DravenSpinning", "Draven", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //w
            CreatePassiveSpell("DravenFury", "Draven", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //e
            CreateLinearSkillshot("DravenDoubleShot", "DravenDoubleShotMissile", "Draven", 1400f, 1400f, 1400f, 1400f, 130f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockBack),
            //r
            CreateLinearSkillshot("DravenRCast", "DravenR", "Draven", int.MaxValue, 2000f, 2000f, 2000f, 160f, false, SpellInfo.SpellSlot.R),
            #endregion  
            //Ekko R location and W delay
            #region Ekko
            //q
            CreateLinearSkillshot("EkkoQ", "EkkoQMis", "Ekko", 1075f, 1650f, 1650f, 1650f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("EkkoQ", "EkkoQReturn", "Ekko", 1075f, 1650f, 1650f, 1650f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("EkkoQ", "EkkoQReturnDead", "Ekko", 1075f, 1650f, 1650f, 1650f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateCircularSkillshot("EkkoW", "EkkoWMis", "Ekko", 1600f, 1500f, 1500f, 1500f, 400f, true, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e
            CreateLinearDash("EkkoE", "Ekko", 325f, 25, SpellInfo.SpellSlot.E),
            CreateTargetedDash("EkkoEAttack", "Ekko", 425f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Targeted),
            //r
            CreateCircularSpell("EkkoR", "Ekko", int.MaxValue, 375f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.Heal),
            #endregion
            //elise human Q particle
            //elise spider W, E and R buff names
            #region Elise
            //aa
            CreateAutoAttack("EliseBasicAttack", "Elise", 1600f, 1600f, 1600f),
            CreateAutoAttack("EliseBasicAttack2", "Elise", 1600f, 1600f, 1600f),
            CreateAutoAttack("EliseBasicAttack3", "Elise", 1600f, 1600f, 1600f),
            //q
            //CreateTargetedMissile("EliseHumanQ", "null", "Elise", 625f, 2200f, 2200f, 2200f, SpellInfo.SpellSlot.Q),
            //q spider
            CreateTargetedDash("EliseQSpiderCast", "Elise", 475f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.TargetedLinear),
            //w
            //spider speed is 5000f. Explosion Radius is 235f
            CreatePassiveSpell("EliseHumanW", "Elise", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            //w spider
            CreatePassiveSpell("EliseSpiderW", "Elise", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            //e
            CreatePassiveSpell("EliseSpiderEInitial", "Elise", "null", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Invulnerability),
            CreatePassiveSpell("EliseSpiderE", "Elise", "null", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Invulnerability),
            CreateTargetedDash("EliseSpiderEDescent", "Elise", 700f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Blink),
            //e spider
            CreateLinearSkillshot("EliseHumanE", "EliseHumanE", "Elise", 1100f, 1600f, 1600f, 1600f, 55f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            //r
            CreatePassiveSpell("EliseR", "Elise", "null", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            CreatePassiveSpell("EliseRSpider", "Elise", "null", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.None),
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
            CreateLinearSkillshot("EzrealMysticShot", "EzrealMysticShotMissile", "Ezreal", 1200f, 2000f, 2000f, 2000f, 80f, false, SpellInfo.SpellSlot.Q),
            //w
            CreateLinearSkillshot("EzrealEssenceFluxMissile", "EzrealEssenceFluxMissile", "Ezreal", 1050f, 1600f, 1600f, 1600f, 80f, false, SpellInfo.SpellSlot.W),
            //e
            CreateBlinkDash("EzrealArcaneShift", "Ezreal", 475f, SpellInfo.SpellSlot.E),
            //r
            CreateLinearSkillshot("EzrealTrueshotBarrage", "EzrealTrueshotBarrage", "Ezreal", int.MaxValue, 2000f, 2000f, 2000f, 160f, false, SpellInfo.SpellSlot.W),
            #endregion
            #region Fiddlesticks
            //q
            CreateTargetedSpell("Terrify", "FiddleSticks", 525f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Fear),
            //w
            CreateTargetedSpell("Drain", "FiddleSticks", 575f, SpellInfo.SpellSlot.W),
            CreateTargetedChannel("DrainChannel", "FiddleSticks", "DrainChannel", 650f, SpellInfo.SpellSlot.W),
            //e
            CreateTargetedMissile("FiddlesticksDarkWind", "FiddlesticksDarkWind", "FiddleSticks", 750f, 1100f, 1100f, 1100f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Silence),
            CreateTargetedMissile("FiddlesticksDarkWind", "FiddleSticksDarkWindMissile", "FiddleSticks", 750f, 1100f, 1100f, 1100f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Silence),
            //r
            CreateSelfActive("Crowstorm", "FiddleSticks", "Crowstorm", 600f, SpellInfo.SpellSlot.R),
            CreateBlinkDash("Crowstorm", "FiddleSticks", 800f, SpellInfo.SpellSlot.R),
            #endregion
            //do fiora w cc calculations
            //fiora r heal
            #region Fiora
            //q
            CreateLinearDash("FioraQ", "Fiora", 400f, 0f, SpellInfo.SpellSlot.Q),
            CreateCircularSpell("FioraQ", "Fiora", 400f, 360f, true, SpellInfo.SpellSlot.Q, SpellInfo.Buff.AttackDamageIncrease),
            //w
            CreateLinearSpell("FioraW", "Fiora", "FioraW", 800f, 70f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            CreateLinearMissile("FioraWMissile", "Fiora", 800f, 3200f, 3200f, 3200f, 70f, SpellInfo.CrowdControlType.Stun),
            //e
            CreatePassiveSpell("FioraE", "Fiora", "FioraE", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow, SpellInfo.Buff.AttackDamageIncrease),
            //r
            //buff name: fiorarmark
            CreateTargetedSpell("FioraR", "Fiora", 500f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            #endregion
            //not sure on fizz w buff name/
            //fix fizz e
            //fix fizz r attachment
            #region Fizz
            //q
            CreateTargetedDash("FizzQ", "Fizz", 550f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.TargetedLinear),
            //w
            CreatePassiveSpell("FizzW", "Fizz", "fizzwdot", SpellInfo.SpellSlot.W),
            //e
            /*new SpellInfo()
            {
                SpellName = "FizzE",
                ChampionName = "Fizz",
                Range = 25000f,
                MissileSpeed = 20f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 330f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },*/
            CreateLinearSkillshot("FizzR", "FizzRMissile", "Fizz", 1000f, 1300f, 1300f, 1300f, 80f, true, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Slow),
            //r
            /*new SpellInfo()
            {
            SpellName = "FizzR",
                ChampionName = "Fizz",
                Range = 10000f,
                MissileSpeed = 1300f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 150f,
                Radius = 300f,
                ConeDegrees = 45f,

                MissileName = "FizzRMissile",
                ChampionName = "Fizz",
                MissileSpeed = 1300f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 10000f,
                Width = 80f,
                SpellType = SpellInfo.SpellTypeInfo.TargetedMissile,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },*/
            #endregion
            #region Galio
            //q
            CreateLinearSkillshot("GalioResoluteSmite", "GalioResoluteSmite", "Galio", 900f, 1300f, 1300f, 1300f, 120f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreatePassiveSpell("GalioBulwark", "Galio", SpellInfo.SpellSlot.W),
            //e
            //this is only drawing the missile, not the actual duration of it.
            CreateLinearSpell("GalioRighteousGust", "Galio", 1f, 1180f, 120f, SpellInfo.SpellSlot.E),
            //r
            CreateSelfActive("GalioIdolOfDurand", "Galio", "GalioIdolOfDurand", 550f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Taunt),
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
            #region Garen
            //q charge
            CreatePassiveSpell("GarenQ", "Garen", "GarenQ", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            CreateTargetedSpell("GarenQAttack", "Garen", 300f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Silence),
            //w
            CreatePassiveSpell("GarenW", "Garen", "GarenW", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            //e
            CreateToggleableSelfActive("GarenE", "Garen", "GarenE", 330f, SpellInfo.SpellSlot.E),
            //r
            //I have not confirmed this range.
            CreateTargetedSpell("GarenR", "Garen", 300f, SpellInfo.SpellSlot.R),
            #endregion
            //gnar R stun logic
            #region Gnar
            //q
            CreateLinearSkillshot("GnarQMissile", "GnarQMissile", "Gnar", 1125f, 2500f, 2500f, 2500f, 55f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("GnarQMissile", "GnarQMissileReturn", "Gnar", 1125f, 2500f, 2500f, 2500f, 75f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //q mega
            CreateLinearSkillshot("GnarBigQMissile", "GnarBigQMissile", "Gnar", 1150f, 2100f, 2100f, 2100f, 90f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w passive. nothing to do here (maybe stacks)
            //w mega
            CreateLinearSpell("GnarBigW", "Gnar", 1.25f, 900f, 80f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e
            CreateCircularSkillshotDash("GnarE", "Gnar", 475f, 150f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Linear, SpellInfo.CrowdControlType.Slow),
            //e form
            CreateCircularSkillshotDash("GnarBigE", "Gnar", 475f, 350f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Linear, SpellInfo.CrowdControlType.Slow),
            //r
            CreateLinearSpell("GnarR", "Gnar", 590f, 590f, 200f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack),
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
            //add graves R cone
            #region Graves
            //aa
            //Graves Basic Attack isn't needed since the Basic Attack Spread has it included.
            CreateLinearMissile("GravesBasicAttack", "Graves", 425f, 3000f, 3000f, 3000f, 25f),
            CreateLinearMissile("GravesCritAttack", "Graves", 425f, 3400f, 3400f, 3400f, 25f),
            CreateLinearMissile("GravesBasicAttackSpread", "Graves", 425f, 3800f, 3800f, 3800f, 25f),
            /*
            SpellName = "GravesAutoAttackRecoil",
                ChampionName = "Graves",
                Range = 650f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 250f,
                ConeDegrees = 45f,
            */
            //q
            CreateLinearSkillshot("GravesQLineSpell", "GravesQLineMis", "Graves", 808f, 3000f, 3000f, 3000f, 100f, false, SpellInfo.SpellSlot.Q),
            CreateLinearSkillshot("GravesQLineSpell", "GravesQReturn", "Graves", 808f, 3000f, 3000f, 3000f, 100f, false, SpellInfo.SpellSlot.Q),
            //w
            CreateCircularSkillshot("GravesSmokeGrenade", "GravesSmokeGrenadeBoom", "Graves", 1100f, 1500f, 1500f, 1500f, 225f, true, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Nearsight),
            //e
            CreateLinearDash("GravesMove", "Graves", 300f, 0f, SpellInfo.SpellSlot.E),
            //r
            //mis speed could be 2100f
            CreateLinearSkillshot("GravesChargeShot", "GravesChargeShotShot", "Graves", 1000f, 1400f, 1400f, 1400f, 100f, false, SpellInfo.SpellSlot.R),
            //r cone split
            /*
            MissileName = "GravesChargeShotFxMissile",
                ChampionName = "Graves",
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 650f,
                Width = 20f,

                MissileName = "GravesChargeShotFxMissile2",
                ChampionName = "Graves",
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 700f,
                Width = 20f,

                MissileName = "GravesClusterShotSoundMissile",
                ChampionName = "Graves",
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 700f,
                Width = 20f,
            */
            #endregion
            #region Hecarim
            //q
            CreateSelfActive("HecarimRapidSlash", "Hecarim", 375f, SpellInfo.SpellSlot.Q),
            //w
            CreateSelfActive("HecarimW", "Hecarim", "HecarimW", 575f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.LifestealSpellVamp),
            //e
            CreatePassiveSpell("HecarimRamp", "Hecarim", "hecarimrampspeed", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //r
            CreateCircularSkillshotDash("HecarimUlt", "Hecarim", 1650f, 300f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Linear, SpellInfo.CrowdControlType.Fear),
            CreateLinearSkillshot("HecarimUlt", "HecarimUltMissile", "Hecarim", 1650f, 1100f, 1100f, 1100f, 200f, true, SpellInfo.SpellSlot.R),
            #endregion
            //add turret hits
            //fix heim empowered E
            #region Heimerdinger
            //Turret Names:
            //Lvl 1 - "H-28G Evolution Turret"
            //Ult - "H-28Q Apex Turret"
            //aa
            CreateAutoAttack("HeimerdingerBasicAttack", "Heimerdinger", 1500f, 1500f, 1500f),
            CreateAutoAttack("HeimerdingerBasicAttack2", "Heimerdinger", 1500f, 1500f, 1500f),
            //q
            CreateCircularSpell("HeimerdingerQ", "Heimerdinger", 350f, 0f, true, SpellInfo.SpellSlot.Q, SpellInfo.Buff.None),
            //w
            CreateLinearSkillshot("HeimerdingerW", "HeimerdingerWAttack2", "Heimerdinger", 1350f, 750f, 750f, 750f, 40f, false, SpellInfo.SpellSlot.W),
            CreateLinearSkillshot("HeimerdingerW", "HeimerdingerWAttack2Ult", "Heimerdinger", 1350f, 750f, 750f, 750f, 40f, false, SpellInfo.SpellSlot.W),
            //e
            CreateCircularSkillshot("HeimerdingerE", "HeimerdingerESpell", "Heimerdinger", 1400f, 1200f, 1200f, 1200, 100f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            CreateCircularSkillshot("HeimerdingerEUlt", "HeimerdingerESpell_ult", "Heimerdinger", 1000f, 1200f, 1200f, 1200f, 150f, true, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            CreateCircularSkillshot("HeimerdingerEUlt", "HeimerdingerESpell_ult2", "Heimerdinger", 1000f, 1200f, 1200f, 1200f, 150f, true, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            CreateCircularSkillshot("HeimerdingerEUlt", "HeimerdingerESpell_ult3", "Heimerdinger", 1000f, 1200f, 1200f, 1200f, 150f, true, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            //r
            CreatePassiveSpell("HeimerdingerR", "Heimerdinger", "HeimerdingerR", SpellInfo.SpellSlot.R),
            #endregion
            #region Illaoi
            //q
            CreateLinearSpell("IllaoiQ", "Illaoi", 0.5f, 800f, 250f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("IllaoiW", "Illaoi", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //e
            CreateLinearSkillshot("IllaoiE", "IllaoiEMis", "Illaoi", 950f, 1900f, 1900f, 1900f, 50f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateSelfActive("IllaoiR", "Illaoi", 450f, SpellInfo.SpellSlot.R),
            #endregion
            //irelia stun/slow logic
            #region Irelia
            //q
            CreateTargetedDash("IreliaGatotsu", "Irelia", 650f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.Linear),
            //w
            CreatePassiveSpell("IreliaHitenStyle", "Irelia", "IreliaHitenStyle", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //e
            CreateTargetedSpell("IreliaEquilibriumStrike", "Irelia", 425f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            //r
            CreateLinearSkillshot("IreliaTranscendentBlades", "IreliaTranscendentBladesSpell", "Irelia",  1200f, 1600f, 1600f, 1600f, 120f, false, SpellInfo.SpellSlot.R),
            #endregion
            #region Ivern
            //q
            CreateLinearSkillshot("IvernQ", "IvernQ", "Ivern", 1100f, 1300f, 1300f, 1300f, 65f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Root),
            //w
            CreateWall("IvernW", "Ivern", 1600f, 300f, 500f, 15f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None),
            //e
            CreateTargetedActive("IvernE", "Ivern", "IvernE", 750f, 525f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //r
            CreatePassiveSpell("IvernR", "Ivern", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            CreatePassiveSpell("IvernRRecast", "Ivern", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.None),
            //daisy
            CreateLinearMissile("IvernRMissile", "Ivern", 800f, 1400f, 1600f, 1600f, 80f, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            //add janna scaling range
            //janna q is going off herself and not the projectile
            #region Janna
            //q
            CreateLinearSkillshot("HowlingGale", "HowlingGaleSpell", "Janna", 1000f, 900f, 900f, 900f, 120f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp),
            //w
            CreateTargetedMissile("SowTheWind", "SowTheWind", "Janna", 600f, 1600f, 1600f, 1600f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            //e
            CreatePassiveSpell("EyeOfTheStorm", "Janna", "EyeOfTheStorm", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //r
            CreateSelfActive("ReapTheWhirlwind", "Janna", 600f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack, SpellInfo.Buff.Heal),
            CreateSelfActiveNoDamage("ReapTheWhirlwind", "Janna", "ReapThWhilwind", 600f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack, SpellInfo.Buff.Heal),
            #endregion
            //j4 knockup logic, J4 w buff name
            #region JarvanIV
            //q
            CreateLinearSpell("JarvanIVDragonStrike", "JarvanIV", 0, 770f, 70f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp),
            //w
            CreateSelfActive("JarvanIVGoldenAegis", "JarvanIV", "null", 625f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow, SpellInfo.Buff.Shield),
            //e
            CreateCircularSpell("JarvanIVDemacianStandard", "JarvanIV", 830f, 75f, true, SpellInfo.SpellSlot.E, SpellInfo.Buff.AttackSpeedIncrease),
            //r
            CreateTargetedSpell("JarvanIVCataclysm", "JarvanIV", 650f, 210f, SpellInfo.SpellSlot.R),
            #endregion
            //not sure of jax R buff name
            #region Jax            
            //empowered aa
            CreatePassiveSpell("jaxrelentlessassaultas", "Jax", "jaxrelentlessassaultas", SpellInfo.SpellSlot.Auto, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //q
            CreateTargetedDash("JaxLeapStrike", "Jax", 700f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.TargetedLinear),
            //w
            CreatePassiveSpell("JaxEmpowerTwo", "Jax", "JaxEmpowerTwo", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //e
            CreateSelfActive("JaxCounterStrike", "Jax", "JaxCounterStrike", 187.5f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun, SpellInfo.Buff.AutoAttackImmune),
            //r
            CreatePassiveSpell("JaxRelentlessAssault", "Jax", "JaxRelentlessAssault", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            #endregion
            //add longrange q
            //fix jayce e wall
            #region Jayce
            //aa
            CreateAutoAttack("JayceHyperChargeRangedAttack", "Jayce", 1800f, 1800f, 1800f),
            CreateAutoAttack("JayceRangedAttack", "Jayce", 2000f, 2000f, 2000f),
            //Don't delete first Q missile if it will hit the gate. If it does hit the gate, the range is based on how long it traveled to begin with.
            //q
            CreateTargetedDash("JayceToTheSkies", "Jayce", 600f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.TargetedLinear),
            //q ranged
            CreateLinearSkillshot("JayceShockBlast", "JayceShockBlastMis", "Jayce", 1300f, 1450f, 1450f, 1450f, 70f, true, SpellInfo.SpellSlot.Q),
            //w
            CreateSelfActive("JayceStaticField", "Jayce", "JayceStaticField", 200f, SpellInfo.SpellSlot.W),
            //w ranged
            CreatePassiveSpell("JayceHyperCharge", "Jayce", "JayceHyperCharge", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            //e
            CreateTargetedSpell("JayceThunderingBlow", "Jayce", 240f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockBack),
            //e ranged
            CreateWall("JayceAccelerationGate", "Jayce", 700f, 0f, 1000f, 5f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None),
            //r
            CreatePassiveSpell("JayceStanceHtG", "Jayce", "jaycestancegun", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            CreatePassiveSpell("JayceStanceGtH", "Jayce", "jaycestancehammer", SpellInfo.SpellSlot.R),
            #endregion
            //add jhin stun only on buff
            #region Jhin
            //aa
            CreateAutoAttack("JhinBasicAttack", "Jhin", 2600f, 2600f, 2600f),
            CreateAutoAttack("JhinBasicAttack2", "Jhin", 2600f, 2600f, 2600f),
            CreateAutoAttack("JhinBasicAttack3", "Jhin", 2600f, 2600f, 2600f),
            CreateAutoAttack("JhinPassiveAttack", "Jhin", 2600f, 2600f, 2600f),
            CreateAutoAttack("JhinCritAttack", "Jhin", 2600f, 2600f, 2600f),
            //q
            CreateTargetedMissile("JhinQ", "JhinQ", "Jhin", 550f, 1800f, 1800f, 1800f, SpellInfo.SpellSlot.Q),
            CreateTargetedMissile("JhinQ", "JhinQMisBounce", "Jhin", 550f, 1800f, 1800f, 1800f, SpellInfo.SpellSlot.Q),
            //w
            CreateLinearSpell("JhinW", "Jhin", 1f, 2500f, 40f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e handled as a trap
            /*
            SpellName = "JhinE",
                ChampionName = "Jhin",
                Range = 750f,
                MissileSpeed = 1000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 120f,
                Radius = 135f,
                ConeDegrees = 45f,

                MissileName = "JhinETrap",
                ChampionName = "Jhin",
                MissileSpeed = 1600f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 2000f,
                Width = 120f,
                */
            //r
            CreateLinearSkillshot("JhinRShot", "JhinRShotMis4", "Jhin", 3500f, 1200f, 1200f, 1200f, 80f, false, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.HeavySlow),
            CreateLinearSkillshot("JhinRShot", "JhinRShotMis", "Jhin", 3500f, 1200f, 1200f, 1200f, 80f, false, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.HeavySlow),
            /*
            SpellName = "JhinR",
                ChampionName = "Jhin",
                Range = 25000f,
                MissileSpeed = 828.5f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 550f,
                ConeDegrees = 45f,
                */
                #endregion
            #region Jinx
            //aa
            CreateAutoAttack("JinxBasicAttack", "Jinx", 2750f, 2750f, 2750f),
            CreateAutoAttack("JinxBasicAttack2", "Jinx", 2750f, 2750f, 2750f),
            CreateAutoAttack("JinxCritAttack", "Jinx", 2750f, 2750f, 2750f),
            //rocket aa
            CreateAutoAttackWithSplashDamage("JinxQAttack", "Jinx", 2000f, 2000f, 2000f, 100f),
            CreateAutoAttackWithSplashDamage("JinxQAttack2", "Jinx", 2000f, 2000f, 2000f, 100f),
            CreateAutoAttackWithSplashDamage("ItemHurricaneJinxAttack", "Jinx", 2000f, 2000f, 2000f, 100f),
            CreateAutoAttackWithSplashDamage("JinxQCritAttack", "Jinx", 2000f, 2000f, 2000f, 100f),
            //q
            CreatePassiveSpell("JinxQ", "Jinx", "JinxQ", SpellInfo.SpellSlot.Q),
            //w
            CreateLinearSkillshot("JinxW",  "JinxWMissile", "Jinx", 1600f, 1200f, 1200f, 1200f, 60f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            CreateLinearSpell("JinxWMissile", "Jinx", 0f, 1500f, 60f, SpellInfo.SpellSlot.W),
            //e handled under trap handler
            //r
            CreateLinearSkillshot("JinxR", "JinxR", "Jinx", int.MaxValue, 1700f, 1700f, 1700f, 140f, false, SpellInfo.SpellSlot.R),
            #endregion
            #region Kalista
            //q
            CreateLinearSkillshot("KalistaMysticShot", "KalistaMysticShotMisTrue", "Kalista", 1200f, 2400f, 2400f, 2400f, 40f, false, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("KalistaW", "Kalista", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            //e
            CreateTargetedMissile("KalistaExpunge", "KalistaExpungeParticle", "Kalista", 1200f, 3000f, 3000f, 3000f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //r
            CreatePassiveSpell("KalistaRx", "Kalista", SpellInfo.SpellSlot.R),
            CreateLinearSkillshot("KalistaRx", "KalistaRMis", "Kalista", 1450f, 1500f, 1500f, 1500f, 80f, true, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            //w buff name
            #region Karma
            //q
            CreateLinearSkillshot("KarmaQ", "KarmaQMissile", "Karma", 1050f, 1700f, 1700f, 1700f, 60f, false, SpellInfo.SpellSlot.Q),
            //rq
            CreateLinearSkillshot("KarmaQ", "KarmaQMissileMantra", "Karma", 950f, 1700f, 1700f, 1700f, 80f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateTargetedChannel("KarmaSpiritBind", "Karma", "null", 700f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Root),
            //e
            CreatePassiveSpell("KarmaSolKimShield", "Karma", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //r
            CreatePassiveSpell("KarmaMantra", "Karma", SpellInfo.SpellSlot.R),
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
            CreateWall("KarthusWallOfPain", "Karthus", 1000f, 5f, 5f, 5f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            //e
            CreateSelfActive("KarthusDefile", "Karthus", "KarthusDefile", 550f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedSpell("KathusFallenOne", "Karthus", int.MaxValue, 3f, SpellInfo.SpellSlot.R),
            #endregion
            #region Kassadin
            //q
            CreateTargetedMissile("NullLance", "NullLance", "Kassadin", 650f, 1400f, 1400f, 1400f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("NetherBlade", "Kassadin", "NetherBlade", SpellInfo.SpellSlot.W),
            //e
            CreateConeSpell("ForcePulse", "Kassadin", 700f, 80f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateCircularSkillshotDash("RiftWalk", "Kassadin", 500f, 150f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Blink, SpellInfo.CrowdControlType.None),
            #endregion
            #region Katarina
            //q
            CreateTargetedMissile("KatarinaQ", "KatarinaQ", "Katarina", 675f, 1800f, 1100f, 1100f, SpellInfo.SpellSlot.Q),
            CreateTargetedMissile("KatarinaQ", "KatarinaQMis", "Katarina", 675f, 1800f, 1100f, 1100f, SpellInfo.SpellSlot.Q),
            //w
            CreateSelfActive("KatarinaW", "Katarina", 60f, SpellInfo.SpellSlot.W),
            //e
            CreateTargetedDash("KatarinaE", "Katarina", 725f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Blink),
            //r
            CreateTargetedMissile("KatarinaR", "KatarinaRMis", "Katarina", 550f, 2400f, 2400f, 2400f, SpellInfo.SpellSlot.R),
            #endregion
            #region Kayle
            //q
            CreateTargetedMissile("JudicatorReckoning", "JudicatorReckoning", "Kayle", 650f, 1500f, 1300f, 1300f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateTargetedPassiveSpell("JudicatorDivineBlessing", "Kayle", "JudicatorDivineBlessing", 900f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //e
            CreatePassiveSpell("JudicatorRighteousFury", "Kayle", "JudicatorRighteousFury", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //r
            CreateTargetedPassiveSpell("JudicatorIntervention", "Kayle", "JudicatorIntervention", 900f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Invulnerability),
            #endregion
            //kennen ability stun conditions
            #region Kennen
            //aa
            CreateAutoAttack("KennenMegaProc", "Kennen", 1700f, 1700f, 1700f, SpellInfo.CrowdControlType.Stun),
            //q
            CreateLinearSkillshot("KennenShurikenHurlMissile1", "KennenShurikenHurlMissile1", "Kennen", 1050f, 1700f, 1700f, 1700f, 50f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateTargetedSpell("KennenBringTheLight", "Kennen", 725f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e
            CreateSelfActive("KennenLightningRush", "Kennen", "KennenLightningRush", 100f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun, SpellInfo.Buff.SpeedUp),
            //r
            CreateSelfActive("KennenShurikenStorm", "Kennen", "KennenShurikenStorm", 780f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
            #endregion
            #region KhaZix
            //q
            CreateTargetedSpell("KhazixQ", "Khazix", 325f, SpellInfo.SpellSlot.Q),
            //evo q
            CreateTargetedSpell("KhazixQLong", "Khazix", 375f, SpellInfo.SpellSlot.Q),
            //w
            CreateLinearSkillshot("KhazixW", "KhazixWMissile", "Khazix", 1025f, 1700f, 1700f, 1700f, 70f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            //Evo W
            CreateLinearSkillshot("KhazixWLong", "KhazixWMissile", "Khazix", 1025f, 1700f, 1700f, 1700f, 70f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            //E
            CreateCircularSkillshotDash("KhazixE", "Khazix", 700f, 300f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Linear),
            /*
            MissileName = "KhazixEInvisMissile",
                ChampionName = "Khazix",
                MissileSpeed = 1000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 120f,
                */
            //Evo E
            CreateCircularSkillshotDash("KhazixLongE", "Khazix", 900f, 300f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Linear),
            //r
            CreatePassiveSpell("KhazixR", "Khazix", "khazixrstealth", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //Evo r
            CreatePassiveSpell("KhazixLongR", "Khazix", "khazixrstealth", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //evolutions
            CreatePassiveSpell("KhazixQEvo", "Khazix", "KhazixQEvo", SpellInfo.SpellSlot.Q),
            CreatePassiveSpell("KhazixWvo", "Khazix", "KhazixWEvo", SpellInfo.SpellSlot.W),
            CreatePassiveSpell("KhazixEEvo", "Khazix", "KhazixEEvo", SpellInfo.SpellSlot.E),
            CreatePassiveSpell("KhazixREvo", "Khazix", "KhazixREvo", SpellInfo.SpellSlot.R),
            #endregion
            //Kindred Q Range
            #region Kindred
            //aa
            CreateAutoAttack("KindredBasicAttack", "Kindred", 2000f, 2000f, 2000f),
            CreateAutoAttack("KindredBasicAttackBounty1", "Kindred", 2000f, 2000f, 2000f),
            CreateAutoAttack("KindredBasicAttackBounty2", "Kindred", 2000f, 2000f, 2000f),
            CreateAutoAttack("KindredBasicAttackBounty3", "Kindred", 2000f, 2000f, 2000f),
            CreateAutoAttack("KindredBasicAttackOverrideLightbombFinal", "Kindred", 2000f, 2000f, 2000f),
            //q
            CreateLinearDash("KindredQ", "Kindred", 200f, 0f, SpellInfo.SpellSlot.Q),
            CreateTargetedMissile("null", "KindredQMissile", "Kindred", 1800f, 1600f, 1600f, 1600f, SpellInfo.SpellSlot.Auto),
            //w
            CreateCircularSpell("KindredW", "Kindred", 0f, 800f, false, SpellInfo.SpellSlot.W, SpellInfo.Buff.None),
            //e
            CreateTargetedMissile("KindredE", "KindredE", "Kindred", 500f, 1600f, 1600f, 1600f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            CreateAutoAttack("KindredEWolfMissile", "Kindred", 1400f, 1400f, 1400f),
            /*
            SpellName = "KindredEWrapper",
                ChampionName = "Kindred",
                Range = 1200f,
                MissileSpeed = 2200f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 210f,
                ConeDegrees = 45f,
            */
            //r
            CreateCircularSpell("KindredR", "Kindred", 4f, 500f, 1200f, false, SpellInfo.SpellSlot.R, SpellInfo.Buff.Invulnerability),
            /*
                SpellName = "KindredFakeCastTimeSpell",
                ChampionName = "Kindred",
                Range = 400f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 400f,
                ConeDegrees = 45f,
                */
            #endregion
            //Kled Q channel on catch. It doesn't have a spell name. It does have buff "kledqmark"
            //Kled E width
            //Kled R Fix
            #region Kled
            //q on skarrl
            CreateLinearSkillshot("KledQ", "KledQMissile", "Kled", 800f, 1600f, 1600f, 1600f, 45f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Pull),
            //q off skarrl
            CreateConeSpell("KledRiderQ", "Kled", 700f, 45f, SpellInfo.SpellSlot.Q),
            CreateLinearMissile("KledRiderQMissile", "Kled", 700f, 3000f, 3000f, 3000f, 40f),
            //w
            CreatePassiveSpell("null", "Kled", "kledwactive", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //w skarrl
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpell,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e skarrl
            CreateLinearDash("KledE", "Kled", 550f, 50f, SpellInfo.SpellSlot.E),
            //e skarrl hit enemy 1
            CreateTargetedDash("KledE", "Kled", 550f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.TargetedLinear),
            //e skarrl hit enemy 2
            CreateTargetedDash("KledE2", "Kled", 625f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.TargetedLinear),
            //r
            CreateLinearDash("KledR", "Kled", 3500f, 0f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack),
            #endregion
            #region KogMaw
            //q
            CreateLinearSkillshot("KogMawQ", "KogMawQ", "KogMaw", 1200f, 1650f, 1650f, 1650f, 70f, false, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("KogMawBioArcaneBarrage", "KogMaw", "KogMawBioArcaneBarrage", 0f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AutoAttackRangeIncrease),
            //e
            CreateLinearSkillshot("KogMawVoidOozeMissile", "KogMawVoidOozeMissile", "KogMaw", 1500f, 1400f, 1400f, 1400f, 120f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateCircularSpell("KogMawLivingArtillery", "KogMaw", 1200f, 240f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.None),
            #endregion
            //LB Ult spells are coded as when she has the buff: " "
            #region LeBlanc
            //aa
            CreateAutoAttack("LeblancBasicAttack", "Leblanc", 1700f, 1700f, 1700f),
            CreateAutoAttack("LeblancBasicAttack2", "Leblanc", 1700f, 1700f, 1700f),
            CreateAutoAttack("LeblancBasicAttack3", "Leblanc", 1700f, 1700f, 1700f),
            //q
            CreateTargetedMissile("LeblancQ", "LeblancQ", "Leblanc", 700f, 2000f, 2000f, 2000f, SpellInfo.SpellSlot.Q),
            //w
            CreateCircularSkillshotDash("LeblancW", "Leblanc", 600f, 220f, SpellInfo.SpellSlot.W, SpellInfo.Dashtype.Linear),
            CreatePassiveSpell("LeblancWReturn", "Leblanc", SpellInfo.SpellSlot.W),
            //CreatePassiveSpell("LeblancWReturnSFX", "Leblanc", SpellInfo.SpellSlot.W),
            //e
            //e channel
            CreateLinearSkillshot("LeblancE", "LeblancEMissile", "Leblanc", 950f, 1750f, 1750f, 1750f, 55f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreatePassiveSpell("LeblancRToggle", "Leblanc", "LeblancR", SpellInfo.SpellSlot.R),//e
            //re channel
            CreateLinearSkillshot("LeblancE", "LeblancRE", "Leblanc", 950f, 1750f, 1750f, 1750f, 55f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            #endregion
            //lee sin w buff name
            #region LeeSin
            //q
            CreateLinearSkillshot("BlindMonkQOne", "BlindMonkQOne", "LeeSin", 1100f, 1800f, 1500f, 1500f, 60f, false, SpellInfo.SpellSlot.Q),
            //q2
            CreateTargetedDash("BlindMonkQTwo", "LeeSin", 1300f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.Targeted),
            //w
            CreateTargetedDash("BlindMonkWOne", "LeeSin", 700f, SpellInfo.SpellSlot.W, SpellInfo.Dashtype.TargetedLinear),
            //w2
            CreatePassiveSpell("BlindMonkWTwo", "LeeSin", 0f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.LifestealSpellVamp),
            //e
            CreateSelfActive("BlindMonkEOne", "LeeSin", 425f, SpellInfo.SpellSlot.E),
            //e2
            CreateTargetedMissile("BlindMonkETwo", "BlindMonkETwoMissile", "LeeSin", 575f, 1600f, 1400f, 1400f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateTargetedSpell("BlindMonkRKick", "LeeSin", 375f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack),
            #endregion
            #region Leona
            //aa
            CreateTargetedSpell("LeonaShieldOfDaybreakAttack", "Leona", 200f, SpellInfo.SpellSlot.Auto, SpellInfo.CrowdControlType.Stun),
            //q
            CreatePassiveSpell("LeonaShieldOfDaybreak", "Leona", "LeonaShieldOfDaybreak", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateSelfActive("LeonaSolarBarrier", "Leona", "LeonaSolarBarrier", 500f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            //e
            CreateLinearSkillshot("LeonaZenithBlade", "LeonaZenithBladeMissile", "Leona", 900f, 2000f, 2000f, 2000f, 70f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            //r
            CreateCircularSpell("LeonaSolarFlare", "Leona", 1200f, 120f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Stun),
            #endregion
            #region Lissandra
            //aa
            CreateAutoAttack("LissandraBasicAttack", "Lissandra", 2000f, 2000f, 2000f),
            CreateAutoAttack("LissandraBasicAttack2", "Lissandra", 2000f, 2000f, 2000f),
            //q
            CreateLinearSpell("LissandraQ", "Lissandra", 0f, 700f, 75f, SpellInfo.SpellSlot.Q),
            CreateLinearSkillshot("LissandraQMissile", "LissandraQMissile", "Lissandra", 700f, 2200f, 2200f, 2200f, 75f, false, SpellInfo.SpellSlot.Q),
            CreateLinearMissile("LissandraQShards", "Lissandra", 700f, 2200f, 2200f, 2200f, 90f),
            //w
            CreateSelfActive("LissandraW", "Lissandra", 275f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e
            CreateLinearSpell("LissandraE", "Lissandra", 0f, 1025f, 110f, SpellInfo.SpellSlot.Q),
            CreateLinearSkillshot("LissandraEMissile", "LissandraEMissile", "Lissandra", 1025f, 850f, 850f, 850f, 125f, false, SpellInfo.SpellSlot.E),
            //r
            //not sure if this is the same for ally or not
            CreateTargetedSpell("LissandraR", "Lissandra", 550f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun, SpellInfo.Buff.Invulnerability),
            CreateTargetedActive("LissandraR", "Lissandra", "LissandraR", 550f, 450f, SpellInfo.SpellSlot.R),
            CreateTargetedSpell("LissandraREnemy", "Lissandra", 550f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun, SpellInfo.Buff.Invulnerability),
            CreateTargetedActive("LissandraREnemy", "Lissandra", "LissandraR", 550f, 450f, SpellInfo.SpellSlot.R),
            #endregion
            #region Lucian
            //q
            CreateLinearSpell("LucianQ", "Lucian", 0.25f, 500f, 65f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None),
            //w
            CreateLinearSkillshot("LucianW", "LucianWMissile", "Lucian", 900f, 1600f, 1600f, 1600f, 55f, false, SpellInfo.SpellSlot.W),
            //e
            CreateLinearDash("LucianE", "Lucian", 425f, 50f, SpellInfo.SpellSlot.E),
            //r
            CreateLinearSkillshot("LucianR", "LucianRMissile", "Lucian", 1200f, 2800f, 2800f, 2800f, 110f, false, SpellInfo.SpellSlot.R),
            CreateLinearSkillshot("LucianR", "LucianRMissileOffhand", "Lucian", 1200f, 2800f, 2800f, 2800f, 110f, false, SpellInfo.SpellSlot.R),
            #endregion
            //lulu Q coming off pix
            //lulu W and E are different on allys than enemies
            #region Lulu
            //aa
            CreateAutoAttack("LuluBasicAttack", "Lulu", 1450f, 1450f, 1450f),
            CreateAutoAttack("LuluBasicAttack2", "Lulu", 1450f, 1450f, 1450f),
            //q
            CreateLinearSkillshot("LuluQ", "LuluQMissile", "Lulu", 950f, 1450f, 1450f, 1450f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            CreateLinearSkillshot("LuluQMissile", "LuluQMissileTwo", "Lulu", 950f, 1450f, 1450f, 1450f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            /*
            MissileName = "LuluPassiveMissileController",
                ChampionName = "Lulu",
                MissileSpeed = 1000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 350f,
                Width = 1f,

                MissileName = "LuluPassiveMissile",
                ChampionName = "Lulu",
                MissileSpeed = 900f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 30f,
                */
            //w
            CreateTargetedSpell("LuluW", "Lulu", 650f, SpellInfo.SpellSlot.W),
            //e
            CreateTargetedSpell("LuluE", "Lulu", 650f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedPassiveSpell("LuluR", "Lulu", "LuluR", 900f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            CreateTargetedActive("LuluR", "Lulu", "LuluR", 900f, 210f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            #region Lux
            //aa
            CreateAutoAttack("LuxBasicAttack", "Lux", 1600f, 1600f, 1600f),
            CreateAutoAttack("LuxBasicAttack2", "Lux", 1600f, 1600f, 1600f),
            //q
            CreateLinearSkillshot("LuxLightBinding", "LuxLightBindingMis", "Lux", 1200f, 1200f, 1200f, 1200f, 70f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            CreateLinearSkillshot("LuxLightBinding", "LuxLightBindingDummy", "Lux", 1200f, 1200f, 1200f, 1200f, 70f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateLinearSkillshotNoDamage("LuxPrismaticWave", "LuxPrismaticWaveMissile", "Lux", 1175f, 2200f, 2200f, 2200f, 110f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            CreateLinearSkillshotNoDamage("LuxPrismaticWaveReturn", "LuxPrismaticWaveMissile", "Lux", 1175f, 2200f, 2200f, 2200f, 110f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            CreateLinearSkillshotNoDamage("LuxPrismaticWaveReturnDead", "LuxPrismaticWaveMissile", "Lux", 1175f, 2200f, 2200f, 2200f, 110f, false, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //e
            //this is the missile. The ball is handled by Particle Database
            CreateLinearSkillshot("LuxLightStrikeKugel", "LuxLightStrikeKugel", "Lux", 1100f, 1300f, 1300f, 1300f, 55f, true, SpellInfo.SpellSlot.E),

            //treating this as a passive to keep it from printing in the console.
            CreatePassiveSpell("LuxLightstrikeToggle", "Lux", SpellInfo.SpellSlot.E),
             /*   SpellName = "LuxLightstrikeToggle",
                ChampionName = "Lux",
                Range = 1200f,
                MissileSpeed = 1400f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 250f,
                ConeDegrees = 45f,*/
            //r
            CreateLinearSpell("LuxMaliceCannon", "Lux", 0.5f, 3500f, 250f, SpellInfo.SpellSlot.R),
            CreateLinearSpell("LuxMaliceCannonMis", "Lux", 0.1f, 3500f, 250f, SpellInfo.SpellSlot.R),
            #endregion
            #region Malphite
            //q
            CreateTargetedMissile("SeismicShard", "SeismicShard", "Malphite", 625f, 1200f, 1200f, 1200f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreatePassiveSpell("Obduracy", "Malphite", "MalphiteCleave", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            //e
            CreateSelfActive("LandSlide", "Malphite", 400f, SpellInfo.SpellSlot.E),
            //r
            CreateCircularSkillshotDash("UFSlash", "Malphite", 1000f, 300f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Linear, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            //check malz Q
            #region Malzahar
            //aa
            CreateAutoAttack("MalzaharBasicAttack", "Malzahar", 2000f, 2000f, 2000f),
            CreateAutoAttack("MalzaharBasicAttack2", "Malzahar", 2000f, 2000f, 2000f),
            //q
            CreateWall("MalzaharQ", "Malzahar", 900f, 85f, 750f, 0.25f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Silence),
            /*
                MissileName = "MalzaharQMissile",
                ChampionName = "Malzahar",
                MissileSpeed = 1600f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 750f,
                Width = 85f,
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.Silence,
                Slot = SpellInfo.SpellSlot.Q,
            */
            //w
            CreatePassiveSpell("MalzaharW", "Malzahar", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            //e
            //explosion radius is 500f
            CreateTargetedSpell("MalzaharE", "Malzahar", 650f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedChannel("MalzaharR", "Malzahar", "MalzaharR", 700f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Suppression),
            #endregion
            #region Maokai
            //q
            CreateLinearSkillshot("MaokaiTrunkLineMissile", "MaokaiTrunkLineMissile", "Maokai", 650f, 1800f, 1800f, 1800f, 110f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockBack),
            /*
            SpellName = "MaokaiTrunkLine",
                ChampionName = "Maokai",
                Range = 200000f,
                MissileSpeed = 1100f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 110f,
                Radius = 275f,
                ConeDegrees = 45f,
            */
            //w
            CreateTargetedDash("MaokaiUnstableGrowth", "Maokai", 525f, SpellInfo.SpellSlot.W, SpellInfo.Dashtype.Targeted, SpellInfo.CrowdControlType.Stun),
            //e
            CreateCircularSkillshot("MaokaiSapling2Boom", "MaokaiSapling2Boom", "Maokai", 1100f, 1500f, 1500f, 1500f, 225f, true, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            /*
            SpellName = "MaokaiSapling2",
                ChampionName = "Maokai",
                Range = 1100f,
                MissileSpeed = 1750f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 225f,
                ConeDegrees = 45f,
                */
            //r
            CreateSelfActiveNoDamage("MaokaiDrain3", "Maokai", "MaokaiDrain3", 500f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            CreateSelfActive("MaokaiDrain3Toggle", "Maokai", 500f, SpellInfo.SpellSlot.R),
            #endregion
            #region MasterYi
            //q
            //has a missile name "AlphaStrikeMissile"
            CreateTargetedSpell("AlphaStrike", "MasterYi", 600f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Invulnerability),
            //w
            CreatePassiveSpell("Meditate", "MasterYi", "Meditate", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //e
            CreatePassiveSpell("WujuStyle", "MasterYi", "WujuStyle", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //r
            CreatePassiveSpell("Highlander", "MasterYi", "Highlander", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            #endregion
            #region MissFortune
            //q
            CreateTargetedMissile("MissFortuneRicochetShot", "MissFortuneRicochetShot", "MissFortune", 550f, 1400f, 1400f, 1400f, SpellInfo.SpellSlot.Q),
            CreateTargetedMissile("MissFortuneRicochetShot", "MissFortuneRShotExtra", "MissFortune", 575f, 1400f, 1400f, 1400f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("MissFortuneViciousStrikes", "MissFortune", "missfortunestrutstacks", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //e
            CreateCircularSpell("MissFortuneScattershot", "MissFortune", 2f, 1000f, 350f, true, SpellInfo.SpellSlot.E, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Slow),
            //r
            CreateConeSpell("MissFortuneBulletTime", "MissFortune", "missfortunebulletsound", 1000, 17f, SpellInfo.SpellSlot.R),
            //missile name "MissFortuneBullets", and speeds  MissileSpeed = 2000f, MissileMinSpeed = 2000f, MissileMaxSpeed = 2000f, Width = 40f
            #endregion
            #region Mordekaiser
            //q
            CreatePassiveSpell("MordekaiserMaceOfSpades", "Mordekaiser", "MordekaiserMaceOfSpades", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            CreateTargetedSpell("MordekaiserQAttack", "Mordekaiser", 500f, SpellInfo.SpellSlot.Q),
            CreateTargetedSpell("MordekaiserQAttack1", "Mordekaiser", 500f, SpellInfo.SpellSlot.Q),
            CreateTargetedSpell("MordekaiserQAttack2", "Mordekaiser", 500f, SpellInfo.SpellSlot.Q),
            //w
            CreateTargetedActive("MordekaiserCreepingDeathCast", "Mordekaiser", "MordekaiserCreepingDeath", 1000f, 300f, SpellInfo.SpellSlot.W),
            CreateSelfActive("MordekaiserCreepingDeathCast", "Mordekaiser", "MordekaiserCreepingDeath", 300f, SpellInfo.SpellSlot.W),
            CreatePassiveSpell("MordekaiserCreepingDeath2", "Mordekaiser", SpellInfo.SpellSlot.W),
            //this is his w heal missile
            //CreateAutoAttack("MordekaiserWHeal", "Mordekaiser", 1000f, 1000f, 1000f),
            //e
            CreateConeSpell("MordekaiserSyphonOfDestruction", "Mordekaiser", 700f, 25f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedSpell("MordekaiserChildrenOfTheGrave", "Mordekaiser", 650f, SpellInfo.SpellSlot.R),
            CreatePassiveSpell("MordekaiserCotGGuide", "Mordekaiser", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            #endregion
            //morgana e/r buff name
            #region Morgana
            //q
            CreateLinearSkillshot("DarkBindingMissile", "DarkBindingMissile", "Morgana", 1300f, 1200f, 1200f, 1200f, 90f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w handled as partical 
            //e
            CreateTargetedSpell("BlackShield", "Morgana", 800f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpellShield),
            //r
            CreateSelfActive("SoulShackles", "Morgana", "null", 625f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
            #endregion
            //nami w on enemy
            #region Nami
            //q
            CreateLinearSkillshotNoDamage("NamiQMissile", "NamiQMissile", "Nami", 850f, 2500f, 2500f, 2500f, 175f, true, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp),
            CreateCircularSpell("NamiQ", "Nami", 0.75f, 850f, 175f, true, SpellInfo.SpellSlot.Q, SpellInfo.Buff.None, SpellInfo.CrowdControlType.KnockUp),
            //w
            CreateTargetedMissile("NamiW", "NamiWEnemy", "Nami", 725f, 2500f, 2500f, 2500f, SpellInfo.SpellSlot.W),
            CreateTargetedMissile("NamiW", "NamiWAlly", "Nami", 725f, 2500f, 2500f, 2500f, SpellInfo.SpellSlot.W),
            //e
            CreateTargetedSpell("NamiE", "Nami", 800f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //r
            CreateLinearSpell("NamiR", "Nami", 0f, 2750f, 400f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            CreateLinearSkillshot("NamiRMissile", "NamiRMissile", "Nami", 2750f, 850f, 850f, 850f, 400f, false, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            #region Nasus
            //q
            CreatePassiveSpell("NasusQ", "Nasus", "NasusQ", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //w
            CreateTargetedSpell("NasusW", "Nasus", 600f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.HeavySlow),
            //e
            CreateCircularSpell("NasusE", "Nasus", 5f, 650f, 380f, true, SpellInfo.SpellSlot.Q, SpellInfo.Buff.None),
            //r
            CreateSelfActive("NasusR", "Nasus", "NasusR", 175f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            #endregion
            #region Nautilus
            //q
            CreateLinearSpell("NautilusAnchorDrag", "Nautilus", 0f, 1000f, 90f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp),
            CreateLinearSkillshot("NautilusAnchorDragMissile", "NautilusAnchorDragMissile", "Nautilus", 1000f, 2000f, 2000f, 2000f, 90f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp),
            //w
            CreatePassiveSpell("NautilusPiercingGaze", "Nautilus", "nautiluspiercinggazeshield", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //e
            CreateCircularSpell("NautilusSplashZone", "Nautilus", 1f, 0f, 600f, false, SpellInfo.SpellSlot.E, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Slow),
            /*
            new SpellInfo()
            {
                SpellName = "NautilusSplashZone",
                ChampionName = "Nautilus",
                Range = 600f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 600f,
                ConeDegrees = 45f,
                
                MissileName = "NautilusSplashZoneSplash",
                ChampionName = "Nautilus",
                MissileSpeed = 450f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 350f,
                Width = 85f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            */
            //r
            CreateTargetedSpell("NautilusGrandLine", "Nautilus", 825f, 0f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            /*new SpellInfo()
            {
                 SpellName = "NautilusGrandLine",
                ChampionName = "Nautilus",
                Range = 825f,
                MissileSpeed = 1400f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 100f,
                ConeDegrees = 45f,

                SpellType = SpellInfo.SpellTypeInfo.TargetedMissile,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },*/
            #endregion
            //nidalee W jump
            #region Nidalee
            //aa
            CreateAutoAttack("NidaleeBasicAttack", "Nidalee", 2500f, 2500f, 2500f),
            CreateAutoAttack("NidaleeBasicAttack2", "Nidalee", 2500f, 2500f, 2500f),
            //q
            CreateLinearSkillshot("JavelinToss", "JavelinToss", "Nidalee", 1500f, 1300f, 1300f, 1300f, 40f, false, SpellInfo.SpellSlot.Q),
            //q tiger
            CreatePassiveSpell("Takedown", "Nidalee", "Takedown", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //w handled as trap
            /*
                SpellName = "Bushwhack",
                ChampionName = "Nidalee",
                Range = 900f,
                MissileSpeed = 1450f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 80f,
                ConeDegrees = 45f,
            */
            //w tiger
            CreateCircularSkillshotDash("Pounce", "Nidalee", 350f, 150f, SpellInfo.SpellSlot.W, SpellInfo.Dashtype.FixedDistance),  
            //e
            CreateTargetedPassiveSpell("PrimalSurge", "Nidalee", "PrimalSurge", 600f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //e tiger
            CreateConeSpell("Swipe", "Nidalee", 325f, 90f, SpellInfo.SpellSlot.E),
            //r
            CreatePassiveSpell("AspectOfTheCougar", "Nidalee", SpellInfo.SpellSlot.R),
            #endregion
            #region Nocturne
            //q
            CreateLinearSkillshot("NocturneDuskbringer", "NocturneDuskbringer", "Nocturne", 1200f, 1400f, 1400f, 1400f, 60f, false, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("NocturneShroudofDarkness", "Nocturne", "NocturneShroudofDarkness", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpellShield),
            //e
            CreateTargetedChannel("NocturneUnspeakableHorror", "Nocturne", "NocturneUnspeakableHorror", 675f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Fear),
            //r
            CreatePassiveSpell("NocturnedParanoia", "Nocturne", "NocturneParanoia", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Nearsight, SpellInfo.Buff.None),
            CreateTargetedDash("NocturneParanoia2", "Nocturne", 2500, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Targeted),
            #endregion
            #region Nunu
            //q
            CreateTargetedSpell("Consume", "Nunu", 125f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //w
            CreateTargetedPassiveSpell("BloodBoil", "Nunu", "BloodBoil", 700f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            //e
            CreateTargetedMissile("IceBlast", "IceBlast", "Nunu", 550f, 1000f, 1000f, 1000f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateSelfActive("AbsoluteZero", "Nunu", "AbsoluteZero", 650f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Slow),
            #endregion
            //olaf w and r name
            #region Olaf
            //q
            CreateLinearSkillshot("OlafAxeThrowCast", "OlafAxeThrow", "Olaf", 1000f, 1550f, 1550f, 1550f, 60f, true, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreatePassiveSpell("OlafFrenziedStrikes", "Olaf", 0f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.LifestealSpellVamp),
            //e
            CreateTargetedSpell("OlafRecklessStrike", "Olaf", 325f, SpellInfo.SpellSlot.E),
            //r
            CreatePassiveSpell("OlafRagnarok", "Olaf", 6f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.CCImmunity),
            #endregion
            //ball name is "TheDoomBall"
            //spells need to come off ball
            #region Orianna
            //aa
            CreateAutoAttack("OriannaBasicAttack", "Orianna", 1450f, 1450f, 1450f),
            CreateAutoAttack("OriannaBasicAttack2", "Orianna", 1450f, 1450f, 1450f),
            CreateAutoAttack("OriannaBasicAttack3", "Orianna", 1450f, 1450f, 1450f),
            //q
            CreateLinearSkillshot("OrianaIzunaCommand", "OrianaIzuna", "Orianna", 825f, 1200f, 1200f, 1200f, 80f, true, SpellInfo.SpellSlot.Q),
            //w
            CreateSelfActive("OrianaDissonanceCommand", "Orianna", 255f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //e
            CreateTargetedPassiveSpell("OrianaRedactCommand", "Orianna", "orianaredactshield", 1120f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            CreateLinearSkillshot("OrianaRedactCommand", "Orianna", "OrianaRedact", 1120f, 1850f, 1850f, 1850f, 80f, false, SpellInfo.SpellSlot.E),
            //r
            CreateSelfActive("OrianaDetonateCommand", "Orianna", 410f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Pull, SpellInfo.Buff.None),
            #endregion
            #region Pantheon
            CreateTargetedMissile("PantheonQ", "PantheonQ", "Pantheon", 600f, 1500f, 1500f, 1500f, SpellInfo.SpellSlot.Q),
            //w
            CreateTargetedDash("PantheonW", "Pantheon", 600f, SpellInfo.SpellSlot.W, SpellInfo.Dashtype.TargetedLinear, SpellInfo.CrowdControlType.Stun),
            //e
            CreateConeSpell("PantheonE", "Pantheon", "pantheonesound", 400f, 35f, SpellInfo.SpellSlot.E),
            //r
            CreateCircularSkillshotDash("PantheonRJump", "Pantheon", 5500f, 750f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Blink),
            CreateCircularSpell("PantheonRFall", "Pantheon", 0f, 700f, false, SpellInfo.SpellSlot.R, SpellInfo.Buff.None),
            #endregion
            //poppy E stun logic
            //poppy r scaling range
            #region Poppy
            //aa
            CreateAutoAttack("PoppyPassiveAttack", "Poppy", 1600f, 1600f, 1600f),
            //passive bounce to ground
            //CreateLinearMissile("PoppyPassiveBounce", "Poppy", int.MaxValue, 500f, 500f, 500f, 0f, SpellInfo.CrowdControlType.None),
            //passive bounce to poppy
            //CreateLinearMissile("PoppyPassiveKillBounce", "Poppy", int.MaxValue, 1600f, 1600f, 1600f, 0f, SpellInfo.CrowdControlType.None),
            //q
            CreateLinearSpell("PoppyQ", "Poppy", 1f, 430f, 100f, SpellInfo.SpellSlot.Q),
            /*
            new SpellInfo()
            {
                SpellName = "PoppyQSpell",
                ChampionName = "Poppy",
                Range = 430f,
                MissileSpeed = 500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 100f,
                Radius = 0f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },*/
            //w
            CreateSelfActiveNoDamage("PoppyW", "Poppy", 400f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.KnockBack, SpellInfo.Buff.SpeedUp),
            //e
            CreateTargetedDash("PoppyE", "Poppy", 425f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.TargetedLinear, SpellInfo.CrowdControlType.Stun),
            //r
            CreateLinearSpell("PoppyRSpellInstant", "Poppy", 0f, 500f, 100f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            CreatePassiveSpell("PoppyR", "Poppy", "PoppyR", SpellInfo.SpellSlot.R),
            CreateLinearSkillshot("PoppyRSpell", "PoppyRMissile", "Poppy", 1000f, 1600f, 1600f, 1600f, 100f, true, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack),
            #endregion
            #region Quinn
            //aa
            CreateAutoAttack("QuinnBasicAttack", "Quinn", 2000f, 2000f, 2000f),
            //aa enhanced
            CreateAutoAttack("QuinnWEnhanced", "Quinn", 2000f, 2000f, 2000f),
            //q
            CreateLinearSkillshot("QuinnQ", "QuinnQ", "Quinn", 1050f, 1550f, 1550f, 1550f, 60f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Nearsight),
            //w
            CreatePassiveSpell("QuinnW", "Quinn", SpellInfo.SpellSlot.W),
            //e
            CreateTargetedDash("QuinnE", "Quinn", 600f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Targeted, SpellInfo.CrowdControlType.KnockBack),
            //r
            //entering bird form
            CreatePassiveSpell("QuinnR", "Quinn", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            /*new SpellInfo()
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpell,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },*/
            //quinn r dmg
            CreateSelfActive("QuinnRFinale", "Quinn", 700f, SpellInfo.SpellSlot.R),
            #endregion
            //rammus q/w/r buff names
            #region Rammus
            //q
            CreatePassiveSpell("PowerBall", "Rammus", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockBack, SpellInfo.Buff.SpeedUp),
            //w
            CreatePassiveSpell("DefensiveBallCurl", "Rammus", 0f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            //e
            CreateTargetedSpell("PuncturingTaunt", "Rammus", 325f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Taunt),
            //r
            CreateSelfActive("Tremors2", "Rammus", "null", 375f, SpellInfo.SpellSlot.R),
            #endregion
            //on buff lost, RekSaiW, self active was made
            //using tunnels. spell data below
            #region RekSai
            //q
            CreateSelfActive("RekSaiQAttack", "RekSai", 250f, SpellInfo.SpellSlot.Q),
            CreateSelfActive("RekSaiQAttack2", "RekSai", 250f, SpellInfo.SpellSlot.Q),
            CreateSelfActive("RekSaiQAttack3", "RekSai", 250f, SpellInfo.SpellSlot.Q),
            CreatePassiveSpell("RekSaiQ", "RekSai", "RekSaiQ", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //q burrowed
            CreateLinearSkillshot("RekSaiQBurrowed", "RekSaiQBurrowedMis", "RekSai", 1500f, 1600f, 1600f, 1600f, 60f, false, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("RekSaiW", "RekSai", "RekSaiW", SpellInfo.SpellSlot.W),
            //w burrowed
            CreatePassiveSpell("RekSaiWBurrowed", "RekSai", "RekSaiW", SpellInfo.SpellSlot.W),
            //e
            CreateTargetedSpell("RekSaiE", "RekSai", 225f, SpellInfo.SpellSlot.E),
            //e burrowed
            CreateLinearDash("RekSaiEBurrowed", "RekSai", 850f, 1f, 60f, SpellInfo.SpellSlot.E),
            /*
            SpellName = "RekSaiTunnelTime",
                ChampionName = "RekSai",
                Range = 200f,
                MissileSpeed = 2200f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 130f,
                ConeDegrees = 45f,
                */
            //r
            CreateTargetedDash("RekSaiR", "RekSai", int.MaxValue, 3f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.TargetedLinear),
            #endregion
            //renekton w and r buffnames
            #region Renekton
            //q
            CreateSelfActive("RenektonCleave", "Renekton", 325f, SpellInfo.SpellSlot.Q),
            //w just casting
            CreatePassiveSpell("RenektonPreExecute", "Renekton", 0f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun, SpellInfo.Buff.AttackDamageIncrease),
            //w during auto
            CreateTargetedSpell("RenektonExecute", "Renekton", 250f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //w during auto with enhanced w 
            CreatePassiveSpell("RenektonSuperExecute", "Renekton", 0f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun, SpellInfo.Buff.AttackDamageIncrease),
            //slice
            CreateLinearDash("RenektonSliceAndDice", "Renekton", 450f, 50f, SpellInfo.SpellSlot.E),
            //dice
            CreateLinearDash("RenektonDice", "Renekton", 450f, 50f, SpellInfo.SpellSlot.E),
            //r
            CreateSelfActive("RenektonReignOfTheTyrant", "Renekton", "null", 175f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            #endregion
            #region Rengar
            //q
            CreateConeSpell("RengarQ", "Rengar", 325f, 90f, SpellInfo.SpellSlot.Q),
            CreateLinearSpell("RengarQ", "Rengar", 0.25f, 450f, 55f, SpellInfo.SpellSlot.Q),
            /*
                SpellName = "RengarQSound",
                ChampionName = "Rengar",
                Range = 1f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 75f,
                ConeDegrees = 45f,

                SpellName = "RengarQ2",
                ChampionName = "Rengar",
                Range = 25000f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 55f,
                Radius = 300f,
                ConeDegrees = 45f,
            */
            //q emp
            CreateConeSpell("RengarQEmp", "Rengar", 325f, 90f, SpellInfo.SpellSlot.Q),
            CreateLinearSpell("RengarQEmp", "Rengar", 0.5f, 450f, 55f, SpellInfo.SpellSlot.Q),
            /*
                SpellName = "RengarQ2Emp",
                ChampionName = "Rengar",
                Range = 25000f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 55f,
                Radius = 300f,
                ConeDegrees = 45f,
                
                SpellName = "RengarQ2Sound",
                ChampionName = "Rengar",
                Range = 1f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 75f,
                ConeDegrees = 45f,
                */
            //w
            CreateSelfActive("RengarW", "Rengar", 450f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //w emp
            CreateSelfActive("RengarWEmp", "Rengar", 450f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //e
            CreateLinearSkillshot("RengarE", "RengarEMis", "Rengar", 1000f, 1500f, 1500f, 1500f, 70f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //e emp
            CreateLinearSkillshot("RengarEEmp", "RengarEEmpMis", "Rengar", 1000f, 1500f, 1500f, 1500f, 70f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            //r
            CreatePassiveSpell("RengarR", "Rengar", "RengarR", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            #endregion
            //Riven 3Q knockup. Added, but needs testing
            //Riven E range test
            #region Riven
            //Could Not reproduce Riven Tri Slash Buffer
            //Riven Q has a buff that has 1 stack per Q usage. So before Q3 is when she has 2 stacks, but during cast it is when she has 3 stacks
            //q
            //unsure of Q Range
            CreateLinearDash("RivenTriCleave", "Riven", 200f, 0f, SpellInfo.SpellSlot.Q),
            CreateConeSpell("RivenTriCleave", "Riven", "riventricleavesoundone", 200f, 90f, SpellInfo.SpellSlot.Q),
            CreateConeSpell("RivenTriCleave", "Riven", "riventricleavesoundtwo", 200f, 90f, SpellInfo.SpellSlot.Q),
            CreateConeSpell("RivenTriCleave", "Riven", "riventricleavesoundthree", 200f, 90f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp),
            //w
            CreateSelfActive("RivenMartyr", "Riven", 250f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Stun),
            //e
            //unsure of E range
            CreateLinearDash("RivenFeint", "Riven", 200f, 0f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //r
            //R initial cast
            CreatePassiveSpell("RivenFengShuiEngine", "Riven", "RivenFengShuiEngine", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //R Cast 2
            CreateConeSpell("RivenIzunaBlade", "Riven", 1075f, 22f, SpellInfo.SpellSlot.R),
            //R missiles
            CreateLinearMissile("RivenWindslashMissileCenter", "Riven", 1075f, 1600f, 1600f, 1600f, 100f),
            CreateLinearMissile("RivenWindslashMissileRight", "Riven", 1075f, 1600f, 1600f, 1600f, 100f),
            CreateLinearMissile("RivenWindslashMissileLeft", "Riven", 1075f, 1600f, 1600f, 1600f, 100f),
            #endregion  
            //rumble e spell name
            #region Rumble
            //q
            CreateConeSpell("RumbleFlameThrower", "Rumble", "RumbleFlameThrower", 600f, 32f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("RumbleShield", "Rumble", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //e
            CreateLinearSkillshot("RumbleGrenade", "RumbleGrenadeMissile", "Rumble", 850f, 2000f, 2000f, 2000f, 60f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
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
            //w stun calcs
            //r scaling range and correct drawings
            #region Ryze
            //aa
            CreateAutoAttack("RyzeBasicAttack", "Ryze", 2400f, 2400f, 2400f),
            CreateAutoAttack("RyzeBasicAttack2", "Ryze", 2400f, 2400f, 2400f),
            CreateAutoAttack("RyzeBasicAttack3", "Ryze", 2400f, 2400f, 2400f),
            CreateAutoAttack("RyzeBasicAttack4", "Ryze", 2400f, 2400f, 2400f),
            //q
            CreateLinearSkillshot("RyzeQ", "RyzeQ", "Ryze", 1000f, 1700f, 1700f, 1700f, 55f, false, SpellInfo.SpellSlot.Q),
            //w
            CreateTargetedSpell("RyzeW", "Ryze", 550f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //e
            CreateTargetedMissile("RyzeE", "RyzeE", "Ryze", 550f, 3500f, 3500f, 3500f, SpellInfo.SpellSlot.E),
            CreateTargetedMissile("RyzeE", "RyzeEBounce", "Ryze", 550f, 3500f, 3500f, 3500f, SpellInfo.SpellSlot.E),
            //r
            CreateCircularSkillshotDash("RyzeR", "Ryze", 1750f, 365f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Blink),
            #endregion
            //Sej E only applies to those with buff of: "sejuanifrost"
            //Sej W buff may be wrong.
            #region Sejuani
            //q
            CreateLinearDash("SejuaniArcticAssault", "Sejuani", 650f, 75f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockBack),
            //w
            CreateSelfActive("SejuaniNorthernWinds", "Sejuani", "SejuaniNorthernWindsEnrage", 300f, SpellInfo.SpellSlot.W),
            //e
            CreateSelfActive("SejuaniWintersClaw", "Sejuani", 1000f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateLinearSkillshot("SejuaniGlacialPrisonCast", "SejuaniGlacialPrison", "Sejuani", 1100f, 1600, 1600f, 1600f, 110f, true, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
            CreateLinearSkillshot("SejuaniGlacialPrisonStart", "SejuaniGlacialPrison", "Sejuani", 1100f, 1600, 1600f, 1600f, 110f, true, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
            #endregion
            #region Shaco
            //q
            CreateLinearDash("Deceive", "Shaco", 400f, 0f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None),
            //w
            //handled as trap
            //e
            CreateTargetedMissile("TwoShivPoison", "TwoShivPoison", "Shaco", 625f, 1500f, 1500f, 1500f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreatePassiveSpell("HallucinateFull", "Shaco", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            //r movement
            CreatePassiveSpell("HallucinateGuide", "Shaco", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            #endregion
            //I don't quite understand shen q
            #region Shen
            //q
            CreateLinearSkillshotNoDamage("ShenQ", "ShenQMissile", "Shen", int.MaxValue, 2000f, 2000f, 2000f, 80f, true, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateCircularSpell("ShenW", "Shen", 3.75f, int.MaxValue, 400f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.AutoAttackImmune),
            //e
            CreateLinearDash("ShenE", "Shen", 400f, 50f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Taunt),
            //r
            CreateTargetedSpell("ShenR", "Shen", int.MaxValue, 3f, SpellInfo.SpellSlot.R),
            #endregion
            #region Shyvana
            //q
            CreatePassiveSpell("ShyvanaDoubleAttack", "Shyvana", "ShyvanaDoubleAttack", SpellInfo.SpellSlot.Q),
            //q drag
            CreatePassiveSpell("ShyvanaDoubleAttackDragon", "Shyvana", "ShyvanaDoubleAttackDragon", SpellInfo.SpellSlot.Q),
            //w
            CreateSelfActive("ShyvanaImmolationAura", "Shyvana", "ShyvanaImmolationAura", 325f, SpellInfo.SpellSlot.W),
            //w drag
            CreateSelfActive("ShyvanaImmolateDragon", "Shyvana", "ShyvanaImmolateDragon", 325f, SpellInfo.SpellSlot.W),
            //e
            CreateLinearSkillshot("ShyvanaFireball", "ShyvanaFireballMissile", "Shyvana", 950f, 1575f, 1575f, 1575f, 60f, false, SpellInfo.SpellSlot.E),
            //e drag
            CreateLinearSkillshot("ShyvanaFireballDragon2", "ShyvanaFireballDragonMissile", "Shyvana", 950f, 1575f, 1575f, 1575f, 60f, false, SpellInfo.SpellSlot.E),
            //r
            CreatePassiveSpell("ShyvanaTransformCast", "Shyvana", "ShyvanaTransform", SpellInfo.SpellSlot.R),
            CreateLinearDash("ShyvanaTransformLeap", "Shyvana", 850f, 0f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack),
            #endregion
            #region Singed
            //q
            //q is handled under DrawSingedPoison and RefreshSpellList. It is treated like a particle but isnt.
            /*
            SpellName = "PoisonTrail",
                ChampionName = "Singed",
                Range = 20f,
                MissileSpeed = 347.8f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 210f,
                ConeDegrees = 45f,
            */
            //w
            CreateCircularSpell("MegaAdhesive", "Singed", 5f, 1000f, 265f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Slow),
            //e
            CreateTargetedSpell("Fling", "Singed", 125f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockUp),
            //r
            CreatePassiveSpell("InsanityPotion", "Singed", "InsanityPotion", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            #endregion
            //sion q knockup logic
            //sion r dash logic
            #region Sion
            //q
            //300/600 range
            CreateLinearSpell("SionQ", "Sion", "SionQ", 800f, 400f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp),
            //w
            CreateSelfActive("SionW", "Sion", "sionwshieldstacks", 400f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow, SpellInfo.Buff.Shield),
            //e
            CreateLinearSkillshot("SionE", "SionEMissile", "Sion", 800f, 1800f, 1800f, 1800f, 80f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            //7500 total range
            CreateLinearDash("SionR", "Sion", 800f, 100f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            // add sivir e buff name
            //sivir r buff name
            #region Sivir
            //aa
            CreateAutoAttack("SivirBasicAttack", "Sivir", 1750f, 1400f, 1400f),
            //aa
            CreateAutoAttack("SivirCritAttack", "Sivir", 1750f, 1750f, 1750f),
            //w attack bounce
            CreateAutoAttack("SivirWAttack", "Sivir", 1750f, 1750f, 1750f),
            //w attack
            CreateAutoAttack("SivirWAttackBounce", "Sivir", 1750f, 1750f, 1750f),
            //q
            CreateLinearSkillshot("SivirQ", "SivirQMissile", "Sivir", 1250f, 1350f, 1350f, 1350f, 90f, false, SpellInfo.SpellSlot.Q),
            //q
            CreateLinearSkillshot("SivirQ", "SivirQMissileReturn", "Sivir", 1250f, 1350f, 1350f, 1350f, 90f, false, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("SivirW", "Sivir", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            //e
            CreatePassiveSpell("SivirE", "Sivir", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpellShield),
            //r
            CreateSelfActiveNoDamage("SivirR", "Sivir", 1000f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            #endregion
            #region Skarner
            //q
            CreateSelfActive("SkarnerVirulentSlash", "Skarner", 350f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("SkarnerExoskeleton", "Skarner", "SkarnerExoskeleton", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //e
            CreateLinearSkillshot("SkarnerFracture", "SkarnerFractureMissile", "Skarner", 1000f, 1500f, 1500f, 1500f, 70f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            /*
            SpellName = "SkarnerFractureMissile",
                ChampionName = "Skarner",
                Range = 1000f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 70f,
                Radius = 100f,
                ConeDegrees = 45f,
                */
            //r
            CreateTargetedChannel("SkarnerImpale", "Skarner", "SkarnerImpale", 350f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Suppression),
            #endregion
            //sona w and e buff names?
            #region Sona
            //q
            CreateTargetedMissile("SonaQ", "SonaMissile", "Sona", 825f, 1300f, 1300f, 1300f, SpellInfo.SpellSlot.Q),
            //q chord
            //i think this is the empowered auto and not a spell
            CreateTargetedMissile("SonaQ", "SonaQAttackUpgrade", "Sona", 825f, 1300f, 1300f, 1300f, SpellInfo.SpellSlot.Q),
            //w
            CreateSelfActiveNoDamage("SonaW", "Sona", 1000f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //w chord
            //i think this is the empowered auto and not a spell
            CreateTargetedMissile("SonaW", "SonaWMissile", "Sona", 825f, 1300f, 1300f, 1300f, SpellInfo.SpellSlot.W),
            //e
            CreateSelfActiveNoDamage("SonaE", "Sona", 1000f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //e chord
            //i think this is the empowered auto and not a spell
            CreateTargetedMissile("SonaE", "SonaEAttackUpgrade", "Sona", 825f, 1300f, 1300f, 1300f, SpellInfo.SpellSlot.E),
            //r
            CreateLinearSkillshot("SonaR", "SonaR", "Sona", 1000f, 2400f, 2400f, 2400f, 140f, false, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
            #endregion
            #region Soraka
            //q
            CreateCircularSkillshot("SorakaQ", "SorakaQ", "Soraka", 800f, 1100f, 1100f, 1100f, 230f, true, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateTargetedSpell("SorakaW", "Soraka", 550f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //e
            CreateCircularSpell("SorakaE", "Soraka", 1.5f, 875f, 250f, true, SpellInfo.SpellSlot.E, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Silence),
            //r
            CreateTargetedSpell("SorakaR", "Soraka", int.MaxValue, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            #endregion
            //swain q trap?
            #region Swain
            //q
            CreateCircularSpell("SwainDecrepify", "Swain", 4f, 350f, true, SpellInfo.SpellSlot.Q, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Slow),
            /*
                MissileName = "SwainQTrap",
                ChampionName = "Swain",
                MissileSpeed = 1800f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 2000f,
                Width = 120f,
                */
            //w
            CreateCircularSpell("SwainShadowGrasp", "Swain", 0.875f, 900f, 240f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Stun),
            //e
            CreateTargetedMissile("SwainTorment", "SwainTorment", "Swain", 625f, 1400f, 1400f, 1400f, SpellInfo.SpellSlot.E),
            //r
            CreateToggleableSelfActive("SwainMetamorphism", "Swain", "SwainMetamorphism", 625f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None),
            CreateLinearMissile("SwainMetaNuke", "Swain", 1800f, 900f, 900f, 900f, 5f, SpellInfo.CrowdControlType.None),
            CreateLinearMissile("SwainMetaHeal", "Swain", 1800f, 900f, 900f, 900f, 5f, SpellInfo.CrowdControlType.None),
            #endregion
            #region Syndra
            //aa
            CreateAutoAttack("SyndraBasicAttack", "Syndra", 1800f, 1800f, 1800f),
            CreateAutoAttack("SyndraBasicAttack2", "Syndra", 1800f, 1800f, 1800f),
            //q
            CreateCircularSkillshot("SyndraQ", "SyndraQSpell", "Syndra", 1100f, 1600f, 1600f, 1600f, 180f, true, SpellInfo.SpellSlot.Q),
            //w
            //pickup orb
            CreateTargetedSpell("SyndraW", "Syndra", 925f, SpellInfo.SpellSlot.W),
            CreateCircularSpell("SyndraWCast", "Syndra", 950f, 210f, true, SpellInfo.SpellSlot.E, SpellInfo.Buff.None, SpellInfo.CrowdControlType.Slow),
            //e
            CreateConeSpell("SyndraE", "Syndra", 650f, 20f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            CreateLinearMissile("SyndraEMissile", "Syndra", 700f, 2500f, 2500f, 2500f, 60f),
            CreateLinearMissile("SyndraEMissile2", "Syndra", 700f, 2500f, 2500f, 2500f, 60f),
            CreateLinearMissile("SyndraEMissile3", "Syndra", 700f, 2500f, 2500f, 2500f, 60f),
            CreateLinearMissile("SyndraESphereMissile", "Syndra", 700f, 2500f, 2500f, 2500f, 60f, SpellInfo.CrowdControlType.Stun),
            //r
            CreateTargetedMissile("SyndraR", "SyndraRSpread1", "Syndra", 675f, 1100f, 1100f, 1100f, SpellInfo.SpellSlot.R),
            CreateTargetedMissile("SyndraR", "SyndraRSpread2", "Syndra", 675f, 1100f, 1100f, 1100f, SpellInfo.SpellSlot.R),
            CreateTargetedMissile("SyndraR", "SyndraRSpread3", "Syndra", 675f, 1100f, 1100f, 1100f, SpellInfo.SpellSlot.R),
            CreateTargetedMissile("SyndraR", "SyndraRSpell", "Syndra", 675f, 1100f, 1100f, 1100f, SpellInfo.SpellSlot.R),
            #endregion
            //Tahm Kench Q Stun logic. Targets with buff: tahmkenchpdevourable
            #region TahmKench
            //q
            CreateLinearSkillshot("TahmKenchQ", "TahmKenchQMissile", "TahmKench", 951f, 2800f, 2800f, 2800f, 70f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreateTargetedSpell("TahmKenchW", "TahmKench", 250f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Suppression),
            CreatePassiveSpell("TahmKenchWCastTimeAndAnimation", "TahmKench", "tahmkenchwhasdevouredtarget", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Slow),
            //e
            CreatePassiveSpell("TahmKenchE", "TahmKench", "tahmkencheshield", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //r
            CreateBlinkDash("TahmKenchNewR", "TahmKench", 4500f, SpellInfo.SpellSlot.R),
            #endregion
            #region Talon
            //q
            CreateTargetedDash("TalonQ", "Talon", 170f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.TargetedLinear),
            //forced crit attack
            CreateTargetedSpell("TalonQAttack", "Talon", 170f, SpellInfo.SpellSlot.Q),
            //w
            CreateConeSpell("TalonW", "Talon", 650f, 26f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None),
            CreateLinearMissile("TalonWMissileOne", "Talon", 1200f, 2500f, 2500f, 2500f, 75f, SpellInfo.CrowdControlType.None),
            //return
            CreateLinearMissile("TalonWMissileTwo", "Talon", int.MaxValue, 3000f, 3000f, 3000f, 75f, SpellInfo.CrowdControlType.Slow),
            //e
            CreateLinearDash("TalonE", "Talon", 6000f, 0f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None),
            CreateLinearDash("TalonE2", "Talon", 6000f, 0f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None),
            //r
            CreateCircularSpell("TalonR", "Talon", 0f, 650f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.SpeedUp),
            CreateLinearMissile("TalonRMisOne", "Talon", 650f, 2400f, 2400f, 2400f, 140f, SpellInfo.CrowdControlType.Slow),
            //return
            CreateLinearMissile("TalonRMisTwo", "Talon", int.MaxValue, 4000f, 4000f, 4000f, 140f, SpellInfo.CrowdControlType.Slow),
            CreatePassiveSpell("TalonRToggle", "Talon", SpellInfo.SpellSlot.R),
            #endregion
            //Code Taliyah
            #region Taliyah
            //r
            /*
            SpellName = "TaliyahR",
                ChampionName = "Taliyah",
                Range = 25000f,
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 120f,
                Radius = 0f,
                ConeDegrees = 45f,
                MissileName = "TaliyahRMis",
                ChampionName = "Taliyah",
                MissileSpeed = 1700f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 120f,

                SpellName = "TaliyahRBlowUp",
                ChampionName = "Taliyah",
                Range = 900f,
                MissileSpeed = 500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 2600f,
                ConeDegrees = 45f,

            MissileName = "TaliyahRBlowUpSoundMis",
                ChampionName = "Taliyah",
                MissileSpeed = 1700f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 120f,
                */
            #endregion
            //add taric q, e, r buff name, 
            #region Taric
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpell,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            CreateTargetedSpell("TaricW", "Taric", 800f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.DamageReduction),
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            CreatePassiveSpell("TaricR", "Taric", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Invulnerability),
            #endregion
            #region Teemo
            //aa
            CreateAutoAttack("ToxicShotAttack", "Teemo", 1300f, 1300f, 1300f, SpellInfo.CrowdControlType.None),
            //q
            CreateTargetedMissile("BlindingDart", "BlindingDart", "Teemo", 680f, 1500f, 1500f, 1500f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Blind),
            //w
            CreatePassiveSpell("MoveQuick", "Teemo", "teemomovequickspeed", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //e
            //a passive 
            //r
            //handled as a trap
            #endregion
            //thresh w/e/r fix
            //handle w as particle
            #region Thresh
            //q
            CreateLinearSkillshot("ThreshQ", "ThreshQMissile", "Thresh", 1100f, 1900f, 1900f, 1900f, 70f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Pull),
            CreateLinearSkillshot("ThreshQ", "ThreshQPullMissile", "Thresh", 1100f, 1900f, 1900f, 1900f, 70f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Pull),
            CreateTargetedDash("ThreshQLeap", "Thresh", 10000f, SpellInfo.SpellSlot.Q, SpellInfo.Dashtype.Targeted),
            /*
            SpellName = "ThreshQInternal",
                ChampionName = "Thresh",
                Range = 10000f,
                MissileSpeed = 1200f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 60f,
                Radius = 210f,
                ConeDegrees = 45f,
            */
            //w
            
            CreateLinearDash("LanternWAlly", "All", int.MaxValue, 1f, SpellInfo.SpellSlot.None),
            CreateCircularSkillshot("ThreshW", "ThreshWLanternOut", "Thresh", 950f, 800f, 800f, 800f, 120f, true, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None),
            CreateCircularSpell("ThreshW", "Thresh", 4f, 950f, 275f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.Shield),
            //e
            CreateWall("ThreshE", "Thresh", 0f, 210f, 110f, 0f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockBack),
            /*
            
                MissileName = "ThreshEMissile1",
                ChampionName = "Thresh",
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 1075f,
                Width = 110f,
                */
            //r
            new SpellInfo()
            {
                SpellName = "ThreshRPenta",
                ChampionName = "Thresh",
                Range = 850f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 400f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PentaWall,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //tristana e buff name
            #region Tristana
            //q
            CreatePassiveSpell("TristanaQ", "Tristana", "TristanaQ", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            //w
            CreateCircularSkillshotDash("TristanaW", "Tristana", 900f, 270f, SpellInfo.SpellSlot.W, SpellInfo.Dashtype.Linear, SpellInfo.CrowdControlType.Slow),
            //e
            //missile
            CreateLinearMissile("TristanaE", "Tristana", 550f, 2400f, 2400f, 2400f, 5f),
            CreateTargetedSpell("TristanaE", "Tristana", 550f, 210f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedMissile("TristanaR", "TristanaR", "Tristana", 550f, 2000f, 2000f, 2000f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack),
            #endregion
            //trundle e slow range
            #region Trundle
            //q
            CreatePassiveSpell("TrundleTrollSmash", "Trundle", "TrundleTrollSmash", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            CreateCircularSpell("trundledesecrate", "Trundle", 750f, 750f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.SpeedUp),
            //e
            CreateCircularSpell("TrundleCircle", "Trundle", 1000f, 125f, true, SpellInfo.SpellSlot.E, SpellInfo.Buff.None, SpellInfo.CrowdControlType.KnockUp),
            //r
            CreateTargetedSpell("TrundlePain", "Trundle", 650f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            CreateTargetedMissile("TrundlePain", "TrundlePainHealBig", "Trundle", 650f, 1000f, 1000f, 1000f, SpellInfo.SpellSlot.R),
            CreateTargetedMissile("TrundlePain", "TrundlePainHeal", "Trundle", 650f, 1000f, 1000f, 1000f, SpellInfo.SpellSlot.R),
            #endregion
            //tryndamere w slow logic
            #region Tryndamere
            //q
            CreatePassiveSpell("TryndamereQ", "Tryndamere", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //w
            CreateTargetedSpell("TryndamereW", "Tryndamere", 850f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            //e
            CreateLinearDash("TryndamereE", "Tryndamere", 660f, 160f, SpellInfo.SpellSlot.E),
            //r
            CreatePassiveSpell("UndyingRage", "Tryndamere", 5f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Invulnerability),
            #endregion
            //twisted fate r buff
            #region TwistedFate
            //q
            CreateLinearSkillshot("WildCards", "SealFateMissile", "TwistedFate", 1450f, 1000f, 1000f, 1000f, 40f, false, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("PickACard", "TwistedFate", SpellInfo.SpellSlot.W),
            CreatePassiveSpell("BlueCardPreAttack", "TwistedFate", SpellInfo.SpellSlot.W),
            CreatePassiveSpell("GoldCardPreAttack", "TwistedFate", SpellInfo.SpellSlot.W),
            CreatePassiveSpell("RedCardPreAttack", "TwistedFate", SpellInfo.SpellSlot.W),
            CreateAutoAttack("BlueCardAttack", "TwistedFate", 1500f, 1500f, 1500f, SpellInfo.CrowdControlType.None),
            CreateAutoAttack("GoldCardAttack", "TwistedFate", 1500f, 1500f, 1500f, SpellInfo.CrowdControlType.Stun),
            CreateAutoAttack("RedCardAttack", "TwistedFate", 1500f, 1500f, 1500f, SpellInfo.CrowdControlType.Slow),
            //e
            CreateAutoAttack("TwistedFateBasicAttack4", "TwistedFate", 1500f, 1500f, 1500f, SpellInfo.CrowdControlType.None),
            //r vision
            CreatePassiveSpell("Destiny", "TwistedFate", SpellInfo.SpellSlot.R),
            //r teleport
            CreateCircularSkillshotDash("Gate", "TwistedFate", 5500f, 0f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Blink),
            #endregion
            //twitch w and r buff, w length (3 seconds)
            #region Twitch
            //q
            CreatePassiveSpell("TwitchHideInShadows", "Twitch", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //w
            CreateCircularSkillshot("TwitchVenomCask", "TwitchVenomCaskMissile", "Twitch", 1100f, 1400f, 1400f, 1400f, 275f, true, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.Slow),
            //e
            CreateTargetedMissile("TwitchExpunge", "TwitchEParticle", "Twitch", 1200f, 3000f, 3000f, 3000f, SpellInfo.SpellSlot.E),
            //r
            CreatePassiveSpell("TwitchFullAutomatic", "Twitch", 0f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            #endregion
            //udyr bear stance auto?
            #region Udyr
            //q
            CreatePassiveSpell("UdyrTigerStance", "Udyr", "UdyrTigerStance", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //w
            CreatePassiveSpell("UdyrTurtleStance", "Udyr", "UdyrTurtleStance", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //e
            CreatePassiveSpell("UdyrBearStance", "Udyr", "UdyrBearStance", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //r
            CreatePassiveSpell("UdyrPheonixStance", "Udyr", "UdyrPheonixStance", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            #endregion
            //w and r buff names
            #region Urgot
            //q
            CreateLinearSkillshot("UrgotHeatseekingLineMissile", "UrgotHeatseekingLineMissile", "Urgot", 1000f, 1600f, 1600f, 1600f, 60f, false, SpellInfo.SpellSlot.Q),
            //lock on Q
            CreateTargetedMissile("UrgotHeatseekingHomeMissile", "UrgotHeatseekingHomeMissile", "Urgot", 1200f, 1800f, 1800f, 1800f, SpellInfo.SpellSlot.Q),
            //w
            CreatePassiveSpell("UrgotTerrorCapacitorActive2", "Urgot", 0f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Shield),
            //e
            CreateCircularSkillshot("UrgotPlasmaGrenade", "UrgotPlasmaGrenadeBoom", "Urgot", 1100f, 1000f, 1000f, 1000f, 250f, true, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedChannel("UrgotSwap2", "Urgot", "null", 550f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Suppression),
            #endregion
            //varus q size scaling range
            //varus r bounce?
            #region Varus
            //q
            CreateLinearSkillshot("VarusQ", "VarusQMissile", "Varus", 2000f, 1900f, 1900f, 1900f, 70f, true, SpellInfo.SpellSlot.Q),
            //w
            //passive
            //e
            CreateCircularSkillshot("VarusEMissile", "VarusEMissile", "Varus", 1100f, 1500f, 1500f, 1500f, 210f, true, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateLinearSkillshot("VarusR", "VarusRMissile", "Varus", 1250f, 1950f, 1950f, 1950f, 120f, false, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Stun),
            #endregion
            //vayne q auto, vayne 3rd proc auto, vayne q and r buff names, knockback/stun logic
            #region Vayne
            //q
            CreateLinearDash("VayneTumble", "Vayne", 300f, 0f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None),
            CreatePassiveSpell("VayneTumble", "Vayne", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //w
            //passive
            //e
            CreateTargetedMissile("VayneCondemnMissile", "VayneCondemnMissile", "Vayne", 550f, 2200f, 2200f, 2200f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockBack),
            CreateTargetedMissile("VayneCondemnMissile", "VayneCondemnMissile", "Vayne", 550f, 2200f, 2200f, 2200f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            //r
            CreatePassiveSpell("VayneInquisition", "Vayne", 0f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            #endregion
            #region Veigar
            //q
            CreateLinearSkillshot("VeigarBalefulStrike", "VeigarBalefulStrikeMis", "Veigar", 950f, 2200f, 2200f, 2200f, 70f, false, SpellInfo.SpellSlot.Q),
            //w
            CreateCircularSpell("VeigarDarkMatter", "Veigar", 900f, 225f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.None),
            //e
            CreateCircularWall("VeigarEventHorizon", "Veigar", 700f, 350f, 390f, 3.5f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            //r
            CreateTargetedMissile("VeigarR", "VeigarR", "Veigar", 650f, 500f, 500f, 500f, SpellInfo.SpellSlot.R),
            #endregion
            #region VelKoz
            //q
            CreateLinearSkillshot("VelkozQ", "VelkozQMissile", "Velkoz", 1100f, 1300f, 1300f, 1300f, 50f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //q split
            CreateLinearSkillshot("VelkozQ", "VelkozQMissileSplit", "Velkoz", 1100f, 2100f, 2100f, 2100f, 50f, false, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            //w
            //had this as 225f
            CreateLinearSkillshot("VelkozW", "VelkozWMissile", "Velkoz", 1200f, 1700f, 1700f, 1700f, 110f, false, SpellInfo.SpellSlot.W),
            //e
            CreateCircularSkillshot("VelkozE", "VelkozEMissile", "Velkoz", 1100f, 1500f, 1500f, 1500f, 200f, true, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.KnockUp),
            //r
            CreateLinearSpell("VelkozR", "Velkoz", "VelkozR", 1550f, 100f, SpellInfo.SpellSlot.R),
            #endregion
            //fix Vi Q and E and R
            #region Vi
            //q
            //q needs scaling range
            //vi gets these buffs when dashing:
            //viqlaunchsound
            //ViQLaunch
            //ViQ
            //on buff lost ViQ do drawing
            CreatePassiveSpell("ViQ", "Vi", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Slow),
            /*new SpellInfo()
            {SpellName = "ViQ",
                ChampionName = "Vi",
                Range = 250f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 55f,
                Radius = 300f,
                ConeDegrees = 45f,
                
                MissileName = "ViQMissile",
                ChampionName = "Vi",
                MissileSpeed = 1500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 2000f,
                Width = 90f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.Q,
            },*/
            //w
            //passive
            //e
            CreatePassiveSpell("ViE", "Vi", "ViE", SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            /*
                            MissileName = "ViEFx",
                ChampionName = "Vi",
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 800f,
                Width = 20f,
                */
            //r
            CreateTargetedDash("ViR", "Vi", 800f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Targeted, SpellInfo.CrowdControlType.KnockUp),
            //"ViR" is buff name
            /*new SpellInfo()
            {
                SpellName = "ViR",
                ChampionName = "Vi",
                Range = 800f,
                MissileSpeed = 1400f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 100f,
                ConeDegrees = 45f,
                MissileName = "ViRMissile",
                ChampionName = "Vi",
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 2000f,
                Width = 85f,
                SpellType = SpellInfo.SpellTypeInfo.TargetedMissile,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },*/
            #endregion
            //Do Viktor
            #region Viktor
            /*
                MissileName = "ViktorBasicAttack2",
                ChampionName = "Viktor",
                MissileSpeed = 2300f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 625f,
                MissileName = "ViktorBasicAttack",
                ChampionName = "Viktor",
                MissileSpeed = 2300f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 625f,
            */
            //q
            /*
            SpellName = "ViktorPowerTransfer",
                ChampionName = "Viktor",
                Range = 600f,
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 0f,
                ConeDegrees = 45f,

                MissileName = "ViktorPowerTransfer",
                ChampionName = "Viktor",
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 600f,
                Width = 0f,

ViktorPowerTransfer
                MissileName = "ViktorPowerTransferReturn",
                ChampionName = "Viktor",
                MissileSpeed = 2000f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 1500f,
                Width = 0f,
            */
            //w
            /*
            SpellName = "ViktorGravitonField",
                ChampionName = "Viktor",
                Range = 700f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 300f,
                ConeDegrees = 45f,

                */
            //e
            /*
            SpellName = "ViktorDeathRay",
                ChampionName = "Viktor",
                Range = 550f,
                MissileSpeed = 1050f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 90f,
                Radius = 300f,
                ConeDegrees = 45f,

                MissileName = "ViktorDeathRayMissile",
                ChampionName = "Viktor",
                MissileSpeed = 1050f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 80f,

            MissileName = "ViktorEAugMissile",
                ChampionName = "Viktor",
                MissileSpeed = 1050f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 80f,

                MissileName = "ViktorEAugParticle",
                ChampionName = "Viktor",
                MissileSpeed = 1500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 80f,

                MissileName = "ViktorDeathRayMissile2",
                ChampionName = "Viktor",
                MissileSpeed = 1500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Range = 25000f,
                Width = 80f,
            */
            //r
            /*
            SpellName = "ViktorChaosStorm",
                ChampionName = "Viktor",
                Range = 700f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 300f,
                ConeDegrees = 45f,
            */
            #endregion
            //add vlad w. Bot vlad doesn't use it
            #region Vladimir
            //q
            CreateTargetedMissile("VladimirQ", "VladimirTransfusionHeal", "Vladimir", 600f, 1400f, 1400f, 1400f, SpellInfo.SpellSlot.Q),
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            CreateSelfActive("VladimirE", "Vladimir", "VladimirE", 550f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Slow),
            CreateLinearMissile("VladimirEMissile", "Vladimir", 550f, 4000f, 4000f, 4000f, 60f, SpellInfo.CrowdControlType.Slow),
            //r
            CreateCircularSpell("VladimirHemoplague", "Vladimir", 0f, 700f, 375f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.Heal, SpellInfo.CrowdControlType.None),
            CreateTargetedMissile("VladimirHemoplague", "VladimirRHealMissile", "Vladimir", int.MaxValue, 1500f, 1500f, 1500f, SpellInfo.SpellSlot.R),
            #endregion
            #region Volibear
            //q
            CreatePassiveSpell("VolibearQ", "Volibear", "VolibearQ", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp, SpellInfo.Buff.SpeedUp),
            //w
            CreateTargetedSpell("VolibearW", "Volibear", 400f, SpellInfo.SpellSlot.W),
            //e
            CreateSelfActive("VolibearE", "Volibear", 425f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreatePassiveSpell("VolibearR", "Volibear", "VolibearR", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            #endregion
            //ww w and r buff names
            #region Warwick
            //q
            CreateTargetedSpell("HungeringStrike", "Warwick", 400f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Heal),
            //w
            CreateSelfActiveNoDamage("HuntersCall", "Warwick", 1200f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            //e
            CreateSelfActiveNoDamage("BloodScent", "Warwick", 1600f, SpellInfo.SpellSlot.E),
            //r
            CreateTargetedDash("InfiniteDuress", "Warwick", 700f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Blink, SpellInfo.CrowdControlType.Suppression),
            //r channel
            CreateTargetedChannel("InfiniteDuressChannel", "Warwick", "null", 700f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.Suppression),
            #endregion
            //get ult buff name
            #region Wukong
            //q
            CreatePassiveSpell("MonkeyKingDoubleAttack", "MonkeyKing", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //w
            CreatePassiveSpell("MonkeyKingDecoy", "MonkeyKing", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            //e
            CreateTargetedDash("MonkeyKingNimbus", "MonkeyKing", 650f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Linear),
            //r
            CreateSelfActive("MonkeyKingSpinToWin", "MonkeyKing", 163f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            //xerath q range extension. Starts at 750 range
            #region Xerath
            //q
            CreatePassiveSpell("XerathArcanopulseChargeUp", "Xerath", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Slow),
            CreateLinearSpell("XerathArcanopulse2", "Xerath", 0f, 750f, 100f, SpellInfo.SpellSlot.Q),
            //w
            CreateCircularSpell("XerathArcaneBarrage2", "Xerath", 1000f, 100f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.Slow),
            //e
            CreateLinearSkillshot("XerathMageSpearMissile", "XerathMageSpearMissile", "Xerath", 1000f, 1400f, 1400f, 1400f, 60f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Stun),
            //r
            CreatePassiveSpell("XerathLocusOfPower2", "Xerath", "XerathLocusOfPower2", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.None),
            CreateCircularSkillshot("XerathLocusPulse", "XerathLocusPulse", "Xerath", 3200f, 500f, 500f, 500f, 200f, true, SpellInfo.SpellSlot.R),
            /* had this as well
            SpellName = "XerathRMissileWrapper",
                ChampionName = "Xerath",
                Range = 25000f,
                MissileSpeed = 500f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 0f,
                ConeDegrees = 45f,
            */
            #endregion
            //xin q knockup logic. happens on 3rd stack of q buff
            //xin r knockback logic
            #region XinZhao
            //q
            CreatePassiveSpell("XenZhaoComboTarget", "XinZhao", "XenZhaoComboTarget", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.KnockUp, SpellInfo.Buff.AttackDamageIncrease),
            //w
            CreatePassiveSpell("XenZhaoBattleCry", "XinZhao", "XenZhaoBattleCry", SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackSpeedIncrease),
            //e
            CreateTargetedDash("XenZhaoSweep", "XinZhao", 600f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Linear, SpellInfo.CrowdControlType.Slow),
            //r
            CreateSelfActive("XenZhaoParry", "XinZhao", 188f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockBack),
            #endregion
            //On Q cast, if Yasuo casted E is dashing (Yasuo.DummySpell) Q has a circular radius of 3.75f
            //try checking if yasuo gains a buff when dashing. If so when the buff is active and he q create EQ. (buff: YasuoDashWrapper) is for enemies
            // Need to completely fix yasuo dash
            #region Yasuo
            /*//q
            new SpellInfo()
            {
                SpellName = "YasuoQW",
                ChampionName = "Yasuo",
                Range = 520f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 55f,
                Delay = 5f,
                //OtherMissileNames = new string[] { "YasuoQ2", "YasuoQamps ", "YasuoQ2W", },
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
                SpellType = SpellInfo.SpellTypeInfo.TargetedMissile,
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
                SpellType = SpellInfo.SpellTypeInfo.TargetedMissile,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpell,
                CCtype = SpellInfo.CrowdControlType.Suspension,
                Slot = SpellInfo.SpellSlot.R,
            },*/
            #endregion
            //Yorick W should be a particle
            #region Yorick
            //q
            CreatePassiveSpell("YorickQ", "Yorick", "yorickqbuff", SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None, SpellInfo.Buff.AttackDamageIncrease),
            //w
            CreateCircularWall("YorickW", "Yorick", 600f, 225f, 250f, 0f, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.None),
            //e
            CreateLinearSkillshot("YorickE", "YorickEMissile", "Yorick", 1200f, 1800f, 1800f, 1800f, 80f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreatePassiveSpell("YorickR", "Yorick", SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.Pet),
            #endregion
            //fix zac Q
            //zac r knockup logic
            #region Zac
            //q
            CreateLinearSpell("ZacQ", "Zac", 0f, 50f, 120f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Slow),
            /*
            new SpellInfo()
            {
                SpellName = "ZacQ",
                ChampionName = "Zac",
                Range = 2500f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 120f,
                Radius = 0f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },*/
            //w
            CreateCircularSpell("ZacW", "Zac", 0f, 350f, false, SpellInfo.SpellSlot.W, SpellInfo.Buff.None),
            //e
            CreateCircularSkillshotDash("ZacE", "Zac", 300f, 250f, SpellInfo.SpellSlot.E, SpellInfo.Dashtype.Linear, SpellInfo.CrowdControlType.KnockUp),
            //r
            CreateSelfActive("ZacR", "Zac", "ZacR", 210f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.KnockUp),
            #endregion
            //zeds spells will originate from shadows
            #region Zed
            //q
            CreateLinearSkillshot("ZedQ", "ZedQMissile", "Zed", 925f, 1700f, 1700f, 1700f, 50f, false, SpellInfo.SpellSlot.Q),
            //w
            CreateLinearSkillshotNoDamage("ZedW", "ZedWMissile", "Zed", 400f, 1750f, 1750f, 1750f, 60f, false, SpellInfo.SpellSlot.W),
            //w second cast to blink
            CreateBlinkDash("ZedW2", "Zed", 1300f, SpellInfo.SpellSlot.W),
            //e
            CreateSelfActive("ZedE", "Zed", 290f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateTargetedDash("ZedR", "Zed", 625f, SpellInfo.SpellSlot.R, SpellInfo.Dashtype.Blink, SpellInfo.CrowdControlType.None),
            //r second cast to blink
            CreateBlinkDash("ZedW2", "Zed", 1300f, SpellInfo.SpellSlot.R),
            #endregion
            #region Ziggs
            //q
            CreateCircularSkillshot("ZiggsQ", "ZiggsQSpell", "Ziggs", 1625f, 1750f, 1750f, 1750f, 1750f, 125f, true, SpellInfo.SpellSlot.Q),
            CreateCircularSkillshot("ZiggsQ", "ZiggsQSpell2", "Ziggs", 1625f, 1750f, 1750f, 1750f, 1750f, 125f, true, SpellInfo.SpellSlot.Q),
            CreateCircularSkillshot("ZiggsQ", "ZiggsQSpell3", "Ziggs", 1625f, 1750f, 1750f, 1750f, 1750f, 125f, true, SpellInfo.SpellSlot.Q),
            //w
            CreateCircularSkillshot("ZiggsW", "ZiggsW", "Ziggs", 1000f, 1750f, 750f, 1750f, 275f, true, SpellInfo.SpellSlot.W, SpellInfo.CrowdControlType.KnockBack),
            //e
            //handled as particle
            /*
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
            },*/
            //r most damage
            CreateCircularSkillshot("ZiggsR", "ZiggsRBoom", "Ziggs", 5000f, 1750f, 1750f, 1750f, 250f, true, SpellInfo.SpellSlot.R),
            //r lesser damage
            CreateCircularSkillshot("ZiggsR", "ZiggsRBoom", "Ziggs", 5000f, 1750f, 1750f, 1750f, 500f, true, SpellInfo.SpellSlot.R),
            #endregion
            //zilean q buff, e buff, r buff
            #region Zilean
            //q
            CreateCircularSkillshot("ZileanQ", "ZileanQMissile", "Zilean", 900f, 2000f, 2000f, 2000f, 150f, true, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.Stun),
            //w
            CreatePassiveSpell("ZileanW", "Zilean", SpellInfo.SpellSlot.W),
            //e speed up
            CreateTargetedSpell("TimeWarp", "Zilean", 550f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.None, SpellInfo.Buff.SpeedUp),
            //e slow down
            CreateTargetedSpell("Rewind", "Zilean", 550f, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Slow),
            //r
            CreateTargetedSpell("ChronoShift", "Zilean", 900f, SpellInfo.SpellSlot.R, SpellInfo.CrowdControlType.None, SpellInfo.Buff.GuardianAngel),
            #endregion
            //add zyra plants
            //zyra q width/length
            #region Zyra
            //q
            CreateWall("ZyraQ", "Zyra", 800f, 85f, 140f, 0.625f, SpellInfo.SpellSlot.Q, SpellInfo.CrowdControlType.None),
            //w
            CreateCircularSpell("ZyraW", "Zyra", 850f, 45f, true, SpellInfo.SpellSlot.W, SpellInfo.Buff.None),
            //e
            CreateLinearSkillshot("ZyraE", "ZyraE", "Zyra", 1150f, 1150f, 1150f, 1150f, 70f, false, SpellInfo.SpellSlot.E, SpellInfo.CrowdControlType.Root),
            //r
            CreateCircularSpell("ZyraR", "Zyra", 2f, 700f, 500f, true, SpellInfo.SpellSlot.R, SpellInfo.Buff.None, SpellInfo.CrowdControlType.KnockUp),
            #endregion
        };
        #endregion

        #region Spell/Missile Creation
        public static SpellInfo CreateArcSkillshot(string spellName, string missileName, string championName,
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
                Delay = 2f,
                Width = width * 2,
                SpellType = SpellInfo.SpellTypeInfo.ArcSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateLinearSkillshot(string spellName, string missileName, string championName,
            float range, float missileSpeed, float missileMinSpeed, float missileMaxSpeed, float width, bool canVaryInLength, SpellInfo.SpellSlot slot,
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
                Width = width * 2,
                CanVaryInLength = canVaryInLength,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateLinearSkillshotNoDamage(string spellName, string missileName, string championName,
            float range, float missileSpeed, float missileMinSpeed, float missileMaxSpeed, float width, bool canVaryInLength, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None, SpellInfo.Buff buffType = SpellInfo.Buff.None)
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
                Width = width * 2,
                CanVaryInLength = canVaryInLength,
                BuffType = buffType,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshotNoDamage,
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
                Width = width * 2,
                SpellType = SpellInfo.SpellTypeInfo.LinearMissile,
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
                Width = width * 2,
                SpellType = SpellInfo.SpellTypeInfo.LinearSpellWithDuration,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateLinearSpell(string spellName, string championName, string buffName, float range,
            float width, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                BuffName = buffName,
                Width = width * 2,
                SpellType = SpellInfo.SpellTypeInfo.LinearSpellWithBuff,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateLinearDash(string spellName, string championName, float range, float width, 
            SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None, SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                Width = width * 2,
                SpellType = SpellInfo.SpellTypeInfo.LinearDash,
                DashType = SpellInfo.Dashtype.Linear,
                BuffType = buffType,
                CCtype = ccType,
                Slot = slot,
            };
        }
        public static SpellInfo CreateLinearDash(string spellName, string championName, float range, float delay, float width,
            SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                Width = width * 2,
                Delay = delay,
                SpellType = SpellInfo.SpellTypeInfo.LinearDash,
                DashType = SpellInfo.Dashtype.Linear,
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
                SpellType = SpellInfo.SpellTypeInfo.TargetedMissile,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedPassiveSpell(string spellName, string championName, string buffName,
            float range, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None,
            SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                BuffName = buffName,
                Range = range,
                SpellType = SpellInfo.SpellTypeInfo.TargetedPassiveSpell,
                BuffType = buffType,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedSpell(string spellName, string championName,
            float range, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None,
            SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                SpellType = SpellInfo.SpellTypeInfo.TargetedSpell,
                BuffType = buffType,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedSpell(string spellName, string championName,
            float range, float extraDelay, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                Delay = 0.25f + extraDelay,
                SpellType = SpellInfo.SpellTypeInfo.TargetedSpellWithDuration,
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
                SpellType = SpellInfo.SpellTypeInfo.TargetedDash,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedDash(string spellName, string championName, float range, float delay,
            SpellInfo.SpellSlot slot, SpellInfo.Dashtype dashType, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                DashType = dashType,
                Delay = delay,
                SpellType = SpellInfo.SpellTypeInfo.TargetedDash,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSpellWithDuration,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSpell,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSpellWithBuff,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateCircularSkillshotDash(string spellName, string championName,
            float range, float impactRadius, SpellInfo.SpellSlot slot, SpellInfo.Dashtype dashType,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                CanVaryInLength = true,
                Radius = impactRadius,
                DashType = dashType,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshotDash,
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
                SpellType = SpellInfo.SpellTypeInfo.BlinkDash,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpell,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithDuration,
                Delay = extraDelay + 0.25f,
                BuffType = buffType,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreatePassiveSpell(string spellName, string championName, string buffName, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None, SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithBuff,
                BuffName = buffName,
                BuffType = buffType,
                CCtype = ccType,
                Slot = slot,
            };
        }

        public static SpellInfo CreatePassiveSpell(string spellName, string championName, string buffName, float delay, SpellInfo.SpellSlot slot,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None, SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                SpellType = SpellInfo.SpellTypeInfo.PassiveSpellWithBuff,
                Delay = delay,
                BuffName = buffName,
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
                SpellType = SpellInfo.SpellTypeInfo.ConeSpell,
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
                SpellType = SpellInfo.SpellTypeInfo.ConeSpellWithBuff,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActiveWithBuff,
                Radius = radius,
                CCtype = ccType,
                BuffType = buffType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateTargetedActive(string spellName, string championName, string buffName, float range,
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
                BuffName = buffName,
                BuffType = buffType,
                Slot = slot,
            };
        }

        public static SpellInfo CreateWall(string spellName, string championName, float range, float width, float length, float duration,
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

        public static SpellInfo CreateCircularWall(string spellName, string championName, float range, float innerRadius, float outerRadius,
            float duration, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                Range = range,
                Radius = innerRadius,
                SecondRadius = outerRadius,
                Delay = 0.25f + duration,
                SpellType = SpellInfo.SpellTypeInfo.CircularWall,
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
                SpellType = SpellInfo.SpellTypeInfo.AutoAttack,
                Slot = SpellInfo.SpellSlot.Auto,
            };
        }

        public static SpellInfo CreateAutoAttackWithSplashDamage(string missileName, string championName,
            float missileSpeed, float missileMinSpeed, float missileMaxSpeed, float radius,
            SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None)
        {
            return new SpellInfo()
            {
                ChampionName = championName,
                MissileName = missileName,
                MissileSpeed = missileSpeed,
                MissileMinSpeed = missileMinSpeed,
                MissileMaxSpeed = missileMaxSpeed,
                Radius = radius,
                CCtype = ccType,
                SpellType = SpellInfo.SpellTypeInfo.TargetedMissile,
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
        public static SpellInfo CreateSelfActiveNoDamage(string spellName, string championName, string buffName,
            float radius, SpellInfo.SpellSlot slot, SpellInfo.CrowdControlType ccType = SpellInfo.CrowdControlType.None,
            SpellInfo.Buff buffType = SpellInfo.Buff.None)
        {
            return new SpellInfo()
            {
                SpellName = spellName,
                ChampionName = championName,
                BuffName = buffName,
                SpellType = SpellInfo.SpellTypeInfo.SelfActiveNoDamageWithBuff,
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
        #endregion

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
        public static List<SpellInfo> GetSpells(string spellName)
        {
            return SpellList.Where(a => a.SpellName == spellName).ToList();
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
                //this check is for one for all
                if (!ChampionNames.Contains(hero.ChampionName))
                    ChampionNames.Add(hero.ChampionName);

            foreach (SpellInfo spell in SpellInfoList.Where(a=>ChampionNames.Contains(a.ChampionName) || a.ChampionName == "All"))
                SpellList.Add(spell);
        }
    }
}
