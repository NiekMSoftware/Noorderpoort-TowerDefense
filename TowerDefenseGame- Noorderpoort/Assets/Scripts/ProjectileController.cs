using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Transform target;
    public float speed = 0.1f;
    float bulletTTL = 5;
    Vector3 targetDirection;
    // Start is called before the first frame update
    void Start()
    {
        targetDirection = target.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, -targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    // Update is called once per frame
    void Update()
    {
        bulletTTL -= Time.deltaTime;
        if (bulletTTL < 0)
        {
            Destroy(gameObject);
        }
        //Rotation , Movement
        gameObject.GetComponent<Rigidbody>().AddForce(targetDirection * speed, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
