using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{	
	[System.Serializable]
	private class State
	{
		public float currentSpeed;
		public float targetSpeed;
		public Vector2 amountToMove;
		public Vector2 position;
		public Vector3 rotation;
	}
	private Stack<State> states;
	[HideInInspector]

	// Prefabs
	public GameObject bulletPrefab;

	// Player Handling
	public bool facing_left = false;
	public float gravity = 20;
	public float speed = 8;
	public float acceleration = 30;
	public float jumpHeight = 12;

	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	private PlayerPhysics playerPhysics;

	// Id-related variables
	[HideInInspector]
	public int id = 0;
	private string horizontal;
	private string jump;
	private string reverse;
	private string fire;

	// Health and damage
	public int maxHealth = 5;
	public int health;
	private float fireTimer = 0f;
	public float fireTimerVal;

	void Start()
	{
		states = new Stack<State>();
		playerPhysics = GetComponent<PlayerPhysics>();

		horizontal = "Horizontal" + id;
		jump = "Jump" + id;
		reverse = "Reverse" + id;
		fire = "Fire" + id;

		health = maxHealth;
	}
	
	void Update()
	{
		if (TimeController.Rewind) return;

		if (fireTimer > 0f)
			fireTimer -= Time.deltaTime;

		// Reset acceleration upon collision
		if (playerPhysics.movementStopped)
		{
			targetSpeed = 0;
			currentSpeed = 0;
		}
		
		// If player is touching the ground
		if (playerPhysics.grounded)
		{
			amountToMove.y = 0;
			
			// Jump
			if (Input.GetButtonDown(jump))
			{
				amountToMove.y = jumpHeight;	
			}
		}
		
		// Input
		float moveDir = Input.GetAxisRaw(horizontal);
		targetSpeed = moveDir * speed;
		if (targetSpeed != 0f || !playerPhysics.grounded)
			currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
		else
			currentSpeed = 0f;
		
		// Set amount to move
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime);

		// Face Direction
		if (moveDir != 0)
		{
			transform.eulerAngles = (moveDir > 0) ? Vector3.up * 180 : Vector3.zero;
		}
		if (transform.eulerAngles == Vector3.zero)
			facing_left = true;
		else
			facing_left = false;

		if (Input.GetButtonDown(fire) && fireTimer <= 0)
		{
			fireTimer = fireTimerVal;
			Fire();
		}

		SaveState();
	}

	void LateUpdate()
	{
		if (Input.GetButton(reverse))
			TimeController.Rewind = true;
		if (Input.GetButtonUp(reverse))
			TimeController.Rewind = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Deadly")
			return;// damage
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

	private void Fire()
	{
		Vector3 pos = transform.position;
		pos.y += transform.lossyScale.y / 3f;
		if (facing_left)
			pos.x -= 1f;
		else
			pos.x += 1f;

		GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity) as GameObject;
		bullet.GetComponent<BulletObj>().SetDirection(facing_left);
	}

	private void SaveState()
	{
		State state = new State();
		state.currentSpeed = currentSpeed;
		state.targetSpeed = targetSpeed;
		state.amountToMove = amountToMove;
		state.position = transform.position;
		state.rotation = transform.eulerAngles;

		states.Push(state);
	}

	public void RevertState()
	{
		if (states.Count == 0) return;

		State state = states.Pop();
		currentSpeed = state.currentSpeed;
		targetSpeed = state.targetSpeed;
		amountToMove = state.amountToMove;
		transform.position = state.position;
		transform.eulerAngles = state.rotation;
	}
}