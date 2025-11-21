using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffPanel : MonoBehaviour
{
    public GameObject[] panelsToClose;
    public void ClosePanel()
    {
        foreach(GameObject panel in panelsToClose)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }
    }   
}
