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
    }


    public SaveManager saveManager;
    public PlayerData currentData;
    private void Start()
    {
        currentData = saveManager.Load();
        Debug.Log("🎮 Dữ liệu hiện tại: " + JsonUtility.ToJson(currentData, true));
    }
    private void OnApplicationQuit()
    {
        saveManager.Save(currentData);
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause) saveManager.Save(currentData);
    }



}
