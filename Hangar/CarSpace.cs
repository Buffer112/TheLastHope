using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Hardware;
 
namespace TheLastHope.Hangar
{
    /// <summary>
    /// Container of a carriage
    /// </summary>
    public class CarSpace : MonoBehaviour
    {
        /// <summary>
        /// List of all hardware on the carriage
        /// </summary>
        public GameObject[] hardwares;
        /// <summary>
        /// Connected with hardware UI items
        /// </summary>
        public GameObject[] items;
        /// <summary>
        /// Positions where Hardware could be installed
        /// </summary>
        public List<Transform> hardwarePositions;
        /// <summary>
        /// How many positions of type 'square' on carriage
        /// </summary>
        public int squareTypeCount;
       
        private void Start()
        {
            GameObject temp = new GameObject();
            temp.AddComponent<NonHardware>();
            items = new GameObject[squareTypeCount];
            hardwares = new GameObject[squareTypeCount];
            hardwarePositions = new List<Transform>();
            for (int i = 0; i < squareTypeCount; i++)
            {
                //hardwares.Add(temp);
                //items.Add(temp);
                hardwarePositions.Add(transform.GetChild(i));
            }
        }
 
        /// <summary>
        /// Add new hardware in defined slot on carriage
        /// </summary>
        /// <param name="item">UI item with hardware</param>
        /// <param name="index">Numbetr of slot</param>
        public void AddNewHardware(GameObject item, int index)
        {
            items[index] = item;
            hardwares[index] = Instantiate(item.GetComponent<Item>().hw, hardwarePositions[index]);
            hardwares[index].transform.localPosition = new Vector3(0,0,0);
        }
        /// <summary>
        /// Remove hardware from defined slot on carriage
        /// </summary>
        /// <param name="index">Number of slot</param>
        public void RemoveHardware(int index)
        {
            Destroy(hardwares[index]);
            hardwares[index] = null;
            items[index] = null;
        }
    }
}