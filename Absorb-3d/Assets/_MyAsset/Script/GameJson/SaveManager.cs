using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class SaveManager : MonoBehaviour
{
    private string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/playerData.json";
    }

    public void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("💾 Dữ liệu đã được lưu tại: " + path);
    }

    public PlayerData Load()
    {
        PlayerData data;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<PlayerData>(json);

            // JSON lỗi → tạo mới
            if (data == null)
            {
                data = NewDefaultData();
                Save(data);   // ✔️ LƯU NGAY
            }
        }
        else
        {
            data = NewDefaultData();
            Save(data);       // ✔️ LƯU NGAY
        }

        // Fix an toàn
        if (data.talentBought == null)
            data.talentBought = new List<string>();

        return data;
    }

    public PlayerData ResetDatamanager()
    {
        PlayerData data = NewDefaultData();
        Save(data);      // ✔️ PHẢI CÓ
        return data;
    }

    private PlayerData NewDefaultData()
    {
        return new PlayerData
        {
            PlayerId = "0001",
            Gold = 0,
            Diamond = 0,
            SkinId = 0,
            ListSkinOwned = new List<string>(),

            TalentTreeLevel = 1,
            TabIncomeLevel = 1,
            TabVacuumLevel = 1,
            TabSpeedLevel = 1,

            MapLevel = 1,
            ScaleRateOnStart = 1,
            VaccumRateOnStart = 1,
            IncomeRateOnStart = 1,
            SpeedRateOnStart = 1,

            talentBought = new List<string>()
        };
    }
}
