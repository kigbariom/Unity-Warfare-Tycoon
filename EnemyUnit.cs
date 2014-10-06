using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour {

	public int health = 10;
	public bool hasMoved = false;
	public bool isAlive = true;
	public string name;

	void Update() {
		if (health <= 0) {
			isAlive = false;
			Destroy(gameObject);
		}
	}
}


