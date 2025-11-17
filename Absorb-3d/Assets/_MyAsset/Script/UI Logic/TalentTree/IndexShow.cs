using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndexShow : MonoBehaviour
{
    public TextMeshProUGUI vaccumText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI scaleText;
    private void OnEnable()
    {
        SetVacuumText();
        SetSpeedText();
        SetIncomeText();
        SetScaleText();
    }
    public void SetVacuumText()
    {
        var value = DataManager.currentData.VaccumRateOnStart;
        vaccumText.text = "x" + value.ToString();
        Debug.Log("SetVacuumText: " + value);
    }
    public void SetSpeedText()
    {
        var value = DataManager.currentData.SpeedRateOnStart;
        speedText.text = "x" + value.ToString();
        Debug.Log("SetSpeedText: " + value);
    }
    public void SetIncomeText()
    {
        var value = DataManager.currentData.IncomeRateOnStart;
        incomeText.text = "x" + value.ToString();
        Debug.Log("SetIncomeText: " + value);
    }
    public void SetScaleText()
    {
        var value = DataManager.currentData.ScaleRateOnStart;
        scaleText.text = "x" + value.ToString();
        Debug.Log("SetIncomeText: " + value);
    }
}
