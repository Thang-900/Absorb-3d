using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Quản lý tài liệu (document) trong MongoDB thông qua server Node.js.
/// Bao gồm: tạo/cập nhật người chơi, lấy thông tin, thống kê, chuyển vàng.
/// </summary>
public class DocumentControl : MonoBehaviour
{
    [Header("Server Configuration")]
    public string serverUrl = "http://localhost:3000"; // URL server Node.js

    // 🧩 1️⃣ Tạo hoặc cập nhật thông tin người chơi (POST /player)
    public IEnumerator CreateOrUpdateDocument(string playerId, int gold, int diamond, int levelMap)
    {
        PlayerData data = new PlayerData()
        {
            playerId = playerId,
            gold = gold,
            diamond = diamond,
            levelMap = levelMap
        };

        string jsonData = JsonUtility.ToJson(data);
        using (UnityWebRequest www = new UnityWebRequest($"{serverUrl}/player", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log($"✅ Player saved: {www.downloadHandler.text}");
            else
                Debug.LogError($"❌ Error saving player: {www.error} | {www.downloadHandler.text}");
        }
    }
    // 📚 5️⃣ Lấy danh sách tất cả người chơi (GET /players)
    public IEnumerator GetAllDocuments()
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{serverUrl}/players"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"📚 All Players: {www.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"❌ Error fetching all players: {www.error} | {www.downloadHandler.text}");
            }
        }
    }


    // 📥 2️⃣ Lấy thông tin người chơi theo ID (GET /player/:id)
    public IEnumerator GetDocument(string playerId)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{serverUrl}/player/{playerId}"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log($"📄 Player Data: {www.downloadHandler.text}");
            else
                Debug.LogError($"❌ Error fetching player: {www.error} | {www.downloadHandler.text}");
        }
    }

    // 📊 3️⃣ Gọi aggregation pipeline (GET /stats)
    public IEnumerator GetAggregationStats()
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{serverUrl}/stats"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log($"📊 Stats: {www.downloadHandler.text}");
            else
                Debug.LogError($"❌ Error fetching stats: {www.error} | {www.downloadHandler.text}");
        }
    }

    // 💰 4️⃣ Gọi transaction (POST /transferGold)
    public IEnumerator TransferGold(string fromPlayerId, string toPlayerId, int amount)
    {
        TransferData transfer = new TransferData()
        {
            fromPlayerId = fromPlayerId,
            toPlayerId = toPlayerId,
            amount = amount
        };

        string jsonData = JsonUtility.ToJson(transfer);
        using (UnityWebRequest www = new UnityWebRequest($"{serverUrl}/transferGold", "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log($"💸 Transfer success: {www.downloadHandler.text}");
            else
                Debug.LogError($"❌ Transfer failed: {www.error} | {www.downloadHandler.text}");
        }
    }


    public IEnumerator GetDocumentById(string playerId, System.Action<PlayerData> onCompleted)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{serverUrl}/player/{playerId}"))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string json = www.downloadHandler.text;

                // Parse JSON thành object PlayerData
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

                Debug.Log($"📄 Lấy thông tin thành công: {playerData.playerId}, Gold: {playerData.gold}, Level: {playerData.levelMap}");

                // Trả kết quả về cho hàm gọi
                onCompleted?.Invoke(playerData);
            }
            else
            {
                Debug.LogError($"❌ Lỗi khi lấy player: {www.error} | {www.downloadHandler.text}");
                onCompleted?.Invoke(null);
            }
        }
    }
    // 🪙 6️⃣ Cập nhật vàng người chơi (PUT /player/:id)
    public IEnumerator UpdateGold(string playerId, int newGold)
    {
        // Dữ liệu JSON gửi lên server
        var data = new PlayerData()
        {
            playerId = playerId,
            gold = newGold
        };

        string jsonData = JsonUtility.ToJson(data);

        using (UnityWebRequest www = UnityWebRequest.Put($"{serverUrl}/player/{playerId}", jsonData))
        {
            www.method = "PUT"; // Xác định rõ phương thức PUT
            www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"✅ Cập nhật vàng thành công cho {playerId}: {newGold}");
            }
            else
            {
                Debug.LogError($"❌ Lỗi khi cập nhật vàng: {www.error} | {www.downloadHandler.text}");
            }
        }
    }
    // ⚙️ Cấu trúc dữ liệu hỗ trợ JSON
    [System.Serializable]
    public class PlayerData
    {
        public string playerId;
        public int gold;
        public int diamond;
        public int levelMap;
    }

    [System.Serializable]
    public class TransferData
    {
        public string fromPlayerId;
        public string toPlayerId;
        public int amount;
    }

}
