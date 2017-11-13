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
			}

		}


	}

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.gameObject.tag == "Powerup") {

			speedUp = true;

		}
	}
}
