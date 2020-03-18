using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI wallet_display;

    private int funds = 0;

    public void add_value(int value)
    {
        funds += value;

        wallet_display.text = "$" + funds.ToString();
    }
}
