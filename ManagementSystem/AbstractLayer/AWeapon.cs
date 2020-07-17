using System.Collections.Generic;
using TheLastHope.Helpers;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    /// Abstract weapon class.
    /// </summary>
    /// <remarks>
    /// Use this class to create new types of weapons (e.g. projectiles, ranged, etc.).
    /// </remarks>
    public abstract class AWeapon : MonoBehaviour
    {
        #region Serializables

        [Header("Weapon's property"), SerializeField] protected AmmoType ammoType;
        [SerializeField] protected float damage;
        [SerializeField] protected float ammoPerShot;
        [SerializeField] private float clipSize;
        [SerializeField] private float reloadTime;
        [SerializeField] protected GameObject damageEffect;
        [SerializeField] protected GameObject fireEffect;
        [SerializeField] protected Transform muzzle;

        protected WeaponState weaponState;
        protected AudioSource audioSource;
        protected Timer delay = new Timer();
        protected float currentAmmoInClip;
        protected Animator animator;
        protected List<ParticleSystem> firePS = new List<ParticleSystem>();
        protected List<ParticleSystem> hitPS = new List<ParticleSystem>();




        #endregion

        #region Public properties
        /// <summary>
        /// Type of ammunitions that the weapon utilies.
        /// </summary>
        public AmmoType AmmoType { get { return ammoType; } }
        /// <summary>
        /// The state of the weapon.
        /// </summary>
        /// <seealso cref="Data.WeaponState">
        /// Enum that enumerates the weapon states.
        /// </seealso>
        public WeaponState WeaponState { get { return weaponState; } }

        /// <summary>
        /// The general object state. 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// The weapon's clip capacity. 
        /// </summary>
        public float ClipSize { get { return clipSize; } set { clipSize = value; } }
        /// <summary>
        /// Max range of weapon
        /// </summary>
        public float MaxRange { get; set; }
        /// <summary>
        /// Current ammo amount in weapon
        /// </summary>
        public float CurrentAmmoInClip { get { return currentAmmoInClip; } }

        #endregion

        #region Abstract methods
        protected abstract void WeaponMethod(RaycastHit hit);
        #endregion

        #region Virual methods
        /// <summary>
        /// Core weapon's 'Init'
        /// </summary>
        public virtual void Init()
        {
            IsActive = true;
            weaponState = WeaponState.ReadyToFire;
            currentAmmoInClip = clipSize;
            delay = new Timer();

            animator = GetComponentInParent<Animator>();
            audioSource = GetComponent<AudioSource>();
            audioSource?.Stop();

            damageEffect?.SetActive(true);
            foreach (var ps in damageEffect.GetComponentsInChildren<ParticleSystem>())
            {
                hitPS.Add(ps);
                ps.Stop();
            }

            fireEffect?.gameObject.SetActive(true);
            foreach (var ps in fireEffect.GetComponentsInChildren<ParticleSystem>())
            {
                firePS.Add(ps);
                ps.Stop();
            }
        }
        /// <summary>
        /// Core weapon 'Update'
        /// </summary>
        public virtual void WeaponUpdate()
        {
            Checks();
        }

        #endregion
        /// <summary>
        /// 'Reload' this weapon
        /// </summary>
        /// <param name="ammoQuantity"></param>
        public void Reload(int ammoQuantity)
        {
            if (WeaponState != WeaponState.Empty)
                return;
            print(ammoQuantity);
            currentAmmoInClip = ammoQuantity;
            delay.Start(reloadTime);
        }
        /// <summary>
        /// Core weapon's 'Fire'
        /// </summary>
        /// <param name="sceneData"></param>
        public virtual void Fire(SceneData sceneData)
        {
            if (WeaponState != WeaponState.ReadyToFire)
                return;

            weaponState = WeaponState.Firing;
            animator?.SetTrigger("Fire");

            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit))
            {
                if (hit.distance <= MaxRange)
                {
                    if (hit.transform.GetComponent<AEnemy>())
                    {
                        WeaponMethod(hit);
                    }
                }
            }

            EffectsOn();
        }
        /// <summary>
        /// 'Fire'-attack for enemy
        /// </summary>
        /// <param name="hit"></param>
        public virtual void FireIntoThePlayer(RaycastHit hit)
        {
            if (WeaponState != WeaponState.ReadyToFire)
                return;

            weaponState = WeaponState.Firing;
            WeaponMethod(hit);
            EffectsOn();
        }
        /// <summary>
        /// Core weapon checks
        /// </summary>
        protected virtual void Checks()
        {
            if (!IsActive)
            {
                EffectsOff();
                return;
            }

            delay.TimerUpdate();
            if (CurrentAmmoInClip > 0 && delay.Elapsed < 0)
            {
                weaponState = WeaponState.ReadyToFire;
            }

            if (CurrentAmmoInClip <= 0)
            {
                currentAmmoInClip = 0;
                weaponState = WeaponState.Empty;
            }


            if (WeaponState == WeaponState.Empty)
            {
                animator?.SetTrigger("Reload");
            }
            if (WeaponState != WeaponState.Firing)
            {
                EffectsOff();
                animator?.SetTrigger("Idle");
            }
        }
        /// <summary>
        /// Core OnFire effects
        /// </summary>
        protected virtual void EffectsOn()
        {
            audioSource?.Play();

            foreach (var ps in hitPS)
            {
                if (!ps.isPlaying)
                {
                    ps?.Play();
                }
            }
            foreach (var ps in firePS)
            {
                ps?.Play();
            }
        }


        /// <summary>
        /// Core 'EffectsOff'
        /// </summary>
        public virtual void EffectsOff()
        {
            audioSource?.Stop();

            foreach (var ps in hitPS)
            {
                ps?.Stop();
            }
            foreach (var ps in firePS)
            {
                ps?.Stop();
            }
        }
    }
}

