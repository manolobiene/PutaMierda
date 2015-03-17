using UnityEngine;
using System.Collections;

public class PutoScript : MonoBehaviour {
	public string URL;
	bool disconnected = false;

	void Start () {
		PlayerPrefs.SetString ("URL", URL);
		if (PlayerPrefs.GetInt ("identification") == null || PlayerPrefs.GetInt ("identification") == 0) {
			string date = System.DateTime.Now.Year.ToString().Remove(0,2) + System.DateTime.Now.Month.ToString() 
				+ System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() 
					+ System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString();
			PlayerPrefs.SetInt ("identification", int.Parse(date));
			Debug.Log (PlayerPrefs.GetInt ("identification"));
		}
		if (PlayerPrefs.GetString ("name") == null || PlayerPrefs.GetString ("name") == "" || PlayerPrefs.GetString ("name") == " ") {
			PlayerPrefs.SetString("name", "Player");
		}
		StartCoroutine ("CheckNetworkAvailability");
	}

	IEnumerator CheckNetworkAvailability () {
		WWWForm form = new WWWForm();
		form.AddField("mode", "hola");
		byte[] bytes = form.data;
		WWW www = new WWW (PlayerPrefs.GetString("URL"), bytes);
		yield return www;
		if (www.error != null && www.error != "") {
			PlayerPrefs.SetString ("NetError", www.error);
			//Application.LoadLevel ("UnableToConnect");
			disconnected = true;
		} else {
			Application.LoadLevel ("Menu");
		}
	}

	void OnGUI () {
		if (!disconnected) {
			GUI.Box (new Rect (Screen.width / 2 - Screen.width * 0.4f, Screen.height / 2 - Screen.height * 0.25f, Screen.width * 0.8f, Screen.height * 0.5f)
		         , "Conectando");
		} else {
			GUI.Box (new Rect (Screen.width / 2 - Screen.width * 0.4f, Screen.height / 2 - Screen.height * 0.25f
			                   , Screen.width * 0.8f, Screen.height * 0.5f)
			         , "Fallo al intentar conectar. \nCompruebe su conexion \na internet y vuelva a intentarlo. " +
			         	"\nError: " + PlayerPrefs.GetString("NetError"));
			if (GUI.Button(new Rect (Screen.width / 2 - Screen.width * 0.4f, Screen.height - Screen.height*0.2f
			                         , Screen.width * 0.8f, Screen.height * 0.2f), "Continuar de todas formas")){
				Application.LoadLevel("Menu");
			}
		}
	}
}
