using System;
using System.Collections.Generic;
using System.Reflection;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Spells;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using System.Linq;
using System.IO;
using EloBuddy.Sandbox;

namespace UnsignedEvade
{
    class ParticleDatabase
    {
        public static List<ParticleInfo> SingedPoisonTrails = new List<ParticleInfo>();
        public static List<Tuple<AIHeroClient, Obj_AI_Base>> LeeSinQTargets = new List<Tuple<AIHeroClient, Obj_AI_Base>>();
        public static List<Tuple<AIHeroClient, Vector3>> GravesQRewind = new List<Tuple<AIHeroClient, Vector3>>();

        private static List<ParticleInfo> particleDatabase = new List<ParticleInfo>()
        {
            //Corki W 
            new ParticleInfo()
            {
                ParticleName = "Corki_Base_W_tar.troy",
                Length = 300,
                Width = 300,
                SpellType = ParticleInfo.SpellTypeInfo.Wall,
                otherNames = new List<string>()
                {
                    //big w
                    "Corki_Base_W_AoE_ground.troy",
                },
            },
            //Rumble R
            new ParticleInfo()
            {
                ParticleName = "rumble_base_r_burn.troy",
                Length = 300,
                Width = 300,
                SpellType = ParticleInfo.SpellTypeInfo.Wall,
            },
            //Ziggs E 
            new ParticleInfo()
            {
                ParticleName = "Ziggs_Base_E_placedMine.troy",
                Radius = 90f,
                SpellType = ParticleInfo.SpellTypeInfo.CircularSkillshot,
            },
            //Lux E 
            new ParticleInfo()
            {
                ParticleName = "Lux_Base_E_mis.troy",
                Radius = 350f,
                SpellType = ParticleInfo.SpellTypeInfo.CircularSkillshot,
            },
            //Karthus Q
            new ParticleInfo()
            {
                ParticleName = "Karthus_Base_Q_Point_red.troy",
                Radius = 160f,
                SpellType = ParticleInfo.SpellTypeInfo.CircularSkillshot,
            },
            //Karma Mantra-Q
            new ParticleInfo()
            {
                ParticleName = "Karma_Base_Q_impact_R_01.troy",
                Radius = 250f,
                SpellType = ParticleInfo.SpellTypeInfo.CircularSkillshot,
            },
            //Zilean Q on Enemy
            new ParticleInfo()
            {
                ParticleName = "Zilean_Base_Q_Attach_Enemy.troy",
                Radius = 350f,
                SpellType = ParticleInfo.SpellTypeInfo.CircularSkillshot,
            },
            //Yasuo Wind Wall
            new ParticleInfo()
            {
                ParticleName = "Yasuo_Base_W_windwall1.troy",
                Width = 1,
                Length = 200,
                SpellType = ParticleInfo.SpellTypeInfo.Wall,
            },
            //Zilean Q on Ground
            new ParticleInfo()
            {
                ParticleName = "Zilean_Base_Q_Ticking_Ground_Audio.troy",
                Radius = 350f,
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
            //Illaoi Tentacles
            new ParticleInfo()
            {
                ParticleName = "Illaoi_Base_Q_IndicatorBLU.troy",
                //"Illaoi_Base_Q_tentacle.troy",
                //"Illaoi_Base_Q_cas.troy",
                //"Illaoi_Base_Q_tarMinion.troy",
                Length = 800,
                Width = 250,
                Delay = 0.4f,
                SpellType = ParticleInfo.SpellTypeInfo.LinearSkillshot,
            },
            //Morgana W
            new ParticleInfo()
            {
                ParticleName = "Morgana_Base_W_Tar_red.troy",
                Radius = 280f,
                SpellType = ParticleInfo.SpellTypeInfo.CircularSkillshot,
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
