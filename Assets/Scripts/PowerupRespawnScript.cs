using UnityEngine;
public class PowerupRespawnScript : MonoBehaviour
{
	public GameObject pickup;
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
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		// Check if there are any other objects with pickup layer in the given sphere
		Collider[] hitCollider = Physics.OverlapSphere (spawnPoints [spawnPointIndex].position, 2f, layerMask);
		// Instantiate health pellets only if no other health pellet exists in the location
		if (hitCollider.Length == 0) {
			GameObject temp = Instantiate (pickup, spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation) as GameObject;
		}
		else{
			Debug.Log (hitCollider [0]);
		}        
	}
}