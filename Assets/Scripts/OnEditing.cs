using UnityEngine;
using System.Collections;

public class OnEditing : MonoBehaviour {
	public int block;
	
	void OnGUI () {
		GUI.Window (0, new Rect (0, Screen.height * 0.8f, Screen.width, Screen.height * 0.15f), ObjWindow, "objetos");
	}

	void ObjWindow (int id) {
		GUILayout.BeginHorizontal ();
		GUI.color = Color.black;
		if (GUILayout.Button ("")) {
			block = 0;
		}
		GUI.color = Color.white;
		if (GUILayout.Button ("")) {
			block = 1;
		}
		GUI.color = Color.blue;
		if (GUILayout.Button ("")) {
			block = 2;
		}
		GUI.color = Color.yellow;
		if (GUILayout.Button ("")) {
			block = 3;
		}
		GUI.color = Color.red;
		if (GUILayout.Button ("")) {
			block = 4;
		}
		GUI.color = Color.green;
		if (GUILayout.Button ("")) {
			block = 5;
		}
		GUI.color = Color.cyan;
		if (GUILayout.Button ("")) {
			block = 6;
		}
		GUILayout.EndHorizontal ();
		GUI.color = Color.white;
	}
}
