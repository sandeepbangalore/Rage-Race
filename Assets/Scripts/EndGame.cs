using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {
	public PositionManager player = null;
	public Canvas Finish = null;
	private GameManager MyGameManager = null;
    private int placeCount = 0;

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
		if (other.tag == "Player" && MyGameManager.getGameState() == GameManager.States.Race)
        {
            StartCoroutine(gameFinish());
            // Get lap number from game manager
            Debug.Log("Entered");
			Finish.enabled = true; 
			Finish.GetComponent<Animator> ().SetTrigger ("Finish");
            //other.SendMessage("Finish");
            MyGameManager.stopTimer();
            other.GetComponent<PositionManager>().setFinalPlace(++placeCount);
            player.finalResults ();
		}
        else if (other.tag == "NPC")
        {
            other.GetComponent<AIController>().Invoke("stopRunning", 2f);
            other.GetComponent<PositionManager>().setFinalPlace(++placeCount);
        }

    }


	IEnumerator gameFinish()
	{
		yield return new WaitForSeconds(4);
		MyGameManager.setGameState (GameManager.States.Finish);
	}
}
