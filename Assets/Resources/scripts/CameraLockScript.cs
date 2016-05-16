using UnityEngine;
using System.Collections;

public class CameraLockScript : MonoBehaviour {

	public GameObject parent;
	Vector3 parentRotation;
	Quaternion myStartingRot;
	float yRot;
	// Use this for initialization
	void Awake () {
		//yRot = transform.rotation.y;
		myStartingRot = transform.rotation;
		myStartingRot.y = 0f;
		//myStartingRot.z = 0f;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//parentRotation = new Vector3 (parent.transform.rotation.x, parent.transform.rotation.y, parent.transform.rotation.z);
		//transform.Rotate(-1*parentRotation);
		//Debug.Log (myStartingRot);
		transform.rotation = myStartingRot;

		//transform.rotat
		//print (myStartingRot);
	}
}
