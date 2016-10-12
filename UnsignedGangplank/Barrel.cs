using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace UnsignedGP
{
    class Barrel
    {
        public Obj_AI_Base barrel;
        public float timeCreated;
        public float timeSinceLastDecay;
        public float decayRate
        {
            get
            {
                if (Player.Instance.Level >= 13)
                    return 0.5f;
                else if (Player.Instance.Level >= 7)
                    return 1f;
                else
                    return 2f;
            }
        }
        public float TimeAt1HP
        {
            get
            {
                if (barrel.Health == 1)
                    return timeCreated;
                else if (barrel.Health == 2)
                {
                    //no enemies hit it. 
                    if (timeSinceLastDecay != timeCreated)
                        return timeCreated + (decayRate * 2);
                    else
                        //enmies hit it 
                        return timeSinceLastDecay + decayRate;
                }
                else
                    return int.MaxValue;
            }
        }

        public Barrel(Obj_AI_Base obj, float createTime)
        {
            barrel = obj;
            timeCreated = createTime;
            timeSinceLastDecay = createTime;
        }
    }
}
