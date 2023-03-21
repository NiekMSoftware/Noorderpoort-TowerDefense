using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public string status = "nothing";
    [SerializeField] private int gameRound = 0;
    [SerializeField] private Transform enemyEmpty;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnCooldown = 2;
    [SerializeField] private float roundCooldown = 2;
    [SerializeField] private int maxGroupSize = 10;
    [SerializeField] private int minGroupSize = 10;
    [SerializeField] private int amountOfEnemyTypes = 2;
    [SerializeField] private int normalEnemyChance = 100;
    [SerializeField] private int enemy2ChanceStopsAt = 10;
    [SerializeField] private int enemy2StartsAt = 5;
    [SerializeField] private int enemy2MinChance = 5;
    [SerializeField] private int enemy2MaxChance = 30;
    [SerializeField] private float enemyAmountScaleFactor = 2;
    [SerializeField] private int enemiesThisRound;
    [SerializeField] private int maxEnemiesPerRound;
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
    int enemy2Chance = 0;
    int enemy1Chance = 100;
    float enemiesLastRound;
    public float currentSpawnCooldown;
    // Start is called before the first frame update
    void Start()
    {
        enemiesThisRound = 5;
        currentSpawnCooldown = spawnCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = speed;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            roundStart();
        }
        if (spawning == true)
        {
            if (hasGroup == false)
            {
                groupSize = Random.Range(minGroupSize, maxGroupSize);
                type = randomEnemy(type);
                Debug.Log(type);
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
        status = scale.ToString();
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
        hasGroup = false;
        activatedTimer = false;
        roundStart();
    }
    public int randomEnemy(int type)
    {
        int chosenType = 1;
        int maxRandom = normalEnemyChance + enemy2MaxChance;
        int newtype = Random.Range(0, maxRandom);
        Debug.Log(newtype + " Random");
        if (newtype < normalEnemyChance)
        {
            chosenType = 0;
        } else if ( newtype >= normalEnemyChance && newtype < enemy2MaxChance + normalEnemyChance)
        {
            chosenType = 1;
        }
        return chosenType;
    }
    public void ChanceCalculator()
    {
        if (gameRound < enemy2StartsAt)
        {
            enemy2Chance = 0;
        }
        else if (gameRound >= enemy2ChanceStopsAt)
        {
            enemy2Chance = enemy2MaxChance;
        }
        else
        {

            
            int negativeWaves = enemy2ChanceStopsAt - enemy2StartsAt;
            
            int roundsOverChance = gameRound - enemy2StartsAt;
            
            int chanceDifference = enemy2MaxChance - enemy2MinChance; 
            
            int chanceUpPerWave = chanceDifference / (negativeWaves - 1); 
            //95 / 7

            enemy2Chance = enemy2MinChance + (chanceUpPerWave * roundsOverChance);
        }
    }
}
