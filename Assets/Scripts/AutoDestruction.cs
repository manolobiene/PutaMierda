using UnityEngine;
using System.Collections;

public class AutoDestruction : MonoBehaviour {
	public float count;
	public bool onPlaying = false;

	void Update () {
		if (onPlaying) {
			if (Camera.main.GetComponent<PutoMenu>().MenuState != PutoMenu.MenuStates.selectMode){
				Destroy(gameObject);
			}
		}
		count -= 1 * Time.deltaTime;
		if (count < 0) {
			Destroy(gameObject);
		}
	}
}
