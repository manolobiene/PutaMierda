using UnityEngine;
using System.Collections;

public class VictoryDance : MonoBehaviour {
	Vector3 To;

	void Start () {
		Camera.main.GetComponents<AudioSource>()[6].Stop();
		Destroy (GetComponent<MoveLikeCrazy> ());
		float sizex = GetComponent<SpriteRenderer> ().bounds.size.x;
		float sizey = GetComponent<SpriteRenderer> ().bounds.size.y;
		int x = (int)(Screen.width / 2 - sizex/2);
		int y = (int)(Screen.height / 2 - sizey/2);
		float z = transform.position.z;
		Vector3 Sp = new Vector3(x,y,0);
		Vector3 Wp = Camera.main.ScreenToWorldPoint(Sp);
		Wp = new Vector3(Wp.x,Wp.y,z);
		To = Wp;
	}

	void Update () {
		Vector3 dist = To - transform.position;
		if (dist.magnitude > 0.1f) {
			if (!Camera.main.GetComponents<AudioSource>()[4].isPlaying){
				Camera.main.GetComponents<AudioSource>()[0].Stop();
				Camera.main.GetComponents<AudioSource>()[4].Play();
			}
			transform.position = Vector3.Slerp (transform.position, To, 0.01f);
		} else {
			if (transform.localScale != new Vector3(0.01f,0.01f,1)){
				if (!Camera.main.GetComponents<AudioSource>()[5].isPlaying){
					Camera.main.GetComponents<AudioSource>()[0].Stop();
					Camera.main.GetComponents<AudioSource>()[4].Stop();
					Camera.main.GetComponents<AudioSource>()[5].Play();
				}
				transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.01f,0.01f,1), 0.005f);
				transform.rotation *= Quaternion.Euler(new Vector3(0,0,1));
			}else{
				if (!Camera.main.GetComponents<AudioSource>()[1].isPlaying){
					Camera.main.GetComponents<AudioSource>()[4].Stop();
					Camera.main.GetComponents<AudioSource>()[5].Stop();
					Camera.main.GetComponents<AudioSource>()[1].Play();
					Camera.main.GetComponents<AudioSource>()[3].Play();
				}
				Camera.main.GetComponent<PutoMenu> ().MenuState = PutoMenu.MenuStates.editor;
			}
		}
	}
}
