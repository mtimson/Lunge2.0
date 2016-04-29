using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    private Rigidbody rb;
    public float vel = 15.0f;
    public float turn = 3.0f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        rb.AddForce(transform.forward * v * vel, ForceMode.VelocityChange);
        rb.AddTorque(transform.up * turn * h, ForceMode.VelocityChange);
        rb.velocity = rb.velocity.normalized * vel;
        rb.angularVelocity = rb.angularVelocity.normalized * turn;
        if (v == 0.0f)
            rb.velocity = Vector3.zero;
        if (h == 0.0f)
            rb.angularVelocity = Vector3.zero;
	}
}
