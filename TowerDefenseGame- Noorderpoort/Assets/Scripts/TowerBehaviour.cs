using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public void SetUpgrade1()
    {
        transform.GetChild(0).gameObject.SetActive((false));
        transform.GetChild(1).gameObject.SetActive((true));
    }
    
    public void SetUpgrade2()
    {
        transform.GetChild(1).gameObject.SetActive((false));
        transform.GetChild(2).gameObject.SetActive((true));
    }
}
