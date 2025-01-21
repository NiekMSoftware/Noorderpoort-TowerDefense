using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionScript : MonoBehaviour
{
    //Public event script for if you need collision on a specific object for a script on a different object.
    public UnityEvent<Collision> onCollisionEnter;
    public UnityEvent<Collision> onCollisionExit;
    public UnityEvent<Collider> onTriggerEnter;
    public UnityEvent<Collider> onTriggerExit;

    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        onCollisionExit.Invoke(collision);
    }
    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke(other);
    }
}
