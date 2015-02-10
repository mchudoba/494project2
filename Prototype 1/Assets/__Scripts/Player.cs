using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	/* -----Variables----- */

	private Vector3		movement = Vector3.zero;

	public float		speed;
	public int			lives;

	/* -----Unity methods----- */

	void Update()
	{
		Move();
	}

	void FixedUpdate()
	{
		rigidbody.velocity = movement * speed;
	}

	/* -----Custom methods----- */

	// Returns true if successful
	// Should not take damage if damage taken recently
	public bool TakeDamage(int damage)
	{
		if (lives - damage < 0)
			lives = 0;
		else
			lives -= damage;
		return true;
	}

	// Checks input axes and sets velocity vector
	void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		movement = new Vector3(horizontal, vertical, 0f);
	}
}