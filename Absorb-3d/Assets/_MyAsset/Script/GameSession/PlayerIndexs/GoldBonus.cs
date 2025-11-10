using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GoldBonus : MonoBehaviour
{
    public static GoldBonus instance;
    public static int goldBonus = 0;

    public Text TextGoldBonus;
    public DocumentControl documentControl; // Tham chiếu đến DocumentControl
    public string currentPlayerId;          // ID người chơi hiện tại (nên gán từ DataManager hoặc PlayerInformationManager)

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Lấy ID hiện tại nếu có trong DataManager hoặc PlayerPrefs
        if (string.IsNullOrEmpty(currentPlayerId))
        {
            currentPlayerId = PlayerPrefs.GetString("CurrentPlayerId", "");
        }
        goldBonus = 0;
        ShowGoldBonus();
    }

    public static void AddGoldBonus(int goldAdded)
    {
        goldBonus += goldAdded;
        if (instance != null)
        {
            instance.ShowGoldBonus();
        }
    }

    private void ShowGoldBonus()
    {
        if (TextGoldBonus != null)
        {
            TextGoldBonus.text = goldBonus.ToString();
        }
    }

    public static void ResetGoldBonus()
    {
        goldBonus = 0;
        if (instance != null)
        {
            instance.ShowGoldBonus();
        }
    }
    //// --- Hàm cập nhật vàng lên MongoDB ---
    //public void SaveGoldToMongo()
    //{
    //    if (string.IsNullOrEmpty(currentPlayerId))
    //    {
    //        Debug.LogError("❌ Không có PlayerId để lưu vàng!");
    //        return;
    //    }

    //    StartCoroutine(UpdateGoldCoroutine());
    //}

    //private IEnumerator UpdateGoldCoroutine()
    //{
    //    int totalGold = 0;

    //    // Bước 1: Lấy dữ liệu hiện tại của người chơi
    //    yield return StartCoroutine(documentControl.GetDocumentById(currentPlayerId, (playerData) =>
    //    {
    //        if (playerData != null)
    //        {
    //            totalGold = playerData.gold + goldBonus;
    //        }
    //    }));

    //    // Bước 2: Cập nhật dữ liệu mới lên Mongo
    //    yield return StartCoroutine(documentControl.UpdateGold(currentPlayerId, totalGold));

    //    Debug.Log($"✅ Đã cộng {goldBonus} vàng, tổng mới: {totalGold}");
    //    ResetGoldBonus();
    //}
}
