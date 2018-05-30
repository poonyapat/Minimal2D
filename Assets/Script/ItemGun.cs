using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGun: MonoBehaviour {
	private Gun gun;
	public Canvas detail;
	public Text gunName, gunType, ammo, bulletType;
	// Use this for initialization
	void Start () {
		gun = GetComponentInChildren<Gun> ();
		gunName.text = gun.name;
		gunType.text = "(" + gun.type + ")";
		ammo.text = "Ammo: " + gun.ammo;
		bulletType.text = gun.BulletPref.name;
	}

	void OnTriggerEnter2D(Collider2D coll){
		Debug.Log (2);
		if (coll.gameObject.CompareTag ("Player")) {
			detail.gameObject.SetActive (true);
			coll.gameObject.GetComponent<PlayerController> ().itemDrop = gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Player")) {
			detail.gameObject.SetActive (false);

			coll.gameObject.GetComponent<PlayerController> ().itemDrop = null;
		}
			
	}
}
