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
        public static void Initialize()
        {

        }

        public static void HandleDodging()
        {
            if(!Player.Instance.IsSafe())
            {
                if(!TryToMoveToSafePosition())
                {
                    //use spells to evade
                }
            }
        }

        public static bool TryToMoveToSafePosition()
        {
            //timeUntilHit in form of MS
            float timeUntilHit = Player.Instance.FindSpellInfoWithClosestTime().TimeUntilHitsChampion(Player.Instance);
            float movementSpeed = Player.Instance.CharData.MoveSpeed;
            //d = ts/1000
            float DistancePlayerCanWalkToBeforeBeingHit = timeUntilHit * movementSpeed / 1000;
            Geometry.Polygon walkingDistanceAroundPlayer = new Geometry.Polygon.Circle(Player.Instance.Position, DistancePlayerCanWalkToBeforeBeingHit);

            Vector3 topLeftPos = Player.Instance.Position - new Vector3(DistancePlayerCanWalkToBeforeBeingHit / 2, -DistancePlayerCanWalkToBeforeBeingHit / 2, 0f);

            List<Vector3> walkingPositions = new List<Vector3>();

            for(float xoffset = 0; xoffset < DistancePlayerCanWalkToBeforeBeingHit; xoffset++)
                for (float yoffset = 0; yoffset < DistancePlayerCanWalkToBeforeBeingHit; yoffset++)
                {
                    Vector3 tempPos = topLeftPos + new Vector3(xoffset, -yoffset, 0);
                    if (tempPos.IsSafe())
                        walkingPositions.Add(tempPos);
                }

            if(walkingPositions.Count > 0)
            {
                Orbwalker.MoveTo(walkingPositions.Where(a=> a!= null).OrderBy(a =>a.DistanceFromClosestEnemy()).FirstOrDefault());
            }

            return false;
        }
    }
}
