using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastHope.Hangar.UI
{
    /// <summary>
    /// Class for creating and managing non static UI components
    /// </summary>
    public class UICreator : MonoBehaviour
    {
        #region Private variables
        private GameObject txtBg;
        private GameObject txt;
        private GameObject divisionWin;
        private int screenWidth;
        private int screenHeight;


        [Header("Slider")]
        [SerializeField] Sprite sliderBackgroundSprite;
        [SerializeField] Color sliderColor = Color.red;
        [SerializeField] Sprite sliderFillSprite;
        [SerializeField] Color sliderFillColor = Color.green;
        [SerializeField] Sprite sliderHandleSprite;
        [SerializeField] Color sliderHandleColor = Color.black;

        [Header("Text")]
        [SerializeField] Font font;
        [SerializeField] Color fontColor = Color.black;

        [Header("Buttons")]
        [SerializeField] Color btnOkColor = Color.green;
        [SerializeField] Sprite btnOkSprite;
        [SerializeField] Color btnCancelColor = Color.red;
        [SerializeField] Sprite btnCancelSprite;

        private Slot destinationSlot;


        #endregion

        #region Public properties
        /// <summary>
        /// UI text with background on screen
        /// </summary>
        public GameObject TxtBg
        {
            get
            {
                if (txtBg)
                    return txtBg;
                else
                    return CreateTextBg();
            }
        }
        /// <summary>
        /// UI text on screen
        /// </summary>
        public GameObject Txt
        {
            get
            {
                if (txt)
                    return txt;
                else
                    return CreateTxt();

            }
        }
        /// <summary>
        /// Window which appears when need to divide stack of items. Before use it call method CreateDivisionWin
        /// </summary>
        public GameObject DivisionWin
        {
            get
            {
                if (divisionWin)
                    return divisionWin;
                else
                    return null;
            }
        }
        #endregion



        //[SerializeField] Button btn;
        private void Start()
        {
            //btn.onClick.AddListener(CreateDivisionWin);
            screenWidth = Camera.main.pixelWidth;
            screenHeight = Camera.main.pixelHeight;
        }




        GameObject CreateTextBg()
        {
            txtBg = new GameObject() { name = "txtBg" };
            txtBg.AddComponent<RectTransform>();
            txtBg.AddComponent<Image>();
            //size
            GameObject text = new GameObject() { name = "text" };
            text.AddComponent<RectTransform>();
            text.AddComponent<Text>().text = "text";
            text.transform.SetParent(txtBg.transform);
            return txtBg;
        }

        GameObject CreateTxt()
        {
            txt = new GameObject() { name = "txt" };
            txt.AddComponent<RectTransform>();
            txt.AddComponent<Text>().text = "text";
            //size
            return txt;
        }

        public void CreateDivisionWin(int maxAmount, Slot slot)
        {
            destinationSlot = slot;
            float widthProportion = screenWidth * 0.25f;
            float hightProportion = screenHeight * 0.25f;

            divisionWin = new GameObject() { name = "divisionWindow" };
            //divisionWin.AddComponent<RectTransform>();
            //size
            divisionWin.AddComponent<Image>();
            divisionWin.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(widthProportion, hightProportion);
            divisionWin.AddComponent<VerticalLayoutGroup>();
            divisionWin.transform.SetParent(HangarData.instance.uiController.canvas.transform);
            divisionWin.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);

            GameObject textBox = new GameObject() { name = "textBox" };
            //textBox.AddComponent<RectTransform>();
            textBox.AddComponent<Text>().text = "1";
            textBox.GetComponent<RectTransform>().anchorMax = Vector2.one;
            textBox.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            textBox.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            textBox.GetComponent<Text>().font = font;
            textBox.GetComponent<Text>().color = fontColor;
            textBox.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            textBox.GetComponent<Text>().resizeTextForBestFit = true;
            textBox.AddComponent<SliderTextUpdate>();
            textBox.transform.SetParent(divisionWin.transform);

            #region Slider
            GameObject slider = new GameObject() { name = "slider" };
            //slider.AddComponent<RectTransform>();
            slider.AddComponent<Slider>();
            slider.GetComponent<Slider>().minValue = 1;
            slider.GetComponent<Slider>().maxValue = maxAmount;
            slider.GetComponent<Slider>().wholeNumbers = true;
            slider.GetComponent<Slider>().onValueChanged.AddListener(textBox.GetComponent<SliderTextUpdate>().TextUpdate);
            slider.AddComponent<LayoutElement>().preferredHeight = hightProportion * 0.25f;
            slider.GetComponent<LayoutElement>().minHeight = 1;
            slider.transform.SetParent(divisionWin.transform);

            GameObject sliderBg = new GameObject() { name = "Background" };
            sliderBg.AddComponent<Image>().color = sliderColor;
            sliderBg.GetComponent<Image>().sprite = sliderBackgroundSprite;
            //sliderBg.GetComponent<Image>().type = Image.Type.;
            sliderBg.GetComponent<RectTransform>().anchorMax = Vector2.one;
            sliderBg.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            sliderBg.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            sliderBg.transform.SetParent(slider.transform);

            GameObject sliderFillArea = new GameObject() { name = "Fill Area" };
            sliderFillArea.AddComponent<RectTransform>().anchorMax = Vector2.one;
            sliderFillArea.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            sliderFillArea.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            sliderFillArea.transform.SetParent(slider.transform);

            GameObject sliderFill = new GameObject() { name = "Fill" };
            sliderFill.AddComponent<Image>().sprite = sliderFillSprite;
            sliderFill.GetComponent<Image>().color = sliderFillColor;
            //sliderFill.GetComponent<Image>().type = Image.Type.Tiled;
            sliderFill.GetComponent<RectTransform>().anchorMax = Vector2.one;
            sliderFill.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            sliderFill.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            sliderFill.transform.SetParent(sliderFillArea.transform);

            GameObject sliderHandleArea = new GameObject() { name = "Handle Slide Area" };
            sliderHandleArea.AddComponent<RectTransform>().anchorMax = Vector2.one;
            sliderHandleArea.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            sliderHandleArea.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            sliderHandleArea.transform.SetParent(slider.transform);

            GameObject sliderHandle = new GameObject() { name = "Handle" };
            sliderHandle.AddComponent<Image>().sprite = sliderHandleSprite;
            sliderHandle.GetComponent<Image>().color = sliderHandleColor;
            sliderHandle.AddComponent<LayoutElement>().ignoreLayout = true;
            sliderHandle.GetComponent<RectTransform>().sizeDelta = new Vector2(hightProportion * 0.5f, hightProportion * 0.25f);
            sliderHandle.transform.SetParent(sliderHandleArea.transform);

            slider.GetComponent<Slider>().fillRect = sliderFill.GetComponent<RectTransform>();
            slider.GetComponent<Slider>().handleRect = sliderHandle.GetComponent<RectTransform>();
            #endregion

            GameObject btnsLayout = new GameObject() { name = "btnsLayout" };
            btnsLayout.AddComponent<RectTransform>();
            btnsLayout.AddComponent<HorizontalLayoutGroup>();
            btnsLayout.transform.SetParent(divisionWin.transform);

            GameObject btnOk = new GameObject() { name = "btnOk" };
            btnOk.AddComponent<LayoutElement>().preferredHeight = hightProportion * 0.25f;
            btnOk.GetComponent<LayoutElement>().minHeight = 1;
            btnOk.AddComponent<Image>().color = btnOkColor;
            btnOk.GetComponent<Image>().sprite = btnOkSprite;
            btnOk.AddComponent<Button>().targetGraphic = btnOk.GetComponent<Image>();
            btnOk.GetComponent<Button>().onClick.AddListener(ButtonOK_Click);
            //btnOk.GetComponent<RectTransform>().sizeDelta = new Vector2(screenWidth * widthProportion, screenHeight * heightProportion * 0.25f);
            btnOk.transform.SetParent(btnsLayout.transform);

            GameObject btnCancel = new GameObject() { name = "btnCancel" };
            btnCancel.AddComponent<LayoutElement>().preferredHeight = hightProportion * 0.25f;
            btnCancel.GetComponent<LayoutElement>().minHeight = 1;
            btnCancel.AddComponent<Image>().color = btnCancelColor;
            btnCancel.GetComponent<Image>().sprite = btnCancelSprite;
            btnCancel.AddComponent<Button>().targetGraphic = btnCancel.GetComponent<Image>();
            btnCancel.GetComponent<Button>().onClick.AddListener(ButtonCancel_Click);
            //btnCancel.GetComponent<RectTransform>().sizeDelta = new Vector2(screenWidth * widthProportion, screenHeight * heightProportion * 0.25f);
            btnCancel.transform.SetParent(btnsLayout.transform);

            

            //return divisionWin;
        }

        void ButtonOK_Click()
        {
            AssignItemToSlot(Convert.ToInt32(divisionWin.GetComponentInChildren<Text>().text));
            Destroy(divisionWin);
        }

        void ButtonCancel_Click()
        {
            AssignItemToSlot(-1);
            Destroy(divisionWin);
        }

        void AssignItemToSlot(int count)
        {
            if (count > 0)
            {
                if (destinationSlot.slotType == SlotType.Inventory && Item.itemBeingDragged.GetComponentInParent<Slot>().slotType == SlotType.Shop)
                {
                    HangarData.instance.player.Credit -= Item.itemBeingDragged.GetComponent<Item>().price * count;
                    HangarData.instance.shop.CreditUpdate();
                    if (count < Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity)
                    {
                        Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity -= count;
                        Instantiate(Item.itemBeingDragged, destinationSlot.transform).GetComponent<AmmoClip>().Quantity = count;
                        destinationSlot.isVacant = false;

                        Item.itemBeingDragged = null;
                        return;
                    }
                }
                else if (destinationSlot.slotType == SlotType.Shop && Item.itemBeingDragged.GetComponentInParent<Slot>().slotType == SlotType.Inventory)
                {
                    HangarData.instance.player.Credit += Item.itemBeingDragged.GetComponent<Item>().price * count;
                    HangarData.instance.shop.CreditUpdate();
                    if (count < Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity)
                    {
                        Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity -= count;
                        Instantiate(Item.itemBeingDragged, destinationSlot.transform).GetComponent<AmmoClip>().Quantity = count;
                        destinationSlot.isVacant = false;

                        Item.itemBeingDragged = null;
                        return;
                    }
                }
                else if (destinationSlot.slotType == SlotType.Inventory && Item.itemBeingDragged.GetComponentInParent<Slot>().slotType == SlotType.AmmoSlots)
                {
                    if (Item.activeContainer.GetAmmo(Item.itemBeingDragged.GetComponent<AmmoClip>().Type, count))
                    {
                        if (count < Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity)
                        {
                            Instantiate(Item.itemBeingDragged, destinationSlot.transform).GetComponent<AmmoClip>().Quantity = count;
                            Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity -= count;
                            destinationSlot.isVacant = false;

                            Item.itemBeingDragged = null;
                            return;
                        }
                        else
                        {
                            HangarData.instance.properties.GetComponentInChildren<AmmoSlots>().ammoInSlots.Remove(Item.itemBeingDragged);
                        }
                    }
                }
                else if (destinationSlot.slotType == SlotType.AmmoSlots && Item.itemBeingDragged.GetComponentInParent<Slot>().slotType == SlotType.Inventory)
                {
                    Item.activeContainer.AddAmmo(Item.itemBeingDragged.GetComponent<AmmoClip>().Type, count);
                    if (count < Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity)
                    {
                        HangarData.instance.properties.GetComponentInChildren<AmmoSlots>().ammoInSlots.Add(Instantiate(Item.itemBeingDragged, destinationSlot.transform));
                        HangarData.instance.properties.GetComponentInChildren<AmmoSlots>().ammoInSlots.Last().GetComponent<AmmoClip>().Quantity = count;
                        Item.itemBeingDragged.GetComponent<AmmoClip>().Quantity -= count;
                        destinationSlot.isVacant = false;

                        Item.itemBeingDragged = null;
                        return;
                    }
                    else
                    {
                        HangarData.instance.properties.GetComponentInChildren<AmmoSlots>().ammoInSlots.Add(Item.itemBeingDragged);
                    }
                }
            }
            else
            {
                Item.itemBeingDragged = null;
                return;
            }
            Item.itemBeingDragged.transform.parent.GetComponent<Slot>().isVacant = true;
            Item.itemBeingDragged.transform.SetParent(destinationSlot.transform);
            destinationSlot.isVacant = false;
            Item.itemBeingDragged = null;
        }
    }
}
