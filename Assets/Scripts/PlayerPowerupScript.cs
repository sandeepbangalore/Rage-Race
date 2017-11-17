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


	private ThirdPersonCharacter moveScript;
	private AIController AIscript;
	public float maxTime = 2.0f;
	private float speedtimer;
	private float slowtimer;

	private bool speedUp = false;
	private bool slowDown = false;

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
	}
}
