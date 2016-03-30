using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class bulletSync : NetworkBehaviour {
	[SyncVar]private Vector3 syncPos;
	[SyncVar]private float syncYRot;
	[SyncVar]private Vector3 scaleSync;

	private Vector3 lastPos;
	private Quaternion lastRot;
	private Transform myTransform;
	private Vector3 lastScale;
	private float lerpRate=10;
	private float posThreshold = 0f;
	private float rotThreshold = 5;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			//NetworkServer.Spawn (gameObject);
		}
		//CmdspawnBullets ();
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		TransmitMotion ();
		LerpMotion ();
	}

	void TransmitMotion()
	{
		if (!isServer) {
			return;
		}

		if (Vector3.Distance (myTransform.position, lastPos) > posThreshold || Quaternion.Angle (myTransform.rotation, lastRot) > rotThreshold) {
			lastPos = myTransform.position;
			lastRot = myTransform.rotation;
			lastScale = myTransform.localScale;

			syncPos = myTransform.position;
			syncYRot = myTransform.localEulerAngles.y;
			scaleSync = myTransform.localScale;
		}
	}

	void LerpMotion()
	{
		if (isServer) {
			return;
		}

		myTransform.position = Vector3.Lerp (myTransform.position, syncPos, Time.deltaTime * lerpRate);

			Vector3 newRot = new Vector3(0,syncYRot,0);
		myTransform.rotation = Quaternion.Lerp (myTransform.rotation, Quaternion.Euler (newRot), Time.deltaTime * lerpRate);
	}

	/*[Command]
	void CmdspawnBullets()
	{
		NetworkServer.Spawn(gameObject);
	}*/
}
