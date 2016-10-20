using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BulletScript : NetworkBehaviour {

	//destroy after x seconds
	float destroyAfter = 2f;
	//how fast the bullet travels
	public bool doWeMove = false;
	public float speedZ = 25f;
    public float speedX = 0f;
    public float speedY = 0f;
    float createdAt;
	public Rigidbody rb;
	public GameObject Player;
	public string shooter;
	public bool collision = false;
	public bool playerCollision = false;
	public float xTarg, zTarg;
	public float dmg;

	public double OthersHealth;

	public GameObject dbManager;
	public PlayerDBManager playerDBManager;

	[SyncVar]
	public Vector3 direction;
	public Vector3 targetLocation;
	//public Player PlayerFunctionScript;
	
	// Use this for initialization
	void Start () {
		//dbManager = GameObject.FindGameObjectWithTag ("DB");
		//playerDBManager = dbManager.GetComponent <PlayerDBManager> ();
		rb = GetComponent<Rigidbody>();
		print ("target location on the bullet" + targetLocation);
		NetworkServer.Spawn (gameObject);
		//targetLocation.y = 5f;
		//print (targetLocation);
		transform.LookAt (targetLocation);
	}
	void OnEnable() {
		//USE THIS AS START, it runs every time you set the prefab to active, start runs once.
		createdAt = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//move forward! this is currently toward wherever you left click after pressing 'q'
		if(doWeMove)
		{


			transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, speedZ*Time.deltaTime);
		}

		if (collision) {											//detect environment collision
			//gameObject.GetComponent<BulletScript>().enabled = false;
			Destroy(gameObject);

			//gameObject.transform.position = new Vector3(0,-100,0);
			//gameObject.transform.localScale = new Vector3(5f,5f,5f);
			//collision = false;
		}
		
		if (playerCollision) {										//detect player collision
			//gameObject.GetComponent<BulletScript>().enabled = false;
			Destroy(gameObject);
			//gameObject.transform.position = new Vector3(0,-100,0);
			//gameObject.transform.localScale = new Vector3(5f,5f,5f);
			//get player health and reduce it based on what input (q,w,e,r) was made
			//playerCollision = false;
		}


		if((createdAt + destroyAfter) < Time.time)
		{
			//speed = 0f;
			//rb.transform.forward = Vector3.zero;
			//doWeMove = false;
			//gameObject.GetComponent<BulletScript>().enabled = false;
			Destroy(gameObject);
			//gameObject.transform.position = new Vector3(0,-100,0);
			//gameObject.transform.localScale = new Vector3(5f,5f,5f);

		}

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Environment") {
			collision = true;
		}
		if (other.gameObject.tag == "Player") {
			other.GetComponent<PlayerFunction> ().health = other.GetComponent<PlayerFunction> ().health -dmg;
			RpcClientTakeDamage(other.gameObject);

			OthersHealth = other.GetComponent<PlayerFunction> ().health;

			IsItAKill ();


			playerCollision = true;
		}
	}

	void IsItAKill(){
		if (OthersHealth <= 0) {
			Player.GetComponent<PlayerFunction> ().kills = Player.GetComponent<PlayerFunction> ().kills + 1; 
			//playerDBManager.UpdateInfo (Player.GetComponent<PlayerFunction> ().kills, Player.GetComponent<PlayerFunction> ().deaths, shooter);
		}
	}

	[ClientRpc]
	public void RpcClientTakeDamage(GameObject other)
	{
		other.GetComponent<PlayerFunction> ().health = other.GetComponent<PlayerFunction> ().health -dmg;
	}


}
