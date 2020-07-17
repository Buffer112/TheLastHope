using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TheLastHope.Hardware;

namespace TheLastHope.Hangar
{
    public class PositionController : MonoBehaviour
    {
        [HideInInspector] public List<GameObject> itemsOnCarriage;
        [HideInInspector] public List<GameObject> squareSlots;
        public void Init()
        {
            itemsOnCarriage = new List<GameObject>();
            squareSlots = new List<GameObject>();

            InitSlots();
        }
        void Update()
        {
        }

        /// <summary>
        /// Creating UI slots
        /// </summary>
        private void InitSlots()
        {
            for (int i = 0; i < HangarData.instance.currentCarriage.squareTypeCount; i++)
            {
                GameObject slot = new GameObject() { name = $"Slot_{i}" };
                slot.AddComponent<RectTransform>();
                slot.AddComponent<Slot>();
                slot.GetComponent<Slot>().slotType = SlotType.HardwarePosition;
                slot.GetComponent<Slot>().number = i;
                slot.AddComponent<GridLayoutGroup>();
                slot.GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
                slot.GetComponent<GridLayoutGroup>().cellSize = new Vector2(60f, 60f);              //Item's size
                slot.AddComponent<Image>();
                slot.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);                        //white color with 50% alpha channel
                slot.GetComponent<Image>().enabled = false;
                slot.transform.SetParent(transform);
                squareSlots.Add(slot);
				if (HangarData.instance.currentCarriage.items.Length > 0 && HangarData.instance.currentCarriage.items[i] != null)
				{
					HangarData.instance.currentCarriage.items[i].transform.SetParent(squareSlots[i].transform);
					squareSlots[i].GetComponent<Image>().enabled = true;
					itemsOnCarriage.Add(HangarData.instance.currentCarriage.items[i]);

					itemsOnCarriage.Last().GetComponent<Item>().Init();
				}
			}
        }
        /// <summary>
        /// Update UI slots when current carriage is changed
        /// </summary>
        public void UpdateSlots()
        {
            for (int i = 0; i < squareSlots.Count; i++)
            {
                if (squareSlots[i].transform.childCount == 0)
                {
                    Destroy(squareSlots[i]);
                }
                else
                {
                    squareSlots[i].transform.GetChild(0).SetParent(null);
                    Destroy(squareSlots[i]);
                }
            }
            squareSlots.Clear();
            itemsOnCarriage.Clear();
            InitSlots();
        }

        /// <summary>
        /// Show vacant slots when item dragging is begin. (it works only with square type)
        /// </summary>
        public void BeginItemDragging(AHardware hardware)
        {
            if (hardware.typePosition == TypePosition.Square)
            {
                for (int i = 0; i < squareSlots.Count; i++)
                {
                    squareSlots[i].GetComponent<Image>().enabled = true;
                }
            }
        }

        /// <summary>
        /// Hide unfilled slots when item dragging is end
        /// </summary>
        public void EndItemDragging()
        {
            for (int i = 0; i < squareSlots.Count; i++)
            {
                if (squareSlots[i].transform.childCount == 0)
                {
                    squareSlots[i].GetComponent<Image>().enabled = false;
                }
            }
        }
    }
}
