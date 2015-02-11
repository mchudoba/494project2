using UnityEngine;
using System.Collections;

public class RedEnemy : Enemy
{
	/* -----Variables----- */

	private int thisDamage = 1;
	
	/* -----Unity methods----- */

	void Start()
	{
		damage = thisDamage;
		direction = RandomNormalizedDirection();
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
}