using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public Transform waypoint_list;

    private List<Transform> waypoints = new List<Transform>();
    // Start is called before the first frame update
    void Awake()
    {
        int i = 0;
        while (true)
        {
            Transform new_waypoint = waypoint_list.Find(i.ToString());
            if (new_waypoint == null)
            {
                break;
            }
            waypoints.Add(new_waypoint);
            i++;
        }
    }

    public Vector3 get_next_waypoint(int waypoint)
    {
        return waypoints[waypoint].position;
    }

    public int get_max_waypoints()
    {
        return waypoints.Count;
    }
}
