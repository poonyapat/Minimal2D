using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	private PlayerController player;
	private PlayerData data;
	public Image hpBar, staBar, expBar;
	public Image maxHpBar, maxStaBar, maxExpBar;
	public Text levelText, lifeText;
	public Button upSkill, upStat;
	public Text weapon;
	public Image upStatPage;

	// Use this for initialization
	void Start() {
		player = FindObjectOfType<PlayerController> ();
		data = player.GetComponent<PlayerData> ();
	}

	// Update is called once per frame
	void Update () {
		if (player == null)
			return;
		if (data.statPoint <= 0 && upStatPage.IsActive())
			upStatPage.gameObject.SetActive (false);
		upSkill.gameObject.SetActive (data.skillPoint > 0);
		upStat.gameObject.SetActive (data.statPoint > 0);
		Debug.Log (data.statPoint);
		hpBar.rectTransform.sizeDelta = new Vector2(player.GetHpPercentage() * maxHpBar.rectTransform.sizeDelta.x, maxHpBar.rectTransform.sizeDelta.y);
		staBar.rectTransform.sizeDelta = new Vector2 (player.GetStaPercentage () * maxStaBar.rectTransform.sizeDelta.x, maxStaBar.rectTransform.sizeDelta.y);
		expBar.rectTransform.sizeDelta = new Vector2 (player.GetExpPercentage () * maxExpBar.rectTransform.sizeDelta.x, maxExpBar.rectTransform.sizeDelta.y);
		levelText.text = "Level:\t" + data.GetLevel ();
		lifeText.text = "Life:\t" + player.GetLifePoint ();
		if (player.weapon is Gun && ((Gun)player.weapon).ammo > 0)
			weapon.text = player.weapon.title + " (" + ((Gun)player.weapon).BulletPref.name + ") x" + ((Gun)player.weapon).ammo;
		else if (player.weapon == null)
			weapon.text = "";
	}

	public void ShowUpStatPage(){
		upStatPage.gameObject.SetActive (true);
	}

	public void CloseUpStatPage(){
		upStatPage.gameObject.SetActive (false);
	}

	public void IncreaseStaminRegen(){
		data.IncreaseStaminRegen ();
	}

	public void IncreaseMaxSta(){
		data.IncreaseMaxSta ();
	}
	// increase max hp
	public void IncreaseMaxHp(){
		data.IncreaseMaxHp ();
	}
	// increase base speed
	public void IncreaseWalkSpeed(){
		data.IncreaseWalkSpeed ();
	}
	// decrease run cost
	public void DecreaseMovementCost(){
		data.DecreaseMovementCost ();
	}
	// increase jump power
	public void IncreaseJumpPower(){
		data.IncreaseJumpPower ();
	}


}
