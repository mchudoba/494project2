using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour
{
	private Vector3		movement = Vector3.zero;

	public float		speed;
	public Boundary		boundary;

	void Update()
	{
		Move();
	}

	void FixedUpdate()
	{
		rigidbody.velocity = movement * speed;
		
		rigidbody.position = new Vector3
		(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax),
			0f
		);
	}

	// Checks input axes and sets velocity vector
	void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		movement = new Vector3(horizontal, vertical, 0f);
	}
}