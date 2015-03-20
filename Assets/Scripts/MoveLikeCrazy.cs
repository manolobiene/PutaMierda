using UnityEngine;
using System.Collections;

public class MoveLikeCrazy : MonoBehaviour {
	Vector3 To;

	void Start () {
		StartCoroutine ("UpdateGoTo");
		Camera.main.GetComponents<AudioSource>()[0].Stop();
		Camera.main.GetComponents<AudioSource>()[6].Play();
	}

	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, To, 0.1f);
	}

	IEnumerator UpdateGoTo () {
		while (true) {
			int x = (int)Random.Range (0.01f, Screen.width + .99f);
			int y = (int)Random.Range (0.01f, Screen.height + .99f);
			float z = transform.position.z;
			Vector3 Sp = new Vector3(x,y,0);
			Vector3 Wp = Camera.main.ScreenToWorldPoint(Sp);
			Wp = new Vector3(Wp.x,Wp.y,z);
			To = Wp;
			yield return new WaitForSeconds(Random.Range (.2f,.5f));
		}
	}
}
