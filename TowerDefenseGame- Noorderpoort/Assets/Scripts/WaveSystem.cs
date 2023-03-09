using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    private int gameRound = 0;
    [SerializeField] private GameObject normalEnemy;
    [SerializeField] private int[] normalsPerRound;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnCooldown = 2;
    [SerializeField] private float roundCooldown = 2;
    public float timeTillSpawn = 0;
    bool spawning = false;
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
        if (spawning)
        {
            
        }
    }
    public void roundStart()
    {
        timeTillSpawn = 0;
        int spawnedEnemies = 0;
        while (spawnedEnemies < normalsPerRound.Length)
        {
            spawning = true;
            timeTillSpawn -= Time.deltaTime;
            if (timeTillSpawn < 0)
            {
                Instantiate(normalEnemy, spawnPoint.position, spawnPoint.rotation);
                timeTillSpawn = 2f;
                spawnedEnemies++;
                Debug.Log("Spawned " + spawnedEnemies);
            }
        }
/*            spawnedEnemies++
*//*        for (int spawnedEnemies = 0; spawnedEnemies < normalsPerRound.Length)
        {

        }*/
        spawning = false;
    }
    public void roundEnd()
    {
        gameRound++;
        roundStart();
    }
}
