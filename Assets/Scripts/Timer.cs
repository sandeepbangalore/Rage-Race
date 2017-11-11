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


using UnityEngine;
using System.Collections;

public class Timer {

	private float timer = 0.0f;
	public Timer () {
		timer = 0.0f;
	}

	public void Tick (float tick) {
		if (timer > 0.0f)
			timer -= tick;
		else
			timer = 0.0f;
	}
	public void AddTime (float addTime) {
		timer += addTime;
	}
	public float GetTime (){
		return timer;
	}
	public void Reset (){
		timer = 0.0f;
	}
}
