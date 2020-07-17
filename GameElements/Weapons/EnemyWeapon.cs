using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using TheLastHope.Player;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class EnemyWeapon : ARangedWeapon
    {
        protected override void WeaponMethod(RaycastHit hit)
        {
            currentAmmoInClip -= ammoPerShot;

            damageEffect.transform.SetPositionAndRotation(hit.transform.position, Quaternion.Euler(hit.point));

            if (hit.transform?.GetComponent<ATurret>())
                hit.transform?.GetComponent<ATurret>()?.SetDamage(damage);
            if (hit.transform?.GetComponent<MainPlayer>())
                hit.transform?.GetComponent<MainPlayer>()?.SetDamage(damage);

            delay.Start(rateOfFire);
            EffectsOn();
        }

        protected override void Checks()
        {
            base.Checks();
            if (WeaponState == WeaponState.Empty)
            {
                currentAmmoInClip = 100;
            }
        }
    }
}