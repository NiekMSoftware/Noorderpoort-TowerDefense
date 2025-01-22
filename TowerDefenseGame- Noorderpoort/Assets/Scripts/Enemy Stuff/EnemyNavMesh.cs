using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [Header("Movement")]
    public Transform movePositionTransform;
    private NavMeshAgent agent;
    public bool lookingAtMovement = true;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        agent.destination = movePositionTransform.position;
    }
    void Update()
    {
        //Only rotate if it needs to rotate
        if (transform.rotation != Quaternion.Euler(agent.velocity))
        {
            if (lookingAtMovement)
            {
                transform.rotation = Quaternion.LookRotation(agent.velocity);
            }
        }
    }
}
