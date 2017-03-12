using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[assembly: AssemblyVersionAttribute("1.0.0.0")]
public class MoveEnemy : MonoBehaviour {

    [HideInInspector]
    public GameObject[] waypoints;          //Stores waypoints into an array
    private int currentWaypoint = 0;        //Tracks which waypoint the enemy is currently walking away from
    private float lastWaypointSwitchTime;   //Stores time when enemy passed over the last waypoint
    public float speed = 1.0f;              //Enemy speed


	// Use this for initialization
	void Start () {
        lastWaypointSwitchTime = Time.time; //Initializes lastWaypointSwitchTime to the current time
	}
	
	// Update is called once per frame
	void Update () {
        //retrieve the start and end of the current path segment
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        //Calculate the time needed to complete the distance of the current path segment using Time = distance / speed formula
        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        // interpolate the current position of the enemy between the start and end positions
        gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        //check if enemy has reached the end position if true. Handled with two possible scenarios.
        if (gameObject.transform.position.Equals(endPosition))
        {
            //Enemy has not reached end position yet.
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;                      //increment currentWaypoint
                lastWaypointSwitchTime = Time.time;     //update lastWaypointSwitchTime
            }
            //Enemy reached last waypoint
            else
            {
                Destroy(gameObject);                    //destorys enemy object and deduct lives
                
                //ToDo: deduct health
            }
        }
    }
}
