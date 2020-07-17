using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TheLastHope.Management
{
    /// <summary>
    /// Class that contains static methods to get input data.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Returns point to which mouse is pointing now. (Point lays down at the edge of object.)
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetMousePosIn3D()
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            RaycastHit raycastHit;
            Physics.Raycast(ray, out raycastHit);
            return raycastHit.point;
        }
    }
}

