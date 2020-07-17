using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastHope.Hangar
{
    /// <summary>
    /// Shop for buying and selling items
    /// </summary>
    public class Shop : MonoBehaviour
    {
        #region Private variables
        /// <summary>
        /// Temporary array for initialization items in shop
        /// </summary>
        [SerializeField] private GameObject[] objPool;
        /// <summary>
        /// List of items in shop
        /// </summary>
        private List<GameObject> shop;
        /// <summary>
        /// Temporary instance of showcase (should be changed)
        /// </summary>
        private GameObject showcase;
        /// <summary>
        /// Available player's credits
        /// </summary>
        private Text credit;
        #endregion
        private void Filling()
        {
            shop = new List<GameObject>();
            for (int i = 0; i < objPool.Length; i++)
            {
                shop.Add(Instantiate(objPool[i], showcase.transform.GetChild(i).transform));
                shop[i].GetComponent<Item>().Init();
            }
        }

        public void Init()
        {
            showcase = transform.GetChild(1).gameObject;
            credit = transform.GetChild(0).GetComponent<Text>();
            credit.text = HangarData.instance.player.Credit.ToString();

            for (int i = 0; i < showcase.transform.childCount; i++)
            {
                if (showcase.transform.GetChild(i).childCount > 0)
                {
                    showcase.transform.GetComponentsInChildren<Slot>()[i].isVacant = false;
                }
            }
            Filling();
        }

        /// <summary>
        /// Update value when it is changed
        /// </summary>
        public void CreditUpdate()
        {
            credit.text = HangarData.instance.player.Credit.ToString() ;
        }
    }
}
