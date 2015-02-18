using UnityEngine;
using System.Collections;

public class BulletObj : MonoBehaviour
{
	private float destroyTime = 5f;

	public float speed;
	public int damage;

	void Start()
	{
		Destroy(gameObject, destroyTime);
	}

	void Update()
	{
		float curSpeed = speed;
		if (TimeController.Rewind) curSpeed *= 0.1f;

		Vector3 pos = transform.position;
		pos.x += curSpeed * Time.deltaTime;
		transform.position = pos;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 0)
		{
			Destroy(gameObject, 0.01f);
			return;
		}
		if (other.tag == "Player")
		{
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			player.DealDamage(damage);

			Destroy(gameObject, 0.01f);
			return;
		}
	}

	public void SetDirection(bool facing_left)
	{
		if (facing_left)
			speed *= -1f;
	}
}
