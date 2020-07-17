using System.Collections.Generic;
using System;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Generators
{
	/// <summary>
	/// Generator of static objects
	/// </summary>
	class StaticGenerator : MonoBehaviour

	{
		#region Temporary variables of local pattern
		/// <summary>
		/// local temporary pattern 
		/// </summary>
		[SerializeField] private Vector3[] positions;
		int lineCount = 5;
		int currentLine = 0;
		#endregion

		/// <summary>
		/// Generate objects from array by (local) pattern positions
		/// </summary>
		/// <param name="objects">ObjectPool with object's position pattern</param>
		/// <param name="sceneData"></param>
		public void Generate(GameObject[] objects, SceneData sceneData)
		{
			if (sceneData.CurrentLine > currentLine)
			{
				float exactX = sceneData.Props[sceneData.Props.Count - 1].transform.position.x + 10;
				for (var i = 0; i < lineCount; i++)
				{
					positions[i] = new Vector3(exactX, positions[i].y, positions[i].z);
				}
				currentLine = sceneData.CurrentLine;
			}
			for (int i = 0; i < lineCount; i++)
			{
				if (objects[i] != null)
				{
					sceneData.Props.Add(Instantiate(objects[i], positions[i], Quaternion.identity));
				}
			}
		}
	}
}
