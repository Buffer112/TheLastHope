using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEvent : MonoBehaviour
{
    [SerializeField] public int count; //Количество вызовов ивента
    [SerializeField] public bool inf = false; // Бесконечное выполнение ивента - true, конечное - false
    [SerializeField] public bool call = false; // Позволяет отслеживать что ивент был вызван
    public abstract bool Condition(); //Условие при котором ивент сработает
    public abstract void EventCode(); //Что должно произойти при сработке ивента
    public void ActivateEvent()
    {
        call = !call;
        if (!inf)
        {
            count--;
        }
        EventCode();
    }
    public void Destroyer()
    {
        Destroy(this.gameObject);
    }
}
