using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSetActiveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Camera c in Camera.allCameras)
		{
			if(c == gameObject)
			{
				c.enabled = true;
			}
			
		}
}
}