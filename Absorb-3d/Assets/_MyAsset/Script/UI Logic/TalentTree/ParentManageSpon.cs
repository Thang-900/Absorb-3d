using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentManageSpon : MonoBehaviour
{
    public GameObject panelPrefab; // Prefab panel UI
    private GameObject currentPanel;

    // Hàm này sẽ được gọi khi một button con được nhấn
    public void SpawnPanelAboveButton(RectTransform buttonRT, string price, Vector2 offset)
    {
        // Xóa panel cũ nếu có
        if (currentPanel != null)
            Destroy(currentPanel);

        // Instantiate panel mới làm con của parent (cha)
        currentPanel = Instantiate(panelPrefab, transform); // 'transform' là cha của các button
        RectTransform panelRT = currentPanel.GetComponent<RectTransform>();

        // Đặt vị trí panel dựa vào vị trí button + offset
        panelRT.anchoredPosition = buttonRT.anchoredPosition + offset;
        panelRT.localScale = Vector3.one;

        // Gán text giá nếu có
        Text txt = currentPanel.GetComponentInChildren<Text>();
        if (txt != null)
            txt.text = price;
    }
}
