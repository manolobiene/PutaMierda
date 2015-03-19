using UnityEngine;
using System.Collections;

public class InteractiveButton : MonoBehaviour {
	public enum states {idle, transitionF, transitionB};
	public states state;
	float target = 1.1f;
	Vector3 oPos, mPos;
	Vector3 desplazamiento;
	public Vector3 transitionPos;
	float counter = 0;
	public GameObject action;
	public string effect;

	void Start () {
		oPos = transform.localPosition;
	}

	void Update () {
		if (state == states.idle) {
			if (transform.localScale.x > 1.05f) {
				target = 0.9f;
			}
			if (transform.localScale.x < 0.95f) {
				target = 1.1f;
			}
			transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (target, target, 1), 0.1f*Time.deltaTime);
			Vector3 p = transform.localPosition;
			float newx = Mathf.Clamp (p.x, oPos.x - 0.5f, oPos.x + 0.5f);
			float newy = Mathf.Clamp (p.y, oPos.y - 1f, oPos.x + 1f);
			transform.localPosition = new Vector3 (newx, newy, oPos.z);
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, oPos, 2f*Time.deltaTime);
		}
		if (state == states.transitionB) {
			transform.localPosition = Vector3.MoveTowards (transform.localPosition, oPos, 10f*Time.deltaTime);
			Vector3 cS = transform.GetChild(0).transform.localScale;
			Vector3 to = new Vector3 (.001f,.001f,1);
			transform.GetChild(0).transform.localScale = Vector3.MoveTowards(cS, to, 15f*Time.deltaTime);
			Vector3 pos = transform.localPosition;
			if (Mathf.Abs(pos.x - oPos.x) < .2f && Mathf.Abs(pos.y - oPos.y) < .2f){
				GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
				transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
				state = states.idle;
			}
		}
		if (state == states.transitionF) {
			transform.localPosition = Vector3.Slerp (transform.localPosition, transitionPos, 3f*Time.deltaTime);
			Vector3 cS = transform.GetChild(0).transform.localScale;
			Vector3 to = new Vector3 (4,4,1);
			transform.GetChild(0).transform.localScale = Vector3.MoveTowards(cS, to, 5f*Time.deltaTime);
		}
	}

	void OnMouseDown () {
		if (Camera.main.GetComponent<PutoMenu> ().MenuState == PutoMenu.MenuStates.selectMode) {
			counter = 0;
			desplazamiento = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		}
	}
	void OnMouseDrag () {
		if (Camera.main.GetComponent<PutoMenu> ().MenuState == PutoMenu.MenuStates.selectMode) {
			counter += 1 * Time.deltaTime;
			transform.localScale = Vector3.Slerp (transform.localScale, new Vector3 (1.2f, 1.2f, 1), 0.1f);
			transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition) - desplazamiento;
			mPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}
	}
	void OnMouseUp () {
		if (Camera.main.GetComponent<PutoMenu> ().MenuState == PutoMenu.MenuStates.selectMode) {
			if (counter < 0.2f) {
				if (action.name == "Main Camera") {
					if (effect == "GoToEditor") {
						action.GetComponent<PutoMenu> ().MenuState = PutoMenu.MenuStates.editor;
					}
				}
				if (action.name == "Empty") {
					if (effect == "ActivateTable") {
						action.GetComponent<WWWtexture> ().enabled = true;
						action.GetComponent<WWWtexture> ().StartCoroutine ("SyncStart");
						Camera.main.GetComponent<PutoMenu> ().MenuState = PutoMenu.MenuStates.tabla;
					}
				}
				GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
				transform.GetChild (0).GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
				transform.GetChild (0).transform.position = new Vector3 (mPos.x, mPos.y, oPos.z);
				state = states.transitionF;
			}
		}
	}
}
