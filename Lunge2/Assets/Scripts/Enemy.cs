using UnityEngine;
using System.Collections;

//Will be abstract
public abstract class Enemy : MonoBehaviour {
    //set type of enemy
    public string Type { get; protected set; }
	//Property storing damage done on collision with weak point
	public int AttackDamage{ get; protected set; }
	//Property for enemy object's health
	public int Health{ get; protected set; }

	protected void TakeDamage(int damage)
	{
		this.Health -= damage;
		if (this.Health <= 0)
			DeleteEnemy ();
	}

	protected abstract void DeleteEnemy ();
}
