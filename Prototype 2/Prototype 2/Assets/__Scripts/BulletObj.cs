using UnityEngine;
using System.Collections;

public class BulletObj : MonoBehaviour
{
	private float destroyTime = 5f;

	public float speed;

	void Start()
	{
		Destroy(gameObject, destroyTime);
	}

	void FixedUpdate()
	{
		Vector3 pos = transform.position;
		pos.x += speed * Time.fixedDeltaTime;
		transform.position = pos;
	}

	public void SetDirection(bool facing_left)
	{
		if (facing_left)
			speed *= -1f;
	}
}
