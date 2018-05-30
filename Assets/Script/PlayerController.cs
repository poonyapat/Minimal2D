using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	private PlayerData pd;
	// unstable status
	public int lifePoint = 3;
	private float curHp = 100;
	private float curSta = 100;
	private bool runActivation = false;
	//private bool jumping = true;
	// is attacked
	private float immortalTime = 0.25f;
	private float latestAttacked = -0.25f;
	// weapon
	public Weapon weapon;
	// jumping
	private PlayerFoot foot;

	// find item
	public GameObject itemDrop;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		sr = GetComponent<SpriteRenderer> ();
		rb2d = GetComponent<Rigidbody2D> ();
		pd = GetComponent<PlayerData> ();
		foot = GetComponentInChildren<PlayerFoot> ();
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.CompareTag ("DeadLine"))
			Die ();
	}

	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("Monster") && Time.time > immortalTime + latestAttacked) {
			IsAttacked (coll.gameObject.GetComponent<MonsterBehaviour> ().damage);
			latestAttacked = Time.time;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("Monster") && Time.time > immortalTime + latestAttacked) {
			IsAttacked (coll.gameObject.GetComponent<MonsterBehaviour> ().damage);
			latestAttacked = Time.time;
			coll.gameObject.GetComponent<MonsterBehaviour> ().direction *= -1;
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.X))
			GameController.SavePlayerData (pd);
		if (Input.GetKeyDown (KeyCode.Z)) {
			pd.SetAll (GameController.LoadPlayerData ());
		}
		if (itemDrop != null && Input.GetKey (KeyCode.Q)){
			if (itemDrop.GetComponentInChildren<Weapon> () != null) {
				setWeapon (itemDrop.GetComponentInChildren<Weapon> ());
				Destroy (itemDrop);
			}
		}
		// is dead?
		if (curHp <= 0)
			Die ();
		if (weapon != null)
			weapon.Use();
		if (Input.GetKey (KeyCode.LeftShift))
			runActivation = true;
		else
			runActivation = false;
		// set jumping state
		//jumping = rb2d.velocity.y != 0;

		// jump
		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !foot.jumping && curSta >= pd.GetJumpCost()) {
			rb2d.velocity = new Vector2 (0, pd.GetJumpPower());
			curSta -= pd.GetJumpCost();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// stamina regeneration
		if (curSta < pd.GetMaxSta()) {
			curSta +=  (rb2d.velocity != Vector2.zero || Input.anyKey)? pd.GetRegSta() : pd.GetRegStaSlp();
		}
		else
			curSta = pd.GetMaxSta();
		

		// control movement
		// move
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			if (runActivation && curSta > pd.GetRunCost()) {
				rb2d.transform.Translate (pd.GetRunSpeed(), 0, 0);
				curSta -= pd.GetRunCost();
			}
			else
				rb2d.transform.Translate (pd.GetWalkSpeed() , 0, 0);
			sr.flipX = false;
		} else if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			if (runActivation && curSta > pd.GetRunCost()) {
				rb2d.transform.Translate (-pd.GetRunSpeed(), 0, 0);
				curSta -= pd.GetRunCost();
			}
			else
				rb2d.transform.Translate (-pd.GetWalkSpeed() , 0, 0);
			sr.flipX = true;
		}
	}

	private void Die(){
		lifePoint--;
		if (lifePoint <= 0)
			Destroy (gameObject);
		else {
			curHp = pd.GetMaxHp();
			curSta = pd.GetMaxSta();
			GotoSpawnPosition ();
		}	
	}


	public void setWeapon(Weapon weapon){
		this.weapon = Instantiate (weapon);
		this.weapon.owner = gameObject;
	}

	public float GetHpPercentage(){
		return curHp / pd.GetMaxHp();
	}

	public float GetExpPercentage(){
		return pd.GetExpPrecentage ();
	}

	public float GetStaPercentage(){
		return curSta / pd.GetMaxSta();
	}

	public void IsAttacked (float damage){
		curHp -= damage;
	}

	public int GetLifePoint(){
		return lifePoint;
	}

	public void GotoSpawnPosition(){
		transform.position = GameController.SpawnPosition [GameController.level];
	}
}
