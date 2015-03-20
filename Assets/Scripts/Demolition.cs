using UnityEngine;
using System.Collections;

public class Demolition : MonoBehaviour {
	public GameObject DestParticles;
	float time = 5;

	void Start () {
		Destroy (GameObject.Find ("PutaHierba").GetComponent<PutoFood> ());
		GameObject.Find ("PutaHierba").AddComponent<MoveLikeCrazy> ();
		foreach (GameObject g in GameObject.FindObjectsOfType (typeof(GameObject))) {
			if (g.GetComponent<BoxCollider2D>()){
				if (g.layer != LayerMask.NameToLayer("Ignore Raycast")){
					if (g.name != "Main Camera" && g.name != "Generator" && g.name != "Empty" && g != gameObject){
						PunchMe PM = (PunchMe)g.AddComponent<PunchMe>();
						PM.DestParticles = DestParticles;
					}
				}
			}
		}
	}

	void Update () {
		time -= 1 * Time.deltaTime;
		if (time < 0) {
			GameObject.Find ("PutaHierba").AddComponent<VictoryDance> ();
			Destroy (this);
		}
	}
}
