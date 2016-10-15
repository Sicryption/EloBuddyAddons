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
        public static List<Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>>> newProjectiles = new List<Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>>>();

        //change some SelfActives to PassiveActive
        //Self Active is a circular spell that comes off the champions position
        //Passive Active is an effect that happens to a champion (Leona Q, Aatrox w)
        #region Existing Spell List
        static List<SpellInfo> SpellInfoList = new List<SpellInfo>()
        {
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Amumu
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Entangle,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Anivia
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
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Annie
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Pull,
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
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Silence,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Brand
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                CCtype = SpellInfo.CrowdControlType.Slow,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.Silence,
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
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Corki
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
                CCtype = SpellInfo.CrowdControlType.None,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Draven
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
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
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Illaoi
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Root,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //do jayce
            #region Jayce

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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
            //do karma
            #region Karma

            #endregion
            #region Karthus
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
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.Slow,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //w
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.Nearsight,
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
            #region Kayle
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Kennen
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
            //do kled
            #region Kled

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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
            #endregion=
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
            #region LeeSin
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
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Leona
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Root,
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Root,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
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
            //do nidalee
            #region Nidalee

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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
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
                CCtype = SpellInfo.CrowdControlType.knock,
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
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Nearsight,
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
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            //the shards when leaving ult form
            new SpellInfo()
            {
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
            //do reksai
            #region RekSai

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
            //do rengar
            #region Rengar

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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
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
            //left off here
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
        #endregion

        public static bool HasSpell(MissileClient client)
        {
            foreach (SpellInfo info in SpellInfoList)
                if ((info.MissileName == client.SData.Name || (info.MissileName.ToLower().Contains("basicattack") && client.SData.Name.ToLower().Contains("basicattack"))
                    && info.ChampionName == client.SpellCaster.BaseSkinName))
                    return true;
            return false;
        }

        public static bool HasSpell(Obj_AI_Base owner, GameObjectProcessSpellCastEventArgs args)
        {
            foreach (SpellInfo info in SpellInfoList)
                if (info.ChampionName == owner.BaseSkinName
                    && (info.SpellName == args.SData.Name || (info.SpellName.ToLower().Contains("basicattack") && args.SData.Name.ToLower().Contains("basicattack"))))
                    return true;
            return false;
        }

        public static Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>> GetNewProjectileListing(string spellCaster, Vector3 position)
        {
            foreach (Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>> ob in newProjectiles)
            {
                if (ob.Item1 != null)
                {
                    if (ob.Item1.SpellCaster.BaseSkinName == spellCaster
                        && ob.Item1.StartPosition == position)
                        return ob;
                }
                else if (ob.Item2 != null)
                    if (ob.Item2.Item1.BaseSkinName == spellCaster
                        && ob.Item2.Item2.Start == position)
                        return ob;
            }
            return null;
        }

        public static SpellInfo GetSpellInfo(MissileClient client)
        {
            foreach (SpellInfo info in SpellInfoList)
                if (info.MissileName == client.SData.Name
                    && info.ChampionName == client.SpellCaster.BaseSkinName)
                    return info;
            return null;
        }

        public static void PrintOutNewProjectileInformation(Tuple<MissileClient, Tuple<Obj_AI_Base, GameObjectProcessSpellCastEventArgs>> information)
        {
            MissileClient projectile = information.Item1;
            Obj_AI_Base sender = information.Item2.Item1;
            GameObjectProcessSpellCastEventArgs SpellCastInformation = information.Item2.Item2;

            if (projectile == null || sender == null || SpellCastInformation == null)
                //the cause of this is because the spell is already added to the NewSpellList
                return;
            else
            {
                string message = "";
                message += "new SpellInfo()\r\n{";
                message += "\r\nChampionName = \"" + sender.BaseSkinName + "\"";
                message += ",\r\nMissileName = \"" + projectile.SData.Name + "\"";
                message += ",\r\nSpellName = \"" + SpellCastInformation.SData.Name + "\"";

                if (projectile.SData.Name.ToLower().Contains("basicattack"))
                {
                    //this is an auto attack
                    message += ",\r\nSlot = SpellInfo.SpellSlot.Auto";
                    message += ",\r\nRange = " + sender.GetAutoAttackRange();
                }
                else
                {
                    //this is a spell
                    message += ",\r\nSlot = SpellInfo.SpellSlot." + SpellCastInformation.Slot;
                    message += ",\r\nRange = " + projectile.SData.CastRange;
                }
                message += ",\r\nMissileSpeed = " + projectile.SData.MissileSpeed;
                if (projectile.SData.MissileMaxSpeed == 3.402823E+38)
                    message += ",\r\nMissileMaxSpeed = " + int.MaxValue;
                else
                    message += ",\r\nMissileMaxSpeed = " + projectile.SData.MissileMaxSpeed;
                if (projectile.SData.MissileMinSpeed == 3.402823E+38)
                    message += ",\r\nMissileMinSpeed = " + int.MaxValue;
                else
                    message += ",\r\nMissileMinSpeed = " + projectile.SData.MissileMinSpeed;
                message += ",\r\nWidth = " + projectile.SData.LineWidth;
                message += ",\r\nType = " + projectile.SData.CastType;
                message += ",\r\nDelay = " + projectile.SData.SpellCastTime;
                message += ",\r\nSpellType = SpellInfo.SpellTypeInfo.";
                message += ",\r\n},";

                Console.WriteLine(message);
            }
        }

        public static void Initialize()
        {
            foreach (SpellInfo spell in SpellInfoList)
                SpellList.Add(spell);
        }
    }
}
