using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmokeCloud : Enemy
{
    private int startHealth = 20;
    private int damage = 0;
    private int partsPerCloud = 50;
    private int minParts = 10;
    private List<GameObject> smokeParticles;
    public GameObject spPrefab;
    private Steering steer;

    private Rigidbody rb;
    public float vel = 25.0f;

    private static GameObject[] weakPoints;
    private int targetWPIndex;
    private Vector3 targetWP;
    private static int numWP;
   // private static Random rnd = new Random();
	
    void Awake ()
    {
        //Set values from config
    }

    //When smoke cloud is instantiated
	void Start ()
    {
        this.Health = startHealth;
        this.AttackDamage = damage;

        if (weakPoints == null) //if weakpoints uninitialized
        {
            weakPoints = GameObject.FindGameObjectsWithTag("WeakPoint");
            numWP = weakPoints.Length;
        }
        targetWPIndex = UnityEngine.Random.Range(0, numWP);  //choose random weak point to target first
        targetWP = UpdateWeakPointPosition();

        steer = new Steering(gameObject, vel);  //steering behaviour object
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * vel;

        GameObject sp;
        smokeParticles = new List<GameObject>();    //List for attached particles
        for (int i = 0; i < partsPerCloud; i++)     //instantiate required number of particles
        {
            sp = (GameObject)Instantiate(spPrefab, transform.position, transform.rotation);
            sp.transform.parent = transform;        //set as child of smoke cloud
            sp.GetComponent<SmokeParticle>().cloud = smokeParticles;
            smokeParticles.Add(sp);
        }
	}

    void FixedUpdate ()
    {  
        //If close to weak point, begin moving to next wp
	    if(Vector3.Distance(transform.position, targetWP) < 1.0)
        {
            targetWPIndex = (targetWPIndex + 1) % numWP;
            targetWP = UpdateWeakPointPosition();
        }

        // transform.forward = rb.velocity.normalized;
        rb.AddForce(steer.Seek(targetWP), ForceMode.VelocityChange);
        //rb.AddForce(steer.Wander(), ForceMode.VelocityChange);
        rb.velocity = rb.velocity.normalized * vel;
	}

    protected override void DeleteEnemy()
    {
        if (smokeParticles.Count > 0)
        {
            foreach (GameObject sp in smokeParticles)
                Destroy(sp);
            smokeParticles.Clear();
        }
        Destroy(gameObject);
    }

    protected override void TakeDamage(int damage)
    {
        int deadParticles = damage * partsPerCloud / startHealth;
        GameObject sp;
        for (int i = 0; i < deadParticles && smokeParticles.Count > minParts; i++)    //require at least 10 particles so cloud is visible
        {
            sp = smokeParticles[0];
            smokeParticles.RemoveAt(0);
            Destroy(sp);
        }
        base.TakeDamage(damage);
    }

    private Vector3 UpdateWeakPointPosition()
    {
        GameObject wp = weakPoints[targetWPIndex];
        return new Vector3(wp.transform.position.x, transform.position.y, wp.transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(other.GetComponent<Bullet>().AttackDamage);
        }
    }
}
