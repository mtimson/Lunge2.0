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
     public override void addEnemy(Vector3 pos)
    {

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
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("count in keypress" + pc.GetPollenList().Count);
			Debug.Log(rb.velocity.magnitude);
		}
		if(Input.GetKeyDown(KeyCode.A))
		{
			Debug.Log(rb.velocity.magnitude);
		}
		//pollenList
		//Debug.Log (rb.velocity);
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

//	Vector3 ForceFromFlocking ()
//	{
//		Vector3 SteeringForce = new Vector3 ();
//		Vector3 CenterMass = new Vector3 ();
//		Vector3 AvgHeading = new Vector3 ();
//		int partCount = 0;
//
//		foreach (GameObject particle in pc.pollenList) 
//		{
//			if(particle != gameObject)
//			{
//				Vector3 toOther = transform.position - particle.transform.position;
//				SteeringForce += toOther.normalized / toOther.magnitude;
//				CenterMass += particle.transform.position;
//				AvgHeading += particle.transform.forward;
//				partCount++;
//			}
//		}
//		//Debug.Log ("partCount" + partCount);
//		if(partCount > 0)
//		{
//			AvgHeading /= partCount;
//			CenterMass /= partCount;
//		}
//		SteeringForce = SteeringForce.normalized * separationFactor;
//		SteeringForce += AvgHeading * alignmentFactor;
//		SteeringForce += Vector3.Normalize(CenterMass - transform.position) * cohesionFactor;
//
//		return SteeringForce.normalized;
//	}


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
	}
}
