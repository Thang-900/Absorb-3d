using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AddIndex : MonoBehaviour
{
    public bool isVaccum = false;
    public bool isSpeed = false;
    public bool isIncome = false;
    public bool isScale = false;
    public void AddIndexOfButton()
    {
        var indexShow=FindAnyObjectByType<IndexShow>();
        if (isVaccum)
        {
            DataManager.instance.Add_SaveVaccumRateOnStart();
            indexShow.SetVacuumText();
        }
        else if (isSpeed)
        {
            DataManager.instance.Add_SaveSpeedRateOnStart();
            indexShow.SetSpeedText();
        }
        else if (isIncome)
        {
            DataManager.instance.Add_SaveIncomeRateOnStart();
            indexShow.SetIncomeText();
        }
        else if (isScale)
        {
            DataManager.instance.Add_SaveScaleRateOnStart();
            indexShow.SetScaleText();
        }
        else
        {
            Debug.LogWarning("AddIndex: Chưa chọn loại nào!");
        }
    }
}
