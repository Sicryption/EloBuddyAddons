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
                Orbwalker.MoveTo(safePosition);
                return true;
            }
            
            float timeUntilHit = closestTimePolygon.TimeUntilHitsChampion(Player.Instance);
            float movementSpeed = Player.Instance.MoveSpeed;
            //d = ts/1000
            float DistancePlayerCanWalkToBeforeBeingHit = timeUntilHit * movementSpeed / 1000;

            List<Vector3> walkingPositions = GetSafePositions(Player.Instance, DistancePlayerCanWalkToBeforeBeingHit);

            if(walkingPositions.Count > 0)
            {
                Vector3 safePos = walkingPositions.Where(a => a != null).OrderByDescending(a => a.DistanceFromClosestEnemy()).FirstOrDefault();
                if (safePos != null)
                {
                    Orbwalker.MoveTo(safePos);
                    safePosition = safePos;
                    return true;
                }
            }

            return false;
        }

        public static List<Vector3> GetSafePositions(Obj_AI_Base unit, float range)
        {
            Vector3 topLeftPos = Player.Instance.Position - new Vector3(range / 2, -range / 2, 0f);

            List<Vector3> walkingPositions = new List<Vector3>();

            for (float xoffset = 0; xoffset < range; xoffset = xoffset + range / 10)
                for (float yoffset = 0; yoffset < range; yoffset = yoffset + range / 10)
                {
                    Vector3 tempPos = topLeftPos + new Vector3(xoffset, -yoffset, 0);
                    if (tempPos.IsSafe())
                    {
                        walkingPositions.Add(tempPos);
                    }
                }
            return walkingPositions;
        }
    }
}
