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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Draven
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            //add graves auto
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add longrange q
            #region Jayce
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q ranged
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
            //w ranged
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
                CCtype = SpellInfo.CrowdControlType.KnockAside,
                Slot = SpellInfo.SpellSlot.E,
            },
            //e ranged
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Wall,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
            #region Karma
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //rq
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
                CCtype = SpellInfo.CrowdControlType.Root,
                Slot = SpellInfo.SpellSlot.W,
            },
            //rw
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Root,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.E,
            },
            //re
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            #region Kled
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q skarrl
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
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
            //w skarrl
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e skarrl
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
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
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
            //R
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R
            },
            #endregion
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
            //q2
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
            //w2
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
            #region Nidalee
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q tiger
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
            //w tiger
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
            //e tiger
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
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
                CCtype = SpellInfo.CrowdControlType.KnockUp,
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
            //add reksai unburrow
            #region RekSai
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q burrowed
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
            //w burrowed
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
            //e burrowed
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
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
            #region Rengar
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q emp
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            //w emp
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
            //e emp
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
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
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
            },
            //q drag
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            //w drag
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
            //e drag
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
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
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
            #region Sivir
            //q
            new SpellInfo()
            {
                ChampionName = "Sivir",
                MissileName = "SivirQMissile",
                SpellName = "SivirQ",
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.Q,
                Range = 1250,
                MissileSpeed = 1350,
                MissileMaxSpeed = 1350,
                MissileMinSpeed = 1350,
                Width = 90,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Skarner
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
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Suppression,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Sona
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
            #region Soraka
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
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Silence,
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
            #region Swain
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Syndra
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
                SpellType = SpellInfo.SpellTypeInfo.ConeSkillshot,
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
            #region TahmKench
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
                CCtype = SpellInfo.CrowdControlType.Suppression,
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
            #region Talon
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
                CCtype = SpellInfo.CrowdControlType.Slow,
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
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Taric
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Teemo
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Blind,
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Thresh
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
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
                SpellType = SpellInfo.SpellTypeInfo.Wall,
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Tristana
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
                CCtype = SpellInfo.CrowdControlType.Slow,
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
                CCtype = SpellInfo.CrowdControlType.KnockBack,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Trundle
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
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
            #region Tryndamere
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
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
            #region TwistedFate
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
            #region Twitch
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
                CCtype = SpellInfo.CrowdControlType.Slow,
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
            //udyr bear stance auto
            #region Udyr
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
            //add urgot lock on q
            #region Urgot
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
            #region Varus
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Vayne
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
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
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Veigar
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add q split
            #region VelKoz
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
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
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
            #region Vi
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Viktor
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Vladimir
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
                CCtype = SpellInfo.CrowdControlType.Slow,
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
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Volibear
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
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
            #region Warwick
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Suppression,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Wukong
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
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Xerath
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
                CCtype = SpellInfo.CrowdControlType.Slow,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.LinearSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
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
            #region XinZhao
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.PassiveActive,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.Slow,
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
            //add eq, q3, eq3
            #region Yasuo
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
                SpellType = SpellInfo.SpellTypeInfo.Wall,
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
                CCtype = SpellInfo.CrowdControlType.Suspension,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Yorick
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
            #region Zac
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.E,
            },
            //r
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.SelfActive,
                CCtype = SpellInfo.CrowdControlType.KnockUp,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            #region Zed
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
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
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
            #region Ziggs
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
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.W,
            },
            //e
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.KnockBack,
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
            #region Zilean
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.CircularSkillshot,
                CCtype = SpellInfo.CrowdControlType.Stun,
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
                SpellType = SpellInfo.SpellTypeInfo.Targeted,
                CCtype = SpellInfo.CrowdControlType.None,
                Slot = SpellInfo.SpellSlot.R,
            },
            #endregion
            //add zyra plants
            #region Zyra
            //q
            new SpellInfo()
            {
                SpellType = SpellInfo.SpellTypeInfo.Wall,
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
                CCtype = SpellInfo.CrowdControlType.Root,
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
        };
        #endregion

        public static SpellInfo GetSpellInfo(MissileClient client)
        {
            foreach (SpellInfo info in SpellInfoList)
                if (info.MissileName == client.SData.Name
                    && info.ChampionName == client.SpellCaster.BaseSkinName)
                    return info;
            return null;
        }
        
        public static void Initialize()
        {
            foreach (SpellInfo spell in SpellInfoList)
                SpellList.Add(spell);
        }
    }
}
