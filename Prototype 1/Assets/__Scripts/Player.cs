using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	/*
	 * Variables
	 */

	// Private variables
	private Vector3		movement = Vector3.zero;

	// Public variables
	public float		speed;

	/*
	 * Unity methods
	 */

	void Update()
	{
		Move();
	}

	void FixedUpdate()
	{
		rigidbody.velocity = movement * speed;
	}

	/*
	 * Custom methods
	 */

	// Checks input axes and sets velocity vector
	void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		movement = new Vector3(horizontal, vertical, 0f);
	}
}