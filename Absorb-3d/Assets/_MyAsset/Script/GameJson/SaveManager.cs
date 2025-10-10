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

    // Ghi dữ liệu ra file JSON
    public void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("💾 Dữ liệu đã được lưu tại: " + path);
    }

    // Đọc dữ liệu từ file JSON
    public PlayerData Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("📂 Dữ liệu đã được tải.");
            return data;
        }
        else
        {
            Debug.Log("⚠️ Không tìm thấy file lưu, tạo mới dữ liệu mặc định.");
            return new PlayerData
            {
                PlayerId = "0001",
                Gold = 0,
                Diamond = 0,
                SkinId = 0,
                ListSkinOwned = new List<int>(),

                TalentTreeLevel = 0,
                TabIncomeLevel = 0,
                TabVacuumLevel = 0,
                TabSpeedLevel = 0,

                MapLevel = 1,
                ScaleRateOnStart = 1,
                VaccumRateOnStart = 1,
                IncomeRateOnStart = 1,
                SpeedRateOnStart = 1
            };
        }
    }
}
