using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.02f;
    public float max_health = 50f;
    public float attack = 1f;
    public int value = 10;

    public Healthbar healthbar;
    public EnemyManager enemymanager;

    public float curr_health;
    private int next_waypoint_num = 0;
    private int max_waypoints = -1;
    private Vector3 next_waypoint;
    private WaypointManager waypointmanager;
    private GameManager gamemanager;

    private float distance_travelled = 0f;

    public float get_distance()
    {
        return distance_travelled;
    }

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));

        waypointmanager = (WaypointManager)GameObject.Find("WaypointManager").GetComponent(typeof(WaypointManager));
        next_waypoint = waypointmanager.get_next_waypoint(next_waypoint_num);
        max_waypoints = waypointmanager.get_max_waypoints();

        curr_health = max_health;
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

    public void damage(float amount)
    {
        curr_health = Mathf.Max(curr_health-amount, 0);

        healthbar.health = curr_health / max_health;

        if (curr_health == 0)
        {
            die(true);
        }

    }

    void damage_tower()
    {
        gamemanager.damage_tower(attack);
        die(false);
    }

    void die(bool killed)
    {
        if (killed)
        {
            enemymanager.report_death(value);
        }
        GameObject.Destroy(gameObject);
    }
}
