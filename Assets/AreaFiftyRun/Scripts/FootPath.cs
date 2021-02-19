using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPath
{
    //these are nodes in the path that the foot will travel
    private List<Vector2> waypoints = new List<Vector2>();
    //these are the distances between waypoints 
    private List<float> wayPointDistances = new List<float>();
    //the total distance through all the nodes of the foot's path
    private float totalPathDistance = 0;

    public FootPath(List<Vector2> nodes)
    {
        //saving values to class
        //TODO: ask how the input variables of a function can be saved to a different variable of the same name.
        waypoints = nodes;

        //saving the distance from each node to the next one as a list of floats. the last waypoint proceeds it. 
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            wayPointDistances.Add(Vector2.Distance(waypoints[i], waypoints[i + 1]));
            totalPathDistance += wayPointDistances[i];
        }
    }

    public Vector2 TargetPosition(float percentThrough)//percent through is a float that represents what percent through the path the position is at.
    {
        //find which waypoint the position comes after
        int waypointBeforePositionIndex = 0;
        float distanceFromStart = percentThrough * totalPathDistance;
        float currentDistance = 0;
        float distanceFromWaypoint = distanceFromStart;

        for (int i = 0; currentDistance + wayPointDistances[i] < distanceFromStart; i++) 
        {
            currentDistance += wayPointDistances[i];
            waypointBeforePositionIndex = i;

            //find how far away from said waypoint the position is
            distanceFromWaypoint -= wayPointDistances[i];
        }

        //find the direction from said waypoint to the position
        Vector2 directionToPosition = (waypoints[waypointBeforePositionIndex + 1] - waypoints[waypointBeforePositionIndex]).normalized;

        //return the waypoint + directionToPosition * distanceFromWaypoint to get the target position.
        return waypoints[waypointBeforePositionIndex] + directionToPosition * distanceFromWaypoint;
    }

    public void DrawDebugPath(Vector2 offset)
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (i < waypoints.Count - 1)
            {
                Debug.DrawLine(offset + waypoints[i], offset + waypoints[i + 1], Color.green);
            }
        }
    }
}
