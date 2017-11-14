using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManagerCheck : MonoBehaviour {

    public GameObject BGMManager;

	// Use this for initialization
	void Start () {
        if (FindObjectOfType<BGMManager>())
            return;
        else
            Instantiate(BGMManager, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
