/*
 * 									Team Rocket 
 * 
 * 	Agneya A Kerure					akerure3		kerure.agneya@gatech.edu
 *	Christina Chung					cchung44		cchung44@gatech.edu
 *	Erin Hsu						ehsu7			ehsu7@gatech.edu
 *	Dibyendu Mondal					dmondal6		dibyendu@gatech.edu
 *	Sandeep Banaglore Venkatesh 	sbv7			sandeepbanaglore@gatech.edu
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightManager : MonoBehaviour {

	public Light spotLight = null;
	public Rigidbody rocket = null;
	private float angle = 0f;
	private bool triggered = false;
	public float multiplier = 5f;
	void Awake() {
		if (spotLight != null) {
			angle = spotLight.spotAngle;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered == true) {
			angle = angle + Time.deltaTime * multiplier;
			if (angle >= 180) {
				angle = 180f;
				triggered = false;
				SceneManager.LoadScene ("menu");
			}
			spotLight.spotAngle = angle;
			if (angle > 80f) {
				rocket.isKinematic = true;
			}
		}		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Rocket") {
			triggered = true;
		}
	}
}
