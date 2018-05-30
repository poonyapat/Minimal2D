using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
	public string nextScene;
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Player")) {
			GameController.ChangeScene (nextScene);
			GameController.level += 1;
			coll.gameObject.GetComponent<PlayerController> ().GotoSpawnPosition ();
			if (coll.gameObject.GetComponent<PlayerController> ().weapon != null)
				DontDestroyOnLoad (coll.gameObject.GetComponent<PlayerController> ().weapon.gameObject);
		}
	}
}
