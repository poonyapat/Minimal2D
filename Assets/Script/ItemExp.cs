using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExp : MonoBehaviour {

	public int exp;
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Player")) {
			coll.GetComponent<PlayerData> ().IncreaseExp (exp);
			Destroy (gameObject);
		}
	}
}
