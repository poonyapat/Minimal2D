using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	private PlayerData pd;
	
	// weapon
	public Weapon weapon;
	// jumping
	private PlayerFoot foot;

	// find item
	public GameObject itemDrop;

	
    private bool runActivation = false;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		rb2d = GetComponent<Rigidbody2D> ();
		pd = GetComponent<PlayerData> ();
		foot = GetComponentInChildren<PlayerFoot> ();
		pd.SetAll(GameController.activatingPlayer);
	}

	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("Monster") && !pd.IsImmortal()) {
			IsAttacked (coll.gameObject.GetComponent<MonsterBehaviour> ().damage);
			rb2d.AddForce(new Vector2(0, 200));
			pd.UpdateLatestAttackTime();
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.CompareTag ("Monster") && !pd.IsImmortal()) {
			IsAttacked (coll.gameObject.GetComponent<MonsterBehaviour> ().damage);
			rb2d.AddForce(new Vector2(0, 300));
			pd.UpdateLatestAttackTime();
			coll.gameObject.GetComponent<MonsterBehaviour> ().direction *= -1;
		}
		
		if (coll.gameObject.CompareTag ("DeadLine"))
			Die ();
	}

	void Update(){
		// if (Input.GetKeyDown (KeyCode.X))
		// 	GameController.SavePlayerData (pd);
		// if (Input.GetKeyDown (KeyCode.Z)) {
		// 	pd.SetAll (GameController.LoadPlayerData ());
		// }
		if (itemDrop != null && Input.GetKey (KeyCode.Q)){
			if (itemDrop.GetComponentInChildren<Weapon> () != null) {
				setWeapon (itemDrop.GetComponentInChildren<Weapon> ());
				Destroy (itemDrop);
			}
		}
		// is dead?
		if (pd.CurHp <= 0)
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
		if ((Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !foot.jumping && pd.IsJumpable()) {
			rb2d.velocity = new Vector2 (0, pd.JumpPower);
			pd.CurSta = pd.CurSta - pd.JumpCost;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// stamina regeneration
		if (pd.CurSta < pd.MaxSta ){
			if (rb2d.velocity != Vector2.zero || Input.anyKey)
				pd.RegenerateStamina();
			else
				pd.RegenerateStaminaAtSleep();
		}
		

		// control movement
		// move
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			if (runActivation && pd.IsRunnable()) {
				rb2d.transform.Translate (pd.RunSpeed, 0, 0);
				pd.CurSta = pd.CurSta- pd.RunCost;
			}
			else
				rb2d.transform.Translate (pd.WalkSpeed , 0, 0);
			sr.flipX = false;
		} else if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			if (runActivation && pd.IsRunnable()) {
				rb2d.transform.Translate (-pd.RunSpeed, 0, 0);
				pd.CurSta = pd.CurSta - pd.RunCost;
			}
			else
				rb2d.transform.Translate (-pd.WalkSpeed , 0, 0);
			sr.flipX = true;
		}
	}

	private void Die(){
		pd.DecreaseLifePoint();
		if (pd.LifePoint <= 0)
			Destroy (gameObject);
		else {
			pd.CurHp = pd.MaxHp;
			pd.CurSta = pd.MaxSta;
			GotoSpawnPosition ();
			rb2d.velocity = Vector2.zero;
		}	
	}


	public void setWeapon(Weapon weapon){
		this.weapon = Instantiate (weapon);
		this.weapon.owner = gameObject;
	}



	public void IsAttacked (float damage){
		pd.CurHp = pd.CurHp - damage;
	}

	public void GotoSpawnPosition(){
		transform.position = GameController.SpawnPosition [pd.LatestGameLevel];
	}
}
