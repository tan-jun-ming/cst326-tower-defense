using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public WaypointManager waypointmanager;
    public WalletManager walletmanager;

    private Vector3 starting;
    
    void Start()
    {
        starting = waypointmanager.get_next_waypoint(0);
    }
    public void spawn_enemy(int ind)
    {
        GameObject obj = GameObject.Instantiate(enemies[ind], starting, Quaternion.Euler(90, 0, 0));
        ((Enemy)obj.GetComponent(typeof(Enemy))).enemymanager = this;
    }

    public void report_death(int value)
    {
        walletmanager.add_value(value);
    }
}
