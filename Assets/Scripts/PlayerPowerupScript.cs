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
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class PlayerPowerupScript : MonoBehaviour {

    //[SerializeField] private Transform respawnPoint;

	private ThirdPersonCharacter moveScript;
	private AIController AIscript;
	public float maxTime = 2.0f;
    public float maxBoostTime = 0.5f;

    private float speedtimer;
	private float slowtimer;
    private float boosttimer;

    private bool speedUp = false;
	private bool slowDown = false;
    private bool boost = false;
    public Transform slowdownblock;

	// Use this for initialization
	void Start () {

		if (gameObject.tag == "Player") {
			moveScript = GetComponent<ThirdPersonCharacter> ();
		} else if (gameObject.tag == "NPC") {
			AIscript = GetComponent<AIController> ();
		}
	}
	
	// Update is called once per frame
	void Update () {

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

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.gameObject.tag == "Powerup") {
            EventManager.TriggerEvent<PowerUpEvent, Vector3>(transform.position);
            speedUp = true;
		}

		if (collision.transform.gameObject.tag == "Slowdown") {
			//EventManager.TriggerEvent<PowerUpEvent, Vector3>(transform.position);
			slowDown = true;
		}

		if (collision.transform.gameObject.tag == "SlowPickup") {
			EventManager.TriggerEvent<PowerUpEvent, Vector3>(transform.position);
			Vector3 pos = gameObject.transform.position;
			Vector3 dir = gameObject.transform.forward;
			Quaternion rot = gameObject.transform.rotation;

			Vector3 spawnPoint = pos + dir * -2;



			Instantiate (slowdownblock, spawnPoint, rot);

		}

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Boost")
        {
            EventManager.TriggerEvent<PowerUpEvent, Vector3>(transform.position);
            boost = true;
            Debug.Log("Hail Mary!");
        }

        if (other.transform.gameObject.tag == "DeathTrigger")
        {
            Debug.Log("FIRE FIRE FIRE!");
            gameObject.transform.position = GameObject.Find("RespawnPoint").transform.position;
        }
    }
}
