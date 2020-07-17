using TheLastHope.Management;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// Basic Turret - class
    /// </summary>
    public class BasicTurret : ATurret
    {
        #region Serialazed variables

        [SerializeField] private float maxHealth;

        #endregion

        #region Private variables

        private Vector3 aimingPoint;
        private ParticleSystem effect;

        #endregion

        #region Override methods

        /// <summary>
        /// BasicTurret 'Init'
        /// </summary>
        /// <param name="sceneData"></param>
        public override void Init(SceneData sceneData)
        {

        }
        /// <summary>
        /// BasicTurret 'Init'
        /// </summary>
        public override void Init()
        {
            IsActive = true;
            Weapon?.Init();
            soft.Init();
            if (Weapon != null) Weapon.MaxRange = soft.visionRadius;
            MaxHealth = maxHealth;
            Health = MaxHealth;
            effect = GetComponent<ParticleSystem>();
            effect?.Stop();
        }
        /// <summary>
        /// Turret is turning
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void TurnTurret(float deltaTime)
        {
            var eulerTargetRot = Quaternion.FromToRotation(transform.forward,
                            aimingPoint - transform.position).eulerAngles.y;
            var turningDir = 1;
            if (Mathf.Abs(eulerTargetRot) > 180)
                turningDir *= -1;
            if (Mathf.Abs(eulerTargetRot) < turningAngularSpeed * deltaTime)
            {
                gameObject.transform.rotation *= Quaternion.AngleAxis(eulerTargetRot * deltaTime, Vector3.up);
            }
            else
            {
                gameObject.transform.rotation *= Quaternion.AngleAxis(turningAngularSpeed * turningDir * deltaTime, Vector3.up);
            }
        }
        /// <summary>
        /// BasicTurret 'Update'
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="deltaTime"></param>
        public override void TurUpdate(SceneData sceneData, float deltaTime)
        {
            Weapon.WeaponUpdate();
            if (manualMode && soft.canBeManual)
            {
                aimingPoint = InputManager.GetMousePosIn3D();
                if (Input.GetButton("Fire1"))
                {
                    Weapon.Fire(sceneData);
                }
            }
            else
            {
                soft.FindClosestTarget(sceneData);
                aimingPoint = soft.CalculateAim(transform);

                if (soft.ReadyToFire)
                {
                    Weapon.Fire(sceneData);
                }
            }
            if (Weapon.WeaponState != WeaponState.Empty && Weapon.WeaponState != WeaponState.Off)
                TurnTurret(Time.deltaTime);

            if (Health <= 0) Die();
        }
        /// <summary>
        /// BasicTurret 'SetDamage'
        /// </summary>
        /// <param name="damage"></param>
        public override void SetDamage(float damage)
        {
            Health -= damage;
        }

        #endregion

        /// <summary>
        /// BasicTurret 'Die' - method
        /// </summary>
        public void Die()
        {
            effect?.Play();
            IsActive = false;
            Weapon.EffectsOff();
            Weapon.IsActive = false;

            gameObject.tag = "Untagged";
        }
    }
}