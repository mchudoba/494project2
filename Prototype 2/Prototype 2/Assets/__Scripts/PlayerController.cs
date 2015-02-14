using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	private Vector3 direction;
	private List<Vector3> velList;
	private bool forwardTime = true;

	public float speed;

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
	}

	void FixedUpdate()
	{
		if (forwardTime)
		{
			rigidbody.velocity = speed * direction;
			velList.Add(rigidbody.velocity);
		}
	}

	void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		direction = new Vector3(horizontal, 0f, 0f);
	}
}
