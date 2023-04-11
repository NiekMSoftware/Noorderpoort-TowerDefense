using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Transform target;
    public float speed = 0.1f;
    public float bulletTTL = 5;
    Vector3 targetDirection;
    Vector3 targetStartingPosition;
    public bool homing = false;
    [SerializeField] private bool normal = true;
    [SerializeField] private bool goesUp = false;
    public float timeTillDown;
    // Start is called before the first frame update
    void Start()
    {
        if (goesUp)
        {
            timeTillDown = 2f;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(targetDirection.x, 3500, targetDirection.z) * speed, ForceMode.Impulse);
            targetStartingPosition = target.position;
        }
        targetDirection = target.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, -targetDirection, singleStep, 0.0f);
        //transform.rotation = Quaternion.LookRotation(newDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (normal)
        {
            transform.rotation = Quaternion.LookRotation(gameObject.GetComponent<Rigidbody>().velocity);
            gameObject.GetComponent<Rigidbody>().AddForce(targetDirection * speed, ForceMode.Impulse);
            if (homing == true)
            {
                targetDirection = target.position - transform.position;
                float singleStep = speed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, -targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        if (goesUp == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, .1f);
            transform.rotation = Quaternion.LookRotation(gameObject.GetComponent<Rigidbody>().velocity);
            timeTillDown -= Time.deltaTime;
        }
        bulletTTL -= Time.deltaTime;
        if (bulletTTL < 0)
        {
            Destroy(gameObject);
        }
        //Rotation , Movement
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
