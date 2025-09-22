using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScale : MonoBehaviour
{
    public Image imgs;  // Gán Image này từ Inspector (kéo từ Hierarchy vào)

    private RectTransform rt;

    public float upperHight;

    int count = 0;

    void Start()
    {
        // Lấy RectTransform của ảnh khi bắt đầu
        rt = imgs.GetComponent<RectTransform>();
    }

    // Hàm gọi để tăng chiều cao
    public void UpTalentTree()
    {
        float currentHeight = rt.sizeDelta.y;
        if (count<1)
        {
            count++;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, currentHeight + 160);
        }
        else
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, currentHeight + upperHight);
        }
       
    }
}
