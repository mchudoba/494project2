using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour
{
	private Camera cam;
	private Color normalColor;
	private Color reverseColor;

	private PlayerController playerController;
	private PlayerPhysics playerPhysics;

	void Start()
	{
		cam = gameObject.GetComponent<Camera>();
		normalColor = cam.backgroundColor;
		reverseColor = new Color(0.5f, 0.5f, 0.5f, 0f);

		GameObject player = GameObject.Find("Player").gameObject;
		playerController = player.GetComponent<PlayerController>();
		playerPhysics = player.GetComponent<PlayerPhysics>();
	}

	void LateUpdate()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			ReverseTime();
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			ResumeTime();
		}
	}

	void ReverseTime()
	{
		cam.backgroundColor = reverseColor;

		PlayerPhysics.reverseTime = true;
		PlayerController.reverseTime = true;

		playerController.RevertState();
		playerPhysics.RevertState();
	}

	void ResumeTime()
	{
		cam.backgroundColor = normalColor;

		PlayerPhysics.reverseTime = false;
		PlayerController.reverseTime = false;
	}
}