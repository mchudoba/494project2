using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour
{
	[HideInInspector]
	public Vector3 pos;

	public int id = 0;
	public bool initialSpawn = false;
	public int initialPlayerNumber = 0;

	void Awake()
	{
		if (id == 0)
			Debug.LogError("id not set for spawn point", this.gameObject);
		pos = transform.position;
	}
}
