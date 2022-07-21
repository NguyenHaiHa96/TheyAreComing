using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, ISubcribers
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float respawnCooldown;
    [SerializeField] private int enemiesSpawned;
    [SerializeField] private int maxEnemiesSpawned;
    [SerializeField] private bool isSpawning;

    private GameManager gameManager;
    private Vector3 spawnPosition;
    private Vector3 direction;
    private float defaultMoveSpeed;
    private float timeToSpawn;
    private float timer;
    private int numberOfEnemies;

    public bool IsSpawning { get => isSpawning; set => isSpawning = value; }
    public int NumberOfEnemies { get => numberOfEnemies; set => numberOfEnemies = value; }

    private void Start()
    {
        SubscribeEvent();
    }

    private void OnEnable()
    {
        InitializeVariables();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    private void InitializeVariables()
    {
        moveSpeed = 0f;
        isSpawning = false;
        gameManager = GameManager.Instance;
        direction = Vector3.back;
        defaultMoveSpeed = 10f;
        timer = 0f;
    }

    private void Update()
    {
        Move();      
        SpawnEnemy();
        LimitEnemiesSpawned();
        GetNumberOfEnemies();
    }

    private void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    private int GetNumberOfEnemies()
    {
        numberOfEnemies = this.transform.childCount;
        return numberOfEnemies;
    }

    private void SpawnEnemy()
    {
        if (!isSpawning)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= timeToSpawn)
        {
            spawnPosition = RandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, transform.rotation * Quaternion.Euler(0f, 180f, 0f), this.transform);
            enemiesSpawned += 1;
            timeToSpawn += respawnCooldown;
        }
    }

    private Vector3 RandomSpawnPosition()
    {
        float xPosition = Random.Range(-9.5f, 9.6f);
        Vector3 newSpawnPosition = new Vector3(transform.position.x + xPosition,
            transform.position.y,
            transform.position.z);
        return newSpawnPosition;
    }

    private void LimitEnemiesSpawned()
    {
        if (enemiesSpawned >= maxEnemiesSpawned)
        {
            isSpawning = false;
        }
    }

    public void SubscribeEvent()
    {
        GameManager.Instance.OnGameStarted += CheckGameStarted;
    }

    public void UnsubscribeEvent()
    {
        GameManager.Instance.OnGameStarted -= CheckGameStarted;
    }

    private void CheckGameStarted(int obj)
    {
        isSpawning = true;
        moveSpeed = defaultMoveSpeed;
    }
}
