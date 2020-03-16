using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.02f;
    public float health = 50f;

    private int next_waypoint_num = 0;
    private int max_waypoints = -1;
    private Vector3 next_waypoint;
    private WaypointManager waypointmanager;

    private float distance_travelled = 0f;

    // Start is called before the first frame update
    void Start()
    {
        waypointmanager = (WaypointManager)GameObject.Find("WaypointManager").GetComponent(typeof(WaypointManager));
        next_waypoint = waypointmanager.get_next_waypoint(next_waypoint_num);
        max_waypoints = waypointmanager.get_max_waypoints();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        step_towards_waypoint(speed);
    }

    void step_towards_waypoint(float distance)
    {
        float remaining = distance;

        while (remaining > 0.0001)
        {
            Vector3 new_pos = Vector3.MoveTowards(gameObject.transform.position, next_waypoint, remaining);

            float travelled = Mathf.Abs((new_pos - gameObject.transform.position).magnitude);

            remaining -= travelled;
            distance_travelled += travelled;

            print(travelled);
            print(remaining);

            gameObject.transform.position = new_pos;

            if (new_pos == next_waypoint)
            {
                next_waypoint_num++;

                if (next_waypoint_num >= max_waypoints)
                {
                    damage_tower();
                    return;
                }
                next_waypoint = waypointmanager.get_next_waypoint(next_waypoint_num);
            }

        }

    }

    void damage_tower()
    {
        print("ow");
        GameObject.Destroy(gameObject);
    }
}
