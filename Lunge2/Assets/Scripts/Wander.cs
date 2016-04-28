using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//wandering steering behaviour
public class Wander
{
    private GameObject agent;
    private Rigidbody agentRB;
    private float speed;

    float wanderRadius = 15f;
    float wanderDistance = 15f;
    float wanderJitter = 20f;

    public Wander(GameObject pollution, float maxSpeed)
    {
        agent = pollution;
        agentRB = pollution.GetComponent<Rigidbody>();
        speed = maxSpeed;
    }
    public Vector3 Seek(Vector3 target)
    {
        //small random displacement of target
        float x = target.x * Random.Range(1,wanderJitter);
        float y = target.y * Random.Range(1, wanderJitter);
        float z = target.z * Random.Range(1, wanderJitter);
        //displaced vec 3
        Vector3 displacedTarget = new Vector3(x,y,z);
        //target projected back onto circle
        Vector3 projectedTarget = Vector3.Normalize(displacedTarget);

        projectedTarget *= wanderRadius;

        Debug.Log("wandering...");

        return projectedTarget;

    }


   
}