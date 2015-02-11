using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
	/* -----Variables----- */
	
	private const string 		gameScene = "Game_Scene";
	private const string 		menuScene = "Menu_Scene";
	private Text				highScoreText;
	//private GameObject			areYouSure;
	public bool					resetScore = false;
	
	/* -----Unity methods----- */
	
	void Awake()
	{
		// If there is a high score, get it
		if (PlayerPrefs.HasKey("TimeHighScore"))
			GameController.bestTime = PlayerPrefs.GetFloat("TimeHighScore");
		PlayerPrefs.SetFloat("TimeHighScore", GameController.bestTime);

		highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
		highScoreText.text = "High Score: " + GameController.bestTime.ToString("F1");
		//areYouSure = GameObject.Find("AreYouSure");
		//areYouSure.SetActive(false);
	}

	void Update()
	{
		if (resetScore)
		{
			resetScore = false;
			ResetYes();
		}
	}

	/* -----Custom methods----- */

	public void Play()
	{
		Application.LoadLevel(gameScene);
	}

	public void ResetHighScore()
	{
		//areYouSure.SetActive(true);
	}

	public void ResetYes()
	{
		GameController.bestTime = 0f;
		PlayerPrefs.SetFloat("TimeHighScore", GameController.bestTime);
		highScoreText.text = "High Score: " + GameController.bestTime.ToString("F1");

		//areYouSure.SetActive(false);
	}

	public void ResetNo()
	{
		//areYouSure.SetActive(false);
	}
}