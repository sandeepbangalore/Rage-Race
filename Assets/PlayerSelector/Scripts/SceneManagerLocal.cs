using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerLocal : MonoBehaviour {

	private string currentName = "";
	private string trackName = "";
	private static SceneManagerLocal _Instance = null;
	public static SceneManagerLocal Instance{
		get{ 
			if (_Instance == null) {
				_Instance = (SceneManagerLocal)FindObjectOfType (typeof(SceneManagerLocal));
			}
			return _Instance;	
		}
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		if (FindObjectsOfType<SceneManagerLocal>().Length > 1)
			Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string getCurrent() {
		return currentName;
	}

	public void setCurrent(string name) {
		currentName = name;
	}

	public void setTrack(string name) {
		trackName = name;
	}

	public string getTrack() {
		return trackName;
	}
}
