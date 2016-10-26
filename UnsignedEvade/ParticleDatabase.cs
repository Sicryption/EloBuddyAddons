using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsignedEvade
{
    class ParticleDatabase
    {
        private static List<ParticleInfo> particleDatabase = new List<ParticleInfo>()
        {
            //Corki W 
            new ParticleInfo()
            {
                ParticleName = "Corki_Base_W_tar.troy",
                Length = 300,
                Width = 300,
                XOffset = -25,
                YOffset = 100,
                SpellType = ParticleInfo.SpellTypeInfo.Wall,
                otherNames = new List<string>()
                {
                    //big w
                    "Corki_Base_W_AoE_ground.troy",
                },
            },
            //Ziggs E 
            new ParticleInfo()
            {
                ParticleName = "Ziggs_Base_E_placedMine.troy",
                Radius = 90f,
                SpellType = ParticleInfo.SpellTypeInfo.CircularSkillshot,
            },
            //Karma Mantra-Q
            new ParticleInfo()
            {
                ParticleName = "Karma_Base_Q_impact_R_01.troy",
                Radius = 250f,
                SpellType = ParticleInfo.SpellTypeInfo.CircularSkillshot,
            },
            //Anivia W
            new ParticleInfo()
            {
                ParticleName = "cryo_Crystalize.troy",
                Length = 150,
                Width = 150,
                XOffset = 0,
                YOffset = 0,
                SpellType = ParticleInfo.SpellTypeInfo.Wall,
            },
            //Anivia R
            new ParticleInfo()
            {
                ParticleName = "cryo_storm_red_team.troy",
                Radius = 400f,
                SpellType = ParticleInfo.SpellTypeInfo.CircularSkillshot,
                otherNames = new List<string>() { "cryo_storm_blue_team.troy", }
            },
            //Illaoi Q
            new ParticleInfo()
            {
                ParticleName = "Illaoi_Base_Q_IndicatorRed.troy",
                //"Illaoi_Base_Q_tentacle.troy",
                //"Illaoi_Base_Q_cas.troy",
                //"Illaoi_Base_Q_tarMinion.troy",
                Length = 1000,
                Width = 400,
                XOffset = 0,
                YOffset = 0,
                SpellType = ParticleInfo.SpellTypeInfo.Wall,
                otherNames = new List<string>() { "cryo_storm_blue_team.troy", }
            },
        };

        public static ParticleInfo GetParticleInfo(string name)
        {
            foreach (ParticleInfo info in particleDatabase)
                if (info.ParticleName == name)
                    return info;
            return null;
        }
    }
}
