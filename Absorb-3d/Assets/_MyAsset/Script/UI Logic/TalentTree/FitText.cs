using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Thêm namespace TMP

public class FitText : MonoBehaviour
{
    // Gán trực tiếp trong prefab Inspector
    public TMP_Text cost;
    public TMP_Text title;
    public TMP_Text describe;

    // Nếu muốn tìm tự động các con theo tên (không bắt buộc)
    

    /// <summary>
    /// Gán thông tin cho prefab
    /// </summary>
    /// <param name="nameText">Tên button hoặc talent</param>
    /// <param name="costValue">Giá trị cost</param>
    public void Setup(string nameText, int costValue)
    {
        if (cost != null)
            cost.text = costValue.ToString();

        if (title != null)
            title.text = nameText;

        if (describe != null)
            describe.text = $"Increasing {nameText}\r\nmultiplier by 0.1";
    }
}
