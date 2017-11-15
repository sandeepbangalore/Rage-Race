using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDolly : MonoBehaviour {

	public float 	Speed			=	4.0f;	// Move towards target at 1 unit per second by default
	public float 	StopDistance	=	3.0f;	// Stop moving towards target when less than units away
	public bool	 	LookAtTarget	=	false;  // Should it keep looking at the target
	public Vector3	TargetOffset	=	Vector3.zero;
	public Transform Target			=	null;
	public enum State {Dolly, Rotate, CountDown, Done};
	public State current = State.Dolly; 
	public float rotateAngle = 10f;
	public Animator cameraAnim = null;
	public Transform camPos = null;
	public GameObject mainCamera = null;
	private float totalAngle = 0f;
	private Vector3 startPos; 
	private float journeyDistance =0f;
	private float startTime = 0f;
	// Internal variables
	Vector3 	_resetPosition	=  Vector3.zero;
	Quaternion 	_resetRotation	=  Quaternion.identity;

	// Use this for initialization
	void Awake () 
	{
		// Store original transform so we can reset the camera if we need to
		_resetPosition	=	new Vector3		( transform.position.x, transform.position.y, transform.position.z );
		_resetRotation	=	new Quaternion	( transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w );
		totalAngle = 0f;
	}

	// -------------------------------------------------------
	// Name	:	OnEnable
	// Desc	:	Called when the camera is activated. As 
	//			starting grid cameras are only used once,
	//			when the camera is re-enabled we reset its
	//			position and rotation to its initial position
	//			so a starting sequence plays again.
	// -------------------------------------------------------
	void OnEnable()
	{
		ResetCamera();	
	}

	// -------------------------------------------------------
	// Name	:	ResetCamera
	// Desc	:	Sets the transform to its initial values
	// -------------------------------------------------------
	void ResetCamera()
	{
		transform.position = _resetPosition;
		transform.rotation = _resetRotation;
	}


	void LateUpdate () 
	{
		// If no target is assigned do nothing
		if (Target==null) return;
		if (current == State.Dolly) {
			// Transform offset vector and height into target space
			Vector3 offsetVector	= Target.TransformDirection (TargetOffset);
			Vector3 moveDirection = Target.position + offsetVector - transform.position;
			float length = moveDirection.magnitude;
			float distToMove	= Speed * Time.deltaTime;

			if (distToMove > length)
				distToMove = length;

			transform.position += moveDirection.normalized * distToMove;

			// If we have reached our desired position and the camera
			// controls its own lifetime/focus, instruct the camera manager
			// to switch to the next camera in the sequence
			if (length<StopDistance) current = State.Rotate;


			if (LookAtTarget) {
				// Otherwise look at the target
				Quaternion	tRot	=	new Quaternion ();
				tRot.SetLookRotation (Target.position - transform.position);
				transform.rotation	= tRot;
			}
		}
		if (current == State.Rotate) {
			if (totalAngle < 180f) {
				transform.RotateAround (Target.position, Vector3.up, rotateAngle * Time.deltaTime);
				totalAngle += rotateAngle * Time.deltaTime;
			}
			if (totalAngle >= 180f) {
				totalAngle = 0f;
				current = State.CountDown;
				startPos = transform.position;
				journeyDistance = Vector3.Distance (startPos , camPos.position);
				startTime = Time.time;
				Speed = 1.0f;
			}
		}
		if (current == State.CountDown) {
			cameraAnim.SetTrigger ("countDown");
			float distanceCovered = (Time.time - startTime) * Speed;
			transform.position = Vector3.Lerp (startPos, camPos.position, distanceCovered/journeyDistance);
		}
		if (current == State.Done) {
			Debug.Log ("Done");
			gameObject.SetActive (false);
			mainCamera.SetActive (true);
			mainCamera.transform.position = transform.position;
			mainCamera.transform.rotation = transform.rotation;
		}
	}

	public void setState(){
		current = State.Done;
	}


}
