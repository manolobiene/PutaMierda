using UnityEngine;
using System.Collections;

public class VictoryDance : MonoBehaviour {
	Vector3 To;

	void Start () {
		foreach (GameObject g in GameObject.FindObjectsOfType (typeof(GameObject))) {
			if (g.GetComponent<BoxCollider2D>()){
				if (g.layer != LayerMask.NameToLayer("Ignore Raycast")){
					if (g.name != "Main Camera" && g.name != "Generator" && g.name != "Empty" && g != gameObject){
						Destroy(g.GetComponent<PunchMe>());
					}
				}
			}
		}
		Camera.main.GetComponents<AudioSource>()[6].Stop();
		Destroy (GetComponent<MoveLikeCrazy> ());
		Vector3 GP = GameObject.Find ("Gloria_completed").transform.position;
		Vector3 P = transform.position;
		To = new Vector3(GP.x, GP.y, P.z);
	}

	void Update () {
		Vector3 dist = To - transform.position;
		if (dist.magnitude > 0.1f) {
			if (!Camera.main.GetComponents<AudioSource>()[4].isPlaying){
				Camera.main.GetComponents<AudioSource>()[0].Stop();
				Camera.main.GetComponents<AudioSource>()[4].Play();
			}
			transform.position = Vector3.Slerp (transform.position, To, 2f * Time.deltaTime);
		} else {
			if (transform.localScale != new Vector3(0.01f,0.01f,1)){
				if (!Camera.main.GetComponents<AudioSource>()[5].isPlaying){
					Camera.main.GetComponents<AudioSource>()[0].Stop();
					Camera.main.GetComponents<AudioSource>()[4].Stop();
					Camera.main.GetComponents<AudioSource>()[5].Play();
				}
				transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.01f,0.01f,1), 1f * Time.deltaTime);
				transform.rotation *= Quaternion.Euler(new Vector3(0,0,1)*100*Time.deltaTime);
			}else{
				if (!Camera.main.GetComponents<AudioSource>()[1].isPlaying){
					Camera.main.GetComponents<AudioSource>()[4].Stop();
					Camera.main.GetComponents<AudioSource>()[5].Stop();
					Camera.main.GetComponents<AudioSource>()[1].Play();
					Camera.main.GetComponents<AudioSource>()[3].Play();
				}
				Camera.main.GetComponent<PutoMenu> ().MenuState = PutoMenu.MenuStates.editor;
			}
		}
	}
}
