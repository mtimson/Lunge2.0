using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SmokeParticle : Enemy {

    public List<GameObject> cloud;
    private int damage = 1;
    private Rigidbody rb;
    private Steering steer;

    public float cohesionFactor = 10.0f;
    public float alignmentFactor = 1.0f;
    public float separationFactor = 1.0f;

    public float vel = 50.0f;

	void Awake()
    {
        //initialize from config here
    }

	void Start ()
    {
        this.AttackDamage = damage;

        rb = GetComponent<Rigidbody>();
        transform.forward = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        rb.velocity = transform.forward * vel;
        steer = new Steering(gameObject, vel);
	}
	
	void FixedUpdate ()
    {
        transform.forward = rb.velocity.normalized;
        rb.AddForce(Flock(), ForceMode.VelocityChange);
        rb.velocity = rb.velocity.normalized * vel;
    }

    //For smoke particles, a modification on the standard flocking behaviour is
    //used so that all particles follow the cloud position, which is the parent object
    Vector3 Flock()
    {
        Vector3 SteeringForce = new Vector3();
        SteeringForce += steer.Seek (transform.parent.position) * cohesionFactor;
        SteeringForce += steer.Separation(cloud) * separationFactor;
        if (float.IsNaN(SteeringForce.y))
            Debug.Log("sep");
        SteeringForce += steer.Alignment(cloud) * alignmentFactor;
        //SteeringForce += steer.Cohesion(cloud) * cohesionFactor;
        return SteeringForce;
    }

    //Bounce off other objects
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        transform.forward = Vector3.Reflect(transform.forward, contact.normal);
        //rb.AddForce (vel * transform.forward, ForceMode.VelocityChange);
        rb.velocity = vel * transform.forward;
    }

    //particles do not take damage
    protected override void TakeDamage(int damage)
    {
        //do nothing
    }

    protected override void DeleteEnemy()
    {
        Destroy(gameObject);
    }
}
