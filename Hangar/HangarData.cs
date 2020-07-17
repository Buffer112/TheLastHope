using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Hangar.UI;

namespace TheLastHope.Hangar
{
    /// <summary>
    /// Data of the hangar
    /// </summary>
    public class HangarData : MonoBehaviour
    {
        #region Serialized objects - will be changed
        /// <summary>
        /// (serialazed)
        /// </summary>
        public PositionController positionController;
        /// <summary>
        /// (serialazed)
        /// </summary>
        public Inventory inventory;                             
        /// <summary>
        /// (serialazed)
        /// </summary>
        public Shop shop;
        /// <summary>
        /// (serialazed)
        /// </summary>
        public HangarUIController uiController;
        /// <summary>
        /// Description of item
        /// </summary>
        public GameObject description;
        public GameObject properties;
        #endregion

        public CarSpace[] train;

        #region Public variables
        /// <summary>
        /// Carriage in editing
        /// </summary>
        [HideInInspector] public CarSpace currentCarriage;
        /// <summary>
        /// Current window in UI
        /// </summary>
        public CurrentWindow currentWindow;
        /// <summary>
        /// Number of current carriage of Train[]
        /// </summary>
        public int numOfCar=0;

        //public int Credit = 100;
        public Management.Player player;
        public static HangarData instance;
        /// <summary>
        /// Ammunition object pool (items)
        /// </summary>
        [Header("Put prefabs only in that sequence: M792HEI_T, Shotgun, Energy, ADM401_84mms")]
        public List <GameObject> ammunitionPrefabs;

        #endregion
        void Start()
        {
            Init();
        }
        public HangarData()
        {

        }
        
        //public void Init(SceneData sceneData)
        public void Init()
        {
            currentWindow = CurrentWindow.Carriage;
            player = new Management.Player(3200);
            instance = this;
            //print($" CAR {sceneData.TrainCars[2].name}");
            //currentCarriage = sceneData.TrainCars[2].GetComponentInChildren<Container>();
            currentCarriage = train[numOfCar];

            positionController.Init();
            inventory.Init();
            uiController.Init();
            shop.Init();
            description.SetActive(false);
            properties.GetComponentInChildren<AmmoSlots>().Init();
            properties.SetActive(false);
        }
        
        public bool GetNextCarriage()
        {
            if (numOfCar + 1 < train.Length)
            {
                numOfCar++;
                currentCarriage = train[numOfCar];
                return true;
            }
            return false;
        }

        public bool GetPreviousCarriage()
        {
            if (numOfCar - 1 >= 0)
            {
                numOfCar--;
                currentCarriage = train[numOfCar];
                return true;
            }
            return false;
        }

        public void SetInactive()
        {
            inventory.transform.parent.gameObject.SetActive(false);
            //positionController.gameObject.SetActive(false);
        }
    }

    public enum CurrentWindow
    {
        Carriage,
        Shop
    }
}
