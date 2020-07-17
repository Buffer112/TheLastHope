using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;

namespace TheLastHope.Triggers
{
    public class TriggerEndMap : ATrigger
    {
        void Start()
        {
            base.Type = TriggerType.EndGame;
        }

    }
}

