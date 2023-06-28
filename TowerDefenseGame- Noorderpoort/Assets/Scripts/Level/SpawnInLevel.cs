using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInLevel : MonoBehaviour
{
    [SerializeField] private Transform locationLevel1;
    [SerializeField] private GameObject level1;
    
    [SerializeField] private Transform locationLevel2;
    [SerializeField] private GameObject level2;

    public void SpawnLevel1()
    {
        Instantiate(level1, locationLevel1);
    }

    public void SpawnLevel2() {
        Instantiate(this.level2, this.locationLevel2);
    }
}
