using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
	private List<PlayerSpawn> playerSpawnPoints;

	public GameObject playerPrefab;
	public static int numPlayers = 2;

	void Awake()
	{
		playerSpawnPoints = new List<PlayerSpawn>();
		GameObject[] temp = GameObject.FindGameObjectsWithTag("PlayerSpawn");
		foreach (GameObject spawn in temp)
			playerSpawnPoints.Add(spawn.GetComponent<PlayerSpawn>());

		InitialSpawn();
	}

	void Update()
	{
		if (Input.GetButton("Cancel"))
			Application.LoadLevel("Menu_Scene");
	}

	void InitialSpawn()
	{
		int index1 = -1;
		int index2 = -1;
		for (int i = 0; i < playerSpawnPoints.Count; i++)
		{
			PlayerSpawn temp = playerSpawnPoints[i];
			if (temp.initialSpawn)
			{
				if (temp.initialPlayerNumber == 1) index1 = i;
				else if (temp.initialPlayerNumber == 2) index2 = i;
			}
		}
		if (index1 == -1 || index2 == -1 || index1 == index2)
			Debug.LogError("Incorrect initial player spawn setup");

		GameObject player1 = Instantiate(playerPrefab, playerSpawnPoints[index1].pos, Quaternion.identity) as GameObject;
		player1.GetComponent<PlayerController>().id = 1;
		player1.name = "Player1";
		GameObject player2 = Instantiate(playerPrefab, playerSpawnPoints[index2].pos, Quaternion.identity) as GameObject;
		player2.GetComponent<PlayerController>().id = 2;
		player2.name = "Player2";
		
		player2.GetComponentInChildren<Renderer>().material.color = Color.red;
	}

	public Vector3 RandomSpawnLocation()
	{
		int rand = Random.Range(0, playerSpawnPoints.Count);
		return playerSpawnPoints[rand].transform.position;
	}

	public void GameOver()
	{
		Application.LoadLevel("Menu_Scene");
	}
}