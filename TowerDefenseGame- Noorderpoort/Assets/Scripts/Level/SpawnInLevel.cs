using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInLevel : MonoBehaviour
{
    [SerializeField] private Transform locationLevel1;
    [SerializeField] private GameObject level1;

    public void SpawnLevel1()
    {
        Instantiate(level1, locationLevel1);
    }
}
