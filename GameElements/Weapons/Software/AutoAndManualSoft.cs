using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons.Software
{
    public class AutoAndManualSoft : ASoftware
    {
        public override Vector3 CalculateAim(Transform turPosition)
        {
            if (target != null)
            {
                targetingPosition = target.position;
                turrelPosition = turPosition;
                var eulerTargetRot = Quaternion.FromToRotation(turrelPosition.forward,
                                target.position - turrelPosition.position).eulerAngles.y;

                if (Mathf.Abs(eulerTargetRot) > 355 || Mathf.Abs(eulerTargetRot) < 5)
                    readyToFire = true;
                else
                    readyToFire = false;
            }
            else
                readyToFire = false;

            return targetingPosition;
        }


        public override void FindClosestTarget(SceneData sceneData)
        {
            Transform closest = null;
            float distance = visionRadius * visionRadius;
            foreach (GameObject go in sceneData.Enemies)
            {
                Vector3 diff = go.transform.position - gameObject.transform.position;
                float curDistance = diff.sqrMagnitude;

                if (curDistance < distance)
                {
                    closest = go.transform;
                    distance = curDistance;
                }
            }
            target = closest;
        }

        public override void Init()
        {
            readyToFire = false;
            enemyTag = "Enemy";
        }
    }
}
