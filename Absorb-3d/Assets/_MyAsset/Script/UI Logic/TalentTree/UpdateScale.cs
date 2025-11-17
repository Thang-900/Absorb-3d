using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScale : MonoBehaviour
{
    public Image imgs;
    private RectTransform rt;

    // Các biến tùy chỉnh
    public float increasePerLevel = 265f;
    public float scaleSpeed = 300f;
    public RectTransform content;
    public float contentMoveAmount = 200f;
    public float contentMoveSpeed = 500f;

    private void OnEnable()
    {
        if (imgs == null)
        {
            Debug.LogWarning("UpdateScale: imgs chưa gán!");
            return;
        }

        rt = imgs.GetComponent<RectTransform>();

        // Áp trạng thái hiện tại (false = áp dụng update)
        SetTalentTreeLevelDirect(false);
    }

    // Tăng theo level (level là level tổng – không cộng dồn)
    public void UpTalentTree(int level)
    {
        if (level == 16) level = 20;

        float targetHeight = level * increasePerLevel;
        StartCoroutine(SmoothIncreaseHeight(targetHeight));

        float targetContentY = -level * contentMoveAmount;
        StartCoroutine(SmoothMoveContent(targetContentY));

        Debug.Log("UpTalentTree -> Level: " + level +
                  ", TargetHeight: " + targetHeight +
                  ", ContentY: " + targetContentY);
    }

    IEnumerator SmoothIncreaseHeight(float target)
    {
        while (Mathf.Abs(rt.sizeDelta.y - target) > 0.1f)
        {
            float newY = Mathf.MoveTowards(rt.sizeDelta.y, target, scaleSpeed * Time.deltaTime);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, newY);
            yield return null;
        }
    }

    IEnumerator SmoothMoveContent(float targetY)
    {
        while (Mathf.Abs(content.anchoredPosition.y - targetY) > 0.1f)
        {
            float newY = Mathf.MoveTowards(content.anchoredPosition.y, targetY, contentMoveSpeed * Time.deltaTime);
            content.anchoredPosition = new Vector2(content.anchoredPosition.x, newY);
            yield return null;
        }
    }

    // Set trực tiếp từ level (khi OnEnable hoặc khi cần set theo data)
    // notUpscale == true -> không apply (giữ nguyên)
    public void SetTalentTreeLevelDirect(bool notUpscale)
    {
        if (notUpscale)
            return;

        if (DataManager.currentData == null)
        {
            Debug.LogWarning("UpdateScale: DataManager.currentData null");
            return;
        }

        int level = DataManager.currentData.TalentTreeLevel - 1;
        if (level == 16) level = 20;

        float targetHeight = level * increasePerLevel;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, targetHeight);

        float targetContentY = -level * contentMoveAmount;
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, targetContentY);

        Debug.Log("SetTalentTreeLevelDirect -> Level: " + level +
                  ", Height: " + targetHeight +
                  ", ContentY: " + targetContentY);
    }
}
