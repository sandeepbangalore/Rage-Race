using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


[RequireComponent(typeof(AINavSteeringController))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    public enum State // currently unused
    {
        SetNewGoal,
        PathToWaypoint

    }

    public float moveSpeedMultiplier = 1f;

    private PositionManager positionManager;
    private Vector3 nextWaypoint;

    public State state = State.SetNewGoal; // currently unused

    public float waitTime;
    protected float beginWaitTime;
    private Vector3 lastLocation;

    private Animator anim;
    private AINavSteeringController aiSteer;
    private NavMeshAgent agent;

    private bool isDebug = true;

    // may change depending on how game start is implemented
    bool hasGameStarted()
    {
        return Time.timeSinceLevelLoad > 3.0f;
    }


    // Use this for initialization
    void Start()
    {
        // initialize nav mesh steering
        aiSteer = GetComponent<AINavSteeringController>();
        anim = GetComponent<Animator>();
        anim.speed = moveSpeedMultiplier;
        aiSteer.Init();
        aiSteer.waypointLoop = false;
        aiSteer.stopAtNextWaypoint = false;
        agent = GetComponent<NavMeshAgent>();

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
        //if (nextWaypoint == null || potentialNextWaypoint != nextWaypoint)
        if (potentialNextWaypoint != nextWaypoint)
            {
            aiSteer.clearWaypoints();
            aiSteer.setWaypoint(potentialNextWaypoint);
        }
    }

    // return whether the agent is within a certain difference (distance) to a location loc
    bool hasReachedTarget(Vector3 loc, float difference)
    {
        float distance = (loc - transform.position).magnitude;
        return distance < difference;
    }

    // if AI agent has not moved very far from their last recorded position, reset next waypoint.
    // also update last position
    void checkStuck()
    {
        if (hasReachedTarget(lastLocation, 4f))
        {
            // TODO: fix. maybe respawn?
            debugPrint("I'm stuck!" + transform.position);
            debugPrint("next waypoint = " + nextWaypoint);
            //agent.isStopped = true;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            //aiSteer.clearWaypoints();
            //aiSteer.prioritizeFaceWaypoint = true;
            //agent.enabled = false;
            //agent.enabled = true;
            //aiSteer.setWaypoint(nextWaypoint);

        }
        lastLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGameStarted())
        {
            updateNextWaypoint();

            if (Time.timeSinceLevelLoad - beginWaitTime > waitTime)
            {
                beginWaitTime = Time.timeSinceLevelLoad;
                checkStuck();
            }
        }
        else
        {
            aiSteer.setWaypoint(transform);
        }

    }
}