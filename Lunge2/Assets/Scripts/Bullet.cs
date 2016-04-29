using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public int AttackDamage = 10;
    private float bulletSpeed = 60.0f;
	// Use this for initialization
	void Start () {
        //instantiate with set velocity
        GetComponent<Rigidbody>().velocity = transform.up * bulletSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        Destroy(gameObject);
    }
}
