using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour
{
	// Camera variables
	private Camera cam;
	private Color cameraNormalColor;
	private Color cameraReverseColor;
	private float duration = 0.25f;
	private float colorLerp = 0f;

	// Player variables
	private Renderer playerMat;
	private Color playerNormalColor;
	private Color playerDeadColor;

	// Objects that have states to be reverted to
	private PlayerController playerController;
	private PlayerPhysics playerPhysics;

	[HideInInspector]
	public static bool playerDead = false;
	private static bool internalDead = false;

	void Start()
	{
		cam = gameObject.GetComponent<Camera>();
		cameraNormalColor = cam.backgroundColor;
		cameraReverseColor = new Color(0.5f, 0.5f, 0.5f, 0f);

		GameObject player = GameObject.Find("Player1").gameObject;
		playerMat = player.GetComponentInChildren<Renderer>();
		playerNormalColor = playerMat.material.color;
		playerDeadColor = Color.red;

		playerController = player.GetComponent<PlayerController>();
		playerPhysics = player.GetComponent<PlayerPhysics>();
	}

	void LateUpdate()
	{
		if (internalDead && !playerDead)
		{
			playerDead = true;
			playerMat.material.color = playerDeadColor;
		}

		if (Input.GetKey(KeyCode.LeftShift))
		{
			ReverseTime();
			playerDead = false;
			internalDead = false;
			playerMat.material.color = playerNormalColor;
		}
		else if (colorLerp > 0f)
		{
			cam.backgroundColor = Color.Lerp(cameraNormalColor, cameraReverseColor, colorLerp);
			colorLerp -= Time.deltaTime / duration;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			ResumeTime();
		}
	}

	void ReverseTime()
	{
		cam.backgroundColor = Color.Lerp(cameraNormalColor, cameraReverseColor, colorLerp);
		if (colorLerp < 1f)
			colorLerp += Time.deltaTime / duration;

		PlayerPhysics.reverseTime = true;
		PlayerController.reverseTime = true;

		playerController.RevertState();
		playerPhysics.RevertState();
	}

	void ResumeTime()
	{
		PlayerPhysics.reverseTime = false;
		PlayerController.reverseTime = false;
	}

	public static void KillPlayer()
	{
		internalDead = true;
	}
}