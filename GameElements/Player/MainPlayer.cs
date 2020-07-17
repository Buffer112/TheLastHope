using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management;
using TheLastHope.Weapons;
using TheLastHope.Management.AbstractLayer;
using System;

namespace TheLastHope.Player
{

	public class MainPlayer: ABaseObject
	{
		[SerializeField]
		private float raycastDepth;
		[SerializeField]
		private float health;
		[SerializeField]
		private float maxHealth;

		//TEMPORARY RESET SOLUTION. TODO: implement the right way
		[SerializeField] List<GameObject> initObjects = new List<GameObject>();

		public override void Init()
		{
			base.IsActive = true;
            health = maxHealth;

			for (var i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}

			foreach (var obj in initObjects)
			{
				obj.SetActive(true);
			}
		}

		public override void Init(SceneData sceneData)
		{
			throw new System.NotImplementedException();
		}

		public override void SetDamage(float damage)
		{
			health -= damage;
		}

		public void UpdatePlayer(SceneData sceneData) //Temp
		{
			base.MaxHealth = maxHealth;
			base.Health = health;
			if (RayCast()) sceneData.Triggers.Add(RayCast());
			if (health < 1)
			{
				Die(sceneData);
			}

		}

		private GameObject RayCast()
		{
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hitInfo = new RaycastHit();
			Debug.DrawLine(transform.position, transform.forward*raycastDepth, Color.red);
			if (Physics.Raycast(ray, out hitInfo, raycastDepth) && hitInfo.collider.isTrigger)
			{
				print("I had hit a trigger!");
				return hitInfo.collider.gameObject;
			}
			else
			{
                //if (Physics.Raycast(ray, out hitInfo, _raycastDepth))print($"I hit {hitInfo.collider.gameObject.name}");
				return null;
			}
		}

		private void Die(SceneData sceneData)
		{
			sceneData.CurrentState = GameState.Lose;
			this.GetComponent<AudioSource>().clip = null;
			int childCount = gameObject.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				var child = gameObject.transform.GetChild(i);
				if (!child.gameObject.active) child.gameObject.SetActive(true); 
			}
			base.IsActive = false;
		}

	}
}
