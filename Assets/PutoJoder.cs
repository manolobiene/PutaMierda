using UnityEngine;
using System.Collections;

public class PutoJoder : MonoBehaviour {

	public void SyncStart () {
		gameObject.name = "PutoJoder";
	}

	public void Action () {
		Camera.main.GetComponents<AudioSource>()[2].Play();
		Application.LoadLevel (Application.loadedLevelName);
	}
}
