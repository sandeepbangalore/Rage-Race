using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotateDriller : MonoBehaviour {
	public float speed = 10.0f;
	public enum whichWayToRotate {AroundX, AroundY, AroundZ}
//	public float missileVelocity = 100;
	//public Rigidbody drillerMissile; 
	public whichWayToRotate way = whichWayToRotate.AroundX;

	//private bool isColliding = false;

	// Use this for initialization
	void Start () {

	}
	void FixedUpdate(){
		//drillerMissile.velocity = transform.forward * missileVelocity;
		//drillerMissile.AddForce (transform.forward);
	}
	// Update is called once per frame
	void Update () {
		//isColliding = false;

		switch(way)
		{
		case whichWayToRotate.AroundX:
			transform.Rotate (Vector3.right * Time.deltaTime * speed);
			break;
		case whichWayToRotate.AroundY:
			transform.Rotate(Vector3.up * Time.deltaTime * speed);
			break;
		case whichWayToRotate.AroundZ:
			transform.Rotate(Vector3.forward * Time.deltaTime * speed);
			break;
		}	
	}
}
