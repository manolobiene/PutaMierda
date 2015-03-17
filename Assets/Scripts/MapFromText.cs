using UnityEngine;
using System.Collections;

public class MapFromText : MonoBehaviour {
	public GameObject Wall, Floor, Weedy, GloriaBendita, Gloria_Pasa, PutoNombrador;
	
	// Update is called once per frame
	void Start () {
		string map = PlayerPrefs.GetString ("SelectedMap");
		string values = PlayerPrefs.GetString ("SelectedValues");
		GenMap (values);
	}

	void GenMap (string INvalues) {
		string[] objs = INvalues.Split (",".ToCharArray ());
		float size = Wall.GetComponent<SpriteRenderer> ().bounds.size.x;
		int x = 0;
		int y = 0;

		GameObject parent = GameObject.CreatePrimitive (PrimitiveType.Cube);
		parent.transform.position = new Vector3 (5 * size / 2 - size/2, 8 * size / 2 - size/2, 0);
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

		foreach (string s in objs) {
			switch (s){
			case "0":
				GameObject obj = Instantiate(Wall);
				obj.transform.position = new Vector3(x*size, y*size, 0);
				obj.transform.parent = walls.transform;
				break;
			case "1":
				GameObject obj1 = Instantiate(Floor);
				obj1.transform.position = new Vector3(x*size, y*size, 0);
				obj1.transform.parent = walls.transform;
				break;
			case "2":
				GameObject obj2 = Instantiate(Weedy);
				obj2.transform.position = new Vector3(x*size, y*size, 0);
				obj2.transform.parent = objects.transform;
				obj2.name = "PutaHierba";
				obj2 = Instantiate(Floor);
				obj2.transform.position = new Vector3(x*size, y*size, 0);
				obj2.transform.parent = walls.transform;
				break;
			case "3":
				GameObject obj4 = Instantiate(Gloria_Pasa);
				obj4.transform.position = new Vector3(x*size, y*size, 0);
				obj4.name = "Gloria_Pasa";
				obj4.transform.parent = objects.transform;
				break;
			case "4":
				GameObject obj3 = Instantiate(GloriaBendita);
				obj3.transform.position = new Vector3(x*size, y*size, 0);
				obj3.name = "Gloria";
				obj3.transform.parent = objects.transform;
				break;
			case "5":
				GameObject obj5 = Instantiate(PutoNombrador);
				obj5.transform.position = new Vector3(x*size, y*size, 0);
				obj5.GetComponent<SpriteRenderer>().sortingOrder = -1;
				obj5.name = "PutoNombrador";
				obj5.transform.parent = objects.transform;
				break;
			default:
				Debug.Log ("'" + s + "' no reconocido");
				break;
			}
			x++;
			if (x > 4){
				x = 0;
				y++;
			}
		}
		parent.transform.position = Vector3.zero;
	}
}
