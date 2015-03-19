using UnityEngine;
using System.Collections;

public class MapFromText : MonoBehaviour {
	public enum GeneradorStates {crear = 0, editar = 1};
	public GeneradorStates GeneradorState;
	public GameObject Wall, Floor, Weedy, GloriaBendita, Gloria_Pasa, PutoNombrador, Pusher;
	public GameObject Edita;

	void Start () {
		GenMap ();
	}

	public void GenMap () {
		string INvalues = PlayerPrefs.GetString ("SelectedValues");
		Vector2 size = new Vector2 (Wall.GetComponent<SpriteRenderer> ().bounds.size.x
		                           , Wall.GetComponent<SpriteRenderer> ().bounds.size.y);
		int x = 0;
		int y = 0;

		GameObject parent = GameObject.CreatePrimitive (PrimitiveType.Cube);
		parent.transform.position = new Vector3 (5 * size.x / 2 - size.x / 2, 8 * size.y / 2 - size.y / 2, 0);
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
			string[] strs = INvalues.Split (",".ToCharArray ());
			int i = 0;
			string[] objs = new string[strs.Length];
			string[] types = new string[strs.Length];
			foreach (string s in strs) {
				objs[i] = s.Split(".".ToCharArray())[0];
				types[i] = s.Split(".".ToCharArray())[1];
				i++;
			}

			i = 0;
			foreach (string s in objs) {
				if (GeneradorState == GeneradorStates.crear) {
					switch (s) {
					case "0":
						GameObject obj = Instantiate (Wall);
						obj.transform.position = new Vector3 (x * size.x, y * size.y, 0);
						obj.transform.parent = walls.transform;
						break;
					case "1":
						GameObject obj1 = Instantiate (Floor);
						obj1.transform.position = new Vector3 (x * size.x, y * size.y, 0);
						obj1.transform.parent = walls.transform;
						break;
					case "2":
						GameObject obj2 = Instantiate (Weedy);
						obj2.transform.position = new Vector3 (x * size.x, y * size.y, 0);
						obj2.transform.parent = objects.transform;
						obj2.name = "PutaHierba";
						obj2 = Instantiate (Floor);
						obj2.transform.position = new Vector3 (x * size.x, y * size.y, 0);
						obj2.transform.parent = walls.transform;
						break;
					case "3":
						GameObject obj4 = Instantiate (Gloria_Pasa);
						obj4.transform.position = new Vector3 (x * size.x, y * size.y, 0);
						obj4.name = "Gloria";
						obj4.transform.parent = objects.transform;
						obj4.GetComponent<Gloria>().MakePasa();
						break;
					case "4":
						GameObject obj3 = Instantiate (GloriaBendita);
						obj3.transform.position = new Vector3 (x * size.x, y * size.y, 0);
						obj3.name = "Gloria";
						obj3.transform.parent = objects.transform;
						break;
					case "5":
						GameObject obj5 = Instantiate (PutoNombrador);
						obj5.transform.position = new Vector3 (x * size.x, y * size.y, 0);
						obj5.name = "Coin";
						obj5.transform.parent = objects.transform;
						break;
					case "6":
						GameObject obj6 = Instantiate (Pusher);
						obj6.transform.position = new Vector3 (x * size.x, y * size.y, 0);
						obj6.name = "Pusher";
						obj6.GetComponent<Pusher>().facing = (global::Pusher.Faces)int.Parse(types[i]);
						obj6.transform.parent = objects.transform;
						break;
					default:
						Debug.Log ("'" + s + "' no reconocido");
						break;
					}
					GameObject f = Instantiate (Floor);
					f.transform.position = new Vector3 (x * size.x, y * size.y, 0);
					f.transform.parent = walls.transform;
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
					case "6":
						obj.GetComponent<EditaModule> ().block = 6;
						obj.GetComponent<EditaModule> ().type = int.Parse(types[i]);
						break;
					default:
						break;
					}
					obj.transform.position = new Vector3 (x * size.x, y * size.y, 0);
					obj.transform.parent = walls.transform;
					x++;
					if (x > 4) {
						x = 0;
						y++;
					}
				}
				i++;
			}
			if (GeneradorState == GeneradorStates.crear){
				//SyncStart
				foreach (GameObject g in GameObject.FindGameObjectsWithTag ("GloriaVendita")){
					if (g.GetComponent<Coin>() != null){
						g.GetComponent<Coin>().SyncStart();
					}
					if (g.GetComponent<PutoJoder>() != null){
						g.GetComponent<PutoJoder>().SyncStart();
					}
					if (g.GetComponent<Gloria>() != null){
						g.GetComponent<Gloria>().SyncStart();
					}
				}
			}
		}
		//Fill blank space in editor canvas
		if (GeneradorState == GeneradorStates.editar){
			while (y < 8){
				GameObject obj = Instantiate(Edita);
				obj.GetComponent<EditaModule>().block = 0;
				obj.transform.position = new Vector3(x * size.x, y * size.y, 0);
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
