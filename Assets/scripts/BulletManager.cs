using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BulletManager : NetworkBehaviour {

	public GameObject bullet;
	public float numberOfBullets = 60;
	public Vector3 bulletTarget = Vector3.zero;
	public Vector3 bulletLocation = Vector3.zero;
	public Vector3 bulletRotation = Vector3.zero;
	//public BulletScript bulletScript;
	public List <GameObject> bulletList; 
	//[SyncVar] bool amIactive;
	//public SyncListBool bulletSyncList;

	//float bulletSpeed = 10;
	//bool fire = false;


	// Use this for initialization
	void Start () {

		//if (isLocalPlayer) {
			bulletList = new List<GameObject> ();
			//bulletSyncList = new SyncListBool();

			//Debug.Log("Connected to server, go ahead and make the bullets!");
		
			for (int i = 0; i <numberOfBullets; i++) {
				GameObject inst = (GameObject)Instantiate (bullet);
				//ClientScene.RegisterPrefab(inst);
				//NetworkServer.Spawn(bullet);
				inst.transform.position = new Vector3 (0, -25, 0);
				bulletList.Add (inst);
				
				//bullet.name = transform.parent.name + " bullet " + i;
				inst.gameObject.GetComponent<BulletScript> ().enabled = false;

			}
			/*for(int i = 0; i <numberOfBullets;i++)
		{
			bulletSyncList[i] = false;
		}*/
		//}

	}

	// Update is called once per frame
	void Update () {
		/*for(int i = 0; i <numberOfBullets;i++)
		{
			if( bulletList[i].activeSelf == true) bulletSyncList[i] = true;
		}*/
	}
}
