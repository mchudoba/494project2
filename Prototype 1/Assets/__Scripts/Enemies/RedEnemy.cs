using UnityEngine;
using System.Collections;

public class RedEnemy : MonoBehaviour
{
	/*
	 * Variables
	 */

	// Private variables
	private Vector3			movement;
	private Player			player;
	private int				damage = 1;

	// Public variables
	public float			speed;

	/*
	 * Unity methods
	 */

	void Start()
	{
		movement = new Vector3
		(
			Random.Range(-1f, 1f),
			Random.Range(-1f, 1f),
			0f
		);
		movement.Normalize();

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void FixedUpdate()
	{
		rigidbody.velocity = movement * speed;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Wall")
			movement = Vector3.Reflect(movement, other.contacts[0].normal);

		if (other.gameObject.tag == "Player")
			player.TakeDamage(damage);
	}
}