using UnityEngine;
using System.Collections;

public class PutoScript : MonoBehaviour {
	
	void Start () {
		Application.LoadLevel(PlayerPrefs.GetInt ("JodidaComida")+2);
	}
}
