using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UnsignedRengar
{
    class ModeHandler
    {
        public static AIHeroClient Rengar;
        public static bool hasDoneActionThisTick = false;

        public static void Combo()
        {
            int QHitNumber = 0;
            Vector3 QPos = Program.Q.GetBestConeAndLinearCastPosition(Program.Q2, EntityManager.MinionsAndMonsters.Monsters.ToList().ToObj_AI_BaseList(), Rengar.Position, out QHitNumber);
            
            if (QPos != Vector3.Zero && QHitNumber >= 1)
            {
                if (MenuHandler.Combo.GetCheckboxValue("Use Q") && Rengar.Ferocity() < 4)
                    CastQ(QPos);
                if (MenuHandler.Combo.GetCheckboxValue("Use Empowered Q") && Rengar.Ferocity() == 4)
                    CastQ(Rengar.Position.Extend(QPos, Program.Q.Range).To3D());
                Chat.Print(QHitNumber + "|" + QPos + "|" + QPos.Distance(Rengar));
            }

            if (MenuHandler.Combo.GetCheckboxValue("Use W") && Rengar.Ferocity() < 4)
            {

            }
        }

        public static void CastQ(Vector3 pos)
        {
            if (Program.Q.IsReady())
                hasDoneActionThisTick = Program.Q.Cast(pos);
        }

        public static void CastQ(GameObject enemy)
        {
            if (Program.Q.IsReady())
                hasDoneActionThisTick = Program.Q.Cast(enemy.Position);
        }
    }
}
