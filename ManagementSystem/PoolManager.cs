using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Workers;
using TheLastHope.Management.Data;

public class PoolManager : MonoBehaviour
{

	private Dictionary<PoolType, ObjectPool> pools = new Dictionary<PoolType, ObjectPool>(); 

	public ObjectPool AddPool (PoolType id, int size = 0, bool reparent = true)
	{
		var objPool = new ObjectPool(size);
		pools.Add(id, objPool);

		if (reparent)
		{
			var poolsCheck = GameObject.Find("[ObjectPools]") ?? new GameObject("[ObjectPools]");
			var poolToReparent = new GameObject("Pool: " + id);
			poolToReparent.transform.SetParent(poolsCheck.transform);
		}

		return objPool;
	}

}
