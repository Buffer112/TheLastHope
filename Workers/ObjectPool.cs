/// Limerence Games
/// The Last Hope
/// Curator: Dmitri Kuzmin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Interfaces;

namespace TheLastHope.Workers
{
	/// <summary>
	/// The pool of objects.
	/// Stores all of the objects that implement IPoolable interface.
	/// </summary>
	public class ObjectPool
	{
		/// <summary>
		/// List of objects added to the Object Pool.
		/// </summary>
		private List<IPoolable> pooledObjects = new List<IPoolable>();

		/// <summary>
		/// Object Pool initialization method.
		/// </summary>
		/// <param name="size"></param>
		public ObjectPool(int size = 0)
		{
			pooledObjects = new List<IPoolable>(size);
		}

	}
}
