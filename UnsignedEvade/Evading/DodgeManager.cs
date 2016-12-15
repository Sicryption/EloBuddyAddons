using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Spells;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using System.Linq;

/*To DO List
Add Walking Into Darius Inner Q if Walking Out isn't an option.

    */

namespace UnsignedEvade
{
    class DodgeManager
    {
        public static float TimeOfLastMovementCheck = 0;

        public static Vector3 safePosition = Vector3.Zero;
        public static void Initialize()
        {

        }

        public static void HandleDodging()
        {
            AIHeroClient self = Player.Instance;

            if (!self.IsSafe())
            {
                //every 20th of a second it can check for movements. This is to avoid FPS drops.
                if (Game.Time - TimeOfLastMovementCheck >= 0.05f && !TryToMoveToSafePosition())
                {
                    List<CustomPolygon> polysThatHitMe = self.GetPolygonsThatHitMe();
                    List<SpellInfo> selfSpells = self.GetSpells();
                    Menu mainMenu = MenuHandler.mainChampionEvadeMenu;
                    float highestDangerValueIncoming = polysThatHitMe.OrderByDescending(a => a.GetDangerValue()).FirstOrDefault().GetDangerValue();
                    CustomPolygon closestSpell = self.FindSpellInfoWithClosestTime();

                    #region UseSpellShield
                    if (highestDangerValueIncoming >= mainMenu.GetSliderValue("Spell Shield Danger Level") && selfSpells.Any(a=>a.BuffType == SpellInfo.Buff.SpellShield && closestSpell.TimeUntilHitsChampion(self) <= a.Duration && a.IsOffCooldown()))
                    {
                        SpellInfo spellShield = selfSpells.FirstOrDefault(a => a.BuffType == SpellInfo.Buff.SpellShield && closestSpell.TimeUntilHitsChampion(self) <= a.Duration && a.IsOffCooldown());

                        //Sivir W/Nocturne W
                        if (spellShield.SpellType == SpellInfo.SpellTypeInfo.PassiveSpell
                            || spellShield.SpellType == SpellInfo.SpellTypeInfo.PassiveSpellWithBuff
                            || spellShield.SpellType == SpellInfo.SpellTypeInfo.PassiveSpellWithDuration)
                        {
                            Spell.Active spell = new Spell.Active(self.GetSpellSlotFromName(spellShield.SpellName));
                            bool casted = spell.Cast();
                            if (casted)
                                return;
                        }
                        //Morgana W
                        else if(spellShield.SpellType == SpellInfo.SpellTypeInfo.TargetedPassiveSpell)
                        {
                            Spell.Targeted spell = new Spell.Targeted(self.GetSpellSlotFromName(spellShield.SpellName), (uint)spellShield.Range);
                            bool casted = spell.Cast(self);
                            if (casted)
                                return;
                        }
                    }
                    #endregion

                    #region FlashFromSpell

                    if (highestDangerValueIncoming >= mainMenu.GetSliderValue("Spell Shield Danger Level"))
                    {
                        Spell.Skillshot flash = new Spell.Skillshot(self.GetSpellSlotFromName("SummonerFlash"), 425, SkillShotType.Linear, 0, 0, 0);

                        if (flash.IsReady())
                        {
                            Vector3 flashPos = GetSafePositions(self, flash.Range - 5).OrderByDescending(a => a.DistanceFromClosestEnemy()).FirstOrDefault();
                            bool casted = flash.Cast(flashPos);
                            if (casted)
                                return;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                safePosition = Vector3.Zero;
            }
        }
        
        public static bool TryToMoveToSafePosition()
        {
            //timeUntilHit in form of MS
            CustomPolygon closestTimePolygon = Player.Instance.FindSpellInfoWithClosestTime();

            if (closestTimePolygon == null)
                return false;

            if (safePosition != Vector3.Zero && safePosition.IsSafe(Player.Instance))
            {
                Orbwalker.MoveTo(safePosition.Extend(Player.Instance.Position, -100).To3DFromNavMesh());
                return true;
            }

            TimeOfLastMovementCheck = Game.Time;
            float timeUntilHit = closestTimePolygon.TimeUntilHitsChampion(Player.Instance);
            float movementSpeed = Player.Instance.MoveSpeed;
            //d = ts/1000
            //capped at 500 to remove massive fps drops
            float DistancePlayerCanWalkToBeforeBeingHit = Math.Min(timeUntilHit * movementSpeed / 1000, 500);

            List<Vector3> walkingPositions = GetSafePositions(Player.Instance, DistancePlayerCanWalkToBeforeBeingHit);

            if(walkingPositions.Count > 0)
            {
                Console.WriteLine("PossibleSafePositions: " + walkingPositions.Count);
                Vector3 safePos = walkingPositions.Where(a => a != null && a != Vector3.Zero && Player.Instance.GetPath(a).All(b=>!b.IsWall())&& IsSafer(Player.Instance.GetPath(a), timeUntilHit)).OrderBy(a=>a.Distance(Player.Instance)).FirstOrDefault();
                if (safePos != null && safePos != Vector3.Zero)
                {
                    Console.WriteLine("Set");
                    //move past the position so that the player isn't hit by the skillshot
                    Orbwalker.MoveTo(safePos.Extend(Player.Instance.Position, -Player.Instance.BoundingRadius).To3DFromNavMesh());
                    safePosition = safePos;
                    return true;
                }
            }
            else
                Console.WriteLine("No Safe Positions");
            return false;
        }

        public static List<Vector3> GetSafePositions(Obj_AI_Base unit, float range)
        {
            List<Vector3> walkingPositions = new List<Vector3>();

            Vector3 extendingPos = unit.Position + new Vector3(0, range, 0);

            for (int i = 0; i < 360; i++)
            {
                for (int b = 1; b < 5; b++)
                {
                    Vector3 position = unit.Position.Extend(extendingPos.To2D().RotateAroundPoint(unit.Position.To2D(), MathUtil.DegreesToRadians(i)), range / b).To3DFromNavMesh();

                    if (position.IsSafe(unit as AIHeroClient))
                        walkingPositions.Add(position);
                }
            }
            return walkingPositions;
        }

        public static bool IsSafer(Vector3[] path, float timeUntilHit)
        {
            return path.All(a => a.GetSpellInfoWithClosestTime() >= timeUntilHit);
        }
    }
}
