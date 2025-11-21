using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateGems : MonoBehaviour
{
    public Text gemsText;
    public void SetGemsText()
    {
        if (gemsText != null)
        {
            gemsText.text = DataManager.currentData.Diamond.ToString();
        }
    }
    private void Start()
    {
        SetGemsText();
    }
}
