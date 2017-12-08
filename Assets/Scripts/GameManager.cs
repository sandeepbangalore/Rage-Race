using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class GameManager : MonoBehaviour {
	
	public enum TrackMode {Day, Night};
	public TrackMode mode = TrackMode.Day; 
	public GameObject DayMode = null;
	public GameObject NightMode = null;
	public GameObject RightLamps = null;
	public GameObject LeftLamps = null;
	public Material DaySkybox = null;
	public Material NightSkybox = null;
	public MiniMapFollow mmf;
	public EndGame finishPM;
	public CameraDolly dollyCam;
	public ThirdPersonCamera playerCam;
	public Text playerPositionGUI;
	public Image image1;
	public Image image2;
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
	public PositionManager[] gameCurrentPositions;
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
		if (selectedTrack() == "Day Mode")
			mode = TrackMode.Day;
		if (selectedTrack() == "Night Mode")
			mode = TrackMode.Night;
		
		if (mode == TrackMode.Day) {
			DayMode.SetActive (true);
			NightMode.SetActive (false);
			RightLamps.SetActive (false);
			LeftLamps.SetActive (false);
			RenderSettings.skybox = DaySkybox;
		}

		if (mode == TrackMode.Night) {
			DayMode.SetActive (false);
			NightMode.SetActive (true);
			RightLamps.SetActive (true);
			LeftLamps.SetActive (true);
			RenderSettings.skybox = NightSkybox;
		}

		Transform[] dummyLeftWayPoints = leftWaypoints.GetComponentsInChildren<Transform> ();
		Transform[] newArrayLeft = new Transform[dummyLeftWayPoints.Length - 1];
		System.Array.Copy(dummyLeftWayPoints, 1, newArrayLeft, 0, newArrayLeft.Length);
		Transform[] dummyRightWayPoints = rightWaypoints.GetComponentsInChildren<Transform> ();
		Transform[] newArrayRight = new Transform[dummyRightWayPoints.Length - 1];
		System.Array.Copy(dummyRightWayPoints, 1, newArrayRight, 0, newArrayRight.Length);
		playerSelectedName = selectedPlayer();
        var randNPCs = Enumerable.Range(0, NPCs.Length).OrderBy(x => Random.value).ToArray(); // randomly sorts NPC models
        int j = 0; // index for random NPC model array
        for (int i = 0; i < NPCLocations.Length; i++)
        {
            Transform location = NPCLocations[i];
            GameObject NPC_current = NPCs[randNPCs[j++]];
            while (NPC_current.GetComponent<CharacterDetails>().name == playerSelectedName)
            {
                NPC_current = NPCs[randNPCs[j++]];
            }
            GameObject dummy = Instantiate(NPC_current, location.position, location.rotation, parent) as GameObject; // BV
            PositionManager dummyPM = dummy.GetComponent<PositionManager>();
            dummyPM.leftWaypoints = newArrayLeft;
            dummyPM.rightWaypoints = newArrayRight;
        }
        //foreach (Transform location in NPCLocations)
        //{
        //    GameObject NPC_current = NPCs[Random.Range(0, NPCs.Length)];
        //    while (NPC_current.GetComponent<CharacterDetails>().name == playerSelectedName)
        //    {
        //        NPC_current = NPCs[Random.Range(0, NPCs.Length)];
        //    }
        //    GameObject dummy = Instantiate(NPC_current, location.position, location.rotation, parent) as GameObject; // BV
        //    PositionManager dummyPM = dummy.GetComponent<PositionManager>();
        //    dummyPM.leftWaypoints = newArrayLeft;
        //    dummyPM.rightWaypoints = newArrayRight;
        //}
        // playerSelectedName = selectedPlayer();
        mainPlayer = playerG [0];
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
		mmf.player = playerGO.transform;
		PlayerPowerupScript playerPPS = playerGO.GetComponent<PlayerPowerupScript> ();
		playerPPS.image1 = image1;
		playerPPS.image2 = image2;
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

	public void currentPositions(PositionManager[] runners){
		gameCurrentPositions = runners;
	}

	void printResults(){
		if (!printed) {
			printed = true;
			finishCanvas.enabled = false;
			EventSystem.current.SetSelectedGameObject (pausedDummy);
			resultsCanvas.enabled = true;
			foreach (PositionManager runner in results) {
				int place = System.Array.IndexOf (results, runner) + 1;
				string Name = runner.gameObject.name.Substring(0,runner.gameObject.name.Length-7);
				switch (place) {
				case 1: 
					if (runner.gameObject.tag != "Player")
						resultsText += "1ST - " + Name+"\n";
					else
						resultsText += "<color=red>1ST - Player</color>\n";
					break;
				case 2:
					if (runner.gameObject.tag != "Player")
						resultsText += "2ND - " + Name+"\n";
					else
						resultsText += "<color=red>2ND - Player</color>\n";
					break;
				case 3:
					if (runner.gameObject.tag != "Player")
						resultsText += "3RD - " + Name+"\n";
					else
                        //resultsText += "<color=red>3RD - " + Name+"</color>\n";  // If we want to have custom names for later 
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
						resultsText += place+"TH - " + Name+"\n";
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
        GameObject PM = GameObject.Find("SceneManagerLocal");
		if (PM != null)
			return PM.GetComponent<SceneManagerLocal> ().getCurrent ();
		else
			return "";
    }

	public string selectedTrack() {      //BV
		GameObject PM = GameObject.Find("SceneManagerLocal");
		if (PM != null)
			return PM.GetComponent<SceneManagerLocal> ().getTrack ();
		else
			return "";
	}
}
