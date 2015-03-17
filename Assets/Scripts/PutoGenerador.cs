using UnityEngine;
using System.Collections;

public class PutoGenerador : MonoBehaviour {
	public enum GeneradorStates {crear = 0, editar = 1};
	public GeneradorStates GeneradorState;
	public Texture2D textura;
	public GameObject PutoNombrador, Wall, Floor, GloriaBendita, Gloria_Pasa, Weed;
	public GameObject Edita;
	public Color yellow;

	void Start () {

		int x = textura.width;
		int y = textura.height;
		float size = PutoNombrador.GetComponent<SpriteRenderer>().bounds.size.x;

		GameObject parent = GameObject.CreatePrimitive (PrimitiveType.Cube);
		parent.transform.position = new Vector3 (x * size / 2 - size/2, y * size / 2 - size/2, 0);
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

		for (int i = 0; i < x; i++) {
			for (int a = 0; a < y; a++) {
				Color col = textura.GetPixel(i,a);
				if (GeneradorState == GeneradorStates.crear){
					if (col == Color.black){
						GameObject obj = Instantiate(Wall);
						obj.transform.position = new Vector3(i*size, a*size, 0);
						obj.transform.parent = walls.transform;
					}
					if (col == Color.white){
						GameObject obj = Instantiate(Floor);
						obj.transform.position = new Vector3(i*size, a*size, 0);
						obj.transform.parent = walls.transform;
					}
					if (col == Color.blue){
						GameObject obj = Instantiate(Weed);
						obj.transform.position = new Vector3(i*size, a*size, 0);
						obj.transform.parent = objects.transform;
						obj.name = "PutaHierba";
						obj = Instantiate(Floor);
						obj.transform.position = new Vector3(i*size, a*size, 0);
						obj.transform.parent = walls.transform;
					}
					if (col == yellow){
						GameObject obj = Instantiate(Gloria_Pasa);
						obj.transform.position = new Vector3(i*size, a*size, 0);
						obj.name = "Gloria_Pasa";
						obj.transform.parent = objects.transform;
					}
					if (col == Color.red){
						GameObject obj = Instantiate(GloriaBendita);
						obj.transform.position = new Vector3(i*size, a*size, 0);
						obj.name = "Gloria";
						obj.transform.parent = objects.transform;
					}
					if (col == Color.green){
						GameObject obj = Instantiate(PutoNombrador);
						obj.transform.position = new Vector3(i*size, a*size, 0);
						obj.GetComponent<SpriteRenderer>().sortingOrder = -1;
						obj.name = "PutoNombrador";
						obj.transform.parent = objects.transform;
					}
				}
				if (GeneradorState == GeneradorStates.editar){
					GameObject obj = Instantiate(Edita);
					if (col == Color.black){
						obj.GetComponent<EditaModule>().block = 0;
					}
					if (col == Color.white){
						obj.GetComponent<EditaModule>().block = 1;
					}
					if (col == Color.blue){
						obj.GetComponent<EditaModule>().block = 2;
					}
					if (col == Color.yellow){
						obj.GetComponent<EditaModule>().block = 3;
					}
					if (col == Color.red){
						obj.GetComponent<EditaModule>().block = 4;
					}
					if (col == Color.green){
						obj.GetComponent<EditaModule>().block = 5;
					}
				}
				Debug.Log (col);
			}
		}
		parent.transform.position = Vector3.zero;
	}
}
