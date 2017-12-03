using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class BallCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision theCollision)
	{
//		if(theCollision.gameObject.tag == "Player" || theCollision.gameObject.tag == "NPC")
//		{
//			if (theCollision.gameObject.tag == "Player") {
//				theCollision.gameObject.GetComponent<ThirdPersonCharacter> ().Stunned ();
//			}
//			if (theCollision.gameObject.tag == "NPC") {
//				theCollision.gameObject.GetComponent<AIController> ().Stunned ();
//			}
//		}
	}
}
