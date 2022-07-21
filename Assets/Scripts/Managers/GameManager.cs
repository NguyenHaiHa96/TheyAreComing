using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>, ISubcribers
{
    public static int sceneIndex = 0;

    public event Action<int> OnGameStarted = delegate { };
    public event Action OnLevelCompleted = delegate { };
    public event Action OnGameOver = delegate { };

    [SerializeField] private GameObject bulletStorgage;
    [SerializeField] private EnemySpawner[] enemySpawners;
    [SerializeField] private CameraFollow cameraFollow;

    private PlayerMain playerMain;
    private int[] currentEnemies;
    private int totalEnemies;
    private int currentLevel;
    private bool reachedFinishLine;
    private bool gameStarted;
    private bool levelCompleted;
    private bool gameOver;

    public GameObject BulletStorgage { get => bulletStorgage; set => bulletStorgage = value; }
    public bool GameStarted { get => gameStarted; set => gameStarted = value; }
    public bool LevelCompleted { get => levelCompleted; set => levelCompleted = value; }

    private void Awake()
    {
        InitializeSingleton();
        InitializeVariables();
    }

    private void OnEnable()
    {
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    private void Update()
    {
        CheckCurrentEnemies();
        CheckFinalResult();
        GameState();
    }

    public override void InitializeVariables()
    {     
        currentEnemies = new int[enemySpawners.Length];
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        playerMain = PlayerMain.Instance;
        reachedFinishLine = false;
        gameStarted = false;
        levelCompleted = false;
        gameOver = false;
    }

    public void SubscribeEvent()
    {
        playerMain.OnReachedFinishLine += PlayerReachedFinishLine;
        playerMain.OnOutOfMinions += GameOver;
    }

    private void CheckCurrentEnemies()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            currentEnemies[i] = enemySpawners[i].NumberOfEnemies;
        }
        totalEnemies = 0;
        for (int i = 0; i < currentEnemies.Length; i++)
        {
            totalEnemies += currentEnemies[i];
        }
    }

    public void UnsubscribeEvent()
    {
        playerMain.OnReachedFinishLine -= PlayerReachedFinishLine;
        playerMain.OnOutOfMinions -= GameOver;
    }

    public override void InitializeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void PlayerReachedFinishLine(GameObject[] gameObjects)
    {
        reachedFinishLine = true;
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.IsSpawning = false;
        }
        cameraFollow.MoveSpeed = 0f;
    }

    private void CheckFinalResult()
    {
        if (reachedFinishLine)
        {
            if (playerMain.NumberOfMinions > 0 && totalEnemies == 0)
            {
                levelCompleted = true;
            }

            if (playerMain.NumberOfMinions <= 0 && totalEnemies > 0)
            {
                gameOver = true;
            }
        }     
    }

    private void GameState()
    {
        if (gameStarted)
        {
            OnGameStarted?.Invoke(currentLevel);
            gameStarted = false;
        }
        
        if (levelCompleted)
        {
            OnLevelCompleted?.Invoke();
            levelCompleted = false;
        }

        if (gameOver)
        {
            OnGameOver?.Invoke();
            gameOver = false;
        }
    }

    private void GameOver()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.IsSpawning = false;
        }
        cameraFollow.MoveSpeed = 0f;
        gameOver = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        sceneIndex++;
        if (sceneIndex > SceneManager.sceneCountInBuildSettings)
        {
            RestartLevel();
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
