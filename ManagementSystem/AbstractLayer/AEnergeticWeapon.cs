using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    ///  'AEnergeticWeapon' class
    ///  realizes basic bahaviour of the energetic weapon
    ///  Fire() calls WeaponMethod() method that is to be overriden in every 
    /// </summary>
    public abstract class AEnergeticWeapon : AWeapon
    {
        [Header("Some properties"), SerializeField] protected LineRenderer LR;
        protected bool isPlaying;

        #region Override methods

        /// <summary>
        /// Energetic weapon 'Init'
        /// </summary>
        public override void Init()
        {
            base.Init();
            isPlaying = false;
            LR.enabled = false;
            ammoType = AmmoType.Energy;
        }
        /// <summary>
        /// Energetic weapon 'Checks'
        /// </summary>
        protected override void Checks()
        {
            if (!IsActive || WeaponState != WeaponState.Firing)
            {
                EffectsOff();
            }

            base.Checks();
        }
        /// <summary>
        /// AEnergeticWeapon's 'EffectsOff'
        /// </summary>
        public override void EffectsOff()
        {
            base.EffectsOff();
            LR.enabled = false;
            isPlaying = false;
        }

        #endregion
    }
}