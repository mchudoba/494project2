using UnityEngine;
using System.Collections;

public class GreenEnemy : Enemy
{
	/* -----Variables----- */

	private int thisDamage = 2;
	public float despawnTimer;

	/* -----Unity methods----- */

	void Start()
	{
		damage = thisDamage;
		direction = HomingDirection();
		Destroy(this.gameObject, despawnTimer);
	}

	void FixedUpdate()
	{
		direction = HomingDirection();
		GetComponent<Rigidbody>().velocity = speed * direction;
	}

	void OnCollisionEnter(Collision other)
	{
		// Perfectly elastic bounce on a wall
		if (other.gameObject.tag == "Wall")
			direction = Vector3.Reflect(direction, other.contacts[0].normal);
		
		// Deal damage to the player and destroy this game object
		if (other.gameObject.tag == "Player")
		{
			if (player.TakeDamage(damage))
				Destroy(this.gameObject);
		}
	}

	/* -----Custom methods----- */

	Vector3 HomingDirection()
	{
		Vector3 dir = player.transform.position - transform.position;
		dir.Normalize();
		return dir;
	}
}
