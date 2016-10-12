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
        //E Animation Time = 0.5f seconds
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
            bool Q3CHECK = Program.LastHit["LHQ3"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.LastHit["LHE"].Cast<CheckBox>().CurrentValue;
            bool EQCHECK = Program.LastHit["LHEQ"].Cast<CheckBox>().CurrentValue;
            bool EUNDERTURRET = Program.LastHit["LHEUT"].Cast<CheckBox>().CurrentValue;

            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();

            if (QCHECK && QREADY && !IsDashing)
            {
                Obj_AI_Base target = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.Q);

                if ((Program.Q.Range == 1000 && Q3CHECK) || Program.Q.Range == 475)
                    CastQ(target, false, true);
            }

            if (EQCHECK && EREADY && YasuoCalcs.WillQBeReady())
            {
                Obj_AI_Base enemy = GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.EQ, EUNDERTURRET);

                if (enemy != null && YasuoCalcs.ShouldEQ(enemy))
                {
                    Program.E.Cast(enemy);
                    CastQ(enemy, true, true);
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

                if (Program.Q.Range == 475)
                    CastQ(target);
                else if (Program.Q.Range == 1000 && Q3CHECK)
                {
                    Vector3 CastPosition = Program.Q.GetBestLinearCastPosition(
                        ObjectManager.Get<Obj_AI_Base>().Where(a => a.IsEnemy && !a.IsDead && a.Distance(_Player) <= Program.Q.Range),
                        0,
                        _Player.Position.To2D()).CastPosition;

                    if (CastPosition != null)
                        Program.Q.Cast(CastPosition);
                }
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

            if (!ELastHit && EREADY && YasuoCalcs.WillQBeReady() && EQCHECK)
            {
                Obj_AI_Base target = GetEnemy(GameObjectType.obj_AI_Minion, AttackSpell.EQ, EUNDERTURRET);

                if (target != null)
                {
                    Program.E.Cast(target);
                    CastQ(target, true);
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

            if (EREADY && EQCHECK)
            {
                var enemy = GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.EQ, EUNDERTURRET);
                if (enemy != null && YasuoCalcs.ShouldEQ(enemy) && YasuoCalcs.WillQBeReady())
                {
                    Program.E.Cast(enemy);
                    CastQ(enemy, true);
                }
            }
        }

        public enum Status
        {
            Q,
            Q3,
            E,
            EQBeforeQ,
            EQAfterQ,
            E3QBeforeQ,
            E3QAfterQ,
            Standing
        }
        public static Status YasuoStatus = Status.Standing;
        public static float StartEQTime = 0;

        public static float curTime = 0;

        public static void HandleStatus()
        {
            if ((YasuoStatus == Status.Q || YasuoStatus == Status.Q3) && Program.Q.IsOnCooldown)
                YasuoStatus = Status.Standing;
            else if ((YasuoStatus == Status.E 
                || YasuoStatus == Status.EQBeforeQ 
                || YasuoStatus == Status.E3QBeforeQ
                || YasuoStatus == Status.E3QAfterQ
                || YasuoStatus == Status.EQAfterQ) 
                && Program.E.State != SpellState.Surpressed)
                YasuoStatus = Status.Standing;

            foreach (var propertyInfo in Player.Instance.GetType().GetProperties().Where(x => !x.Name.Contains("_")))
            {
                Console.WriteLine($"Name : {propertyInfo.Name} | Value : {propertyInfo.GetValue(Player.Instance)}");
            }
        }
        
        public static void HarrassUpdate()
        {

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
                    if (YasuoCalcs.GetDashingEnd(target).IsInRange(target, 375) && EQCHECK && YasuoCalcs.WillQBeReady())
                        CastQ(target, true);
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
                bool ITEMSCHECK = Program.ComboMenu["CI"].Cast<CheckBox>().CurrentValue;

                bool QREADY = Program.Q.IsReady();
                bool EREADY = Program.E.IsReady();
                bool RREADY = Program.R.IsReady();
                bool EUNDERTURRET = Program.ComboMenu["CEUT"].Cast<CheckBox>().CurrentValue;
                #endregion

                if (ITEMSCHECK)
                    UseItemsAndIgnite(Mode.Combo);
                
                if (Program.ComboMenu["CBB"].Cast<CheckBox>().CurrentValue)
                    Beyblade();

                if (Program.R.IsReady() && Program.UltMenu["Ult"].Cast<CheckBox>().CurrentValue)
                    YasuoFunctions.UltHandler();

                #region Q
                if (QCHECK && QREADY && !IsDashing)
                {
                    Obj_AI_Base enemy = GetEnemy(GameObjectType.AIHeroClient, AttackSpell.Q);

                    if (enemy != null)
                        CastQ(enemy);
                }
                #endregion

                #region E
                //if e is ready and menu allows for it to be used
                if (ECHECK && EREADY)
                {
                    Obj_AI_Base enemy = GetEnemy(GameObjectType.AIHeroClient, AttackSpell.E, EUNDERTURRET);
                    Obj_AI_Base EnemyToDashCloserToChampion = YasuoCalcs.GetBestDashEnemyToChampionWithinAARange(enemy, EUNDERTURRET);

                    //if number of enemies in Auto Attack range is 0, but there are champions yas could dash to, find best enemy to dash to
                    if (_Player.CountEnemiesInRange(_Player.GetAutoAttackRange()) == 0
                        && YasuoCalcs.GetEnemyHeroesInRange(Program.E.Range) >= 1
                        && EnemyToDashCloserToChampion != null)
                        Program.E.Cast(EnemyToDashCloserToChampion);
                    //if there isnt any champions to auto, and no enemies will bring you into auto attack range, E to enemy
                    else if (_Player.CountEnemiesInRange(_Player.GetAutoAttackRange()) == 0)
                    {

                        //enemy in range
                        if (enemy != null)
                        {
                            //if can auto attack, don't e, instead auto attack
                            //this is performed automatically by the orbwalker

                            //if e'ing gets player in auto attack range. e
                            if (!_Player.IsInAutoAttackRange(enemy)
                                && YasuoCalcs.GetDashingEnd(enemy).IsInRange(enemy, _Player.GetAutoAttackRange()))
                            {
                                Program.E.Cast(enemy);

                                if (YasuoCalcs.GetDashingEnd(enemy).IsInRange(enemy, Program.EQRange) && EQCHECK && YasuoCalcs.WillQBeReady())
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
                                    if (YasuoCalcs.GetDashingEnd(dashEnemy).IsInRange(enemy, Program.EQRange) && YasuoCalcs.WillQBeReady() && EQCHECK)
                                        CastQ(dashEnemy, true);
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
                    float angle1 = _Player.Position.To2D().AngleBetween(YasuoCalcs.GetDashingEnd(fleeObject).To2D());
                    float angle2 = _Player.Position.To2D().AngleBetween(Game.CursorPos.To2D());
                    
                    //angle1 = 20 angle 2 = 40
                    if (Math.Abs(YasuoCalcs.RadiansToDegrees(angle1)) - Math.Abs(YasuoCalcs.RadiansToDegrees(angle2)) >= 20 ||
                        Math.Abs(YasuoCalcs.RadiansToDegrees(angle2)) - Math.Abs(YasuoCalcs.RadiansToDegrees(angle1)) >= 20)
                        Program.E.Cast(fleeObject);
                }
            }
        }
        
        //if q cd is 1.33 and enemy champion does not have yasuodashwrapper. do eq to minion, f, eq to champion, r - keyblade
        public static void Beyblade()
        {
            if (!IsDashing
                && _Player.HasBuff("yasuoq3w")
                && YasuoCalcs.WillQBeReady()
                && Program.E.IsReady()
                && Program.Flash != null
                && Program.Flash.IsReady()
                && Program.R.IsReady())
            {
                AIHeroClient enemy = ObjectManager.Get<AIHeroClient>().Where(a =>
                    a != null
                    && _Player.Distance(a) <= Program.BeybladeRange
                    //&& _Player.Distance(a) >= Program.E.Range + (Program.EQRange / 2) add this back when put into the combo
                    && a.IsEnemy
                    && !a.IsDead).OrderBy(a => a.Distance(_Player)).FirstOrDefault();

                if (enemy != null)
                {
                    Obj_AI_Base minion = ObjectManager.Get<Obj_AI_Base>().Where(a =>
                        a.IsEnemy
                        && a != enemy
                        && !a.HasBuff("YasuoDashWrapper")
                        && !a.IsDead
                        && _Player.Distance(a) <= Program.E.Range
                        && YasuoCalcs.GetDashingEnd(a).Distance(enemy) <= Program.Flash.Range + (Program.EQRange / 2)).FirstOrDefault();

                    if (enemy != null && minion != null)
                        Program.E.Cast(minion);
                }
            }
            //if is at the end of a dash
            else if (_Player.Spellbook.GetSpell(SpellSlot.E).CooldownExpires - Game.Time <= 0.2f
                && _Player.HasBuff("yasuoq3w")
                && Program.R.IsReady()
                && Program.E.State == SpellState.Surpressed
                //if q just came on cooldown (so started eq)
                //cd time (1.33) - time left (1) <= .3f (.3 of a second window)
                && _Player.Spellbook.GetSpell(SpellSlot.E).Cooldown - (_Player.Spellbook.GetSpell(SpellSlot.E).CooldownExpires - Game.Time ) <= 0.3f)
                //and is doing beyblade)
            {
                AIHeroClient enemy = EntityManager.Heroes.Enemies.Where(a =>
                _Player.Distance(a) <= Program.BeybladeRange
                && !a.IsDead).FirstOrDefault();

                if (enemy != null)
                {
                    Program.Q.Cast(enemy.Position);

                    if (_Player.Position.Distance(enemy) >= 400 && _Player.Position.Distance(enemy) <= 400 + (Program.EQRange / 2))
                        Program.Flash.Cast(_Player.Position.Extend(enemy, 400).To3D());
                    else if (_Player.Position.Distance(enemy) < 400)
                        Program.Flash.Cast(enemy.Position);
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
                    CastDelay = 250,
                    Width = 25,
                    Speed = 1500,
                    AllowedCollisionCount = int.MaxValue
                };
        }
        
        public static void CastQ(Obj_AI_Base target, bool EQ = false, bool lastHit = false)
        {
            if (!Program.Q.IsReady() || target == null || target.Position == Vector3.Zero && _Player.CanAttack)
                return;

            if (!EQ && !IsDashing && (Program.E.IsReady() || !Program.E.IsLearned))
            {
                IEnumerable<Obj_AI_Base> enemies = ObjectManager.Get<Obj_AI_Base>().Where(
                    a => a.IsEnemy 
                    && !a.IsDead 
                    && a.Type == target.Type
                    && (a.Type == GameObjectType.obj_AI_Minion
                    || a.Type == GameObjectType.AIHeroClient
                    || a.Type == GameObjectType.NeutralMinionCamp));
                if (lastHit)
                    enemies = enemies.Where(a => YasuoCalcs.Q(a) >= a.Health);

                Vector3 position = Program.Q.GetBestLinearCastPosition(enemies, 0, _Player.Position.To2D()).CastPosition;
                if (position != null && position != Vector3.Zero)
                {
                    Program.Q.Cast(position);
                    if(_Player.HasBuff("yasuoq3w"))
                        YasuoStatus = Status.Q3;
                    else
                        YasuoStatus = Status.Q;
                }
                //Chat.Print(position);
            }
            else if (EQ)
            {
                Program.Q.Cast(_Player.Position);
                if (_Player.HasBuff("yasuoq3w"))
                    YasuoStatus = Status.E3QAfterQ;
                else
                    YasuoStatus = Status.EQAfterQ;
            }
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

        public static void UltHandler()
        {
            bool UltIfAllEnemiesKU = Program.UltMenu["UltAEIV"].Cast<CheckBox>().CurrentValue;
            bool UltIfHalfEnemiesKU = Program.UltMenu["UltHEIV"].Cast<CheckBox>().CurrentValue;
            bool UltIf10Health = Program.UltMenu["UltLH"].Cast<CheckBox>().CurrentValue;
            bool UltAtLastSecond = Program.UltMenu["UltLS"].Cast<CheckBox>().CurrentValue;
            int UltEnemiesKnockedUp = Program.UltMenu["UltREnemies"].Cast<Slider>().CurrentValue;

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
        }
    }
}
