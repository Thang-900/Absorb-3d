using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScale : MonoBehaviour
{
    public Image imgs;
    private RectTransform rt;

    public float increasePerLevel = 265f;
    public float scaleSpeed = 300f;

    public RectTransform content;
    public float contentMoveAmount = 200f;
    public float contentMoveSpeed = 500f;

    // UI blocker để ngăn người chơi tương tác
    public GameObject screenBlocker;

    private void OnEnable()
    {
        if (imgs == null)
        {
            Debug.LogWarning("UpdateScale: imgs chưa gán!");
            return;
        }
        screenBlocker.SetActive(false); // Mo Khóa tương tác lúc bắt đầu
        rt = imgs.GetComponent<RectTransform>();
        SetTalentTreeLevelDirect(false);
    }

    public void UpTalentTree(int level)
    {
        if (level == 16) level = 20;

        // 🔒 Khóa tương tác
        LockScreen(true);

        float targetHeight = level * increasePerLevel;
        float targetContentY = -level * contentMoveAmount;

        StartCoroutine(SmoothUpdateAll(targetHeight, targetContentY));
    }

    IEnumerator SmoothUpdateAll(float targetHeight, float targetContentY)
    {
        bool heightDone = false;
        bool contentDone = false;

        // chạy song song 2 coroutine
        StartCoroutine(SmoothIncreaseHeight(targetHeight, () => heightDone = true));
        StartCoroutine(SmoothMoveContent(targetContentY, () => contentDone = true));

        // chờ cả hai cùng xong
        while (!heightDone || !contentDone)
            yield return null;

        // 🔓 Mở lại tương tác
        LockScreen(false);
    }

    IEnumerator SmoothIncreaseHeight(float target, System.Action onDone)
    {
        while (Mathf.Abs(rt.sizeDelta.y - target) > 0.1f)
        {
            float newY = Mathf.MoveTowards(rt.sizeDelta.y, target, scaleSpeed * Time.deltaTime);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, newY);
            yield return null;
        }
        onDone?.Invoke();
    }

    IEnumerator SmoothMoveContent(float targetY, System.Action onDone)
    {
        while (Mathf.Abs(content.anchoredPosition.y - targetY) > 0.1f)
        {
            float newY = Mathf.MoveTowards(content.anchoredPosition.y, targetY, contentMoveSpeed * Time.deltaTime);
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, newY);
            yield return null;
        }
        onDone?.Invoke();
    }

    public void SetTalentTreeLevelDirect(bool notUpscale)
    {
        if (notUpscale)
            return;

        if (DataManager.currentData == null)
        {
            Debug.LogWarning("UpdateScale: Data null");
            return;
        }

        int level = DataManager.currentData.TalentTreeLevel - 1;
        if (level == 16) level = 20;

        float targetHeight = level * increasePerLevel;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, targetHeight);

        float targetContentY = -level * contentMoveAmount;
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, targetContentY);
    }

    // 🔒 / 🔓 toggle tương tác UI
    private void LockScreen(bool state)
    {
        if (screenBlocker != null)
        {
            screenBlocker.SetActive(state);
        }
    }
}
