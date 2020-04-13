using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public WaypointManager waypointmanager;
    public WalletManager walletmanager;

    public AudioSource speaker;
    public AudioClip deathsound;

    [HideInInspector]
    public GameManager gamemanager;

    private Vector3 starting;

    private int[] wave_data = { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1 };
    private int enemy_count = 0;
    private Queue<int> enemy_pool = new Queue<int>();

    private bool wave_started = false;
    private int wave_delay_max = 30;
    private int wave_delay = 0;
    
    void Start()
    {
        starting = waypointmanager.get_next_waypoint(0);
    }

    public void start_wave()
    {
        if (!wave_started)
        {
            wave_delay = wave_delay_max;
            foreach (int i in wave_data)
            {
                enemy_pool.Enqueue(i);
                enemy_count++;
            }
            wave_started = true;

        }
    }

    void FixedUpdate()
    {
        if (wave_started)
        {
            if (enemy_pool.Count != 0)
            {
                wave_delay--;
                if (wave_delay == 0)
                {
                    wave_delay = wave_delay_max;
                    GameObject obj = GameObject.Instantiate(enemies[enemy_pool.Dequeue()], starting, Quaternion.Euler(90, 0, 0));
                    ((Enemy)obj.GetComponent(typeof(Enemy))).enemymanager = this;
                }
            }

        }
    }

    public void report_death(int value)
    {
        enemy_count--;
        walletmanager.add_value(value);
        speaker.PlayOneShot(deathsound, 1f);

        if (enemy_count <= 0)
        {
            gamemanager.game_over();
        }
    }
}
