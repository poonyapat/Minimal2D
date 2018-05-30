using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	void Start()
	{
		PlayerData.LoadStatData();
	}

	public void StartButton(){
		GameController.activatingPlayer = new PlayerData();
		GameController.LoadLevel(GameController.activatingPlayer.LatestGameLevel);
	}

	public void ExitButton(){
		Application.Quit ();
	}
}
