using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    private float time;
    public GameObject barrel;
    public GameObject bullet;

    void Start()
    {
        time = 0.0f;
    }
	void Update ()
    {
        time += Time.deltaTime;
        if (Input.GetButtonDown("Jump") && time > 0.5f) //only allow 2 bullets/second
        {
            GameObject b = (GameObject)Instantiate(bullet, barrel.transform.position, Quaternion.identity);
            b.transform.up = barrel.transform.forward;
            time = 0.0f;
        }
	}
}
