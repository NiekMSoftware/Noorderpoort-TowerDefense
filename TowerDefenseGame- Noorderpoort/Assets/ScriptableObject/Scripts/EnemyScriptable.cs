using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
[System.Serializable]
public class EnemyScriptable : ScriptableObject
{
    public GameObject prefab;
    public int health;
    public int speed;
    public int bitsOnDeath;

    [Header("Spawning Enemy")]
    public int startSpawnWave;
    public int endSpawnWave;
    public int minSpawnChance;
    public int maxSpawnChance;
}
