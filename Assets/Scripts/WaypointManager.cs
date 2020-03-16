using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public Transform waypoint_list;

    private List<Transform> waypoints = new List<Transform>();
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
