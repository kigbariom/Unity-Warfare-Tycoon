using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public Director director;
	public int screenHeight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called nce per frame
	void Update () {
		screenHeight = Screen.height;
		float mousePosX = Input.mousePosition.x;
		float mousePosY = Input.mousePosition.y;
		int scrollDistance = 1;
		float scrollSpeed = 30;

		// at the end of player's turn, the camera will snap to the first enemy unit that is alive [1 - 4]
		if (Director.cam && !Director.hasMoved)
		{
			// if enemy unit 1 is alive
			if (director.eunit1.transform.gameObject.GetComponent<EnemyUnit>().isAlive == true) {
				float x = director.eunit1.transform.position.x - transform.position.x;
				float y = director.eunit1.transform.position.y - transform.position.y;
				float z = director.eunit1.transform.position.z - transform.position.z;
				
				transform.Translate((x * 2 * Time.deltaTime), 0, ((z - 5) * 2 * Time.deltaTime), Space.World);
			}
			// if enemy unit 2 is alive
			else if (director.eunit2.transform.gameObject.GetComponent<EnemyUnit>().isAlive == true) {
				float x = director.eunit2.transform.position.x - transform.position.x;
				float y = director.eunit2.transform.position.y - transform.position.y;
				float z = director.eunit2.transform.position.z - transform.position.z;
				
				transform.Translate((x * 2 * Time.deltaTime), 0, ((z - 5) * 2 * Time.deltaTime), Space.World);
			}
			// if enemy unit 3 is alive
			else if (director.eunit3.transform.gameObject.GetComponent<EnemyUnit>().isAlive == true) {
				float x = director.eunit3.transform.position.x - transform.position.x;
				float y = director.eunit3.transform.position.y - transform.position.y;
				float z = director.eunit3.transform.position.z - transform.position.z;
				
				transform.Translate((x * 2 * Time.deltaTime), 0, ((z - 5) * 2 * Time.deltaTime), Space.World);
			}
			// if enemy unit 4 is alive
			else if (director.eunit4.transform.gameObject.GetComponent<EnemyUnit>().isAlive == true) {
				float x = director.eunit4.transform.position.x - transform.position.x;
				float y = director.eunit4.transform.position.y - transform.position.y;
				float z = director.eunit4.transform.position.z - transform.position.z;
				
				transform.Translate((x * 2 * Time.deltaTime), 0, ((z - 5) * 2 * Time.deltaTime), Space.World);
			}
		}

		// just after the enemy turn, the camera will snap to the first player unit that is alive [1 - 4]
		if (Director.hasMoved) {
			// if player unit 1 is alive
			if (director.punit1.transform.gameObject.GetComponent<PlayerUnit>().isAlive == true) {
				Vector3 newPos = new Vector3(director.punit1.transform.position.x, transform.position.y, director.punit1.transform.position.z);
				transform.position = newPos;
				Director.hasMoved = false;
			}
			// if player unit 2 is alive
			if (director.punit2.transform.gameObject.GetComponent<PlayerUnit>().isAlive == true) {
				Vector3 newPos = new Vector3(director.punit2.transform.position.x, transform.position.y, director.punit2.transform.position.z);
				transform.position = newPos;
				Director.hasMoved = false;
			}
			// if player unit 3 is alive
			if (director.punit3.transform.gameObject.GetComponent<PlayerUnit>().isAlive == true) {
				Vector3 newPos = new Vector3(director.punit3.transform.position.x, transform.position.y, director.punit3.transform.position.z);
				transform.position = newPos;
				Director.hasMoved = false;
			}
			// if player unit 4 is alive
			if (director.punit4.transform.gameObject.GetComponent<PlayerUnit>().isAlive == true) {
				Vector3 newPos = new Vector3(director.punit4.transform.position.x, transform.position.y, director.punit4.transform.position.z);
				transform.position = newPos;
				Director.hasMoved = false;
			}
		}

		// controls panning the camera
		if ((transform.position.x < 10) && (Director.cam == false))
		{
			//Allows the camera to move right
			if (mousePosX >= Screen.width - scrollDistance)
			{
				transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
			}
		}

		if ((transform.position.x > -10)  && (Director.cam == false))
		{
			//Allows the camera to move left
			if (mousePosX < scrollDistance)
			{
				transform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
			}
		}

		if ((transform.position.z < -5) && (Director.cam == false))
		{
			//Allows the camera to move up
			if (mousePosY >= Screen.height - scrollDistance)
			{
				transform.Translate((new Vector3(0,0,1) * scrollSpeed * Time.deltaTime), Space.World);
			}
		}

		if ((transform.position.z > -27)  && (Director.cam == false))
		{
			//Allows the camera to move down
			if (mousePosY < scrollDistance)
			{
				transform.Translate((new Vector3(0,0,-1) * scrollSpeed * Time.deltaTime), Space.World);
			}
		}
	}
}
