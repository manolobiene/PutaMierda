using UnityEngine;
using System.Collections;

public class PutoJoder : MonoBehaviour {

	public void SyncStart () {
		gameObject.name = "PutoJoder";
	}

	public void Action () {
		Camera.main.GetComponents<AudioSource>()[2].Play();
		Destroy (GameObject.Find ("map"));
		Destroy (GameObject.Find ("PutaHierba"));
		GameObject.Find ("Generator").GetComponent<MapFromText> ().SyncStart ();
		//Application.LoadLevel (Application.loadedLevelName);
	}
}
