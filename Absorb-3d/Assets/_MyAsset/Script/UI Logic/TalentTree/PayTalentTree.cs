using System.Collections;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class PayTalentTree : MonoBehaviour
{
    private SponeTab sponeTab;
    private MainMoneyShow mainMoneyShow;
    private UpdateScale updateScale;

    public bool hasBuy = false; // control the prefab to spawn linked to clicktospone
    public bool notUpscale = false; // control the button can be up scale linked to clicktospone
    public GameObject buttonJustClick; // linked to button have clicktospone
    public int cost = 0; // cost được truyền từ SponeTab khi spawn

    private void OnEnable()
    {
        // Gán refs không giả định buttonJustClick đã có ngay lập tức
        sponeTab = GameObject.FindAnyObjectByType<SponeTab>();
        mainMoneyShow = GameObject.FindAnyObjectByType<MainMoneyShow>();
        updateScale = GameObject.FindAnyObjectByType<UpdateScale>();
        
        // Nếu buttonJustClick được gán ngay khi instantiate, vẫn an toàn; nếu gán sau,
        // chờ một frame để lấy thông tin từ button
        StartCoroutine(LateInitOneFrame());
    }

    private IEnumerator LateInitOneFrame()
    {
        yield return null; // chờ một frame, đảm bảo caller đã gán buttonJustClick
        if (buttonJustClick != null)
        {
            var button = buttonJustClick.GetComponent<ClickToSponeTab>();
            if (button == null)
            {
                Debug.Log("Khong thay ClickToSponeTab tren buttonJustClick");
            }
            
            else
            {
                hasBuy = button.hasBuy;
                notUpscale = button.notUpscale;
            }
        }
    }

    public void PayForTalentTree()
    {

        // Đánh dấu nút đã mua
        var click = buttonJustClick.GetComponent<ClickToSponeTab>();
        if (sponeTab == null)
            sponeTab = GameObject.FindAnyObjectByType<SponeTab>();

        if (DataManager.currentData == null)
        {
            Debug.LogWarning("DataManager.currentData null - không thể thanh toán");
            sponeTab?.ClearAllSpawnedPrefabs();
            return;
        }

        int playerGold = DataManager.currentData.Gold;
        int payCost = cost; // cost đã được gán
        var addIndex = buttonJustClick.GetComponent<AddIndex>();
        if (playerGold >= payCost)
        {
            if (addIndex == null)
            {
                Debug.Log("Khong thay AddIndex tren buttonJustClick");
            }
            else
            {
                addIndex.AddIndexOfButton();
            }
            if (buttonJustClick != null)
            {
                var c = buttonJustClick.GetComponent<ClickToSponeTab>();
                if (c != null)
                    c.hasBuy = true;
            }

            SubMoney(payCost);
            UpScale();
            ShowMoney();

            // 🔥 LƯU TALENT ĐÃ MUA
            SaveTalent(click.talentID);

            // Cập nhật state của button và UI
            sponeTab?.EditButton();

            // Xóa popup sau delay ngắn để animation/cập nhật UI kịp hiển thị
            if (sponeTab != null)
                StartCoroutine(ClearPopupAfterDelay(0.12f));
        }
        else
        {
            sponeTab?.ClearAllSpawnedPrefabs();
            Debug.LogWarning("❌ Không đủ vàng để nâng cấp Talent Tree!");
        }
    }

    private IEnumerator ClearPopupAfterDelay(float t)
    {
        yield return new WaitForSeconds(t);
        sponeTab?.ClearAllSpawnedPrefabs();
    }

    public void SubMoney(int cost)
    {
        DataManager.currentData.Gold -= cost;

        if (!notUpscale)
        {
            DataManager.currentData.TalentTreeLevel++;
        }

        DataManager.instance.saveManager.Save(DataManager.currentData);
    }

    public void ShowMoney()
    {
        if (mainMoneyShow != null)
        {
            mainMoneyShow.ShowInJson();
            Debug.Log("💰 Cập nhật hiển thị tiền sau khi thanh toán Talent Tree.");
        }
        Debug.Log("✅ Thanh toán thành công! Talent Tree đã được nâng cấp.");
    }

    public void UpScale()
    {
        if (updateScale != null)
        {
            // updateScale sẽ tự kiểm tra notUpscale bên trong
            updateScale.UpTalentTree(DataManager.currentData.TalentTreeLevel-1);
            Debug.Log("📈 Cập nhật kích thước Talent Tree sau khi nâng cấp.");
        }
    }
    private void SaveTalent(string id)
    {
        if (!DataManager.currentData.talentBought.Contains(id))
            DataManager.currentData.talentBought.Add(id);

        DataManager.instance.saveManager.Save(DataManager.currentData);
    }

}
