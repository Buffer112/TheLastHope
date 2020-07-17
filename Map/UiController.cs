using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс отвечающий за поведение интерфейса.
/// На данном этапе это всплывающая панелька при нажатии на вершину графа
/// </summary>
public class UiController : MonoBehaviour
{

    #region Public variables
    [SerializeField] public Canvas canMap;
    [SerializeField] public Canvas canUi;
    [SerializeField] public Text targetName;
    [SerializeField] public Text distanceFromTarget;
    [SerializeField] public Text nextCity;
    /// <summary>
    /// Открыто или закрыто ui
    /// </summary>
    public bool uiActive;
    #endregion

    #region Public methods
    /// <summary>
    /// Загружает уровень. Но возможно это сделать
    /// через PathController
    /// </summary>
    public void LoadLevel()
	{
		Application.LoadLevel("MainScene");
	}
    /// <summary>
    /// Открывает и закрывает интерфей, передавая в него
    /// информаци из карты
    /// </summary>
    /// <param name="target">Выбранная точка на графе</param>
    /// <param name="dist">Дистанция до выбранной точки</param>
    /// <param name="nextCity">Следующий город, в который нужно отправится</param>
    /// <returns>Закрыто или открыто меню</returns>
	public bool OpenCloseUi(PointController target, int dist, PointController nextCity)
    {
        if (uiActive)
        {
            canUi.sortingOrder = 0;
            canMap.sortingOrder = 1;
            uiActive = false;
            targetName.text = "";
            return false;
        }
        else
        {
            canUi.sortingOrder = 1;
            canMap.sortingOrder = 0;
            uiActive = true;
            targetName.text = " Selected city:  " + target.name;
            distanceFromTarget.text = " Distance to " + target.name + " :  " + dist;
            this.nextCity.text = " Next city where are you go:  " + nextCity.name;
            return true;
        }
    }
    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        uiActive = false;
    }
    #endregion
}
