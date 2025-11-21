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
        SetAllIndexShow();
        if (DataManager.currentData.VacuumRateOnStart<1)
        {
            DataManager.currentData.VacuumRateOnStart = 1;
        }
        if(DataManager.currentData.SpeedRateOnStart<1)
        {
            DataManager.currentData.SpeedRateOnStart = 1;
        }
        if (DataManager.currentData.IncomeRateOnStart < 1)
        {
            DataManager.currentData.IncomeRateOnStart = 1;
        }
    }
    public void SetAllIndexShow()
    {
        SetVacuumText();
        SetSpeedText();
        SetIncomeText();
        SetScaleText();
    }
    public void SetVacuumText()
    {
        var value = DataManager.currentData.VacuumRateOnStart;
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
