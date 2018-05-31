using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehaviour : MonoBehaviour {
	

	// action attr
	public int maxHp;
	private float curHp;
	public float damage;
	public float exp;

	// hp bar
	public Image maxHpBar,hpBar;

	// Use this for initialization
	void Start () {
		curHp = maxHp;
	}

	// Update is called once per frame
	void Update () {
		// hp and life
		hpBar.rectTransform.sizeDelta = new Vector2 (maxHpBar.rectTransform.sizeDelta.x * curHp / maxHp, maxHpBar.rectTransform.sizeDelta.y);
		
		if (curHp <= 0)
			Die ();
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("Bullet")) {
			IsAttack (coll.gameObject.GetComponent<Bullet> ().damage);
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
