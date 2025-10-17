using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CreateSamplePlayer : MonoBehaviour
{

    public DocumentControl documentControl; // Gán trong Inspector
    [Header("Optional UI")]
    public Text createdPlayerIdText; // Hiển thị ID người chơi vừa tạo (nếu muốn)

    public string currentCreatedPlayerId; // Lưu lại player vừa tạo
    public string serverUrl = "http://localhost:3000";
    // ✅ 1️⃣ Tạo người chơi mới với ID duy nhất
    public void CreateUniquePlayer()
    {
        // Tạo ID duy nhất bằng Guid
        string uniqueId = System.Guid.NewGuid().ToString();

        // Thông tin khởi tạo
        int gold = 500;
        int diamond = 5;
        int levelMap = 1;

        // Gọi coroutine tạo player
        StartCoroutine(CreatePlayerCoroutine(uniqueId, gold, diamond, levelMap));
    }

    private IEnumerator CreatePlayerCoroutine(string playerId, int gold, int diamond, int levelMap)
    {
        // Gọi hàm từ DocumentControl
        yield return StartCoroutine(documentControl.CreateOrUpdateDocument(playerId, gold, diamond, levelMap));

        currentCreatedPlayerId = playerId;

        if (createdPlayerIdText != null)
            createdPlayerIdText.text = $"🧍 ID vừa tạo: {playerId}";

        Debug.Log($"✅ Đã tạo người chơi mới với ID: {playerId}");
    }

    // 🗑️ 2️⃣ Xóa người chơi vừa tạo
    public void DeletePlayer()
    {
        if (string.IsNullOrEmpty(currentCreatedPlayerId))
        {
            Debug.LogWarning("⚠️ Chưa có người chơi nào để xóa!");
            return;
        }
        StartCoroutine(DeletePlayerById(currentCreatedPlayerId));
        currentCreatedPlayerId = null;
        createdPlayerIdText.text = "Deleted";
    }

    public IEnumerator DeletePlayerById(string playerId)
    {
        using (UnityWebRequest www = UnityWebRequest.Delete($"{serverUrl}/player/{playerId}"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"✅ Đã xóa người chơi: {playerId}");
            }
            else
            {
                string errorMsg = www.error ?? "Không có thông tin lỗi";
                string responseText = www.downloadHandler != null ? www.downloadHandler.text : "Không có phản hồi từ server";
                Debug.LogError($"❌ Lỗi khi xóa người chơi: {errorMsg} | {responseText}");
            }
        }
    }

}
