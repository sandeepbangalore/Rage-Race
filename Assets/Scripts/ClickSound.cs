using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class ClickSound : MonoBehaviour {

    public AudioClip sound;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    // Use this for initialization
    void Start ()
    {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
        // Add any other settings here..
        button.onClick.AddListener(() => PlaySound());
	}

    void  PlaySound ()
    {
        source.PlayOneShot(sound);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
