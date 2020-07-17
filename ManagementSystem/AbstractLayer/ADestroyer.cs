using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
	/// <summary>
	/// Abstract destroyer class.
	/// Use this class to create new versions of 
	/// the classes that destroy something.
	/// </summary>
	public abstract class ADestroyer : MonoBehaviour
    {
		#region Abstract methods
		/// <summary>
		/// Method that implements elimination of the object.
		/// </summary>
		/// <remarks>
		/// Takes sceneData on input.
		/// </remarks>
		/// <seealso cref="TheLastHope.Workers.ObjectPool">
		/// The better practice is to implement using ObjectPool.
		/// </seealso>
		/// <param name="sceneData"> Link to the SceneData object.</param>
		public abstract void Destroy(SceneData sceneData);
		#endregion
	}
}

