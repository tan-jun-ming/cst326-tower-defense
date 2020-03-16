using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public Healthbar healthbar;

    public float max_health = 10f;
    private float curr_health = 0;
    private GameManager gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = (GameManager)GameObject.Find("GameManager").GetComponent(typeof(GameManager));
        curr_health = max_health;
    }

    public void damage(float amount)
    {
        curr_health = Mathf.Max(curr_health - amount, 0);

        healthbar.health = curr_health / max_health;

        if (curr_health == 0)
        {
            die();
        }
    }

    void die()
    {
        gamemanager.report_tower_death();
    }
}
