using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldBonus : MonoBehaviour
{
    public static GoldBonus instance;
    public static int goldBonus=0;
    public Text TextGoldBonus;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ShowGoldBonus();
        goldBonus = DataManager.currentData.Gold;
    }
    public static void AddGoldBonus(int goldAdded)
    {
        goldBonus += goldAdded;
        if (instance != null)
        {
            instance.ShowGoldBonus();
        }
    }
    private void ShowGoldBonus()
    {
        TextGoldBonus.text = goldBonus.ToString();
    }
    public static void ResetGoldBonus()
    {
        goldBonus = 0;
    }
}
