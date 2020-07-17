using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Управляет всеми компонентами на карте. Без него
/// ни одна мыш не пошевельнется.
/// </summary>
public class MapManager : MonoBehaviour
{

    #region Public variables
    /// <summary>
    /// Классы , с которыми работает MapManager
    /// path - класс , через который можно общаться с графом
    /// </summary>
    [SerializeField] public PathController path;
    /// <summary>
    /// ui - панелька с интерфейсом, который можно вызывать и убирать
    /// </summary>
    [SerializeField] public UiController ui;
    /// <summary>
    /// Переменные для закраски разных типов вершин графа. 
    /// roadClr - цвет вершины, которая входит в 
    /// построенный маршрут
    /// </summary>
    [SerializeField] public Color roadClr;
    /// <summary>
    /// pointClr - обычный цвет вершины, когда она не входит в маршрут
    /// и не является стартовой
    /// </summary>
    [SerializeField] public Color pointClr;
    /// <summary>
    /// currentClr - цвет стартовой точки
    /// </summary>
    [SerializeField] public Color currentClr;
    /// <summary>
    /// keyPointClr - цвет ключевой точки
    /// </summary>
    [SerializeField] public Color keyPointClr;
    /// <summary>
    /// blockPointClr - цвет заблокированной точки
    /// </summary>
    [SerializeField] public Color blockPointClr;
    #endregion

    private SLSystem sl = new SLSystem();
    public bool fix = false;

    #region Public Methods
    public void PointClick(PointController point)
    {
        
        if (fix)
        {
            PointEnter(point);
        }
        else
        {
            if (!ui.uiActive)
            {
                PointEnter(point);
            }
        }
        fix = !fix;
    }
    /// <summary>
    /// Метод обрабатывает нажатие точки
    /// </summary>
    /// <param name="point"> Точка , которую нажал игрок</param>
    public void PointEnter(PointController point)
    {
        path.clearRoad(pointClr, currentClr, keyPointClr, blockPointClr);
        if (point != path.startPoint && !point.IsBlockPoint)
        {
            if (point.IsKeyPoint && path.nextKeyPoint == point)
            {
                // point - ключевая точка
                path.searchRoad(path.startPoint.num, point.num);
                if (ui.OpenCloseUi(point, path.GetDistance(point.num), path.GetNextCity()))
                {
                    path.drawRoad(roadClr);
                }
            }
            else
            {
                path.searchRoad(path.startPoint.num, point.num);
                foreach (int p in path.road)
                {
                    //Если на маршруте есть заблокированная точка
                    if (path.mapsObj[p].IsBlockPoint)
                    {
                        if (ui.uiActive)
                        {
                            ui.OpenCloseUi(point, path.GetDistance(point.num), path.GetNextCity());
                        }
                        return;
                    }
                }
                if (!path.road.Contains(path.nextKeyPoint.num))
                {
                    if (point.IsOpenPoint)
                    {
                        //point - открытая точка для перемещения
                        path.searchRoad(path.startPoint.num, point.num);
                        if (ui.OpenCloseUi(point, path.GetDistance(point.num), path.GetNextCity()))
                        {
                            path.drawRoad(roadClr);
                        }
                    }
                    else
                    {
                        //point закрытая точка для перемещния
                        //строим дорогу от стартовой до ключевой
                        //через закрытую точку
                        if (path.searchRoadKey(path.startPoint.num, path.nextKeyPoint.num, point.num))
                        {
                            if (ui.OpenCloseUi(point, path.GetDistance(point.num), path.GetNextCity()))
                            {
                                path.drawRoad(roadClr);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (ui.uiActive)
            {
                ui.OpenCloseUi(point, path.GetDistance(point.num), path.GetNextCity());
            }
        }
    }
    public void PointIntoExit(PointController point)
    {
        path.clearRoad(pointClr, currentClr, keyPointClr, blockPointClr);
        if (point != path.startPoint && !point.IsBlockPoint)
        {
            if (point.IsKeyPoint)
            {
                // point - ключевая точка
                path.searchRoad(path.startPoint.num, point.num);
                path.drawRoad(roadClr);
                
            }
            else
            {
                path.searchRoad(path.startPoint.num, point.num);
                foreach (int p in path.road)
                {
                    //Если на маршруте есть заблокированная точка
                    if (path.mapsObj[p].IsBlockPoint)
                    {
                        return;
                    }
                }
                if (!path.road.Contains(path.nextKeyPoint.num))
                {
                    if (point.IsOpenPoint)
                    {
                        //point - открытая точка для перемещения
                        path.searchRoad(path.startPoint.num, point.num);
                        path.drawRoad(roadClr);
                        
                    }
                    else
                    {
                        //point закрытая точка для перемещния
                        //строим дорогу от стартовой до ключевой
                        //через закрытую точку
                        if (path.searchRoadKey(path.startPoint.num, path.nextKeyPoint.num, point.num))
                        {
                            path.drawRoad(roadClr);
                            
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// Метод ищет ближайшую ключевую точку
    /// </summary>
    public void ChangeKeyPoint()
    {
        foreach (PointController p in path.mapsObj)
        {
            if (p.IsKeyPoint)
            {
                bool temp = true;
                path.searchRoad(path.startPoint.num, p.num);
                foreach (int k in path.road)
                {
                    if (path.mapsObj[k].IsKeyPoint)
                    {
                        if (k != p.num)
                        {
                            temp = false;
                        }
                    }
                }
                if (temp)
                {
                    path.nextKeyPoint = p;
                }
            }
        }
    }
    /// <summary>
    /// Устанавливает стартовую точку
    /// </summary>
    public void ChangeStartPoint(string name)
    {
        foreach (PointController p in path.mapsObj)
        {
            if (p.name == name)
            {
                p.IsStartPoint = true;
                path.startPoint = p;
            }
        }
    }
    /// <summary>
    /// Стартует лвл
    /// </summary>
    public void StartLvl(int numScene)
    {
        if (numScene == 1)
        {
            //path.GetNextCity().IsStartPoint = true;
            path.startPoint.IsStartPoint = false;
            path.startPoint.IsBlockPoint = true;
            sl.SaveMapFile(path.mapsObj, path.startPoint.name, path.GetNextCity().name);
			//Новый уровень
			Application.LoadLevel("EmergencyScene");
        }
        if (numScene == 2)
        {
            sl.SaveMapFile(path.mapsObj, path.startPoint.name, path.GetNextCity().name);
			Application.LoadLevel("Trit");
            //Вызов сцены ангара
        }
    }
    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            path.Init();
            //sl.LoadMapFile(path.mapsObj);
            string name = sl.LoadNextLvlName();
            if (name != "")
            {
                ChangeStartPoint(name);
            }
            else
            {
                ChangeStartPoint(sl.LoadPrevLvlName());
            }
            if (path.startPoint.IsKeyPoint)
            {
                print(path.startPoint.name);
                path.startPoint.IsKeyPoint = false;
            }
            ChangeKeyPoint();
            path.clearRoad(pointClr, currentClr, keyPointClr, blockPointClr);
        }
        catch
        {
            path.Init();
            path.clearRoad(pointClr, currentClr, keyPointClr, blockPointClr);
            sl.SaveMapFile(path.mapsObj, "LVL1", "LVL2");
            print("Файл отсутствует. Создан новый.");
        }
    }
    #endregion
}
