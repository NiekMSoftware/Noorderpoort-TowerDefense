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
    bool changed = false;
    [SerializeField] private GameObject hitParticle; 

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
            //transform.rotation = Quaternion.LookRotation(target.position);
            gameObject.GetComponent<Rigidbody>().AddForce(targetDirection * speed, ForceMode.Impulse);
        }
        else
        {
            if(changed == false)
            {
                bulletTTL = 0.5f;
                changed = true;
            }
        }
        //Bullet dies after existing too long
        bulletTTL -= Time.deltaTime;
        if (bulletTTL < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Bullet dies after hitting the floor or an enemy
        if (collision.collider.tag == "Enemy")
        {
            Destroy(gameObject);
            Instantiate(hitParticle, transform.position, transform.rotation);
        } else if (collision.collider.tag == "Floor")
        {
            //Destroy(gameObject);
        }
    }
}
