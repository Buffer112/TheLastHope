using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Hardware
{
    /// <summary>
    /// Hardware for items which won't be installed on carriage. Non type.
    /// </summary>
    public class NonHardware : AHardware
    {
        void Start()
        {
            typePosition = TypePosition.Non;
        }
    }
}
