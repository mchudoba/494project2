using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
	/*
	 * Variables
	 */

	// Private variables
	private Text			lifeText;
	private Text			timeText;
	private float			time = 0f;
	private Player			player;

	/*
	 * Unity methods
	 */

	void Start()
	{
		lifeText = GameObject.Find("LifeText").GetComponent<Text>();
		timeText = GameObject.Find("TimeText").GetComponent<Text>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	void Update()
	{
		time += Time.deltaTime;
		timeText.text = "Time: " + time.ToString("F1");
		lifeText.text = "Lives: " + player.lives;
	}
}