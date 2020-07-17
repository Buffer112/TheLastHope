using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLastHope.Management.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastHope.Hangar
{
    public class AmmoClip : MonoBehaviour
    {
        /// <summary>
        /// Type of ammunition. Default value is M792HEI_T
        /// </summary>
		/// <summary>
		/// Amount of ammunition
		/// </summary>

		public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                if (gameObject.GetComponentInChildren<Text>())
                    UpdateText();
                else
                    CreateQuantityText();
            }
        }

		public AmmoType Type { get => type; set => type = value; }

		[SerializeField] private Font font;
        [SerializeField] private Color fontColor;
		[SerializeField] private AmmoType type = AmmoType.M792HEI_T;
		[SerializeField] private int quantity;
		private void Start()
        {
            gameObject.GetComponent<Item>().isAmmo = true;
            if (quantity == 0)
                Quantity = 10;
            else
                Quantity = quantity;
        }

        private void CreateQuantityText()
        {
            GameObject txt = new GameObject() { name = "quantity" };
            txt.AddComponent<Text>().font = font;
            txt.transform.SetParent(transform);
            txt.GetComponent<RectTransform>().anchorMax = Vector2.one;
            txt.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            txt.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            txt.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            txt.GetComponent<RectTransform>().localScale = Vector3.one;
            txt.GetComponent<Text>().color = fontColor;
            txt.GetComponent<Text>().text = $"{quantity}";
            txt.GetComponent<Text>().alignment = TextAnchor.LowerLeft;
        }

        private void UpdateText()
        {
            gameObject.GetComponentInChildren<Text>().text = $"{quantity}";
        }
    }
}
