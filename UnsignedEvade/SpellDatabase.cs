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
                ChampionName = "Anivia",
                MissileSpeed = 4000f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 4000f,
                Range = 1000f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
            },
            #endregion
            #region Aatrox
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
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
            #region Akali
            //q
            new SpellInfo()
            {
                SpellName = "AkaliMota",
                ChampionName = "Akali",
                Range = 600f,
                MissileSpeed = 1000f,
                MissileMinSpeed = 1000f,
                MissileMaxSpeed = 1000f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "AkaliSmokeBomb",
                ChampionName = "Akali",
                Range = 700f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 390f,
                ConeDegrees = 45f,
                Delay = 8.25f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "AkaliShadowSwipe",
                ChampionName = "Akali",
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Radius = 325f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "AkaliShadowDance",
                ChampionName = "Akali",
                Range = 400f,
                MissileSpeed = 2200f,
                MissileMinSpeed = 2200f,
                MissileMaxSpeed = 2200f,
                Radius = 300f,
                ConeDegrees = 45f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Alistar
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
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
            #region Amumu
            //q
            new SpellInfo()
            {
                SpellName = "BandageToss",
                MissileName = "SadMummyBandageToss",
                ChampionName = "Amumu",
                Range = 1100f,
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Width = 80f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "AuraofDespair",
                BuffName = "AuraofDespair",
                ChampionName = "Amumu",
                Range = 300f,
                Radius = 300f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "Tantrum",
                ChampionName = "Amumu",
                Range = 350f,
                Radius = 350f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "CurseoftheSadMummy",
                ChampionName = "Amumu",
                Range = 550f,
                Radius = 550f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Entangle,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Anivia
            //q
            new SpellInfo()
            {
                SpellName = "FlashFrost",
                ChampionName = "Anivia",
                MissileName = "FlashFrostSpell",
                MissileSpeed = 850f,
                MissileMinSpeed = 850f,
                MissileMaxSpeed = 850f,
                Range = 1100f,
                Width = 110f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w Handled Under Particle Database
            new SpellInfo()
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
            },
            //e
            new SpellInfo()
            {
                SpellName = "Frostbite",
                MissileName = "Frostbite",
                ChampionName = "Anivia",
                Range = 600f,
                MissileSpeed = 1600f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r Handled under Particle Darabase
            new SpellInfo()
            {
                //SpellName = "GlacialStorm",
                ChampionName = "Anivia",
                Range = 750f,
                MissileSpeed = 20f,
                MissileMinSpeed = 20f,
                MissileMaxSpeed = 20f,
                Width = 150f,
                Radius = 200f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Annie
            //aa
            new SpellInfo()
            {
                MissileName = "AnnieBasicAttack",
                ChampionName = "Annie",
                MissileSpeed = 1200f,
                MissileMinSpeed = 1200f,
                MissileMaxSpeed = 1200f,
                Range = 665f,
                OtherMissileNames = new string[] { "AnnieBasicAttack2" },
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //q
            new SpellInfo()
            {
                SpellName = "Disintegrate",
                MissileName = "Disintegrate",
                ChampionName = "Annie",
                MissileSpeed = 1400f,
                MissileMinSpeed = 1400f,
                MissileMaxSpeed = 1400f,
                Range = 625f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "Incinerate",
                ChampionName = "Annie",
                Range = 600f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                ConeDegrees = 24.76f,
                Width = 0f,
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
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
                SpellName = "InfernalGuardian",
                ChampionName = "Annie",
                Range = 600f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Radius = 250.3f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add ashe basic attack having a slow
            #region Ashe
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
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
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
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
            #region Azir
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
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
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.MovingWall,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Bard
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
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
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Statis,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Blitzcrank
            //q
            new SpellInfo()
            {
                SpellName = "RocketGrab",
                ChampionName = "Blitzcrank",
                MissileName = "RocketGrabMissile",
                MissileSpeed = 1800f,
                MissileMinSpeed = 1800f,
                MissileMaxSpeed = 1800f,
                Range = 1050f,
                Width = 70f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Pull,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "Overdrive",
                ChampionName = "Blitzcrank",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "PowerFist",
                ChampionName = "Blitzcrank",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "StaticField",
                ChampionName = "Blitzcrank",
                Range = 600f,
                MissileSpeed = 347.8f,
                MissileMinSpeed = 347.8f,
                MissileMaxSpeed = 347.8f,
                Width = 0f,
                Radius = 600f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Silence,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add brand passive
            #region Brand
            //q
            new SpellInfo()
            {
                SpellName = "BrandQ",
                MissileName = "BrandQMissile",
                ChampionName = "Brand",
                MissileSpeed = 1600f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Range = 1100f,
                Width = 60f,
                CollisionCount = 1,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "BrandW",
                ChampionName = "Brand",
                Range = 900f,
                Width = 0f,
                Radius = 240f,
                Delay = 0.875f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "BrandE",
                ChampionName = "Brand",
                Range = 675f,
                MissileSpeed = 1800f,
                MissileMinSpeed = 1400f,
                MissileMaxSpeed = 1400f,
                Width = 0f,
                Radius = 710f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "BrandR",
                MissileName = "BrandRMissile",
                ChampionName = "Brand",
                Range = 750f,
                MissileSpeed = 750f,
                MissileMinSpeed = 250f,
                MissileMaxSpeed = 3000f,
                Width = 0f,
                Radius = 0f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
                OtherMissileNames = new string[] { "BrandR" },
            },
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
            #region Caitlyn
            //aa
            new SpellInfo()
            {
                MissileName = "CaitlynBasicAttack",
                ChampionName = "Caitlyn",
                Range = 750f,
                MissileSpeed = 2500f,
                MissileMinSpeed = 2500f,
                MissileMaxSpeed = 2500f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //headshot
            new SpellInfo()
            {
                MissileName = "CaitlynHeadshotMissile",
                ChampionName = "Caitlyn",
                MissileSpeed = 3000f,
                MissileMinSpeed = 2500f,
                MissileMaxSpeed = 2500f,
                Range = 750f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
            },
            //q
            new SpellInfo()
            {
                SpellName = "CaitlynPiltoverPeacemaker",
                MissileName = "CaitlynPiltoverPeacemaker",
                ChampionName = "Caitlyn",
                Range = 1300f,
                Width = 60f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q2
            new SpellInfo()
            {
                SpellName = "CaitlynPiltoverPeacemaker",
                MissileName = "CaitlynPiltoverPeacemaker2",
                ChampionName = "Caitlyn",
                Range = 1300f,
                Width = 90f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
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
            },
            //e
            new SpellInfo()
            {
                SpellName = "CaitlynEntrapment",
                MissileName = "CaitlynEntrapmentMissile",
                ChampionName = "Caitlyn",
                Range = 800f,
                MissileSpeed = 1600f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Width = 70f,
                Radius = 20f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "CaitlynAceintheHole",
                MissileName = "CaitlynAceintheHoleMissile",
                ChampionName = "Caitlyn",
                Range = 2000f,
                MissileSpeed = 3200f,
                MissileMinSpeed = 2200f,
                MissileMaxSpeed = 2200f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
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
            #region ChoGath
            //q
            new SpellInfo()
            {
                SpellName = "Rupture",
                ChampionName = "Chogath",
                Range = 950f,
                Radius = 250f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "FeralScream",
                ChampionName = "Chogath",
                Range = 300f,
                ConeDegrees = 28f,
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.Silence,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                MissileName = "VorpalSpikesMissle",
                ChampionName = "Chogath",
                MissileSpeed = 1475f,
                MissileMinSpeed = 1475f,
                MissileMaxSpeed = 1475f,
                Range = 650f,
                Width = 170f,//190. scales with size
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
                OtherMissileNames = new string[] { "VorpalSpikesMissle2", "VorpalSpikesMissle3","VorpalSpikesMissle4","VorpalSpikesMissle5","VorpalSpikesMissle6","VorpalSpikesMissle7","VorpalSpikesMissle8",},
            },
            //r
            new SpellInfo()
            {
                SpellName = "Feast",
                ChampionName = "Chogath",
                Range = 175f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Corki
            //aa
            new SpellInfo()
            {
                MissileName = "CorkiBasicAttack",
                ChampionName = "Corki",
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Range = 650f,
                SpellType = SpellInfo.SpellTypeInfo.None,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Auto,
                OtherMissileNames = new string[] { "CorkiBasicAttack2" },
            },
            //q
            new SpellInfo()
            {
                SpellName = "PhosphorusBomb",
                MissileName = "PhosphorusBombMissile",
                ChampionName = "Corki",
                Range = 825f,
                MissileSpeed = 1000f,
                MissileMinSpeed = 1000f,
                MissileMaxSpeed = 1000f,
                Radius = 250f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
                OtherMissileNames = new string[] { "PhosphorusBombMissileMin" },
            },
            //w
            new SpellInfo()
            {
                SpellName = "CarpetBomb",
                ChampionName = "Corki",
                Range = 600f,
                MissileSpeed = 700f,
                MissileMinSpeed = 700f,
                MissileMaxSpeed = 700f,
                Width = 200f,
                Radius = 100f,
                ConeDegrees = 45f,
                canVaryInLength = true,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            
            //w2
            new SpellInfo()
            {
                SpellName = "CarpetBombMega",
                ChampionName = "Corki",
                Range = 1800f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Width = 200f,
                canVaryInLength = true,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //w
            new SpellInfo()
            {
                SpellName = "CarpetBomb2",
                ChampionName = "Corki",
                Range = 600f,
                MissileSpeed = 700f,
                MissileMinSpeed = 700f,
                MissileMaxSpeed = 700f,
                Width = 200f,
                Radius = 100f,
                ConeDegrees = 45f,
                //2 seconds for fire to stay
                Delay = 2.25f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            
            //w2
            new SpellInfo()
            {
                SpellName = "CarpetBombMega2",
                ChampionName = "Corki",
                Range = 1800f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Width = 200f,
                //5 seconds for fire to stay
                Delay = 5.25f,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "GGun",
                ChampionName = "Corki",
                BuffName = "GGun",
                Range = 600f,
                MissileSpeed = 902f,
                MissileMinSpeed = 902f,
                MissileMaxSpeed = 902f,
                Width = 0f,
                Radius = 100f,
                ConeDegrees = 28f,
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "MissileBarrageMissile",
                MissileName = "MissileBarrageMissile",
                ChampionName = "Corki",
                Range = 1300f,
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Width = 40f,
                Radius = 299.3f,
                ConeDegrees = 45f,
                CollisionCount = 1,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
                OtherMissileNames = new string[] { "MissileBarrage" }
            },
            //r2
            new SpellInfo()
            {
                SpellName = "MissileBarrageMissile2",
                MissileName = "MissileBarrageMissile2",
                ChampionName = "Corki",
                Range = 1500f,
                MissileSpeed = 2000f,
                MissileMinSpeed = 2000f,
                MissileMaxSpeed = 2000f,
                Width = 40f,
                CollisionCount = 1,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },

            #endregion
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
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
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
            #region Draven
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion  
            #region Ekko
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Ezreal
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Fiddlesticks
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Fear,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Silence,
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
            #region Galio
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Taunt,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Gangplank
            //q
            new SpellInfo()
            {
                SpellName = "GangplankQWrapper",
                MissileName = "GangplankQProceed",
                ChampionName = "Gangplank",
                Range = 625f,
                MissileSpeed = 2600f,
                MissileMinSpeed = 2600f,
                MissileMaxSpeed = 2600f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
                OtherMissileNames = new string[] { "GangplankQProceed" },
            },
            //w
            new SpellInfo()
            {
                SpellName = "GangplankW",
                ChampionName = "Gangplank",
                Range = 400f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                Width = 0f,
                Radius = 0f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
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
            new SpellInfo()
            {
                SpellName = "GangplankR",
                ChampionName = "Gangplank",
                Range = 30000f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                Width = 0f,
                Radius = 525f,
                ConeDegrees = 45f,
                Delay = 7.25f,
                TravelTime = 0,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Garen
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Silence,
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
            #region Gnar
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q form
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
            //w form
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e form
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
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
            #region Gragas
            //q
            new SpellInfo()
            {
                SpellName = "GragasQ",
                MissileName = "GragasQMissile",
                ChampionName = "Gragas",
                Range = 1100f,
                MissileSpeed = 800f,
                MissileMinSpeed = 800f,
                MissileMaxSpeed = 800f,
                Width = 110f,
                Radius = 250f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "GragasWAttack",
                ChampionName = "Gragas",
                Range = 400f,
                MissileSpeed = 828.5f,
                MissileMinSpeed = 828.5f,
                MissileMaxSpeed = 828.5f,
                Width = 0f,
                Radius = 210f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "GragasE",
                ChampionName = "Gragas",
                Range = 600f,
                MissileSpeed = 900f,
                MissileMinSpeed = 900f,
                MissileMaxSpeed = 900f,
                Width = 200f,
                Radius = 200f,
                CollisionCount = 1,
                DashType = SpellInfo.Dashtype.Linear,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "GragasR",
                ChampionName = "Gragas",
                MissileName = "GragasRBoom",
                TravelTime = 0.55f,
                MissileSpeed = 1800f,
                MissileMinSpeed = 1800f,
                MissileMaxSpeed = 1800f,
                Range = 1050f,
                Width = 120f,
                Radius = 350f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
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
            //fix illaoi tentacles when .Direction is Fixed
            #region Illaoi
            //q
            new SpellInfo()
            {
                SpellName = "IllaoiQ",
                ChampionName = "Illaoi",
                Range = 800f,
                MissileSpeed = 500f,
                MissileMinSpeed = 500f,
                MissileMaxSpeed = 500f,
                Delay = 0.75f,
                Width = 250f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "IllaoiW",
                ChampionName = "Illaoi",
                Range = 350f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 350f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "IllaoiE",
                ChampionName = "Illaoi",
                MissileName = "IllaoiEMis",
                MissileSpeed = 1900f,
                MissileMinSpeed = 1900f,
                MissileMaxSpeed = 1900f,
                Range = 950f,
                Width = 50f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "IllaoiR",
                ChampionName = "Illaoi",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Irelia
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.Nearsight,
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
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Ivern
            //q
            new SpellInfo()
            {
                SpellName = "IvernQ",
                MissileName = "IvernQ",
                ChampionName = "Ivern",
                MissileSpeed = 1300f,
                MissileMinSpeed = 1300f,
                MissileMaxSpeed = 1300f,
                Range = 1100f,
                Width = 65f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Root,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellName = "IvernW",
                ChampionName = "Ivern",
                Range = 1600f,
                MissileSpeed = 1600f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
                Radius = 100f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "IvernE",
                ChampionName = "Ivern",
                Range = 750f,
                Delay = 2.25f,
                Radius = 525f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellName = "IvernR",
                ChampionName = "Ivern",
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
                OtherMissileNames = new string[] { "IvernRRecast" },
            },
            //daisy
            new SpellInfo()
            {
                MissileName = "IvernRMissile",
                ChampionName = "Ivern",
                MissileSpeed = 1400f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
                Range = 800f,
                Width = 100f,
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
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
            #region JarvanIV
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Jax
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
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
                CCtype = SpellInfo.CrowdControlType.Slow,
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
            //q
            new SpellInfo()
            {
                SpellName = "KarthusLayWasteA3",
                ChampionName = "Karthus",
                MissileSpeed = 10000f,
                Range = 875f,
                Radius = 160f,
                Delay = 0.75f,
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
                OtherMissileNames = new string[] { "KarthusLayWasteA1", "KarthusLayWasteA2",},
            },
            //w
            new SpellInfo()
            {
                SpellName = "KarthusWallOfPain",
                ChampionName = "Karthus",
                Range = 1000f,
                MissileSpeed = 1600f,
                MissileMinSpeed = 1600f,
                MissileMaxSpeed = 1600f,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
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
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
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
                Range = 750f,
                MissileSpeed = 1800f,
                MissileMinSpeed = 1400f,
                MissileMaxSpeed = 1400f,
                Width = 0f,
                Radius = 400f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Nearsight,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "KatarinaE",
                ChampionName = "Katarina",
                Range = 700f,
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 210f,
                ConeDegrees = 45f,
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
                MissileSpeed = 1450f,
                MissileMinSpeed = 1450f,
                MissileMaxSpeed = 1450f,
                Width = 0f,
                Radius = 325f,
                ConeDegrees = 45f,
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
                MissileSpeed = 20f,
                MissileMinSpeed = 20f,
                MissileMaxSpeed = 20f,
                Width = 0f,
                Radius = 210f,
                ConeDegrees = 45f,
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellName = "JudicatorRighteousFury",
                ChampionName = "Kayle",
                Range = 20f,
                MissileSpeed = 779.9f,
                MissileMinSpeed = 779.9f,
                MissileMaxSpeed = 779.9f,
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
                MissileSpeed = 0f,
                MissileMinSpeed = 0f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 300f,
                ConeDegrees = 45f,
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
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //R
            new SpellInfo()
            {
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
                SpellName = "BlindMonkQOne",
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
                SpellName = "BlindMonkWOne",
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
                SpellName = "BlindMonkEOne",
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
                Range = 200f,
                MissileSpeed = 828.5f,
                MissileMinSpeed = 828.5f,
                MissileMaxSpeed = 828.5f,
                Width = 0f,
                Radius = 500f,
                ConeDegrees = 45f,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Malphite
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            #region MissFortune
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Taunt,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                MissileSpeed = 3000f,
                MissileMinSpeed = 3000f,
                MissileMaxSpeed = 3000f,
                Width = 55f,
                Radius = 325f,
                ConeDegrees = 90f,
                OtherMissileNames = new string[] { "RengarQ2"},
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q emp
            new SpellInfo()
            {
                SpellName = "RengarQEmp",
                ChampionName = "Rengar",
                Range = 300,
                MissileSpeed = 3000f,
                MissileMinSpeed = 3000f,
                MissileMaxSpeed = 3000f,
                Width = 55f,
                Radius = 300f,
                ConeDegrees = 90f,
                OtherMissileNames = new string[] { "RengarQ2Emp"},
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
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
                MissileSpeed = 0f,
                MissileMinSpeed = 2.959704E-34f,
                MissileMaxSpeed = 0f,
                Width = 0f,
                Radius = 75f,
                ConeDegrees = 45f,
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
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
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
            #region Shaco
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
                CCtype = SpellInfo.CrowdControlType.Fear,
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
            //do shyvana
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
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
            #region Soraka
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Silence,
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
                SpellName = "ToxicShotAttack",
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
                SpellName = "TeemoRCast",
                MissileName = "BantamTrapBounceSpell",
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
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
            #region TwistedFate
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            #region Twitch
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //udyr bear stance auto
            #region Udyr
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add urgot lock on q
            #region Urgot
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
                canVaryInLength = true,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
            #region Veigar
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
                Width = 87.5f,
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
                Radius = 225f,
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
                Range = 1550f,
                MissileSpeed = 1500f,
                MissileMinSpeed = 1500f,
                MissileMaxSpeed = 1500f,
                Width = 10f,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            #region Wukong
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
            //r
            new SpellInfo()
            {
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
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
            //add eq, q3, eq3
            #region Yasuo
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
                SpellType = SpellInfo.SpellTypeInfo.Wall,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
            #region Zed
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
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
                canVaryInLength = true,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
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
            foreach (SpellInfo spell in SpellInfoList)
                SpellList.Add(spell);
        }
    }
}
