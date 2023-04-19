using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaveSystem : MonoBehaviour
{
    //Testing
    int gameRound = 0;
    public int wavesEnded = 0;
    [SerializeField] private Transform enemyEmpty;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform destination;
    //Spawning
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnCooldown = 2;
    [SerializeField] private float roundCooldown = 2;
    //Groups
    [SerializeField] private int maxGroupSize = 10;
    [SerializeField] private int minGroupSize = 10;
    //Chances
    [SerializeField] private int[] enemyChanceStopsAt;
    [SerializeField] private int[] enemyStartsAt;
    [SerializeField] private int[] enemyMinChance;
    [SerializeField] private int[] enemyMaxChance;
    //Amount scaling
    [SerializeField] private float enemyAmountScaleFactor = 2;
    [SerializeField] private int enemiesThisRound;
    [SerializeField] private int maxEnemiesPerRound;
    //HP multiplying
    public float enemyHealthMultiplier = 1;
    [SerializeField] private float enemyHealthMultiplierPerRound = 0.05f;
    public int speed;
    GameObject spawningGroup;
    float timeTillSpawn = 0;
    float timeTillWave = 0;
    bool spawning = false;
    bool hasGroup = false;
    bool activatedTimer;
    int spawnedEnemies = 0;
    int spawnedGroup = 0;
    int groupSize;
    int type = 0;
    public int[] enemyChance = new int[2];
    int basicEnemyChance = 100;
    float enemiesLastRound;
    public float currentSpawnCooldown;
    bool gaveMoney = false;
    // Start is called before the first frame update
    void Start()
    {
        ChanceCalculator();
        enemiesThisRound = 5;
        currentSpawnCooldown = spawnCooldown;
        enemyHealthMultiplier = enemyHealthMultiplier - enemyHealthMultiplierPerRound;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            roundStart();
        }
        if (spawning == true)
        {
            if (hasGroup == false)
            {
                groupSize = Random.Range(minGroupSize, maxGroupSize);
                type = randomEnemy();
                spawningGroup = enemies[type];
                Debug.Log("Spawning " + type);
                spawnedGroup = 0;
                hasGroup = true;
            }
            timeTillSpawn -= Time.deltaTime;
            if (timeTillSpawn < 0)
            {
                GameObject enemy = Instantiate(spawningGroup, spawnPoint.position, spawnPoint.rotation);
                enemy.transform.parent = enemyEmpty;
                enemy.GetComponent<EnemyNavMesh>().movePositionTransform = destination;
                timeTillSpawn = currentSpawnCooldown;
                spawnedGroup++;
                spawnedEnemies++;
                Debug.Log("Spawned " + spawnedEnemies);
            }
            if (spawnedGroup >= groupSize)
            {
                hasGroup = false;
            }
            if (spawnedEnemies >= enemiesThisRound)
            {
                enemiesLastRound = enemiesThisRound;
                gameRound++;
                spawning = false;
            }
        }
        if (enemyEmpty.childCount == 0)
        {
            if (gaveMoney == false)
            {
                wavesEnded++;
                gaveMoney = true;
            }
            if (activatedTimer == false)
            {
                timeTillWave = roundCooldown;
                activatedTimer = true;
            }
            timeTillWave -= Time.deltaTime;
            if (timeTillWave < 0)
            {
                roundEnd();
            }
        }
    }
    public void roundStart()
    {
        Debug.Log("Started Round " + gameRound);
        enemiesLastRound = enemiesThisRound;
        float scale = enemyAmountScaleFactor * gameRound;
        float i = enemiesLastRound + scale;
        enemiesThisRound = Mathf.RoundToInt(i);
        if (enemiesThisRound > maxEnemiesPerRound)
        {
            enemiesThisRound = maxEnemiesPerRound;
        }
        if (enemiesThisRound > 50 && enemiesThisRound< 100)
        {
            currentSpawnCooldown = spawnCooldown / 2;
        } else if (enemiesThisRound >= 100 &&enemiesThisRound <= 200)
        {
            currentSpawnCooldown = spawnCooldown / 3;
        } else if (enemiesThisRound > 200)
        {
            currentSpawnCooldown = spawnCooldown / 6;
        }
        else
        {
            currentSpawnCooldown = spawnCooldown;
        }
        spawnedEnemies = 0;
        spawning = true;
        timeTillSpawn = 0;
        ChanceCalculator();
    }
    public void roundEnd()
    {
        enemyHealthMultiplier = enemyHealthMultiplier + enemyHealthMultiplierPerRound;
        hasGroup = false;
        activatedTimer = false;
        gaveMoney = false;
        roundStart();
    }
    public int randomEnemy()
    {
        int chosenType = 0;
        int newtype = Random.Range(0, 100);
        Debug.Log(newtype + "random");
        int total = 0;
        if (newtype >= 0 && newtype < enemyChance[0])
        {
            chosenType = 1;
        }
        total = enemyChance[0];
        if (newtype >= enemyChance[0] && newtype < enemyChance[1] +total)
        {
            chosenType = 2;
        }
        total = total + enemyChance[1];
        return chosenType;
    }
    public void ChanceCalculator()
    {
        for (int enemy = 0; enemy < enemies.Length - 1; enemy++)
        {
            Debug.Log(enemy);
            if (gameRound < enemyStartsAt[enemy])
            {
                enemyChance[enemy] = 0;
            }
            else if (gameRound >= enemyChanceStopsAt[enemy])
            {
                enemyChance[enemy] = enemyMaxChance[enemy];
            }
            else
            {

                int negativeWaves = enemyChanceStopsAt[enemy] - enemyStartsAt[enemy];

                int roundsOverChance = gameRound - enemyStartsAt[enemy];

                int chanceDifference = enemyMaxChance[enemy] - enemyMinChance[enemy];

                int chanceUpPerWave = chanceDifference / (negativeWaves - 1);

                enemyChance[enemy] = enemyMinChance[enemy] + (chanceUpPerWave * roundsOverChance);
            }
        }
    }
}
