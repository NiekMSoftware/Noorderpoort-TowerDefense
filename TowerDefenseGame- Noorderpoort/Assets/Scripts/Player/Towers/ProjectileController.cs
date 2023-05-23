using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Bullet Time To Live")]
    [SerializeField] private float bulletTTL = 5;
    [Header("Other")]
    public Transform target;
    [Space]
    public float speed = 0.1f;
    Vector3 targetDirection;

    void Start()
    {
        targetDirection = target.position - transform.position;
    }
    void Update()
    {
        //Bullet moves and rotates towards target
        if (target != null)
        {
            transform.rotation = Quaternion.LookRotation(gameObject.GetComponent<Rigidbody>().velocity);
            gameObject.GetComponent<Rigidbody>().AddForce(targetDirection * speed, ForceMode.Impulse);
            targetDirection = target.position - transform.position;
            float singleStep = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, -targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            //Bullet dies after existing too long
            bulletTTL -= Time.deltaTime;
            if (bulletTTL < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Bullet dies after hitting the floor or an enemy
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
