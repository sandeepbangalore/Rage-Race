using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour
{
	public float missileVelocity = 100;
	public float turn = 20;
	public Rigidbody homingMissile; 
	public float fuseDelay = 0;
	public GameObject missileMod;
	public ParticleSystem SmokePrefab;
	public AudioClip missileClip;
	private static Transform target;
	private bool isColliding = false;

	void Start ()
	{
//		camera = Camera.main;
		Fire ();
	}
		
	void FixedUpdate (){
		//Debug.Log (target);
		if(target == null || homingMissile == null)
			return;
		homingMissile.velocity = transform.forward * missileVelocity;
		var targetRotation = Quaternion.LookRotation(target.position - transform.position);
		homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
		isColliding = false;

	}
		
	public void Fire (){
		//yield return new WaitForSeconds(fuseDelay);
		AudioSource.PlayClipAtPoint(missileClip, transform.position);
		/*float distance = Mathf.Infinity;
	    GameObject[] targets;
		targets = GameObject.FindGameObjectsWithTag ("target");

		foreach ( GameObject settarget in targets) {
			Vector3 viewPos = camera.WorldToViewportPoint(settarget.transform.position);
			bool onScreen = viewPos.z > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;
			Debug.Log (onScreen);
			Debug.Log ("here");
			float diff = (settarget.transform.position - transform.position).sqrMagnitude;
			if(diff < distance && (onScreen)){
				distance = diff;
				target = settarget.transform;
			}
		}*/
	//	return;
	}

	public void SetTarget( Transform currentTarget){
		//Debug.Log ("setting here");
		target = currentTarget;
		//Debug.Log (target);
	}
		
/*	void OnCollisionEnter(Collision collision){

		//if(theCollision.gameObject.name == "Cube")
		//{
		SmokePrefab.emissionRate = 0.0f;
		Destroy(missileMod.gameObject);
		//yield return new WaitForSeconds(1);
		Destroy(gameObject);
		//}
	//	return;
	}*/
	void OnTriggerEnter(Collider collision){
		if (isColliding)
			return;
		isColliding = true;

		if(collision.gameObject.tag == "Respawn")
		{
			SmokePrefab.emissionRate = 0.0f;
			//GameObject tempExplosion = (GameObject)Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);
			Destroy(missileMod.gameObject);
			//yield return new WaitForSeconds(1);
			Destroy(gameObject);
			//Destroy (collision.gameObject, 1);
		}
		else if(target != null && (collision.gameObject.tag == target.gameObject.tag))
		{
			SmokePrefab.emissionRate = 0.0f;
			missileMod.GetComponent<Collider> ().isTrigger = false;
			//GameObject tempExplosion = (GameObject)Instantiate (ExplosionPrefab, transform.position, targetRotation);
			Destroy(missileMod.gameObject,0.25f);
			//yield return new WaitForSeconds(1);
			Destroy(gameObject,0.25f);
			//Destroy (tempExplosion,2);
		}
		//	return;
	}
}

