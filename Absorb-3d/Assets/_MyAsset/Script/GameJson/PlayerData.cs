using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public string PlayerId;
    public int Gold;
    public int Diamond;
    public int SkinId;
    public List<int> ListSkinOwned;

    public int MapLevel;
    public int TalentTreeLevel;
    public int TabIncomeLevel;
    public int TabVacuumLevel;
    public int TabSpeedLevel;

    public float ScaleRateOnStart;
    public float VaccumRateOnStart;
    public float IncomeRateOnStart;
    public float SpeedRateOnStart;
}

