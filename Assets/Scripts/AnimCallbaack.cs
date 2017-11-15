using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCallbaack : MonoBehaviour {

	public GameObject CameraObj = null;
	private GameManager MyGameManager = null;
	private CameraDolly cD;
	// Use this for initialization
	void Start () {
		MyGameManager = GameManager.Instance;
		cD = CameraObj.GetComponent<CameraDolly> ();
	}
	
	// Update is called once per frame
	public void setState(){
		cD.setState ();
		MyGameManager.setGameState (GameManager.States.Race);
	}
}
