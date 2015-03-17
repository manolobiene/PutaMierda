using UnityEngine;
using System.Collections;

public class PutoPervive : MonoBehaviour {

	void Start () {
		DontDestroyOnLoad (gameObject);
		StartCoroutine ("CheckNetworkAvailability");
	}

	IEnumerator CheckNetworkAvailability () {
		while (true) {
			WWWForm form = new WWWForm ();
			form.AddField ("mode", "hola");
			byte[] bytes = form.data;
			WWW www = new WWW (PlayerPrefs.GetString ("URL"), bytes);
			yield return www;
			if (www.error != null && www.error != "") {
				PlayerPrefs.SetString ("NetError", www.error);
				//Application.LoadLevel ("UnableToConnect");
				GetComponent<PutoMenu>().disconnectAlert = true;
			}else{
				GetComponent<PutoMenu>().disconnectAlert = false;
			}
			yield return new WaitForSeconds(10);
		}
	}
}
