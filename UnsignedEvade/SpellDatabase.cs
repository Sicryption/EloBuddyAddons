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
        static SpellInfo[] SpellInfoList = new SpellInfo[]
        {
            #region Aatrox
            #endregion
            #region Ahri
            #endregion
            #region Akali
            #endregion
            #region Alistar
            #endregion
            #region Amumu
            #endregion
            #region Anivia
            #endregion
            #region Annie
            #endregion
            #region Ashe
            #endregion
            #region Azir
            #endregion
            #region Bard
            #endregion
            #region Blitzcrank
            #endregion
            #region Brand
            #endregion
            #region Braum
            #endregion
            #region Caitlyn
            #endregion
            #region Cassiopeia
            #endregion
            #region ChoGath
            #endregion
            #region Corki
            #endregion
            #region Darius
            #endregion
            #region Diana
            #endregion
            #region DrMundo
            new SpellInfo()
            {
                ChampionName = "DrMundo",
                SpellName = "InfectedCleaverMissile",
                SkillshotType = SkillShotType.Linear,
                Slot = SpellSlot.Q,
                Range = 1050,
                Delay = 250,
                MissileSpeed = 2000,
                Width = 60,
                CollisionCount = 1
            },
            #endregion
            #region Draven
            #endregion  
            #region Ekko
            #endregion
            #region Elise
            #endregion
            #region Evelynn
            #endregion
            #region Ezreal
            #endregion
            #region Fiddlesticks
            #endregion
            #region Fiora
            #endregion
            #region Fizz
            #endregion
            #region Galio
            #endregion
            #region Gangplank
            #endregion
            #region Garen
            #endregion
            #region Gnar
            #endregion
            #region Gragas
            #endregion
            #region Graves
            #endregion
            #region Hecarim
            #endregion
            #region Heimerdinger
            #endregion
            #region Illaoi
            #endregion
            #region Irelia
            #endregion
            #region Janna
            #endregion
            #region JarvanIV
            #endregion
            #region Jax
            #endregion
            #region Jayce
            #endregion
            #region Jinx
            #endregion
            #region Kalista
            #endregion
            #region Karma
            #endregion
            #region Karthus
            #endregion
            #region Kassadin
            #endregion
            #region Katarina
            #endregion
            #region Kayle
            #endregion
            #region Kennen
            #endregion
            #region KhaZix
            #endregion
            #region Kindred
            #endregion
            #region KogMaw
            #endregion
            #region LeBlanc
            #endregion
            #region LeeSin
            #endregion
            #region Leona
            #endregion
            #region Lissandra
            #endregion
            #region Lucian
            #endregion
            #region Lulu
            #endregion
            #region Lux
            #endregion
            #region Malphite
            #endregion
            #region Malzahar
            #endregion
            #region Maokai
            #endregion
            #region MasterYi
            #endregion
            #region MissFortune
            #endregion
            #region Mordekaiser
            #endregion
            #region Morgana
            #endregion
            #region Nami
            #endregion
            #region Nasus
            #endregion
            #region Nautilus
            #endregion
            #region Nidalee
            #endregion
            #region Nocturne
            #endregion
            #region Nunu
            #endregion
            #region Olaf
            #endregion
            #region Orianna
            #endregion
            #region Pantheon
            #endregion
            #region Poppy
            #endregion
            #region Quinn
            #endregion
            #region Rammus
            #endregion
            #region RekSai
            #endregion
            #region Renekton
            #endregion
            #region Rengar
            #endregion
            #region Riven
            #endregion
            #region Rumble
            #endregion
            #region Ryze
            #endregion
            #region Sejuani
            #endregion
            #region Shaco
            #endregion
            #region Shen
            #endregion
            #region Shyvana
            #endregion
            #region Singed
            #endregion
            #region Sion
            #endregion
            #region Sivir
            #endregion
            #region Skarner
            #endregion
            #region Sona
            #endregion
            #region Soraka
            #endregion
            #region Swain
            #endregion
            #region Syndra
            #endregion
            #region TahmKench
            #endregion
            #region Talon
            #endregion
            #region Taric
            #endregion
            #region Teemo
            #endregion
            #region Thresh
            #endregion
            #region Tristana
            #endregion
            #region Trundle
            #endregion
            #region Tryndamere
            #endregion
            #region TwistedFate
            #endregion
            #region Twitch
            #endregion
            #region Udyr
            #endregion
            #region Urgot
            new SpellInfo()
            {
                ChampionName = "Urgot",
                SpellName = "UrgotHeatseekingLineMissile",
                SkillshotType = SkillShotType.Linear,
                Slot = SpellSlot.Q,
                Range = 1000,
                Delay = 125,
                MissileSpeed = 1600,
                Width = 60,
                CollisionCount = 1
            }
            #endregion
            #region Varus
            #endregion
            #region Vayne
            #endregion
            #region Veigar
            #endregion
            #region VelKoz
            #endregion
            #region Vi
            #endregion
            #region Viktor
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
            #endregion
            #region XinZhao
            #endregion
            #region Yasuo
            #endregion
            #region Yorick
            #endregion
            #region Zac
            #endregion
            #region Zed
            #endregion
            #region Ziggs
            #endregion
            #region Zilean
            #endregion
            #region Zyra
            #endregion
        };

        public static void Initialize()
        {
            foreach (SpellInfo spell in SpellInfoList)
                SpellList.Add(spell);
        }
    }
}
