using UnityEngine;
using System.Collections;

public class PutoMenu : MonoBehaviour {
	public enum MenuStates {selectMode = 1, editor = 2, singleplayer = 3, idle = 4};
	public MenuStates MenuState;
	public bool disconnectAlert = false;

	void Start () {
		MenuState = MenuStates.selectMode;
		DontDestroyOnLoad (gameObject);
	}

	void OnGUI () {
		switch (MenuState) {
		case MenuStates.selectMode:
			if (GetComponent<WWWmaps>().enabled == true){
				GetComponent<WWWmaps>().enabled = false;
			}
			if (GUI.Button(new Rect(0,0,Screen.width,Screen.height/2), "Jugar")){
				MenuState = MenuStates.singleplayer;
			}
			if (GUI.Button(new Rect(0,Screen.height/2,Screen.width,Screen.height/2), "Editar")){
				MenuState = MenuStates.editor;
			}
			break;
		case MenuStates.editor:
			if (GetComponent<WWWmaps>().enabled == false){
				GetComponent<WWWmaps>().enabled = true;
			}
			break;
		case MenuStates.singleplayer:
			break;
		case MenuStates.idle:
			if (GUI.Button(new Rect(0,Screen.height*0.95f,Screen.width*0.3f,Screen.height*0.05f), "Selector")){
				Destroy(GameObject.Find ("map"));
				MenuState = MenuStates.editor;
			}
			break;
		default:
			break;
		}

		if (disconnectAlert) {
			GUILayout.BeginArea(new Rect(0,Screen.height*0.9f, Screen.width, Screen.height*0.2f));
			GUILayout.Box("Error de conexion. \nLos datos no se guardaran ni se actualizaran " +
				"\nError: " + PlayerPrefs.GetString("NetError"));
			GUILayout.EndArea();
		}
	}
}
