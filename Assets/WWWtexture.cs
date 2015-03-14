﻿using UnityEngine;
using System.Collections;

public class WWWtexture : MonoBehaviour {
	public string URL;
	string text;
	string mode = "select";
	string name = "gato";
	int score = 6969;

	void OnGUI () {
		GUILayout.Label (text);
		score = int.Parse(GUILayout.TextArea (score.ToString()));
		name = GUILayout.TextArea (name);
		if (GUILayout.Button ("insert")) {
			mode = "insert";
			StartCoroutine("MySQL");
		}
		if (GUILayout.Button ("select")) {
			mode = "select";
			StartCoroutine("MySQL");
		}
		if (GUILayout.Button ("update")) {
			mode = "update";
			StartCoroutine("MySQL");
		}
		if (GUILayout.Button ("delete")) {
			mode = "delete";
			StartCoroutine("MySQL");
		}
	}

	IEnumerator MySQL () {
		WWWForm form = new WWWForm ();
		form.AddField ("mode", mode);
		form.AddField ("name", name);
		form.AddField ("score", score);
		byte[] rawData = form.data;

		WWW download = new WWW (URL, rawData);
		while (!download.isDone) {
		}
		text = download.text;
		yield return null;
	}
}
