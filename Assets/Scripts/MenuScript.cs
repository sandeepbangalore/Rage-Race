using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas startMenu;
	public Canvas creditsMenu;
	public Button playText;
	public Button creditsText;
	public Button exitText;
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
	}

	public void CreditPress()
	{
		Debug.Log("Credits Pressed!");
		creditsMenu.enabled = true;
		quitMenu.enabled = false;
		startMenu.enabled = false;
		playText.enabled = false;
		creditsText.enabled = false;
		exitText.enabled = false;
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
	}

	public void NoPress()
	{
		Debug.Log("No Pressed!");
		creditsMenu.enabled = false;
		quitMenu.enabled = false;
		startMenu.enabled = true;
		playText.enabled = true;
		creditsText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel()
	{
		Application.LoadLevel(1);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

}
