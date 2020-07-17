using TheLastHope.Management.AbstractLayer;
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// 'AEnergeticWeapon' - class. 
    /// </summary>
    public class LaserGun : AEnergeticWeapon
    {
        #region Override methods

        protected override void WeaponMethod(RaycastHit hit)
        {
            isPlaying = true;
            damageEffect.transform.SetPositionAndRotation(hit.transform.position, Quaternion.Euler(hit.point));

            HitTheEnemy(hit);
            SetLRToTarget(hit);
        }
        #endregion

        #region Private methods

        private void SetLRToTarget(RaycastHit hit)
        {
            LR.enabled = true;
            LR.SetPosition(0, muzzle.position);
            LR.SetPosition(1, new Vector3(hit.transform.position.x, hit.transform.position.y + 3, hit.transform.position.z));
        }

        private void HitTheEnemy(RaycastHit hit)
        {
            hit.transform.GetComponent<AEnemy>().SetDamage(damage * Time.deltaTime);
            currentAmmoInClip = CurrentAmmoInClip - ammoPerShot * Time.deltaTime;
        }

        #endregion
    }
}