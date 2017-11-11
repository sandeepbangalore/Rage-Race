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

public class CameraRotate : MonoBehaviour {
	
	public Vector3 from = new Vector3(0f, 0f, 135f);
	public Vector3 to   = new Vector3(0f, 0f, 225f);
	public float speed = 1.0f; 

	void Update() {
		float t = Mathf.PingPong(Time.time * speed * 2.0f, 1.0f);
		transform.eulerAngles = Vector3.Lerp (from, to, t);

	}
}
