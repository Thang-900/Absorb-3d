using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string PlayerId;
    public int Gold;
    public int Diamond;
    public List<string> ListSkinOwned;
    public string SelectedSkin;

    public int MapLevel;
    public int TalentTreeLevel;
    public int TabIncomeLevel;
    public int TabVacuumLevel;
    public int TabSpeedLevel;

    public float ScaleRateOnStart;
    public float VacuumRateOnStart;
    public float IncomeRateOnStart;
    public float SpeedRateOnStart;

    public List<string> talentBought = new List<string>();

}
