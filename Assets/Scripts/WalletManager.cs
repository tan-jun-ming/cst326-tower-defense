using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI wallet_display;

    public int starting_amount = 50;
    private int funds = 0;

    void Start()
    {
        funds = starting_amount;
        update_display();
    }

    public void add_value(int value)
    {
        funds += value;
        update_display();
    }

    public int purchase(int value)
    {
        funds -= value;
        update_display();
        return funds;
    }

    public bool can_purchase(int value)
    {
        return funds >= value;
    }

    public int get_funds()
    {
        return funds;
    }

    void update_display()
    {
        wallet_display.text = "$" + funds.ToString();
    }
}
