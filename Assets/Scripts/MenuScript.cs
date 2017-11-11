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
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour {
	public Text RageRaceTitle;
	public Canvas quitMenu;
	public Canvas startMenu;
	public Canvas creditsMenu;
	public Button playText;
	public Button creditsText;
	public Button exitText;
	private bool activated = true;
	public Image screenCredits = null;
	public Animator rageRaceCredits = null;
	public Animator scrollCredits = null;
	public GameObject creditsDummy = null;
	public GameObject menuDummy = null;
	public GameObject quitDummy = null;
	private bool escPressed = false;
	private bool inCredits = false;
	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startMenu = startMenu.GetComponent<Canvas> ();
		creditsMenu = creditsMenu.GetComponent<Canvas> ();
		playText = playText.GetComponent<Button> ();
		creditsText = creditsText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;
		creditsMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !escPressed && !inCredits) {
			escPressed = true;
			EventSystem.current.SetSelectedGameObject (quitDummy);
			ExitPress ();
		} else if (Input.GetKeyDown (KeyCode.Escape) && !escPressed && inCredits) {
			escPressed = true;
			Time.timeScale = 0;
			EventSystem.current.SetSelectedGameObject (quitDummy);
			quitMenu.enabled = true;
		} else if (Input.GetKeyDown (KeyCode.Escape) && escPressed && !inCredits) {
			EventSystem.current.SetSelectedGameObject (menuDummy);
			escPressed = false;
			NoPress ();
		} else if (Input.GetKeyDown (KeyCode.Escape) && escPressed && inCredits) {
			escPressed = false;
			Time.timeScale = 1;
			EventSystem.current.SetSelectedGameObject (creditsDummy);
			quitMenu.enabled = false;
		}
	}

	public void ExitPress() 
	{
		Debug.Log("Exit Pressed!");
		quitMenu.enabled = true;
		creditsMenu.enabled = false;
		startMenu.enabled = false;
		playText.enabled = false;
		creditsText.enabled = false;
		exitText.enabled = false;
		RageRaceTitle.enabled = false;
		escPressed = true;

	}

	public void CreditPress()
	{
		Debug.Log("Credits Pressed!");
		EventSystem.current.SetSelectedGameObject (creditsDummy);
		screenCredits.gameObject.SetActive (true);
		StartCoroutine (ScreenFade ());
		inCredits = true;
	}

	public void OKPress()
	{
		Debug.Log("OK Pressed!");
		quitMenu.enabled = false;
		creditsMenu.enabled = false;
		startMenu.enabled = true;
		playText.enabled = true;
		creditsText.enabled = true;
		exitText.enabled = true;
		RageRaceTitle.enabled = true;
		EventSystem.current.SetSelectedGameObject (menuDummy);
		StartCoroutine (ScreenFadeOut ());
		inCredits = false;
	}

	public void NoPress()
	{
		escPressed = false;
		Debug.Log("No Pressed!");
		if (!inCredits) {
			creditsMenu.enabled = false;
			quitMenu.enabled = false;
			startMenu.enabled = true;
			playText.enabled = true;
			creditsText.enabled = true;
			exitText.enabled = true;
			RageRaceTitle.enabled = true;
			EventSystem.current.SetSelectedGameObject (menuDummy);
		} else {
			quitMenu.enabled = false;
			EventSystem.current.SetSelectedGameObject (creditsDummy);
			Time.timeScale = 1;
		}
	}

	public void StartLevel()
	{
		Application.LoadLevel("PlayerSelector");
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	private IEnumerator ScreenFade () {
		float screenTimer = 2.0f;
		float fixedTimer = 2.0f;
		while (screenTimer >= 0.0f) {
			activated = false;
			screenTimer -= Time.deltaTime;
			if (screenCredits != null) {
				Color temp = screenCredits.color;
				temp.a = 1.0f - (screenTimer / fixedTimer);
				screenCredits.color = temp;
			}
			yield return null;
		}
//		if (!activated) {
//			cameraArray [position].SetActive (false);
//			if (position == cameraArray.Length - 1) {
//				position = 0;
//			} else
//				position++;
//			cameraArray [position].SetActive (true);
//		}
//		while (screenTimer <= fixedTimer) {
//			activated = false;
//			screenTimer += Time.deltaTime;
//			if (Screen != null) {
//				Color temp = Screen.color;
//				temp.a = 1.0f - (screenTimer / fixedTimer);
//				Screen.color = temp;
//			}
//			yield return null;
//		}
		if (!activated) {
			creditsMenu.enabled = true;
			rageRaceCredits.SetBool ("CreditsStarted", true);
			scrollCredits.SetBool ("CreditsStarted", true);
			quitMenu.enabled = false;
			startMenu.enabled = false;
			playText.enabled = false;
			creditsText.enabled = false;
			exitText.enabled = false;
			activated = true;
		}
	}

	private IEnumerator ScreenFadeOut () {
		float screenTimer = 0.0f;
		float fixedTimer = 2.0f;
		while (screenTimer <= fixedTimer) {
			activated = false;
			screenTimer += Time.deltaTime;
			if (screenCredits != null) {
				Color temp = screenCredits.color;
				temp.a = 1.0f - (screenTimer / fixedTimer);
				screenCredits.color = temp;
			}
			yield return null;
		}
		if (!activated) {
			activated = true;
			screenCredits.gameObject.SetActive (false);
		}
	}
}
