using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Helpers
{
	public class Banana : SaveableBanana
	{
		private ABaseObject baseObject;

		void Init()
		{
			baseObject = GetComponent<ABaseObject>();
		}

		public override void Save(int id)
		{
			base.Save(id);
		}

		public override void Load(string[] values)
		{
			base.Load(values);
		}
	}
}