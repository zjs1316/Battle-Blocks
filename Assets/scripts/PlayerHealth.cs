using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour {
	
	[SyncVar (hook = "OnHealthDown")] private int health = 100;
	private Text healthText;
	private bool death = false;
	public bool youDead = false;
	
	public delegate void DelegateDeath();
	public event DelegateDeath DieEvent;
	
	public delegate void DelegateRespawn();
	public event DelegateRespawn RespawnEvent;
	
	// Use this for initialization
	void Start () {
		healthText = GameObject.Find ("Health Text").GetComponent<Text> ();
		SetHealthText ();
		
	}
	
	// Update is called once per frame
	void Update () {
		ConditionCheck ();
		
	}
	
	void ConditionCheck()
	{
		if (health <= 0 && !death && !youDead) {
			death = true;
		}
		if (health <= 0 && death)
		{
			if(DieEvent != null)
			{
				DieEvent();
			}
			death = false;
		}
		
		if (health > 0 && youDead) {
			if (RespawnEvent != null) {
				RespawnEvent ();
			}
			
			youDead = false;
		}
	}
	
	void SetHealthText()
	{
		if (isLocalPlayer) {
			healthText.text = " Health : " + health.ToString ();
		}
	}
	
	public void DeductHealth(int dmg)
	{
		health -= dmg;
	}
	
	void OnHealthDown(int hlth)
	{
		health = hlth;
		SetHealthText ();
	}
	
	public void HealthReset()
	{
		health = 100;
	}
}
