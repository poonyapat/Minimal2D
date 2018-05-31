using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLoopMovement : MonoBehaviour {

	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	// about movement
	public Transform lPoint, rPoint;
	public float speed = 2;
	public int direction;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		// set direction
		if (transform.position.x > rPoint.position.x)
			direction = -1;
		else if (transform.position.x < lPoint.position.x)
			direction = 1;
		// movement and facing
		rb2d.velocity = new Vector2 (direction*speed, rb2d.velocity.y);
		if (rb2d.velocity.x > 0)
			sr.flipX = false;
		else if (rb2d.velocity.x < 0)
			sr.flipX = true;
	}

	
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("Monster")) {
			direction *= -1;
		}
	}
}
