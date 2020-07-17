using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TheLastHope.Management.Data;
using System.Linq;


namespace TheLastHope.Hangar
{
    public class AmmoSlots : MonoBehaviour
    {
        //[SerializeField] private Dictionary<AmmoType, GameObject> objPool; ///!!!!!!!!!!!!!CHANGE!!!!!!!!!!!!!!!!\\\Items
        [HideInInspector]public List<GameObject> ammoInSlots;
        private int lastVacantSlot = 0;
        [SerializeField] private Button close;
        public void Init()
        {
            close.onClick.AddListener(Close);
            ammoInSlots = new List<GameObject>();
        }
        public void Open(AmmoContainer container)
        {
            if (container.ammo.Count != 0)
            {
                foreach (KeyValuePair<AmmoType, int> pair in container.ammo)
                {
                    if (pair.Value > 0)
                    {
                        ammoInSlots.Add(Instantiate(HangarData.instance.ammunitionPrefabs[(int)pair.Key], transform.GetChild(lastVacantSlot)));
                        ammoInSlots.Last().GetComponent<Item>().Init();
                        ammoInSlots.Last().GetComponent<AmmoClip>().Quantity = pair.Value;
                        lastVacantSlot++;
                    }
                }
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).childCount > 0)
                    transform.GetComponentsInChildren<Slot>()[i].isVacant = false;
                else
                    transform.GetComponentsInChildren<Slot>()[i].isVacant = true;
            }
            HangarData.instance.properties.SetActive(true);
        }
        public void Close()
        {
            foreach (var item in ammoInSlots)
            {
                Destroy(item);
            }
            ammoInSlots.Clear();

            HangarData.instance.properties.SetActive(false);
            lastVacantSlot = 0;
        }
    }
}
