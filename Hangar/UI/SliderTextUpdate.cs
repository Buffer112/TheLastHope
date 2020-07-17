using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastHope.Hangar.UI
{
    public class SliderTextUpdate : MonoBehaviour
    {
        Text text;
        private void Start()
        {
            text = GetComponent<Text>();
        }

        public void TextUpdate(float value)
        {
            text.text = $"{value}";
        }
    }
}
