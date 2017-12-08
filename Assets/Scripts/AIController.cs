using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


[RequireComponent(typeof(AINavSteeringController))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    //public enum State // currently unused
    //{
    //    SetNewGoal,
    //    PathToWaypoint

    //}

    private PlayerPowerupScript powerup = null;
    public float speedPowerUp = 1f;
    private bool shouldUseSlow = false;
    private bool shouldUseDriller = false;
    private bool shouldUseHoming = false;

    public float moveSpeedMultiplier = 1f;

    private PositionManager positionManager;
    private Vector3 nextWaypoint;
    private bool isPathingToPowerup = false;

    //public State state = State.SetNewGoal; // currently unused

    public float waitTime;
    protected float beginWaitTime;
    private Vector3 lastLocation;
	private GameManager MyGameManager = null;
    private Animator anim;
    private AINavSteeringController aiSteer;
    private NavMeshAgent agent;

    private bool isDebug = false;

    // may change depending on how game start is implemented
    bool hasGameStarted()
    {
		if (MyGameManager.getGameState() != GameManager.States.Countdown)
        {
			anim.speed = moveSpeedMultiplier * speedPowerUp;
            return true;
        }
		else
        {
            anim.speed = 1f + Random.Range(-0.3f, 0.3f);
            return false;
        }
        //return Time.timeSinceLevelLoad > 3.0f;
    }

    bool hasGameEnded()
    {
        return MyGameManager.getGameState() == GameManager.States.Finish;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.impulse.magnitude > 15)
    //    {
    //        //debugPrint("collision impulse magnitude = " + collision.impulse.magnitude);
    //        agent.enabled = false;
    //        Invoke("reenableNavmeshAgent", 0.2f);
    //    }
    //}

    //void reenableNavmeshAgent()
    //{
    //    agent.enabled = true;
    //    aiSteer.clearWaypoints();
    //    aiSteer.setWaypoint(nextWaypoint);
    //}


    // Use this for initialization
    void Start()
    {
		MyGameManager = GameManager.Instance;

        // initialize nav mesh steering
        aiSteer = GetComponent<AINavSteeringController>();
        anim = GetComponent<Animator>();
        aiSteer.Init();
        aiSteer.waypointLoop = false;
        aiSteer.stopAtNextWaypoint = false;
        agent = GetComponent<NavMeshAgent>();

        // get powerup script
        powerup = this.GetComponent<PlayerPowerupScript>();

        // get position manager script
        positionManager = this.GetComponent<PositionManager>();
        positionManager.setWaypoints(true); // use random waypoints

        // prepare navigation
        //nextWaypoint = positionManager.getNextWaypoint();
        nextWaypoint = transform.position;
        aiSteer.setWaypoint(nextWaypoint);
        //agent.isStopped = true;
        aiSteer.useNavMeshPathPlanning = true;

        // set up to check if agent gets stuck
        beginWaitTime = Time.timeSinceLevelLoad;
        lastLocation = transform.position;

    }

    // print function for debugging
    void debugPrint(string s)
    {
        if (isDebug)
        {
            Debug.Log(s);
        }
    }

    // Update waypoint progress and the next waypoint if agent has reached nextWaypoint
    void updateNextWaypoint()
    {
        Vector3 potentialNextWaypoint = positionManager.getNextWaypoint();
        if (potentialNextWaypoint != nextWaypoint)
        {
            nextWaypoint = potentialNextWaypoint;
            aiSteer.clearWaypoints();
            aiSteer.setWaypoint(nextWaypoint);
        }
    }

    // return whether the agent is within a certain difference (distance) to a location loc
    bool hasReachedTarget(Vector3 loc, float difference)
    {
        float distance = (loc - transform.position).magnitude;
        return distance <= difference;
    }

    // if AI agent has not moved very far from their last recorded position
    // and it is at least 1 waypoint behind the player, warp to the next waypoint
    // also update last position
    void checkStuck()
    {
        if (hasReachedTarget(lastLocation, 10f))
        {
            debugPrint("I'm stuck!" + transform.position);
            debugPrint("I've passed waypoint " + positionManager.getWaypointProgress());
            debugPrint("next waypoint = " + nextWaypoint);
            
            if (positionManager.isBehindPlayer(false, 2)) // check if agent is behind player using waypoints, not race position
            {
                debugPrint("I'm warping!!");
                agent.Warp(nextWaypoint);
                powerup.AICatchUpCheat();
            }

        }
        lastLocation = transform.position;
    }

    public bool[] getShouldUsePowerups()
    {
        return new bool[] { shouldUseSlow, shouldUseDriller, shouldUseHoming };
    }

    void updateShouldUsePowerups()
    {
        updateShouldUseSlow();
        updateShouldUseDriller(10f);
        updateShouldUseHoming();
    }

    // AI should use the slow powerup if it is in front of the player
    void updateShouldUseSlow()
    {
        shouldUseSlow = positionManager.isBehindPlayer(true, -1); // check if agent is in front of player using race position, not waypoints
    }

    // AI should use the driller powerup if there is at least 1 runner in front of this AI a minimum distance away
    void updateShouldUseDriller(float minDist)
    {
        shouldUseDriller = positionManager.getPosition() > 1 && positionManager.getDistanceToFirstPlace() > minDist; 
    }

    // AI should use the homing powerup if it's not in 1st place
    void updateShouldUseHoming()
    {
        shouldUseHoming = positionManager.getPosition() != 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGameStarted())
        {
            updateNextWaypoint();
            updateShouldUsePowerups();

            if (Time.timeSinceLevelLoad - beginWaitTime > waitTime)
            {
                beginWaitTime = Time.timeSinceLevelLoad;
                checkStuck();
            }
        }
        else if (hasGameEnded())
        {
            aiSteer.clearWaypoints();
            nextWaypoint = transform.position;
            aiSteer.setWaypoint(transform);
            //agent.isStopped = true;
            anim.speed = 1f;
        }

    }

	public void Stunned()
    {
		anim.SetTrigger ("Stun");
	}
}