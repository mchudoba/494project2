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
	public static bool reverseTime = false;
	[HideInInspector]

	// Player Handling
	public float gravity = 20;
	public float speed = 8;
	public float acceleration = 30;
	public float jumpHeight = 12;

	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	private PlayerPhysics playerPhysics;

	void Start()
	{
		states = new Stack<State>();
		playerPhysics = GetComponent<PlayerPhysics>();
	}
	
	void Update()
	{
		if (reverseTime || TimeController.playerDead) return;
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
			if (Input.GetButtonDown("Jump"))
			{
				amountToMove.y = jumpHeight;	
			}
		}
		
		// Input
		targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
		if (targetSpeed != 0f || !playerPhysics.grounded)
			currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
		else
			currentSpeed = 0f;
		
		// Set amount to move
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime);

		// Face Direction
		float moveDir = Input.GetAxisRaw("Horizontal");
		if (moveDir != 0)
		{
			transform.eulerAngles = (moveDir > 0) ? Vector3.up * 180 : Vector3.zero;
		}

		SaveState();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Deadly")
			TimeController.KillPlayer();
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