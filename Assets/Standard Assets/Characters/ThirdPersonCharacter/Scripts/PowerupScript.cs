using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour {

	public AudioSource sound;
//	public ThirdPersonCharacter playerScript;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.gameObject.tag == "Player") {
//			sound.Play ();

//			playerScript = collision.transform.gameObject.GetComponent (ThirdPersonCharacter);
//			playerScript.speedPowerUp = 2.0f;

			//speedup


			Destroy(gameObject);
		}
		
	}

}
