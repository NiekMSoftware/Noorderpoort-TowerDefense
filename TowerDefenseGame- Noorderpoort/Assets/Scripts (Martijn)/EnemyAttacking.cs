using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyAttacking : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject Enemy;
    private float attack;
    private float damage;

    [Header("Tower Position")]
    public Transform TowerPositionTransform;

    [Header("Tower")]
    public GameObject Tower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void isAttacking()
    {
        //if (distance < minDistance)
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = TowerPositionTransform.position;
        
    }
}
