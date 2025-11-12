using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChilSponeButton : MonoBehaviour

{
    public string price;
    public Vector2 offset = new Vector2(0, 50); // panel hiện trên nút bao nhiêu units

    private Button btn;
    private ParentManageSpon parentManager;

    private void Awake()
    {
        btn = GetComponent<Button>();
        parentManager = GetComponentInParent<ParentManageSpon>();
        btn.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        RectTransform btnRT = GetComponent<RectTransform>();
        parentManager.SpawnPanelAboveButton(btnRT, price, offset);
    }
}


