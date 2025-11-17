using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSponeTab : MonoBehaviour
{
    public string talentID;

    public int LevelOfButton;
    public bool hasBuy = false; // main source
    public bool notUpscale = false; // main source (không thay đổi trong code nếu designer set)
    private SetUiOfTalen setUiOfTalen;
    public SponeTab sponeTab;

    private Transform buttonTransform;
    private string nameOfButton;

    private void OnEnable()
    {
        buttonTransform = this.transform;
        nameOfButton = this.gameObject.name;

        // Fix: gán khi bằng null hoặc không gán trước đó
        if (setUiOfTalen == null)
        {
            setUiOfTalen = GetComponent<SetUiOfTalen>();
        }
        // Khởi tạo sponeTab
        if (sponeTab == null)
            sponeTab = FindAnyObjectByType<SponeTab>();

        // 🔥 Load hasBuy từ DataManager
        if (DataManager.currentData != null)
        {
            hasBuy = DataManager.currentData.talentBought.Contains(talentID);
        }
    }

    // Gọi từ UI Button OnClick
    public void OnClickSponeTab()
    {
        if (sponeTab != null)
        {
            // Ngăn Clear ngay lập tức bởi update click-outside
            sponeTab.PreventImmediateClear(0.2f);

            // Clear hiện có rồi show dialog/update tab
            sponeTab.ClearAllSpawnedPrefabs();
            sponeTab.ShowUpdateTalentTree(nameOfButton, LevelOfButton, gameObject, hasBuy, notUpscale);
        }
        else
        {
            Debug.LogWarning("❌ SponeTab chưa được gán!");
        }
    }
}
