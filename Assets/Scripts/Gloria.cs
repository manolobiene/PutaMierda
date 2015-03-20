using UnityEngine;
using System.Collections;

public class Gloria : MonoBehaviour {
	public GameObject DestParticles;
	int points = 0;

	void OnGUI () {
		if (gameObject.name == "Gloria") {
			Vector3 gpos = Camera.main.WorldToScreenPoint (transform.position);
			GUI.Box (new Rect (gpos.x - Screen.width * 0.08f/2, Screen.height - gpos.y - Screen.height * 0.05f/2
			                   , Screen.width * 0.08f, Screen.height * 0.05f), Mathf.Abs(points).ToString ());
		}
	}

	public void SyncStart () {
		if (gameObject.name != "Gloria_Pasa")
			gameObject.name = "Gloria";
		foreach (GameObject g in GameObject.FindGameObjectsWithTag ("Coin")) {
			if (g.name == "Coin"){
				points--;
			}
		}
	}

	public void AddPoint () {
		points++;
		if (points >= 0) {
			MakePasa();
		}
	}

	public void MakePasa() {
		gameObject.name = "Gloria_Pasa";
		GetComponent<SpriteRenderer> ().material.color = Color.yellow;
	}

	public void Pasa () {
		Camera.main.GetComponents<AudioSource> () [1].Play ();
		Demolition D = (Demolition)gameObject.AddComponent<Demolition> ();
		D.DestParticles = DestParticles;
		Destroy (this);
	}
}
