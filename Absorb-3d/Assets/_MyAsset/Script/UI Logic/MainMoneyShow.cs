using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainMoneyShow : MonoBehaviour
{
    private int mainMoney;
    public Text mainMoneyText;
    public bool isShowInJson = true;
    public bool isShowInMongo = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isShowInJson)
        {
            isShowInMongo = false;
        }
        else
        {
            isShowInMongo = true;
        }
        if (isShowInJson)
        {
            ShowInJson();
        }
        if (isShowInMongo)
        {
            ShowInMongo();
        }
    }

    void ShowInJson()
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
    void ShowInMongo()
    {

    }
}
