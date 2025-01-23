using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInLevel : MonoBehaviour
{
    //Self explanatory
    [SerializeField] private Transform[] locationLevels;
    [SerializeField] private GameObject[] levels;

    public void SpawnLevel(int level)
    {
        Instantiate(levels[level],locationLevels[level]);
    }
}
