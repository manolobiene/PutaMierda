using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	
	public void SyncStart () {
		gameObject.name = "Coin";
	}

	public void Action () {
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("GloriaVendita")) {
			if (g.name == "Gloria"){
				g.GetComponent<Gloria>().AddPoint();
			}
		}
		gameObject.name = "Coin_used";
		GetComponent<SpriteRenderer> ().material.color = Color.white;
	}
}
