using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PositionManager : MonoBehaviour {

    public Text positionText;
    public string time;

    public bool forceResetNav = false;
    public Transform[] leftWaypoints;
    public Transform[] rightWaypoints;
    private Vector3[] waypoints;
    private Vector3[] closestWaypoints;
    private int lapProgress = 1; // TODO: implement lap progress
    private int waypointProgress = 0;
    private Vector3 nextWaypoint;
    private GameManager MyGameManager = null;
    private PositionManager[] runners;
    private bool isPlayer = false;
    private PositionManager playerPositionManager = null;
    private float finalPlace = Mathf.Infinity;


    // Use this for initialization
    void Start() {
        MyGameManager = GameManager.Instance;

        // set midpoint waypoints if this script is attached to the player
        if (this.tag == "Player")
        {
            setWaypoints(false);
            isPlayer = true;
        }

        // get all runners/competitors

        runners = FindObjectsOfType<PositionManager>();
        positions();
    }

    public string GetTime()
    {
        return this.GetComponent<Timer1>().timerText.text;
    }

    // Call this function whenever the gameobject with this script gets displaced outside of their
    // control. usually occurs during collisions or respawns
    public void repairWaypoints()
    {
        //var stackTrace = new System.Diagnostics.StackTrace();

        // sort waypoints by closest distance to this gameobject
        System.Array.Sort(closestWaypoints,
            (a, b) => ((transform.position - a).magnitude).CompareTo((transform.position - b).magnitude));

        // of the two closest waypoints, nextwaypoint should be the one further along the track 
        // (if it isn't already)
        //print("repair option 1: " + closestWaypoints[0] + ", index: " + System.Array.IndexOf(waypoints, closestWaypoints[0]));
        //print("repair option 2: " + closestWaypoints[1] + ", index: " + System.Array.IndexOf(waypoints, closestWaypoints[1]));
        int index = Mathf.Max(System.Array.IndexOf(waypoints, closestWaypoints[0]),
            System.Array.IndexOf(waypoints, closestWaypoints[1]));
        if (waypointProgress != index - 1)
        {
            if (index == waypoints.Length - 1)
            {
                index = 0;
            }
            //print(gameObject.name + " repairing waypoints" + index);
            //print((index-1).ToString() + closestWaypoints[0] + closestWaypoints[1]);
            waypointProgress = index - 1;
            nextWaypoint = waypoints[++waypointProgress];
            forceResetNav = true;
        }
    }

    // repair waypoints after AI dies on ramp
    public void diedOnRamp()
    {
        repairWaypoints();
        recalculateNextRandomWaypoints(2);
    }

    // recalculates the next n waypoints randomly
    // called when the AI dies on ramp (usually their random waypoints are bad)
    private void recalculateNextRandomWaypoints(int n)
    {
        int maxIter = Mathf.Min(waypointProgress + n, waypoints.Length);
        for (int i = waypointProgress; i < maxIter; i++)
        {
            float r = Random.Range(0.2f, 0.8f);
            Vector3 waypt = Vector3.Lerp(leftWaypoints[i].position, rightWaypoints[i].position, n);
            waypoints[i] = waypt;
        }
        nextWaypoint = waypoints[waypointProgress];
        System.Array.Copy(waypoints, closestWaypoints, waypoints.Length);
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
        closestWaypoints = new Vector3[waypoints.Length];
        for (int i = 0; i < leftWaypoints.Length; i++)
        {
            float n = 0.5f; // if not random, default to to interpolating to the midpt of the two waypts

            if (randWaypts)
            {
                n = Random.Range(0.2f, 0.8f);
            }

            Vector3 waypt = Vector3.Lerp(leftWaypoints[i].position, rightWaypoints[i].position, n);
            waypoints[i] = waypt;
            nextWaypoint = i == 0 ? waypt : nextWaypoint; // default nextWaypoint to the first one
        }

        System.Array.Copy(waypoints, closestWaypoints, waypoints.Length);
    }

    // gets the race position of this gameobject (starting at 1)
    public int getPosition()
    {
        int position = 1;
        System.Array.Sort(runners, sortByPosition);

        foreach (PositionManager runner in runners)
        {
            if (runner == this)
            {
                position += System.Array.IndexOf(runners, runner);
            }
        }
        return position;
    }

    // gets the straight line distance of this gameobject to the first place runner gameobject
    public float getDistanceToFirstPlace()
    {
        System.Array.Sort(runners, sortByPosition);
        PositionManager firstPlaceRunner = runners[0];
        return (transform.position - firstPlaceRunner.transform.position).magnitude;
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
        if (hasReachedTarget(nextWaypoint, 12f))
        {
            nextWaypoint = waypoints[++waypointProgress];
            //if (isPlayer)
            //{
            //    print(gameObject.name + ": " + waypointProgress + ", next waypoint: " + nextWaypoint);
            //}
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

    // returns whether this runner is at least a given number of spaces behind the player
    // can use either position/race rankings or waypoint progress
    public bool isBehindPlayer(bool usingPosition, int spaces)
    {
        // grab the player position manager if not already stored
        if (playerPositionManager == null)
        {
            foreach (PositionManager runner in runners)
            {
                if (runner.getIsPlayer())
                {
                    playerPositionManager = runner;
                    break;
                }
            }
        }

        if (usingPosition)
        {
            System.Array.Sort(runners, sortByPosition);
            int playerPosition = 0;
            int runnerPosition = 0;

            foreach (PositionManager runner in runners)
            {
                if (runner.getIsPlayer())
                {
                    playerPosition = System.Array.IndexOf(runners, runner);
                }
                if (runner == this)
                {
                    runnerPosition = System.Array.IndexOf(runners, runner);
                }
            }

            //print("player position: " + playerPosition);
            //print("runner position: " + runnerPosition);

            return runnerPosition - playerPosition >= spaces;
        }
        else
        {
            // TODO currently does not consider laps
            //print("my waypoint progress: " + getWaypointProgress());
            //print("player waypoint progress: " + playerPositionManager.getWaypointProgress());
            //print("should I warp? " + ((playerPositionManager.getWaypointProgress() - getWaypointProgress() >= spaces) ? "yes" : "no"));
            return playerPositionManager.getWaypointProgress() - getWaypointProgress() >= spaces;
        }
    }

    // return whether this GameObject is within a certain difference (distance) to a location loc
    bool hasReachedTarget(Vector3 loc, float difference)
    {
        float distance = (loc - transform.position).magnitude;
        return distance <= difference;
    }

    // Update is called once per frame
    void Update () {
        updateWaypointProgress();
        //if (positionText != null && MyGameManager.getGameState() == GameManager.States.Race)
        if (positionText != null && finalPlace == Mathf.Infinity)
        {
            updatePositionDisplay();
        }

		//if (MyGameManager.getGameState() == GameManager.States.Finish) {

		//}
	}

    public void setFinalPlace(int place)
    {
        finalPlace = place;
    }

	public void finalResults()
    {
        //System.Array.Sort(runners, sortByPosition);
        //MyGameManager.finalResults(runners);
        System.Array.Sort(runners, sortByFinalPositions);
        MyGameManager.finalResults(runners);
    }

    static int sortByFinalPositions(PositionManager runner1, PositionManager runner2)
    {
        int ret = runner1.finalPlace.CompareTo(runner2.finalPlace);
        if (ret != 0)
        {
            return ret;
        }
        else
        {
            return sortByPosition(runner1, runner2);
        }
    }

	public void positions()
    {
		System.Array.Sort (runners, sortByPosition);
		MyGameManager.currentPositions (runners);
	}
}
