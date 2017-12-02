using UnityEngine;
using System.Collections;

public class DrillLauncher : MonoBehaviour {
	public GameObject Driller = null;
	//private Driller driller;
	private Transform target;
	private Vector3 direction;

	// Use this for initialization
	void Awake () {
		enabled = false;
	}
	
	// Update is called once per frame
	void OnEnable(){
		//driller = Driller.GetComponent<Driller> ();
		//homingmissile.SetTarget (target.transform);
		Instantiate (Driller, transform.position, transform.rotation);
		enabled = false;
	}


}
