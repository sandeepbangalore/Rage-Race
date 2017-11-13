﻿/*
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
	public float maxTime = 5.0f;
	private float timer;

	private bool speedUp = false;

	// Use this for initialization
	void Start () {
		moveScript = GetComponent<ThirdPersonCharacter> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (speedUp == true) {

			moveScript.speedPowerUp = 3.0f;

			timer += Time.deltaTime;

			if (timer >= maxTime) {
				moveScript.speedPowerUp = 1.0f;
				timer = 0;
				speedUp = false;
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.gameObject.tag == "Powerup") {

			speedUp = true;

		}
	}
}
