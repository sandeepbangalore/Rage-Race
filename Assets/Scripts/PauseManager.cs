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

public class PauseManager : MonoBehaviour {

	public Canvas pauseMenu = null;
	public GameObject pausedDummy = null;
	private bool escPressed = false;


	//Awake is always called before any Start functions
	void Awake()
	{
		pauseMenu.enabled = false;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !escPressed) {
			escPressed = true;
			Time.timeScale = 0;
			pauseMenu.enabled = true;
			EventSystem.current.SetSelectedGameObject (pausedDummy);
		} else if (Input.GetKeyDown (KeyCode.Escape) && escPressed) {
			escPressed = false;
			Time.timeScale = 1;
			pauseMenu.enabled = false;
			//EventSystem.current.SetSelectedGameObject (selectorDummy);
		}

	}


	public void MenuScreen()
	{
		Time.timeScale = 1;
		Application.LoadLevel("menu");
	}

	

	public void ExitGame()
	{
		Time.timeScale = 1;
		Application.Quit();
	}
}
