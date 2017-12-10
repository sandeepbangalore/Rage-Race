/*
 *                                  Team Rocket 
 * 
 *  Agneya A Kerure                 akerure3        kerure.agneya@gatech.edu
 *  Christina Chung                 cchung44        cchung44@gatech.edu
 *  Erin Hsu                        ehsu7           ehsu7@gatech.edu
 *  Dibyendu Mondal                 dmondal6        dibyendu@gatech.edu
 *  Sandeep Banaglore Venkatesh     sbv7            sandeepbanaglore@gatech.edu
 * 
*/


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;


public class PlayerPowerupScript : MonoBehaviour {

    //[SerializeField] private Transform respawnPoint;

	private ThirdPersonCharacter moveScript;
	private AIController AIscript;
    private PositionManager positionManager;
	public float maxTime = 2.0f;
    public float maxBoostTime = 0.5f;

    private float speedtimer;
	private float slowtimer;
    private float boosttimer;

    private bool[] shouldUseAIpowerups = null;

    private bool speedUp = false;
	public bool slowDown = false;
	public bool hasSlowDown = false;
	public bool hasDriller = false;
	public bool hasHoming = false;
	public bool hasGasPickup = false;
    private bool boost = false;
    private GameObject[] respawnPoints;
	public string[] powerupSlots;
	public bool slotsAvailable = true;
    public Transform slowdownblock;
	public GameObject Driller;
	public GameObject Homing;
	public GameObject GasCloud;

	public Sprite speed_sprite;
	public Sprite slow_sprite;
	public Sprite gas_sprite;
	public Sprite driller_sprite;
	public Sprite homing_sprite;
	public Sprite default_sprite;

	public Image image1;
	public Image image2;

	private GameManager MyGameManager = null;


	void Awake() {
		MyGameManager = GameManager.Instance;
	}

	// Use this for initialization
	void Start () {
		powerupSlots = new string[2];
		powerupSlots [0] = "";
		powerupSlots [1] = "";
		if (gameObject.tag == "Player") {
			moveScript = GetComponent<ThirdPersonCharacter> ();
		} else if (gameObject.tag == "NPC") {
			AIscript = GetComponent<AIController> ();
            positionManager = GetComponent<PositionManager>();
		}

        // initiate the boolean array of whether an AI should use powerups
        // bools match this order of powerups: slow, driller, homing
        // should remain false if this script is attached to the player
        shouldUseAIpowerups = new bool[] { false, false, false };

        // get all locations of respawn locations for the ramp
        respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
	}

    // Update is called once per frame
    void Update() {
        // update whether AI wants to use powerups if this gameobject is an NPC
        if (AIscript != null)
        {
            shouldUseAIpowerups = AIscript.getShouldUsePowerups();
        }

		if (Input.GetButtonDown ("Fire1") || shouldUseAIpowerups.Contains(true)) {
			//Debug.Log ("Fire1!!");
			if (hasSlowDown && powerupSlots[0] == "SlowPickup" && (AIscript == null || shouldUseAIpowerups[0])) {
				Vector3 pos = gameObject.transform.position;
				Vector3 dir = gameObject.transform.forward;
				Quaternion rot = gameObject.transform.rotation;
				Vector3 spawnPoint = pos + dir * -2;
				Instantiate (slowdownblock, spawnPoint, rot);
				if (powerupSlots[1] != "SlowPickup")
					hasSlowDown = false;
				powerupSlots [0] = "";
				slotsAvailable = true;
                ////Sound effect of dropping block
                EventManager.TriggerEvent<BlockDropEvent, Vector3>(transform.position);
            }
			if (hasGasPickup && powerupSlots[0] == "GasPickup" && (AIscript == null || shouldUseAIpowerups[0])) {
				Vector3 pos = gameObject.transform.position;
				Vector3 dir = gameObject.transform.forward;
				Quaternion rot = gameObject.transform.rotation;
				Vector3 spawnPoint = pos + dir * -2;
				Instantiate (GasCloud, spawnPoint, rot);
				if (powerupSlots[1] != "GasPickup")
					hasGasPickup = false;
				powerupSlots [0] = "";
				slotsAvailable = true;
                ////Sound effect of gas cloud
                EventManager.TriggerEvent<GasEvent, Vector3>(transform.position);
            }

			if (hasDriller && powerupSlots[0] == "DrillerPickup" && (AIscript == null || shouldUseAIpowerups[1])) {
				Vector3 pos = gameObject.transform.position;
				Vector3 dir = gameObject.transform.forward;
				Quaternion rot = gameObject.transform.rotation * Quaternion.Inverse(Quaternion.Euler(new Vector3(0f,90f,0f)));
				Vector3 spawnPoint = pos + dir * 1.75f + gameObject.transform.up * 1.75f;
				Instantiate (Driller, spawnPoint, rot);
				if (powerupSlots[1] != "DrillerPickup")
					hasDriller = false;
				powerupSlots [0] = "";
				slotsAvailable = true;
                ////Sound effect of dropping block
                EventManager.TriggerEvent<DrillerEvent, Vector3>(transform.position);
            }
			if (hasHoming && powerupSlots[0] == "HomingPickup" && (AIscript == null || shouldUseAIpowerups[2])) {
				Vector3 pos = gameObject.transform.position;
				Vector3 dir = gameObject.transform.forward;
				Quaternion rot = gameObject.transform.rotation * Quaternion.Inverse(Quaternion.Euler(new Vector3(0f,90f,0f)));
				Vector3 spawnPoint = pos + dir * 2f + gameObject.transform.up * 5f;
				GameObject tempHoming = Instantiate (Homing, spawnPoint, rot);
				MyGameManager.HomingOn (tempHoming, true);
				if (powerupSlots[1] != "HomingPickup")
					hasHoming = false;
				powerupSlots [0] = "";
				slotsAvailable = true;
                ////Sound effect of dropping block
                EventManager.TriggerEvent<HomingEvent, Vector3>(transform.position);
            }
			if(moveScript != null)
				image1.sprite = default_sprite;
		}

		if (Input.GetButtonDown ("Fire2") || shouldUseAIpowerups.Contains(true)) {
			if (hasSlowDown && powerupSlots[1] == "SlowPickup" && (AIscript == null || shouldUseAIpowerups[0])) {
				Vector3 pos = gameObject.transform.position;
				Vector3 dir = gameObject.transform.forward;
				Quaternion rot = gameObject.transform.rotation;
				Vector3 spawnPoint = pos + dir * -2;
				Instantiate (slowdownblock, spawnPoint, rot);
				if (powerupSlots[0] != "SlowPickup")
					hasSlowDown = false;
				powerupSlots [1] = "";
				slotsAvailable = true;
                ////Sound effect of dropping block
                EventManager.TriggerEvent<BlockDropEvent, Vector3>(transform.position);
            }
			if (hasGasPickup && powerupSlots[1] == "GasPickup" && (AIscript == null || shouldUseAIpowerups[0])) {
				Vector3 pos = gameObject.transform.position;
				Vector3 dir = gameObject.transform.forward;
				Quaternion rot = gameObject.transform.rotation;
				Vector3 spawnPoint = pos + dir * -2;
				Instantiate (GasCloud, spawnPoint, rot);
				if (powerupSlots[0] != "GasPickup")
					hasSlowDown = false;
				powerupSlots [1] = "";
				slotsAvailable = true;
                ////Sound effect of gas cloud
                EventManager.TriggerEvent<GasEvent, Vector3>(transform.position);
            }

			if (hasDriller && powerupSlots[1] == "DrillerPickup" && (AIscript == null || shouldUseAIpowerups[1])) {
				Vector3 pos = gameObject.transform.position;
				Vector3 dir = gameObject.transform.forward;
				Quaternion rot = gameObject.transform.rotation * Quaternion.Inverse(Quaternion.Euler(new Vector3(0f,90f,0f)));
				Vector3 spawnPoint = pos + dir * 1.75f + gameObject.transform.up * 1.75f;
				Instantiate (Driller, spawnPoint, rot);
				if (powerupSlots[0] != "DrillerPickup")
					hasDriller = false;
				powerupSlots [1] = "";
				slotsAvailable = true;
                ////Sound effect of dropping block
                EventManager.TriggerEvent<DrillerEvent, Vector3>(transform.position);
            }
			if (hasHoming && powerupSlots[1] == "HomingPickup" && (AIscript == null || shouldUseAIpowerups[2])) {
				Vector3 pos = gameObject.transform.position;
				Vector3 dir = gameObject.transform.forward;
				Quaternion rot = gameObject.transform.rotation * Quaternion.Inverse(Quaternion.Euler(new Vector3(0f,90f,0f)));
				Vector3 spawnPoint = pos + dir * 2f + gameObject.transform.up * 5f;
				GameObject tempHoming = Instantiate (Homing, spawnPoint, rot);
				MyGameManager.HomingOn (tempHoming, true);
				if (powerupSlots[0] != "HomingPickup")
					hasHoming = false;
				powerupSlots [1] = "";
				slotsAvailable = true;
                ////Sound effect of dropping block
                EventManager.TriggerEvent<HomingEvent, Vector3>(transform.position);
            }
			if(moveScript != null)
				image2.sprite = default_sprite;
		}
			

		if (speedUp == true) {
			GetComponent<TrailRenderer> ().enabled = true;
			if (gameObject.tag == "Player") {
				moveScript.speedPowerUp = 3.0f;
			} else if (gameObject.tag == "NPC") {
				AIscript.speedPowerUp = 3.0f;
			}
			speedtimer += Time.deltaTime;
			if (speedtimer >= maxTime) {

				if (speedUp == true) {

					if (gameObject.tag == "Player") {
						moveScript.speedPowerUp = 1.0f;
						if(image1.sprite == speed_sprite)
							image1.sprite = default_sprite;
						else if(image2.sprite == speed_sprite)
							image2.sprite = default_sprite;
					} else if (gameObject.tag == "NPC") {
						AIscript.speedPowerUp = 1.0f;
					}
					speedtimer = 0;
					speedUp = false;
					GetComponent<TrailRenderer> ().enabled = false;

				}
			}
		}

        if (boost == true)
        {
            if (gameObject.tag == "Player")
            {
                moveScript.speedPowerUp = 1.5f;
            }
            else if (gameObject.tag == "NPC")
            {
                AIscript.speedPowerUp = 1.5f;
            }
            boosttimer += Time.deltaTime;
            if (boosttimer >= maxBoostTime)
            {
                if (boost == true)
                {

                    if (gameObject.tag == "Player")
                    {
                        moveScript.speedPowerUp = 1.0f;
                    }
                    else if (gameObject.tag == "NPC")
                    {
                        AIscript.speedPowerUp = 1.0f;
                    }
                    boosttimer = 0;
                    boost = false;
                }
            }
        }

        if (slowDown == true) {
			if (gameObject.tag == "Player") {
				moveScript.speedPowerUp = 0.3f;
			} else if (gameObject.tag == "NPC") {
				AIscript.speedPowerUp = 0.3f;
			}
			slowtimer += Time.deltaTime;
			if (slowtimer >= maxTime) {

				if (slowDown == true) {

					if (gameObject.tag == "Player") {
						moveScript.speedPowerUp = 1.0f;
					} else if (gameObject.tag == "NPC") {
						AIscript.speedPowerUp = 1.0f;
					}
					slowtimer = 0;
					slowDown = false;
				}
			}
		}
	}

    // gives AI a boost after warping behind player when stuck
    public void AICatchUpCheat()
    {
        boost = true;
    }

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Slowdown") {
			//If collided with log
	
			if (gameObject.tag == "Player") {
				moveScript.Stunned ();
			}
			if (gameObject.tag == "NPC") {
				AIscript.Stunned ();
			}

			Destroy (collision.gameObject);
		}
	}
		

    private void OnTriggerEnter(Collider other)
    {

		if (other.gameObject.tag == "Powerup") {
			EventManager.TriggerEvent<PowerUpEvent, Vector3>(transform.position);
			Debug.Log("Speedup");
			if(moveScript != null) {
				if(image1.sprite == default_sprite)
					image1.sprite = speed_sprite;
				else if(image2.sprite == default_sprite)
					image2.sprite = speed_sprite;
			}
			speedUp = true;
			Destroy (other.transform.parent.gameObject);
		}
			

		if (other.gameObject.tag == "SlowPickup") {
			if (areSlotsAvailable ()) {
				EventManager.TriggerEvent<PowerUpEvent, Vector3> (transform.position);
				hasSlowDown = true;
				if (powerupSlots [0] == null || powerupSlots [0] == "") {
					powerupSlots [0] = "SlowPickup";
					if(moveScript != null)
						image1.sprite = slow_sprite;
					Destroy (other.transform.parent.gameObject);
				} else if (powerupSlots [1] == null || powerupSlots [1] == "") {
					powerupSlots [1] = "SlowPickup";
					if(moveScript != null)
						image2.sprite = slow_sprite;
					Destroy (other.transform.parent.gameObject);
				}
				else
					slotsAvailable = false;
			} else
				Debug.Log ("Slots not available");
		}

		if (other.gameObject.tag == "GasCloudTag") {
			slowDown = true;
		}

		if (other.gameObject.tag == "GasPickup") {
			if (areSlotsAvailable ()) {
				EventManager.TriggerEvent<PowerUpEvent, Vector3> (transform.position);
				hasGasPickup = true;
				if (powerupSlots [0] == null || powerupSlots [0] == "") {
					powerupSlots [0] = "GasPickup";
					if(moveScript != null)
						image1.sprite = gas_sprite;
					Destroy (other.transform.parent.gameObject);
				} else if (powerupSlots [1] == null || powerupSlots [1] == "") {
					powerupSlots [1] = "GasPickup";
					if(moveScript != null)
						image2.sprite = gas_sprite;
					Destroy (other.transform.parent.gameObject);
				}
				else
					slotsAvailable = false;
			} else
				Debug.Log ("Slots not available");
		}



		if (other.gameObject.tag == "DrillerPickup") {
			if (areSlotsAvailable ()) {
				EventManager.TriggerEvent<PowerUpEvent, Vector3> (transform.position);
				hasDriller = true;
				if (powerupSlots [0] == null || powerupSlots [0] == "") {
					powerupSlots [0] = "DrillerPickup";
					if(moveScript != null)
						image1.sprite = driller_sprite;
					Destroy (other.transform.parent.gameObject);
				} else if (powerupSlots [1] == null || powerupSlots [1] == "") {
					powerupSlots [1] = "DrillerPickup";
					if(moveScript != null)
						image2.sprite = driller_sprite;
					Destroy (other.transform.parent.gameObject);
				}
				else
					slotsAvailable = false;
			} else
				Debug.Log ("Slots not available");
		}

		if (other.gameObject.tag == "HomingPickup") {
			if (areSlotsAvailable ()) {
				EventManager.TriggerEvent<PowerUpEvent, Vector3> (transform.position);
				hasHoming = true;
				if (powerupSlots [0] == null || powerupSlots [0] == "") {
					powerupSlots [0] = "HomingPickup";
					if(moveScript != null)
						image1.sprite = homing_sprite;
					Destroy (other.transform.parent.gameObject);
				} else if (powerupSlots [1] == null || powerupSlots [1] == "") {
					powerupSlots [1] = "HomingPickup";
					if(moveScript != null)
						image2.sprite = homing_sprite;
					Destroy (other.transform.parent.gameObject);
				}
				else
					slotsAvailable = false;
			} else
				Debug.Log ("Slots not available");
		}

        if (other.transform.gameObject.tag == "Boost")
        {
            EventManager.TriggerEvent<PowerUpEvent, Vector3>(transform.position);
            boost = true;
            Debug.Log("Hail Mary!");
        }

        if (other.transform.gameObject.tag == "DeathTrigger")
        {
            Debug.Log("FIRE FIRE FIRE!");
            //gameObject.transform.position = GameObject.Find("RespawnPoint").transform.position;
            // respawn at closest respawn point
            System.Array.Sort(respawnPoints, (a, b) => ((transform.position - a.transform.position).magnitude).CompareTo((transform.position - b.transform.position).magnitude));
            gameObject.transform.position = respawnPoints[0].transform.position;
            if (AIscript != null)
            {
                positionManager.diedOnRamp();
            }
        }
    }

	public bool areSlotsAvailable(){
		return slotsAvailable;
	}
}
