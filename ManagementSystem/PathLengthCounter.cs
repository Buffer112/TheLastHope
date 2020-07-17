using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management
{
    public class PathLengthCounter
    {
        float drivenPath = 0;
        int currentLine = 0;
        float lineLenght;
        /// <summary>
        /// Number of current line.
        /// </summary>
        public int CurrentLine { get { return currentLine; } }
        
        /// <summary>
        /// Initializes PathLengthCounter.
        /// </summary>
        /// <param name="sceneData"></param>
        public void Init(SceneData sceneData)
        {
            lineLenght = sceneData.LineLength;
        }
        /// <summary>
        /// Updates driven path and current line in SceneData.
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="deltaTime"></param>
        public void CountLenght(SceneData sceneData, float deltaTime)
        {
            drivenPath += sceneData.TrainSpeed * deltaTime;
            currentLine = (int) (drivenPath / lineLenght); //get only whole part of number.
            sceneData.CurrentLine = currentLine;
        }

    }
}

