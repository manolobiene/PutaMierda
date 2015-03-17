using UnityEngine;
using System.Collections;

public class MapFromText : MonoBehaviour {
	public enum GeneradorStates {crear = 0, editar = 1};
	public GeneradorStates GeneradorState;
	public GameObject Wall, Floor, Weedy, GloriaBendita, Gloria_Pasa, PutoNombrador;
	public GameObject Edita;
	// Update is called once per frame
	void Start () {
		GenMap ();
	}

	public void GenMap () {
		string INvalues = PlayerPrefs.GetString ("SelectedValues");
		float size = Wall.GetComponent<SpriteRenderer> ().bounds.size.x;
		int x = 0;
		int y = 0;

		GameObject parent = GameObject.CreatePrimitive (PrimitiveType.Cube);
		parent.transform.position = new Vector3 (5 * size / 2 - size / 2, 8 * size / 2 - size / 2, 0);
		parent.name = "map";
		Destroy (parent.GetComponent<MeshRenderer> ());
		
		GameObject walls = GameObject.CreatePrimitive (PrimitiveType.Cube);
		walls.transform.position = parent.transform.position;
		walls.transform.parent = parent.transform;
		walls.name = "walls";
		Destroy (walls.GetComponent<MeshRenderer> ());
		
		GameObject objects = GameObject.CreatePrimitive (PrimitiveType.Cube);
		objects.transform.position = parent.transform.position;
		objects.transform.parent = parent.transform;
		objects.name = "objects";
		Destroy (objects.GetComponent<MeshRenderer> ());

		if (INvalues != "") {
			string[] objs = INvalues.Split (",".ToCharArray ());

			foreach (string s in objs) {
				if (GeneradorState == GeneradorStates.crear) {
					switch (s) {
					case "0":
						GameObject obj = Instantiate (Wall);
						obj.transform.position = new Vector3 (x * size, y * size, 0);
						obj.transform.parent = walls.transform;
						break;
					case "1":
						GameObject obj1 = Instantiate (Floor);
						obj1.transform.position = new Vector3 (x * size, y * size, 0);
						obj1.transform.parent = walls.transform;
						break;
					case "2":
						GameObject obj2 = Instantiate (Weedy);
						obj2.transform.position = new Vector3 (x * size, y * size, 0);
						obj2.transform.parent = objects.transform;
						obj2.name = "PutaHierba";
						obj2 = Instantiate (Floor);
						obj2.transform.position = new Vector3 (x * size, y * size, 0);
						obj2.transform.parent = walls.transform;
						break;
					case "3":
						GameObject obj4 = Instantiate (Gloria_Pasa);
						obj4.transform.position = new Vector3 (x * size, y * size, 0);
						obj4.name = "Gloria_Pasa";
						obj4.transform.parent = objects.transform;
						break;
					case "4":
						GameObject obj3 = Instantiate (GloriaBendita);
						obj3.transform.position = new Vector3 (x * size, y * size, 0);
						obj3.name = "Gloria";
						obj3.transform.parent = objects.transform;
						break;
					case "5":
						GameObject obj5 = Instantiate (PutoNombrador);
						obj5.transform.position = new Vector3 (x * size, y * size, 0);
						obj5.GetComponent<SpriteRenderer> ().sortingOrder = -1;
						obj5.name = "PutoNombrador";
						obj5.transform.parent = objects.transform;
						break;
					default:
						Debug.Log ("'" + s + "' no reconocido");
						break;
					}
					x++;
					if (x > 4) {
						x = 0;
						y++;
					}
				}
				if (GeneradorState == GeneradorStates.editar) {
					GameObject obj = Instantiate (Edita);
					switch (s) {
					case "0":
						obj.GetComponent<EditaModule> ().block = 0;
						break;
					case "1":
						obj.GetComponent<EditaModule> ().block = 1;
						break;
					case "2":
						obj.GetComponent<EditaModule> ().block = 2;
						break;
					case "3":
						obj.GetComponent<EditaModule> ().block = 3;
						break;
					case "4":
						obj.GetComponent<EditaModule> ().block = 4;
						break;
					case "5":
						obj.GetComponent<EditaModule> ().block = 5;
						break;
					default:
						break;
					}
					obj.transform.position = new Vector3 (x * size, y * size, 0);
					obj.transform.parent = walls.transform;
					x++;
					if (x > 4) {
						x = 0;
						y++;
					}
				}
			}
		}
		//Fill blank space in editor canvas
		if (GeneradorState == GeneradorStates.editar){
			while (y < 8){
				GameObject obj = Instantiate(Edita);
				obj.GetComponent<EditaModule>().block = 0;
				obj.transform.position = new Vector3(x*size, y*size, 0);
				obj.transform.parent = walls.transform;
				x++;
				if (x > 4){
					x = 0;
					y++;
				}
			}
		}
		parent.transform.position = Vector3.zero;
	}
}
