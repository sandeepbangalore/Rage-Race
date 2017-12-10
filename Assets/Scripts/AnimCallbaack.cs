using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCallbaack : MonoBehaviour {

	public GameObject CameraObj = null;
	private GameManager MyGameManager = null;
	private CameraDolly cD;
    GameObject[] gos;
    GameObject[] enemies;
	// Use this for initialization
	void Start () {
		MyGameManager = GameManager.Instance;
		cD = CameraObj.GetComponent<CameraDolly> ();
        gos = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("NPC");
    }
	
	// Update is called once per frame
	public void setState(){
		cD.setState ();
		MyGameManager.setGameState (GameManager.States.Race);
        foreach(var go in gos)
        {
            go.GetComponent<Collider>().SendMessage("StartTimer");
        }
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Collider>().SendMessage("StartTimer");
        }

    }
}
