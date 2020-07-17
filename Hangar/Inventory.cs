using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastHope.Hangar
{
    /// <summary>
    /// UI inventory controller
    /// </summary>
    public class Inventory : MonoBehaviour
    {
        #region Private variables
        /// <summary>
        /// Temporary array for initialization items in inventory
        /// </summary>
        [SerializeField] private GameObject[] objPool;
        /// <summary>
        /// List of items in inventory
        /// </summary>
        private List<GameObject> inventory;
        #endregion

        void Filling()
        {
            inventory = new List<GameObject>();
            for (int i = 0; i < objPool.Length; i++)
            {
                inventory.Add(Instantiate(objPool[i], transform.GetChild(i).transform));
                //inventory[i].GetComponent<Item>().controller = HangarData.instance.positionController;
                inventory[i].GetComponent<Item>().Init();
            }
        }

        public void Init()
        {
            Filling();
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).childCount > 0)
                {
                    transform.GetComponentsInChildren<Slot>()[i].isVacant = false;
                }
            }
        }
    }
}

