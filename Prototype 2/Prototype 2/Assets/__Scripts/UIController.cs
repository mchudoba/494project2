using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
	private PlayerController player1;
	private PlayerController player2;
	private Text player1Text;
	private Text player2Text;
	private Slider player1Slider;
	private Slider player2Slider;

	void Start()
	{
		player1 = GameObject.Find("Player1").GetComponent<PlayerController>();
		player2 = GameObject.Find("Player2").GetComponent<PlayerController>();
		player1Text = GameObject.Find("Player1Text").GetComponent<Text>();
		player2Text = GameObject.Find("Player2Text").GetComponent<Text>();
		player1Slider = GameObject.Find("Player1Rewind").GetComponent<Slider>();
		player2Slider = GameObject.Find("Player2Rewind").GetComponent<Slider>();

		player1Text.text = "Player 1\nHealth: " + player1.health + "\nLives: " + player1.lives;
		player2Text.text = "Player 2\nHealth: " + player2.health + "\nLives: " + player2.lives;

		player1Slider.maxValue = player1.rewindTime;
		player2Slider.maxValue = player2.rewindTime;
		player1Slider.value = player1Slider.maxValue;
		player2Slider.value = player2Slider.maxValue;
	}

	void Update()
	{
		player1Text.text = "Player 1\nHealth: " + player1.health + "\nLives: " + player1.lives;
		player2Text.text = "Player 2\nHealth: " + player2.health + "\nLives: " + player2.lives;
		player1Slider.value = player1.rewindTimeLeft;
		player2Slider.value = player2.rewindTimeLeft;
	}
}
