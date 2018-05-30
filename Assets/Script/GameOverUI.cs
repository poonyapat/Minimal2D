using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

	public void TryAgainButton(){
		GameController.ChangeScene ("level " + GameController.activatingPlayer.LatestGameLevel);
	}

	public void ExitButton(){
		Application.Quit ();
	}
}
