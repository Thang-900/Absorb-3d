using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SaveManager))]
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public SaveManager saveManager;
    [SerializeField]
    public static PlayerData currentData;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentData = saveManager.Load();
        if (currentData == null)
        {
            Debug.Log("🎮 Dữ liệu khong hiện tại: " + JsonUtility.ToJson(currentData, true));
        }
    }
    public static void SaveAll()
    {
        instance.saveManager.Save(currentData);
    }
    public void SaveGold()
    {
        currentData.Gold += GoldBonus.goldBonus;
        saveManager.Save(currentData);
        Debug.Log("💰 Đã thêm vàng: " + GoldBonus.goldBonus);
        GoldBonus.ResetGoldBonus();
    }
    public void SaveDiamond(int newDiamond)
    {
        currentData.Diamond = newDiamond;
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
    public void Add_SaveScaleRateOnStart()
    {
        currentData.ScaleRateOnStart += 0.1f;
        saveManager.Save(currentData);
    }
    public void Add_SaveVaccumRateOnStart()
    {
        currentData.VacuumRateOnStart += 0.1f;
        saveManager.Save(currentData);
    }
    public void Add_SaveIncomeRateOnStart()
    {
        currentData.IncomeRateOnStart += 0.1f;
        saveManager.Save(currentData);
    }
    public void Add_SaveSpeedRateOnStart()
    {
        currentData.SpeedRateOnStart += 0.1f;
        saveManager.Save(currentData);
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
