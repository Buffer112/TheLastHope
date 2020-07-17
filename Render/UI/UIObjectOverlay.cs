using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Render;
using UnityEngine.UI;
using TheLastHope;
using TheLastHope.Management;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.UI {
	public class UIObjectOverlay : MonoBehaviour
	{
		[SerializeField]
		private Canvas frame;
		[SerializeField]
		private Canvas healthBar;
		[SerializeField]
		private Canvas ammoPrefabBar;
		[SerializeField]
		private Canvas reloadBar;
		[SerializeField]
		private Canvas buttonBar;
		[SerializeField]
		private Image healthBarImage;
		[SerializeField]
		private Image ammoPrefabBarImage;
		private float currentHealth = 0;
		private float currentAmmo = 0;
		private Vector3 overlaySize;

		public float CurrentHealth {set => currentHealth = value; }
		public Vector3 OverlaySize { get => overlaySize; set => overlaySize = value; }
		public float CurrentAmmo { set => currentAmmo = value; }


		private void Start()
		{
			getInfo();
			HideOverlay();
		}

		private void getInfo()
		{	
				healthBarImage.fillAmount = currentHealth;
				if(ammoPrefabBarImage) ammoPrefabBarImage.fillAmount = currentAmmo;
		}

		public void ShowOverlay(bool frame)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
		}

		public void ShowOverlay(bool frame, bool health)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
			if (health) { healthBar.enabled = true; }
		}
		public void ShowOverlay(bool frame, bool health, bool button)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
			if (health) { healthBar.enabled = true; }
			if (button) { buttonBar.enabled = true; }
		}

		public void ShowOverlay(bool frame, bool health, bool button, bool ammo)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
			if (health) { healthBar.enabled = true; }
			if (button) { buttonBar.enabled = true; }
			if (ammo) { ammoPrefabBar.enabled = true;  }
		}

		public void ShowOverlay(bool frame, bool health, bool button, bool ammo, bool reload)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
			if (health) { healthBar.enabled = true; }
			if (button) { buttonBar.enabled = true; }
			if (ammo) { ammoPrefabBar.enabled = true; }
			//if (reload) { reloadBar.enabled = true; }
		}

		public void HideOverlay()
		{
			frame.enabled = false;
			healthBar.enabled = false;
			buttonBar.enabled = false;
			if (reloadBar) reloadBar.enabled = false;
			if (ammoPrefabBar) ammoPrefabBar.enabled = false;
			//if (ammoPrefabBarImage) ammoPrefabBarImage.enabled = false;
		}


	}
}