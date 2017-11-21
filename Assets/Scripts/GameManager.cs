using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
	public EndGame finishPM;
	public CameraDolly dollyCam;
	public ThirdPersonCamera playerCam;
	public Text playerPositionGUI;
	public Transform[] NPCLocations;
	public GameObject[] NPCs;
	public GameObject leftWaypoints;
	public GameObject rightWaypoints;
	public GameObject[] playerG;
	public Transform playerParent;
	public Transform playerLocation;
	public Transform parent;
	public enum States {Countdown, Race, Finish};
	public Text resultsTextUI = null;
	public Canvas resultsCanvas = null;
	public Canvas finishCanvas = null;
	public GameObject pausedDummy = null;
	private States gameState = States.Countdown; 
	private PositionManager[] results; 
	private string resultsText = "";
	private bool printed = false;
    private GameObject mainPlayer = null; //BV
    private string playerSelectedName = null;  //BV
	private static GameManager _Instance = null;
	public static GameManager Instance{
		get{ 
			if (_Instance == null) {
				_Instance = (GameManager)FindObjectOfType (typeof(GameManager));
			}
			return _Instance;	
		}
	}

	void Awake(){
		Transform[] dummyLeftWayPoints = leftWaypoints.GetComponentsInChildren<Transform> ();
		Transform[] newArrayLeft = new Transform[dummyLeftWayPoints.Length - 1];
		System.Array.Copy(dummyLeftWayPoints, 1, newArrayLeft, 0, newArrayLeft.Length);
		Transform[] dummyRightWayPoints = rightWaypoints.GetComponentsInChildren<Transform> ();
		Transform[] newArrayRight = new Transform[dummyRightWayPoints.Length - 1];
		System.Array.Copy(dummyRightWayPoints, 1, newArrayRight, 0, newArrayRight.Length);
		foreach (Transform location in NPCLocations) {
			GameObject dummy = Instantiate (NPCs [Random.Range (0, NPCs.Length)], location.position,location.rotation, parent) as GameObject; // BV
			PositionManager dummyPM = dummy.GetComponent<PositionManager> ();
			dummyPM.leftWaypoints = newArrayLeft;
			dummyPM.rightWaypoints = newArrayRight;
		}
        playerSelectedName = selectedPlayer();
        foreach (GameObject playerSelected in playerG) {
            if (playerSelected.GetComponent<CharacterDetails>().name == playerSelectedName) {
                mainPlayer = playerSelected;
                break;
            }
        }
		GameObject playerGO = Instantiate (mainPlayer, playerLocation.position,playerLocation.rotation) as GameObject; //BV playerG[Random.Range(0,playerG.Length)]
        PositionManager playerPM = playerGO.GetComponent<PositionManager> ();
		playerPM.leftWaypoints = newArrayLeft;
		playerPM.rightWaypoints = newArrayRight;
		playerPM.positionText = playerPositionGUI;
		playerCam.standardPos = playerGO.transform.Find ("camPos");
		dollyCam.camPos = playerGO.transform.Find ("camPos");
		finishPM.player = playerPM;
	}

	// Use this for initialization
	void Start () {
		resultsCanvas.enabled = false;
		resultsTextUI.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == States.Finish) {
			printResults ();
		}
	}

	public void setGameState(States currentState){
		gameState = currentState;
	}

	public States getGameState(){
		return gameState;
	}

	public void finalResults(PositionManager[] runners){
        results = new PositionManager[runners.Length];       //BV
		System.Array.Copy(runners,results,runners.Length);  //BV
	}

	void printResults(){
		if (!printed) {
			printed = true;
			finishCanvas.enabled = false;
			EventSystem.current.SetSelectedGameObject (pausedDummy);
			resultsCanvas.enabled = true;
			foreach (PositionManager runner in results) {
				int place = System.Array.IndexOf (results, runner) + 1;
				switch (place) {
				case 1: 
					if (runner.gameObject.tag != "Player")
						resultsText += "1ST - " + runner.gameObject.name+"\n";
					else
						resultsText += "<color=red>1ST - Player</color>\n";
					break;
				case 2:
					if (runner.gameObject.tag != "Player")
						resultsText += "2ND - " + runner.gameObject.name+"\n";
					else
						resultsText += "<color=red>2ND - Player</color>\n";
					break;
				case 3:
					if (runner.gameObject.tag != "Player")
						resultsText += "3RD - " + runner.gameObject.name+"\n";
					else
                        //resultsText += "<color=red>3RD - " + runner.gameObject.name+"</color>\n";  // If we want to have custom names for later 
                        resultsText += "<color=red>3RD - Player</color>\n";
                        break;
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
					if (runner.gameObject.tag != "Player")
						resultsText += place+"TH - " + runner.gameObject.name+"\n";
					else
						resultsText += "<color=red>"+place +"TH - Player</color>\n";
					break;
				}
//				Debug.Log (place);
//				Debug.Log (runner.gameObject);

			}
			resultsTextUI.text = resultsText;
		}
	}

    public string selectedPlayer() {      //BV
        GameObject PM = GameObject.Find("PlayerManager");
        return PM.GetComponent<PlayerManager>().getCurrent();
    }
}
