using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncTransform : NetworkBehaviour
{
	public Camera mainCamera;
	public Camera myCamera;

	[SyncVar]Vector3 syncPos;

	[SerializeField]private float lerpRate = 15; //I forgot to initalize this, so it defaulted to 0. 
	// messed up our calculation in LerpPosition.


	[SyncVar] private Quaternion syncPlayerRotation;
	[SyncVar] private Quaternion syncCamRotation;
	
	[SerializeField]private Transform playerTransform;
	[SerializeField]private Transform camTransform;
	Renderer rend;
	public Material myMat;
	public Material otherMat;

	 
	
	// Update is called once per frame but it's not guaranteed to be exactly regular,
	// because the processing load varies.
	// This is why we typically use Time.deltaTime to smooth out transforms.
	// But it's a good place to interpolate (lerp) between an old position (
	// or rotation) and the newly acquired position information
	void Update ()
	{
	}
	void Start()
	{
		rend = GetComponent<Renderer>();
		//mainCamera.enabled = false;


		print ("set color fired");
		if(isLocalPlayer)
		{
			myCamera.enabled = true;
			rend.material = myMat;
		}
		else{
			rend.material = otherMat;
		}
	}

	// FixedUpdate will fire at regular intervals, making it a good place
	// To send our regular position updates.
	void FixedUpdate ()
	{
		TransmitPosition ();
		TransmitRotations();
		LerpRotations ();
		LerpPosition ();
	}

	[Command]
	void CmdSendPositionToServer (Vector3 pos)
	{
		//runs on server, we call on client
		syncPos = pos;
	}


	[Client]
	void TransmitPosition ()
	{
		// This is where we (the client) send out our position.
		if (isLocalPlayer) {
			// Send a command to the server to update our position, and 
			// it will update a SyncVar, which then automagically updates on everyone's game instance
			CmdSendPositionToServer (transform.position);
		}
	}

	void LerpPosition ()
	{
		//only on non-client characters, not us
		//smootly move from our old position data to our updated data we got from the server.
		if (!isLocalPlayer) {
			transform.position = Vector3.Lerp (transform.position, syncPos, Time.deltaTime * lerpRate);
		}
	}



	//ROTATIONS


	void LerpRotations(){
		
		if(!isLocalPlayer)
		{
			playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation,syncPlayerRotation, Time.deltaTime *lerpRate);
			camTransform.rotation = Quaternion.Lerp (camTransform.rotation, syncCamRotation, Time.deltaTime *lerpRate);
		}
		
	}
	
	[Command]
	void CmdProvideRotationsToServer(Quaternion playerRot, Quaternion camRot)
	{
		syncPlayerRotation = playerRot;
		syncCamRotation = camRot;
	}
	
	[Client]
	void TransmitRotations()
	{
		if(isLocalPlayer)
		{
			CmdProvideRotationsToServer(playerTransform.rotation, camTransform.rotation);
		}
	}


}








