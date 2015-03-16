using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	/* -----Variables----- */

	private const string 		menuScene = "Menu_Scene";
	private Vector3		movement = Vector3.zero;
	private bool		waiting = true;

	public float		speed;
	public int			lives;

	public bool			Waiting
	{
		get { return waiting; }
	}

	/* -----Unity methods----- */

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.LoadLevel(menuScene);

		Move();
	}

	void FixedUpdate()
	{
		GetComponent<Rigidbody>().velocity = movement * speed;
	}

	/* -----Custom methods----- */

	// Returns true if successful
	// Should not take damage if damage taken recently
	public bool TakeDamage(int damage)
	{
		if (lives - damage < 0)
			lives = 0;
		else
			lives -= damage;
		return true;
	}

	// Checks input axes and sets velocity vector
	void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		movement = new Vector3(horizontal, vertical, 0f);

		if (waiting && (horizontal != 0f || vertical != 0f))
		{
			waiting = false;
			GameController temp = GameObject.Find("_Main Camera").GetComponent<GameController>();
			temp.Play();
		}
	}
}