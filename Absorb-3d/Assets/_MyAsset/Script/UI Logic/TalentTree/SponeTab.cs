using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SponeTab : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject prefab_1;
    public GameObject prefab_2;
    public Transform prefabParent; // gán parent từ Inspector nếu muốn
    public int finalCost; // không static nữa
    [Header("Offset cho từng prefab")]
    public Vector3 prefab1Offset = new Vector3(0f, 50f, 0f);
    public Vector3 prefab2Offset = new Vector3(0f, 50f, 0f);
    public GameObject[] buttons;

    private PlayerData playerData;
    private int costValue = 500;

    // Clear control
    private bool isClearing = false;
    private float ignoreClearUntil = 0f;

    // Lưu tất cả prefab vừa spawn để có thể xóa
    private List<GameObject> spawnedPrefabs = new List<GameObject>();

    private void Start()
    {
        if (DataManager.instance == null)
        {
            Debug.LogError("❌ DataManager instance chưa tồn tại!");
            return;
        }

        if (DataManager.currentData == null)
        {
            Debug.LogWarning("⚠️ currentData null, thử load lại...");
        }

        playerData = DataManager.currentData;
        Debug.Log("✅ PlayerData loaded: " + JsonUtility.ToJson(playerData));
        EditButton();
    }
    private void OnEnable()
    {
        EditButton();
    }
    /// <summary>
    /// Hiển thị prefab dựa trên button với parent tùy chọn
    /// </summary>
    public void ShowUpdateTalentTree(string nameOfButton, int LevelOfButton, GameObject button, bool hasBuy, bool notUpscale)
    {
        if (playerData == null)
            playerData = DataManager.currentData;

        int levelOfTalentTree = playerData != null ? playerData.TalentTreeLevel : 0;
        finalCost = costValue * LevelOfButton;

        var setUiOfTalent = button.GetComponent<SetUiOfTalen>();
        GameObject prefabToSpawn;
        Vector3 offset;

        if (levelOfTalentTree >= LevelOfButton && hasBuy == false)
        {
            prefabToSpawn = prefab_1;
            offset = prefab1Offset;
        }
        else
        {
            prefabToSpawn = prefab_2;
            offset = prefab2Offset;
        }

        // CHUYỂN ĐỔI TỌA ĐỘ CHUẨN
        RectTransform buttonRect = button.GetComponent<RectTransform>();
        RectTransform parentRect = prefabParent as RectTransform;

        // Lấy Canvas của prefabParent để biết camera (nếu có)
        Canvas parentCanvas = prefabParent.GetComponentInParent<Canvas>();
        Camera uiCamera = null;
        if (parentCanvas != null && parentCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
            uiCamera = parentCanvas.worldCamera;

        // Lấy screen position của button (dùng camera nếu cần)
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, buttonRect.position);

        // Chuyển screen position về local position của parent
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenPos, uiCamera, out localPos);

        // Instantiate prefab
        GameObject uiItem = Instantiate(prefabToSpawn, prefabParent);
        RectTransform itemRect = uiItem.GetComponent<RectTransform>();

        // Set đúng vị trí
        itemRect.localPosition = localPos + (Vector2)offset;

        spawnedPrefabs.Add(uiItem);

        // Gán dữ liệu vào prefab (cost, link button)
        var payTalent = uiItem.GetComponentInChildren<PayTalentTree>();
        if (payTalent != null)
        {
            payTalent.buttonJustClick = button; // link về button
            payTalent.cost = finalCost;         // truyền cost (không dùng static)
        }

        FitText talentItem = uiItem.GetComponent<FitText>();
        if (talentItem != null)
            talentItem.Setup(nameOfButton, finalCost);
    }

    public void ClearAllSpawnedPrefabs()
    {
        foreach (GameObject go in spawnedPrefabs)
        {
            if (go != null)
                Destroy(go);
        }
        spawnedPrefabs.Clear();
    }

    public void EditButton()
    {
        foreach (GameObject button in buttons)
        {
            var clickToSponeTab = button.GetComponent<ClickToSponeTab>();
            if (clickToSponeTab != null && clickToSponeTab.hasBuy)
            {
                var setUiOfTalen = button.GetComponent<SetUiOfTalen>();
                if (setUiOfTalen != null)
                {
                    setUiOfTalen.SetUI();
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time < ignoreClearUntil)
                return; // đang nằm trong khoảng chặn clear do click button vừa xảy ra

            if (!ClickedOnAnyTalentButton())  // ❗ không click vào button có ClickToSponeTab
            {
                StartCoroutine(DelayedClear());
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            if (Time.time < ignoreClearUntil)
                return;

            if (!ClickedOnAnyTalentButton())
            {
                StartCoroutine(DelayedClear());
            }
        }
    }

    private bool ClickedOnAnyTalentButton()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var hit in results)
        {
            // Trúng button spawn
            if (hit.gameObject.GetComponent<ClickToSponeTab>() != null)
                return true;

            // Trúng prefab UI
            if (hit.gameObject.GetComponent<PayTalentTree>() != null ||
                hit.gameObject.GetComponentInParent<PayTalentTree>() != null)
                return true;

            // Trúng bất kỳ UI nào thuộc Canvas chứa prefab
            if (hit.gameObject.transform.IsChildOf(prefabParent))
                return true;
        }

        return false;
    }


    private IEnumerator DelayedClear()
    {
        if (isClearing)
            yield break;

        isClearing = true;
        yield return new WaitForSeconds(0.12f);

        ClearAllSpawnedPrefabs();

        isClearing = false;
    }

    /// <summary>
    /// Gọi để tạm ngăn clear trong một khoảng thời gian (ví dụ khi click button)
    /// </summary>
    public void PreventImmediateClear(float seconds)
    {
        ignoreClearUntil = Time.time + seconds;
    }
}
