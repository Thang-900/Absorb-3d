using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static DocumentControl;

public class PlayerInformationManager : MonoBehaviour
{
    public DocumentControl documentControl;
    public int currentSlot = 1; // 1, 2, hoặc 3
    public string currentPlayerId;

    public Text goldText;
    public Text diamondText;

    public static PlayerInformationManager Instance;
    public DocumentControl.PlayerData currentData;
    public int currentLevel = 1;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại khi đổi scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SetCurrentSlot(1);
    }

    public void SetCurrentSlot(int slot)
    {
        currentSlot = slot;
        string key = $"PlayerId{currentSlot}";

        if (PlayerPrefs.HasKey(key))
        {
            currentPlayerId = PlayerPrefs.GetString(key);
            Debug.Log($"👋 Switched to Player {currentSlot} with ID: {currentPlayerId}");

            // ⬇️ Cập nhật UI bằng dữ liệu trên server
            StartCoroutine(UpdatePlayerUI());
        }
        else
        {
            StartCoroutine(CreateNewPlayer(key));
        }
    }

    private IEnumerator CreateNewPlayer(string key)
    {
        string newPlayerId = System.Guid.NewGuid().ToString();
        int startGold = 100;
        int startDiamond = 10;
        int startLevelMap = 1;

        yield return StartCoroutine(documentControl.CreateOrUpdateDocument(
            newPlayerId, startGold, startDiamond, startLevelMap
        ));

        PlayerPrefs.SetString(key, newPlayerId);
        PlayerPrefs.Save();

        currentPlayerId = newPlayerId;
        Debug.Log($"🆕 Created Player {key} with ID: {newPlayerId}");

        yield return StartCoroutine(UpdatePlayerUI());
    }
    public void GoUi()
    {
        StartCoroutine(UpdatePlayerUI());
    }
    private IEnumerator UpdatePlayerUI()
    {
        yield return StartCoroutine(documentControl.GetDocumentById(currentPlayerId, (playerData) =>
        {
            if (playerData != null)
            {
                goldText.text = playerData.gold.ToString();
                diamondText.text = playerData.diamond.ToString();

                Debug.Log($"📊 Updated UI for Player {currentPlayerId}");
            }
            else
            {
                Debug.LogError("❌ Failed to update UI - player data null");
            }
        }));
    }

    public string GetPlayerId(int slot)
    {
        string key = $"PlayerId{slot}";
        if (PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetString(key);
        else
            return null;
    }
}
