using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempScript : MonoBehaviour
{
    [SerializeField] public MapManager map;
    [SerializeField] public int kek;
    // Start is called before the first frame update
    void Start()
    {
        switch (kek)
        {
            case 1:
                GetComponent<Image>().color = map.blockPointClr;
                break;
            case 2:
                GetComponent<Image>().color = map.currentClr;
                break;
            case 3:
                GetComponent<Image>().color = map.keyPointClr;
                break;
            case 4:
                GetComponent<Image>().color = map.pointClr;
                break;
            case 5:
                GetComponent<Image>().color = map.roadClr;
                break;
            default:
                break;
        }
    }
}
