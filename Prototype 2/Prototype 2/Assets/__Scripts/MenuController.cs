using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
	public void StartLevel1()
	{
		Application.LoadLevel("Level_1");
	}

	public void StartLevel2()
	{
		Application.LoadLevel("Level_2");
	}

	public void StartDemo()
	{
		Application.LoadLevel("Level_Demo");
	}

	void Update()
	{
		if (Input.GetButtonDown("Cancel"))
			Application.LoadLevel(Application.loadedLevelName);
	}
}
