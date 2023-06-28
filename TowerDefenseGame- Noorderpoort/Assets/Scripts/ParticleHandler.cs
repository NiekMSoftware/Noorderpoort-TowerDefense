using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleHandler : MonoBehaviour
{
    public GameObject[] Particles = GameObject.FindGameObjectsWithTag("ElectricityParticle");
   
    // Start is called before the first frame update
    void Start()
    { 
     while (true)
        {
            int randomNumber = Random.Range(0, Particles.Length);
            ParticleSystem ps = Particles[randomNumber].GetComponent<ParticleSystem>();
            ps.Play();
        }
    }
}