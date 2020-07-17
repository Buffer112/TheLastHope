using TheLastHope.Hangar;
//using TheLastHope.Hippo; // Temporary. For the Hippo needs.
using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using TheLastHope.Player;
using TheLastHope.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheLastHope.Management
{
    /// <summary>
    /// Class that controls whole scene.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private SceneData sceneData;
        private PathLengthCounter pathCounter = new PathLengthCounter();
        [SerializeField] private GeneratorManager generatorManager;
        [SerializeField] private int targetEnemyCount;
        [SerializeField] private int targetPropsCount;
        [SerializeField] private float trainSpeed;
        [SerializeField] private AWorldMover worldMover;
        [SerializeField] private ADestroyer destroyer;
        [SerializeField] private WeaponManager weaponController;
        [SerializeField] private MainPlayer mainPlayer;
        [SerializeField] private RenderManager renderManager;
        [SerializeField] private SkillManager skillManager;
        [SerializeField] private TriggerManager triggerManager;
        [SerializeField] private TrainManager trainManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private float lineLength;
        [SerializeField] private int linesCount;
        [SerializeField] private string currentLevel;
        [SerializeField] private Canvas winCanvas;
        [SerializeField] private Canvas looseCanvas;
        [Tooltip("Number of lines to pregenerate scene")]
        [SerializeField] private int firstFrameLengthInLines = 14;
        private GameObject playerTrain;
        [SerializeField] private int credits;
        private Player player;
        [SerializeField] private HangarData hangar;

        // Start is called before the first frame update
        private void Start()
        {
            SaveLoadManager.Load(out playerTrain, out player);
            sceneData = new SceneData();
            //
            playerTrain = FindObjectOfType<TrainManager>().gameObject;
            //
            mainPlayer = playerTrain.GetComponentInChildren<MainPlayer>();
            trainManager = playerTrain.GetComponentInChildren<TrainManager>();
            trainManager.Init(sceneData);
            mainPlayer.Init();
            sceneData.TargetEnemyCount = targetEnemyCount;
            sceneData.TargetPropsCount = targetPropsCount;
            sceneData.TrainSpeed = trainSpeed;
            sceneData.LineLength = lineLength;
            sceneData.CurrentLevel = currentLevel;
            sceneData.LinesCount = linesCount;
            sceneData.Player = player;
            worldMover.SetupMover(sceneData);
            generatorManager.Init(sceneData);
            renderManager.Init();
            triggerManager.Init(generatorManager);
            //hangar.Init(sceneData); //Commited by Danny

            weaponController.Init(sceneData);


            trainStuffAdd();
            if (skillManager != null)
            {
                skillManager.Init();
            }
            pathCounter.Init(sceneData);
            sceneData.LinesOverall = generatorManager.LevelLenght;
            GenerateFirstArea();
            sceneData.CurrentState = GameState.Start;
            //sceneData.CurrentState = GameState.Preroll;
            //sceneData.CurrentState = GameState.Wait;
            //sceneData.CurrentState = GameState.Loop;
            //SaveLoadManager.SavePlayer(playerTrain, sceneData.Player); TEMPORARY OFF.
        }

        private void trainStuffAdd()
        {
            foreach (ABaseObject tur in weaponController.TurretList)
            {
                print("111");
                sceneData.TrainStuff.Add(tur);
                print(sceneData.TrainStuff);
            }

            sceneData.TrainStuff.Add(mainPlayer);
        }

        // Update is called once per frame
        private void Update()
        {
            if (sceneData.CurrentState == GameState.Loop)
            {
                generatorManager.UpdateGenerators(sceneData);
                weaponController.UpdateWeapons(sceneData, Time.deltaTime);
                renderManager.UpdateRender(sceneData);
                //Something goes wrong!
                uiManager.UIUpdate(sceneData, pathCounter);
                if (skillManager != null)
                {
                    skillManager.SkillUpdate(sceneData);
                }
                pathCounter.CountLenght(sceneData, Time.deltaTime);
                foreach (var trigger in sceneData.Triggers)
                {
                    print($"T {trigger.name}");
                }
                triggerManager.ExecuteCurrentEvents(sceneData);
                //******************************************//
                //Temp Progress Bar
                //******************************************//
                trainManager.UpdateTrain(sceneData);
                foreach (var enemy in sceneData.Enemies)
                {
                    enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
                }

                destroyer.Destroy(sceneData);
                worldMover.MoveWorld(sceneData, Time.deltaTime);
                mainPlayer.UpdatePlayer(sceneData);
            }

            else if (sceneData.CurrentState == GameState.Lose)
            {
                EndGame(false, sceneData);
                generatorManager.UpdateGenerators(sceneData);
                foreach (var enemy in sceneData.Enemies)
                {
                    enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
                }

                destroyer.Destroy(sceneData);
                worldMover.MoveWorld(sceneData, Time.deltaTime);
                mainPlayer.UpdatePlayer(sceneData);
            }
            else if (sceneData.CurrentState == GameState.Win)
            {
                EndGame(true, sceneData);
                foreach (var enemy in sceneData.Enemies)
                {
                    enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
                }

                destroyer.Destroy(sceneData);
                worldMover.MoveWorld(sceneData, Time.deltaTime);
                mainPlayer.UpdatePlayer(sceneData);
            }
            else if (sceneData.CurrentState == GameState.Start)
            {
                uiManager.Init(sceneData);
                sceneData.CurrentState = GameState.Loop;
            }
            else if (sceneData.CurrentState == GameState.Wait)
            {

            }


        }

        private void EndGame(bool win, SceneData sceneData)
        {
            if (win)
            {
                var _oldSpeed = sceneData.TrainSpeed;
                sceneData.TrainSpeed = Mathf.Lerp(_oldSpeed, 0, Time.deltaTime);
                print("You win!");
                winCanvas.gameObject.active = true;
                foreach (GameObject enemy in sceneData.Enemies)
                {
                    var enemyController = enemy.GetComponent<AEnemy>();
                    enemyController.Health = 0;
                }
            }
            else
            {
                var _oldSpeed = sceneData.TrainSpeed;
                sceneData.TrainSpeed = Mathf.Lerp(_oldSpeed, 0, Time.deltaTime);
                print("You lose!");
                looseCanvas.gameObject.active = true;
            }
        }

        public void StartOver()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void GenerateFirstArea()
        {
            for (var i = 0; i < firstFrameLengthInLines; i++)
            {
                generatorManager.UpdateGenerators(sceneData);
                worldMover.MoveWorld(sceneData, sceneData.LineLength / sceneData.TrainSpeed);
                pathCounter.CountLenght(sceneData, sceneData.LineLength / sceneData.TrainSpeed);
            }
        }

        public void RunScene()
        {
            sceneData.CurrentState = GameState.Start;
        }
    }
}

