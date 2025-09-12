using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelTransition : MonoBehaviour
{
    public GameObject[] Panels;
    // Start is called before the first frame update

    // Update is called once per frame
    private void PanelTranslation(int number)
    {
        foreach (var panel in Panels)
        {
            panel.SetActive(false);
        }
        Panels[number - 1].SetActive(true);
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
}
