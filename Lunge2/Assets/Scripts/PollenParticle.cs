using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PollenParticle : Enemy {

	private int startHealth = 10;
	private int damage = 5;
	public float neighbourhood = 4.0f;
	private List<GameObject> pollenList;
	private PollenCollection pc;
	private Rigidbody rb;
	private Steering steer;
	//private Vector3 steeringForce;

	public float cohesionFactor = 1;
	public float separationFactor = 1;
	public float alignmentFactor = 1;

	public float vel = 5.0f;

	void Awake ()
	{
		//Set values from config
		//Pollen health, attack damage -> parent property, neighbourhood
	}
	// Use instantiating a pollen
	void Start () 
	{
		pc = GameObject.Find ("LeftPollenCollection").GetComponent<PollenCollection> ();
		pc.AddPollen (gameObject);
		this.AttackDamage = damage;
		this.Health = startHealth;

		rb = GetComponent<Rigidbody> ();
//		transform.forward = -transform.up;
		rb.velocity = vel*transform.forward;
		steer = new Steering (gameObject, vel);
	}
	
	void FixedUpdate()
	{
		transform.forward = rb.velocity.normalized;
		rb.AddForce (Flock ().normalized, ForceMode.VelocityChange);
		rb.velocity = rb.velocity.normalized * vel;
//		transform.forward = rb.velocity;
//		rb.velocity = transform.forward * vel;
	}

	Vector3 Flock()
	{
		Vector3 SteeringForce = new Vector3 ();
//		SteeringForce += steer.Seek (new Vector3 (0.0f, 1.5f, 0.0f));
		SteeringForce += steer.Separation (pc.pollenList, neighbourhood) * separationFactor;
		SteeringForce += steer.Alignment (pc.pollenList, neighbourhood) * alignmentFactor;
		SteeringForce += steer.Cohesion (pc.pollenList, neighbourhood) * cohesionFactor;
		return SteeringForce;
	}


	protected override void DeleteEnemy()
	{
		pc.DeletePollen (gameObject);
		Destroy (gameObject);
	}

	//Bounce off other objects
	void OnCollisionEnter(Collision collision) 
	{
		ContactPoint contact = collision.contacts [0];
		transform.forward = Vector3.Reflect (transform.forward, contact.normal);
		//rb.AddForce (vel * transform.forward, ForceMode.VelocityChange);
		rb.velocity = vel*transform.forward;
        if (collision.collider.CompareTag("WeakPoint"))
        {
            collision.collider.GetComponent<WeakPoint>().lm.DamageLung(AttackDamage);
        }
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(other.GetComponent<Bullet>().AttackDamage);
        }
    }
}
