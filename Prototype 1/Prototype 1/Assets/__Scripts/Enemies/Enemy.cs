using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	/* -----Variables----- */

	private Player _player;
	private int _damage;
	private Vector3 _direction;

	public float speed;

	// Read-only reference to the player
	protected Player player
	{
		get { return _player; }
	}

	// Amount of damage the enemy does to the player
	// Must be set in subclass
	protected int damage
	{
		get
		{
			if (_damage == 0)
				Debug.LogError("Damage value was never set for this enemy type");
			return _damage;
		}
		set { _damage = value; }
	}

	protected Vector3 direction
	{
		get { return _direction; }
		set { _direction = value; }
	}

	/* -----Unity methods----- */

	void Awake()
	{
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void FixedUpdate()
	{
		GetComponent<Rigidbody>().velocity = speed * _direction;
	}

	/* -----Custom methods----- */

	// Returns a normalized vector of a random direction
	// Direction will never be strictly up, down, left, or right
	protected Vector3 RandomNormalizedDirection()
	{
		Vector3 thisDir = new Vector3
		(
			RandomLimitedSingleDirection(),
			RandomLimitedSingleDirection(),
			0f
		);
		thisDir.Normalize();

		return thisDir;
	}

	// Returns a number between +/- 0.2 to +/- 0.8
	private float RandomLimitedSingleDirection()
	{
		float num = Random.Range(0.2f, 0.8f);
		if (Random.Range (0, 2) == 1)
			num *= -1f;
		return num;
	}
}
