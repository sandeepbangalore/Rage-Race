/*
 *                                  Team Rocket 
 * 
 *  Agneya A Kerure                 akerure3        kerure.agneya@gatech.edu
 *  Christina Chung                 cchung44        cchung44@gatech.edu
 *  Erin Hsu                        ehsu7           ehsu7@gatech.edu
 *  Dibyendu Mondal                 dmondal6        dibyendu@gatech.edu
 *  Sandeep Banaglore Venkatesh     sbv7            sandeepbanaglore@gatech.edu
 * 
*/

using UnityEngine;
public class PowerupRespawnUpdated : MonoBehaviour
{
	public GameObject[] pickup;
	public float spawnTime = 5f;
	public Transform[] spawnPoints;
	int layerMask;
	void Start ()
	{
		// Get the layerMask for pick-up objects
		layerMask = LayerMask.GetMask ("Pickup");
		Debug.Log (layerMask);
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	void Spawn ()
	{
		//int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		for (int spawnPointIndex = 0; spawnPointIndex < spawnPoints.Length; spawnPointIndex++){
			// Check if there are any other objects with pickup layer in the given sphere
			Collider[] hitCollider = Physics.OverlapSphere (spawnPoints [spawnPointIndex].position, 2f);
			// Instantiate only if no other exists in the location
			if (hitCollider.Length == 0) {
				GameObject temp = Instantiate (pickup[Random.Range(0, pickup.Length)], spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation) as GameObject;
			}
			else{
				Debug.Log (hitCollider [0]);
			}
		}
	}
}