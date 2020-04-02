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
