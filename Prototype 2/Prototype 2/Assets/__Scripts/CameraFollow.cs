using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
	private Transform player;

	public float trackSpeed = 10f;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	
	// Set target
	public void Start()
	{
		player = GameObject.Find("Player").transform;
	}
	
	// Track target
	void LateUpdate()
	{
		float x = IncrementTowards(transform.position.x, player.position.x, trackSpeed);
		float y = IncrementTowards(transform.position.y, player.position.y, trackSpeed);

		// Restrict camera to bounds
		if (x < minX) x = minX;
		if (x > maxX) x = maxX;
		if (y < minY) y = minY;
		if (y > maxY) y = maxY;

		transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}
	
	// Increase n towards target by speed
	private float IncrementTowards(float n, float target, float a)
	{
		if (n == target)
		{
			return n;	
		}
		else
		{
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target - n)) ? n : target; // if n has now passed target then return target, otherwise return n
		}
	}
}
