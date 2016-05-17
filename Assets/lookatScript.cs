using UnityEngine;
using System.Collections;

public class lookatScript : MonoBehaviour {

	public GameObject parent;
	PlayerFunction playerFunctionScript;
	 Vector3 destination;
	public float yRot;
	// Use this for initialization
	void Start () {
		playerFunctionScript = parent.GetComponent<PlayerFunction> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		destination = playerFunctionScript.destination;
		//destination.x = 0f;
		//destination.z = 0f;

		if (Vector3.Distance (gameObject.transform.position, destination) > playerFunctionScript.SnapTo) {
			
			gameObject.transform.LookAt (destination); //snap to destination
			//yRot = transform.rotation.y;
		
		} else {
			//transform.rotation = Quaternion.Euler (0f, yRot, 0f);
		}
	
	}
}
