using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MapController - класс созданный для управление картой
/// игроком. Данный класс реализует скролл карты и перемещение
/// ее навправо, навлево, вверх , вниз.
/// </summary>
public class MapController : MonoBehaviour
{

    #region Public variable
    /// <summary>
    /// Параметр отвечающий за скорость перемещения карты
    /// </summary>
    [SerializeField] public float movSpeed;
    /// <summary>
    /// Параметр отвечающий за скорость отдаления и приближения
    /// </summary>
    [SerializeField] public float scaleSpeed;
    #endregion

    #region Private variable
    /// <summary>
    /// Параметр контейнер, с помощью которого создается
    /// вектор для сложения с вектором позиции карты
    /// </summary>
    private Vector3 difScale;
    /// <summary>
    /// Параметр, с помощью которого идет преобразование карты
    /// </summary>
    private RectTransform rectT;
    #endregion

    #region Private methods
    /// <summary>
    /// Метод проверяющий вызов пользователем скролла
    /// и меняющий масштаб карты на новый
    /// </summary>
    private void ScaleControll()
    {
        float _mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (_mouseWheel != 0)
        {
            Vector3 temp = rectT.localScale;
            if (_mouseWheel > 0)
            {
                temp.x *= scaleSpeed;
                temp.y *= scaleSpeed;
            }
            else
            {
                temp.x *= 2 - scaleSpeed;
                temp.y *= 2 - scaleSpeed;
            }
            rectT.localScale = temp;
        }
    }

    /// <summary>
    /// Метод проверки вызова пользователем команд перемещения
    /// и изменения координат позиции карты.
    /// </summary>
    private void MovingController()
    {
        if (Input.GetButton("UpMap"))
        {
            difScale.y = -movSpeed * Time.deltaTime;
            rectT.transform.position += difScale;
            difScale.y = 0;
        }
        if (Input.GetButton("DownMap"))
        {
            difScale.y = movSpeed * Time.deltaTime;
            rectT.transform.position += difScale;
            difScale.y = 0;
        }
        if (Input.GetButton("LeftMap"))
        {
            difScale.x = movSpeed * Time.deltaTime;
            rectT.transform.position += difScale;
            difScale.x = 0;
        }
        if (Input.GetButton("RightMap"))
        {
            difScale.x = -movSpeed * Time.deltaTime;
            rectT.transform.position += difScale;
            difScale.x = 0;
        }
    }
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        rectT = GetComponent<RectTransform>();
        difScale = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        ScaleControll();
        MovingController();
    }
    #endregion

}
