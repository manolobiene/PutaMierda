using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {
	public GameObject target;
	public bool mouse = false;

	void Update () {
		if (!mouse) {
			transform.position = target.transform.position;
		} else {
			Vector3 mP = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = mP;
		}
	}
}
