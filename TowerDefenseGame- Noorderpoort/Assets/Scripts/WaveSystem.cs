using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private int gameRound = 0;
    [SerializeField] private Transform enemyEmpty;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int[] enemiesPerRound;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnCooldown = 2;
    [SerializeField] private float roundCooldown = 2;
    [SerializeField] private int maxGroupSize = 10;
    [SerializeField] private int minGroupSize = 10;
    [SerializeField] private int amountOfEnemyTypes = 2;
    [SerializeField] private int normalEnemyChance = 70;
    [SerializeField] private int enemy2Chance = 30;
    GameObject spawningGroup;
    float timeTillSpawn = 0;
    bool spawning = false;
    bool hasGroup = false;
    int spawnedEnemies = 0;
    int spawnedGroup = 0;
    int groupSize;
    int lastType = -1;
    int type = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
                type = randomEnemy(type);
                lastType = type;
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
                timeTillSpawn = spawnCooldown;
                spawnedGroup++;
                spawnedEnemies++;
                Debug.Log("Spawned " + spawnedEnemies);
            }
            if (spawnedGroup >= groupSize)
            {
                hasGroup = false;
            }
            if (spawnedEnemies >= enemiesPerRound[gameRound])
            {
                gameRound++;
                spawning = false;
            }
        }
        if (enemyEmpty.childCount == 0)
        {
            roundEnd();
        }
    }
    public void roundStart()
    {
        Debug.Log("Started Round " + gameRound);
        spawnedEnemies = 0;
        spawning = true;
        timeTillSpawn = 0;
    }
    public void roundEnd()
    {
        lastType = -1;
        roundStart();
    }
    public int randomEnemy(int type)
    {
        int maxRandom = normalEnemyChance + enemy2Chance;
        int newtype = Random.Range(0, maxRandom);
        Debug.Log(maxRandom + " Random");
        if (newtype < normalEnemyChance)
        {
            newtype = 0;
        } else if ( newtype > normalEnemyChance && newtype < enemy2Chance)
        {
            newtype = 1;
        }
        return newtype;
    }
}
