using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerEnableScripts : NetworkBehaviour {


	public PlayerFunction playerFunctionScript;
	public PlayerSyncTransform playerSyncTransformScript;
	// Use this for initialization
	void Start () {
		playerFunctionScript = gameObject.GetComponent<PlayerFunction>();
		playerSyncTransformScript = gameObject.GetComponent<PlayerSyncTransform>();

		if (isLocalPlayer) {
			playerFunctionScript.enabled = true;
			//playerSyncTransformScript.enabled = true;
			gameObject.GetComponentInChildren<Camera>().enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
