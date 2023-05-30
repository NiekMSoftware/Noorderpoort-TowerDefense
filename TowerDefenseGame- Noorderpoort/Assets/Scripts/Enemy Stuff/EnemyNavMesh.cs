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
    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //Look at where it is going
        if (transform.rotation != Quaternion.Euler(agent.velocity))
        {
            if (lookingAtMovement)
            {
                transform.rotation = Quaternion.LookRotation(agent.velocity);
            }
        }
        agent.destination = movePositionTransform.position;
    }
}
