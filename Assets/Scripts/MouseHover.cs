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
using UnityEngine.EventSystems; 

public class MouseHover : MonoBehaviour
, IPointerEnterHandler
// ... And many more available!

{
	EventSystem position;
	void Awake(){
		EventSystem position = GetComponent<EventSystem> ();
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		position.firstSelectedGameObject = eventData.hovered[0];
	}

//	public void OnPointerExit(PointerEventData eventData)
//	{
//		target = Color.red;
//	}
}
