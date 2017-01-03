using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;
using System.Linq;
using SharpDX;

namespace UnsignedRengar
{
    internal class Program
    {
        public static AIHeroClient Rengar;
        public static Spell.Active W,
            R;
        public static Spell.Skillshot E,
            Q,
            Q2;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != "Rengar")
                return;
            
            Rengar = Player.Instance;
            ModeHandler.Rengar = Player.Instance;
            
            MenuHandler.Initialize();

            Q = new Spell.Skillshot(SpellSlot.Q, 326, EloBuddy.SDK.Enumerations.SkillShotType.Cone, 250, 3000, 150, DamageType.Physical)
            {
                ConeAngleDegrees = 180,
                AllowedCollisionCount = int.MaxValue,
                MinimumHitChance = EloBuddy.SDK.Enumerations.HitChance.High,

            };
            Q2 = new Spell.Skillshot(SpellSlot.Q, 450, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 500, 3000, 150, DamageType.Physical)
            {
                AllowedCollisionCount = int.MaxValue,
            };
            W = new Spell.Active(SpellSlot.W, 450, DamageType.Magical);
            E = new Spell.Skillshot(SpellSlot.E, 1000, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 250, 1500, 70, DamageType.Physical)
            {
                AllowedCollisionCount = 0,
                MinimumHitChance = EloBuddy.SDK.Enumerations.HitChance.High,
            };
            R = new Spell.Active(SpellSlot.R, 2000);

            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += Drawing_OnDraw;
            Drawing.OnEndScene += Drawing_OnEndScene;
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            if (Rengar.IsDead)
                return;

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Enemy Health after Combo"))
                foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(a=>a.MeetsCriteria()))
                {
                    int hpBarWidth = 96;
                    float enemyHPPercentAfterCombo = Math.Max((100 * ((enemy.Health - enemy.ComboDamage()) / enemy.MaxHealth)), 0);
                    //Vector2 FriendlyHPBarOffset = new Vector2(26, 3);
                    Vector2 EnemyHPBarOffset = new Vector2(2, 9.5f);
                    Vector2 CurrentHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(100 * enemy.HealthPercent / hpBarWidth, 0);
                    Vector2 EndHP = enemy.HPBarPosition + EnemyHPBarOffset + new Vector2(enemyHPPercentAfterCombo, 0);
                    if(enemyHPPercentAfterCombo == 0)
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Green);
                    else
                        Drawing.DrawLine(CurrentHP, EndHP, 9, System.Drawing.Color.Yellow);
                }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (Rengar.IsDead)
                return;

            System.Drawing.Color drawColor = System.Drawing.Color.Blue;

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Q"))
            {
                new Geometry.Polygon.Sector(Rengar.Position, Rengar.Position.Extend(Rengar.Position + Rengar.Direction, Q.Range).To3D((int)Rengar.Position.Z), (float)(Q.ConeAngleDegrees * Math.PI / 180), Q.Range).Draw(drawColor);
                DrawLinearSkillshot(Rengar.Position, Rengar.Position.Extend(Rengar.Position + Rengar.Direction, Q2.Range).To3D((int)Rengar.Position.Z), Q.Width, Q.Speed, Q.Range, Q.AllowedCollisionCount);
            }

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Q Radius"))
                Q.DrawRange(drawColor, 3);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw W"))
                W.DrawRange(drawColor, 3);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw E"))
                E.DrawRange(drawColor, 3);

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw R Detection Range"))
                R.DrawRange(drawColor, 3);

            AIHeroClient closestEnemy = EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria() && a.IsInRange(Rengar, 3000)).OrderBy(a=>a.Distance(Rengar)).FirstOrDefault();

            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Arrow to R Target") && Rengar.HasBuff("RengarR") && closestEnemy != null)
                Rengar.Position.DrawArrow(closestEnemy.Position, drawColor);
            
            if (MenuHandler.GetCheckboxValue(MenuHandler.Drawing, "Draw Killable Text"))
            {
                foreach (AIHeroClient enemy in EntityManager.Heroes.Enemies.Where(a => a.MeetsCriteria() && a.Health < a.ComboDamage()))
                    Drawing.DrawText(enemy.Position.WorldToScreen(), System.Drawing.Color.GreenYellow, "Killable", 15);
             }

        }

        public static void DrawLinearSkillshot(Vector3 startPosition, Vector3 endPosition, float width, float missileSpeed, float range, float collisionCount)
        {
            System.Drawing.Color drawColor = System.Drawing.Color.Blue;

            if (collisionCount != 0 && collisionCount != int.MaxValue)
            {
                List<Obj_AI_Base> enemiesThatWillBeHit = new List<Obj_AI_Base>();
                //get if unit(s) will be hit by spell if so get the info.CollisionCount's units position and set it as the end position
                foreach (Obj_AI_Base enemy in EntityManager.Enemies.Where(a => !a.IsDead && a.Distance(startPosition) <= range))
                    if (Prediction.Position.Collision.LinearMissileCollision(enemy, startPosition.To2D(), endPosition.To2D(), missileSpeed, (int)width, 0))
                        enemiesThatWillBeHit.Add(enemy);

                enemiesThatWillBeHit.OrderByDescending(a => a.Distance(startPosition));
                if (enemiesThatWillBeHit.Count() >= collisionCount)
                    endPosition = enemiesThatWillBeHit[(int)collisionCount - 1].Position;
            }

            Vector3 northernMostPoint = (startPosition.Y >= endPosition.Y) ? startPosition : endPosition;
            Vector3 southernMostPoint = (startPosition.Y >= endPosition.Y) ? endPosition : startPosition;

            Vector3 betweenVector = new Vector3(northernMostPoint.X - southernMostPoint.X, northernMostPoint.Y - southernMostPoint.Y, 0f);
            Vector2 betweenVector2 = new Vector2(betweenVector.Y, -betweenVector.X);
            double Length = Math.Sqrt(betweenVector2.X * betweenVector2.X + betweenVector2.Y * betweenVector2.Y); //Thats length of perpendicular
            Vector2 NewVector = new Vector2((float)(betweenVector2.X / Length), (float)(betweenVector2.Y / Length)); //Now N is normalized perpendicular

            Vector3 NEPoint = new Vector3(southernMostPoint.X + NewVector.X * (width / 2), southernMostPoint.Y + NewVector.Y * (width / 2), startPosition.Z);
            Vector3 NWPoint = new Vector3(southernMostPoint.X - NewVector.X * (width / 2), southernMostPoint.Y - NewVector.Y * (width / 2), startPosition.Z);
            Vector3 SEPoint = new Vector3(northernMostPoint.X + NewVector.X * (width / 2), northernMostPoint.Y + NewVector.Y * (width / 2), startPosition.Z);
            Vector3 SWPoint = new Vector3(northernMostPoint.X - NewVector.X * (width / 2), northernMostPoint.Y - NewVector.Y * (width / 2), startPosition.Z);

            //top
            Drawing.DrawLine(NEPoint.WorldToScreen(), NWPoint.WorldToScreen(), 3, drawColor);
            //bottom
            Drawing.DrawLine(SEPoint.WorldToScreen(), SWPoint.WorldToScreen(), 3, drawColor);
            //right
            Drawing.DrawLine(NEPoint.WorldToScreen(), SEPoint.WorldToScreen(), 3, drawColor);
            //left
            Drawing.DrawLine(NWPoint.WorldToScreen(), SWPoint.WorldToScreen(), 3, drawColor);
        }

        private static void Game_OnTick(EventArgs args)
        {
            ModeHandler.hasDoneActionThisTick = false;

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
                ModeHandler.Combo();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                ModeHandler.JungleClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
                ModeHandler.LastHit();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                ModeHandler.LaneClear();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
                ModeHandler.Harass();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                ModeHandler.Flee();
            if (MenuHandler.Killsteal.GetCheckboxValue("Killsteal"))
                ModeHandler.Killsteal();
        }
    }
}