using UnityEngine;
using System.Collections;

public class WWWmaps : MonoBehaviour {
	string[] maps = new string[1];
	string[] values = new string[1];
	string[] objs = new string[1];
	string[] types = new string[1];
	Texture2D[] mapsT = new Texture2D[16];
	float posy;
	Vector3 storedMouse;
	public bool syncing = false;

	//notification
	bool noprofile = false;

	public void SyncStart () {
		StartCoroutine("UpdateData");
	}

	void OnGUI () {
		if (!syncing) {
			GUILayout.BeginArea (new Rect (Screen.width*0.05f, Screen.height*0.2f, Screen.width*0.9f, Screen.height*0.7f));
			int x = 0;
			int y = 0;
				if (mapsT [0] != null) {
					for (int i = 0; i < maps.Length; i++) {
					GUI.DrawTexture (new Rect (Screen.width * 0.015f + Screen.width * 0.2f * x, Screen.width * 0.025f * 1.6f + Screen.width * 0.2f * 1.6f * y 
					                           + posy, Screen.width * 0.17f, Screen.width * 0.17f * 1.6f), mapsT [i]);
						if (GUI.Button (new Rect (0 + Screen.width * 0.2f * x, Screen.width * 0.025f + Screen.width * 0.2f * 1.6f * y 
							+ posy, Screen.width * 0.2f, Screen.width * 0.2f * 1.6f), maps [i])) {
							PlayerPrefs.SetString ("SelectedMap", maps [i]);
							PlayerPrefs.SetString ("SelectedValues", values [i]);
							GetComponent<PutoMenu> ().MenuState = PutoMenu.MenuStates.idle;
							this.enabled = false;
							GameObject.Find ("Generator").GetComponent<MapFromText>().GeneradorState = MapFromText.GeneradorStates.crear;
							GameObject.Find ("Generator").GetComponent<MapFromText>().SyncStart();
							//Application.LoadLevel ("PutoMapGenerator");
						}
						x++;
						if (x > 3) {
							x = 0;
							y++;
						}
					}
				}
			if (noprofile) {
				GUILayout.Box("Error. No existe un perfil asignado a la sesion. \nReinicie la aplicacion");
			} else {
				if (PlayerPrefs.GetInt ("MapCount") < 9) {
					if (GUI.Button (new Rect (0 + Screen.width * 0.2f * x, Screen.width * 0.025f + Screen.width * 0.2f * 1.6f * y 
						+ posy, Screen.width * 0.2f, Screen.width * 0.2f * 1.6f), "Nuevo")) {
						PlayerPrefs.SetInt ("EditIDs", PlayerPrefs.GetInt ("EditIDs") + 1);
						PlayerPrefs.SetString ("SelectedMap", "Map" + PlayerPrefs.GetInt ("EditIDs").ToString ());
						PlayerPrefs.SetString ("SelectedValues", "");
						GetComponent<PutoMenu> ().MenuState = PutoMenu.MenuStates.idle;
						this.enabled = false;
						GameObject.Find ("Generator").GetComponent<MapFromText>().GeneradorState = MapFromText.GeneradorStates.editar;
						GetComponent<PutoMenu> ().MenuState = PutoMenu.MenuStates.editing;
						GameObject.Find ("Generator").GetComponent<MapFromText>().SyncStart();
						//Application.LoadLevel ("PutoMapGenerator");
					}
				}
			}
			GUILayout.EndArea ();
		} else {
			GUI.Box(new Rect(0,0,Screen.width,Screen.height), "Sincronizando");
		}
	}

	public IEnumerator SendMaps () {
		for (int i = 0; i < 2; i++) {
			Debug.Log ("SENDING: " + PlayerPrefs.GetString ("maps"));
			WWWForm form = new WWWForm ();
			form.AddField ("mode", "setMaps");
			form.AddField ("maps", PlayerPrefs.GetString ("maps"));
			form.AddField ("identification", PlayerPrefs.GetString ("identification"));
			byte[] data = form.data;

			WWW download = new WWW (PlayerPrefs.GetString ("URL"), data);
			yield return download;
			Debug.Log (download.text);
			yield return null;
		}
		StartCoroutine ("UpdateData");
	}

	void GUIUpdate () {
		string str = PlayerPrefs.GetString("maps");
		if (str != "" && str != null && str != " ") {
			string[] strs = str.Split (";".ToCharArray ());
			int a = 0;
			int b = 0;
			maps = new string[strs.Length];
			values = new string[strs.Length];
			PlayerPrefs.SetInt("MapCount", strs.Length);
			foreach (string s in strs) {
				maps [a] = s.Split (":".ToCharArray ()) [0];
				a++;
				values [b] = s.Split (":".ToCharArray ()) [1];
				b++;
			}
			mapsT = new Texture2D[maps.Length];
			for (int i = 0; i < maps.Length; i++) {
				Texture2D tx = MapImage (values [i]);
				tx.filterMode = FilterMode.Point;
				mapsT [i] = tx;
			}
		} else {
			mapsT[0] = null;
		}
	}

	IEnumerator UpdateData () {
		syncing = true;
		//offline loading
		GUIUpdate ();

		//online syncing
		WWWForm form = new WWWForm ();
		form.AddField("mode", "selectMaps");
		form.AddField("identification", PlayerPrefs.GetString("identification"));

		byte[] bytes = form.data;
		WWW download = new WWW (PlayerPrefs.GetString ("URL"), bytes);
		yield return download;

		string str = "";
		if (download.error == null || download.error == "") {
			Debug.Log ("RECEIVING: " + download.text);
			str = download.text.Substring (0, download.text.Length - 1);
			PlayerPrefs.SetString("maps", str);
			GUIUpdate ();
		} else {
			str = PlayerPrefs.GetString("maps");
		}
		syncing = false;
		StartCoroutine("PlayerExists");
	}

	IEnumerator PlayerExists () {
		WWWForm form = new WWWForm ();
		form.AddField("mode", "identification");
		form.AddField("identification", PlayerPrefs.GetString("identification"));
		
		byte[] bytes = form.data;
		WWW download = new WWW (PlayerPrefs.GetString ("URL"), bytes);
		yield return download;

		if (download.text == " ") {
			noprofile = true;
		} else {
			noprofile = false;
		}
	}

	Texture2D MapImage (string INvalues) {
		string[] strs = INvalues.Split (",".ToCharArray ());
		int i = 0;
		objs = new string[strs.Length];
		types = new string[strs.Length];
		foreach (string s in strs) {
			objs[i] = s.Split(".".ToCharArray())[0];
			types[i] = s.Split(".".ToCharArray())[1];
			i++;
		}
		int x = 0;
		int y = 0;
		int a = 0;
		Color[] cols = new Color[5*8];
		foreach (string s in objs) {
			switch (s){
			case "0":
				cols[a] = Color.black;
				break;
			case "1":
				cols[a] = Color.white;
				break;
			case "2":
				cols[a] = Color.blue;
				break;
			case "3":
				cols[a] = Color.yellow;
				break;
			case "4":
				cols[a] = Color.red;
				break;
			case "5":
				cols[a] = Color.green;
				break;
			case "6":
				cols[a] = Color.cyan;
				break;
			default:
				Debug.Log ("'" + s + "' no reconocido");
				break;
			}
			x++;
			a++;
			if (x > 4){
				x = 0;
				y++;
			}
		}
		Texture2D tx = new Texture2D (5, 8);
		tx.SetPixels (cols);
		tx.Apply ();
		return tx;
	}

	void Update () {
		if (Input.touchCount == 0) {
			storedMouse = Vector3.zero;
		} else {
			if (storedMouse == Vector3.zero) {
				storedMouse = Input.mousePosition;
			} else {
				posy -= Input.mousePosition.y - storedMouse.y;
				storedMouse = Input.mousePosition;
			}
		}
		posy = Mathf.Clamp(posy, -Screen.height*0.14f*(int)(maps.Length/3f), 0);
	}
}
