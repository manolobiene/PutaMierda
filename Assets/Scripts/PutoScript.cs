using UnityEngine;
using System.Collections;

public class PutoScript : MonoBehaviour {
	enum States {identification, name, connection, check, success, created};
	States state;
	public string URL;
	bool disconnected = false;

	void Start () {
		state = States.identification;
		PlayerPrefs.SetString ("URL", URL);
		if (PlayerPrefs.GetString ("identification") == null || PlayerPrefs.GetString ("identification") == "") {
			string date = System.DateTime.Now.Year.ToString().Remove(0,2) + System.DateTime.Now.Month.ToString() 
				+ System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Millisecond.ToString();
			PlayerPrefs.SetString ("identification", date);
			Debug.Log (PlayerPrefs.GetString ("identification"));
		}
		state = States.name;
		if (PlayerPrefs.GetString ("name") == null || PlayerPrefs.GetString ("name") == "" || PlayerPrefs.GetString ("name") == " ") {
			PlayerPrefs.SetString("name", "Player");
		}
		Debug.Log (PlayerPrefs.GetString ("name"));
		StartCoroutine ("CheckNetworkAvailability");
	}

	IEnumerator CheckNetworkAvailability () {
		state = States.connection;
		WWWForm form = new WWWForm();
		form.AddField("mode", "identification");
		form.AddField("identification", PlayerPrefs.GetString("identification"));
		byte[] bytes = form.data;
		WWW www = new WWW (PlayerPrefs.GetString("URL"), bytes);
		yield return www;
		if (www.error != null && www.error != "") {
			PlayerPrefs.SetString ("NetError", www.error);
			//Application.LoadLevel ("UnableToConnect");
			disconnected = true;
		} else {
			if (www.text == "" || www.text == " " || www.text == null){
				PlayerPrefs.SetString ("NetText", www.text);
				state = States.check;
				WWWForm form1 = new WWWForm();
				form1.AddField("mode", "insert");
				form1.AddField("score", 10);
				form1.AddField("name", PlayerPrefs.GetString("name"));
				form1.AddField("identification", PlayerPrefs.GetString("identification"));
				bytes = form1.data;
				WWW www1 = new WWW (PlayerPrefs.GetString("URL"), bytes);
				yield return www1;
				if (www1.error != null && www1.error != "") {
					PlayerPrefs.SetString ("NetError", www1.error);
					Application.LoadLevel ("UnableToConnect");
				} else {
					state = States.created;
					Application.LoadLevel ("Menu");
				}
			}else{
				state = States.success;
				Application.LoadLevel ("Menu");
			}
		}
	}

	void OnGUI () {
		if (!disconnected) {
			GUI.Box (new Rect (Screen.width / 2 - Screen.width * 0.4f, Screen.height / 2 - Screen.height * 0.25f, Screen.width * 0.8f, Screen.height * 0.5f)
			         , "Conectando \n" + state.ToString() + "\n" + PlayerPrefs.GetString("NetText") + "\n" + PlayerPrefs.GetString ("name") + "\nIDENTIFICATION: " + PlayerPrefs.GetString("identification"));
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
