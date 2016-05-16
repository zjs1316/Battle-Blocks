using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

	public GameObject image;


	public GameObject dbManager;
	private PlayerDBManager playerDBManager;
	public bool isItOn = false;

	public void Start()
	{
		dbManager = GameObject.FindGameObjectWithTag ("DB");
		playerDBManager = dbManager.GetComponent <PlayerDBManager> ();

	}

	public void Update() 
	{
		buttonPress ();
	}

	private void buttonPress()
	{
		if (Input.GetKeyDown (KeyCode.Tab)) 
		{
			isItOn = !isItOn;
			toggleBoard ();
		} 
	}

	public void toggleBoard()
	{
		
		if (isItOn == true) {
			image.SetActive (true);
			playerDBManager.ShowInfo ();
			return;
		} else {
			playerDBManager.ClearInfo ();
			image.SetActive (false);
			return;
		}
	}

}
