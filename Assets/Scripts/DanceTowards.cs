using UnityEngine;
using System.Collections;

public class DanceTowards : MonoBehaviour {
	public Vector3 direction;
	public int velocity;

	void Update () {
		transform.position += direction * velocity * Time.deltaTime;
	}
}
