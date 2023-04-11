using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{

    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent agent;
    public bool lookingAtMovement = true;
    [Header("Testing")]
    public float deathTimer = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation != Quaternion.Euler(agent.velocity))
        {
            if (lookingAtMovement)
            {
                transform.rotation = Quaternion.LookRotation(agent.velocity);
            }
        }
        agent.destination = movePositionTransform.position;
        deathTimer -= Time.deltaTime;
        if (deathTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
