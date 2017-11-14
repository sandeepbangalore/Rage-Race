﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    public AudioSource BGM;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType<BGMManager>().Length > 1)
            Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeBGM( AudioClip music)
    {
        if (BGM.clip.name == music.name)
            return;

        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }
}
