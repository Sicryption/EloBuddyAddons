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

namespace UnsignedGP
{
    class GPFunctions
    {
        public static float TimeOfLastBarrelCheck = 0;

        public static bool HasDoneActionThisTick = false;

        public enum AttackSpell
        {
            Q,
            W,
            E,
            R,
            ENearEnemy,
            Ignite
        };
        
        public static Obj_AI_Base GetEnemy(float range, GameObjectType type)
        {
            return ObjectManager.Get<Obj_AI_Base>().OrderBy(a => a.Health).Where(a => a.IsEnemy
            && a.Type == type
            && a.Distance(GP) <= range
            && !a.IsDead
            && !a.IsInvulnerable
            && a.IsValidTarget(range)).FirstOrDefault();
        }

        public static Obj_AI_Base GetEnemyKS(GameObjectType type, AttackSpell spell, bool EwithQ = false)
        {
            float range = 0;
            if (spell == AttackSpell.Q)
                range = Program.Q.Range;
            else if (spell == AttackSpell.E)
                range = Program.E.Range + 200;
            else if (spell == AttackSpell.Ignite)
                range = Program.Ignite.Range;
            else if (spell == AttackSpell.R)
                range = int.MaxValue;
            //ksing
            return ObjectManager.Get<Obj_AI_Base>().OrderBy(a => a.Health).Where(a => a.IsEnemy
                && a.Type == type
                && a.Distance(GP) <= range
                && !a.IsDead
                && !a.IsInvulnerable
                && a.Name != "Barrel"
                && a.IsValidTarget(range)
                &&
                (
                (a.Health <= GPCalcs.Q(a) && AttackSpell.Q == spell && !NearbyBarrel(350, a.Position)) ||
                (a.Health <= GPCalcs.E(a, EwithQ) && AttackSpell.E == spell) ||
                (a.Health <= (GPCalcs.RDamagePerWave(a) * 8) && AttackSpell.R == spell) ||
                (a.Health <= GPCalcs.Ignite(a) && AttackSpell.Ignite == spell)
                )).FirstOrDefault();
        }

        public static AIHeroClient GP { get { return ObjectManager.Player; } }
        
        public static void LastHit()
        {
            bool QCHECK = Program.LastHit["LHQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.LastHit["LHE"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();
            
            if (QCHECK && QREADY && !NearbyBarrel((int)Program.Q.Range))
            {
                Obj_AI_Minion enemy = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.Q);

                if (enemy != null)
                    HasDoneActionThisTick = Program.Q.Cast(enemy);
            }
            else if (QCHECK && QREADY && NearbyBarrel((int)Program.Q.Range))
            {
                List<Obj_AI_Base> barrels = GetNearbyBarrels((int)Program.Q.Range);

                int enemyCount = 0;
                Obj_AI_Base target = null;

                foreach(Obj_AI_Base barrel in barrels)
                {
                    if (barrel.Health == 1)
                    {
                        int tempCount = EntityManager.Enemies.Where(a => a.Distance(barrel) <= 350 && !a.IsDead && a.Health <= GPCalcs.E(a, true)).Count();
                        target = (tempCount > enemyCount) ? barrel : target;
                        enemyCount = (tempCount > enemyCount) ? tempCount : enemyCount;
                    }
                }

                if (target != null && enemyCount != 0)
                    HasDoneActionThisTick =  Program.Q.Cast(target);
            }
            else if (GP.CanAttack && NearbyBarrel((int)GP.GetAutoAttackRange()))
            {
                List<Obj_AI_Base> barrels = GetNearbyBarrels((int)Program.Q.Range);

                int enemyCount = 0;
                Obj_AI_Base target = null;

                foreach (Obj_AI_Base barrel in barrels)
                {
                    if (barrel.Health == 1)
                    {
                        int tempCount = EntityManager.Enemies.Where(a => a.Distance(barrel) <= 350 && !a.IsDead && a.Health <= GPCalcs.E(a, false)).Count();
                        target = (tempCount > enemyCount) ? barrel : target;
                        enemyCount = (tempCount > enemyCount) ? tempCount : enemyCount;
                    }
                }

                if (target != null && enemyCount != 0)
                {
                    Orbwalker.ForcedTarget = target;
                    HasDoneActionThisTick = true;
                }
            }


            if (ECHECK && EREADY)
            {
                //find spot that can hit all 6 minions and use barrel
                List<Obj_AI_Base> enemies = ObjectManager.Get<Obj_AI_Base>().Where(a => a.IsEnemy && !a.IsDead && a.Name != "Barrel" && a.Distance(GP) <= Program.E.Range && a.Type == GameObjectType.obj_AI_Minion).ToList();

                if (enemies != null && enemies.FirstOrDefault() != null && enemies.Count > 0)
                {
                    try
                    {
                        Spell.Skillshot.BestPosition pos = Program.E.GetBestCircularCastPosition(enemies);

                        if (pos.CastPosition != null)
                        {
                            List<Obj_AI_Base> obs = EntityManager.Enemies.Where(a => a.Distance(pos.CastPosition) <= 350 && !a.IsDead).ToList();

                            if (obs != null)
                            {
                                if (obs.Count >= 4)
                                    HasDoneActionThisTick = PlaceBarrel(pos.CastPosition);
                            }
                        }
                    }
                    catch
                    {
                        //Chat.Print("FAILING WITH PLACING BARREL");
                    }
                }
            }
        }

        public static bool NearbyBarrel(int range = 1000, Vector3 position = new Vector3())
        {
            if (position == new Vector3())
                position = GP.Position;

            List<Obj_AI_Base> barrels = ObjectManager.Get<Obj_AI_Base>().Where(a => a.Name == "Barrel" && !a.IsDead && a.Distance(position) <= range).ToList();

            if (barrels.FirstOrDefault() != null)
                return true;
            return false;
        }

        public static List<Obj_AI_Base> GetNearbyBarrels(int range = 1000)
        {
            List<Obj_AI_Base> barrels = ObjectManager.Get<Obj_AI_Base>().Where(a => a.Name == "Barrel" && a.Distance(GP) <= range).ToList();

            if (barrels.FirstOrDefault() != null)
                return barrels;
            return null;
        }
        
        public static void KillSteal()
        {
            bool QCHECK = Program.Killsteal["KSQ"].Cast<CheckBox>().CurrentValue;
            bool RCHECK = Program.Killsteal["KSR"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.Killsteal["KSE"].Cast<CheckBox>().CurrentValue;
            bool ICHECK = Program.Killsteal["KSI"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool RREADY = Program.R.IsReady();
            bool EREADY = Program.E.IsReady();
            bool IREADY = (Program.Ignite != null && Program.Ignite.IsReady()) ? true : false;
            
            TryToComboBarrels(true);

            if (GP.CountEnemiesInRange(Program.Q.Range) >= 1)
            {
                if (QCHECK && QREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.Q);

                    if (enemy != null)
                        HasDoneActionThisTick = Program.Q.Cast(enemy.Position);
                }
                if (ICHECK && IREADY)
                {
                    AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.Ignite);

                    if (enemy != null)
                        HasDoneActionThisTick = Program.Ignite.Cast(enemy);
                }
            }

            if (RCHECK && RREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemyKS(GameObjectType.AIHeroClient, AttackSpell.R);

                if (enemy != null)
                    HasDoneActionThisTick = Program.R.Cast(enemy.Position);
            }
        }
        
        public static void LaneClear()
        {
            bool QCHECK = Program.LaneClear["LCQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.LaneClear["LCE"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();
            
            if (QCHECK && QREADY && !NearbyBarrel((int)Program.Q.Range))
            {
                Obj_AI_Minion enemy = (Obj_AI_Minion)GetEnemyKS(GameObjectType.obj_AI_Minion, AttackSpell.Q);

                if (enemy != null)
                    HasDoneActionThisTick = Program.Q.Cast(enemy);
            }
            else if (QCHECK && QREADY && NearbyBarrel((int)Program.Q.Range))
            {
                List<Obj_AI_Base> barrels = GetNearbyBarrels((int)Program.Q.Range);

                int enemyCount = 0;
                Obj_AI_Base target = null;

                foreach (Obj_AI_Base barrel in barrels)
                {
                    if (barrel.Health == 1)
                    {
                        int tempCount = EntityManager.Enemies.Where(a => a.Distance(barrel) <= 350).Count();
                        target = (tempCount > enemyCount) ? barrel : target;
                        enemyCount = (tempCount > enemyCount) ? tempCount : enemyCount;
                    }
                }

                if (target != null && enemyCount != 0)
                    HasDoneActionThisTick = Program.Q.Cast(target);
            }
            else if (GP.CanAttack && NearbyBarrel((int)GP.GetAutoAttackRange()))
            {
                List<Obj_AI_Base> barrels = GetNearbyBarrels((int)Program.Q.Range);

                int enemyCount = 0;
                Obj_AI_Base target = null;

                foreach (Obj_AI_Base barrel in barrels)
                {
                    if (barrel.Health == 1)
                    {
                        int tempCount = EntityManager.Enemies.Where(a => a.Distance(barrel) <= 350).Count();
                        target = (tempCount > enemyCount) ? barrel : target;
                        enemyCount = (tempCount > enemyCount) ? tempCount : enemyCount;
                    }
                }

                if (target != null && enemyCount != 0)
                {
                    Orbwalker.ForcedTarget = target;
                    HasDoneActionThisTick = true;
                }
            }

            if (ECHECK && EREADY)
            {
                //find spot that can hit all 6 minions and use barrel
                List<Obj_AI_Base> enemies = EntityManager.Enemies.Where(a => a.IsEnemy && !a.IsDead && !a.IsMoving && a.Distance(GP) <= Program.E.Range + 350 && a.Type == GameObjectType.obj_AI_Minion).ToList();

                if (enemies.Count > 0)
                {
                    Spell.Skillshot.BestPosition pos = Program.E.GetBestCircularCastPosition(enemies);

                    if (pos.CastPosition != null)
                    {
                        List<Obj_AI_Base> obs = EntityManager.Enemies.Where(a => a.Distance(pos.CastPosition) <= 350).ToList();

                        if (obs != null)
                        {
                            if (obs.Count >= 4)
                                HasDoneActionThisTick = PlaceBarrel(pos.CastPosition);
                        }
                    }
                }
            }
        }
        
        public static void Harrass()
        {
            bool QCHECK = Program.Harass["HQ"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.Harass["HE"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();


            TryToComboBarrels();

            if (QCHECK && QREADY)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    HasDoneActionThisTick = Program.Q.Cast(enemy);
            }
        }
        
        public static void Combo()
        {
            bool QCHECK = Program.ComboMenu["QU"].Cast<CheckBox>().CurrentValue;
            bool ECHECK = Program.ComboMenu["EU"].Cast<CheckBox>().CurrentValue;
            bool ItemsCHECK = Program.ComboMenu["IU"].Cast<CheckBox>().CurrentValue;
            bool QREADY = Program.Q.IsReady();
            bool EREADY = Program.E.IsReady();

            TryToComboBarrels();

            if (QCHECK && QREADY && !NearbyBarrel((int)Program.Q.Range))
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(Program.Q.Range, GameObjectType.AIHeroClient);

                if (enemy != null)
                    HasDoneActionThisTick = Program.Q.Cast(enemy);
            }
            
            if (ItemsCHECK)
            {
                AIHeroClient enemy = (AIHeroClient)GetEnemy(2500, GameObjectType.AIHeroClient);

                if (enemy != null)
                    HasDoneActionThisTick = UseItems(true);
            }
        }

        public static void TryToComboBarrels(bool ks = false)
        {
            //cont == continue. If at any point it uses a barrel, do not try to continue the script. Back up and reset
            List<Obj_AI_Base> NearbyBarrels = ObjectManager.Get<Obj_AI_Base>().Where(a => a.Name == "Barrel" && !a.IsDead && a.Distance(GP) <= Program.Q.Range).ToList();

            //first check if enemy is on any of the barrels. use no barrels
            foreach(Obj_AI_Base barrel in NearbyBarrels)
            {
                List<AIHeroClient> nearbyEnemies = ObjectManager.Get<AIHeroClient>()
                    .Where(a => a.IsEnemy 
                    && !a.IsDead 
                    && (!ks || GPCalcs.E(a, false) >= a.Health) 
                    && a.Distance(barrel) <= 350 
                    && a.Type == GameObjectType.AIHeroClient).ToList();

                if(nearbyEnemies.FirstOrDefault() != null)
                {
                    HasDoneActionThisTick = FindAndDoBestAttackMethod(barrel);
                }
            }
            //then check if enemy is within 1 barrel of an existing barrel,
            //need to check if barrel has 2 hp and auto then q it.
            
            if ((Program.E.AmmoQuantity >= 1 || !Program.E.IsOnCooldown) && Program.E.IsReady() && GP.CanCast && !HasDoneActionThisTick)
            {
                foreach (Obj_AI_Base barrel in NearbyBarrels)
                {
                    List<AIHeroClient> nearbyEnemies = ObjectManager.Get<AIHeroClient>().Where(a => a.IsEnemy && !a.IsDead && (!ks || GPCalcs.E(a, false) >= a.Health) && a.Distance(barrel) <= 1000).ToList();
                    
                    if (nearbyEnemies.FirstOrDefault() != null)
                    {
                        PlaceBarrel(barrel.Position.Extend(nearbyEnemies.First(), 650).To3D());
                        HasDoneActionThisTick = FindAndDoBestAttackMethod(barrel);
                    }
                }
            }
            //then check if an enemy is within 2 barrel of an existing barrel and do 3 barrel combo
            if (Program.E.AmmoQuantity >= 2 && Program.E.IsReady() && !HasDoneActionThisTick)
            {

            }

            //first barrel placement
            if(NearbyBarrels.Count() == 0 
                && Program.E.IsReady() && GP.CanCast && !HasDoneActionThisTick 
                && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo)
                && Program.E.AmmoQuantity >= 1)
            {
                if(Program.ComboMenu.Get<ComboBox>("BarrelSettings").SelectedText == "EloBuddy Prediction")
                {
                    Spell.Skillshot.BestPosition prediction = Program.E.GetBestCircularCastPosition(EntityManager.Heroes.Enemies);
                    if(prediction.CastPosition != null && prediction.CastPosition != Vector3.Zero && prediction.CastPosition.CountEnemyHeroesInRangeWithPrediction((int)Program.E.Range, 0) >= 1)
                        HasDoneActionThisTick = PlaceBarrel(prediction.CastPosition);
                }

                else if (Program.ComboMenu.Get<ComboBox>("BarrelSettings").SelectedText == "On Closest Enemy")
                {
                    AIHeroClient closestEnemy = EntityManager.Heroes.Enemies.Where(a => !a.IsDead && a.IsHPBarRendered && a.IsInRange(GP, Program.E.Range)).OrderBy(a => a.Distance(GP)).FirstOrDefault();
                    if (closestEnemy != null)
                        HasDoneActionThisTick = PlaceBarrel(closestEnemy.Position);
                }

                else if (Program.ComboMenu.Get<ComboBox>("BarrelSettings").SelectedText == "On Lowest HP Enemy")
                {
                    AIHeroClient closestEnemy = EntityManager.Heroes.Enemies.Where(a => !a.IsDead && a.IsHPBarRendered && a.IsInRange(GP, Program.E.Range)).OrderBy(a => a.Health).FirstOrDefault();
                    if (closestEnemy != null)
                        HasDoneActionThisTick = PlaceBarrel(closestEnemy.Position);
                }

                else if (Program.ComboMenu.Get<ComboBox>("BarrelSettings").SelectedText == "On Lowest % HP Enemy")
                {
                    AIHeroClient closestEnemy = EntityManager.Heroes.Enemies.Where(a => !a.IsDead && a.IsHPBarRendered && a.IsInRange(GP, Program.E.Range)).OrderBy(a => a.HealthPercent).FirstOrDefault();
                    if (closestEnemy != null)
                        HasDoneActionThisTick = PlaceBarrel(closestEnemy.Position);
                }

                else if (Program.ComboMenu.Get<ComboBox>("BarrelSettings").SelectedText == "Between Enemy and Me")
                {
                    Spell.Skillshot.BestPosition prediction = Program.E.GetBestCircularCastPosition(EntityManager.Heroes.Enemies);
                    if (prediction.CastPosition != null && prediction.CastPosition != Vector3.Zero && prediction.CastPosition.CountEnemyHeroesInRangeWithPrediction((int)Program.E.Range, 0) >= 1)
                        HasDoneActionThisTick = PlaceBarrel(GP.Position.Extend(prediction.CastPosition, Program.E.Range / 2).To3D());
                }
            }
        }

        public static bool PlaceBarrel(Vector3 position)
        {
            List<Obj_AI_Base> barrels = ObjectManager.Get<Obj_AI_Base>().Where(a => a.Name == "Barrel" && !a.IsDead).ToList();

            foreach (Obj_AI_Base ob in barrels)
                if (ob.Distance(position) <= 340)
                    return false;

            return Program.E.Cast(position);
        }   

        public static bool FindAndDoBestAttackMethod(Obj_AI_Base barrel)
        {
            if (GP.IsInAutoAttackRange(barrel) && GP.CanAttack)
            {
                Orbwalker.ForcedTarget = barrel;
                return true;
            }
            else if (Program.Q.IsReady() && barrel.Distance(GP) <= Program.Q.Range)
                return GPCalcs.CastQOnBarrel(barrel);

            return false;
        }
        
        //Use bombs to proc phage passive if it exists or auto to get off move speed buff if gp has passive
        public static void Flee()
        {
            Orbwalker.MoveTo(Game.CursorPos);

            if (Program.Q.IsReady())
            {
                Obj_AI_Base barrel = ObjectManager.Get<Obj_AI_Base>().Where(a =>
                    a.Name == "Barrel"
                    && a.Distance(GP) <= Program.Q.Range
                    && !a.IsDead).FirstOrDefault();

                if (barrel != null)
                    HasDoneActionThisTick = GPCalcs.CastQOnBarrel(barrel);
            }
        }
        
        public static bool UseItems(bool isInComboMode = false)
        {
            bool useTiamat = Program.Items.Get<CheckBox>("ItemsT").CurrentValue;
            bool useRavenous = Program.Items.Get<CheckBox>("ItemsRH").CurrentValue;
            bool useTitanic = Program.Items.Get<CheckBox>("ItemsTH").CurrentValue;
            bool useCutlass = Program.Items.Get<CheckBox>("ItemsBC").CurrentValue;
            bool useYoumuus = Program.Items.Get<CheckBox>("ItemsY").CurrentValue;
            bool useBORK = Program.Items.Get<CheckBox>("ItemsBORK").CurrentValue;
            InventorySlot[] items = GP.InventoryItems;

            foreach (InventorySlot item in items)
            {
                if (item.CanUseItem())
                {
                    if (item.Id == ItemId.Health_Potion
                        && GP.HealthPercent <= Program.Items["PotSlider"].Cast<Slider>().CurrentValue
                        && !GP.IsRecalling()
                        && !GP.IsInShopRange()
                        && !GP.HasBuff("RegenerationPotion"))
                    {
                        return item.Cast();
                    }
                }

                if (((item.Id == ItemId.Blade_of_the_Ruined_King && useBORK)
                        || (item.Id == ItemId.Bilgewater_Cutlass && useCutlass)) &&
                        isInComboMode)
                {
                    var enemy = GetEnemy(550, GameObjectType.AIHeroClient);

                    if (enemy != null)
                        return item.Cast(enemy);
                }

                if ((item.Id == ItemId.Tiamat_Melee_Only && useTiamat)
                    || (item.Id == ItemId.Ravenous_Hydra_Melee_Only && useRavenous)
                    || (item.Id == ItemId.Titanic_Hydra && useTitanic)
                    && isInComboMode)
                {
                    var enemy = GetEnemy(400, GameObjectType.AIHeroClient);

                    if (enemy != null)
                        return item.Cast();
                }

                if ((item.Id == ItemId.Youmuus_Ghostblade && useYoumuus)
                    && isInComboMode
                    && GP.CountEnemiesInRange(GP.GetAutoAttackRange()) >= 1)
                    return item.Cast();    
            }
            return false;
        }

        public static void AutoBarrel()
        {
            if (Game.Time - TimeOfLastBarrelCheck >= 1)
                TimeOfLastBarrelCheck = Game.Time;
            else
                return;

            if(Program.E.AmmoQuantity == 3 && Program.E.IsReady())
            {
                GrassObject bush = ObjectManager.Get<GrassObject>().Where(
                    a =>
                    a.Distance(GP) <= Program.E.Range
                    && !NearbyBarrel(a)).OrderBy(a=>a.Distance(GP)).FirstOrDefault();

                if (bush != null)
                    HasDoneActionThisTick = PlaceBarrel(bush.Position);
            }
        }

        public static void AutoW()
        {
            #region QSS/AutoWCleanse
            bool QSSBlind = Program.Items.Get<CheckBox>("QSSBlind").CurrentValue,
                QSSCharm = Program.Items.Get<CheckBox>("QSSCharm").CurrentValue,
                QSSFear = Program.Items.Get<CheckBox>("QSSFear").CurrentValue,
                QSSKB = Program.Items.Get<CheckBox>("QSSKB").CurrentValue,
                QSSSilence = Program.Items.Get<CheckBox>("QSSSilence").CurrentValue,
                QSSSlow = Program.Items.Get<CheckBox>("QSSSlow").CurrentValue,
                QSSSnare = Program.Items.Get<CheckBox>("QSSSnare").CurrentValue,
                QSSStun = Program.Items.Get<CheckBox>("QSSStun").CurrentValue,
                QSSTaunt = Program.Items.Get<CheckBox>("QSSTaunt").CurrentValue;

            if (
                    (
                        (Program.W.IsReady() && Program.Items.Get<CheckBox>("AW").CurrentValue)
                            || (Program.Items.Get<CheckBox>("ItemsQSS").CurrentValue 
                        && GP.HasItem(ItemId.Quicksilver_Sash) 
                        && GP.InventoryItems.Where(a=>a.Id == ItemId.Quicksilver_Sash).FirstOrDefault() != null
                        && GP.InventoryItems.Where(a => a.Id == ItemId.Quicksilver_Sash).FirstOrDefault().CanUseItem())
                            || (Program.Items.Get<CheckBox>("ItemsMS").CurrentValue
                        && GP.HasItem(ItemId.Mercurial_Scimitar)
                        && GP.InventoryItems.Where(a => a.Id == ItemId.Mercurial_Scimitar).FirstOrDefault() != null
                        && GP.InventoryItems.Where(a => a.Id == ItemId.Mercurial_Scimitar).FirstOrDefault().CanUseItem())
                    )

                &&
                ((GP.HasBuffOfType(BuffType.Blind) && QSSBlind)
                || (GP.HasBuffOfType(BuffType.Charm) && QSSCharm)
                || (GP.HasBuffOfType(BuffType.Fear) && QSSFear)
                || (GP.HasBuffOfType(BuffType.Knockback) && QSSKB)
                //not standing on raka silence 
                || (GP.HasBuffOfType(BuffType.Silence) && QSSSilence && !GP.HasBuff("sorakaepacify"))
                || (GP.HasBuffOfType(BuffType.Slow) && QSSSlow)
                || (GP.HasBuffOfType(BuffType.Snare) && QSSSnare)
                || (GP.HasBuffOfType(BuffType.Stun) && QSSStun)
                || (GP.HasBuffOfType(BuffType.Taunt) && QSSTaunt))
                //not being knocked back by dragon
                && !GP.HasBuff("moveawaycollision"))
                HasDoneActionThisTick = Program.W.Cast();
            #endregion
            #region W At %HP and %Mana
            if (Program.W.IsReady()
                && !GP.IsRecalling()
                && Program.SettingsMenu.Get<Slider>("SAWH").CurrentValue != 100
                && Program.SettingsMenu.Get<Slider>("SAWH").CurrentValue >= GP.HealthPercent
                && Program.SettingsMenu.Get<Slider>("SAWM").CurrentValue != 100
                && Program.SettingsMenu.Get<Slider>("SAWM").CurrentValue <= GP.ManaPercent)
                HasDoneActionThisTick = Program.W.Cast();
            #endregion
        }

        public static bool NearbyBarrel(GameObject obj)
        {
            List<GameObject> listOfNearbyObjects = ObjectManager.Get<GameObject>().Where(a => a.Distance(obj) <= 699 && !a.IsDead).ToList();
            if (listOfNearbyObjects.Any(a => a.Name == "Barrel" && !a.IsDead))
                return true;

            return false;
        }
    }
}
