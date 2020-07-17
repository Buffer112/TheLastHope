using TheLastHope.Interfaces;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    /// Abstract ammunition class.
    /// Use this class to create new versions of ammunition.
    /// </summary>

    [RequireComponent(typeof(Collider))]
    public abstract class AAmmo : MonoBehaviour, IPoolable
    {
        #region Serializables
        /// <summary>
        /// The damage that ammunition applies.
        /// </summary>
        [SerializeField] protected float damage;
        /// <summary>
        /// Speed of the guided ammunition.
        /// </summary>
        [SerializeField] private float speed;
        /// <summary>
        /// Maximal magnitude of the guided ammunition.
        /// </summary>
        [SerializeField] private float maxMagnitude;
        #endregion

        #region Public Properties
        /// <summary>
        /// Speed of the guided ammunition.
        /// </summary>
        public float Speed { get { return speed; } set { speed = value; } }
        /// <summary>
        /// Maximal magnitude of the guided ammunition.
        /// </summary>
        public float MaxMagnitude { get { return maxMagnitude; } set { maxMagnitude = value; } }
        /// <summary>
        /// Starting point transform of the guided ammunition.
        /// </summary>
        public Vector3 StartPoint { get; set; }
        #endregion

        #region Abstract Functions
        /// <summary>
        /// Method called when the object populates from Object Pool.
        /// </summary>
        /// <seealso cref="Workers.ObjectPool">
        /// Object pool is to be implemented. 
        /// </seealso>
        public abstract void OnPopulate();
        /// <summary>
        /// Method called when the object depopulates 
        /// and coming back to Object Pool.
        /// </summary>
        /// <seealso cref="TheLastHope.Workers.ObjectPool">
        /// Object pool is to be implemented. 
        /// </seealso>
        public abstract void OnDepopulate();
        /// <summary>
        /// Method called when the ammo
        /// touches a trigger.
        /// </summary>
        protected abstract void OnTriggerEnter(Collider collision);
        #endregion
    }
}