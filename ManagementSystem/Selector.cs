using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Management
{
    /// <summary>
    /// Class that makes select gameObjects easier.
    /// </summary>
    public class Selector : MonoBehaviour
    {
        /// <summary>
        /// Returns selected gameObject.
        /// </summary>
        /// <returns></returns>
        public GameObject GetSelectedGameObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            Physics.Raycast(ray, out raycastHit, Mathf.Infinity);
            if (raycastHit.collider != null)
            {
                return raycastHit.collider.gameObject;
            }
            else
            {
                return this.gameObject;
            }
        }
    }
}

