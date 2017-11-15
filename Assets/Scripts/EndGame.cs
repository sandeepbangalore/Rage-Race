﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	public Canvas Finish = null;
	private GameManager MyGameManager = null;

	// Use this for initialization
	void Awake() {
		Finish.enabled = false;
	}
	void Start () {
		MyGameManager = GameManager.Instance;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log("Entered");
		if (other.tag == "Player") {
		// Get lap number from game manager
			Debug.Log("Entered");
			Finish.enabled = true; 
			Finish.GetComponent<Animator> ().SetTrigger ("Finish");
			StartCoroutine (gameFinish ());
		}
	}


	IEnumerator gameFinish()
	{
		yield return new WaitForSeconds(4);
		MyGameManager.setGameState (GameManager.States.Finish);
	}
}
