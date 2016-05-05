using UnityEngine;
using System.Collections;

public class DBGameInfoStorage : MonoBehaviour {

	public GameObject playerInfo;

	void Start(){
		playerInfo.GetComponent<PlayerFunction> ();
	}

	public void LoadInfo( int kills, int deaths){
		//kills = playerInfo.kills;
		//deaths = playerInfo.deaths;
		return;
	}


}
