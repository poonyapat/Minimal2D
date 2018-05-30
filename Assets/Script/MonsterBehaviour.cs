using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehaviour : MonoBehaviour {
	
	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	// action attr
	public int maxHp;
	private float curHp;
	public float damage;
	public float exp;
	// about movement
	public Transform lPoint, rPoint;
	public float speed = 2;
	public int direction;
	// hp bar
	public Image maxHpBar,hpBar;

	// Use this for initialization
	void Start () {
		curHp = maxHp;
		rb2d = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		// hp and life
		hpBar.rectTransform.sizeDelta = new Vector2 (maxHpBar.rectTransform.sizeDelta.x * curHp / maxHp, maxHpBar.rectTransform.sizeDelta.y);
		if (curHp <= 0)
			Die ();
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

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Bullet")) {
			IsAttack (coll.gameObject.GetComponent<Bullet> ().damage);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("Monster")) {
			direction *= -1;
			Debug.Log (555);
		}
	}

	void Die(){
		Destroy (gameObject);
		FindObjectOfType<PlayerData> ().IncreaseExp (exp);
	}

	public void IsAttack(float damage){
		curHp -= damage;
	}
}
