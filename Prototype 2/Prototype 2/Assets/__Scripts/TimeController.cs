using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeController : MonoBehaviour
{
	// Camera variables
	private Camera cam;
	private Color cameraNormalColor;
	private Color cameraReverseColor;
	private float duration = 0.25f;
	private float colorLerp = 0f;

	// Player variables
	private class Player
	{
		public Renderer render;
		public Color normalColor;
		public Color deadColor;
		public PlayerController controller;
		public PlayerPhysics physics;
	}
	private List<Player> players;

	[HideInInspector]
	public static bool Rewind = false;
	private int numPlayers;

	void Start()
	{
		cam = gameObject.GetComponent<Camera>();
		cameraNormalColor = cam.backgroundColor;
		cameraReverseColor = new Color(0.5f, 0.5f, 0.5f, 0f);

		PlayerSetup();
	}

	void LateUpdate()
	{
		if (Rewind)
		{
			ReverseTime();
		}
		else if (colorLerp > 0f)
		{
			cam.backgroundColor = Color.Lerp(cameraNormalColor, cameraReverseColor, colorLerp);
			colorLerp -= Time.deltaTime / duration;
		}
	}

	void PlayerSetup()
	{
		numPlayers = GameController.numPlayers;
		players = new List<Player>();
		for (int i = 1; i <= numPlayers; i++)
		{
			Player curPlayer = new Player();
			GameObject playerObj = GameObject.Find("Player" + i).gameObject;
			curPlayer.render = playerObj.GetComponentInChildren<Renderer>();
			curPlayer.normalColor = curPlayer.render.material.color;
			curPlayer.deadColor = Color.red;
			curPlayer.controller = playerObj.GetComponent<PlayerController>();
			curPlayer.physics = playerObj.GetComponent<PlayerPhysics>();

			players.Add(curPlayer);
		}
	}

	void ReverseTime()
	{
		cam.backgroundColor = Color.Lerp(cameraNormalColor, cameraReverseColor, colorLerp);
		if (colorLerp < 1f)
			colorLerp += Time.deltaTime / duration;

		for (int i = 0; i < players.Count; i++)
		{
			players[i].controller.RevertState();
			players[i].physics.RevertState();
		}
	}
}