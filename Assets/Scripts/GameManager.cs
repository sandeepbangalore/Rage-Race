using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
	public enum States {Countdown, Race, Finish};
	public Text resultsTextUI = null;
	public Canvas resultsCanvas = null;
	public Canvas finishCanvas = null;
	public GameObject pausedDummy = null;
	private States gameState = States.Countdown; 
	private PositionManager[] results; 
	private string resultsText = "";
	private bool printed = false;
	private static GameManager _Instance = null;
	public static GameManager Instance{
		get{ 
			if (_Instance == null) {
				_Instance = (GameManager)FindObjectOfType (typeof(GameManager));
			}
			return _Instance;	
		}
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
		results = runners; 
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
					resultsText += "1ST - " + runner.gameObject.name+"\n";
					break;
				case 2:
					resultsText += "2ND - " + runner.gameObject.name+"\n";
					break;
				case 3:
					resultsText += "3RD - " + runner.gameObject.name+"\n";
					break;
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
					resultsText += place+"TH - " + runner.gameObject.name+"\n";
					break;
				}
//				Debug.Log (place);
//				Debug.Log (runner.gameObject);

			}
			resultsTextUI.text = resultsText;
		}
	}
}
