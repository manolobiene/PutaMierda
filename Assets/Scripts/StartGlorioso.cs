using UnityEngine;
using System.Collections;

public class StartGlorioso : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log (PlayerPrefs.GetInt("identification"));
		Debug.Log (PlayerPrefs.GetString("name"));
		PlayerPrefs.SetInt ("JodidaComida", 0);
		Application.LoadLevel (1);
		Destroy (this);
	}
}
