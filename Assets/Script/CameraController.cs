using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private GameObject player;
	public GameObject gameOverUI;
	private Vector3 offset;
	private bool isGameOver = false;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ().gameObject;
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			transform.position = player.transform.position + offset;
		} else if (!isGameOver) {
			isGameOver = true;
			Instantiate (gameOverUI);
			FindObjectOfType<UIController> ().gameObject.SetActive (false);
		}
	}

	public GameObject getPlayer(){
		return player;
	}
}
