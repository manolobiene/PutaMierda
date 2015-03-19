using UnityEngine;
using System.Collections;

public class MenuNube : MonoBehaviour {
	enum states {start, idle};
	float[] heights = new float[7];
	states state;

	void Start () {
		StartCoroutine ("SetHeights");
	}

	void Update () {
		switch (state) {
		case states.start:
			if (transform.localPosition.y > -8f) {
				transform.localPosition -= Vector3.down * (-8f - transform.localPosition.y)* Time.deltaTime;
			}else{
				state = states.idle;
			}
			break;
		case states.idle:
			break;
		}
		if (!Input.GetMouseButton (0)) {
			for (int i = 0; i < transform.childCount; i++) {
				Vector3 p = transform.GetChild (i).transform.position;
				transform.GetChild (i).transform.position = Vector3.MoveTowards (p, new Vector3 (p.x, heights [i], 0), .2f*Time.deltaTime);
			}
		} else {
			for (int i = 0; i < transform.childCount; i++) {
				float x = Camera.main.WorldToScreenPoint(transform.GetChild(i).transform.position).x;
				float y = 200-Mathf.Abs(Mathf.Clamp((x-Input.mousePosition.x), -200f, 200f));
				heights [i] = Mathf.Clamp(0.5f-0.6f*y/200, -0.4f, 0.4f);
				Vector3 p = transform.GetChild (i).transform.position;
				transform.GetChild (i).transform.position = Vector3.Lerp (p, new Vector3 (p.x, heights [i], 0), 0.05f);
			}
		}
	}

	IEnumerator SetHeights () {
		float delay = 1;
		while (true) {
			if (!Input.GetMouseButton(0)){
				delay = Random.Range(.5f,1f);
				for (int i = 0; i < transform.childCount; i++) {
					heights [i] = Random.Range (-0.1f, 0.6f);
				}
			}
			yield return new WaitForSeconds (delay);
		}
	}
}
