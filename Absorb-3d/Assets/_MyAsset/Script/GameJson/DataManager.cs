using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentData = saveManager.Load();
        Debug.Log("🎮 Dữ liệu hiện tại: " + JsonUtility.ToJson(currentData, true));
    }

    public SaveManager saveManager;
    public static PlayerData currentData;
    public void SaveGold()
    {
        currentData.Gold += GoldBonus.goldBonus;
        saveManager.Save(currentData);
        Debug.Log("💰 Đã thêm vàng: " + GoldBonus.goldBonus);
        GoldBonus.goldBonus = 0;
    }
    public void SaveDiamond(int newDiamond)
    {
        currentData.Diamond = newDiamond;
        saveManager.Save(currentData);
    }
    public void SaveSkinId(int newSkinId)
    {
        currentData.SkinId = newSkinId;
        saveManager.Save(currentData);
    }
    public void SaveListSkinOwned(string newListSkinOwned)
    {
        currentData.ListSkinOwned.Add(newListSkinOwned);
        saveManager.Save(currentData);
    }
    public void SaveMapLevel()
    {
        currentData.MapLevel += 1;
        saveManager.Save(currentData);
    }
    public void SaveTalentTreeLevel(int newTalentTreeLevel)
    {
        currentData.TalentTreeLevel = newTalentTreeLevel;
        saveManager.Save(currentData);
    }
    public void SaveIncomeLevel(int newIncomeLevel)
    {
        currentData.TabIncomeLevel = newIncomeLevel;
        saveManager.Save(currentData);
    }
    public void SaveVacuumLevel(int newVacuumLevel)
    {
        currentData.TabVacuumLevel = newVacuumLevel;
        saveManager.Save(currentData);
    }
    public void SaveTabSpeedLevel(int newTabSpeedLevel)
    {
        currentData.TabSpeedLevel = newTabSpeedLevel;
        saveManager.Save(currentData);
    }
    public void SaveScaleRateOnStart(float newScaleRateOnStart)
    {
        currentData.ScaleRateOnStart = newScaleRateOnStart;
        saveManager.Save(currentData);
    }
    public void SaveVaccumRateOnStart(float newVaccumRateOnStart)
    {
        currentData.VaccumRateOnStart = newVaccumRateOnStart;
        saveManager.Save(currentData);
    }
    public void SaveIncomeRateOnStart(float newIncomeRateOnStart)
    {
        currentData.IncomeRateOnStart = newIncomeRateOnStart;
        saveManager.Save(currentData);
    }
    public void SaveSpeedRateOnStart(float newSpeedRateOnStart)
    {
        currentData.SpeedRateOnStart = newSpeedRateOnStart;
        saveManager.Save(currentData);
    }
    private void Start()
    {
        
    }
    private void OnApplicationQuit()
    {
        saveManager.Save(currentData);
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause) saveManager.Save(currentData);
    }
    public void ResetData()
    {
        currentData = saveManager.ResetDatamanager();
        saveManager.Save(currentData);
    }
}
