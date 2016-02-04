using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace UnsignedYasuo
{
    class YasuoFunctions
    {
        public static bool IsDashing
        {
            get
            {
                if (Program.E.State == SpellState.Surpressed
                    && !Program._Player.HasBuffOfType(BuffType.Suppression))
                    return true;
                else
                    return false;
            }
        }

        public enum AttackSpell
        {
            Q,
            E,
            EQ,
            R,
            AA,
            DashQ,
            Ignite,
            Hydra,
            BilgewaterCutlass
        }
        public enum Mode
        {
            Combo,
            LaneClear,
            Harass,
            PotionManager
        }

        public static Obj_AI_Base GetEnemy(GameObjectType type, AttackSpell spell, bool EUNDERTURRET = false)
        {
            float range = 0;
            if (spell == AttackSpell.E || spell == AttackSpell.EQ)
                range = Program.E.Range;
            else if (spell == AttackSpell.Q || spell == AttackSpell.DashQ)
                range = Program.Q.Range;
            else if (spell == AttackSpell.Ignite)
                range = Program.Ignite.Range;
            //is in sight range
            else if (spell == AttackSpell.R)
                range = Program.R.Range;
            else if (spell == AttackSpell.Hydra)
                range = 400;
            else if (spell == AttackSpell.BilgewaterCutlass)
                range = 550;
            else if (spell == AttackSpell.AA)
                range = _Player.GetAutoAttackRange();

            return ObjectManager.Get<Obj_AI_Base>().Where(a => a.IsEnemy
                && a.Type == type
                && a.IsInRange(_Player, range)
                && !a.IsDead
                && !a.IsInvulnerable
                &&
                ((AttackSpell.Q == spell && !IsDashing)
                || (AttackSpell.DashQ == spell && IsDashing)
                || (spell == AttackSpell.E && YasuoCalcs.ERequirements(a, EUNDERTURRET))
                || (spell == AttackSpell.EQ && YasuoCalcs.ERequirements(a, EUNDERTURRET) && a.IsInRange(YasuoCalcs.GetDashingEnd(a), Program.EQRange))
                || (AttackSpell.Q != spell && AttackSpell.E != spell && AttackSpell.EQ != spell && AttackSpell.DashQ != spell))
                && a.IsValidTarget(range)).OrderBy(a => a.HealthPercent).FirstOrDefault();
        }

        public static Obj_AI_Base GetEnemyKS(GameObjectType type, AttackSpell spell, bool EUNDERTURRET = false)
        {
            float range = 0;
            if (spell == AttackSpell.E || spell == AttackSpell.EQ)
                range = Program.E.Range;
            else if (spell == AttackSpell.Q)
                range = Program.Q.Range;
            else if (spell == AttackSpell.Ignite)
                range = Program.Ignite.Range;
            else if (spell == AttackSpell.AA)
                range = _Player.GetAutoAttackRange();

            return ObjectManager.Get<Obj_AI_Base>().Where(a => a.IsEnemy
                && a.Type == type
                && a.IsInRange(_Player, range)
                && !a.IsDead
                && !a.IsInvulnerable
                && a.IsValidTarget(range)
                &&
                ((spell == AttackSpell.Q && a.Health <= YasuoCalcs.Q(a) && !IsDashing) ||
                (spell == AttackSpell.E && a.Health <= YasuoCalcs.E(a) && YasuoCalcs.ERequirements(a, EUNDERTURRET)) ||
                (spell == AttackSpell.EQ && 
                        a.Health <= (YasuoCalcs.Q(a) + YasuoCalcs.E(a)) && 
                        YasuoCalcs.ERequirements(a, EUNDERTURRET) && 
                        a.IsInRange(YasuoCalcs.GetDashingEnd(a), Program.EQRange)) ||
                (spell == AttackSpell.Ignite && a.Health <= YasuoCalcs.Ignite(a)))).FirstOrDefault();
        }

        public static AIHeroClient _Player { get { return ObjectManager.Player; } }

        //complete
        public static void LastHit()
        {
            bool QCHECK = Program.LastHit["LHQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.LastHit["LHE"].Cast<CheckBox>().CurrentValue;
            bool EQCHECK = Program.LastHit["LHEQ"].Cast<CheckBox>().CurrentValue;
            bool EUNDERTURRET = Program.LastHit["LHEUT"].Cast<CheckBox>().CurrentValue;

            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();
            
            if (QCHECK && QREADY && !IsDashing)
            {
                Obj_AI_Minion minion = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.Q);

                CastQ(minion);
            }

            if (EQCHECK && EREADY && QREADY)
            {
                Obj_AI_Base enemy = GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.EQ, EUNDERTURRET);

                if (enemy != null)
                {
                    Program.E.Cast(enemy);
                    CastQ(enemy);
                }
            }

            if (ECHECK && EREADY)
            {
                Obj_AI_Base enemy = GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.E, EUNDERTURRET);

                if (enemy != null)
                    Program.E.Cast(enemy);
            }
        }

        //complete
        public static void LaneClear()
        {
            bool QCHECK = Program.LaneClear["LCQ"].Cast<CheckBox>().CurrentValue;
            bool Q3CHECK = Program.LaneClear["LC3Q"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.LaneClear["LCE"].Cast<CheckBox>().CurrentValue;
            bool EQCHECK = Program.LaneClear["LCEQ"].Cast<CheckBox>().CurrentValue;
            bool EUNDERTURRET = Program.LaneClear["LCEUT"].Cast<CheckBox>().CurrentValue;
            bool ELastHit = Program.LaneClear["LCELH"].Cast<CheckBox>().CurrentValue;
            bool ITEMSCHECK = Program.LaneClear["LCI"].Cast<CheckBox>().CurrentValue;
            
            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();


            if (ITEMSCHECK)
                UseItemsAndIgnite(Mode.LaneClear);


            if (QCHECK && QREADY && !IsDashing)
            {
                Obj_AI_Base target = GetEnemy(GameObjectType.obj_AI_Minion, AttackSpell.Q);

                if ((Program.Q.Range == 1000 && Q3CHECK) || Program.Q.Range == 475)
                    CastQ(target);
            }

            if (ECHECK && EREADY)
            {
                Obj_AI_Base target = null;

                if (ELastHit)
                    target = GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.E, EUNDERTURRET);
                else
                    target = GetEnemy(GameObjectType.obj_AI_Minion, AttackSpell.E, EUNDERTURRET);

                if (target != null)
                    Program.E.Cast(target);
            }

            if (!ELastHit && EREADY && QREADY && EQCHECK)
            {
                Obj_AI_Base target = GetEnemy(GameObjectType.obj_AI_Minion, AttackSpell.EQ, EUNDERTURRET);

                if (target != null)
                {
                    Program.E.Cast(target);
                    CastQ(target);
                }
            }
        }

        //complete
        public static void KS()
        {
            bool QCHECK = Program.KSMenu["KSQ"].Cast<CheckBox>().CurrentValue;
            bool Q3CHECK = Program.KSMenu["KS3Q"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.KSMenu["KSE"].Cast<CheckBox>().CurrentValue;
            bool EQCHECK = Program.KSMenu["KSEQ"].Cast<CheckBox>().CurrentValue;
            bool IgniteCheck = Program.KSMenu["KSI"].Cast<CheckBox>().CurrentValue;

            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();
            bool EUNDERTURRET = Program.KSMenu["KSEUT"].Cast<CheckBox>().CurrentValue;

            if (IgniteCheck && Program.Ignite != null && Program.Ignite.IsReady())
            {
                var igniteEnemy = GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.Ignite);

                if (igniteEnemy != null)
                    Program.Ignite.Cast(igniteEnemy);
            }

            //empowered q
            if (QREADY)
            {
                if (
                     (Program.Q.Range == 1000 && Q3CHECK)
                     ||
                     (Program.Q.Range == 475 && QCHECK)
                    )
                {
                    var enemy = GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.Q);

                    CastQ(enemy);
                }
            }

            if (EREADY && ECHECK)
            {
                var enemy = GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.E, EUNDERTURRET);

                if (enemy != null)
                    Program.E.Cast(enemy);
            }

            if (QREADY && EREADY && EQCHECK)
            {
                var enemy = GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.EQ, EUNDERTURRET);
                if (enemy != null && YasuoCalcs.ShouldEQ(enemy))
                {
                    Program.E.Cast(enemy);
                    CastQ(enemy);
                }
            }
        }

        //complete
        public static void Harrass()
        {
            bool QCHECK = Program.Harass["HQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.Harass["HE"].Cast<CheckBox>().CurrentValue;
            bool EQCHECK = Program.Harass["HEQ"].Cast<CheckBox>().CurrentValue;
            bool EUNDERTURRET = Program.Harass["HEUT"].Cast<CheckBox>().CurrentValue;
            bool ITEMSCHECK = Program.Harass["HI"].Cast<CheckBox>().CurrentValue;

            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();


            if (ITEMSCHECK)
                UseItemsAndIgnite(Mode.Harass);
            
            if (QCHECK && QREADY && !IsDashing)
            {
                Obj_AI_Base enemy = GetEnemy(GameObjectType.AIHeroClient, AttackSpell.Q);

                CastQ(enemy);
            }

            if (ECHECK && EREADY)
            {
                Obj_AI_Base target = GetEnemy(GameObjectType.AIHeroClient, AttackSpell.E, EUNDERTURRET);

                if (target != null)
                {
                    Program.E.Cast(target);
                    if (YasuoCalcs.GetDashingEnd(target).IsInRange(target, 375) && QREADY && EQCHECK)
                        CastQ(target);
                }
            }
        }

        //complete
        public static void Combo()
        {
            if (_Player.CountEnemiesInRange(Program.R.Range) >= 1)
            {
                #region variables
                bool QCHECK = Program.ComboMenu["CQ"].Cast<CheckBox>().CurrentValue;
                bool ECHECK = Program.ComboMenu["CE"].Cast<CheckBox>().CurrentValue;
                bool EQCHECK = Program.ComboMenu["CEQ"].Cast<CheckBox>().CurrentValue;
                bool RCHECK = Program.ComboMenu["CR"].Cast<CheckBox>().CurrentValue;
                bool ITEMSCHECK = Program.ComboMenu["CI"].Cast<CheckBox>().CurrentValue;
                bool UltIfAllEnemiesKU = Program.ComboMenu["UltAEIV"].Cast<CheckBox>().CurrentValue;
                bool UltIfHalfEnemiesKU = Program.ComboMenu["UltHEIV"].Cast<CheckBox>().CurrentValue;
                bool UltIf10Health = Program.ComboMenu["UltLH"].Cast<CheckBox>().CurrentValue;
                bool UltAtLastSecond = Program.ComboMenu["UltLS"].Cast<CheckBox>().CurrentValue;
                int UltEnemiesKnockedUp = Program.ComboMenu["UltREnemies"].Cast<Slider>().CurrentValue;

                bool QREADY = Program.Q.IsReady();
                bool EREADY = Program.E.IsReady();
                bool RREADY = Program.R.IsReady();
                bool EUNDERTURRET = Program.ComboMenu["CEUT"].Cast<CheckBox>().CurrentValue;
                #endregion

                if (ITEMSCHECK)
                    UseItemsAndIgnite(Mode.Combo);

                #region Q
                if (QCHECK && QREADY && !IsDashing)
                {
                    Obj_AI_Base enemy = GetEnemy(GameObjectType.AIHeroClient, AttackSpell.Q);

                    if (enemy != null)
                        CastQ(enemy);
                }
                #endregion

                #region R
                if (Program.R.IsReady())
                {
                    int enemiesKnockedUp = YasuoCalcs.GetNumEnemiesKnockedUp();
                    int enemiesInVision = _Player.CountEnemiesInRange(Program.R.Range);

                    if (UltIfAllEnemiesKU && enemiesKnockedUp >= enemiesInVision && enemiesInVision != 0)
                        CastR(UltAtLastSecond);
                    else if (UltIfHalfEnemiesKU && enemiesKnockedUp >= enemiesInVision / 2 && enemiesInVision != 0)
                        CastR(UltAtLastSecond);
                    else if (UltEnemiesKnockedUp != 0 && enemiesKnockedUp >= UltEnemiesKnockedUp)
                        CastR(UltAtLastSecond);
                    else if (UltIf10Health && _Player.HealthPercent <= 10 && enemiesKnockedUp >= 1)
                        CastR(UltAtLastSecond);
                }
                #endregion
                
                #region E
                //if e is ready and menu allows for it to be used
                if (ECHECK && EREADY)
                {
                    Obj_AI_Base enemy = GetEnemy(GameObjectType.AIHeroClient, AttackSpell.E, EUNDERTURRET);
                    Obj_AI_Base EnemyToDashCloserToChampion = YasuoCalcs.GetBestDashEnemyToChampionWithinAARange(enemy, EUNDERTURRET);
                    //if number of enemies in Auto Attack range is 0, but there are champions yas could dash to, find 
                    if (_Player.CountEnemiesInRange(_Player.GetAutoAttackRange()) == 0
                        && YasuoCalcs.GetEnemyHeroesInRange(Program.E.Range) >= 1
                        && EnemyToDashCloserToChampion != null)
                        Program.E.Cast(EnemyToDashCloserToChampion);
                    //if there isnt any champions to auto, and no enemies will bring you into auto attack range, E to enemy
                    else if(_Player.CountEnemiesInRange(_Player.GetAutoAttackRange()) == 0)
                    {

                        //enemy in range
                        if (enemy != null)
                        {
                            //if can auto attack, don't e, instead auto attack
                            //if e'ing gets player in auto attack range. e
                            if (!_Player.IsInAutoAttackRange(enemy)
                                && YasuoCalcs.GetDashingEnd(enemy).IsInRange(enemy, _Player.GetAutoAttackRange()))
                            {
                                Program.E.Cast(enemy);

                                if (YasuoCalcs.GetDashingEnd(enemy).IsInRange(enemy, Program.EQRange) && EQCHECK && QREADY)
                                    CastQ(enemy);
                            }
                        }
                        //no enemy in e range, dash to minions to get closer
                        else
                        {
                            //R range is same as Vision Range
                            enemy = GetEnemy(GameObjectType.AIHeroClient, AttackSpell.R);

                            //if enemy in sight range, this is a double check
                            if (enemy != null)
                            {
                                Obj_AI_Base dashEnemy = YasuoCalcs.GetBestDashMinionToChampion(enemy, EUNDERTURRET);

                                //there is something to dash too
                                if (dashEnemy != null)
                                {
                                    Program.E.Cast(dashEnemy);
                                    if (YasuoCalcs.GetDashingEnd(dashEnemy).IsInRange(enemy, Program.EQRange) && QREADY && EQCHECK)
                                        CastQ(dashEnemy);
                                }
                            }
                        }
                    }
                }
                #endregion

            }
        }

        //complete
        public static void Flee()
        {
            if (Program.E.IsReady() && !IsDashing)
            {
                Obj_AI_Base fleeObject = ObjectManager.Get<Obj_AI_Base>().Where(a =>
                    !a.IsDead &&
                    a.IsEnemy &&
                    YasuoCalcs.GetDashingEnd(a).Distance(Game.CursorPos) <= _Player.Distance(Game.CursorPos) &&
                    !a.HasBuff("YasuoDashWrapper") &&
                    a.IsInRange(_Player, Program.E.Range)).OrderBy(a => a.Distance(Game.CursorPos)).FirstOrDefault();

                if (fleeObject != null)
                {
                    int angle1 = YasuoCalcs.RadiansToDegrees(_Player.Position.AngleBetween(YasuoCalcs.GetDashingEnd(fleeObject)));
                    int angle2 = YasuoCalcs.RadiansToDegrees(_Player.Position.AngleBetween(Game.CursorPos));

                    //angle1 = 20 angle 2 = 40
                    if (Math.Abs(angle1) - Math.Abs(angle2) >= 20 ||
                        Math.Abs(angle2) - Math.Abs(angle1) >= 20)
                        Program.E.Cast(fleeObject);
                }
            }
        }

        //complete
        public static void UseItemsAndIgnite(Mode mode)
        {
            InventorySlot[] items = _Player.InventoryItems;
            GameObjectType type = GameObjectType.AIHeroClient;
            if (mode == Mode.LaneClear)
                type = GameObjectType.obj_AI_Minion;

            foreach (InventorySlot item in items)
            {
                if (item.CanUseItem())
                {
                    bool useTiamat = Program.Items.Get<CheckBox>("ItemsT").CurrentValue;
                    bool useRavenous = Program.Items.Get<CheckBox>("ItemsRH").CurrentValue;
                    bool useTitanic = Program.Items.Get<CheckBox>("ItemsTH").CurrentValue;
                    bool useCutlass = Program.Items.Get<CheckBox>("ItemsBC").CurrentValue;
                    bool useYoumuus = Program.Items.Get<CheckBox>("ItemsY").CurrentValue;
                    bool useBORK = Program.Items.Get<CheckBox>("ItemsBORK").CurrentValue;
                    bool useQSS = Program.Items.Get<CheckBox>("ItemsQSS").CurrentValue;
                    bool useMercScim = Program.Items.Get<CheckBox>("ItemsMS").CurrentValue;
                    bool QSSBlind = Program.Items.Get<CheckBox>("QSSBlind").CurrentValue;
                    bool QSSCharm = Program.Items.Get<CheckBox>("QSSCharm").CurrentValue;
                    bool QSSFear = Program.Items.Get<CheckBox>("QSSFear").CurrentValue;
                    bool QSSKB = Program.Items.Get<CheckBox>("QSSKB").CurrentValue;
                    bool QSSSilence = Program.Items.Get<CheckBox>("QSSSilence").CurrentValue;
                    bool QSSSlow = Program.Items.Get<CheckBox>("QSSSlow").CurrentValue;
                    bool QSSSnare = Program.Items.Get<CheckBox>("QSSSnare").CurrentValue;
                    bool QSSStun = Program.Items.Get<CheckBox>("QSSStun").CurrentValue;
                    bool QSSTaunt = Program.Items.Get<CheckBox>("QSSTaunt").CurrentValue;
                    bool usePotions = Program.Items.Get<CheckBox>("ItemsPotions").CurrentValue;
                    int PotionPercent = Program.Items.Get<Slider>("PotSlider").CurrentValue;

                    if (((item.Id == ItemId.Blade_of_the_Ruined_King && useBORK) 
                        || (item.Id == ItemId.Bilgewater_Cutlass && useCutlass)) &&
                        (mode == Mode.Combo || mode == Mode.Harass))
                    {
                        var enemy = GetEnemy(type, AttackSpell.BilgewaterCutlass);

                        if (enemy != null)
                            item.Cast(enemy);
                    }

                    if ((item.Id == ItemId.Tiamat_Melee_Only && useTiamat)
                        || (item.Id == ItemId.Ravenous_Hydra_Melee_Only && useRavenous)
                        || (item.Id == ItemId.Titanic_Hydra && useTitanic))
                    {
                        var enemy = GetEnemy(type, AttackSpell.Hydra);

                        if (enemy != null)
                            item.Cast();
                    }

                    if ((item.Id == ItemId.Youmuus_Ghostblade && useYoumuus)
                        && (mode == Mode.Combo || mode == Mode.Harass)
                        && _Player.CountEnemiesInRange(Program.Q.Range) >= 1)
                        item.Cast();

                    if (((item.Id == ItemId.Quicksilver_Sash && useQSS)
                        || (item.Id == ItemId.Mercurial_Scimitar && useMercScim))
                        && 
                        ((_Player.HasBuffOfType(BuffType.Blind) && QSSBlind)
                        || (_Player.HasBuffOfType(BuffType.Charm) && QSSCharm)
                        || (_Player.HasBuffOfType(BuffType.Fear) && QSSFear)
                        || (_Player.HasBuffOfType(BuffType.Knockback) && QSSKB)
                        //not standing on raka silence
                        || (_Player.HasBuffOfType(BuffType.Silence) && QSSSilence && !_Player.HasBuff("sorakaepacify"))
                        || (_Player.HasBuffOfType(BuffType.Slow) && QSSSlow)
                        || (_Player.HasBuffOfType(BuffType.Snare) && QSSSnare)
                        || (_Player.HasBuffOfType(BuffType.Stun) && QSSStun)
                        || (_Player.HasBuffOfType(BuffType.Taunt) && QSSTaunt))
                        //not being knocked back by dragon
                        && !_Player.HasBuff("moveawaycollision"))
                        item.Cast();
                    if((item.Id == ItemId.Health_Potion || item.Id == ItemId.Refillable_Potion || item.Id == ItemId.Hunters_Potion || item.Id == ItemId.Corrupting_Potion)
                        &&
                        usePotions &&
                        _Player.HealthPercent <= PotionPercent &&
                        !_Player.HasBuff("RegenerationPotion") &&
                        !_Player.HasBuff("ItemCrystalFlask") &&
                        !_Player.HasBuff("ItemCrystalFlaskJungle") &&
                        !_Player.HasBuff("ItemDarkCrystalFlask"))
                        item.Cast();
                }
            }
        }
        
        //complete
        public static void AutoHarrass()
        {
            if (_Player.IsUnderEnemyturret())
                return;

            bool QCHECK = Program.Harass["AHQ"].Cast<CheckBox>().CurrentValue;
            bool Q3CHECK = Program.Harass["AH3Q"].Cast<CheckBox>().CurrentValue;
            var QRange = Program.Q.Range;
            if ((QRange == 1000 && Q3CHECK) || (QRange == 475 && QCHECK) && !IsDashing)
            {
                var enemy = GetEnemy(GameObjectType.AIHeroClient, AttackSpell.Q);

                CastQ(enemy);
            }
        }

        public static Spell.Skillshot GetQType()
        {
            if (_Player.HasBuff("yasuoq3w") && !IsDashing)
                return new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear)
                {
                    Width = 55,
                    CastDelay = 400,
                    Speed = int.MaxValue,
                    AllowedCollisionCount = int.MaxValue
                };

            else if (IsDashing)
                return new Spell.Skillshot(SpellSlot.Q, 375, SkillShotType.Circular);
            else
                return new Spell.Skillshot(SpellSlot.Q, 475, SkillShotType.Linear)
                {
                    CastDelay = 500,
                    Width = 90,
                    Speed = 1500,
                    AllowedCollisionCount = int.MaxValue
                };
        }
        
        public static void CastQ(Obj_AI_Base target)
        {
            if (!Program.Q.IsReady() || target == null || target.Position == Vector3.Zero)
                return;

            if (IsDashing)
                Program.Q.Cast(target.Position);
            else if (Program.Q.GetPrediction(target).HitChance >= Program.QHitChance)
                Program.Q.Cast(target.Position);
            else if (Program.QHitChance == HitChance.Unknown)
                Program.Q.Cast(target.Position);
        }
        public static void CastR(bool UltAtLastSecond)
        {
            if (!Program.R.IsReady())
                return;

            if (UltAtLastSecond)
            {
                List<AIHeroClient> Enemies = YasuoCalcs.GetEnemiesKnockedUp();
                AIHeroClient LowestKnockUpTime = Enemies.OrderBy(a => a.Buffs.OrderBy(b => b.EndTime).FirstOrDefault().EndTime).FirstOrDefault();
                if (YasuoCalcs.IsLastKnockUpSecond(LowestKnockUpTime))
                    Program.R.Cast();
            }
            else
                Program.R.Cast();
        }
    }
}
