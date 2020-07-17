using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoad : AEvent
{
    public PointController city;
    public Canvas ui;
    public void Invisible()
    {
        ui.sortingOrder = 0;
    }

    public override bool Condition()
    {
        if (city.IsStartPoint)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void EventCode()
    {
        ui.sortingOrder = 3;
    }
}
