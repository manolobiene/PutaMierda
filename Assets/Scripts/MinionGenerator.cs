using UnityEngine;
using System.Collections;

public class MinionGenerator : MonoBehaviour {
	public GameObject minion;
	public Vector3 L, R;
	float count = 0, killcount;
	public int killed = 0;
	float level = 1;

	void Update () {
		if (Camera.main.GetComponent<PutoMenu> ().MenuState == PutoMenu.MenuStates.selectMode) {
			if (count <= 0) {
				count = Random.Range (1f, 5f);
				GameObject min = (GameObject)Instantiate (minion);
				int d = (int)Random.Range (0.01f, 1.99f);
				if (d == 0) {
					min.transform.position = L;
					DanceTowards DT = (DanceTowards)min.AddComponent<DanceTowards> ();
					DT.direction = Vector3.right;
					DT.velocity = (int)Random.Range (1.01f, 3.99f);
				}
				if (d == 1) {
					min.transform.position = R;
					DanceTowards DT = (DanceTowards)min.AddComponent<DanceTowards> ();
					DT.direction = -Vector3.right;
					DT.velocity = (int)Random.Range (1.01f, 3.99f);
				}
			}
			count -= 1 * level * Time.deltaTime;
			killcount -= 1 * Time.deltaTime;
			if (killcount < 0){
				level = killed;
				level = Mathf.Clamp (level, 1, 5);
				killcount = 5;
				killed = 0;
			}
		}
	}
}
