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
        public enum ProjectileType
        {
            LinearSkillshot,
            CircularSkillshot,
            ArcSkillshot,
            LockOnProjectile,
            ConeSkillshot
        }
        public enum ChannelType
        {
            Cone,
            Linear,
            Circular,
            None
        }

        public static List<SpellInfo> SpellList = new List<SpellInfo>()
        {
            #region Aatrox
            new SpellInfo()
                {
                    ChampionName = "Aatrox",
                    SpellName = "AatroxE",
                    MissileName = "AatroxEConeMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 1075,
                    Delay = 250,
                    MissileSpeed = 1250,
                    Width = 35,
                    CollisionCount = 0
                },
        #endregion
            #region Ahri
            new SpellInfo()
                {
                    ChampionName = "Ahri",
                    SpellName = "AhriOrbofDeception",
                    MissileName = "AhriOrbMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1000,
                    Delay = 250,
                    MissileSpeed = 2500,
                    Width = 100,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Ahri",
                    SpellName = "AhriOrbReturn",
                    MissileName = "AhriOrbReturn",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1000,
                    Delay = 250,
                    MissileSpeed = 2500,
                    Width = 100,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Ahri",
                    SpellName = "AhriSeduce",
                    MissileName = "AhriSeduceMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Charm,
                    Slot = SpellSlot.E,
                    Range = 1000,
                    Delay = 250,
                    MissileSpeed = 1550,
                    Width = 60,
                    CollisionCount = 1
                },
            #endregion
            #region Akali
            new SpellInfo()
                {
                    ChampionName = "Akali",
                    SpellName = "AkaliMota",
                    MissileName = "AkaliMota",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.Q,
                    Range = 600,
                    Delay = 250,
                    MissileSpeed = 1000,
                    Width = 50,
                    CollisionCount = 0
                },
            #endregion
            #region Alistar
            #endregion
            #region Amumu
            new SpellInfo()
                {
                    ChampionName = "Amumu",
                    SpellName = "BandageToss",
                    MissileName = "SadMummyBandageToss",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 2000,
                    Width = 90,
                    CollisionCount = 1
                },
            #endregion
            #region Anivia
            new SpellInfo()
                {
                    ChampionName = "Anivia",
                    SpellName = "FlashFrost",
                    MissileName = "FlashFrostSpell",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 850,
                    Width = 110,
                    CollisionCount = 1
                },
            #endregion
            #region Annie
            new SpellInfo()
                {
                    ChampionName = "Annie",
                    SpellName = "Disintegrate",
                    MissileName = "Disintegrate",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 625,
                    Delay = 250,
                    MissileSpeed = 1400,
                    Width = 100,
                    CollisionCount = 1
                },
            #endregion
            #region Ashe
            new SpellInfo()
                {
                    ChampionName = "Ashe",
                    SpellName = "Volley",
                    MissileName = "VolleyAttack",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.W,
                    Range = 1250,
                    Delay = 250,
                    MissileSpeed = 1500,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Ashe",
                    SpellName = "EnchantedCrystalArrow",
                    MissileName = "EnchantedCrystalArrow",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.R,
                    Range = 20000,
                    Delay = 250,
                    MissileSpeed = 1600,
                    Width = 130,
                    CollisionCount = 1
                },
            #endregion
            //Could Stop Azir Ult
            #region Azir
            #endregion
            #region Bard
            new SpellInfo()
                {
                    ChampionName = "Bard",
                    SpellName = "BardQ",
                    MissileName = "bardqmissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1600,
                    Width = 60,
                    CollisionCount = 2
                },
            #endregion
            #region Blitzcrank
            new SpellInfo()
                {
                    ChampionName = "Blitzcrank",
                    SpellName = "RocketGrab",
                    MissileName = "RocketGrabMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1800,
                    Width = 70,
                    CollisionCount = 1
                },
            #endregion
            #region Brand
            new SpellInfo()
                {
                    ChampionName = "Brand",
                    SpellName = "BrandBlaze",
                    MissileName = "BrandBlazeMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 1600,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Brand",
                    SpellName = "BrandWildfire",
                    MissileName = "BrandWildfireMissile",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.R,
                    Range = 2500,
                    Delay = 250,
                    MissileSpeed = 1000,
                    Width = 100,
                    CollisionCount = 1
                },
            #endregion
            #region Braum
            new SpellInfo()
                {
                    ChampionName = "Braum",
                    SpellName = "BraumQ",
                    MissileName = "BraumQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Braum",
                    SpellName = "BraumRWrapper",
                    MissileName = "braumrmissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.R,
                    Range = 1200,
                    Delay = 500,
                    MissileSpeed = 1400,
                    Width = 115,
                    CollisionCount = 1
                },
            #endregion
            #region Caitlyn
            new SpellInfo()
                {
                    ChampionName = "Caitlyn",
                    SpellName = "CaitlynPiltoverPeacemaker",
                    MissileName = "CaitlynPiltoverPeacemaker",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1300,
                    Delay = 625,
                    MissileSpeed = 2200,
                    Width = 90,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Caitlyn",
                    SpellName = "CaitlynEntrapment",
                    MissileName = "CaitlynEntrapmentMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 1300,
                    Delay = 625,
                    MissileSpeed = 2200,
                    Width = 90,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Caitlyn",
                    SpellName = "CaitlynAceintheHole",
                    MissileName = "CaitlynAceintheHoleMissile",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.R,
                    Range = 1300,
                    Delay = 625,
                    MissileSpeed = 2200,
                    Width = 90,
                    CollisionCount = 0
                },
            #endregion
            #region Cassiopeia
            new SpellInfo()
                {
                    ChampionName = "Cassiopeia",
                    SpellName = "CassiopeiaMiasma",
                    MissileName = "CassiopeiaMiasma",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.W,
                    Range = 850,
                    Delay = 250,
                    MissileSpeed = 2500,
                    Width = 106,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Cassiopeia",
                    SpellName = "CassiopeiaTwinFang",
                    MissileName = "CassiopeiaTwinFang",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.E,
                    Range = 700,
                    Delay = 0,
                    MissileSpeed = 1900,
                    Width = 50,
                    CollisionCount = 1
                },
            #endregion
            #region ChoGath
            #endregion
            #region Corki
            new SpellInfo()
                {
                    ChampionName = "Corki",
                    SpellName = "PhosphorusBomb",
                    MissileName = "PhosphorusBombMissile",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 825,
                    Delay = 300,
                    MissileSpeed = 1000,
                    Width = 100,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Corki",
                    SpellName = "MissileBarrage",
                    MissileName = "MissileBarrageMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 1300,
                    Delay = 200,
                    MissileSpeed = 2000,
                    Width = 40,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Corki",
                    SpellName = "MissileBarrage2",
                    MissileName = "MissileBarrageMissile2",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 1500,
                    Delay = 200,
                    MissileSpeed = 2000,
                    Width = 40,
                    CollisionCount = 1
                },
            #endregion
            #region Darius
            #endregion
            #region Diana
            new SpellInfo()
                {
                    ChampionName = "Diana",
                    SpellName = "DianaArc",
                    MissileName = "dianaarcthrowouter",
                    ProjectileType = ProjectileType.ArcSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 825,
                    Delay = 300,
                    MissileSpeed = 1000,
                    Width = 100,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Diana",
                    SpellName = "DianaArc",
                    MissileName = "dianaarcthrowinner",
                    ProjectileType = ProjectileType.ArcSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 825,
                    Delay = 300,
                    MissileSpeed = 1000,
                    Width = 100,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Diana",
                    SpellName = "DianaArc",
                    MissileName = "dianaarcthrow",
                    ProjectileType = ProjectileType.ArcSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 825,
                    Delay = 300,
                    MissileSpeed = 1000,
                    Width = 100,
                    CollisionCount = 0
                },
            #endregion
            #region DrMundo
            new SpellInfo()
                {
                    ChampionName = "DrMundo",
                    SpellName = "InfectedCleaverMissileCast",
                    MissileName = "InfectedCleaverMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 2000,
                    Width = 60,
                    CollisionCount = 1
                },
            #endregion
            #region Draven
            new SpellInfo()
                {
                    ChampionName = "Draven",
                    SpellName = "DravenDoubleShot",
                    MissileName = "DravenDoubleShotMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Knockback,
                    Slot = SpellSlot.E,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 1400,
                    Width = 130,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Draven",
                    SpellName = "DravenRCast",
                    MissileName = "DravenR",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 20000,
                    Delay = 400,
                    MissileSpeed = 2000,
                    Width = 160,
                    CollisionCount = 1
                },
            #endregion  
            #region Ekko
            new SpellInfo()
                {
                    ChampionName = "Ekko",
                    SpellName = "EkkoQ",
                    MissileName = "ekkoqmis",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1650,
                    Width = 60,
                    CollisionCount = 0
                },
            #endregion
            //Need to add Elise Cocoon
            #region Elise
            #endregion
            //Need to add Evelynn Q
            #region Evelynn
            #endregion
            #region Ezreal
            new SpellInfo()
                {
                    ChampionName = "Ezreal",
                    SpellName = "EzrealMysticShot",
                    MissileName = "EzrealMysticShotMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1200,
                    Delay = 250,
                    MissileSpeed = 2000,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Ezreal",
                    SpellName = "EzrealEssenceFlux",
                    MissileName = "EzrealEssenceFluxMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.W,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1600,
                    Width = 80,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Ezreal",
                    SpellName = "EzrealArcaneShift",
                    MissileName = "EzrealArcaneShiftMissile",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.E,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1600,
                    Width = 80,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Ezreal",
                    SpellName = "EzrealTrueshotBarrage",
                    MissileName = "EzrealTrueshotBarrage",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 20000,
                    Delay = 1000,
                    MissileSpeed = 2000,
                    Width = 160,
                    CollisionCount = 0
                },
            #endregion
            #region Fiddlesticks
            new SpellInfo()
                {
                    ChampionName = "FiddleSticks",
                    SpellName = "FiddlesticksDarkWind",
                    MissileName = "FiddlesticksDarkWind",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Silence,
                    Slot = SpellSlot.E,
                    Range = 600,
                    Delay = 250,
                    MissileSpeed = 1200,
                    Width = 210,
                    CollisionCount = 0
                },
            #endregion
            #region Fiora
            new SpellInfo()
                {
                    ChampionName = "Fiora",
                    SpellName = "FioraW",
                    MissileName = "FioraWMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Counter,
                    Slot = SpellSlot.W,
                    Range = 800,
                    Delay = 500,
                    MissileSpeed = 3200,
                    Width = 70,
                    CollisionCount = 0
                },
            #endregion
            #region Fizz
            new SpellInfo()
                {
                    ChampionName = "Fizz",
                    SpellName = "FizzMarinerDoom",
                    MissileName = "FizzMarinerDoomMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.R,
                    Range = 1300,
                    Delay = 250,
                    MissileSpeed = 1350,
                    Width = 120,
                    CollisionCount = 1
                },
            #endregion
            #region Galio
            new SpellInfo()
                {
                    ChampionName = "Galio",
                    SpellName = "GalioResoluteSmite",
                    MissileName = "GalioResoluteSmite",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 900,
                    Delay = 250,
                    MissileSpeed = 1300,
                    Width = 200,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Galio",
                    SpellName = "GalioRighteousGust",
                    MissileName = "GalioRighteousGust",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.E,
                    Range = 1200,
                    Delay = 250,
                    MissileSpeed = 1200,
                    Width = 120,
                    CollisionCount = 0
                },
            #endregion
            #region Gangplank
            new SpellInfo()
                {
                    ChampionName = "Gangplank",
                    SpellName = "GangplankQWrapper",
                    MissileName = "GangplankQWrapper",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.Q,
                    Range = 625,
                    Delay = 250,
                    MissileSpeed = 1650,
                    Width = 30,
                    CollisionCount = 1
                },
            #endregion
            #region Garen
            #endregion
            #region Gnar
            new SpellInfo()
                {
                    ChampionName = "Gnar",
                    SpellName = "GnarQ",
                    MissileName = "gnarqmissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 1125,
                    Delay = 250,
                    MissileSpeed = 2500,
                    Width = 60,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Gnar",
                    SpellName = "GnarQReturn",
                    MissileName = "GnarQMissileReturn",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 2500,
                    Delay = 0,
                    MissileSpeed = 2500,
                    Width = 75,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Gnar",
                    SpellName = "GnarBigQ",
                    MissileName = "GnarBQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 1150,
                    Delay = 500,
                    MissileSpeed = 2100,
                    Width = 90,
                    CollisionCount = 0
                },
            #endregion
            #region Gragas
            new SpellInfo()
                {
                    ChampionName = "Gragas",
                    SpellName = "GragasQ",
                    MissileName = "GragasQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 1300,
                    Width = 275,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Gragas",
                    SpellName = "GragasR",
                    MissileName = "GragasRBoom",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Knockback,
                    Slot = SpellSlot.R,
                    Range = 1050,
                    Delay = 375,
                    MissileSpeed = 1800,
                    Width = 200,
                    CollisionCount = 0
                },
            #endregion
            #region Graves
            new SpellInfo()
                {
                    ChampionName = "Graves",
                    SpellName = "GravesQLineSpell",
                    MissileName = "GravesQLineMis",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 808,
                    Delay = 250,
                    MissileSpeed = 3000,
                    Width = 40,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Graves",
                    SpellName = "GravesChargeShot",
                    MissileName = "GravesChargeShotShot",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 2100,
                    Width = 100,
                    CollisionCount = 0
                },
            #endregion
            //Need to add Hecarim Ult
            #region Hecarim
            #endregion
            //Need to add Heimerdinger Ulted W/E. Possible fix on W
            #region Heimerdinger
            new SpellInfo()
                {
                    ChampionName = "Heimerdinger",
                    SpellName = "HeimerdingerW",
                    MissileName = "HeimerdingerW",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.W,
                    Range = 2500,
                    Delay = 250,
                    MissileSpeed = 902,
                    Width = 100,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Heimerdinger",
                    SpellName = "HeimerdingerE",
                    MissileName = "heimerdingerespell",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 925,
                    Delay = 250,
                    MissileSpeed = 1200,
                    Width = 100,
                    CollisionCount = 0
                },
            #endregion
            #region Illaoi
            #endregion
            #region Irelia
            new SpellInfo()
                {
                    ChampionName = "Irelia",
                    SpellName = "IreliaTranscendentBlades",
                    MissileName = "IreliaTranscendentBlades",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 1200,
                    Delay = 0,
                    MissileSpeed = 1600,
                    Width = 65,
                    CollisionCount = 0
                },
            #endregion
            #region Janna
            new SpellInfo()
                {
                    ChampionName = "Janna",
                    SpellName = "HowlingGale",
                    MissileName = "HowlingGaleSpell",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.Q,
                    Range = 1700,
                    Delay = 250,
                    MissileSpeed = 900,
                    Width = 120,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Janna",
                    SpellName = "SowTheWind",
                    MissileName = "SowTheWind",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.W,
                    Range = 1700,
                    Delay = 250,
                    MissileSpeed = 900,
                    Width = 120,
                    CollisionCount = 0
                },
            #endregion
            #region JarvanIV
            #endregion
            #region Jax
            #endregion
            #region Jayce
            new SpellInfo()
                {
                    ChampionName = "Jayce",
                    SpellName = "jayceshockblast",
                    MissileName = "JayceShockBlastMis",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1300,
                    Delay = 250,
                    MissileSpeed = 1450,
                    Width = 70,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Jayce",
                    SpellName = "JayceQAccel",
                    MissileName = "JayceShockBlastWallMis",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1300,
                    Delay = 250,
                    MissileSpeed = 2350,
                    Width = 70,
                    CollisionCount = 0
                },
            #endregion
            #region Jhin
            new SpellInfo()
                {
                    ChampionName = "Jhin",
                    SpellName = "JhinQ",
                    MissileName = "JhinQMisBounce",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1500,
                    Delay = 600,
                    MissileSpeed = 3300,
                    Width = 60,
                    CollisionCount = 4
                },
            new SpellInfo()
                {
                    ChampionName = "Jhin",
                    SpellName = "JhinW",
                    MissileName = "JhinWMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Snare,
                    Slot = SpellSlot.W,
                    Range = 1500,
                    Delay = 600,
                    MissileSpeed = 3300,
                    Width = 60,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Jhin",
                    SpellName = "JhinE",
                    MissileName = "JhinETrap",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.E,
                    Range = 1500,
                    Delay = 600,
                    MissileSpeed = 3300,
                    Width = 60,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Jhin",
                    SpellName = "JhinR",
                    MissileName = "JhinRShotMis",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 1500,
                    Delay = 600,
                    MissileSpeed = 3300,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Jhin",
                    SpellName = "JhinR",
                    MissileName = "JhinRShotMis4",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 1500,
                    Delay = 600,
                    MissileSpeed = 3300,
                    Width = 60,
                    CollisionCount = 1
                },
            #endregion
            #region Jinx
            new SpellInfo()
                {
                    ChampionName = "Jinx",
                    SpellName = "JinxW",
                    MissileName = "JinxWMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.W,
                    Range = 1500,
                    Delay = 600,
                    MissileSpeed = 3300,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Jinx",
                    SpellName = "JinxR",
                    MissileName = "JinxR",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 20000,
                    Delay = 600,
                    MissileSpeed = 1700,
                    Width = 140,
                    CollisionCount = 0
                },
            #endregion
            #region Kalista
            new SpellInfo()
                {
                    ChampionName = "Kalista",
                    SpellName = "KalistaMysticShot",
                    MissileName = "KalistaMysticShot",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1700,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 40,
                    CollisionCount = 0
                },
            #endregion
            #region Karma
            new SpellInfo()
                {
                    ChampionName = "Karma",
                    SpellName = "KarmaQ",
                    MissileName = "KarmaQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Karma",
                    SpellName = "KarmaQManta",
                    MissileName = "KarmaQMissileMantra",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 80,
                    CollisionCount = 1
                },
            #endregion
            #region Karthus
            #endregion
            //Need to add Kassadin Q
            #region Kassadin
            #endregion
            #region Katarina
            new SpellInfo()
                {
                    ChampionName = "Katarina",
                    SpellName = "KatarinaQ",
                    MissileName = "KatarinaQ",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Katarina",
                    SpellName = "KatarinaR",
                    MissileName = "KatarinaRMis",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    ChannelType = ChannelType.Circular,
                    Slot = SpellSlot.R,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 1
                },
            #endregion
            #region Kayle
            new SpellInfo()
                {
                    ChampionName = "Kayle",
                    SpellName = "JudicatorReckoning",
                    MissileName = "JudicatorReckoning",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 650,
                    Delay = 250,
                    MissileSpeed = 900,
                    Width = 120,
                    CollisionCount = 1
                },
            #endregion
            #region Kennen
            new SpellInfo()
                {
                    ChampionName = "Kennen",
                    SpellName = "KennenShruikenHurlMissile1",
                    MissileName = "KennenShruikenHurlMissile1",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 125,
                    MissileSpeed = 1700,
                    Width = 50,
                    CollisionCount = 1
                },
            #endregion
            //Possibly need to add Khazix upgraded W
            #region KhaZix
            new SpellInfo()
                {
                    ChampionName = "Khazix",
                    SpellName = "KhazixW",
                    MissileName = "KhazixWMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.W,
                    Range = 1025,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 73,
                    CollisionCount = 1
                },
            #endregion
            //Need to add Kindred Q and Kindred E
            #region Kindred
            #endregion
            #region KogMaw
            new SpellInfo()
            {
                ChampionName = "KogMaw",
                SpellName = "KogMawQ",
                MissileName = "KogMawQ",
                ProjectileType = ProjectileType.LinearSkillshot,
                Slot = SpellSlot.Q,
                Range = 1200,
                Delay = 250,
                MissileSpeed = 1650,
                Width = 70,
                CollisionCount = 1
            },
            new SpellInfo()
            {
                ChampionName = "KogMaw",
                SpellName = "KogMawVoidOoze",
                MissileName = "KogMawVoidOozeMissile",
                ProjectileType = ProjectileType.LinearSkillshot,
                CCType = BuffType.Slow,
                Slot = SpellSlot.E,
                Range = 1650,
                Delay = 250,
                MissileSpeed = 1400,
                Width = 120,
                CollisionCount = 0
            },
            #endregion
            #region LeBlanc
            new SpellInfo()
                {
                    ChampionName = "Leblanc",
                    SpellName = "LeblancSoulShackle",
                    MissileName = "LeblancSoulShackle",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1750,
                    Width = 70,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Leblanc",
                    SpellName = "LeblancSoulShackleM",
                    MissileName = "LeblancSoulShackleM",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.R,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1750,
                    Width = 70,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Leblanc",
                    SpellName = "LeblancChaosOrb",
                    MissileName = "LeblancChaosOrb",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.Q,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1750,
                    Width = 70,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Leblanc",
                    SpellName = "LeblancChaosOrbM",
                    MissileName = "LeblancChaosOrbM",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.R,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1750,
                    Width = 70,
                    CollisionCount = 1
                },
            #endregion
            #region LeeSin
            new SpellInfo()
                {
                    ChampionName = "LeeSin",
                    SpellName = "BlindMonkQOne",
                    MissileName = "BlindMonkQOne",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 1800,
                    Width = 65,
                    CollisionCount = 1
                },
            #endregion
            #region Leona
            new SpellInfo()
                {
                    ChampionName = "Leona",
                    SpellName = "LeonaZenithBlade",
                    MissileName = "LeonaZenithBladeMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 905,
                    Delay = 250,
                    MissileSpeed = 2000,
                    Width = 70,
                    CollisionCount = 0
                },
            #endregion
            #region Lissandra
            new SpellInfo()
                {
                    ChampionName = "Lissandra",
                    SpellName = "LissandraQ",
                    MissileName = "LissandraQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 700,
                    Delay = 250,
                    MissileSpeed = 2200,
                    Width = 75,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Lissandra",
                    SpellName = "LissandraQShards",
                    MissileName = "lissandraqshards",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 700,
                    Delay = 250,
                    MissileSpeed = 2200,
                    Width = 90,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Lissandra",
                    SpellName = "LissandraE",
                    MissileName = "LissandraEMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.E,
                    Range = 1025,
                    Delay = 250,
                    MissileSpeed = 850,
                    Width = 125,
                    CollisionCount = 0
                },
            #endregion
            #region Lucian
            new SpellInfo()
                {
                    ChampionName = "Lucian",
                    SpellName = "LucianW",
                    MissileName = "lucianwmissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.W,
                    Range = 1000,
                    Delay = 250,
                    MissileSpeed = 1600,
                    Width = 55,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Lucian",
                    SpellName = "LucianRMis",
                    MissileName = "lucianrmissileoffhand",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    ChannelType = ChannelType.Linear,
                    Slot = SpellSlot.R,
                    Range = 1400,
                    Delay = 500,
                    MissileSpeed = 2800,
                    Width = 110,
                    CollisionCount = 1
                },
            #endregion
            //Add Lulu W
            #region Lulu
            new SpellInfo()
                {
                    ChampionName = "Lulu",
                    SpellName = "LuluQ",
                    MissileName = "LuluQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1450,
                    Width = 60,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Lulu",
                    SpellName = "LuluQPix",
                    MissileName = "LuluQMissileTwo",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1450,
                    Width = 60,
                    CollisionCount = 0
                },
            #endregion
            //Add Lux W
            #region Lux
            new SpellInfo()
                {
                    ChampionName = "Lux",
                    SpellName = "LuxLightBinding",
                    MissileName = "LuxLightBindingMis",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 1300,
                    Delay = 250,
                    MissileSpeed = 1200,
                    Width = 70,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Lux",
                    SpellName = "LuxLightStrikeKugel",
                    MissileName = "LuxLightStrikeKugel",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 1300,
                    Width = 275,
                    CollisionCount = 0
                },
            #endregion
            #region Malphite
            new SpellInfo()
                {
                    ChampionName = "Malphite",
                    SpellName = "SeismicShard",
                    MissileName = "SeismicShard",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 625,
                    Delay = 250,
                    MissileSpeed = 900,
                    Width = 120,
                    CollisionCount = 0
                },
            #endregion
            #region Malzahar
            #endregion
            #region Maokai
            #endregion
            #region MasterYi
            #endregion
            #region MissFortune
            new SpellInfo()
                {
                    ChampionName = "MissFortune",
                    SpellName = "MissFortuneRicochetShot",
                    MissileName = "MissFortuneRicochetShot",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "MissFortune",
                    SpellName = "MissFortuneRicochetShot",
                    MissileName = "MissFortuneRicochetShotDud",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "MissFortune",
                    SpellName = "MissFortuneRicochetShot",
                    MissileName = "MissFortuneRShotExtra",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "MissFortune",
                    SpellName = "MissFortuneBulletTime",
                    MissileName = "missfortunebullets",
                    ProjectileType = ProjectileType.ConeSkillshot,
                    ChannelType = ChannelType.Cone,
                    Slot = SpellSlot.R,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "MissFortune",
                    SpellName = "MissFortuneBulletTime",
                    MissileName = "missfortunebulletsclone",
                    ProjectileType = ProjectileType.ConeSkillshot,
                    ChannelType = ChannelType.Cone,
                    Slot = SpellSlot.R,
                    Range = 1050,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 1
                },
            #endregion
            #region Mordekaiser
            #endregion
            #region Morgana
            new SpellInfo()
                {
                    ChampionName = "Morgana",
                    SpellName = "DarkBindingMissile",
                    MissileName = "DarkBindingMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Snare,
                    Slot = SpellSlot.Q,
                    Range = 1300,
                    Delay = 250,
                    MissileSpeed = 1200,
                    Width = 80,
                    CollisionCount = 1
                },
            #endregion
            #region Nami
            new SpellInfo()
                {
                    ChampionName = "Nami",
                    SpellName = "NamiQ",
                    MissileName = "namiqmissile",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.Q,
                    Range = 1625,
                    Delay = 250,
                    MissileSpeed = 1450,
                    Width = 150,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Nami",
                    SpellName = "NamiR",
                    MissileName = "NamiRMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.R,
                    Range = 2750,
                    Delay = 500,
                    MissileSpeed = 850,
                    Width = 260,
                    CollisionCount = 0
                },
            #endregion
            #region Nasus
            #endregion
            //Add Naut E
            #region Nautilus
            new SpellInfo()
                {
                    ChampionName = "Nautilus",
                    SpellName = "NautilusAnchorDrag",
                    MissileName = "NautilusAnchorDragMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 1250,
                    Delay = 250,
                    MissileSpeed = 2000,
                    Width = 90,
                    CollisionCount = 1
                },
            #endregion
            #region Nidalee
            new SpellInfo()
                {
                    ChampionName = "Nidalee",
                    SpellName = "JavelinToss",
                    MissileName = "JavelinToss",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1500,
                    Delay = 250,
                    MissileSpeed = 1300,
                    Width = 40,
                    CollisionCount = 0
                },
            #endregion
            //Need to add Nocturne Q
            #region Nocturne
            #endregion
            #region Nunu
            new SpellInfo()
                {
                    ChampionName = "Nunu",
                    SpellName = "IceBlast",
                    MissileName = "IceBlast",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 550,
                    Delay = 250,
                    MissileSpeed = 900,
                    Width = 120,
                    CollisionCount = 0
                },
            #endregion
            #region Olaf
            #endregion
            #region Orianna
            #endregion
            //Need to add Pantheon Q/E
            #region Pantheon
            #endregion
            #region Poppy
            new SpellInfo()
                {
                    ChampionName = "Poppy",
                    SpellName = "PoppyPassiveBounce",
                    MissileName = "PoppyPassiveAttack",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.Internal,
                    Range = 1200,
                    Delay = 100,
                    MissileSpeed = 1600,
                    Width = 100,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Poppy",
                    SpellName = "PoppyRSpell",
                    MissileName = "PoppyRMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.R,
                    Range = 1200,
                    Delay = 100,
                    MissileSpeed = 1600,
                    Width = 100,
                    CollisionCount = 1
                },
            #endregion
            #region Quinn
            new SpellInfo()
                {
                    ChampionName = "Quinn",
                    SpellName = "QuinnQ",
                    MissileName = "QuinnQ",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Blind,
                    Slot = SpellSlot.Q,
                    Range = 1050,
                    Delay = 313,
                    MissileSpeed = 1550,
                    Width = 60,
                    CollisionCount = 1
                },
            #endregion
            #region Rammus
            #endregion
            #region RekSai
            new SpellInfo()
                {
                    ChampionName = "RekSai",
                    SpellName = "rekaiqburrowed",
                    MissileName = "RekSaiQBurrowedMis",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1625,
                    Delay = 500,
                    MissileSpeed = 1950,
                    Width = 60,
                    CollisionCount = 0
                },
            #endregion
            #region Renekton
            #endregion
            //Need to add Rengar E with ferocity?
            #region Rengar
            new SpellInfo()
                {
                    ChampionName = "Rengar",
                    SpellName = "RengarE",
                    MissileName = "RengarEFinal",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 1000,
                    Delay = 250,
                    MissileSpeed = 1500,
                    Width = 70,
                    CollisionCount = 1
                },
            #endregion
            #region Riven
            new SpellInfo()
                {
                    ChampionName = "Riven",
                    SpellName = "rivenizunablade",
                    MissileName = "RivenLightsaberMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 1600,
                    Delay = 250,
                    MissileSpeed = 1450,
                    Width = 125,
                    CollisionCount = 0
                },
            #endregion
            #region Rumble
            new SpellInfo()
                {
                    ChampionName = "Rumble",
                    SpellName = "RumbleGrenade",
                    MissileName = "RumbleGrenade",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.E,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 2000,
                    Width = 60,
                    CollisionCount = 0
                },
            #endregion
            #region Ryze
            new SpellInfo()
                {
                    ChampionName = "Ryze",
                    SpellName = "RyzeQ",
                    MissileName = "RyzeQ",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 900,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 50,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Ryze",
                    SpellName = "ryzerq",
                    MissileName = "ryzerq",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 900,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 50,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Ryze",
                    SpellName = "RyzeE",
                    MissileName = "RyzeE",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.E,
                    Range = 600,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 50,
                    CollisionCount = 1
                },
            #endregion
            #region Sejuani
            new SpellInfo()
                {
                    ChampionName = "Sejuani",
                    SpellName = "SejuaniGlacialPrisonStart",
                    MissileName = "sejuaniglacialprison",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.R,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 1600,
                    Width = 110,
                    CollisionCount = 0
                },
            #endregion
            //Need to add Shaco Box's attacks and E
            #region Shaco
            #endregion
            #region Shen
            #endregion
            #region Shyvana
            new SpellInfo()
                {
                    ChampionName = "Shyvana",
                    SpellName = "ShyvanaFireball",
                    MissileName = "ShyvanaFireballMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.E,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Shyvana",
                    SpellName = "shyvanafireballdragon2",
                    MissileName = "ShyvanaFireballDragonFxMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.E,
                    Range = 850,
                    Delay = 250,
                    MissileSpeed = 2000,
                    Width = 70,
                    CollisionCount = 0
                },
            #endregion
            #region Singed
            #endregion
            #region Sion
            new SpellInfo()
                {
                    ChampionName = "Sion",
                    SpellName = "SionE",
                    MissileName = "SionEMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 800,
                    Delay = 250,
                    MissileSpeed = 1800,
                    Width = 80,
                    CollisionCount = 1
                },
            #endregion
            #region Sivir
            new SpellInfo()
                {
                    ChampionName = "Sivir",
                    SpellName = "SivirQReturn",
                    MissileName = "SivirMissileReturn",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1250,
                    Delay = 0,
                    MissileSpeed = 1350,
                    Width = 100,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Sivir",
                    SpellName = "SivirQ",
                    MissileName = "SivirQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1250,
                    Delay = 250,
                    MissileSpeed = 1350,
                    Width = 90,
                    CollisionCount = 0
                },
            #endregion
            #region Skarner
            new SpellInfo()
                {
                    ChampionName = "Skarner",
                    SpellName = "SkarnerFracture",
                    MissileName = "SkarnerFractureMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 1000,
                    Delay = 250,
                    MissileSpeed = 1500,
                    Width = 70,
                    CollisionCount = 0
                },
            #endregion
            #region Sona
            new SpellInfo()
                {
                    ChampionName = "Sona",
                    SpellName = "SonaQ",
                    MissileName = "sonaqmissile",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.Q,
                    Range = 1000,
                    Delay = 250,
                    MissileSpeed = 2400,
                    Width = 140,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Sona",
                    SpellName = "SonaR",
                    MissileName = "SonaR",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.R,
                    Range = 1000,
                    Delay = 250,
                    MissileSpeed = 2400,
                    Width = 140,
                    CollisionCount = 0
                },
            #endregion
            #region Soraka
            new SpellInfo()
                {
                    ChampionName = "Soraka",
                    SpellName = "SorakaQ",
                    MissileName = "SorakaQMissile",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 950,
                    Delay = 500,
                    MissileSpeed = 1750,
                    Width = 300,
                    CollisionCount = 0
                },
            #endregion
            #region Swain
            new SpellInfo()
                {
                    ChampionName = "Swain",
                    SpellName = "SwainToment",
                    MissileName = "SwainToment",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.E,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Swain",
                    SpellName = "SwainMetaNuke",
                    MissileName = "SwainMetamorphism",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    ChannelType = ChannelType.Circular,
                    Slot = SpellSlot.R,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 60,
                    CollisionCount = 0
                },
            #endregion
            //Add Syndra R
            #region Syndra
            new SpellInfo()
                {
                    ChampionName = "Syndra",
                    SpellName = "syndrae5",
                    MissileName = "syndrae5",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 950,
                    Delay = 0,
                    MissileSpeed = 2000,
                    Width = 100,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Syndra",
                    SpellName = "SyndraE",
                    MissileName = "SyndraE",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 950,
                    Delay = 0,
                    MissileSpeed = 2000,
                    Width = 100,
                    CollisionCount = 0
                },
            #endregion
            #region TahmKench
            new SpellInfo()
                {
                    ChampionName = "TahmKench",
                    SpellName = "TahmKenchQ",
                    MissileName = "tahmkenchqmissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 951,
                    Delay = 250,
                    MissileSpeed = 2800,
                    Width = 90,
                    CollisionCount = 0
                },
            #endregion
            //Add Talon R
            #region Talon
            new SpellInfo()
                {
                    ChampionName = "Talon",
                    SpellName = "TalonRake",
                    MissileName = "talonrakemissileone",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 800,
                    Delay = 250,
                    MissileSpeed = 2300,
                    Width = 80,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Talon",
                    SpellName = "TalonRakeReturn",
                    MissileName = "talonrakemissiletwo",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 800,
                    Delay = 250,
                    MissileSpeed = 1850,
                    Width = 80,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Talon",
                    SpellName = "TalonShadowAssault",
                    MissileName = "TalonShadowAssaultMisTwo",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.R,
                    Range = 800,
                    Delay = 250,
                    MissileSpeed = 1850,
                    Width = 80,
                    CollisionCount = 0
                },
            #endregion
            #region Taric
            new SpellInfo()
                {
                    ChampionName = "Taric",
                    SpellName = "TaricDazzleStun",
                    MissileName = "TaricDazzleStunMissile",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 625,
                    Delay = 250,
                    MissileSpeed = 1000,
                    Width = 80,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Taric",
                    SpellName = "Dazzle",
                    MissileName = "Dazzle",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 625,
                    Delay = 250,
                    MissileSpeed = 1400,
                    Width = 80,
                    CollisionCount = 0
                },
            #endregion
            #region Teemo
            new SpellInfo()
                {
                    ChampionName = "Teemo",
                    SpellName = "BlindingDart",
                    MissileName = "BlindingDart",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Blind,
                    Slot = SpellSlot.Q,
                    Range = 1100,
                    Delay = 500,
                    MissileSpeed = 1900,
                    Width = 70,
                    CollisionCount = 0
                },
            #endregion
            #region Thresh
            new SpellInfo()
                {
                    ChampionName = "Thresh",
                    SpellName = "ThreshQ",
                    MissileName = "ThreshQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.Q,
                    Range = 1100,
                    Delay = 500,
                    MissileSpeed = 1900,
                    Width = 70,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Thresh",
                    SpellName = "ThreshEFlay",
                    MissileName = "ThreshEMissile1",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.E,
                    Range = 1075,
                    Delay = 125,
                    MissileSpeed = 2000,
                    Width = 110,
                    CollisionCount = 0
                },
            #endregion
            #region Tristana
            new SpellInfo()
                {
                    ChampionName = "Tristana",
                    SpellName = "tristanae",
                    MissileName = "tristanae",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.E,
                    Range = 669,
                    Delay = 250,
                    MissileSpeed = 900,
                    Width = 50,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Tristana",
                    SpellName = "TristanaR",
                    MissileName = "TristanaR",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    CCType = BuffType.Knockback,
                    Slot = SpellSlot.R,
                    Range = 669,
                    Delay = 250,
                    MissileSpeed = 900,
                    Width = 50,
                    CollisionCount = 0
                },
            #endregion
            #region Trundle
            #endregion
            #region Tryndamere
            #endregion
            //Add Yellow Card
            #region TwistedFate
            new SpellInfo()
                {
                    ChampionName = "TwistedFate",
                    SpellName = "WildCards",
                    MissileName = "SealFateMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1450,
                    Delay = 250,
                    MissileSpeed = 1000,
                    Width = 40,
                    CollisionCount = 0
                },
            #endregion
            #region Twitch
            new SpellInfo()
                {
                    ChampionName = "Twitch",
                    SpellName = "TwitchVenomCask",
                    MissileName = "TwitchVenomCaskMissile",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.W,
                    Range = 900,
                    Delay = 250,
                    MissileSpeed = 1400,
                    Width = 275,
                    CollisionCount = 0
                },
            #endregion
            #region Udyr
            #endregion
            #region Urgot
            new SpellInfo()
            {
                ChampionName = "Urgot",
                SpellName = "UrgotHeatseekingLineMissile",
                MissileName = "UrgotHeatseekingLineMissile",
                ProjectileType = ProjectileType.LinearSkillshot,
                Slot = SpellSlot.Q,
                Range = 1000,
                Delay = 125,
                MissileSpeed = 1600,
                Width = 60,
                CollisionCount = 1
            },
            new SpellInfo()
            {
                ChampionName = "Urgot",
                SpellName = "UrgotPlasmaGrenade",
                MissileName = "UrgotPlasmaGrenadeBoom",
                ProjectileType = ProjectileType.CircularSkillshot,
                Slot = SpellSlot.E,
                Range = 1100,
                Delay = 250,
                MissileSpeed = 1500,
                Width = 60,
                CollisionCount = 1
            },
            #endregion
            #region Varus
            new SpellInfo()
                {
                    ChampionName = "Varus",
                    SpellName = "VarusQMissilee",
                    MissileName = "VarusQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 1800,
                    Delay = 250,
                    MissileSpeed = 1900,
                    Width = 70,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Varus",
                    SpellName = "VarusE",
                    MissileName = "VarusE",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.E,
                    Range = 925,
                    Delay = 1000,
                    MissileSpeed = 1500,
                    Width = 235,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Varus",
                    SpellName = "VarusR",
                    MissileName = "VarusRMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 1200,
                    Delay = 250,
                    MissileSpeed = 1950,
                    Width = 120,
                    CollisionCount = 0
                },
            #endregion
            //Add Vayne Condemn
            #region Vayne
            #endregion
            #region Veigar
            new SpellInfo()
                {
                    ChampionName = "Veigar",
                    SpellName = "VeigarBalefulStrike",
                    MissileName = "VeigarBalefulStrikeMis",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 2000,
                    Width = 70,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Veigar",
                    SpellName = "VeigarPrimordialBurst",
                    MissileName = "VeigarPrimordialBurst",
                    ProjectileType = ProjectileType.LockOnProjectile,
                    Slot = SpellSlot.R,
                    Range = 950,
                    Delay = 250,
                    MissileSpeed = 2000,
                    Width = 70,
                    CollisionCount = 0
                },
            #endregion
            #region VelKoz
            new SpellInfo()
                {
                    ChampionName = "Velkoz",
                    SpellName = "VelkozQ",
                    MissileName = "VelkozQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 1300,
                    Width = 50,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Velkoz",
                    SpellName = "VelkozQSplit",
                    MissileName = "VelkozQMissileSplit",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Slow,
                    Slot = SpellSlot.Q,
                    Range = 1100,
                    Delay = 250,
                    MissileSpeed = 2100,
                    Width = 55,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Velkoz",
                    SpellName = "VelkozW",
                    MissileName = "VelkozWMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.W,
                    Range = 1200,
                    Delay = 125,
                    MissileSpeed = 1700,
                    Width = 88,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Velkoz",
                    SpellName = "VelkozE",
                    MissileName = "VelkozEMissile",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.E,
                    Range = 1200,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 88,
                    CollisionCount = 0
                },
            #endregion
            #region Vi
            #endregion
            //Add Viktor Q
            #region Viktor
            new SpellInfo()
                {
                    ChampionName = "Viktor",
                    SpellName = "Laser",
                    MissileName = "ViktorDeathRayMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.E,
                    Range = 1500,
                    Delay = 250,
                    MissileSpeed = 780,
                    Width = 80,
                    CollisionCount = 0
                },
            #endregion
            #region Vladimir
            #endregion
            #region Volibear
            #endregion
            #region Warwick
            #endregion
            #region Wukong
            #endregion
            #region Xerath
            new SpellInfo()
                {
                    ChampionName = "Xerath",
                    SpellName = "XerathMageSpear",
                    MissileName = "XerathMageSpearMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 1150,
                    Delay = 200,
                    MissileSpeed = 1400,
                    Width = 60,
                    CollisionCount = 1
                },
            #endregion
            #region XinZhao
            #endregion
            #region Yasuo
            new SpellInfo()
                {
                    ChampionName = "Yasuo",
                    SpellName = "yasuoq3w",
                    MissileName = "yasuoq3mis",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.Q,
                    Range = 1150,
                    Delay = 500,
                    MissileSpeed = 1500,
                    Width = 90,
                    CollisionCount = 0
                },
            #endregion
            #region Yorick
            #endregion
            #region Zac
            #endregion
            //Add Zed W
            #region Zed
            new SpellInfo()
                {
                    ChampionName = "Zed",
                    SpellName = "ZedQ",
                    MissileName = "ZedQMissile",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 925,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 50,
                    CollisionCount = 0
                },
            #endregion
            #region Ziggs
            new SpellInfo()
                {
                    ChampionName = "Ziggs",
                    SpellName = "ZiggsQ",
                    MissileName = "ZiggsQSpell",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 850,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 140,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Ziggs",
                    SpellName = "ZiggsQ",
                    MissileName = "ZiggsQSpell2",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 850,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 140,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Ziggs",
                    SpellName = "ZiggsQ",
                    MissileName = "ZiggsQSpell3",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Q,
                    Range = 850,
                    Delay = 250,
                    MissileSpeed = 1700,
                    Width = 140,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Ziggs",
                    SpellName = "ZiggsW",
                    MissileName = "ZiggsW",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    CCType = BuffType.Knockup,
                    Slot = SpellSlot.W,
                    Range = 1000,
                    Delay = 250,
                    MissileSpeed = 1750,
                    Width = 50,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Ziggs",
                    SpellName = "ZiggsE",
                    MissileName = "ziggse2",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    Slot = SpellSlot.E,
                    Range = 900,
                    Delay = 500,
                    MissileSpeed = 1750,
                    Width = 50,
                    CollisionCount = 0
                },
            new SpellInfo()
                {
                    ChampionName = "Ziggs",
                    SpellName = "ZiggsR",
                    MissileName = "ziggsrboom",
                    ProjectileType = ProjectileType.CircularSkillshot,
                    Slot = SpellSlot.R,
                    Range = 5300,
                    Delay = 500,
                    MissileSpeed = 5000,
                    Width = 50,
                    CollisionCount = 0
                },
            #endregion
            #region Zilean
            #endregion
            #region Zyra
            new SpellInfo()
                {
                    ChampionName = "Zyra",
                    SpellName = "ZyraGraspingRoots",
                    MissileName = "ZyraGraspingRoots",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    CCType = BuffType.Stun,
                    Slot = SpellSlot.E,
                    Range = 1150,
                    Delay = 250,
                    MissileSpeed = 1150,
                    Width = 70,
                    CollisionCount = 1
                },
            new SpellInfo()
                {
                    ChampionName = "Zyra",
                    SpellName = "zyrapassivedeathmanager",
                    MissileName = "zyrapassivedeathmanager",
                    ProjectileType = ProjectileType.LinearSkillshot,
                    Slot = SpellSlot.Unknown,
                    Range = 1474,
                    Delay = 500,
                    MissileSpeed = 2000,
                    Width = 70,
                    CollisionCount = 0
                }
            #endregion
        };
    }
}
