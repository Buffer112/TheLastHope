/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov
/// Author: Nikolay Pankrakhin

using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons.Software
{
    /// <summary>
    /// Primitive Control Soft
    /// </summary>
    public class PrimitiveControlSoft : ASoftware
    {
        #region Override methods

        /// <summary>
        /// Calculate vector for return
        /// </summary>
        /// <param name="turPosition"></param>
        /// <returns></returns>
        public override Vector3 CalculateAim(Transform turPosition)
        {
            //По умолчанию турель стреляет прямо по цели, но, если цель движется, то нужно высчитать точку,
            //которая находится перед движущейся целью и по которой будет стрелять турель.
            //То есть турель должна стрелять на опережение
            if (target != null)
            {
                targetingPosition = target.position;
                turrelPosition = turPosition;
                float eulerTargetRot = Quaternion.FromToRotation(turrelPosition.forward,
                    target.position - turrelPosition.position).eulerAngles.y;

                //Проверяем навелись мы или нет
                if (Mathf.Abs(eulerTargetRot) > 355 || Mathf.Abs(eulerTargetRot) < 5)
                {
                    readyToFire = true;
                }
                else
                {
                    readyToFire = false;
                }
            }
            else
            {
                readyToFire = false;
            }

            return targetingPosition;
        }
        /// <summary>
        /// Find the closest target
        /// </summary>
        /// <param name="sceneData"></param>
        public override void FindClosestTarget(SceneData sceneData)
        {
            //Ближайшая цель, попавшая в радиус обзора
            Transform closest = null;
            //Квадрат радиуса обзора, это значение потребуется при поиске ближайшей цели
            float distance = visionRadius;
            foreach (GameObject go in sceneData.Enemies)
            {
                //Находим расстояние между турелью и предполагаемой целью
                Vector3 diff = go.transform.position - gameObject.transform.position;
                //С точки зрения производительности быстрее сравнить квадраты расстояний,
                //чем делать лишнюю операцию извлечения квадратного корня
                float curDistance = diff.sqrMagnitude;
                //если найдена цель в радиусе поражения, то запоминаем её
                if (curDistance < distance)
                {
                    closest = go.transform;
                    distance = curDistance;
                }
            }
            target = closest;
        }
        /// <summary>
        /// PrimitiveControlSoft 'Init'
        /// </summary>
        public override void Init()
        {
            canBeManual = false;
            readyToFire = false;
        }

        #endregion
    }
}
