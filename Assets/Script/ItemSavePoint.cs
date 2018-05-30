using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSavePoint : MonoBehaviour
{

	public GameObject detail, savePoint;
	public bool activated = false;


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            detail.SetActive(true);
            coll.gameObject.GetComponent<PlayerController>().itemDrop = gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            detail.SetActive(false);

            coll.gameObject.GetComponent<PlayerController>().itemDrop = null;
        }

    }

	public void Activate(){
		if (!activated){
			savePoint.SetActive(true);
			activated = true;
			Time.timeScale = 0;
		}
	}

	public void Close(){
		savePoint.SetActive(false);
		activated = false;
		Time.timeScale = 1;
	}
}
