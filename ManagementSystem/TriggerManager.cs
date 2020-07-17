using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Management
{
    public class TriggerManager : MonoBehaviour
    {
        GeneratorManager genMan;

        public void Init(GeneratorManager genMan)
        {
            this.genMan = genMan;
        }

        public void ExecuteCurrentEvents(SceneData sceneData)
        {
            while(sceneData.Triggers.Count>0)
            {
                GameObject currentTrigger = sceneData.Triggers[0];
                ProcessTrigger(currentTrigger, sceneData);
            }
        }

        void ProcessTrigger(GameObject currentTrigger, SceneData sceneData)
        {
            ATrigger trigger = currentTrigger.GetComponent<ATrigger>();
            switch (trigger.Type)
            {
                case TriggerType.EndGame:
                    print("EndGame!");
                    sceneData.CurrentState = GameState.Win;
                    break;
                case TriggerType.GenerateEnemy:

                    genMan.GenerateDynamicFromTrigger(trigger.EnemiesNames, sceneData, trigger.Pattern);
                    break;
            }
            sceneData.Triggers.RemoveAt(0);
        }
    }
}

