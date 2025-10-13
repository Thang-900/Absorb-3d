using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainMoneyShow : MonoBehaviour
{
    private int mainMoney;
    public Text mainMoneyText;
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.currentData == null)
        {
            mainMoney = 0;
        }
        else
        {
            mainMoney = DataManager.currentData.Gold;
        }
        mainMoneyText.text = mainMoney.ToString();
    }
}
