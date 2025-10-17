﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpdate : MonoBehaviour
{
    public Text[] levelTexts;
    public PlayerInformationManager playerInfoManager;
    public int currentLevel = 0;

    private void Start()
    {
        if (playerInfoManager == null)
        {
            playerInfoManager = FindObjectOfType<PlayerInformationManager>();
            Debug.Log(playerInfoManager != null ? "✅ playerInfoManager tìm thấy" : "❌ Không tìm thấy playerInfoManager");
        }

        // Khi sẵn sàng, load level hiện tại lên UI
        StartCoroutine(UpdateLevelUIWhenReady());
    }

    private void Update()
    {
        if (playerInfoManager == null)
            playerInfoManager = FindObjectOfType<PlayerInformationManager>();
    }

    // 🧩 Hàm này để load dữ liệu level từ server và hiển thị
    private IEnumerator UpdateLevelUIWhenReady()
    {
        while (playerInfoManager == null || string.IsNullOrEmpty(playerInfoManager.currentPlayerId))
            yield return null;

        yield return StartCoroutine(playerInfoManager.documentControl.GetDocumentById(
            playerInfoManager.currentPlayerId,
            playerData =>
            {
                if (playerData != null)
                {
                    currentLevel = playerData.levelMap;
                    UpdateOnScreen(currentLevel);
                }
                else
                    Debug.LogWarning("⚠️ PlayerData null, không thể cập nhật level!");
            }
        ));
    }

    // 🆙 Hàm được gọi khi bấm nút “+1 Level”
    public void OnAddLevelButton()
    {
        StartCoroutine(AddOneLevel());
    }

    private IEnumerator AddOneLevel()
    {
        // Đảm bảo có player ID
        while (playerInfoManager == null || string.IsNullOrEmpty(playerInfoManager.currentPlayerId))
            yield return null;

        string playerId = playerInfoManager.currentPlayerId;

        // Gửi yêu cầu PUT lên server để tăng level và vàng
        using (UnityEngine.Networking.UnityWebRequest www =
               new UnityEngine.Networking.UnityWebRequest($"{playerInfoManager.documentControl.serverUrl}/player/{playerId}", "PUT"))
        {
            www.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes("{}"));
            www.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ Đã gửi yêu cầu tăng level + vàng thành công!");
                // Sau khi server xử lý xong, load lại level từ server
                StartCoroutine(UpdateLevelUIWhenReady());
            }
            else
            {
                Debug.LogError($"❌ Lỗi khi gửi yêu cầu: {www.error} | {www.downloadHandler.text}");
            }
        }
    }

    // 🧩 Hàm PUT dữ liệu mới lên server
    private IEnumerator UpdatePlayerOnServer(string playerId, string jsonData)
    {
        using (UnityEngine.Networking.UnityWebRequest www =
               new UnityEngine.Networking.UnityWebRequest($"{playerInfoManager.documentControl.serverUrl}/player/{playerId}", "PUT"))
        {
            www.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            www.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ Cập nhật thành công: +1000 vàng và +1 level");
            }
            else
            {
                Debug.LogError($"❌ Lỗi khi cập nhật: {www.error} | {www.downloadHandler.text}");
            }
        }
    }

    // 🔢 Cập nhật hiển thị trên UI
    public void UpdateOnScreen(int level)
    {
        ReText();
        UpdateTextLevel(level);
    }

    public void UpdateTextLevel(int level)
    {
        if (level <= 0) return;

        levelTexts[level % 10].text = level.ToString();
        if (level % 10 == 0) return;
        UpdateTextLevel(level - 1);
    }

    public void ReText()
    {
        foreach (var text in levelTexts)
            text.text = "";
    }
}
