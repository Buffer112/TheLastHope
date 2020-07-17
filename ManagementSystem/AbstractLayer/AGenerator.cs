using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
	/// <summary>
	/// Abstract generator class.
	/// Use this class to create new object generators.
	/// </summary>
	public abstract class AGenerator : MonoBehaviour
	{
		/// <summary>
		/// Generation method that is to be used in Static Generators.
		/// </summary>
		/// <remarks>
		/// Generates objects from the object list and places them to free positions.
		/// </remarks>
		/// <param name="sceneData">A link to the Scene Data.</param>
		/// <seealso cref="SceneData">
		/// The object that gathers all of the data needed to 
		/// build a Scene and organize the main loop. 
		/// </seealso>
		/// <param name="objList">A list of objects to be generated.</param>
		/// <param name="positions">A list of positions that objects from the list
		/// are to be placed on.</param>
		public abstract void Generate(List<GameObject> objList, SceneData sceneData, List<Vector3> positions);
		/// <summary>
		/// Generation method that is to be used in Static Generators.
		/// </summary>
		/// <remarks>
		/// Generates objects from the object list and places them to free positions.
		/// </remarks>
		/// <param name="sceneData">A link to the Scene Data.</param>
		/// <seealso cref="SceneData">
		/// The object that gathers all of the data needed to 
		/// build a Scene and organize the main loop. 
		/// </seealso>
		/// <param name="objList">A list of objects to be generated.</param>
		/// <param name="positions">A list of positions that objects from the list
		/// are to be placed on.</param>
		public abstract void Generate(GameObject[] objects, SceneData sceneData);

	}
}
