using UnityEngine;
using System.Collections;

public class MouseSmash : MonoBehaviour {
	public enum smashModes {normal, destruction};
	public smashModes smashMode;
	public GameObject Destruction_smashParticles, Normal_smashParticles, DragParticles;
	
	void Update () {
		switch(smashMode){
		case smashModes.normal:
			if (Input.GetMouseButtonDown (0)) {
				Vector3 P = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Instantiate(Normal_smashParticles, P, Quaternion.identity);
			}
			break;
		case smashModes.destruction:
			if (Input.GetMouseButtonDown (0)) {
				Vector3 P = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Instantiate(Destruction_smashParticles, P, Quaternion.identity);
			}
			break;
		}
		if (Input.GetMouseButton(0)) {
			if (!GameObject.Find ("DragParticles")) {
				Vector3 P = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				GameObject inst = (GameObject)Instantiate (DragParticles, P, Quaternion.identity);
				inst.name = "DragParticles";
			}
		} else if (GameObject.Find ("DragParticles")) {
			Destroy(GameObject.Find ("DragParticles"));
		}
	}
}
