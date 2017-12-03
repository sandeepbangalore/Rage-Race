using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;



public class DrillerMissile : MonoBehaviour {
	public GameObject missileMod;
	public ParticleSystem SmokePrefab;
	public AudioClip missileClip;
	private bool isColliding = false;
	// Use this for initialization
	void Start () {
		Fire ();
	}

	public void Fire (){
		AudioSource.PlayClipAtPoint (missileClip, transform.position,0.2f);
	}
	// Update is called once per frame
	void FixedUpdate () {
		isColliding = false;
	}

	void OnCollisionEnter(Collision collision){
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
			if (collision.gameObject.tag == "Player") {
				collision.gameObject.GetComponent<ThirdPersonCharacter> ().Stunned ();
			}
			if (collision.gameObject.tag == "NPC") {
				collision.gameObject.GetComponent<AIController> ().Stunned ();
			}
		}
		else{
			SmokePrefab.emissionRate = 0.0f;
			Rigidbody rb = GetComponent<Rigidbody>();
			if (rb != null)
				rb.AddExplosionForce(1000f, gameObject.transform.position, 3, 3.0f);
			Destroy(missileMod.gameObject,0.25f);
			Destroy(gameObject,0.25f);
			if (collision.gameObject.tag == "Player") {
				collision.gameObject.GetComponent<ThirdPersonCharacter> ().Stunned ();
			}
			if (collision.gameObject.tag == "NPC") {
				collision.gameObject.GetComponent<AIController> ().Stunned ();
			}
		}
		//	return;
	}
}
