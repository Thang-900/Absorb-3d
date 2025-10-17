using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldGive : MonoBehaviour
{
    public PlayerInformationManager playerManager; // Gán PlayerInformationManager
    public DocumentControl documentControl;        // Gán DocumentControl
    public Text transactionLog;                     // Hiển thị kết quả giao dịch (optional)
    public CreateSamplePlayer createSamplePlayer; // Gán CreateSamplePlayer để lấy ID người chơi vừa tạo

    // Chuyển tiền 100 vàng từ player 1 -> player 2
    public void TransferGoldBetweenSamples(int amount)
    {
        string playerAId = PlayerInformationManager.Instance.currentPlayerId; // Lấy player A
        string playerBId = createSamplePlayer.currentCreatedPlayerId; // Lấy player B (thay đổi theo slot khác nếu cần)
        if (string.IsNullOrEmpty(playerAId) || string.IsNullOrEmpty(playerBId))
        {
            Debug.LogWarning("⚠️ Chưa có player A hoặc player B để giao dịch!");
            return;
        }

        if (amount <= 0)
        {
            Debug.LogWarning("⚠️ Số tiền chuyển phải lớn hơn 0!");
            return;
        }

        // Lấy dữ liệu cả 2 player trước
        StartCoroutine(documentControl.GetDocumentById(playerAId, playerAData =>
        {
            if (playerAData == null)
            {
                Debug.LogError("❌ Không lấy được dữ liệu player A");
                transactionLog.text = "Failed";
                return;
            }

            StartCoroutine(documentControl.GetDocumentById(playerBId, playerBData =>
            {
                if (playerBData == null)
                {
                    Debug.LogError("❌ Không lấy được dữ liệu player B");
                    transactionLog.text = "Failed";

                    return;
                }

                // Kiểm tra điều kiện không âm
                if (playerAData.gold - amount < 0)
                {
                    Debug.LogWarning("⚠️ Player A không đủ vàng để chuyển!");
                    transactionLog.text = "Failed";

                    return;
                }

                if (playerBData.gold + amount < 0)
                {
                    Debug.LogWarning("⚠️ Player B sẽ bị âm vàng!");
                    transactionLog.text = "Failed";

                    return;
                }

                // Thực hiện chuyển
                StartCoroutine(documentControl.TransferGold(playerAId, playerBId, amount));
                Debug.Log($"💸 Chuyển {amount} vàng từ A → B thành công");
                transactionLog.text = "+100 gold";

            }));
        }));
    }

}
