using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	/* -----Variables----- */

	private Player player;
	private const string gameScene = "_Scene_0";

	/* -----Unity methods----- */

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void Update()
	{
		if (player.lives <= 0)
			Application.LoadLevel(gameScene);
	}
}
