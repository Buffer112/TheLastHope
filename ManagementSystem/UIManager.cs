using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.UI;
using TheLastHope.Helpers;

namespace TheLastHope.Management
{

	public class UIManager : MonoBehaviour
	{
		[SerializeField]
		private Image _tempProgressBar;
		private float _tempWholePath;
		private float _tempCurrentPos;
		private UIOverlayController[] controllers;
		private Timer timer;

		[SerializeField]
		private Text text;


		// Start is called before the first frame update
		public void Init(SceneData sceneData)
		{
			_tempWholePath = sceneData.LineLength;
			controllers = FindObjectsOfType<UIOverlayController>();
			foreach (UIOverlayController controller in controllers)
			{
				sceneData.UiOverlayControllers.Add(controller);
				controller.Init();
			}
			timer = new Timer();
			timer.Start(5);
		}

		public void UIUpdate(SceneData sceneData, PathLengthCounter pathCounter)
		{      
			foreach (UIOverlayController controller in sceneData.UiOverlayControllers)
			{
                controller.OverlayUpdate(sceneData);
				_tempProgressBar.fillAmount = (float)pathCounter.CurrentLine / (float)sceneData.LinesOverall;
			}

			timer.TimerUpdate();

		//	if (timer.Elapsed < 0) text.CrossFadeAlpha(0, 1, false);
		}
	}
}