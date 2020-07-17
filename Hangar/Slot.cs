using TheLastHope.Management.AbstractLayer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

namespace TheLastHope.Hangar
{
    public class Slot : MonoBehaviour, IDropHandler
    {
        /// <summary>
        /// Slots of inventory or not
        /// </summary>
        //public bool isInventory = true;
        public  SlotType slotType = SlotType.Inventory;
        /// <summary>
        /// Slot is vacant or not
        /// </summary>
        public bool isVacant = true;
        /// <summary>
        /// Number of a slot
        /// </summary>
        public int number;
        /// <summary>
        /// Amount of items will be in slot
        /// </summary>
        public int amountOfItems;
        /// <summary>
        /// Item in this slot
        /// </summary>
        public GameObject item
        {
            get
            {
                if (transform.childCount > 0)
                    return transform.GetChild(0).gameObject;
                return null;
            }
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (!item)
            {
                if (!Item.itemBeingDragged.GetComponent<Item>().isAmmo)
                {
                    if (Item.itemBeingDragged.transform.parent.GetComponent<Slot>().slotType == SlotType.HardwarePosition)
                    {
                        if (slotType == SlotType.Inventory) //From HWPosition to inventory
                        {
                            HangarData.instance.currentCarriage.RemoveHardware(Item.itemBeingDragged.transform.parent.GetComponent<Slot>().number);
                        }
                        else if (slotType == SlotType.HardwarePosition) //From HWPosition to HWPosition
                        {
                            HangarData.instance.currentCarriage.RemoveHardware(Item.itemBeingDragged.transform.parent.GetComponent<Slot>().number);
                            HangarData.instance.currentCarriage.AddNewHardware(Item.itemBeingDragged, number);
                            Item.itemBeingDragged.transform.SetParent(transform);
                        }
                    }
                    else if (slotType == SlotType.HardwarePosition && Item.itemBeingDragged.transform.parent.GetComponent<Slot>().slotType == SlotType.Inventory) //From inventory to HWPosition
                    {
                        HangarData.instance.positionController.itemsOnCarriage.Add(Item.itemBeingDragged);
                        HangarData.instance.currentCarriage.AddNewHardware(Item.itemBeingDragged, number);
                    }

                    else if (Item.itemBeingDragged.transform.parent.GetComponent<Slot>().slotType == SlotType.Shop)
                    {
                        if (slotType == SlotType.Inventory && Item.itemBeingDragged.GetComponent<Item>().price <= HangarData.instance.player.Credit) // From shop to inventory
                        {
                            HangarData.instance.player.Credit -= Item.itemBeingDragged.GetComponent<Item>().price;

                            HangarData.instance.shop.CreditUpdate();
                        }
                        else if (slotType == SlotType.Shop) //From shop to shop
                        {
                            return;
                        }
                    }
                    else if (slotType == SlotType.Shop && Item.itemBeingDragged.transform.parent.GetComponent<Slot>().slotType == SlotType.Inventory) //From inventory to shop
                    {
                        HangarData.instance.player.Credit += Item.itemBeingDragged.GetComponent<Item>().price;
                        HangarData.instance.shop.CreditUpdate();
                    }

                    Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isVacant = true;
                    Item.itemBeingDragged.transform.SetParent(transform);
                    isVacant = false;
                }
                /////////////////////////////////
                else
                {
                    if (Item.itemBeingDragged.transform.parent.GetComponent<Slot>().slotType == SlotType.Shop)
                    {
                        int tempAmount = HangarData.instance.player.Credit / Item.itemBeingDragged.GetComponent<Item>().price;
                        if (slotType == SlotType.Inventory && tempAmount > 0)// From shop to inventory
                        {
                            if (tempAmount < Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity)
                                HangarData.instance.uiController.uiCreator.CreateDivisionWin(HangarData.instance.player.Credit / Item.itemBeingDragged.GetComponent<Item>().price, this);
                            else
                                HangarData.instance.uiController.uiCreator.CreateDivisionWin(Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity, this);
                        }
                    }
                    else if (Item.itemBeingDragged.transform.parent.GetComponent<Slot>().slotType == SlotType.Inventory && slotType == SlotType.Inventory)
                    {
                        //do nothing
                    }
                    else //if (slotType == SlotType.Shop && Item.itemBeingDragged.transform.parent.GetComponent<Slot>().slotType == SlotType.Inventory) //From inventory to shop
                    {
                        HangarData.instance.uiController.uiCreator.CreateDivisionWin(Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity, this);
                    }
                }
                
            }
            else if(item.GetComponent<Item>().isAmmo && Item.itemBeingDragged.GetComponent<Item>().isAmmo)
            {
                if (slotType == SlotType.Inventory)
                {
                    if (item.GetComponent<AmmoClip>().Type == Item.itemBeingDragged.GetComponent<AmmoClip>().Type)
                    {
                        item.GetComponent<AmmoClip>().Quantity += Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity;
                        Destroy(Item.itemBeingDragged);
                        Item.itemBeingDragged = null;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Type of windows where slots are being used
    /// </summary>
    public enum SlotType
    {
        Inventory,
        Shop,
        HardwarePosition,
        AmmoSlots
    }
}
