using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class homingC : MonoBehaviour {
	public float missileVelocity = 100;
	public float turn = 90;
	public Rigidbody homingMissile;
	public float fuseDelay;
	public GameObject missileMod ;
	public ParticleSystem SmokePrefab;
	public AudioClip missileClip;

	private Transform target;
	private bool stageOne = true;
	private GameManager MyGameManager;
	void Start () {
		MyGameManager = GameManager.Instance;
		Fire();
	}

	void Update(){
		target = MyGameManager.gameCurrentPositions [0].gameObject.transform;
	}

	void FixedUpdate ()

	{
		if(target == null || homingMissile == null)
			return;

		homingMissile.velocity = transform.forward * missileVelocity;

		Quaternion targetRotation = Quaternion.identity;
		if ((transform.position - (target.position + target.transform.up * 4 + target.transform.forward * 20)).sqrMagnitude < 5) {
			stageOne = false;
			//Debug.Log ((transform.position - (target.position+target.transform.up*4 + target.transform.forward * 20)).sqrMagnitude);
		}
		if(stageOne)
			targetRotation = Quaternion.LookRotation(target.position+target.transform.up*4 + target.transform.forward * 20 - transform.position);
		else
			targetRotation = Quaternion.LookRotation (target.position - transform.position);
		
		homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));


	}

	void Fire ()
	{
		//yield WaitForSeconds =fuseDelay;
		AudioSource.PlayClipAtPoint(missileClip, transform.position);
	}

	void OnCollisionEnter (Collision theCollision)
	{
		if(theCollision.gameObject.tag == "Player" || theCollision.gameObject.tag == "NPC")
		{
			SmokePrefab.emissionRate = 0.0f;
			Destroy(missileMod.gameObject);
			Destroy(gameObject);
			if (theCollision.gameObject.tag == "Player") {
				theCollision.gameObject.GetComponent<ThirdPersonCharacter> ().Stunned ();
			}
			if (theCollision.gameObject.tag == "NPC") {
				theCollision.gameObject.GetComponent<AIController> ().Stunned ();
			}
		}
	}
}