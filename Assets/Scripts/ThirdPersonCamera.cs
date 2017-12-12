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

using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
	public float smooth = 3f;		// a public variable to adjust smoothing of camera motion
	public Transform standardPos;			// the usual position for the camera, specified by a transform in the game
	//Transform lookAtPos;			// the position to move the camera to when using head look

	void Start()
	{

        //Debug.Log("camera init");

		// initialising references
		//standardPos = GameObject.FindGameObjectWithTag ("Player").transform.Find("camPos").transform;

        if (standardPos == null)
        {
            Debug.Log("CamPos is null!");
        }
		
//		if(GameObject.Find ("LookAtPos"))
//			lookAtPos = GameObject.Find ("LookAtPos").transform;
	}
	
	void FixedUpdate ()
	{
//		// if we hold Alt
//		if(Input.GetButton("Fire2") && lookAtPos)
//		{
//			// lerp the camera position to the look at position, and lerp its forward direction to match 
//			transform.position = Vector3.Lerp(transform.position, lookAtPos.position, Time.deltaTime * smooth);
//			transform.forward = Vector3.Lerp(transform.forward, lookAtPos.forward, Time.deltaTime * smooth);
//		}
//		else
		{	
            //Debug.Log("camera update");
			// return the camera to standard position and direction
			transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);	
			transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
		}
		
	}
}
