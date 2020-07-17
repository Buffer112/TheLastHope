using System;
using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.UI;
using UnityEngine;

namespace TheLastHope.Management.Data
{

    /// <summary>
    /// Data of the scene.
    /// </summary>
    /// <remarks>
    /// All the data needed to organize the scene and the game loop.
    /// </remarks>
    [Serializable]
    public class SceneData
    {
        private List<GameObject> props = new List<GameObject>();
        private List<GameObject> enemies = new List<GameObject>();
        private List<GameObject> trainCars = new List<GameObject>();
        private List<GameObject> rails = new List<GameObject>();
        private List<GameObject> enemiesPatterns = new List<GameObject>();
        private List<GameObject> ammos = new List<GameObject>();
        private List<GameObject> triggers = new List<GameObject>();
        private List<ABaseObject> trainStuff = new List<ABaseObject>();
        private List<UIOverlayController> uiOverlayControllers = new List<UIOverlayController>();
        private GameObject _selectedEnemy;
        private GameState State;

        /// <summary>
        /// List of active props on scene.
        /// </summary>
        public List<GameObject> Props { get { return props; } set { props = value; } }
        /// <summary>
        /// List of active enemies on scene.
        /// </summary>
        public List<GameObject> Enemies { get { return enemies; } set { enemies = value; } }
        /// <summary>
        /// List of tran cars on scene.
        /// </summary>
        public List<GameObject> TrainCars { get { return trainCars; } set { trainCars = value; } }
        /// <summary>
        /// List of  active rails on scene.
        /// </summary>
        public List<GameObject> Rails { get { return rails; } set { rails = value; } }
        /// <summary>
        /// List of enemies' spawn points.
        /// </summary>
        public List<GameObject> EnemiesPatterns { get { return enemiesPatterns; } set { enemiesPatterns = value; } }
        /// <summary>
        /// List of shot ammos.
        /// </summary>
        public List<GameObject> Ammos { get { return ammos; } set { ammos = value; } }
        /// <summary>
        /// Count of expected enemies on scene right now.
        /// </summary>
        public int TargetEnemyCount { get; set; }
        /// <summary>
        /// Count of expected props on scene right now.
        /// </summary>
        public int TargetPropsCount { get; set; }
        /// <summary>
        /// Current train speed.
        /// </summary>
        public float TrainSpeed { get; set; }
        /// <summary>
        /// Length of vertical (from camera) line. Used for counting driven path.
        /// </summary>
        public float LineLength { get; set; }
        /// <summary>
        /// Number of horizontalLines on scene.
        /// </summary>
        public int LinesCount { get; set; }
        /// <summary>
        /// Store number of driven lines;
        /// </summary>
        public int CurrentLine { get; set; }
        /// <summary>
        /// Overall lines count;
        /// </summary>
        public int LinesOverall { get; set; }
        /// <summary>
        /// Name of current level.
        /// </summary>
        public string CurrentLevel { get; set; }
        /// <summary>
        /// List of current triggers.
        /// </summary>
        public List<GameObject> Triggers { get => triggers; set => triggers = value; }
        /// <summary>
        /// List of all things that belong to the train.
        /// </summary>
        public List<ABaseObject> TrainStuff { get => trainStuff; set => trainStuff = value; }
        /// <summary>
        /// List of all UI Overlay Controllers.
        /// </summary>
        public List<UIOverlayController> UiOverlayControllers { get => uiOverlayControllers; set => uiOverlayControllers = value; }
        /// <summary>
        /// The enemy being currently selected.
        /// </summary>
        public GameObject SelectedEnemy { get => _selectedEnemy; set => _selectedEnemy = value; }

        public Player Player { get; set; }


        public GameState CurrentState { get; set; }

        public SceneData()
        {
        }

    }

}