using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// 'AEnergeticWeapon' - class
    /// </summary>
    public class ElectroGun : AEnergeticWeapon
    {
        [SerializeField] private float radiusElectro;

        private List<GameObject> allEnemies;
        private List<AEnemy> nearestEnemies;

        #region Override methods

        /// <summary>
        /// ElectroGun 'Init'
        /// </summary>
        public override void Init()
        {
            base.Init();
            nearestEnemies = new List<AEnemy>();
        }
        /// <summary>
        /// ElectroGun 'Fire'
        /// </summary>
        /// <param name="sceneData"></param>
        public override void Fire(SceneData sceneData)
        {
            allEnemies = sceneData.Enemies;
            base.Fire(sceneData);
        }
        /// <summary>
        /// ElectroGun 'Update'
        /// </summary>
        /// <param name="hit"></param>
        protected override void WeaponMethod(RaycastHit hit)
        {
            isPlaying = true;
            damageEffect.transform.SetPositionAndRotation(hit.transform.position, Quaternion.Euler(hit.point));

            FindTheNearestEnemies(hit.transform.GetComponent<AEnemy>());
            HitTheEnemies();
            SetLRToTarget(hit);
        }
        /// <summary>
        /// ElectroGun 'Checks'
        /// </summary>
        protected override void Checks()
        {
            base.Checks();

            if (WeaponState != WeaponState.Firing)
            {
                LR.positionCount = 2;
                nearestEnemies.Clear();
            }
        }



        #endregion

        #region Private methods

        private void SetLRToTarget(RaycastHit hit)
        {
            var enemyPosition = hit.point;
            LR.enabled = true;

            LR.SetPosition(0, muzzle.position);
            LR.SetPosition(1, enemyPosition);

            LR.positionCount = nearestEnemies.Count;
            if (nearestEnemies.Count > 0)
            {
                foreach (var enemy in nearestEnemies)
                {
                    LR.positionCount++;
                    LR.SetPosition(LR.positionCount - 1, new Vector3(enemy.transform.position.x,
                                                                               enemy.transform.position.y + 3,
                                                                               enemy.transform.position.z));
                }
            }
        }

        private void HitTheEnemies()
        {
            if (nearestEnemies.Count > 0)
            {
                foreach (var enemy in nearestEnemies)
                {
                    enemy.SetDamage(damage * Time.deltaTime);
                    currentAmmoInClip = CurrentAmmoInClip - ammoPerShot * Time.deltaTime;
                }
            }
        }

        private void FindTheNearestEnemies(AEnemy firstEnemy)
        {
            foreach (var enemy in allEnemies)
            {
                var tempEnemy = enemy?.GetComponent<AEnemy>();
                var tempDistance = Vector3.Distance(enemy.transform.position, firstEnemy.transform.position); //Если эта операция будет сильно напрягать память, можно считать дистанцию вручную.

                if (radiusElectro >= tempDistance && !nearestEnemies.Contains(tempEnemy))
                    nearestEnemies.Add(tempEnemy);
                if (radiusElectro < tempDistance && nearestEnemies.Contains(tempEnemy))
                    nearestEnemies.Remove(tempEnemy);
            }
        }

        #endregion
    }
}