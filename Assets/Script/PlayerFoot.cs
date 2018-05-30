using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour {

	public bool jumping = false;

	void OnTriggerEnter2D(Collider2D coll){
		jumping = false;
	}

	void OnTriggerExit2D(Collider2D coll){
		jumping = true;
	}

	void OnTriggerStay2D(Collider2D coll){
		jumping = false;
	}
}
