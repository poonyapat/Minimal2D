using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject owner;
	public float damage = 1;
	public float speed = 1;
	public float lifeTime = 1;
	public float err = 1;

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Floor") || coll.gameObject.CompareTag ("Monster") || coll.gameObject.CompareTag("Player") && coll.gameObject != owner)
			Destroy (gameObject);
	}
}
