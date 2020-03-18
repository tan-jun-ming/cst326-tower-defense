using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TowerManager towermanager;
    public WalletManager walletmanager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float distance = 5f;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, distance);

            if (hit)
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    ((Enemy)hit.transform.gameObject.GetComponent(typeof(Enemy))).damage(10f);
                }
            }

        }
    }

    public void damage_tower(float amount)
    {
        towermanager.damage(amount);
    }
    public void report_tower_death()
    {
        print("you died");
    }
}
