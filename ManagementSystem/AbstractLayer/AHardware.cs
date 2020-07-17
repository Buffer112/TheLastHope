using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    /// Abstract hardware class. Use it for creating hardware of new type.
    /// </summary>
    public class AHardware : MonoBehaviour
    {
        /// <summary>
        /// Required slot type on carriage
        /// </summary>
        public TypePosition typePosition;
        /// <summary>
        /// Icon of item in inventory
        /// </summary>
        public Sprite sprite;
        /// <summary>
        /// Contained objects
        /// </summary>
        public List<GameObject> objects;
        /// <summary>
        /// Kind of hardware
        /// </summary>
        public HardwareKind hardwareKind;
    }


    public enum TypePosition
    {
        /// <summary>
        /// Require square slots for installing on carriage
        /// </summary>
        Square,
        /// <summary>
        /// Couldn't be installed on carriage
        /// </summary>
        Non
    }
    public enum HardwareKind
    {
        /// <summary>
        /// Some weapon
        /// </summary>
        Weapon,
        /// <summary>
        /// Some shield
        /// </summary>
        Shield,
        /// <summary>
        /// Container for ammunition
        /// </summary>
        AmmoContainer,
        /// <summary>
        /// Container for valuable loot
        /// </summary>
        LootContainer,
        /// <summary>
        /// Ammunition
        /// </summary>
        Ammo
    }
}