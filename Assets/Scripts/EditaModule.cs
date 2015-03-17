using UnityEngine;
using System.Collections;

public class EditaModule : MonoBehaviour {
	public int block;

	void Start () {
		UpdateColor ();
	}

	void UpdateColor () {
		SpriteRenderer rend = GetComponent<SpriteRenderer> ();

		switch (block) {
		case 0:
			rend.material.color = Color.black;
			break;
		case 1:
			rend.material.color = Color.white;
			break;
		case 2:
			rend.material.color = Color.blue;
			break;
		case 3:
			rend.material.color = Color.yellow;
			break;
		case 4:
			rend.material.color = Color.red;
			break;
		case 5:
			rend.material.color = Color.green;
			break;

		default:
			break;
		}
	}

	void OnMouseDown () {
		block++;
		if (block > 5) {
			block = 0;
		}
		UpdateColor ();
	}
}
