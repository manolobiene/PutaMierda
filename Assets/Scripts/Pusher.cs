using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {
	public enum Faces {U=0,D=1,L=2,R=3};
	public Faces facing;

	void OnGUI () {
		switch (facing) {
		case Faces.U:
			break;
		case Faces.D:
			break;
		case Faces.L:
			break;
		case Faces.R:
			break;
		}
		Vector3 gpos = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Box (new Rect (gpos.x - Screen.width * 0.08f/2, Screen.height - gpos.y - Screen.height * 0.05f/2
		                   , Screen.width * 0.08f, Screen.height * 0.05f), facing.ToString());
	}

	void OnMouseDrag () {
		float size = GetComponent<SpriteRenderer> ().bounds.size.x;
		switch(facing){
		case Faces.U:
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + size/2+0.001f), Vector2.up);
			if (hit.collider != null){
				Debug.Log (hit.collider);
				if (hit.collider.gameObject.GetComponent<Rigidbody2D>()){
					hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up*0.6f, ForceMode2D.Impulse);
					hit.collider.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
				}
			}
			break;
		case Faces.D:
			RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - size/2-0.001f), -Vector2.up);
			if (hit1.collider != null){
				if (hit1.collider.gameObject.GetComponent<Rigidbody2D>()){
					hit1.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-Vector3.up*0.6f, ForceMode2D.Impulse);
					hit1.collider.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
				}
			}
			break;
		case Faces.L:
			RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - size/2-0.001f, transform.position.y), -Vector2.right);
			if (hit2.collider != null){
				if (hit2.collider.gameObject.GetComponent<Rigidbody2D>()){
					hit2.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-Vector3.right*0.6f, ForceMode2D.Impulse);
					hit2.collider.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
				}
			}
			break;
		case Faces.R:
			RaycastHit2D hit3 = Physics2D.Raycast(new Vector2(transform.position.x + size/2+0.001f, transform.position.y), Vector2.right);
			if (hit3.collider != null){
				if (hit3.collider.gameObject.GetComponent<Rigidbody2D>()){
					hit3.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right*0.6f, ForceMode2D.Impulse);
					hit3.collider.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
				}
			}
			break;
		}
	}
}
