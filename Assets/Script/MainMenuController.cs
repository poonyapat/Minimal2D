using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public void StartButton(){
		GameController.ChangeScene ("level 1");
	}

	public void ExitButton(){
		Application.Quit ();
	}
}
