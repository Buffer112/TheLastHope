using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] public List<AEvent> events;
    // Update is called once per frame
    public void UpdateEvents()
    {
        int temp = -1;
        for (int i=0; i < events.Count; i++)
        {
            //Если выполнилось условие ивента
            if (events[i].Condition())
            {
                if (events[i].count > 0)
                {
                    events[i].ActivateEvent();
                }
                else
                {
                    temp = i;
                }
            }
        }
        if (temp != -1)
        {
            AEvent ev = events[temp];
            events.Remove(events[temp]);
            ev.Destroyer();
        }
    }
    private void Update()
    {
        //UpdateEvents();
    }
}
