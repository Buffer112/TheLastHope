using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
    internal enum UpOrDownType
    {
        Up,
        Down
    }
    /// <summary>
    /// Abstract enemy class.
    /// Use this class to create new enemies.
    /// </summary>
    public abstract class AEnemy : ABaseObject
    {
		#region Serializables
		/// <summary>
		/// The maximal health level of the enemy.
		/// </summary>
		[SerializeField] internal float maxHealth;
		#endregion

		#region Protected variables
		/// <summary>
		/// The object that enemy tries to reach.
		/// </summary>
		protected Transform target;
		/// <summary>
		/// The position that enemy needs to occupy.
		/// </summary>
		protected Transform targetPosition;
		/// <summary>
		/// Type of the enemy.
		/// </summary>
		///<remarks>
		/// Can be used to aim selected turrets to selected enemy type.
		/// </remarks>
		protected EnemyType enemyType;
		#endregion

		#region Abstract methods
		public abstract void EnemyUpdate(SceneData sceneData, float deltaTime);
        public abstract void Die();
		#endregion
    }
}

