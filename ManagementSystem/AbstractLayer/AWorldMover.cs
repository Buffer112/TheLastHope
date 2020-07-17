using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class AWorldMover : MonoBehaviour
    {
        public abstract void MoveWorld(SceneData sceneData, float deltaTime);
        public abstract void SetupMover(SceneData sceneData);
    }
}

