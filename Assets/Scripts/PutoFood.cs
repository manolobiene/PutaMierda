using UnityEngine;
using System.Collections;

public class PutoFood : MonoBehaviour {
	Vector3 topos;

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "PutoJoder") {
			Camera.main.GetComponents<AudioSource>()[2].Play();
			Application.LoadLevel(Application.loadedLevelName);
		}
		if (col.gameObject.tag == "GloriaVendita") {
			if (col.gameObject.name == "Gloria_Pasa"){
				int i = PlayerPrefs.GetInt ("JodidaComida");
				PlayerPrefs.SetInt ("JodidaComida", i+1);
				PlayerPrefs.SetInt ("JodidaComidaYaTenida", i);
				Camera.main.GetComponents<AudioSource>()[1].Play();
				Application.LoadLevel(1);
			}
			if (col.gameObject.name == "PutoNombrador"){
				if (!GameObject.Find("Gloria_Pasa")){
					GameObject gloria = GameObject.Find("Gloria");
					gloria.name = "Gloria_Pasa";
					gloria.GetComponent<SpriteRenderer>().material.color = Color.yellow;
					Destroy (col.GetComponent<BoxCollider2D>());
					Material mat = new Material(col.GetComponent<SpriteRenderer>().material);
					mat.color = Color.white;
					col.GetComponent<SpriteRenderer>().material = mat;
					col.name = col.name + "-Deceased";
				}
			}
		}
	}

	void OnMouseEnter (){
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
