using UnityEngine;
using System.Collections;

public class DrillFwd : MonoBehaviour {
	public float missileVelocity = 200;
	public Rigidbody drillerMissile; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate(){
		drillerMissile.velocity = transform.forward * missileVelocity;
		drillerMissile.AddForce (transform.forward);
	}
}
