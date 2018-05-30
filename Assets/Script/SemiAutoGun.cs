using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoGun : MonoBehaviour {

	private float latestFire = 0;
	private float miniCoolDown = 0.15f;
	private int nBullet;
	[Range(2,3)]
	public int bulletOutput;

	public bool Ready(){
		if (Time.time > latestFire + miniCoolDown && nBullet > 0) {
			nBullet--;
			return true;
		}
		return false;
	}

	public void Activate(){
		this.nBullet = bulletOutput;
	}

	public bool last(){
		return nBullet == 0;
	}
}
