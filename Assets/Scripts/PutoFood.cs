using UnityEngine;
using System.Collections;

public class PutoFood : MonoBehaviour {
	Vector3 topos;

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "PutoJoder") {
			col.GetComponent<PutoJoder>().Action();
		}
		if (col.gameObject.tag == "GloriaVendita") {
			if (col.gameObject.name == "Gloria_Pasa"){
				col.GetComponent<Gloria>().Pasa();
			}
		}
		if (col.gameObject.tag == "Coin") {
			if (col.gameObject.name == "Coin"){
				col.GetComponent<Coin>().Action();
			}
		}
	}

	void OnMouseDown (){
		Vector3 cpos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 pos = transform.position;
		topos = cpos - pos;
	}

	void OnMouseDrag (){
		Vector3 cpos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 pos = transform.position;
		pos = new Vector3 (cpos.x - topos.x, cpos.y - topos.y, pos.z);
		transform.position = pos;
	}
}
