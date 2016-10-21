using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using System.Collections.Generic;
using System;
using SharpDX;

namespace UnsignedEvade
{
    class SpellInfo
    {
        public float Delay = 0.25f,
            MissileMaxSpeed,
            MissileMinSpeed,
            MissileSpeed,
            Radius,
            SecondRadius,
            Width = 10,
            Range,
            CollisionCount = int.MaxValue,
            ConeDegrees,
            TimeOfCast;
        private float travelTime = 0;
        public string ChampionName,
            MissileName,
            SpellName,
            BuffName = "";
        public bool canVaryInLength = false;
        public string[] OtherMissileNames = new string[0];
        public SpellSlot Slot = SpellSlot.None;
        public SpellTypeInfo SpellType = SpellTypeInfo.None;
        public CrowdControlType CCtype = CrowdControlType.None;
        public SpellCreationLocation CreationType = SpellCreationLocation.None;
        public Dashtype DashType = Dashtype.None;

        public Vector3 startPosition = Vector3.Zero,
            endPosition = Vector3.Zero;
        public GameObject target = null;
        public Obj_AI_Base caster = null;
        public MissileClient missile = null;
        public List<Vector3> GetPath()
        {
            List<Vector3> temp = new List<Vector3>();

            //animation
            if (CreationType == SpellCreationLocation.OnProcessSpell)
            {

            }
            //spell cast
            else if (CreationType == SpellCreationLocation.OnSpellCast)
            {

            }
            //missile/projectile
            else if (CreationType == SpellCreationLocation.OnObjectCreate)
            {
                //if missile is targetting me
                //if (missile.Target.Name == Player.Instance.Name)
                {
                    float slope = (endPosition.Y - startPosition.Y) / (endPosition.X - startPosition.X);
                    float yInt = -(slope * startPosition.X) + startPosition.Y;
                    float startX = (startPosition.X > endPosition.X) ? endPosition.X : startPosition.X;
                    float endX = (startPosition.X > endPosition.X) ? startPosition.X : endPosition.X;

                    //using point-slope form create a linear line and get all vectors in between. Each vector will be 1 x 
                    for (int i = (int)startX; i < endX; i++)
                        temp.Add(new Vector3(i, (slope * i) + yInt, 0));
                }
            }

            return temp;
        }

        public float TravelTime
        {
            get
            {
                if (travelTime == -1f || MissileSpeed == 0)
                    return 0f;
                else if(travelTime == 0)
                    travelTime = startPosition.Distance(endPosition) / MissileSpeed;
                return travelTime;
            }
            set
            {
                travelTime = value;
            }
        }
        
        public enum SpellCreationLocation
        {
            OnSpellCast,
            OnObjectCreate,
            OnProcessSpell,
            None
        }

        public enum Dashtype
        {
            Linear,
            Blink,
            None
        }

        public enum SpellTypeInfo
        {
            CircularSkillshot,
            LinearSkillshot,
            ArcSkillshot,
            SelfActive,
            PassiveActive,
            Targeted,
            Wall,
            MovingWall,
            ConeSkillshot,
            None
        }
        public enum CrowdControlType
        {
            KnockAside,//Velkoz E/Dragon
            KnockUp,//Aatrox Q, Malphite Ult
            KnockBack,//Tristana R
            Pull,//Thresh Q/Blitz Q
            Blind,//Teemo Q
            Entangle,//Amumu R
            Polymorph,//Lulu
            Charm,//Ahri E
            Fear,//Fiddlesticks Q
            Taunt,//Rammus E/Galio R
            Ground,//Cassiopeia W
            Nearsight,//Quinn Q/ Graves E
            Root,//Cait E, Rengar E, Lux Q
            Silence,//Soraka E, Fidds E
            Slow,//Janna W
            Statis,//Aatrox Passive
            Stun,//Alistar Q, Xerath E, Riven W
            Suspension,//Yasuo R
            Suppression,//Malzahar R, Warwick R
            None
        }

        public enum SpellSlot
        {
            Q,
            W,
            E,
            R,
            Auto,
            None
        } 

        public EloBuddy.SpellSlot GetEBSpellSlot()
        {
            if (Slot == SpellSlot.Q)
                return EloBuddy.SpellSlot.Q;
            else if (Slot == SpellSlot.W)
                return EloBuddy.SpellSlot.W;
            else if (Slot == SpellSlot.E)
                return EloBuddy.SpellSlot.E;
            else if (Slot == SpellSlot.R)
                return EloBuddy.SpellSlot.R;
            else
                return EloBuddy.SpellSlot.Unknown;
        }
        public SpellDataInst GetChampionSpell()
        {
            return caster.Spellbook.GetSpell(GetEBSpellSlot());
        }
    }
}
