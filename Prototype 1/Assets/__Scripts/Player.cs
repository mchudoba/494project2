using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour
{
	public float		speed;
	public Boundary		boundary;

	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(horizontal, vertical, 0f);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3
		(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax),
			0f
		);
	}
}