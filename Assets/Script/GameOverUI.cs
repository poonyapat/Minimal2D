using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

	public void TryAgainButton(){
		GameController.ChangeScene ("level 1");
		GameController.level = 0;
	}

	public void ExitButton(){
		Application.Quit ();
	}
}
