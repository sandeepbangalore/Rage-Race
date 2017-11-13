using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionManager : MonoBehaviour {

    public Text positionText;
    private bool isPlayer = false;

    public Transform[] leftWaypoints;
    public Transform[] rightWaypoints;
    private Vector3[] waypoints;
    private int lapProgress = 1; // TODO: implement lap progress
    private int waypointProgress = 0;
    private Vector3 nextWaypoint;

    private PositionManager[] runners;
    

	// Use this for initialization
	void Start () {
        // set midpoint waypoints if this script is attached to the player
        if (this.tag == "Player")
        {
            setWaypoints(false);
            isPlayer = true;
        }

        // get all runners/competitors
        runners = FindObjectsOfType<PositionManager>();
	}

    // creates waypoints for the whole track
    public void setWaypoints(bool randWaypts)
    {
        // alert if there are problems with waypoints
        if (leftWaypoints.Length != rightWaypoints.Length)
        {
            Debug.Log("Must have equal number of left waypoints and right waypoints");
        }

        // for all the left & right waypoints of the track, interpolate a position between them
        // and append the waypoint to private list of waypoints
        waypoints = new Vector3[leftWaypoints.Length];
        for (int i = 0; i < leftWaypoints.Length; i++)
        {
            float n = 0.5f; // if not random, default to to interpolating to the midpt of the two waypts

            if (randWaypts)
            {
                n = Random.Range(0.1f, 0.9f);
            }

            Vector3 waypt = Vector3.Lerp(leftWaypoints[i].position, rightWaypoints[i].position, n);
            waypoints[i] = waypt;
            nextWaypoint = i == 0 ? waypt : nextWaypoint; // default nextWaypoint to the first one
        }
    }

    // getter for isPlayer (is this GameObject the player?)
    public bool getIsPlayer()
    {
        return isPlayer;
    }

    // getter for lapProgress
    public int getLapProgress()
    {
        return lapProgress;
    }

    // getter for waypointProgress
    public int getWaypointProgress()
    {
        return waypointProgress;
    }

    // getter for nextWaypoint
    public Vector3 getNextWaypoint()
    {
        return nextWaypoint;
    }

    // Update waypoint progress and the next waypoint if this GameObject has reached nextWaypoint
    void updateWaypointProgress()
    {
        if (hasReachedTarget(nextWaypoint, 20f))
        {
            nextWaypoint = waypoints[++waypointProgress];
        }
    }

    // updates the position text display with the position of this GameObject
    void updatePositionDisplay()
    {
        System.Array.Sort(runners, sortByPosition);

        foreach (PositionManager runner in runners)
        {
            if (runner.getIsPlayer())
            {
                int place = System.Array.IndexOf(runners, runner) + 1;
                string placeText = place.ToString();

                switch (place % 10)
                {
                    case 1:
                        placeText += "st";
                        break;

                    case 2:
                        placeText += "nd";
                        break;

                    case 3:
                        placeText += "rd";
                        break;

                    default:
                        placeText += "th";
                        break;

                }
                positionText.text = placeText;
                break;
            }
        }
    }

    // sorting function for position
    // first sort by lap progress, then waypoint progress, then distance to next waypoint
    static int sortByPosition(PositionManager runner1, PositionManager runner2)
    {
        int compareVal = -runner1.getLapProgress().CompareTo(runner2.getLapProgress());
        if (compareVal == 0)
        {
            compareVal = -runner1.getWaypointProgress().CompareTo(runner2.getWaypointProgress());
            if (compareVal == 0)
            {
                compareVal = runner1.getDistanceToNextWaypoint().CompareTo(runner2.getDistanceToNextWaypoint());
            }
        }

        return compareVal;
    }

    public float getDistanceToNextWaypoint()
    {
        return (transform.position - nextWaypoint).magnitude;
    }

    // return whether this GameObject is within a certain difference (distance) to a location loc
    bool hasReachedTarget(Vector3 loc, float difference)
    {
        float distance = (loc - transform.position).magnitude;
        return distance < difference;
    }

    // Update is called once per frame
    void Update () {
        updateWaypointProgress();
        if (positionText != null)
        {
            updatePositionDisplay();
        }
	}
}
