 using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerFunction : NetworkBehaviour 
{

	public GameObject bulletPrefab;

	public BulletScript bulletScript;
	public Transform mover; //the object being moved
	public float SnapTo = 2f; //how close we get before snapping to the desination
	private Vector3 destination = Vector3.zero; //where we want to move
	public Vector3 bulletDestination = Vector3.zero; //where we want the bullet to move
    public Vector3 bulletPos;
	public float speed = 35;
    public float damage = 15;
    public float counter = 0;
	public BulletManager bulletManagerScript;
	public GameObject bulletManager;
	public GameObject moveMarker;
	GameObject inst;
    public GameObject R1Target;
    GameObject targeting;
    public GameObject R1GhostTarget;
    GameObject ghostTarget;
	public float abilityOffsetx;
	public float abilityOffsetz;
	Camera  playerCam;
    bool DamageBuff = false;
    public float maxHp = 100f;
	public float currentHp;
	public float health = 100f;
    public float healthRegen = 0.5f;
    public bool regen = true;
    bool postArmorBoost = false;
    float tempHp = 0.0f;
    int ShotCount = 0;
    bool firing = false;
	int ShotCountQ3 = 0;
	bool firingQ3 = false;
    float scale = 25f;
    float startingScale = 0;
    bool shotDelay = false;
	bool shotDelayQ3 = false;
    public GameObject bul;
	public Image img;
	public Text txt;

	//Bullet Variables
	float xPosOff = 0;
	float zPosOff = 0;
	float xNegOff = 0;
	float zNegOff = 0;
	Vector3 bulScale = new Vector3(0f,0f,0f);
	float zSpeed = 0;
	float ySpeed = 0;
	float xSpeed = 0;
	bool doesItMove = false;
	Vector3  bulDest = new Vector3 (0f,0f,0f);
	float bulDmg;

	public List<Vector3> spawn = new List<Vector3>();
	public Vector3 spawn1;  
	public Vector3 spawn2;  
	public Vector3 spawn3;  
	public Vector3 spawn4;  
	public GameObject player2;
	public GameObject player3;

	// add a gameobject of the second player's ingame prefab name here 


    ///Joe = 1
    ///Zach = 2
    ///Anthony = 3
    ///Nyrih = 4


    public bool basicSingleShot = false;
	public bool q2notImplemented = false;
	public bool fiveBurstMultiShot = false;
	public bool q4notImplemented = false;

	public bool threeAbilityDamageMultiplyer = false;
	public bool w2notImplemented = false;
	public bool regenEnhance = false;
	public bool w4notImplemented = false;

	public bool speedBoost = false;
	public bool e2notImplemented = false;
	public bool healthBoost = false;
		public bool boostIsOn = false;
	public bool e4NotImplemented = false;

	public bool meteor = false;
	public bool r2notImplemented = false;
	public bool sniper = false;
	public bool armor = false;
        float preArmorHp;


   
    //////////////////////////////////////////////////////////////////////////////*** q vvv ***/////////////////////////////////////////////
	bool abilityQ1IsOn = false; //ability 'q'
		float abilityQ1Cooldown = 1f; //IN SECONDS
		float abilityQ1CooldownEnd = 0;
		bool abilityQ1CooldownOn = false;

    bool abilityQ2IsOn = false; //ability 'q'
        float abilityQ2Cooldown = 1f; //IN SECONDS
        float abilityQ2CooldownEnd = 0;
        bool abilityQ2CooldownOn = false;

    bool abilityQ3IsOn = false; //ability 'q'
        float abilityQ3Cooldown = 2f; //IN SECONDS
        float abilityQ3CooldownEnd = 0;
        bool abilityQ3CooldownOn = false;

    bool abilityQ4IsOn = false; //ability 'q'
        float abilityQ4Cooldown = 1f; //IN SECONDS
        float abilityQ4CooldownEnd = 0;
        bool abilityQ4CooldownOn = false;
    //////////////////////////////////////////////////////////////////////////////*** q ^^^ ***/////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////////////*** w vvv ***/////////////////////////////////////////////
    bool abilityW1IsOn = false; //ability 'w'
		float abilityW1Cooldown = 1f; //IN SECONDS
		float abilityW1CooldownEnd = 0;
		bool abilityW1CooldownOn = false;

    bool abilityW2IsOn = false; //ability 'w'
        float abilityW2Cooldown = .5f; //IN SECONDS
        float abilityW2CooldownEnd = 0;
        bool abilityW2CooldownOn = false;

    bool abilityW3IsOn = false; //ability 'w'
        float abilityW3Cooldown = 25f; //IN SECONDS
        float abilityW3CooldownEnd = 0;
        bool abilityW3CooldownOn = false;
            float healingDurration = 5f;//in seconds
            float healingDurrationEnd = 0;
            bool boostedRegeneration = false;


    bool abilityW4IsOn = false; //ability 'w'
        float abilityW4Cooldown = 1f; //IN SECONDS
        float abilityW4CooldownEnd = 0;
        bool abilityW4CooldownOn = false;
    //////////////////////////////////////////////////////////////////////////////*** w ^^^ ***/////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////////////*** e vvv ***/////////////////////////////////////////////
    bool abilityE1IsOn = false; //ability 'e'
		float abilityE1Cooldown = 15f; //IN SECONDS
		float abilityE1CooldownEnd = 0;
		bool abilityE1CooldownOn = false;
		float sprintTimer = 3;
		float sprintTimerEnd = 0;

    bool abilityE2IsOn = false; //ability 'e'
        float abilityE2Cooldown = 1f; //IN SECONDS
        float abilityE2CooldownEnd = 0;
        bool abilityE2CooldownOn = false;

    bool abilityE3IsOn = false; //ability 'e'
        float abilityE3Cooldown = 25f; //IN SECONDS
        float abilityE3CooldownEnd = 0;
        bool abilityE3CooldownOn = false;

    bool abilityE4IsOn = false; //ability 'e'
        float abilityE4Cooldown = 1f; //IN SECONDS
        float abilityE4CooldownEnd = 0;
        bool abilityE4CooldownOn = false;
    //////////////////////////////////////////////////////////////////////////////*** e ^^^ ***/////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////////////*** r vvv ***/////////////////////////////////////////////
    bool abilityR1IsOn = false; //ability 'r'
		float abilityR1Cooldown = 45f; //IN SECONDS
		float abilityR1CooldownEnd = 0;                                
		bool abilityR1CooldownOn = false;
        float targTimer = 1f;
        float targTimerEnd = 0f;
        bool targTimerOn = false;
        float shotTimerR1 = 0.3f;
        float shotTimerEndR1 = 0;

    bool abilityR2IsOn = false; //ability 'r'
        float abilityR2Cooldown = 1f; //IN SECONDS
        float abilityR2CooldownEnd = 0;                                
        bool abilityR2CooldownOn = false;

    bool abilityR3IsOn = false; //ability 'r'
		float abilityR3Cooldown = 25f; //IN SECONDS
		float abilityR3CooldownEnd = 0;                                 
		bool abilityR3CooldownOn = false;

    bool abilityR4IsOn = false; //ability 'r'
        float abilityR4Cooldown = 45f; //IN SECONDS
        float abilityR4CooldownEnd = 0;
        bool abilityR4CooldownOn = false;                               
        float armorUpgradeCooldown = 15f;
        float armorUpgradeCooldownEnd = 0f;
        bool armorBoost = false;

        
    //////////////////////////////////////////////////////////////////////////////*** r ^^^ ***/////////////////////////////////////////////

	public Text healthText;

    // Use this for initialization
    public void Start ()
	{
		//gameObject.transform.position = new Vector3(0,5,0);
		bulletManagerScript = transform.FindChild("BulletManager").GetComponent<BulletManager> ();
		destination = mover.position; //set the destination to the objects position when the script is run the first time
		DontDestroyOnLoad(gameObject);
		playerCam = gameObject.GetComponentInChildren<Camera>();
		inst = (GameObject)Instantiate(moveMarker);
		inst.SetActive(false);
        targeting = (GameObject)Instantiate(R1Target);
        targeting.SetActive(false);
        ghostTarget = (GameObject)Instantiate(R1GhostTarget);
        ghostTarget.SetActive(false);
        health = 100f;
        damage = 15f;
        regen = true;
        healthRegen = 0.5f;
        //print("starting HP = " + health);
		//bul = (GameObject)Instantiate (bul);
		bul = (GameObject)Resources.Load("bullet");
		healthText = GameObject.Find ("Health Text").GetComponent<Text> ();
		spawn1 = new Vector3(1276f, 4.0f, -63f);
		spawn2 = new Vector3(816f, 4.0f, 390f);
		spawn3 = new Vector3(1276f, 4.0f, 390f);
		spawn4 = new Vector3(816f, 4.0f, -63f);
		spawn.Add(spawn1);
		spawn.Add(spawn2);
		spawn.Add(spawn3);
		spawn.Add(spawn4);
		StartCoroutine(regerateHealth());
	}

    void wBuff()
    {
       //print("called W1");
            //counter = counter + 1;
            if (counter % 3 == 0)       //every third ability sets it off
            {
                damage = damage * 2; ;
                speed = 15f;
                //print("2 x damage: " + damage + ", speed: " + speed);
            }
            else if (counter % 3 != 0)
            {
                damage = 15f;
                speed = 35f;
                //print("normal damage: " + damage + ", speed: " + speed);
                //counter = counter +1;
            }
        
    }

	public void toArena()
	{
		int r = Random.Range(0,4);
		Vector3 temp = spawn [r];
		float tempX =  temp.x;
		float tempY =  temp.y;
		float tempZ =  temp.z;
		player2 = GameObject.Find ("Player 2");
		player2.transform.position = new Vector3(tempX, tempY, tempZ);

		//VVV change the GameObject player2 to the new GameObject VVV//

		//New game Object		Player's inGame name
		//	|							|
		//	V							V
		player3 = GameObject.Find ("Player 3");
		player3.transform.position = new Vector3(tempX, tempY, tempZ);
	}


	void OnTriggerEnter(Collider other){

		//Bullet Collision/dmg
		if (other.gameObject.tag == "Bullet") {
			print ("In Damage function");
			print (other.GetComponent<BulletScript> ().dmg);
			health = health - other.GetComponent<BulletScript> ().dmg;
			//playerCollision = true;
		}


		/////////////////////////////////////////////////////*V* q *V*/////////////////////////////////////////////////////
		if (other.gameObject.tag == "q1")
		{
			basicSingleShot = true;
			q2notImplemented = false;
			fiveBurstMultiShot = false;
			q4notImplemented = false;
		}
		if (other.gameObject.tag == "q2")
		{
			q2notImplemented = true;
			basicSingleShot = false;
			fiveBurstMultiShot = false;
			q4notImplemented = false;
		}
		if (other.gameObject.tag == "q3")
		{
			fiveBurstMultiShot = true;
			q2notImplemented = false;
			basicSingleShot = false;
			q4notImplemented = false;
		}
		if (other.gameObject.tag == "q4")
		{
			q4notImplemented = true;
			q2notImplemented = false;
			fiveBurstMultiShot = false;
			basicSingleShot = false;
		}
		/////////////////////////////////////////////////////*^* q *^*/////////////////////////////////////////////////////

		/////////////////////////////////////////////////////*V* w *V*/////////////////////////////////////////////////////

		if (other.gameObject.tag == "w1")
		{
			threeAbilityDamageMultiplyer = true;
			w2notImplemented = false;
			regenEnhance = false;
			w4notImplemented = false;
		}
		if (other.gameObject.tag == "w2")
		{
			w2notImplemented = true;
			threeAbilityDamageMultiplyer = false;
			regenEnhance = false;
			w4notImplemented = false;
		}
		if (other.gameObject.tag == "w3")
		{
			regenEnhance = true;
			w2notImplemented = false;
			threeAbilityDamageMultiplyer = false;
			w4notImplemented = false;
		}
		if (other.gameObject.tag == "w4")
		{
			w4notImplemented = true;
			w2notImplemented = false;
			regenEnhance = false;
			threeAbilityDamageMultiplyer = false;
		}
		/////////////////////////////////////////////////////*^* w *^*/////////////////////////////////////////////////////

		/////////////////////////////////////////////////////*V* e *V*/////////////////////////////////////////////////////
		if (other.gameObject.tag == "e1")
		{
			speedBoost = true;
			e2notImplemented = false;
			healthBoost = false;
			e4NotImplemented = false;
		}
		if (other.gameObject.tag == "e2")
		{
			e2notImplemented = true;
			speedBoost = false;
			healthBoost = false;
			e4NotImplemented = false;
		};
		if (other.gameObject.tag == "e3")
		{
			healthBoost = true;
			e2notImplemented = false;
			speedBoost = false;
			e4NotImplemented = false;
		}
		if (other.gameObject.tag == "e4")
		{
			e4NotImplemented = true;
			e2notImplemented = false;
			healthBoost = false;
			speedBoost = false;
		}
		/////////////////////////////////////////////////////*^* e *^*/////////////////////////////////////////////////////

		/////////////////////////////////////////////////////*V* r *V*/////////////////////////////////////////////////////
		if (other.gameObject.tag == "r1")
		{
			meteor = true;
			r2notImplemented = false;
			sniper = false;
			armor = false;
		}
		if (other.gameObject.tag == "r2")
		{
			r2notImplemented = true;
			meteor = false;
			sniper = false;
			armor = false;
		}
		if (other.gameObject.tag == "r3")
		{
			sniper = true;
			r2notImplemented = false;
			meteor = false;
			armor = false;
		}
		if (other.gameObject.tag == "r4")
		{
			armor = true;
			r2notImplemented = false;
			sniper = false;
			meteor = false;
		}
		/////////////////////////////////////////////////////*^* r *^*/////////////////////////////////////////////////////


	}

	void hpBoost()
	{
		if (!boostIsOn) 
		{
			speed = 35;
			health = currentHp;
		}

		if(boostIsOn)
		{
			currentHp = health;
			health = health + 150f;
			speed = speed / 2;
		}

	}

    IEnumerator regerateHealth()
    {
        while (true)
        {
            if (regen == true)
            {
                health += healthRegen;
                yield return new WaitForSeconds(1);
                //print("IEnummerator health = " + health);
            }
            else
            {
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

		if(isLocalPlayer)
		{
			//Debug console 

            //Debug console
			bulScale = Vector3.zero;
			bulDmg=0f;

			healthText.text = "Hp: " + health.ToString () + " / " + maxHp;

			if(health<0)
			{
				gameObject.transform.position = new Vector3(0f,5f,0f);
				health = 100;

			}

            

            if (health < maxHp)
            {
                regen = true;
            }
               
            if (health >= maxHp)
            {
                regen = false;
            }

            if (abilityW1IsOn == false)             //W1
            {
                if (abilityQ1IsOn) damage = 15;
                if (abilityR1IsOn) damage = 45;
            }

            if (sprintTimerEnd < Time.time && firing == false && firingQ3 == false )     //E1
            {
                speed = 35;
            }

            if (shotTimerEndR1 > Time.time)
                shotDelay = true;
            if (shotTimerEndR1 < Time.time)
                shotDelay = false;

            if (healingDurrationEnd < Time.time && armorBoost == false)        //W3
            {
                healthRegen = 0.34f;
                maxHp = 100.0f;
                boostedRegeneration = false;
            }

            if (armorUpgradeCooldownEnd < Time.time && armorBoost == true)     //R4
            {
                //print("ArmorBoost Done");
                armorBoost = false;
                
            }

            if (armorUpgradeCooldownEnd < Time.time && armorUpgradeCooldownEnd >= Time.time - 1 && postArmorBoost == true)
            {
                maxHp = 100.0f;
                //health = health + Mathf.Round(((maxHp - health) * 0.2f));
                //print("post armor boost bonus health = " + health);
                postArmorBoost = false;
            }

            /////////////////////////////////////////////////////////////////////////////////////////*** COOLDOWNS FOR Q,W,E,R    vvv  ***//////////////

			///////Visual stuff for CDs
			 
		 

			//////////////////////////////////////////////////////////////////////////*** q ***/////////////////////////////////////////////
			
			if(basicSingleShot && abilityQ1CooldownOn == true){
				img =  GameObject.Find("Q Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;

				txt =  GameObject.Find("Q Ability").GetComponent<Text>();

				float cdStart = abilityQ1CooldownEnd - abilityQ1Cooldown;
				float timeElapsed = Time.time-cdStart;
				float cdTimer = abilityQ1Cooldown -timeElapsed;
				txt.text = (cdTimer).ToString("F1");
			}
			if(basicSingleShot && abilityQ1CooldownEnd<Time.time){
				img =  GameObject.Find("Q Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.yellow;

				txt =  GameObject.Find("Q Ability").GetComponent<Text>();
				txt.text = "Q";
			}
			
			/*if(q2notImplemented && abilityQ2CooldownOn == true){
				img =  GameObject.Find("Q Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;
			}
			if(q2notImplemented && abilityQ2CooldownEnd<Time.time){
				img =  GameObject.Find("Q Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.yellow;
			}*/
			
			if(fiveBurstMultiShot && abilityQ3CooldownOn == true){
				img =  GameObject.Find("Q Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;

				txt =  GameObject.Find("Q Ability").GetComponent<Text>();
				float cdStart = abilityQ3CooldownEnd - abilityQ3Cooldown;
				float timeElapsed = Time.time-cdStart;
				float cdTimer = abilityQ3Cooldown -timeElapsed;
				txt.text = (cdTimer).ToString("F1");
			}
			if(fiveBurstMultiShot && abilityQ3CooldownEnd<Time.time){
				img =  GameObject.Find("Q Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.yellow;

				txt =  GameObject.Find("Q Ability").GetComponent<Text>();
				txt.text = "Q";
			}
			
			/*if(q4notImplemented && abilityQ4CooldownOn == true){
				img =  GameObject.Find("Q Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;
			}
			if(q4notImplemented && abilityQ4CooldownEnd<Time.time){
				img =  GameObject.Find("Q Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.yellow;
			}*/
			
			//////////////////////////////////////////////////////////////////////////*** w ***/////////////////////////////////////////////
			
			if(threeAbilityDamageMultiplyer && abilityW1CooldownOn == true){
				img =  GameObject.Find("W Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;

				txt =  GameObject.Find("W Ability").GetComponent<Text>();
				float cdStart = abilityW1CooldownEnd - abilityW1Cooldown;
				float timeElapsed = Time.time-cdStart;
				float cdTimer = abilityW1Cooldown -timeElapsed;
				txt.text = (cdTimer).ToString("F1");
			}
			if(threeAbilityDamageMultiplyer && abilityW1CooldownEnd<Time.time){
				img =  GameObject.Find("W Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.white;

				txt =  GameObject.Find("W Ability").GetComponent<Text>();

				txt.text = "W";
			}
			

			
			if(regenEnhance && abilityW3CooldownOn == true){
				img =  GameObject.Find("W Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;

				txt =  GameObject.Find("W Ability").GetComponent<Text>();
				float cdStart = abilityW3CooldownEnd - abilityW3Cooldown;
				float timeElapsed = Time.time-cdStart;
				float cdTimer = abilityW3Cooldown -timeElapsed;
				txt.text = (cdTimer).ToString("F1");
			}
			if(regenEnhance && abilityW3CooldownEnd<Time.time){
				img =  GameObject.Find("W Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.white;

				txt =  GameObject.Find("W Ability").GetComponent<Text>();

				txt.text = "W";
			}

			
			//////////////////////////////////////////////////////////////////////////*** e ***/////////////////////////////////////////////
			
			if(speedBoost && abilityE1CooldownOn == true){
				img =  GameObject.Find("E Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;

				txt =  GameObject.Find("E Ability").GetComponent<Text>();
				float cdStart = abilityE1CooldownEnd - abilityE1Cooldown;
				float timeElapsed = Time.time-cdStart;
				float cdTimer = abilityE1Cooldown -timeElapsed;
				txt.text = (cdTimer).ToString("F1");
			}
			if( abilityE1CooldownEnd<Time.time){
				img =  GameObject.Find("E Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.blue;

				txt =  GameObject.Find("E Ability").GetComponent<Text>();

				txt.text = "E";
			}

			
			if(healthBoost && abilityE3CooldownOn == true){
				img =  GameObject.Find("E Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;

				txt =  GameObject.Find("E Ability").GetComponent<Text>();
				float cdStart = abilityE3CooldownEnd - abilityE3Cooldown;
				float timeElapsed = Time.time-cdStart;
				float cdTimer = abilityE3Cooldown -timeElapsed;
				txt.text = (cdTimer).ToString("F1");
			}
			if(healthBoost && abilityE3CooldownEnd<Time.time){
				img =  GameObject.Find("E Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.blue;

				
				txt =  GameObject.Find("E Ability").GetComponent<Text>();
				txt.text = "E";
			}
			
			//////////////////////////////////////////////////////////////////////////*** r ***/////////////////////////////////////////////
			
			if(meteor && abilityR1CooldownOn == true){
				img =  GameObject.Find("R Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;

				
				txt =  GameObject.Find("R Ability").GetComponent<Text>();
				float cdStart = abilityR1CooldownEnd - abilityR1Cooldown;
				float timeElapsed = Time.time-cdStart;
				float cdTimer = abilityR1Cooldown -timeElapsed;
				txt.text = (cdTimer).ToString("F1");
			}
			if(meteor && abilityR1CooldownEnd<Time.time){
				img =  GameObject.Find("R Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.red;

				
				txt =  GameObject.Find("R Ability").GetComponent<Text>();
				txt.text = "R";
			}

			
			if(sniper && abilityR3CooldownOn == true){
				img =  GameObject.Find("R Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.black;

				txt =  GameObject.Find("R Ability").GetComponent<Text>();
				float cdStart = abilityR3CooldownEnd - abilityR3Cooldown;
				float timeElapsed = Time.time-cdStart;
				float cdTimer = abilityR3Cooldown -timeElapsed;
				txt.text = (cdTimer).ToString("F1");
			}
			if(sniper && abilityR3CooldownEnd<Time.time){
				img =  GameObject.Find("R Panel").GetComponent<Image>();
				img.color = UnityEngine.Color.red;

				txt =  GameObject.Find("R Ability").GetComponent<Text>();
				txt.text = "R";
			}



            //////////////////////////////////////////////////////////////////////*** q CD end vvv ***//////////////////////////////////////
            


			if (abilityQ1CooldownEnd<Time.time)
			{
				abilityQ1CooldownOn = false;
			}

            if (abilityQ2CooldownEnd < Time.time)
            {
                abilityQ2CooldownOn = false;
            }

			if (abilityQ3CooldownEnd < Time.time && abilityQ3CooldownOn == true)
			{
				print("Q3 is ready");
				ShotCountQ3 = 0;
				abilityQ3CooldownOn = false;
			}

				if (Input.GetKeyDown("q") && abilityQ3IsOn == true && fiveBurstMultiShot == true & firingQ3 == true)
				{
					print("Q3 shot cancled");
					abilityQ3CooldownEnd = Time.time + ((abilityQ3Cooldown / 3)*ShotCountQ3);
					abilityQ3CooldownOn = true;
					abilityQ3IsOn = false;
					firingQ3 = false;
				}

            if (abilityQ4CooldownEnd < Time.time)
            {
                abilityQ4CooldownOn = false;
            }
            //////////////////////////////////////////////////////////////////////*** q CD end ^^^ ***//////////////////////////////////////

            //////////////////////////////////////////////////////////////////////*** w CD end vvv ***//////////////////////////////////////
            if (abilityW1CooldownEnd<Time.time)
			{
				abilityW1CooldownOn = false;
			}

            if (abilityW2CooldownEnd < Time.time)
            {
                abilityW2CooldownOn = false;
            }

            if (abilityW3CooldownEnd < Time.time)
            {
                abilityW3CooldownOn = false;
            }

            if (abilityW4CooldownEnd < Time.time)
            {
                abilityW4CooldownOn = false;
            }
            //////////////////////////////////////////////////////////////////////*** w CD end ^^^ ***//////////////////////////////////////

            //////////////////////////////////////////////////////////////////////*** e CD end vvv ***//////////////////////////////////////
            if (abilityE1CooldownEnd<Time.time)
			{
				abilityE1CooldownOn = false;
			}

            if (abilityE2CooldownEnd < Time.time)
            {
                abilityE2CooldownOn = false;
            }

            if (abilityE3CooldownEnd < Time.time)
            {
                abilityE3CooldownOn = false;
            }

            if (abilityE4CooldownEnd < Time.time)
            {
                abilityE4CooldownOn = false;
            }

            //////////////////////////////////////////////////////////////////////*** e CD end ^^^ ***//////////////////////////////////////

            //////////////////////////////////////////////////////////////////////*** r CD end vvv ***//////////////////////////////////////
            if (abilityR1CooldownEnd < Time.time && abilityR1CooldownOn == true)
			{
                //print("R1 is ready");
                ShotCount = 0;
				abilityR1CooldownOn = false;
			}
                if (Input.GetKeyDown("r") && abilityR1IsOn == true && meteor == true & firing == true)
                {
                    //print("shot cancled");
                    startingScale = 0.0f;
                    abilityR1CooldownEnd = Time.time + ((abilityR1Cooldown / 3)*ShotCount);
                    abilityR1CooldownOn = true;
                    abilityR1IsOn = false;
                    firing = false;
                }

                if (targTimerEnd < Time.time)
                {
                    targTimerOn = false;
                }

                if (targTimerOn == false)
                {
                    targeting.SetActive(false);
                    ghostTarget.SetActive(false);
                }

            if (abilityR2CooldownEnd < Time.time)
            {
                abilityR2CooldownOn = false;
            }

            if (abilityR3CooldownEnd<Time.time)
			{
				abilityR3CooldownOn = false;
			}

            if (abilityR4CooldownEnd < Time.time)
            {
                abilityR4CooldownOn = false;
            }
            //////////////////////////////////////////////////////////////////////*** r CD end ^^^ ***//////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////*** COOLDOWNS FOR Q,W,E,R    ^^^  ***//////////////

            /////////////////////////////////////////////////////////////////////////////////////////*** TOGGLES THE NEXT SHOT'S ACTIVE BASED ON Q,W,E,R    vvv  ***//////////////
            ////////////////////////////////////////////////////////////////////////////////*** q on vvv ***///////////////////////////////////////////////
            if (Input.GetKeyDown("q") && abilityQ1CooldownOn == false && basicSingleShot == true)
			{
                abilityQ2IsOn = false;
                abilityQ3IsOn = false;
                abilityQ4IsOn = false;
                abilityW2IsOn = false;
                abilityW4IsOn = false;
                abilityE2IsOn = false;
                abilityR1IsOn = false;
                abilityR2IsOn = false;
                abilityR3IsOn = false;
                abilityQ1IsOn = !abilityQ1IsOn;
                //print("Q1 is: " + abilityQ1IsOn);
			}

            if (Input.GetKeyDown("q") && abilityQ2CooldownOn == false && q2notImplemented == true)
            {
                abilityQ1IsOn = false;
                abilityQ3IsOn = false;
                abilityQ4IsOn = false;
                abilityW2IsOn = false;
                abilityE2IsOn = false;
                abilityW4IsOn = false;
                abilityR1IsOn = false;
                abilityR2IsOn = false;
                abilityR3IsOn = false;
                abilityQ2IsOn = !abilityQ2IsOn;
                //print("Q2 is: " + abilityQ2IsOn);
            }

            if (Input.GetKeyDown("q") && abilityQ3CooldownOn == false && fiveBurstMultiShot == true)
            {
                abilityQ2IsOn = false;
                abilityQ1IsOn = false;
                abilityQ4IsOn = false;
                abilityW2IsOn = false;
                abilityW4IsOn = false;
                abilityE2IsOn = false;
                abilityR1IsOn = false;
                abilityR2IsOn = false;
                abilityR3IsOn = false;
                abilityQ3IsOn = !abilityQ3IsOn;
                //print("Q3 is: " + abilityQ3IsOn);
				firingQ3 = true;
            }

            if (Input.GetKeyDown("q") && abilityQ4CooldownOn == false && q4notImplemented == true)
            {
                abilityQ2IsOn = false;
                abilityQ3IsOn = false;
                abilityQ1IsOn = false;
                abilityW2IsOn = false;
                abilityW4IsOn = false;
                abilityE2IsOn = false;
                abilityR1IsOn = false;
                abilityR2IsOn = false;
                abilityR3IsOn = false;
                abilityQ4IsOn = !abilityQ4IsOn;
                //print("Q4 is: " + abilityQ4IsOn);
            }
            ////////////////////////////////////////////////////////////////////////////////*** q on ^^^ ***///////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////*** w on vvv ***///////////////////////////////////////////////
            if (Input.GetKeyDown("w") && threeAbilityDamageMultiplyer == true) // && ability2CooldownOn == false
			{
                abilityW1IsOn = !abilityW1IsOn;
                //print("W1 is: " + abilityW1IsOn);
			}

            if (Input.GetKeyDown("w") && abilityW2CooldownOn == false && w2notImplemented == true)
            {
                abilityQ1IsOn = false;
                abilityQ2IsOn = false;
                abilityQ3IsOn = false;
                abilityQ4IsOn = false;
                abilityW4IsOn = false;
                abilityE2IsOn = false;
                abilityR1IsOn = false;
                abilityR2IsOn = false;
                abilityR3IsOn = false;
                abilityW2IsOn = !abilityW2IsOn;
                //print("W2 is: " + abilityW2IsOn);
            }

            if (Input.GetKeyDown("w") && abilityW3CooldownOn == false && regenEnhance == true)
            {
                abilityW3IsOn = !abilityW3IsOn;
                //print("W3 is: " + abilityW3IsOn);
            }

            if (Input.GetKeyDown("w") && abilityW4CooldownOn == false && w4notImplemented == true)
            {
                abilityQ1IsOn = false;
                abilityQ2IsOn = false;
                abilityQ3IsOn = false;
                abilityQ4IsOn = false;
                abilityW2IsOn = false;
                abilityE2IsOn = false;
                abilityR1IsOn = false;
                abilityR2IsOn = false;
                abilityR3IsOn = false;
                abilityW4IsOn = !abilityW4IsOn;
                //print("W4 is: " + abilityW4IsOn);
            }
            ////////////////////////////////////////////////////////////////////////////////*** w on ^^^ ***///////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////*** e on vvv ***///////////////////////////////////////////////
            if (Input.GetKeyDown("e") && abilityE1CooldownOn == false && speedBoost == true)
			{
				abilityE1IsOn = !abilityE1IsOn;
                //print("E1 is: " + abilityE1IsOn);
			}

            if (Input.GetKeyDown("e") && abilityE2CooldownOn == false && e2notImplemented == true)
            {
                abilityQ1IsOn = false;
                abilityQ2IsOn = false;
                abilityQ3IsOn = false;
                abilityQ4IsOn = false;
                abilityW2IsOn = false;
                abilityR1IsOn = false;
                abilityR2IsOn = false;
                abilityR3IsOn = false;
                abilityE2IsOn = !abilityE2IsOn;
                //print("E2 is: " + abilityE2IsOn);
            }

            if (Input.GetKeyDown("e") && abilityE3CooldownOn == false && healthBoost == true)
            {
                abilityE3IsOn = !abilityE3IsOn;
                //print("E3 is: " + abilityE3IsOn);
            }

            if (Input.GetKeyDown("e") && abilityE4CooldownOn == false && e4NotImplemented == true)
            {
                abilityE4IsOn = !abilityE4IsOn;
                //print("E4 is: " + abilityE4IsOn);
            }
            ////////////////////////////////////////////////////////////////////////////////*** e on ^^^ ***///////////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////*** r on vvv ***///////////////////////////////////////////////
            if (Input.GetKeyDown("r") && abilityR1CooldownOn == false && meteor == true)
            {
                abilityQ1IsOn = false;
                abilityQ2IsOn = false;
                abilityQ3IsOn = false;
                abilityQ4IsOn = false;
                abilityW2IsOn = false;
                abilityW4IsOn = false;
                abilityE2IsOn = false;
                abilityR2IsOn = false;
                abilityR3IsOn = false;
                abilityR1IsOn = !abilityR1IsOn;                
                //print("R1 is: " + abilityR1IsOn);
                firing = true;
			}
            

                if (Input.GetKeyDown("r") && abilityR2CooldownOn == false && r2notImplemented == true)
			{
                abilityQ1IsOn = false;
                abilityQ2IsOn = false;
                abilityQ3IsOn = false;
                abilityQ4IsOn = false;
                abilityW2IsOn = false;
                abilityW4IsOn = false;
                abilityE2IsOn = false;
                abilityR1IsOn = false;
                abilityR3IsOn = false;
                abilityR2IsOn = !abilityR2IsOn;				
				print("R2 is: " + abilityR2IsOn);
			}

			if(Input.GetKeyDown("r") && abilityR3CooldownOn == false && sniper == true)
			{
                abilityQ1IsOn = false;
                abilityQ2IsOn = false;
                abilityQ3IsOn = false;
                abilityQ4IsOn = false;
                abilityW2IsOn = false;
                abilityW4IsOn = false;
                abilityE2IsOn = false;
                abilityR1IsOn = false;
                abilityR2IsOn = false;
                abilityR3IsOn = !abilityR3IsOn;				
				print("R3 is: " + abilityR3IsOn);
			}

            if (Input.GetKeyDown("r") && abilityR4CooldownOn == false && armor== true)
            {
                abilityR4IsOn = !abilityR4IsOn;
                //print("R4 is: " + abilityR4IsOn);
            }
            ////////////////////////////////////////////////////////////////////////////////*** r on ^^^ ***///////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////*** TOGGLES THE NEXT SHOT'S ACTIVE BASED ON Q,W,E,R    ^^^  ***//////////////

            /////////////////////////////////////////////////////////////////////////////////////////*** RESETS THE ACTIVE SHOT FOR Q,W,E,R    vvv  ***//////////////
            if (Input.GetKeyDown("s"))
			{
                if (abilityQ1IsOn) abilityQ1IsOn = false;
                if (abilityQ2IsOn) abilityQ2IsOn = false;
                if (abilityQ3IsOn) abilityQ3IsOn = false;
                if (abilityQ4IsOn) abilityQ4IsOn = false;
                
                if (abilityW1IsOn) abilityW1IsOn = false;
                if (abilityW2IsOn) abilityW2IsOn = false;
                if (abilityW3IsOn) abilityW3IsOn = false;
                if (abilityW4IsOn) abilityW4IsOn = false;

                if (abilityE1IsOn) abilityE1IsOn = false;
                if (abilityE2IsOn) abilityE2IsOn = false;
                if (abilityE3IsOn) abilityE3IsOn = false;
                if (abilityE4IsOn) abilityE4IsOn = false;

                if (abilityR1IsOn) abilityR1IsOn = false;
                if (abilityR2IsOn) abilityR2IsOn = false;
                if (abilityR3IsOn) abilityR3IsOn = false;
                if (abilityR4IsOn) abilityR4IsOn = false;

                destination = transform.position;
				inst.SetActive(false); 
				
			}
			/////////////////////////////////////////////////////////////////////////////////////////*** RESETS THE ACTIVE SHOT FOR Q,W,E,R    ^^^  ***//////////////

			/////////////////////////////////////////////////////////////////////////////////////////*** GETS MOUSE CLICK POSITION IN THE SCENE    vvv  ***//////////////
			if (Input.GetMouseButtonDown(1))
			{
				Ray ray = (playerCam.ScreenPointToRay(Input.mousePosition)); //create the ray
					RaycastHit hit; //create the var that will hold the information of where the ray hit
					
					if (Physics.Raycast(ray, out hit)) //did we hit something?
					{
							if (hit.transform.name == "Ground") //did we hit the ground?	
						{
							//print (hit.point);
							destination = hit.point; //set the destinatin to the vector3 where the ground was contacted
							destination.y = 5f;
							inst.SetActive(true);
							inst.transform.position = destination;

						}
					}
			}
			/////////////////////////////////////////////////////////////////////////////////////////*** GETS MOUSE CLICK POSITION IN THE SCENE    ^^^  ***//////////////

			/////////////////////////////////////////////////////////////////////////////////////////*** MOVES THE PLAYER    vvv  ***//////////////
			if (Vector3.Distance(mover.position, destination) > SnapTo)
			{
				mover.position = Vector3.MoveTowards(mover.transform.position, destination, Time.deltaTime * speed);
				//transform.LookAt(destination);

			}
			
			if(Vector3.Distance(mover.position, destination) < SnapTo)
			{
				mover.position = destination; //snap to destination
				inst.SetActive(false);
			}
			/////////////////////////////////////////////////////////////////////////////////////////*** MOVES THE PLAYER    ^^^  ***//////////////
            


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////*** vvv Abilities vvv ***//////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** W1 ***//////////////////////////////////////////////////
            if(threeAbilityDamageMultiplyer == true)
			{

				if (abilityW1IsOn && ((abilityQ1IsOn && abilityQ1CooldownOn == false) || (abilityQ2IsOn && abilityQ2CooldownOn == false) || (abilityQ3IsOn && abilityQ3CooldownOn == false) || (abilityQ4IsOn && abilityQ4CooldownOn == false)) && Input.GetMouseButtonDown(0)) //&& ability1On && Input.GetMouseButtonDown(0)) || (ability2On && ability4On && Input.GetMouseButtonDown(0)
	            {
	                damage = 15;
	                wBuff();
	                counter = counter + 1;
	            }
				if (abilityW1IsOn && ( (abilityR2IsOn && abilityR2CooldownOn == false) || (abilityR3IsOn && abilityR3CooldownOn == false) || (abilityW4IsOn && abilityW4CooldownOn == false)) && Input.GetMouseButtonDown(0)) //&& ability1On && Input.GetMouseButtonDown(0)) || (ability2On && ability4On && Input.GetMouseButtonDown(0)

	            {
	                damage = 45;
	                wBuff();
	                counter = counter + 1;
	            }
			} 
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** W2 ***//////////////////////////////////////////////////
			if(w2notImplemented == true)
			{
				
			}
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** W3 ***//////////////////////////////////////////////////
			if(regenEnhance == true)
			{
                if (abilityW3IsOn == true && abilityW3CooldownOn == false)
                {
                    boostedRegeneration = true;
                    healthRegen = 10.0f;
                    abilityW3CooldownEnd = Time.time + abilityW3Cooldown;
                    healingDurrationEnd = Time.time + healingDurration;
                    abilityW3CooldownOn = true;
                    abilityW3IsOn = false;
                }	
            }
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** W4 ***//////////////////////////////////////////////////
			if(w4notImplemented == true)
			{
				
			}
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** Q1 ***//////////////////////////////////////////////////
			if(basicSingleShot == true)
			{

				if(abilityQ1IsOn == true && Input.GetMouseButtonDown(0) && abilityQ1CooldownOn == false)
				{
					// Get the mouse clicked location for left click!
					GetMouseBulletClickLocation();
	                
					print("shoot a bullet here: " + "x: " + bulletDestination.x + " and z: " + bulletDestination.z);

						//does this ability spawn moving bullets? DEFAULT IS YES. Don't change it unless you dont want to move.
						//It's processor intensive so setting it to true would just waste CPU power
						//bulletManagerScript.bulletList[i].GetComponent<BulletScript>().doWeMove = true;
						doesItMove = true;

						//bulletManagerScript.bulletList[i].GetComponent<BulletScript>().speedZ = 100f;
                        //bulletManagerScript.bulletList[i].GetComponent<BulletScript>().speedY = 0.0f;
						zSpeed = 100f;
						ySpeed = 0f;
						xSpeed = 0f;
					//damage = 10;
						//set the position of the bullet you took out of the list to the players position!
							//use this for bombs,buffs,shields whatever else that aoe's/spawns right on top of you.
						//bulletManagerScript.bulletList[i].transform.position = gameObject.transform.position;
						
	                    //bulletManagerScript.bulletList[i].transform.localScale = new Vector3(5F, 5f, 5f);
						bulScale = new Vector3(5F, 5f, 5f); 

						//this is the offest so the bullet doesnt spawn in the player
						if(bulletDestination.z>transform.position.z)
						{
							zPosOff = 10f;
						//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z+10f);
						}
						else {zPosOff = 0f;}

						if(bulletDestination.z<transform.position.z)
						{
						zNegOff = -10f;
							//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z-10f);
						}

						else{zNegOff = 0f;}

						if(bulletDestination.x<transform.position.x)
						{
						xNegOff = -10f;
							//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x-10f, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z);
						}
					else{xNegOff = 0f;}
						if(bulletDestination.x>transform.position.x)
						{
						xPosOff = 10f;
							//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x+10f, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z);
						}
					else{xPosOff = 0f;}
						/*this will set it at the mouse curser, where you click after pressing q
								use this for abilities such as aoe at target location*/
						//bulletManagerScript.bulletList[i].transform.position = bulletDestination;

						//this sets the rotation to look where you click so it moves forward ie toward your click
						//bulletManagerScript.bulletList[i].transform.LookAt(bulletDestination);
						if(!isServer){														// float XPosOff,float ZPosOff, float XNegOff, float ZNegOff,
						CmdspawnBullet(new Vector3(transform.position.x,transform.position.y,transform.position.z),xPosOff,zPosOff,xNegOff, zNegOff, bulScale, xSpeed, ySpeed, zSpeed, doesItMove, bulletDestination,damage);
						}
						if(isServer)
						{
						spawnBullet(new Vector3(transform.position.x,transform.position.y,transform.position.z),xPosOff,zPosOff,xNegOff, zNegOff, bulScale, xSpeed, ySpeed, zSpeed, doesItMove, bulletDestination,damage);
						}
						//exit the ability1on loop
						abilityQ1IsOn = false; // don't know if this does anything currently but it works so I left it in there.

					abilityQ1CooldownEnd = Time.time + abilityQ1Cooldown;
	                
					abilityQ1CooldownOn = true;
					abilityQ1IsOn = false;
	                //print("Q1damage: " + damage);
				}
			}
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** Q2 ***//////////////////////////////////////////////////

			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** Q3 ***//////////////////////////////////////////////////
			if(fiveBurstMultiShot == true)
			{
				if(abilityQ3IsOn == true && Input.GetMouseButtonDown(0) && abilityQ3CooldownOn == false && ShotCountQ3 < 5 && firingQ3 == true)
				{
					// Get the mouse clicked location for left click!
					GetMouseBulletClickLocation();
					damage = 3.0f;

					print("shoot a bullet here: " + "x: " + bulletDestination.x + " and z: " + bulletDestination.z);
					

						//bulletManagerScript.bulletList[i].GetComponent<BulletScript>().doWeMove = true;
						doesItMove = true;
						
						//bulletManagerScript.bulletList[i].GetComponent<BulletScript>().speedZ = 125f;
						//bulletManagerScript.bulletList[i].GetComponent<BulletScript>().speedY = 0.0f;
						xSpeed = 0;
						ySpeed = 0;
						zSpeed = 125f;
						
						bulScale = new Vector3(5f,5f,5f);

						//bulletManagerScript.bulletList[i].transform.localScale = new Vector3(5F, 5f, 5f);
						
						//this is the offest so the bullet doesnt spawn in the player
					//this is the offest so the bullet doesnt spawn in the player
					if(bulletDestination.z>transform.position.z)
					{
						zPosOff = 10f;
						//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z+10f);
					}
					else {zPosOff = 0f;}
					
					if(bulletDestination.z<transform.position.z)
					{
						zNegOff = -10f;
						//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z-10f);
					}
					
					else{zNegOff = 0f;}
					
					if(bulletDestination.x<transform.position.x)
					{
						xNegOff = -10f;
						//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x-10f, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z);
					}
					else{xNegOff = 0f;}
					if(bulletDestination.x>transform.position.x)
					{
						xPosOff = 10f;
						//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x+10f, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z);
					}
					else{xPosOff = 0f;}

					print ("bullet scale is:" + bulScale);
					if(!isServer){														// float XPosOff,float ZPosOff, float XNegOff, float ZNegOff,
						CmdspawnBullet(new Vector3(transform.position.x,transform.position.y,transform.position.z),xPosOff,zPosOff,xNegOff, zNegOff, bulScale, xSpeed, ySpeed, zSpeed, doesItMove, bulletDestination,damage);
					}
					if(isServer)
					{
						spawnBullet(new Vector3(transform.position.x,transform.position.y,transform.position.z),xPosOff,zPosOff,xNegOff, zNegOff, bulScale, xSpeed, ySpeed, zSpeed, doesItMove, bulletDestination,damage);
					}
						
				}
					if (ShotCountQ3 >= 5 && firingQ3 == true)
					{
						firingQ3 = false;
						abilityQ3CooldownEnd = Time.time + abilityQ3Cooldown;

						abilityQ3CooldownOn = true;
						abilityQ3IsOn = false;
						print("Q3 damage: " + damage);
					}

				if (Input.GetMouseButtonDown(0) && abilityQ3IsOn == true && abilityQ3CooldownOn == false && firingQ3 == true)
				{
					ShotCountQ3 ++;
					print("Q3 shot Count = " + ShotCountQ3);
				}
			}
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** Q4 ***//////////////////////////////////////////////////
			if(q4notImplemented == true)
			{
				//thicker slower q1
			}
            

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** E1 ***//////////////////////////////////////////////////
			if (speedBoost == true)
			{
				if(!abilityE1CooldownOn && abilityE1IsOn)
				{
					speed = 70f;
					abilityE1CooldownEnd = Time.time + abilityE1Cooldown;
					sprintTimerEnd = Time.time + sprintTimer;
					abilityE1CooldownOn = true;
					abilityE1IsOn = false;
	                //counter = counter + 1; 
	                //print("changing speed!");

				}
			}
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** E2 ***//////////////////////////////////////////////////
			if(e2notImplemented)
			{
				
			}
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** E3 ***//////////////////////////////////////////////////
			if(healthBoost)
			{
				boostIsOn = !boostIsOn;
				hpBoost();

				
			}
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** E4 ***//////////////////////////////////////////////////
			if(e4NotImplemented)
			{
				
			}

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** R1 ***//////////////////////////////////////////////////
            if (meteor)
			{
				if (abilityR1IsOn == true && Input.GetMouseButtonDown(0) && abilityR1CooldownOn == false && firing == true && shotDelay == false)
	            {
                    
                    
	                // Get the mouse clicked location for left click!
	                GetMouseBulletClickLocation();
                    shotTimerEndR1 = shotTimerR1 + Time.time;
                    speed = 0f;

	                //print("shoot a bullet here: " + "x: " + bulletDestination.x + " y: " + bulletDestination.y + " and z: " + bulletDestination.z);
	                //go through the list of bullets one by one


                            //does this ability spawn moving bullets?
							doesItMove = true;
								
                            //this is how fast the bullet will go
							xSpeed = 0f;
							ySpeed = 0f;
							zSpeed = 500.0f;

                            //bulletManagerScript.bulletList[i].transform.localScale = new Vector3(startingScale + scale, startingScale + scale, startingScale + scale);
							bulScale = new Vector3(25f, 25f, 25f);
                            targeting.transform.localScale = new Vector3( scale, 0.02f,  scale);
                            

                            bulletDestination.y = 500.0f;
                           
                            targeting.SetActive(true);
                            bulletDestination.y = -5.0f;
                            ghostTarget.SetActive(true);
                            ghostTarget.transform.position = bulletDestination;
                            targTimerEnd = Time.time + targTimer;
                            targTimerOn = true;
                            
                        
					if(!isServer){														// float XPosOff,float ZPosOff, float XNegOff, float ZNegOff,
						CmdspawnBullet(new Vector3(bulletDestination.x,bulletDestination.y+500f,bulletDestination.z),xPosOff,zPosOff,xNegOff, zNegOff, bulScale, xSpeed, ySpeed, zSpeed, doesItMove, bulletDestination,damage);
					}
					if(isServer)
					{
						spawnBullet(new Vector3(bulletDestination.x,bulletDestination.y+500f,bulletDestination.z),xPosOff,zPosOff,xNegOff, zNegOff, bulScale, xSpeed, ySpeed, zSpeed, doesItMove, bulletDestination,damage);
					}
                            
					//bulScale = new Vector3(5f,5f,5f);
                    
	            }
                if (ShotCount >= 3 && firing == true)
                {
                    firing = false;
                    abilityR1CooldownEnd = Time.time + abilityR1Cooldown;
                    abilityR1CooldownOn = true;
                    abilityR1IsOn = false;
                    //bulletManagerScript.bullet.transform.localScale = new Vector3(5F, 5f, 5f);
                    //print("R damage: " + damage);
					meteor = false;
					print ("meteor bullet scale" + scale);
					//scale = 5f;
					bulScale = new Vector3(5f,5f,5f);
                }

				if (Input.GetMouseButtonDown(0) && abilityR1IsOn == true && abilityR1CooldownOn == false && shotDelay == false)
				{
					ShotCount = ShotCount + 1;
					shotTimerEndR1 = shotTimerR1 + Time.time;
					//print("shot Count = " + ShotCount);
				}

			}
            


			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*** R3 ***//////////////////////////////////////////////////
			if(sniper)
			{
				if (abilityR3IsOn == true && Input.GetMouseButtonDown(0) && abilityR3CooldownOn == false)
				{
					speed = 0f;
					// Get the mouse clicked location for left click!
					GetMouseBulletClickLocation();
					
					print("shoot a bullet here: " + "x: " + bulletDestination.x + " and z: " + bulletDestination.z);
					//go through the list of bullets one by one

						//does this ability spawn moving bullets?
						//bulletManagerScript.bulletList[i].GetComponent<BulletScript>().doWeMove = true;
						doesItMove = true;
						//this is how fast the bullet will go
					xSpeed = 0f;
					ySpeed = 0f;
					zSpeed = 300.0f;
						//bulletManagerScript.bulletList[i].GetComponent<BulletScript>().speedZ = 300.0f;
                       // bulletManagerScript.bulletList[i].GetComponent<BulletScript>().speedY = 0.0f;
						
						
						
						//set the position of the bullet you took out of the list to the players position!
						//use this for bombs,buffs,shields whatever else that aoe's/spawns right on top of you.
						//bulletPos = bulletManagerScript.bulletList[i].transform.position = gameObject.transform.position;
						
						//bulletManagerScript.bulletList[i].transform.LookAt(bulletDestination);
						
						//bulletManagerScript.bulletList[i].transform.localScale = new Vector3(3F, 3f, 15f);
						bulScale = new Vector3(10f,10f,10f);
						

					if(bulletDestination.z>transform.position.z)
					{
						zPosOff = 13f;
						//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z+10f);
					}
					else {zPosOff = 0f;}
					
					if(bulletDestination.z<transform.position.z)
					{
						zNegOff = -13f;
						//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z-10f);
					}
					
					else{zNegOff = 0f;}
					
					if(bulletDestination.x<transform.position.x)
					{
						xNegOff = -13f;
						//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x-10f, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z);
					}
					else{xNegOff = 0f;}
					if(bulletDestination.x>transform.position.x)
					{
						xPosOff = 13f;
						//bulletManagerScript.bulletList[i].transform.position = new Vector3(bulletManagerScript.bulletList[i].transform.position.x+10f, bulletManagerScript.bulletList[i].transform.position.y, bulletManagerScript.bulletList[i].transform.position.z);
					}
					else{xPosOff = 0f;}
		
					if(!isServer){														// float XPosOff,float ZPosOff, float XNegOff, float ZNegOff,
						CmdspawnBullet(new Vector3(transform.position.x,transform.position.y,transform.position.z),xPosOff,zPosOff,xNegOff, zNegOff, bulScale, xSpeed, ySpeed, zSpeed, doesItMove, bulletDestination,damage);
					}
					if(isServer)
					{
						spawnBullet(new Vector3(transform.position.x,transform.position.y,transform.position.z),xPosOff,zPosOff,xNegOff, zNegOff, bulScale, xSpeed, ySpeed, zSpeed, doesItMove, bulletDestination,damage);
					}
						

                        //exit the ability1on loop
                        abilityR3IsOn = false; // don't know if this does anything currently but it works so I left it in there.
				
					abilityR3CooldownEnd = Time.time + abilityR3Cooldown;
					abilityR3CooldownOn = true;
					abilityR3IsOn = false;
					//bulletManagerScript.bullet.transform.localScale = new Vector3(5F, 5f, 5f);
					//print("R damage: " + damage);
				}
			}


		}

	}


	/////////////////////////////////////////////////////////////////////////////////////////*** SETS THE MOUSE CLICK POSITION AS A VARIABLE    vvv  ***//////////////
	void GetMouseBulletClickLocation()
	{
		Ray ray = (playerCam.ScreenPointToRay(Input.mousePosition)); //create the ray
		RaycastHit hit; //create the var that will hold the information of where the ray hit
        
		if (Physics.Raycast(ray, out hit)) //did we hit something?
			if (hit.transform.name == "Ground") //did we hit the ground?	
				bulletDestination = hit.point;
		bulletDestination.y = 5f;
        targeting.transform.position = bulletDestination;
		//abilityOffsetx = gameObject.transform.position.x - bulletDestination.x;
		//abilityOffsetz = gameObject.transform.position.z - bulletDestination.z;
	}
	/////////////////////////////////////////////////////////////////////////////////////////*** SETS THE MOUSE CLICK POSITION AS A VARIABLE    ^^^  ***//////////////
	/// float xPosOff, yPosOff, xNegOff,yNegOff;
	///Vector3 bulScale;
	///float zSpeed, ySpeed;
	///bool doesItMove;
	///bulDest
	void spawnBullet(Vector3 parent, float XPosOff,float ZPosOff, float XNegOff, float ZNegOff, Vector3 scaleFactor, float XSpeed, float YSpeed, float ZSpeed, bool Mover, Vector3 bulletTarget, float Dmg)
	{
		print (ZSpeed);
		//set the transform of the bullet then instantiate and spawn
		GetMouseBulletClickLocation ();
		GameObject bullet = bulletPrefab;
		//print ("Spawn " + bullet);
		//bullet.SetActive (true);
		Vector3 bulletPos =  new Vector3(parent.x + XPosOff + XNegOff, parent.y, parent.z + ZNegOff + ZPosOff);
		ClientScene.RegisterPrefab(bullet);

		bullet.GetComponent<BulletScript>().targetLocation = bulletTarget;
		//print ("target location in playerFunction: " + bullet.GetComponent<BulletScript>().targetLocation);
		bullet.transform.position = bulletPos;
		bullet.transform.localScale = scaleFactor;
		bullet.GetComponent<BulletScript>().speedZ = ZSpeed;
		bullet.GetComponent<BulletScript>().speedY = YSpeed;
		bullet.GetComponent<BulletScript>().speedX = XSpeed;
		bullet.GetComponent<BulletScript>().doWeMove = Mover;
		bullet.GetComponent<BulletScript>().dmg = Dmg;
		GameObject.Instantiate(bullet, bulletPos, Quaternion.identity);



		
	}

	[Command]
	void CmdspawnBullet(Vector3 parent, float XPosOff,float ZPosOff, float XNegOff, float ZNegOff, Vector3 scaleFactor, float XSpeed, float YSpeed, float ZSpeed, bool Mover, Vector3 bulletTarget, float Dmg)
	{
		GetMouseBulletClickLocation ();
		//set the transform of the bullet then instantiate and spawn
		GameObject bullet = bulletPrefab;
		//print ("Spawn " + bullet);
		//bullet.SetActive (true);
		Vector3 bulletPos =  new Vector3(parent.x + XPosOff + XNegOff, parent.y, parent.z + ZNegOff + ZPosOff);
		ClientScene.RegisterPrefab(bullet);
		bullet.GetComponent<BulletScript>().targetLocation = bulletTarget;

		Vector3 bulTargLocation = bulletTarget;
		//print (bulletTarget);
		bullet.transform.position = bulletPos;
		bullet.transform.localScale = scaleFactor;
		bullet.GetComponent<BulletScript>().speedZ = ZSpeed;
		bullet.GetComponent<BulletScript>().speedY = YSpeed;
		bullet.GetComponent<BulletScript>().speedX = XSpeed;
		bullet.GetComponent<BulletScript>().doWeMove = Mover;
		bullet.GetComponent<BulletScript>().dmg = Dmg;

		GameObject.Instantiate(bullet, bulletPos, Quaternion.identity);

		



	}
}
