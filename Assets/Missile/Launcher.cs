using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {

	public GameObject missile;
	void Update () {
		if(Input.GetButtonDown("Fire1"))
		{
			Instantiate(missile, transform.position,transform.rotation);
		}
	}
}
