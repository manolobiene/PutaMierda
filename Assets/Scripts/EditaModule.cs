using UnityEngine;
using System.Collections;

public class EditaModule : MonoBehaviour {
	public int block, type;

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
		case 6:
			rend.material.color = Color.cyan;
			break;
		default:
			break;
		}
	}

	void OnGUI () {
		Vector3 gpos = Camera.main.WorldToScreenPoint (transform.position);
		switch (block) {
		case 0:
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			break;
		case 5:
			break;
		case 6:
			GUI.Box (new Rect (gpos.x - Screen.width * 0.08f/2, Screen.height - gpos.y - Screen.height * 0.05f/2
			                   , Screen.width * 0.08f, Screen.height * 0.05f), type.ToString());
			break;
		default:
			break;
		}
	}

	void OnMouseDown () {
		/*block++;
		if (block > 5) {
			block = 0;
		}*/
		if (block == Camera.main.GetComponent<OnEditing> ().block) {
			switch (block) {
			case 0:
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			case 5:
				break;
			case 6:
				type++;
				if (type >= 4) {
					type = 0;
				}
				break;
			default:
				break;
			}
		} else {
			type = 0;
			block = Camera.main.GetComponent<OnEditing> ().block;
			UpdateColor ();
		}
	}
}
