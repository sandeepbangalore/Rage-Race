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

public class ThrowObjects : MonoBehaviour 
{
	public GameObject player;
    public Rigidbody rbody_player;
    private Vector3 playerVelocity;
    public GameObject ballPrefab;
    private Rigidbody rbody_ball;
    private GameObject heldBall = null;
    private float beginTime = 0.0f;

	// Use this for initialization
	void Start () 
	{
		playerVelocity = rbody_player.velocity;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(beginTime != 0.0f && Time.timeSinceLevelLoad - beginTime >= 6)
			Destroy(heldBall);
	}

	void ExecuteThrowCallback()
    {
        // heldBall.transform.parent = null;
        rbody_ball.isKinematic = false;

        //predictive code
        float ballSpeed = 15.0f;
        float playerSpeed = playerVelocity.magnitude;
        Vector3 playerDir = playerVelocity;
        playerDir.Normalize();

        Vector3 playerToBall = heldBall.transform.position - (player.transform.position + new Vector3(0,1.6f,0));
        float playerToBallDist = playerToBall.magnitude;
        Vector3 playerToBallDir = playerToBall;
        playerToBallDir.Normalize();

        float angle = Vector3.Dot(playerToBallDir,playerDir);

        float a = ballSpeed*ballSpeed - playerSpeed*playerSpeed;
        float b = 2*playerToBallDist*playerSpeed*angle;
        float c = -playerToBallDist*playerToBallDist;
        float disc = b*b - 4*a*c;
        float t0 = (-b + Mathf.Sqrt(disc))/(2*a);
        float t1 = (-b - Mathf.Sqrt(disc))/(2*a);

        float t = Mathf.Min(t0,t1);
        if(t0*t1 < 0)
            t = Mathf.Max(t0,t1);
        Vector3 launchV = playerVelocity - playerToBall/t;
        // Vector3 launchV = force*(mover.transform.position - transform.position);
        // EventManager.TriggerEvent<ThrowEvent, Vector3>(heldBall.transform.position);
        rbody_ball.AddForce(launchV, ForceMode.VelocityChange);
    }

    void OnTriggerExit(Collider c)
    {
		if (ballPrefab != null)
        {
          heldBall = Instantiate(ballPrefab, this.transform.position + new Vector3(0,5.0f,-this.transform.localScale.z/2), Quaternion.identity);
          beginTime = Time.timeSinceLevelLoad;
          rbody_ball = heldBall.GetComponent<Rigidbody>();
          // heldBall.transform.parent = leftHand;
          // rbody_ball.isKinematic = true;
          ExecuteThrowCallback();
        }
    }
}
