using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;

namespace TheLastHope.Hangar
{
    public class AmmoContainer : MonoBehaviour
    {
        /// <summary>
        /// Max capacity of ammunition which container has
        /// </summary>
        public int maxCapacity = 1000;
        /// <summary>
        /// Remaining capacity for ammunition in container
        /// </summary>
        private int currentCapacity;

        public Dictionary<AmmoType, int> ammo;
        private Dictionary<AmmoType, int> ammoSize;

        //private void Start() // Init exist in WeaponManager
        //{
        //    Init();
        //}



        public void Init()
        {
            currentCapacity = maxCapacity;
            ammo = new Dictionary<AmmoType, int>();

            ammoSize = new Dictionary<AmmoType, int>()
            {
                [AmmoType.ADM401_84mms] = 25,
                [AmmoType.Energy] = 25,
                [AmmoType.M792HEI_T] = 1,
                [AmmoType.Shotgun] = 5
            };

            ammo.Add(AmmoType.M792HEI_T, 500);
            ammo.Add(AmmoType.ADM401_84mms, 50);
            ammo[AmmoType.Energy] = 500;
            ammo[AmmoType.Shotgun] = 50;
        }

        /// <summary>
        /// Adding certain type ammo in container
        /// </summary>
        /// <param name="type">Ammunition type</param>
        /// <param name="amount">How many add</param>
        /// <returns>How many were added</returns>
        public int AddAmmo(AmmoType type, int amount)
        {
            if (amount * ammoSize[type] >= currentCapacity)
            {
                ammo[type] += currentCapacity / ammoSize[type];
                amount = (currentCapacity / ammoSize[type]) * ammoSize[type];   //for example: (19 / 5) * 5 = (3) * 5 = 15
                currentCapacity -= (currentCapacity / ammoSize[type]) * ammoSize[type];
                return amount;
            }
            else
            {
                ammo[type] += amount ;
                currentCapacity -= amount * ammoSize[type];
                return amount;
            }
        }

        /// <summary>
        /// Taking away certain type ammo from container
        /// </summary>
        /// <param name="type">Ammunition type</param>
        /// <param name="amount">How many take</param>
        /// <returns></returns>
        public bool GetAmmo(AmmoType type, int amount)
        {
            //print($"TYPE: {type.ToString()}");
            if (ammo[type] >= amount)
            {
                ammo[type] -= amount;
                currentCapacity += amount * ammoSize[type];
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
