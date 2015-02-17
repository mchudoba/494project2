using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
{
	[System.Serializable]
	private class State
	{
		public bool grounded;
		public bool movementStopped;
	}
	private Stack<State> states;
	[HideInInspector]
	public static bool reverseTime = false;

	public LayerMask collisionMask;

	private BoxCollider thisCollider;
	private Vector3 s;
	private Vector3 c;
	private Vector3 originalSize;
	private Vector3 originalCenter;
	private float colliderScale;
	private int collisionDivisionsX = 3;
	private int collisionDivisionsY = 10;
	private float skin = .005f;

	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool movementStopped;

	Ray ray;
	RaycastHit hit;
	
	void Start()
	{
		states = new Stack<State>();

		thisCollider = GetComponent<BoxCollider>();
		colliderScale = transform.localScale.x;
		
		originalSize = thisCollider.size;
		originalCenter = thisCollider.center;
		SetCollider(originalSize, originalCenter);
	}
	
	public void Move(Vector2 moveAmount)
	{
		if (reverseTime || TimeController.playerDead) return;

		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 p = transform.position;
		
		// Check collisions above and below
		grounded = false;
		
		for (int i = 0; i<collisionDivisionsX; i ++)
		{
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + c.x - s.x / 2) + s.x / (collisionDivisionsX - 1) * i; // Left, centre and then rightmost point of collider
			float y = p.y + c.y + s.y / 2 * dir; // Bottom of collider
			
			ray = new Ray(new Vector2(x, y), new Vector2(0, dir));
			Debug.DrawRay(ray.origin, ray.direction);

			if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin, collisionMask))
			{
				// Get Distance between player and ground
				float dst = Vector3.Distance(ray.origin, hit.point);
				
				// Stop player's downwards movement after coming within skin width of a collider
				if (dst > skin)
				{
					deltaY = dst * dir - skin * dir;
				}
				else
				{
					deltaY = 0;
				}
				grounded = true;
				if (hit.transform.gameObject.tag == "Deadly")
					TimeController.KillPlayer();
				break;
			}
		}

		// Check collisions left and right
		movementStopped = false;
		for (int i = 0; i<collisionDivisionsY; i ++)
		{
			float dir = Mathf.Sign(deltaX);
			float x = p.x + c.x + s.x / 2 * dir;
			float y = p.y + c.y - s.y / 2 + s.y / (collisionDivisionsY - 1) * i;
			
			ray = new Ray(new Vector2(x, y), new Vector2(dir, 0));
			Debug.DrawRay(ray.origin, ray.direction);

			if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + skin, collisionMask))
			{
				// Get Distance between player and ground
				float dst = Vector3.Distance(ray.origin, hit.point);
				
				// Stop player's downwards movement after coming within skin width of a collider
				if (dst > skin)
				{
					deltaX = dst * dir - skin * dir;
				}
				else
				{
					deltaX = 0;
				}
				movementStopped = true;
				if (hit.transform.gameObject.tag == "Deadly")
					TimeController.KillPlayer();
				break;
			}
		}
		
		if (!grounded && !movementStopped)
		{
			Vector3 playerDir = new Vector3(deltaX, deltaY);
			Vector3 o = new Vector3(p.x + c.x + s.x / 2 * Mathf.Sign(deltaX), p.y + c.y + s.y / 2 * Mathf.Sign(deltaY));
			ray = new Ray(o, playerDir.normalized);
			Debug.DrawRay(ray.origin, ray.direction);
			
			if (Physics.Raycast(ray, out hit, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), collisionMask))
			{
				grounded = true;
				deltaY = 0;
				if (hit.transform.gameObject.tag == "Deadly")
					TimeController.KillPlayer();
			}
		}
		
		Vector2 finalTransform = new Vector2(deltaX, deltaY);
		transform.Translate(finalTransform, Space.World);

		SaveState();
	}
	
	// Set collider
	public void SetCollider(Vector3 size, Vector3 centre)
	{
		thisCollider.size = size;
		thisCollider.center = centre;
		
		s = size * colliderScale;
		c = centre * colliderScale;
	}

	private void SaveState()
	{
		State state = new State();
		state.grounded = grounded;
		state.movementStopped = movementStopped;

		states.Push(state);
	}

	public void RevertState()
	{
		if (states.Count == 0) return;

		State state = states.Pop();
		grounded = state.grounded;
		movementStopped = state.movementStopped;
	}
}
