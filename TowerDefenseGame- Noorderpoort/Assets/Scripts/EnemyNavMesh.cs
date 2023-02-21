using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    //[SerializeField] Transform[] checkpoints;
    public List<Transform> checkpoints = new List<Transform>();
    public List<Transform> takenCheckpoints = new List<Transform>();
    [SerializeField] private Transform movePositionTransform;
    public int checkpointamount;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        checkpoints.AddRange(GameObject.Find("Checkpoints").GetComponentsInChildren<Transform>());
        checkpoints.Remove(GameObject.Find("Checkpoints").transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(agent.velocity);
        agent.destination = checkpoints[checkpointamount].position;
        //agent.destination = movePositionTransform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint" && !takenCheckpoints.Contains(other.gameObject.transform) && other.gameObject.transform == checkpoints[checkpointamount])
        {
            takenCheckpoints.Add(checkpoints[checkpointamount]);
            checkpointamount++;
            if (checkpointamount == checkpoints.Count)
            {
                takenCheckpoints.Clear();
                checkpointamount = 0;
            }
        }
    }
}
