using UnityEngine;
using System.Collections;

public class WWWtexture : MonoBehaviour {
	string text, error;
	string mode, submode;
	string[] names = new string[0];
	int[] scores = new int[0];
	int score = 10;

	IEnumerator Start () {
		WWWForm form = new WWWForm ();
		form.AddField ("mode", "identification");
		form.AddField ("identification", PlayerPrefs.GetString("identification"));
		byte[] rawData = form.data;
		
		WWW download = new WWW (PlayerPrefs.GetString("URL"), rawData);
		yield return download;
		if (download.text == " " || download.text == null) {
			Debug.Log ("no");
			mode = "insert";
			StartCoroutine("MySQL");
		}
		mode = "select";
		submode = "top";
		StartCoroutine("MySQL");
		yield return null;
	}

	void OnGUI () {
		GUILayout.Window (1, new Rect (0, 0, Screen.width, Screen.height), Window, "HIGH SCORES");	
	}

	void Window (int id){
		if (text != null && text != "" && text != " ") {
			if (!text.Contains (":") && !text.Contains (";")) {
				GUILayout.Box (text);
			} else if (names.Length != 0) {
				for (int i = 0; i < names.Length; i++) {
					GUILayout.BeginHorizontal ();
					GUILayout.Box (names [i]);
					GUILayout.Box (scores [i].ToString ());
					GUILayout.EndHorizontal ();
				}
			}
		}
		GUILayout.Label ("errors: " + error);
		score = int.Parse(GUILayout.TextArea (score.ToString()));
		PlayerPrefs.SetString ("name", GUILayout.TextArea (PlayerPrefs.GetString ("name")));
		if (GUILayout.Button ("select")) {
			mode = "select";
			submode = "top";
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
		form.AddField ("identification", PlayerPrefs.GetString("identification"));
		byte[] rawData = form.data;
		
		WWW download = new WWW (PlayerPrefs.GetString("URL"), rawData);
		yield return download;
		if (download.text == " ") {
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
		if (submode != "") {
			form.AddField ("submode", submode);
		}
		form.AddField ("name", PlayerPrefs.GetString("name"));
		form.AddField ("score", score);
		form.AddField ("identification", PlayerPrefs.GetString("identification"));
		byte[] rawData = form.data;

		WWW download = new WWW (PlayerPrefs.GetString("URL"), rawData);
		yield return download;
		error = download.error;
		text = "please wait...";
		text = download.text;
		Formatting ();
		yield return null;
	}

	void Formatting () {
		if (text.Remove (text.Length - 1).EndsWith (";") && text.Contains (":")) {
			text = text.Remove (text.Length - 2);
			string[] strs = text.Split (";".ToCharArray ());
			int a = 0;
			names = new string[strs.Length];
			scores = new int[strs.Length];
			foreach (string s in strs) {
				string[] subs = s.Split (":".ToCharArray ());
				names [a] = subs [0];
				scores [a] = int.Parse (subs [1]);
				a++;
			}
		}
	}
}
