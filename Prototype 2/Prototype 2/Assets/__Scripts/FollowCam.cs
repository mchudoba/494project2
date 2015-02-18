using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour
{
	private GameObject player;
	private PlayerController controller;

	void Start()
	{
		player = GameObject.Find("Player1");
		controller = player.GetComponent<PlayerController>();
		controller.rewindTime = 100f;
		controller.maxLives = 100;
		controller.maxHealth = 100;
	}

	void Update()
	{
		float x = Mathf.Clamp(player.transform.position.x, 0f, 14.5f);
		Vector3 pos = transform.position;
		pos.x = x;
		transform.position = pos;

		controller.rewindTimeLeft = 100f;
	}
}
