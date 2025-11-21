using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfiniteHorizontalSpinner : MonoBehaviour
{
    [Header("Scroll Settings")]
    public ScrollRect scrollRect;
    public RectTransform content;
    public float itemWidth = 300f;
    public float spinSpeed = 1500f;
    public float deceleration = 2000f;

    [Header("Pointer")]
    public RectTransform pointer;

    [Header("Result Panel / Prefabs")]
    public GameObject resultPanel;
    public Transform spawnParent;
    public List<GameObject> rewardPrefabs;

    private List<RectTransform> items = new List<RectTransform>();
    private bool isSpinning = false;
    private float currentSpeed;

    void Start()
    {
        // lấy toàn bộ item trong content
        foreach (Transform child in content)
        {
            items.Add(child.GetComponent<RectTransform>());
        }

        scrollRect.horizontal = true;
    }

    void Update()
    {
        if (isSpinning)
        {
            SpinMovement();
        }

        HandleInfiniteLoop();
    }

    // -------------------------------------------------------
    // QUAY
    // -------------------------------------------------------
    public void StartSpin()
    {
        currentSpeed = spinSpeed;
        isSpinning = true;
    }

    void SpinMovement()
    {
        // ScrollRect control repos
        content.anchoredPosition -= new Vector2(currentSpeed * Time.deltaTime, 0);

        // giảm tốc dần
        currentSpeed -= deceleration * Time.deltaTime;

        if (currentSpeed <= 0)
        {
            currentSpeed = 0;
            isSpinning = false;

            Invoke(nameof(SelectReward), 0.2f);
        }
    }

    // -------------------------------------------------------
    // LOOP VÔ HẠN
    // -------------------------------------------------------
    void HandleInfiniteLoop()
    {
        RectTransform first = items[0];
        RectTransform last = items[items.Count - 1];

        float leftEdge = content.anchoredPosition.x;

        // Nếu item đầu trôi qua bên trái → chuyển xuống cuối
        if (first.anchoredPosition.x + leftEdge < -itemWidth)
        {
            items.RemoveAt(0);
            items.Add(first);

            first.SetAsLastSibling();
            content.anchoredPosition += new Vector2(itemWidth, 0);
        }

        // Nếu item cuối trôi ra bên phải → chuyển lên đầu
        if (last.anchoredPosition.x + leftEdge > itemWidth * items.Count)
        {
            items.RemoveAt(items.Count - 1);
            items.Insert(0, last);

            last.SetAsFirstSibling();
            content.anchoredPosition -= new Vector2(itemWidth, 0);
        }
    }

    // -------------------------------------------------------
    // CHỌN KẾT QUẢ
    // -------------------------------------------------------
    void SelectReward()
    {
        RectTransform bestItem = null;
        float smallest = float.MaxValue;

        Vector3 pointerPos = pointer.position;

        foreach (RectTransform item in items)
        {
            float dist = Mathf.Abs(item.position.x - pointerPos.x);

            if (dist < smallest)
            {
                smallest = dist;
                bestItem = item;
            }
        }

        if (bestItem != null)
        {
            string rewardName = bestItem.name.Replace("(Clone)", "");

            Debug.Log("Reward: " + rewardName);
            ShowResult(rewardName);
        }
    }

    // -------------------------------------------------------
    // HIỆN PANEL VÀ SPAWN
    // -------------------------------------------------------
    void ShowResult(string rewardName)
    {
        resultPanel.SetActive(true);

        foreach (GameObject prefab in rewardPrefabs)
        {
            if (prefab.name == rewardName)
            {
                Instantiate(prefab, spawnParent);
                break;
            }
        }
    }
}
