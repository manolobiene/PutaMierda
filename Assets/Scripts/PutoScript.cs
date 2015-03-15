using UnityEngine;
using System.Collections;

public class PutoScript : MonoBehaviour {
	
	void Start () {
		if (PlayerPrefs.GetInt ("identification") == null || PlayerPrefs.GetInt ("identification") == 0) {
			string date = System.DateTime.Now.Day.ToString() + System.DateTime.Now.Hour.ToString() 
				+ System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Millisecond.ToString() 
					+ ((int)Random.Range(0,9.99f)).ToString();
			PlayerPrefs.SetInt ("identification", int.Parse(date));
			Debug.Log (PlayerPrefs.GetInt ("identification"));
		}
		Application.LoadLevel(PlayerPrefs.GetInt ("JodidaComida")+2);
	}
}
