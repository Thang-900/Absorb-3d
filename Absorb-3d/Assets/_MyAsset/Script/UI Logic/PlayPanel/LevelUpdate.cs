using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpdate : MonoBehaviour
{
    // ham giu vi tri cua tung o hien thi level
    public int level = 0;
    public Text[] levelTexts;
    public void UpdateTextLevel(int currentLevel)
    {
        Debug.Log("UpdateTextLevel: " + currentLevel);

        // Ghi text trước
        levelTexts[currentLevel % 10].text = currentLevel.ToString();

        // Nếu chạm mốc (0,10,20,...) return
        if (currentLevel % 10 == 0)
        {
            return;
        }

        // Gọi đệ quy tiếp
        UpdateTextLevel(currentLevel - 1);
    }

    //public void OnLevelChange()
    //{
    //    level++;
    //    SaveLevel();
    //    ReText();
    //    UpdateTextLevel(PlayerPrefs.GetInt("Level", 0));
    //}
    public void ReText()
    {
        Debug.Log("ReText");
        foreach (var text in levelTexts)
        {
            text.text = "";
        }
    }
    private void Start()
    {
        ReText();
        if(DataManager.currentData!=null)
        {
            UpdateTextLevel(DataManager.currentData.MapLevel);
        }
        else
        {
            Debug.Log("khong co DataManager.currentData");
            UpdateTextLevel(1);
        }
    }
   
}