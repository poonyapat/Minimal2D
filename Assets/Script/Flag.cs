using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Player")) {
			GameController.activatingPlayer = coll.GetComponent<PlayerData>();
			GameController.activatingPlayer.IncreaseGameLevel();
			GameController.LoadLevel (GameController.activatingPlayer.LatestGameLevel);
			coll.gameObject.GetComponent<PlayerController> ().GotoSpawnPosition ();
			if (coll.gameObject.GetComponent<PlayerController> ().weapon != null)
				DontDestroyOnLoad (coll.gameObject.GetComponent<PlayerController> ().weapon.gameObject);
		}
	}
}
