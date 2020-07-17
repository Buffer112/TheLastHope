/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov
/// Author: Nikolay Pankrakhin

using TheLastHope.Management;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons.Software
{
    /// <summary>
    /// Manual Control Soft
    /// </summary>
    public class ManualControlSoft : ASoftware
    {
        #region Override methods

        /// <summary>
        /// Calculate vector for return
        /// </summary>
        /// <param name="turPosition"></param>
        /// <returns></returns>
        public override Vector3 CalculateAim(Transform turPosition)
        {
            ReadyToFire = Input.GetButton("Fire1");
            return InputManager.GetMousePosIn3D();
        }
        /// <summary>
        /// Findthe closest target
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
        /// ManualControlSoft 'Init'
        /// </summary>
        public override void Init()
        {
            canBeManual = false;
            readyToFire = false;
        }

        #endregion

    }
}
