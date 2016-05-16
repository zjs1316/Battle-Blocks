using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public GameObject playerName;
	public GameObject playerKills;
	public GameObject playerDeaths;

	public void SetData (string name, string kills, string deaths)
	{
		this.playerName.GetComponent<Text>().text = name;
		this.playerKills.GetComponent<Text>().text = kills;
		this.playerDeaths.GetComponent<Text>().text = deaths;
	}
}
