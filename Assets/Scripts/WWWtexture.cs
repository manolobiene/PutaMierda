using UnityEngine;
using System.Collections;

public class WWWtexture : MonoBehaviour {
	string text, error;
	string mode;
	int score = 10;

	IEnumerator Start () {
		WWWForm form = new WWWForm ();
		form.AddField ("mode", "identification");
		form.AddField ("identification", PlayerPrefs.GetInt("identification"));
		byte[] rawData = form.data;
		
		WWW download = new WWW (PlayerPrefs.GetString("URL"), rawData);
		yield return download;
		if (download.text == " " || download.text == null) {
			Debug.Log ("no");
			mode = "insert";
			StartCoroutine("MySQL");
		}
		mode = "select";
		StartCoroutine("MySQL");
		yield return null;
	}

	void OnGUI () {
		GUILayout.Window (1, new Rect (0, 0, Screen.width, Screen.height), Window, "Functions");	
	}

	void Window (int id){
		GUILayout.Label (text);
		GUILayout.Label ("errors: " + error);
		score = int.Parse(GUILayout.TextArea (score.ToString()));
		PlayerPrefs.SetString ("name", GUILayout.TextArea (PlayerPrefs.GetString ("name")));
		if (GUILayout.Button ("select")) {
			mode = "select";
			StartCoroutine("MySQL");
		}
		if (GUILayout.Button ("update")) {
			StartCoroutine("Upd");
		}
		if (GUILayout.Button ("delete")) {
			mode = "delete";
			StartCoroutine("MySQL");
		}
	}

	IEnumerator Upd () {
		WWWForm form = new WWWForm ();
		form.AddField ("mode", "identification");
		form.AddField ("identification", PlayerPrefs.GetInt("identification"));
		byte[] rawData = form.data;
		
		WWW download = new WWW (PlayerPrefs.GetString("URL"), rawData);
		yield return download;
		if (download.text == " " || download.text == null) {
			mode = "insert";
			StartCoroutine ("MySQL");
		} else {
			mode = "update";
			StartCoroutine ("MySQL");
		}
		yield return null;
	}

	IEnumerator MySQL () {
		WWWForm form = new WWWForm ();
		form.AddField ("mode", mode);
		form.AddField ("name", PlayerPrefs.GetString("name"));
		form.AddField ("score", score);
		form.AddField ("identification", PlayerPrefs.GetInt("identification"));
		byte[] rawData = form.data;

		WWW download = new WWW (PlayerPrefs.GetString("URL"), rawData);
		yield return download;
		error = download.error;
		text = "please wait...";
		text = download.text;
		yield return null;
	}
}
