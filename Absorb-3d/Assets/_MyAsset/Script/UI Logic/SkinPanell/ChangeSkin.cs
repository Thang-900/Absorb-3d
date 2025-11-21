using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public GameObject[] skins;
    private void OnEnable()
    {
        foreach (GameObject go in skins)
        {
            if(go.name == DataManager.currentData.SelectedSkin)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
}
