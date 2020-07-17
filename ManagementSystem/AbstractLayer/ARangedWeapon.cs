using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    ///  ARangedWeapon class
    ///  realizes basic bahaviour of the ranged weapon
    ///  Fire() calls Shot() method that is to be overriden in every 
    /// </summary>
    public class ARangedWeapon : AWeapon
    {
        [Header("Some properties"), SerializeField] protected float rateOfFire;

        protected override void WeaponMethod(RaycastHit hit)
        {
            currentAmmoInClip -= ammoPerShot;

            damageEffect.transform.SetPositionAndRotation(hit.transform.position, Quaternion.Euler(hit.point));
            hit.transform.GetComponent<AEnemy>().SetDamage(damage);
            delay.Start(rateOfFire);
        }
    }
}
