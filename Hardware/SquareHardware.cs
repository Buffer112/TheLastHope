using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Hardware
{
    /// <summary>
    /// Hardware for items which will be installed on carriage. Square type.
    /// </summary>
    public class SquareHardware : AHardware
    {
        void Start()
        {
            typePosition = TypePosition.Square;
        }
    }
}
