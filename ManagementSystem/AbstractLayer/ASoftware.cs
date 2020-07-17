using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons.Software
{
    /// <summary>
    /// Software - core class
    /// </summary>
    public abstract class ASoftware : MonoBehaviour
    {
        #region Parametrs
        [SerializeField] internal float visionRadius = 10;
        [SerializeField] internal bool canBeManual = false;
        internal string enemyTag;
        internal Transform target;
        private Vector3 previousTargetPosition;
        internal bool readyToFire;
        internal Vector3 targetingPosition = new Vector3(0, 0, 0);
        internal Transform turrelPosition;
        private float projectileSpeed;

        public bool ReadyToFire { get => readyToFire; set => readyToFire = value; }
        public Transform Target { get => target; set => target = value; }
        #endregion

        #region Abstruct methods

        public abstract void Init();
        public abstract Vector3 CalculateAim(Transform turPosition);
        public abstract void FindClosestTarget(SceneData sceneData);

        #endregion

        public virtual void SwitchMode()
        {
            canBeManual = !canBeManual;
        }
    }
}
