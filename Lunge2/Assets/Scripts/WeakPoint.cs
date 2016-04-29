using UnityEngine;
using System.Collections;

public class WeakPoint : MonoBehaviour {

    public LungManager lm;

	// Use this for initialization
	void Start () {
        lm = GameObject.Find("LungArena").GetComponent<LungManager>();
	}
	

    //void OnColliderEnter(Collision col)
    //{
    //    Debug.Log("hit");
    //    if(col.collider.CompareTag("Enemy"))
    //    {
    //        lm.DamageLung(col.collider.GetComponent<Enemy>().AttackDamage);
    //    }
    //}
}
