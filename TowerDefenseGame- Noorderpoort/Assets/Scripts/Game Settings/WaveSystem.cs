using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class WaveSystem : MonoBehaviour
{
    [Header("Waves")]
    int gameRound = 0;
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
         
        enemyHealthMultiplier = enemyHealthMultiplier - enemyHealthMultiplierPerRound;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
                //Decides the group type
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
             
            if (spawnedGroup >= groupSize)
            {
                hasGroup = false;
            }
             
            if (spawnedEnemies >= enemiesThisRound)
            {
                //Finishes spawning wave
                enemiesLastRound = enemiesThisRound;
                gameRound++;
                spawning = false;
            }
        }

        if (enemyEmpty.childCount == 0 && wavesEnded > -1)
        {
            //Ends wave
            if (gaveMoney == false)
            {
                wavesEnded++;
                if (wavesEnded > 0)
                {
                    bits.AddBits(moneyPerWave);
                }
                gaveMoney = true;
            }

            if (activatedTimer == false)
            {
                //Waits a bit before starting next round
                timeTillWave = roundCooldown;
                activatedTimer = true;
            }
            if (!spawning)
            {
                waveSys.waitingUI.SetActive(true);
                timeTillWave -= Time.deltaTime;
            }
            waveSys.time.text = (((int)timeTillWave) + 1).ToString();
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
            waveSys.waitingUI.SetActive(false);
            print("Huh?");
        }
    }
    public void SkipTime()
    {
        timeTillWave = 0;
    }
    public IEnumerator SpawnArrow(float cooldown, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            //Spawns the enemies
            GameObject enemy = Instantiate(arrowEnemy.prefab, spawnPoint.position, spawnPoint.rotation);
            enemy.transform.parent = enemyEmpty;
            enemy.GetComponent<EnemyNavMesh>().movePositionTransform = destination;
            yield return new WaitForSeconds(cooldown);
        }
    }
    public void roundStart() {
        this.text.text = "Wave: " + (this.wavesEnded + 1).ToString();
        try
        {
            int e = (int)(wavesEnded / wavesPerBoss);
            if (lastBoss < e)
            {
                spawnBossWave = true;
                lastBoss = e;
            }
        }
        catch
        {
            print("Er Not posible");
        }
         
        //Decides how many enemies to spawn
        enemiesLastRound = enemiesThisRound;
        float scale = enemyAmountScaleFactor * gameRound;
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
        /*print(tot);*/
        int newtype = Random.Range(0, 100);
        int total = 0;
        /*print(newtype + " Random");*/
        bool hasType = false;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (i >= 1)
            {
/*                print(total + " Current total" + i);
                print((enemyChance[i - 1] + total) + "Chance total" + i);*/
                if (newtype >= total && newtype < enemyChance[i - 1] + total)
                {
                    chosenType = i;
                    /*print("Chosen type = " + i  + "(+1)");*/
                    hasType = true;
                }
                total = total + enemyChance[i - 1];
            }
            else
            {
                if (newtype >= 0 && newtype < enemyChance[0])
                {
                    chosenType = 1;
                    total = total + enemyChance[0];
                    hasType = true;
                    /*print("Chosen type = 1");*/
                }
            }
        }
        if (hasType == false)
        {
            chosenType = 0;
        }
        /*print(total + " Chosen");*/
        return chosenType;
    }
    public void ChanceCalculator()
    {
        //Chance for every type of enemy to spawn , Math
        for (int enemy = 0; enemy < enemies.Length-1; enemy++)
        {
            if (wavesEnded < enemyStartsAt[enemy])
            {
                enemyChance[enemy] = 0;
            }
            else if (wavesEnded >= enemyChanceStopsAt[enemy])
            {
                enemyChance[enemy] = enemyMaxChance[enemy];
            }
            else
            {
                //Math

                int negativeWaves = enemyChanceStopsAt[enemy] - enemyStartsAt[enemy];
                 
                int roundsOverChance = wavesEnded - enemyStartsAt[enemy];
                 
                int chanceDifference = enemyMaxChance[enemy] - enemyMinChance[enemy];
                 
                int chanceUpPerWave = chanceDifference / (negativeWaves - 1);
                 
                enemyChance[enemy] = enemyMinChance[enemy] + (chanceUpPerWave * roundsOverChance);
            }
        }
    }
}
