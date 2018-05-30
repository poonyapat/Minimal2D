using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
	public GameObject owner;
	public float damage;
	public string title;
	protected float latestUsing = 0;
	[Range(0,5)]
	public float UsedCooldown;

	public abstract void Use();

}
