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
