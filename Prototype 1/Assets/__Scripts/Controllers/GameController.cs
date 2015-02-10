using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
	/* -----Variables----- */

	// Game management variables
	private const string 		gameScene = "Game_Scene";
	private const string 		menuScene = "Menu_Scene";
	private SpawnController 	spawner;
	private GameObject			instructions;
	private Player 				player;
	private Text				lifeText;
	private Text				timeText;
	private Text				highScoreText;
	private float				time = 0f;

	// Statics
	static public float			bestTime = 0f;

	/* -----Unity methods----- */

	void Awake()
	{
		// If there is a high score, get it
		//  (This is handled in the main menu, but repeated
		//  here for debugging purposes)
		if (PlayerPrefs.HasKey("TimeHighScore"))
			GameController.bestTime = PlayerPrefs.GetFloat("TimeHighScore");

		// Set all object references
		player = 			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		instructions =		GameObject.Find("Instructions");
		lifeText = 			GameObject.Find("LifeText").GetComponent<Text>();
		timeText = 			GameObject.Find("TimeText").GetComponent<Text>();
		highScoreText =		GameObject.Find("HighScoreText").GetComponent<Text>();
		spawner = 			this.gameObject.GetComponent<SpawnController>();

		Wait();
	}

	void Update()
	{
		if (player.Waiting)
			return;

		// Update time and life text
		time += Time.deltaTime;
		timeText.text = "Time: " + time.ToString("F1");
		lifeText.text = "Lives: " + player.lives;
		highScoreText.text = "High Score: " + bestTime.ToString("F1");

		// If the current time beats the previous high score, set the high score
		if (time > PlayerPrefs.GetFloat("TimeHighScore"))
		{
			bestTime = time;
			PlayerPrefs.SetFloat("TimeHighScore", bestTime);
		}

		// Load main menu when player dies
		if (player.lives <= 0)
			Application.LoadLevel(menuScene);
	}

	/* -----Custom methods----- */

	// Enable/disable certain UI elements until the user moves
	public void Wait()
	{
		spawner.enabled = false;
		timeText.enabled = false;
		highScoreText.enabled = false;

		lifeText.text = "Lives: " + player.lives;
	}

	// Enable/disable certain UI elements once the user moves
	public void Play()
	{
		instructions.SetActive(false);
		timeText.enabled = true;
		highScoreText.enabled = true;
		spawner.enabled = true;
	}
}
