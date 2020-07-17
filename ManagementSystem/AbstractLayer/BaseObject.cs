using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;


namespace TheLastHope.Management.AbstractLayer
{
    public abstract class ABaseObject : MonoBehaviour
    {

		public bool IsActive { get; set; }
        public bool IsDestructable { get; set; }
		public float MaxHealth { get; set; }
		public float Health { get; set; }

		public abstract void Init();
		public abstract void Init(SceneData sceneData);
        public abstract void SetDamage(float damage);

    }
}

