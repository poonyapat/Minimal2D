using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {
	public enum Types {AUTOMATIC, SEMI, MANUAL, SHOTGUN, GRENADE}
	public GameObject BulletPref;
	[Range(0,30)]
	public float err;
	[Range(5,1000)]
	public int bulletSpeed;
	[Range(0,3)]
	public float bulletLifeTime;
	[Range(1, 999)]
	public int ammo;
	public Types type;
	// for semi-auto and shotgun

	public override void Use(){
		if (Time.time < latestUsing + UsedCooldown)
			return;
		if (ammo <= 0) {
			Destroy (gameObject);
		}
		switch (type){
			case Types.AUTOMATIC:
				AutomaticFire ();
				break;
			case Types.SEMI:
				SemiAutoFire ();
				break;
			case Types.MANUAL:
				ManualFire ();
				break;
			case Types.SHOTGUN:
				ShotGunFire ();
				break;
			case Types.GRENADE:
				GrenadeLaunch ();
				break;
		}
	}

	private void AutomaticFire(){
		if (Input.GetKey (KeyCode.Space)) {
			SpawnBullet ();
			ammo--;
			latestUsing = Time.time;
		}
	}

	private void SemiAutoFire(){
		SemiAutoGun sag = GetComponent<SemiAutoGun> ();
		if (Input.GetKeyDown (KeyCode.Space)) {
			sag.Activate ();
		}
		if (sag.Ready ()) {
			SpawnBullet ();
			ammo--;
			if (sag.last())
				latestUsing = Time.time;
		}
	}

	private void ManualFire(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			SpawnBullet ();
			ammo--;
			latestUsing = Time.time;
		}
	}

	private void ShotGunFire(){
		if (Input.GetKeyDown (KeyCode.Space)) {
			for (int i = 0; i < GetComponent<Shotgun> ().bulletOutput; i++) {
				SpawnBullet ();
			}
			ammo--;
			latestUsing = Time.time;
		}
	}

	private void GrenadeLaunch(){
		// projectile move

	}

	private void SpawnBullet(){
		GameObject bullet = Instantiate (BulletPref, owner.transform.position, Quaternion.identity);
		Bullet bulletComp = bullet.GetComponent<Bullet> ();
		// set attribute
		bulletComp.damage *= damage;
		bulletComp.owner = owner;
		// set movement speed and direction
		Vector2 movement = bulletMovementDirection (bulletComp.err);
		bullet.GetComponent<Rigidbody2D> ().rotation = Vector2.Angle (Vector2.right, movement);
		if (movement.y < 0)
			bullet.GetComponent<Rigidbody2D> ().rotation *= -1;
		bullet.GetComponent<Rigidbody2D> ().velocity = movement * bulletComp.speed * bulletSpeed;
		if (owner.GetComponent<SpriteRenderer> ().flipX)
			bullet.GetComponent<Rigidbody2D> ().velocity *= -1;
		// life time
		Destroy (bullet, bulletLifeTime*bulletComp.lifeTime);
	}

	private Vector2 bulletMovementDirection (float externalErr){
		float error = err * externalErr;
		error = Random.Range(-error, error);
		float sin = Mathf.Sin (error * Mathf.Deg2Rad);
		float cos = Mathf.Cos (error * Mathf.Deg2Rad);

		return new Vector2 (cos, sin);
	}
}
