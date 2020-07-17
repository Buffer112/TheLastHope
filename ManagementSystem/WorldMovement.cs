using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

class WorldMovement : AWorldMover
{
    [SerializeField] float speedDivider;
    float trainPrevSpeed;

    /// <summary>
    /// Initializes script's variables according to train speed and some other.
    /// </summary>
    /// <param name="sceneData"></param>
    public override void SetupMover(SceneData sceneData)
    {
        trainPrevSpeed =  sceneData.TrainSpeed;
    }
    /// <summary>
    /// Moves objects static on the scene. Except train.
    /// </summary>
    /// <param name="sceneData"></param>
    /// <param name="deltaTime"></param>
    public override void MoveWorld(SceneData sceneData, float deltaTime)
    {
        for (var i = 0; i < sceneData.Props.Count; i++)
        {
            sceneData.Props[i].transform.position = new Vector3(sceneData.Props[i].transform.position.x - sceneData.TrainSpeed * deltaTime,
                                                                sceneData.Props[i].transform.position.y,
                                                                sceneData.Props[i].transform.position.z);
        }
        for (var i = 0; i < sceneData.Rails.Count; i++)
        {
            sceneData.Rails[i].transform.position = new Vector3(sceneData.Rails[i].transform.position.x - sceneData.TrainSpeed * deltaTime,
                                                                sceneData.Rails[i].transform.position.y,
                                                                sceneData.Rails[i].transform.position.z);
        }

        if (trainPrevSpeed != sceneData.TrainSpeed)
        {
            for (var i=0; i< sceneData.EnemiesPatterns.Count;i++)
            {
                sceneData.EnemiesPatterns[i].transform.position = new Vector3(sceneData.EnemiesPatterns[i].transform.position.x + (sceneData.TrainSpeed - trainPrevSpeed) * deltaTime,
                                                    sceneData.EnemiesPatterns[i].transform.position.y,                 
                                                    sceneData.EnemiesPatterns[i].transform.position.z);
            }
            trainPrevSpeed = sceneData.TrainSpeed;
        }
    }
}
