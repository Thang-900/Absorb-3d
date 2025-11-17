using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUiOfTalen : MonoBehaviour
{
    public void SetUI()
    {
        // 1. Tìm object con tên "Dark"
        Transform darkChild = transform.Find("Dark");
        if (darkChild != null)
        {
            darkChild.gameObject.SetActive(false);
            //Debug.Log("da chuyen sang mau sang"+name);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy object con tên 'Dark'!");
        }

        // 2. Lấy Image của object hiện đang gắn script
        Image img = GetComponent<Image>();
        if (img != null)
        {
            img.color = new Color32(4, 76, 131, 255); // đổi màu
        }
        else
        {
            Debug.LogWarning("Object không có Image component!");
        }
    }
}
