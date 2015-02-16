using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	private Vector3 direction;
	private List<Vector3> velList;
	private bool forwardTime = true;
	private bool jumping = false;

	public float speed;
	public float maxHorizontalSpeed;
	public float jumpSpeed;

	void Start()
	{
		velList = new List<Vector3>();
	}

	void Update()
	{
		// Time control
		if (Input.GetKey(KeyCode.LeftShift) && velList.Count > 0)
		{
			forwardTime = false;
			int lastIndex = velList.Count - 1;
			Vector3 temp = velList[lastIndex];
			temp.x *= -1f;
			temp.y *= -1f;
			velList.RemoveAt(lastIndex);
			rigidbody.velocity = temp;
		}
		else
		{
			forwardTime = true;
			Move();
		}

		if (Input.GetAxis("Jump") != 0 && Mathf.Abs(rigidbody.velocity.y) <= 0.1f)
			jumping = true;
	}

	void FixedUpdate()
	{
		if (forwardTime)
		{
			if (jumping)
			{
				jumping = false;
				Vector3 temp = rigidbody.velocity;
				temp.y = jumpSpeed;
				rigidbody.velocity = temp;
			}

			//rigidbody.velocity = speed * direction;
			if (Mathf.Abs(rigidbody.velocity.x) <= maxHorizontalSpeed)
				rigidbody.AddForce(direction * speed);
			if (direction.x == 0f)
			{
				Vector3 temp = rigidbody.velocity;
				temp.x = 0f;
				rigidbody.velocity = temp;
			}
			velList.Add(rigidbody.velocity);
		}
	}

	void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		direction = new Vector3(horizontal, 0f, 0f);
	}
}
