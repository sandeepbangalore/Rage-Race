using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour {

	public Transform player;
	public Camera main;
	public Shader overdraw;
	void Start() {
		main.SetReplacementShader  (overdraw, "Map");
	}
	void LateUpdate ()
	{
		if (player != null) {
			Vector3 newPosition = player.position;
			newPosition.y = transform.position.y;
			transform.position = newPosition;

			transform.rotation = Quaternion.Euler (90f, player.eulerAngles.y, 0f);
		}
	}

}