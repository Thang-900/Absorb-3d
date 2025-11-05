using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelTransition : MonoBehaviour
{
    public GameObject[] Panels;
    public GameObject[] Buttons;
    // Start is called before the first frame update
    private void Start()
    {
        PanelTranslation(3);
    }
    // Update is called once per frame
    private void PanelTranslation(int number)
    {
        foreach (var panel in Panels)
        {
            panel.SetActive(false);
        }
        foreach(var button in Buttons)
        {
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(148.5103f, 221.1367f); 
        }
        Panels[number - 1].SetActive(true);
        SetButtonsSize(Buttons[number - 1]);
    }
    private void SetButtonsSize(GameObject button)
    {
        if(button!=null)
        {
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(198.74f, 221.1367f);
        }
    }
    public void SetActivePanel_1()
    {
        PanelTranslation(1);
    }
    public void SetActivePanel_2()
    {
        PanelTranslation(2);
    }
    public void SetActivePanel_3()
    {
        PanelTranslation(3);
    }
    public void SetActivePanel_4()
    {
        PanelTranslation(4);
    }
    public void SetActivePanel_5()
    {
        PanelTranslation(5);
    }
    public void SetActivePanel_6()
    {
        PanelTranslation(6);
    }
    public void UnActivePanel_6()
    {
        Panels[5].SetActive(false);
    }
}
