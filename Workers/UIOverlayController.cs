/// Limerence Games
/// The Last Hope
/// Curator: Dmitri Kuzmin
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Player;
using TheLastHope.Management.Data;
using TheLastHope.Management;
using TheLastHope.Helpers;
using TheLastHope.Weapons;

namespace TheLastHope.UI {

	public class UIOverlayController : MonoBehaviour
	{
		private float health;
		private ABaseObject baseObject;
		private float startHealth;
		private float healthBarValue;
		private float ammoPrefabBarValue;
		private UIObjectOverlay overlay;
		private ObjType currentType;
		private Vector3 overlaySize;
		private bool isUnderControl;
		private float oldHealth;
		private Timer timer;
		private IEnumerator coroutine;
        GameState gameState = GameState.Wait;

		[SerializeField]
		private bool showOnDamage;
		[SerializeField]
		private float showTime = 1.0f;

		public void Init()
		{
			baseObject = GetComponentInChildren<ABaseObject>();
			if (!baseObject) GetComponent<ABaseObject>();
			healthBarValue = baseObject.MaxHealth;
			overlay = GetComponentInChildren<UIObjectOverlay>();
			if (overlay) print(gameObject.name + " has an overlay!");
			oldHealth = baseObject.Health;

			if (baseObject.GetComponentInChildren<AEnemy>()) currentType = ObjType.Enemy;
			else if (baseObject.GetComponentInChildren<ATurret>()) currentType = ObjType.Turret;
			else if (baseObject.GetComponent<MainPlayer>()) currentType = ObjType.Loco;
			//print("I am " + _baseObject.name + " and my type is " + _currentType);
			HideOverlay();
			timer = new Timer();
		}

		public void OverlayUpdate(SceneData sceneData)
		{
			if (gameState != GameState.Loop) HideOverlay();
			if ((baseObject.Health != oldHealth) && showOnDamage && baseObject.IsActive)
			{
				coroutine = DamageUI(showTime);
				StartCoroutine(coroutine);
			}

			if (currentType == ObjType.Turret)
			{
				CountAmmo();
				ShowOverlay();
			}
            gameState = sceneData.CurrentState;
		}


		public void CountHealth()
		{
			if (baseObject)
			{
				healthBarValue = baseObject.Health / baseObject.MaxHealth;
				//print("Buggy Health is " + _healthBarValue);
				overlay.CurrentHealth = healthBarValue;
			}
		}

		public void CountAmmo()
		{
			if (baseObject && currentType == ObjType.Turret)
			{
				ammoPrefabBarValue = baseObject.GetComponentInChildren<AWeapon>().CurrentAmmoInClip / baseObject.GetComponentInChildren<AWeapon>().ClipSize;
				overlay.CurrentAmmo = ammoPrefabBarValue;
			}
		}


		private void OnMouseEnter()
		{
            //print("Mouse Over!");
            if (gameState==GameState.Loop)
            {
                CountHealth();
				CountAmmo();
				ShowOverlay();
			}

		}

		private void OnMouseExit()
		{
			HideOverlay();
			//print("Mouse Exit!");
		}

		//private void OnMouseDown()
		//{
		//	//ControlButton();
		//}

		public void ShowOverlay()
		{
            if (currentType == ObjType.Enemy) overlay.ShowOverlay(true, true);
            else if (currentType == ObjType.Loco) overlay.ShowOverlay(true, true); 
			else if (currentType == ObjType.Turret)
			{
				if (ammoPrefabBarValue == 0) overlay.ShowOverlay(true, true, true, true, true);
				else overlay.ShowOverlay(true, true, true, true);
			}
		}

		public void HideOverlay()
		{
            if (overlay) overlay.HideOverlay();
            //else print("Overlay not found!");
        }

		public void ControlButton()
		{
			if (currentType == ObjType.Turret && !isUnderControl && baseObject.GetComponentInChildren<ATurret>().soft.canBeManual)
			{
				isUnderControl = true;
				baseObject.GetComponentInChildren<ATurret>().SwitchMode();
			}

			else
			{
				isUnderControl = false;
				baseObject.GetComponentInChildren<ATurret>().SwitchMode();
			}
		}

		private IEnumerator DamageUI(float waitTime)
		{
			CountHealth();
			ShowOverlay();
			yield return new WaitForSeconds(waitTime);
			HideOverlay();
			oldHealth = baseObject.Health;
		}
	}

}
