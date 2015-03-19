using UnityEngine;
using System.Collections;

public class PutoJoder : MonoBehaviour {

	public void SyncStart () {
		gameObject.name = "PutoJoder";
	}

	public void Action () {
		Camera.main.GetComponents<AudioSource>()[2].Play();
		Destroy(GameObject.Find ("map"));
		GameObject.Find ("Generator").GetComponent<MapFromText> ().SyncStart ();
		Destroy (GameObject.Find ("PutaHierba"));
		//Application.LoadLevel (Application.loadedLevelName);
	}
}
