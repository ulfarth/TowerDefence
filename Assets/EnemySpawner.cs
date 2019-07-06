using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] waypoints;
    public Wave[] waves;
    public int timeBetweenWaves = 5;
    //public GameObject testEnemyPrefab; //todo remove

    private GameManagerBehavior _gameManager;
    private float _lastSpawnTime;
    private int _enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
       // Instantiate(testEnemyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;
        _lastSpawnTime = Time.time;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = _gameManager.Wave;
        if(currentWave < waves.Length)
        {
            float timeInterval = Time.time - _lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;

            if(((_enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                timeInterval > spawnInterval) &&
                _enemiesSpawned < waves[currentWave].maxEnemies)
            {
                _lastSpawnTime = Time.time;
                GameObject newEnemy = Instantiate(waves[currentWave].enemyPrefab,GameObject.FindGameObjectWithTag("EnemyContainer").transform);
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                _enemiesSpawned++;
            }
            if(_enemiesSpawned == waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                _gameManager.Wave++;
                //_gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f); //for when we add the gold
                _enemiesSpawned = 0;
                _lastSpawnTime = Time.time;
            }
        }
        else
        {
            _gameManager.gameOver = true;
            //TODO later
            //GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            //_gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }
}
