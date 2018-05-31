using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

	public void TryAgainButton(){
		GameController.LoadLevel(GameController.activatingPlayer.LatestGameLevel);
	}

	public void MainMenuButton(){
		GameController.ChangeScene("Main Menu");
	}

	public void ExitButton(){
		Application.Quit ();
	}
}
