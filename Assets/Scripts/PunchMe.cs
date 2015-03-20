using UnityEngine;
using System.Collections;

public class PunchMe : MonoBehaviour {
	public GameObject DestParticles;

	void OnMouseDown () {
		GameObject obj = (GameObject)Instantiate (DestParticles, transform.position, Quaternion.identity);
		obj.GetComponent<ParticleSystemRenderer> ().material = GetComponent<SpriteRenderer> ().material;
		if (tag == "Minion") {
			if (GameObject.Find ("MinionG")){
				GameObject.Find ("MinionG").GetComponent<MinionGenerator>().killed++;
			}
		}
		Destroy (gameObject);
	}
}
