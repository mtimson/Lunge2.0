using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pollution : Enemy
{

  
    private int startHealth = 10;
    private int damage = 5;

    private Rigidbody rb;
    private Steering wander;
    public float vel = 15.0f;
    public List<GameObject> pollutionList;
    // Object pollutionPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefab/Pollution.prefab", typeof(GameObject));
   
   void Start ()
    {
    
        
  
        this.AttackDamage = damage;
        this.Health = startHealth;
        this.rb = GetComponent<Rigidbody>();
        //transform.forward = -transform.up;
        this.rb.velocity = vel * transform.forward;
        this.wander = new Steering(gameObject, vel);
        
    }
  
    void Update()
    {

    }

void FixedUpdate()
    {
        transform.forward = rb.velocity.normalized;
        rb.AddForce(wander.Wander());
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
