using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public enum States {Countdown, Race, Finish};
	private States gameState = States.Countdown; 
	private PositionManager[] results; 
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
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == States.Finish) {
			foreach (PositionManager runner in results) {
				int place = System.Array.IndexOf (results, runner) + 1;
				Debug.Log (place);
				Debug.Log (runner.gameObject);

			}
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
}
