using UnityEngine;
using System.Collections;

public class PutoMenu : MonoBehaviour {
	public enum MenuStates {selectMode = 1, editor = 2, tabla = 3, idle = 4, editing = 5};
	public MenuStates MenuState;
	public bool disconnectAlert = false;
	bool disconnected = false;

	void Start () {
		MenuState = MenuStates.selectMode;
		DontDestroyOnLoad (gameObject);
	}

	void Update () {
		if (disconnectAlert) {
			disconnected = true;
		}
		if (disconnected && !disconnectAlert) {
			disconnected = false;
			GetComponent<WWWmaps>().StartCoroutine("SendMaps");
		}
	}

	void OnGUI () {
		switch (MenuState) {
		case MenuStates.selectMode:
			if (!GetComponents<AudioSource>()[3].isPlaying){
				GetComponents<AudioSource>()[0].Stop();
				GetComponents<AudioSource>()[3].Play();
			}
			if (GetComponent<OnEditing>().enabled == true){
				GetComponent<OnEditing>().enabled = false;
			}
			if (GetComponent<WWWmaps>().enabled == true){
				GetComponent<WWWmaps>().enabled = false;
			}
			break;
		case MenuStates.editor:
			if (!GetComponents<AudioSource>()[3].isPlaying){
				GetComponents<AudioSource>()[0].Stop();
				GetComponents<AudioSource>()[3].Play();
			}
			if (GetComponent<OnEditing>().enabled == true){
				GetComponent<OnEditing>().enabled = false;
			}
			if (GetComponent<WWWmaps>().enabled == false){
				GetComponent<WWWmaps>().enabled = true;
				GetComponent<WWWmaps>().SyncStart();
			}
			if (GameObject.Find ("map")){
				Destroy(GameObject.Find("map"));
			}
			GUILayout.BeginArea (new Rect (0, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f));
			//PlayerPrefs.SetString ("maps", GUILayout.TextArea (PlayerPrefs.GetString ("maps")));
			if (GUILayout.Button ("Menu", GUILayout.Height(Screen.height*0.15f * 0.8f))) {
				GetComponent<WWWmaps>().enabled = false;
				MenuState = PutoMenu.MenuStates.selectMode;
				GameObject.Find ("Manu_Editor").GetComponent<InteractiveButton>().state = InteractiveButton.states.transitionB;
			}
			if (GUILayout.Button ("EmptyPREFS", GUILayout.Height(Screen.height*0.15f * 0.2f))) {
				PlayerPrefs.DeleteAll();
				Destroy(gameObject);
				Application.LoadLevel(0);
			}
			GUILayout.EndArea ();
			break;
		case MenuStates.tabla:
			if (!GetComponents<AudioSource>()[3].isPlaying){
				GetComponents<AudioSource>()[0].Stop();
				GetComponents<AudioSource>()[3].Play();
			}
			if (GetComponent<OnEditing>().enabled == true){
				GetComponent<OnEditing>().enabled = false;
			}
			if (GUI.Button(new Rect(0,Screen.height*0.95f,Screen.width*0.3f,Screen.height*0.05f), "Volver")){
				GameObject.Find ("Empty").GetComponent<WWWtexture>().enabled = false;
				GameObject.Find ("Manu_Tabla").GetComponent<InteractiveButton>().state = InteractiveButton.states.transitionB;
				MenuState = MenuStates.selectMode;
			}
			break;
		case MenuStates.idle:
			if (GetComponent<OnEditing>().enabled == true){
				GetComponent<OnEditing>().enabled = false;
			}
			if (GameObject.Find ("map")){
				if (GameObject.Find("map").transform.localScale != new Vector3 (.7f,.7f,1)){
					GameObject.Find("map").transform.localScale = new Vector3 (.7f,.7f,1);
				}
			}
			if (GUI.Button(new Rect(0,Screen.height*0.95f,Screen.width*0.3f,Screen.height*0.05f), "Volver")){
				GameObject.Find ("Generator").GetComponent<MapFromText>().GeneradorState = MapFromText.GeneradorStates.crear;
				Destroy(GameObject.Find ("map"));
				MenuState = MenuStates.editor;
			}
			if (GUI.Button(new Rect(Screen.width*0.3f,Screen.height*0.95f,Screen.width*0.3f,Screen.height*0.05f), "Editar")){
				Destroy(GameObject.Find ("map"));
				GameObject.Find ("Generator").GetComponent<MapFromText>().GeneradorState = MapFromText.GeneradorStates.editar;
				GameObject.Find ("Generator").GetComponent<MapFromText>().SyncStart();
				MenuState = MenuStates.editing;
			}
			break;
		case MenuStates.editing:
			if (!GetComponents<AudioSource>()[3].isPlaying){
				GetComponents<AudioSource>()[0].Stop();
				GetComponents<AudioSource>()[3].Play();
			}
			if (GetComponent<OnEditing>().enabled == false){
				GetComponent<OnEditing>().enabled = true;
			}
			if (GameObject.Find ("map")){
				if (GameObject.Find("map").transform.localScale != new Vector3 (.6f,.6f,1)){
					GameObject.Find("map").transform.localScale = new Vector3 (.6f,.6f,1);
					GameObject.Find("map").transform.position = new Vector3(0,1.8f,0); 
				}
			}
			if (GUI.Button(new Rect(0,Screen.height*0.95f,Screen.width*0.3f,Screen.height*0.05f), "Salir")){
				Destroy(GameObject.Find ("map"));
				MenuState = MenuStates.editor;
			}
			if (GUI.Button(new Rect(Screen.width*0.3f,Screen.height*0.95f,Screen.width*0.3f,Screen.height*0.05f), "Guardar")){
				SaveCustoms();
				GetComponent<WWWmaps>().StartCoroutine("SendMaps");
				MenuState = MenuStates.editor;
			}
			if (GUI.Button(new Rect(Screen.width-Screen.width*0.3f,Screen.height*0.95f,Screen.width*0.3f,Screen.height*0.05f), "Borrar")){
				SaveCustoms();
				DeleteMap();
				GetComponent<WWWmaps>().StartCoroutine("SendMaps");
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

	void SaveCustoms () {
		string values = "";
		foreach (GameObject g in GameObject.FindGameObjectsWithTag ("Edita")) {
			if (values != ""){
				values += ",";
			}
			values += g.GetComponent<EditaModule>().block.ToString() + "." + g.GetComponent<EditaModule>().type.ToString();
		}
		string str = PlayerPrefs.GetString("maps");
		string newmaps = "";
		if (str != "" && str != null && str != " ") {
			string[] strs = str.Split (";".ToCharArray ());
			int a = 0;
			int b = 0;
			string[] maps = new string[strs.Length + 1];
			string[] vals = new string[strs.Length + 1];
			bool coincidencia = false;
			foreach (string s in strs) {
				maps [a] = s.Split (":".ToCharArray ()) [0];
				vals [b] = s.Split (":".ToCharArray ()) [1];
				if (maps [a] == PlayerPrefs.GetString ("SelectedMap")) {
					coincidencia = true;
					vals [b] = values;
				}
				a++;
				b++;
			}
			if (!coincidencia) {
				maps [a] = PlayerPrefs.GetString ("SelectedMap");
				vals [b] = values;
			}
			for (int i = 0; i < maps.Length; i++) {
				if (maps [i] != null && maps [i] != "") {
					if (newmaps != "") {
						newmaps += ";";
					}
					newmaps += maps [i] + ":" + vals [i];
				}
			}
		} else {
			newmaps = PlayerPrefs.GetString ("SelectedMap") + ":" + values;
		}
		PlayerPrefs.SetString ("maps", newmaps);
	}

	void DeleteMap () {
		string str = PlayerPrefs.GetString("maps");
		string newmaps = "";
		if (str != "" && str != null && str != " ") {
			string[] strs = str.Split (";".ToCharArray ());
			int a = 0;
			int b = 0;
			string[] maps = new string[strs.Length];
			string[] vals = new string[strs.Length];
			Debug.Log (PlayerPrefs.GetString ("SelectedMap"));
			foreach (string s in strs) {
				if (s.Split (":".ToCharArray ()) [0] != PlayerPrefs.GetString ("SelectedMap")){
					maps [a] = s.Split (":".ToCharArray ()) [0];
					vals [b] = s.Split (":".ToCharArray ()) [1];
					a++;
					b++;
				}
			}
			for (int i = 0; i < maps.Length; i++) {
				if (maps [i] != null && maps [i] != "") {
					if (newmaps != "") {
						newmaps += ";";
					}
					newmaps += maps [i] + ":" + vals [i];
				}
			}
		}
		PlayerPrefs.SetString ("maps", newmaps);
	}
}
