using TheLastHope.Management.Data;
using TheLastHope.Weapons.Software;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    /// Turret - core class
    /// </summary>
    public abstract class ATurret : ABaseObject
    {
        #region Serialized variables

        [SerializeField] private AWeapon weapon;
        [SerializeField] internal ASoftware soft;
        [SerializeField] internal bool manualMode = false;
        [SerializeField] internal float turningAngularSpeed;

        #endregion
        public AWeapon Weapon { get => weapon; set => weapon = value; }

        #region Abstruct methods

        public abstract void TurnTurret(float deltaTime);

        public abstract void TurUpdate(SceneData sceneData, float deltaTime);

        #endregion


        public virtual void SwitchMode()
        {
            if (soft.canBeManual) manualMode = !manualMode;
        }
    }
}

