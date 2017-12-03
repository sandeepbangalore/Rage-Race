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

public class PlayerManager : MonoBehaviour {
//	public GameObject pauseMenuCanvas = null;
//	public GameObject characterCanvas = null;
//	public GameObject characterParent = null;
	//public GameObject[] charactersPrefab;
	public GameObject[] characters;
	public Text characterName = null;
	public Slider characterSpeed = null;
	public Slider characterDefense = null;
	public Slider characterAttack = null;
	public Animator sliderAnimation = null;
	public Canvas pauseMenu = null;
	public GameObject pausedDummy = null;
	public GameObject selectorDummy = null;
	private CharacterDetails current;
	private int playerNumber = 0;
	private int oldNumber = 0;
	private bool escPressed = false;
	private bool stateChanged = false;
    private GameManager MyGameManager = null;
	private SceneManagerLocal MySceneManager = null;

    private static PlayerManager _Instance = null;
	public static PlayerManager Instance{
		get{ 
			if (_Instance == null) {
				_Instance = (PlayerManager)FindObjectOfType (typeof(PlayerManager));
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
		//GameObject pMenu = Instantiate (pauseMenuCanvas) as GameObject;
		//GameObject cCanvas = Instantiate (characterCanvas) as GameObject;
		//pauseMenu = pMenu.GetComponent<Canvas> ();

		//characters = new GameObject[charactersPrefab.Length];
		//GameObject cParent = Instantiate (characterParent) as GameObject;
//		for (int i = 0; i < charactersPrefab.Length; i++) {
//			characters[i] = Instantiate (charactersPrefab[i], cParent.transform) as GameObject;
//			characters [i].SetActive (false);
//		}
        characters[playerNumber].SetActive (true);
		current = characters [playerNumber].GetComponent<CharacterDetails>();
		if (characterName != null) 
			characterName.text = current.name;
		if (characterSpeed != null)
			characterSpeed.value = current.speed;
		if (characterAttack != null)
			characterAttack.value = current.attack;
		if (characterDefense != null)
			characterDefense.value = current.defense;
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
			characters [oldNumber].SetActive (false);
			current = characters [playerNumber].GetComponent<CharacterDetails>();
			characters [playerNumber].SetActive (true);
			if (characterName != null) 
				characterName.text = current.name;
			if (characterSpeed != null)
				characterSpeed.value = current.speed;
			if (characterAttack != null)
				characterAttack.value = current.attack;
			if (characterDefense != null)
				characterDefense.value = current.defense;
		}			
	}

	public void playerNumberIncrease() {
		if (EventSystem.current.currentSelectedGameObject.name == "Next"){
			oldNumber = playerNumber;
			sliderAnimation.SetTrigger ("slide");
			playerNumber = (playerNumber + 1) % characters.Length;
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
			oldNumber = playerNumber;
			sliderAnimation.SetTrigger ("slide");
			if (playerNumber == 0)
				playerNumber = characters.Length - 1;
			else
				playerNumber = (playerNumber - 1);
			StartCoroutine (WaitInfoChange ());
			Debug.Log (playerNumber);
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

	public void Play()
	{
        MySceneManager.setCurrent(current.name);
		Application.LoadLevel("TrackSelector");
	}

	public void ExitGame()
	{
		Time.timeScale = 1;
		Application.Quit();
	}
}
