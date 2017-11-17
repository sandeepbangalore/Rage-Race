/*
 *                                  Team Rocket 
 * 
 *  Agneya A Kerure                 akerure3        kerure.agneya@gatech.edu
 *  Christina Chung                 cchung44        cchung44@gatech.edu
 *  Erin Hsu                        ehsu7           ehsu7@gatech.edu
 *  Dibyendu Mondal                 dmondal6        dibyendu@gatech.edu
 *  Sandeep Banaglore Venkatesh     sbv7            sandeepbanaglore@gatech.edu
 * 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour 
{
	private string[] names = {"idle","applause","applause2","celebration","celebration2","celebration3"};

	// Use this for initialization
	void Start () 
	{
		Animation[] AudienceMembers = gameObject.GetComponentsInChildren<Animation>();
		foreach(Animation anim in AudienceMembers)
		{
			string thisAnimation = names[Random.Range(0,5)];

			anim.wrapMode = WrapMode.Loop;
			anim.CrossFade(thisAnimation);
			anim[thisAnimation].time = Random.Range(0f,3f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}