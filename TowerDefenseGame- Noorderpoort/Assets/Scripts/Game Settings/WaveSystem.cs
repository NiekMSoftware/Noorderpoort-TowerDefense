using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [Header("Waves")]
    public int wavesEnded = 0;

    [SerializeField] private int wavesPerBoss;
    int lastBoss;
    bool spawnBossWave;
    [SerializeField] private GameObject[] bosses;

    [SerializeField] private int moneyPerWave;
     
    [Header("Spawning Enemies")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private EnemyScriptable[] enemyStats;
    [Space]
    [SerializeField] private float spawnCooldown = 2;
    [SerializeField] private float roundCooldown = 2;
    float currentSpawnCooldown;
     
    float timeTillSpawn = 0;
    float timeTillWave = 0;
    bool spawning = false;
    int spawnedEnemies = 0;
     
    [Header("Enemy Groups")]
    [SerializeField] private int maxGroupSize = 10;
    [SerializeField] private int minGroupSize = 10;
     
    GameObject spawningGroup;
    int spawnedGroup = 0;
     
    int groupSize;
    int type = 0;
    bool hasGroup = false;
    
    [Header("Enemy Chances")]
    [SerializeField] private int[] enemyChanceStopsAt;
    [SerializeField] private int[] enemyStartsAt;
    [SerializeField] private int[] enemyMinChance;
    [SerializeField] private int[] enemyMaxChance;
     
    public int[] enemyChance = new int[10];
     
    [Header("Enemy Scaling")]
    [SerializeField] private float enemyAmountScaleFactor = 2;
    [SerializeField] private int enemiesThisRound;
    [SerializeField] private int maxEnemiesPerRound;
    float enemiesLastRound;

    public float enemyHealthMultiplier = 1;
    [SerializeField] private float enemyHealthMultiplierPerRound = 0.05f;
     
    [Header("Money")]
    bool gaveMoney = false;
    Bitscript bits;

    [Header("Showing Path")]
    [SerializeField] private EnemyScriptable arrowEnemy;
    [SerializeField] private int arrowAmount;
    [SerializeField] private float arrowCooldown;
    private int arrowsShown;

    [Header("Other")]
    [SerializeField] private Transform enemyEmpty;
    [SerializeField] private Transform destination;
    bool activatedTimer;

    [SerializeField] TMP_Text text;
    [SerializeField] private OutsideWavesystem waveSys;

    void Start() {
        //OutsideWaveSystem basically calls the wave system through ui, since this wave system gets instantiated every map
        waveSys = FindAnyObjectByType<OutsideWavesystem>();
        waveSys.waveSystem = this;
        for(int i = 0; i < enemyStats.Length; i++)
        {
            enemyMaxChance[i] = enemyStats[i].maxSpawnChance;
            enemyMinChance[i] = enemyStats[i].minSpawnChance;
            enemyStartsAt[i] = enemyStats[i].startSpawnWave;
            enemyChanceStopsAt[i] = enemyStats[i].endSpawnWave;
        }
        text = GameObject.Find("WaveText").GetComponent<TMP_Text>();
        
        bits = FindObjectOfType<Bitscript>();
         
        ChanceCalculator();
         
        //Basic amount of enemies for first round
        enemiesThisRound = 5;
         
        currentSpawnCooldown = spawnCooldown;
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Spawn arrows if you are in wave -1
        if(wavesEnded == -1)
        {
            timeTillSpawn -= Time.deltaTime;
            if (timeTillSpawn < 0)
            {
                StartCoroutine(SpawnArrow(0.05f, 3));
                timeTillSpawn = arrowCooldown;
                arrowsShown++;
                if(arrowsShown >= arrowAmount) { wavesEnded = 0;roundStart();}
            }
        }
         
        if (spawning == true && wavesEnded > 0)
        {
            if (spawnBossWave)
            {
                //Gets one of the bosses and counts it as a group
                int bos = Random.Range(0, bosses.Length);
                GameObject enemy = Instantiate(bosses[bos], spawnPoint.position, spawnPoint.rotation);
                enemy.transform.parent = enemyEmpty;
                enemy.GetComponent<EnemyNavMesh>().movePositionTransform = destination;
                timeTillSpawn = currentSpawnCooldown;
                spawnedGroup++;
                spawnedEnemies++;
                spawnBossWave = false;
            }
            if (hasGroup == false)
            {
                //Decides the group type and size
                groupSize = Random.Range(minGroupSize, maxGroupSize);
                type = randomEnemy();
                spawningGroup = enemies[type];
                spawnedGroup = 0;
                hasGroup = true;
            }
             
            timeTillSpawn -= Time.deltaTime;
            if (timeTillSpawn < 0)
            {
                //Spawns the enemies
                GameObject enemy = Instantiate(spawningGroup, spawnPoint.position, spawnPoint.rotation);
                enemy.transform.parent = enemyEmpty;
                enemy.GetComponent<EnemyNavMesh>().movePositionTransform = destination;
                timeTillSpawn = currentSpawnCooldown;
                spawnedGroup++;
                spawnedEnemies++;
            }
             
            //If the size of the current group reaches the expected group size, get a new group
            if (spawnedGroup >= groupSize)
            {
                hasGroup = false;
            }
             
            if (spawnedEnemies >= enemiesThisRound)
            {
                //Finishes spawning wave
                enemiesLastRound = enemiesThisRound;
                spawning = false;
            }
        }

        //if all enemies are dead
        if (enemyEmpty.childCount == 0 && wavesEnded > -1)
        {
            //Ends wave
            if (gaveMoney == false)
            {
                if (wavesEnded > 0)
                {
                    bits.AddBits(moneyPerWave);
                }
                wavesEnded++;
                gaveMoney = true;
            }

            if (activatedTimer == false)
            {
                //Waits a bit before starting next round
                timeTillWave = roundCooldown;
                activatedTimer = true;
            }

            //Turns on the clock
            if (!spawning)
            {
                waveSys.waitingUI.SetActive(true);
                timeTillWave -= Time.deltaTime;
            }
            waveSys.time.text = (((int)timeTillWave) + 1).ToString();

            //Starts next wave
            if (timeTillWave < 0 || waveSys.skipWaveTime)
            {
                if(spawning == false)
                {
                    waveSys.waitingUI.SetActive(false);
                    roundEnd();
                }
            }
        }
        else
        {
            //if its spawning turn off the clock
            waveSys.waitingUI.SetActive(false);
        }
    }

    /// <summary>
    /// Skips the current waiting, forces the next wave to start
    /// </summary>
    public void SkipTime()
    {
        timeTillWave = 0;
    }

    /// <summary>
    /// Spawns arrows with a slight cooldown between them
    /// </summary>
    /// <param name="cooldown"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public IEnumerator SpawnArrow(float cooldown, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            //Spawns the arrows
            GameObject enemy = Instantiate(arrowEnemy.prefab, spawnPoint.position, spawnPoint.rotation);
            enemy.transform.parent = enemyEmpty;
            enemy.GetComponent<EnemyNavMesh>().movePositionTransform = destination;
            yield return new WaitForSeconds(cooldown);
        }
    }
    public void roundStart() {
        text.text = "Wave: " + (wavesEnded + 1).ToString();
        try
        {
            //Every wavesperboos the int goes up by 1
            int currentBoss = (int)((wavesEnded + 1) / wavesPerBoss);
            if (lastBoss < currentBoss)
            {
                spawnBossWave = true;
                lastBoss = currentBoss;
            }
        }
        catch
        {
            print("Error!");
        }
         
        //Decides how many enemies to spawn
        enemiesLastRound = enemiesThisRound;
        float scale = enemyAmountScaleFactor * wavesEnded;
        float i = enemiesLastRound + scale;
        enemiesThisRound = Mathf.RoundToInt(i);
         
        //Makes sure it doesnt spawn too many
        if (enemiesThisRound > maxEnemiesPerRound)
        {
            enemiesThisRound = maxEnemiesPerRound;
        }
         
        //Makes the spawning faster if there are more enemies
        if (enemiesThisRound > 25 && enemiesThisRound< 50)
        {
            currentSpawnCooldown = spawnCooldown / 2;
        } else if (enemiesThisRound >= 50 &&enemiesThisRound <= 100)
        {
            currentSpawnCooldown = spawnCooldown / 3;
        } else if (enemiesThisRound > 100)
        {
            currentSpawnCooldown = spawnCooldown / 6;
        }
        else
        {
            currentSpawnCooldown = spawnCooldown;
        }
         
        //Sets up for spawning
        ChanceCalculator();
        spawnedEnemies = 0;
        spawning = true;
        timeTillSpawn = 0;
    }
    public void roundEnd()
    {
        //Adds health multiplier
        enemyHealthMultiplier = enemyHealthMultiplier + enemyHealthMultiplierPerRound;

        //Resets bools for the next wave
        hasGroup = false;
        activatedTimer = false;
        gaveMoney = false;
         
        roundStart();
    }
    public int randomEnemy()
    {
        int chosenType = 0;
        int tot = 0;

        //Chooses a type of enemy
        for (int i = 1; i < enemies.Length; i++)
        {
            tot = tot + enemyChance[i - 1];
        }
        int newtype = Random.Range(0, 100);
        int total = 0;
        bool hasType = false;
        for (int i = 1; i < enemies.Length; i++)
        {
            //Puts all the numbers together, and chooses an enemy;
            if (newtype >= total && newtype < enemyChance[i - 1] + total)
            {
                chosenType = i;
                hasType = true;
            }
            total += enemyChance[i - 1];
        }
        if (hasType == false)
        {
            chosenType = 0;
        }
        return chosenType;
    }
    public void ChanceCalculator()
    {
        //Chance for every type of enemy to spawn , Math
        for (int enemy = 0; enemy < enemies.Length-1; enemy++)
        {
            if (wavesEnded < enemyStartsAt[enemy])
            {
                //if the wave is before the enemy can spawn, 0% chance
                enemyChance[enemy] = 0;
            }
            else if (wavesEnded >= enemyChanceStopsAt[enemy])
            {
                //if the wave is above the enemy chance increase wave, max chance
                enemyChance[enemy] = enemyMaxChance[enemy];
            }
            else
            {
                //if the wave is between the start and end, calculate the % chance.


                //Math

                //Total waves between start and stop
                int negativeWaves = enemyChanceStopsAt[enemy] - enemyStartsAt[enemy];
                //current wave over start
                int roundsOverChance = wavesEnded - enemyStartsAt[enemy];
                //total difference in chance between starting and stopping
                int chanceDifference = enemyMaxChance[enemy] - enemyMinChance[enemy];
                //Amount of % the chance needs to go up
                int chanceUpPerWave = chanceDifference / (negativeWaves - 1);
                //the amount of % + the minimum chance
                enemyChance[enemy] = enemyMinChance[enemy] + (chanceUpPerWave * roundsOverChance);
            }
        }
    }
}
