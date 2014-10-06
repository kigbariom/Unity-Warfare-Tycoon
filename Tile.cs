using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public float north;
	public float east;
	public float south;
	public float west;

#if UNITY_EDITOR
	public GUISkin skin;
#endif

	public bool occupied = false;
	public GameObject who = null;
	public Camera mainCamera;
	public GameObject fullCover;
	public GameObject halfCover;
	public GameObject halfCoverHelper;

	// whether or not cover is being displayed in each direction from this tile
	public bool NdisplayCover = false;
	public bool EdisplayCover = false;
	public bool SdisplayCover = false;
	public bool WdisplayCover = false;

	// clone sockets for instantiating/destroying cover information
	GameObject Nsocket;
	GameObject Esocket;
	GameObject Ssocket;
	GameObject Wsocket;

	// clone sockets for instantiating/destroying the transparent piece of half cover info
	GameObject NsocketH;
	GameObject EsocketH;
	GameObject SsocketH;
	GameObject WsocketH;

	bool selected = false;


	// Use this for initialization
	void Start () {
		gameObject.renderer.enabled = false;
	}

	// On GUI
	void OnGUI(){
#if UNITY_EDITOR
		GUI.skin = skin;
#endif



		if (selected && who.transform.parent.name == "PlayerUnits") {
			GUI.Box (new Rect (Screen.width - 300, Screen.height - 90, 80, 80), "Name: " + who.transform.gameObject.GetComponent<PlayerUnit>().name + 
			         "\nHealth: " + who.transform.gameObject.GetComponent<PlayerUnit>().health + "\n");
		}
		/*
		if (selected && who.transform.parent.name == "EnemyUnits") {
			GUI.Box (new Rect (0, Screen.height - 100, 100, 100), "Name: " + who.transform.gameObject.GetComponent<EnemyUnit>().name + 
			         "\nHealth: " + who.transform.gameObject.GetComponent<EnemyUnit >().health + "\n");
		}
		*/
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			//Debug.Log("Mouse is down");
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) 
			{
				// if this instance is selected and the hit was not on it
				if (selected == true && hitInfo.transform.gameObject.name != gameObject.name && who.transform.parent.name == "PlayerUnits" && who.transform.gameObject.GetComponent<PlayerUnit>().hasMoved == 1) {
					// if we click on a tile that is unoccupied
					if ((hitInfo.transform.parent.name == "Tiles") && (hitInfo.transform.gameObject.GetComponent<Tile>().occupied == false))
					{
						who.transform.position = new Vector3(hitInfo.transform.gameObject.transform.position.x, transform.position.y, hitInfo.transform.gameObject.transform.position.z);
						hitInfo.transform.gameObject.GetComponent<Tile>().occupied = true;
						hitInfo.transform.gameObject.GetComponent<Tile>().who = who;
						who.transform.gameObject.GetComponent<PlayerUnit>().hasMoved = 0;
						who = null;
						occupied = false;
						selected = false;
					}
					// if we click on a tile that IS occupied
					if ((hitInfo.transform.parent.name == "Tiles") && (hitInfo.transform.gameObject.GetComponent<Tile>().occupied == true)) {
						selected = false;
					}
					// if we click on a tile that is occupied by an enemy
					if ((hitInfo.transform.gameObject.GetComponent<Tile>().who.transform.parent.name == "EnemyUnits") && (who.transform.gameObject.GetComponent<PlayerUnit>().hasAttacked > 0))  {
						hitInfo.transform.gameObject.GetComponent<Tile>().who.GetComponent<EnemyUnit>().health -= 1;
						who.transform.gameObject.GetComponent<PlayerUnit>().hasAttacked -= 1;
						selected = false;
					}
				}
				// if a tile is selected and there is no player unit available to move on it.
				else if (hitInfo.transform.gameObject.name != gameObject.name) {
					selected = false;

				}
				//"Hit " + hitInfo.transform.gameObject.name);
			} 
			else {
				//("No hit");
			}
			//"Mouse is down");
		} 
	}

	// selects a tile on mouse down
	void OnMouseDown() {
		if (occupied) {
			Debug.Log ("Unit hit!");
			health.currentTile = this;
			armor.currentTile = this;
			transparency.currentTile = this;
			pulse.currentTile = this;
			selected = true;
		}
	}

	// displays the cover info on hovering over a tile
	void OnMouseOver() {
		gameObject.renderer.enabled = true;
		//Quaternion Nrot = Quaternion.identity;
		Quaternion Erot = Quaternion.identity;
		//Quaternion Srot = Quaternion.identity;
		Quaternion Wrot = Quaternion.identity;

		if ((north == 1f) && (!NdisplayCover)) {
			Nsocket = Instantiate(fullCover, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z + 0.4f), Quaternion.identity) as GameObject;
			NdisplayCover = true;
		}
		if ((north == .5f) && (!NdisplayCover)) {
			Nsocket = Instantiate(halfCover, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z + 0.4f), Quaternion.identity) as GameObject;
			NsocketH = Instantiate(halfCoverHelper, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z + 0.418f), Quaternion.identity) as GameObject;
			NdisplayCover = true;
		}
		if ((east == 1f) && (!EdisplayCover)) {
			Erot *= Quaternion.Euler(0, 90, 0);
			Esocket = Instantiate(fullCover, new Vector3(transform.position.x + 0.4f, transform.position.y + 1f, transform.position.z), Erot) as GameObject;
			EdisplayCover = true;
		}
		if ((east == .5f) && (!EdisplayCover)) {
			Erot *= Quaternion.Euler(0, 90, 0);
			Esocket = Instantiate(halfCover, new Vector3(transform.position.x + 0.4f, transform.position.y + .5f, transform.position.z), Erot) as GameObject;
			EsocketH = Instantiate(halfCoverHelper, new Vector3(transform.position.x + 0.42f, transform.position.y + .5f, transform.position.z), Erot) as GameObject;
			EdisplayCover = true;
		}
		if ((south == 1f) && (!SdisplayCover)) {
			Ssocket = Instantiate(fullCover, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z - 0.825f), Quaternion.identity) as GameObject;
			SdisplayCover = true;
		}
		if ((south == .5f) && (!SdisplayCover)) {
			Ssocket = Instantiate(halfCover, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z - 0.825f), Quaternion.identity) as GameObject;
			SsocketH = Instantiate(halfCoverHelper, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z - 0.825f), Quaternion.identity) as GameObject;
			SdisplayCover = true;
		}
		if ((west == 1f) && (!WdisplayCover)) {
			Wrot *= Quaternion.Euler(0, -90, 0);
			Wsocket = Instantiate(fullCover, new Vector3(transform.position.x - 0.4f, transform.position.y + 1f, transform.position.z), Wrot) as GameObject;
			WdisplayCover = true;
		}
		if ((west == .5f) && (!WdisplayCover)) {
			Wrot *= Quaternion.Euler(0, -90, 0);
			Wsocket = Instantiate(halfCover, new Vector3(transform.position.x - 0.4f, transform.position.y + .5f, transform.position.z), Wrot) as GameObject;
			WsocketH = Instantiate(halfCoverHelper, new Vector3(transform.position.x - 0.42f, transform.position.y + .5f, transform.position.z), Wrot) as GameObject;
			WdisplayCover = true;
		}
	}

	// destroys any displayed cover info on mouse hover exit
	void OnMouseExit() {
		gameObject.renderer.enabled = false;
		if (north == 1f) {
			Destroy (Nsocket);
			NdisplayCover = false;
		}
		if (north == .5f) {
			Destroy(Nsocket);
			Destroy (NsocketH);
			NdisplayCover = false;
		}
		if (east == 1f) {
			Destroy (Esocket);
			EdisplayCover = false;
		}
		if (east == .5f) {
			Destroy(Esocket);
			Destroy(EsocketH);
			EdisplayCover = false;
		}
		if (south == 1f) {
			Destroy (Ssocket);
			SdisplayCover = false;
		}
		if (south == .5f) {
			Destroy(Ssocket);
			Destroy (SsocketH);
			SdisplayCover = false;
		}
		if (west == 1f) {
			Destroy (Wsocket);
			WdisplayCover = false;
		}
		if (west == .5f) {
			Destroy(Wsocket);
			Destroy(WsocketH);
			WdisplayCover = false;
		}
	}
}
