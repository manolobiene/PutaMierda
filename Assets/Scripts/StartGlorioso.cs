using UnityEngine;
using System.Collections;

public class StartGlorioso : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("JodidaComida", 0);
		Application.LoadLevel (1);
		Destroy (this);
	}
}
