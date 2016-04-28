using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pollution : Enemy
{

  
    private int startHealth = 10;
    private int damage = 5;

    private Rigidbody rb;
    private Wander wander;
    public float vel = 5.0f;
    public List<GameObject> pollutionList;
    private PollutionCollection pCol;
    // Object pollutionPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefab/Pollution.prefab", typeof(GameObject));

   void start ()
    {
        pCol = GameObject.Find("LeftEnemyCollection").GetComponent<PollutionCollection>();
        pCol.AddPollution(this.gameObject);
        // GameObject pol = Instantiate(Resources.Load("Assets/prefab/Pollution.prefab")) as GameObject;
        this.AttackDamage = damage;
        this.Health = startHealth;
        this.rb = GetComponent<Rigidbody>();
        //transform.forward = -transform.up;
        this.rb.velocity = vel * transform.forward;
        this.wander = new Wander(gameObject, vel);
    }
    //empty will remove
    public override void addEnemy(Vector3 pos)
    {
        
    }
    void Update()
    {

    }

void FixedUpdate()
    {
        transform.forward = rb.velocity.normalized;
        rb.AddForce(wander.Seek(transform.forward));
        rb.velocity = rb.velocity.normalized * vel;
    }

    protected override void DeleteEnemy()
    {
        Destroy(gameObject);
    }
    //Bounce off other objects
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        transform.forward = Vector3.Reflect(transform.forward, contact.normal);
        //rb.AddForce (vel * transform.forward, ForceMode.VelocityChange);
       // rb.velocity = vel * transform.forward;
    }
}
