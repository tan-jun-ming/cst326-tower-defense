using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TowerManager towermanager;
    public EnemyManager enemymanager;

    public GameObject towermodel;

    public Canvas start_canvas;
    public Canvas restart_canvas;

    void Start()
    {
        enemymanager.gamemanager = this;
        restart_canvas.enabled = false;
        set_timescale(1f);
    }

    public void damage_tower(float amount)
    {
        towermanager.damage(amount);
    }

    public void start_game()
    {
        start_canvas.enabled = false;
        enemymanager.start_wave();
    }

    public void game_over()
    {
        restart_canvas.enabled = true;
        set_timescale(0);
        
    }
    public void restart_game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void set_timescale(float num)
    {
        Time.timeScale = num;
    }

    public void increase_timescale(float num)
    {
        Time.timeScale += num;
    }
}
