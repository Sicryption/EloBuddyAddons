using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsignedEvade
{
    class Utilities
    {
        public static void ResetPassiveSpellCounter()
        {
            for (int i = 0; i < SpellDatabase.championSpellsDrawnOnChampion.Count; i++)
                SpellDatabase.championSpellsDrawnOnChampion[i] = new Tuple<string, int>(SpellDatabase.championSpellsDrawnOnChampion[i].Item1, 0);
        }
    }
}
