using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLevel : MonoBehaviour
{
    [SerializeField] private GameObject level1;
    [SerializeField] private GameObject level2;

    public void DestroyLevel1()
    {
        Destroy(level1);
    }

    public void DestroyLevel2()
    {
        Destroy(level2);
    }
}
