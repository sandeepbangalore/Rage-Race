/*
 * 									Team Rocket 
 * 
 * 	Agneya A Kerure					akerure3		kerure.agneya@gatech.edu
 *	Christina Chung					cchung44		cchung44@gatech.edu
 *	Erin Hsu						ehsu7			ehsu7@gatech.edu
 *	Dibyendu Mondal					dmondal6		dibyendu@gatech.edu
 *	Sandeep Banaglore Venkatesh 	sbv7			sandeepbanaglore@gatech.edu
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrackManager : MonoBehaviour {
	public Animator sliderAnimation = null;

	public GameObject[] tracks = null;
	public Canvas pauseMenu = null;
	public GameObject pausedDummy = null;
	public GameObject selectorDummy = null;
	public Text trackName = null;
	private TrackDetails current;
	private int trackNumber = 0;
	private int oldNumber = 0;
	private bool escPressed = false;
	private bool stateChanged = false;
	private GameManager MyGameManager = null;
	private SceneManagerLocal MySceneManager = null;

	private static TrackManager _Instance = null;
	public static TrackManager Instance{
		get{ 
			if (_Instance == null) {
				_Instance = (TrackManager)FindObjectOfType (typeof(TrackManager));
			}
			return _Instance;	
		}
	}

	public string getCurrent() {
		return current.name;
	}
	//Awake is always called before any Start functions
	void Awake()
	{
		MyGameManager = GameManager.Instance;
		MySceneManager = SceneManagerLocal.Instance;

		tracks[trackNumber].SetActive (true);
		current = tracks [trackNumber].GetComponent<TrackDetails>();
		if (trackName != null) 
			trackName.text = current.name;
		pauseMenu.enabled = false;

	}
	// Use this for initialization
	void Start () {
		//        DontDestroyOnLoad(gameObject);
		//		if (FindObjectsOfType<PlayerManager>().Length > 1)
		//			Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (!EventSystem.current.alreadySelecting && EventSystem.current.currentSelectedGameObject != null && 
			(EventSystem.current.currentSelectedGameObject.name == "Next" || EventSystem.current.currentSelectedGameObject.name == "Previous" ))
			EventSystem.current.SetSelectedGameObject (selectorDummy);
		if (EventSystem.current.currentSelectedGameObject == null)
			EventSystem.current.SetSelectedGameObject (selectorDummy);
		if (Input.GetKeyDown (KeyCode.Escape) && !escPressed) {
			escPressed = true;
			Time.timeScale = 0;
			pauseMenu.enabled = true;
			EventSystem.current.SetSelectedGameObject (pausedDummy);
		} else if (Input.GetKeyDown (KeyCode.Escape) && escPressed) {
			escPressed = false;
			Time.timeScale = 1;
			pauseMenu.enabled = false;
			EventSystem.current.SetSelectedGameObject (selectorDummy);
		}
		if (stateChanged == true) {
			stateChanged = false;
			tracks [oldNumber].SetActive (false);
			current = tracks [trackNumber].GetComponent<TrackDetails>();
			tracks [trackNumber].SetActive (true);
			if (trackName != null) 
				trackName.text = current.name;
		}			
	}

	public void playerNumberIncrease() {
		if (EventSystem.current.currentSelectedGameObject.name == "Next"){
			oldNumber = trackNumber;
			sliderAnimation.SetTrigger ("slide");
			trackNumber = (trackNumber + 1) % tracks.Length;
			StartCoroutine (WaitInfoChange ());
			//Debug.Log (playerNumber);
		}
	}

	public IEnumerator WaitInfoChange() {
		yield return new WaitForSeconds(0.5f); // waits 3 seconds
		stateChanged = true; // will make the update method pick up 
	}

	public void playerNumberDecrease() {
		if (EventSystem.current.currentSelectedGameObject.name == "Previous") {			
			oldNumber = trackNumber;
			sliderAnimation.SetTrigger ("slide");
			if (trackNumber == 0)
				trackNumber = tracks.Length - 1;
			else
				trackNumber = (trackNumber - 1);
			StartCoroutine (WaitInfoChange ());
			Debug.Log (trackNumber);
		}
	}

	public void clickReset(){
		WaitClick ();
		Debug.Log ("Done1");
		EventSystem.current.SetSelectedGameObject (selectorDummy);
		Debug.Log ("Done");
	}

	public IEnumerator WaitClick() {
		yield return new WaitForSeconds(0.5f); // waits 3 seconds
	}

	public void MenuScreen()
	{
		Time.timeScale = 1;
		Application.LoadLevel("menu");
	}

	public void PlayerSelectionScreen()
	{
		Time.timeScale = 1;
		Application.LoadLevel("PlayerSelector");
	}

	public void Play()
	{
		MySceneManager.setTrack(current.name);
		Application.LoadLevel("track");
	}

	public void ExitGame()
	{
		Time.timeScale = 1;
		Application.Quit();
	}
}
