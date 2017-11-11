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

public class CameraSelector : MonoBehaviour {
	public GameObject[] cameraArray;
	public float time;
	public Image Screen = null;
	private float startTime;
	private int position = 0;
	private float initialAlpha;
	private bool activated =true;
	// Use this for initialization
	void Awake() {
		time= Random.Range (3, 5);
		cameraArray[position].SetActive(true);
		startTime = Time.timeSinceLevelLoad;
		initialAlpha = Screen.color.a;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad - startTime > time && activated) {
			StartCoroutine (ScreenFade ());
		}
	}

	private IEnumerator ScreenFade () {
		float screenTimer = 2.0f;
		float fixedTimer = 2.0f;
		while (screenTimer >= 0.0f) {
			activated = false;
			screenTimer -= Time.deltaTime;
			if (Screen != null) {
				Color temp = Screen.color;
				temp.a = 1.0f - (screenTimer / fixedTimer);
				Screen.color = temp;
			}
			yield return null;
		}
		if (!activated) {
			cameraArray [position].SetActive (false);
			if (position == cameraArray.Length - 1) {
				position = 0;
			} else
				position++;
			cameraArray [position].SetActive (true);
		}
		while (screenTimer <= fixedTimer) {
			activated = false;
			screenTimer += Time.deltaTime;
			if (Screen != null) {
				Color temp = Screen.color;
				temp.a = 1.0f - (screenTimer / fixedTimer);
				Screen.color = temp;
			}
			yield return null;
		}
		if (!activated) {
			startTime = Time.timeSinceLevelLoad;
			time = Random.Range (5, 7);
			activated = true;
		}
	}
}
