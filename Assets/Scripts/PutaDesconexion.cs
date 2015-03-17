using UnityEngine;
using System.Collections;

public class PutaDesconexion : MonoBehaviour {
	
	void OnGUI () {
		GUI.Button (new Rect (0, 0, Screen.width, Screen.height)
		            , "Ha habido un problema al conectarse a la red \nERROR: " + PlayerPrefs.GetString ("NetError"));
	}
}
