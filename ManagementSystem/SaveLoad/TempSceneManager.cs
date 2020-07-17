using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management
{
    public class TempSceneManager : MonoBehaviour
    {
        [SerializeField] int tempCredits =2;
        [SerializeField] bool save;
        [SerializeField] bool load;
        // Start is called before the first frame update
        void Start()
        {
            if (load)
            {
                Player player;
                GameObject playerTrain;
                SaveLoadManager.Load(out playerTrain, out player);
                foreach (var car in FindObjectsOfType<Carriage>())
                {
                    car.Init();
                }
            }
            if (save)
            {
                foreach (var car in FindObjectsOfType<Carriage>())
                {
                    car.Init();
                }
                Player player = new Player(tempCredits);
                GameObject playerTrain = FindObjectOfType<TrainManager>().gameObject;
                SaveLoadManager.SavePlayer(playerTrain, player);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

