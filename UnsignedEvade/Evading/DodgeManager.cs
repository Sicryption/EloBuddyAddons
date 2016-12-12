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

namespace UnsignedEvade
{
    class DodgeManager
    {
        public static Vector3 safePosition = Vector3.Zero;
        public static void Initialize()
        {

        }

        public static void HandleDodging()
        {
            if(!Player.Instance.IsSafe())
            {
                if(!TryToMoveToSafePosition())
                {
                    /*//use spells to evade
                    Chat.Print("No safe position to walk to");

                    Spell.Skillshot flash = new Spell.Skillshot(Player.Instance.GetSpellSlotFromName("SummonerFlash"), 400, SkillShotType.Linear, 0, 0, 0);

                    if(flash.IsReady())
                    {
                        flash.Cast(GetSafePositions(Player.Instance, flash.Range)[0]);
                    }
                    */
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

            if (safePosition != Vector3.Zero)
            {
                Orbwalker.MoveTo(safePosition.Extend(Player.Instance.Position, -100).To3DFromNavMesh());
                return true;
            }

            float timeUntilHit = closestTimePolygon.TimeUntilHitsChampion(Player.Instance);
            float movementSpeed = Player.Instance.MoveSpeed;
            //d = ts/1000
            float DistancePlayerCanWalkToBeforeBeingHit = timeUntilHit * movementSpeed / 1000;

            List<Vector3> walkingPositions = GetSafePositions(Player.Instance, DistancePlayerCanWalkToBeforeBeingHit);

            if(walkingPositions.Count > 0)
            {
                Console.WriteLine("PossibleSafePositions: " + walkingPositions.Count);
                Vector3 safePos = walkingPositions.Where(a => a != null && a != Vector3.Zero && Player.Instance.GetPath(a).All(b=>!b.IsWall())&& IsSafer(Player.Instance.GetPath(a), timeUntilHit)).OrderBy(a=>a.Distance(Player.Instance)).FirstOrDefault();
                if (safePos != null)
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
