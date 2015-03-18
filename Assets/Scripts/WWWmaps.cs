using UnityEngine;
using System.Collections;

public class WWWmaps : MonoBehaviour {
	string[] maps = new string[1];
	string[] values = new string[1];
	string[] objs = new string[1];
	Texture2D[] mapsT = new Texture2D[16];
	float posy;
	Vector3 storedMouse;

	//notification
	bool noprofile = false;

	void Start () {
		StartCoroutine("UpdateData");
	}

	void OnGUI () {
		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height*0.7f));
		int x = 0;
		int y = 0;
		if (mapsT [0] != null) {
			for (int i = 0; i < maps.Length; i++) {
				GUI.DrawTexture (new Rect (Screen.width * 0.02f + Screen.width * 0.333333f * x, Screen.width * 0.04f * 1.6f 
					+ Screen.width * 0.333333f * 1.6f * y + posy, Screen.width * 0.29f, Screen.width * 0.29f * 1.6f), mapsT [i]);
				if (GUI.Button (new Rect (0 + Screen.width * 0.333333f * x, Screen.width * 0.02f * 1.6f + Screen.width * 0.333333f * 1.6f * y 
					+ posy, Screen.width * 0.333333f, Screen.width * 0.333333f * 1.6f), maps [i])) {
					PlayerPrefs.SetString ("SelectedMap", maps [i]);
					PlayerPrefs.SetString ("SelectedValues", values [i]);
					GetComponent<PutoMenu> ().MenuState = PutoMenu.MenuStates.idle;
					this.enabled = false;
					Application.LoadLevel ("PutoMapGenerator");
				}
				x++;
				if (x > 2) {
					x = 0;
					y++;
				}
			}
		}
		if (noprofile) {
			GUILayout.Box("Error. No existe un perfil asignado a la sesion. \nReinicie la aplicacion");
		} else {
			if (PlayerPrefs.GetInt ("MapCount") < 9) {
				if (GUI.Button (new Rect (0 + Screen.width * 0.333333f * x, Screen.width * 0.02f * 1.6f + Screen.width * 0.333333f * 1.6f * y 
					+ posy, Screen.width * 0.333333f, Screen.width * 0.333333f * 1.6f), "Nuevo")) {
					PlayerPrefs.SetInt ("EditIDs", PlayerPrefs.GetInt ("EditIDs") + 1);
					PlayerPrefs.SetString ("SelectedMap", "Map" + PlayerPrefs.GetInt ("EditIDs").ToString ());
					PlayerPrefs.SetString ("SelectedValues", "");
					GetComponent<PutoMenu> ().MenuState = PutoMenu.MenuStates.idle;
					this.enabled = false;
					Application.LoadLevel ("PutoMapGenerator");
				}
			}
		}
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (0, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f));
		//PlayerPrefs.SetString ("maps", GUILayout.TextArea (PlayerPrefs.GetString ("maps")));
		if (GUILayout.Button ("Menu", GUILayout.Height(Screen.height*0.15f * 0.8f))) {
			GetComponent<PutoMenu>().MenuState = PutoMenu.MenuStates.selectMode;
		}
		if (GUILayout.Button ("EmptyPREFS", GUILayout.Height(Screen.height*0.15f * 0.2f))) {
			PlayerPrefs.DeleteAll();
			Destroy(gameObject);
			Application.LoadLevel(0);
		}
		GUILayout.EndArea ();
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

	IEnumerator UpdateData () {
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
		} else {
			str = PlayerPrefs.GetString("maps");
		}
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
		objs = INvalues.Split (",".ToCharArray ());
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
			GUI.enabled = true;
		} else {
			GUI.enabled = false;
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
