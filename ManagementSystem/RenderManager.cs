using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Render;
using System.Collections.Generic;
using System;

namespace TheLastHope.Management
{
	public class RenderManager : MonoBehaviour
	{
		private Camera _currentCam;
		private CameraController _camController;

		public void Init()
		{
			_currentCam = Camera.main;
			_camController = _currentCam.GetComponent<CameraController>();
			_camController.Init();
		}
		/// <summary>
		/// Controls all of the rendering routine.
		/// </summary>
		/// <param name="sceneData"></param>
		public void UpdateRender(SceneData sceneData)
		{
			MoveCamera(_currentCam);

			if (sceneData.TrainSpeed == 0) CameraShake(_currentCam);
		}

		private void MoveCamera(Camera currentCam)
		{
			if (currentCam.GetComponent<CameraController>()) _camController.CameraUpdate();
		}

		private void CameraShake(Camera currentCam)
		{
			if (currentCam.GetComponent<CameraController>()) _camController.CameraShake();
		}
	}
}