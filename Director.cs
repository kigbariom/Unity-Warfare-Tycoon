using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {

	public static int FreeUnits = 4;
	public static string currentTurn = "Player";
	public static bool cam = false;
	public static bool hasMoved = false;

#if UNITY_EDITOR
	public GUISkin skin;
#endif

	public GameObject punit1;
	public GameObject punit2;
	public GameObject punit3;
	public GameObject punit4;

	public GameObject eunit1;
	public GameObject eunit2;
	public GameObject eunit3;
	public GameObject eunit4;



	void OnGUI() {
#if UNITY_EDITOR
		GUI.skin = skin;
#endif

		GUI.Box (new Rect (0, 0, Screen.width, 100), "Menu");

		//GUI.Box (new Rect (0, 100, 100, 100), currentTurn + " Turn\nUnits Left: " + FreeUnits);
		//GUI.Box (new Rect (0, 0, 100, 100), "Enemy Roster\n" + eunit1.transform.gameObject.GetComponent<EnemyUnit> ().name + ": "
		  //       + eunit1.transform.gameObject.GetComponent<EnemyUnit> ().health +
		     //    "\n" + eunit2.transform.gameObject.GetComponent<EnemyUnit> ().name + ": " + eunit2.transform.gameObject.GetComponent<EnemyUnit> ().health +
		    //   "\n" + eunit3.transform.gameObject.GetComponent<EnemyUnit> ().name + ": " + eunit3.transform.gameObject.GetComponent<EnemyUnit> ().health +
		      //   "\n" + eunit4.transform.gameObject.GetComponent<EnemyUnit> ().name + ": " + eunit4.transform.gameObject.GetComponent<EnemyUnit> ().health);

		//GUI.Box (new Rect (Screen.width - 100, 0, 100, 100), "Player Roster\n" + punit1.transform.gameObject.GetComponent<PlayerUnit>().name + ": "
		  //       + punit1.transform.gameObject.GetComponent<PlayerUnit> ().health +
		    //     "\n" + punit2.transform.gameObject.GetComponent<PlayerUnit>().name + ": " + punit2.transform.gameObject.GetComponent<PlayerUnit> ().health +
		      //   "\n" + punit3.transform.gameObject.GetComponent<PlayerUnit>().name + ": " + punit3.transform.gameObject.GetComponent<PlayerUnit> ().health +
		        // "\n" + punit4.transform.gameObject.GetComponent<PlayerUnit>().name + ": " + punit4.transform.gameObject.GetComponent<PlayerUnit> ().health);
		}


	void Update() {
				FreeUnits = punit1.transform.gameObject.GetComponent<PlayerUnit> ().hasMoved +
						punit2.transform.gameObject.GetComponent<PlayerUnit> ().hasMoved +
						punit3.transform.gameObject.GetComponent<PlayerUnit> ().hasMoved +
						punit4.transform.gameObject.GetComponent<PlayerUnit> ().hasMoved;

				if (FreeUnits > 0) {
						currentTurn = "Player";
				} else {
						StartCoroutine("Wait");
				}
		}

	// wait for int seconds
	IEnumerator Wait() {
		reset();
		Debug.Log ("Before Waiting 3 Seconds");
		currentTurn = "Enemy";
		cam = true;
		yield return new WaitForSeconds(3);
		hasMoved = true;
		currentTurn = "Player";
		cam = false;
		Debug.Log ("After Waiting 3 Seconds");
	}

	void reset() {
		punit1.transform.gameObject.GetComponent<PlayerUnit> ().hasMoved = 1;
		punit1.transform.gameObject.GetComponent<PlayerUnit> ().hasAttacked = 1;
		punit2.transform.gameObject.GetComponent<PlayerUnit> ().hasMoved = 1;
		punit2.transform.gameObject.GetComponent<PlayerUnit> ().hasAttacked = 1;
		punit3.transform.gameObject.GetComponent<PlayerUnit> ().hasMoved = 1;
		punit3.transform.gameObject.GetComponent<PlayerUnit> ().hasAttacked = 1;
		punit4.transform.gameObject.GetComponent<PlayerUnit> ().hasMoved = 1;
		punit4.transform.gameObject.GetComponent<PlayerUnit> ().hasAttacked = 1;
		}

}
