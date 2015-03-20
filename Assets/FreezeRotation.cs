using UnityEngine;
using System.Collections;

public class FreezeRotation : MonoBehaviour {
	
	void Start () {
		GetComponent<Rigidbody2D> ().fixedAngle = true;
	}
}
