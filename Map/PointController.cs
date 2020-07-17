using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// Структура точки, создана для удобства заполнения в Unity редакторе
/// </summary>
[Serializable]
public struct Points
{
    public PointController point;
    public int dist;
}

/// <summary>
/// PointController - отвечает за логику и ф-ционал отдельно взятой точки
/// </summary>
public class PointController : MonoBehaviour, IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    #region Public variables
    /// <summary>
    /// Номер точки (очень важно, чтобы он был уникальным 
    /// иначе ничего работать не будет)
    /// </summary>
    [SerializeField] public int num;
    /// <summary>
    /// Список соседий , к которым можно перейти из этой точки
    /// </summary>
    [SerializeField] public Points[] Neightboring;
    /// <summary>
    /// Доступ к MapManager , с помощью него точка передает событие
    /// , что ее нажали
    /// </summary>
    [SerializeField] public MapManager mapMan;
    /// <summary>
    /// Является ли точка ключевой
    /// </summary>
    [SerializeField] public bool IsKeyPoint = false;
    /// <summary>
    /// Является ли точка открытой для перемещения
    /// </summary>
    [SerializeField] public bool IsOpenPoint = false;
    /// <summary>
    /// Является ли точка заблокированной
    /// </summary>
    [SerializeField] public bool IsBlockPoint = false;
    /// <summary>
    /// Является ли точка стартовой
    /// </summary>
    [SerializeField] public bool IsStartPoint = false;
    #endregion


    #region Public Methods
    /// <summary>
    /// Метод встроенный в юнити, вызывается при нажатии на точку
    /// </summary>
    /// <param name="eventData">Хз что это</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        mapMan.PointClick(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!mapMan.fix)
        {
            mapMan.PointEnter(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!mapMan.fix)
        {
            if (mapMan.ui.uiActive)
            {
                mapMan.PointEnter(this);
            }
        }
    }

    /// <summary>
    /// Меняет цвет точки
    /// </summary>
    /// <param name="clr">Цвет на который меняется</param>
    public void setColor(Color clr)
    {
        GetComponent<Image>().color = clr;
    }

    private void OnMouseOver()
    {
        print("kek");
    }
    #endregion
}
