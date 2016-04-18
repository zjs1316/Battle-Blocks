using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TargetSyncPos : NetworkBehaviour
{

	public GameObject[] players;
	public float ScoreThreshhold = 15;
	float dist;
	

	
	// Update is called once per frame but it's not guaranteed to be exactly regular,
	// because the processing load varies.
	// This is why we typically use Time.delt-	aTime to smooth out transforms.
	// But it's a good place to interpolate (lerp) between an old position (
	// or rotation) and the newly acquired position information
	void Update ()
	{
	}
	
	// FixedUpdate will fire at regular intervals, making it a good place
	// To send our regular position updates.
	void FixedUpdate ()
	{

		if(isServer)
		{
		
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject p in players)
		{
			dist = Vector3.Distance(p.transform.position, gameObject.transform.position);
			if( dist < ScoreThreshhold)
			{
				
				Vector3 position = new Vector3(Random.Range(-350, 350), 30f, Random.Range(-250, 250));
				gameObject.transform.position = position;
			}
		}
		}
	}
}