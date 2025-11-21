using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallSpinPane : MonoBehaviour
{
    public GameObject spinPane;
    public int gemsRequired = 5;
    private void Start()
    {
        if (spinPane != null)
        {
            spinPane.SetActive(false);
        }
    }
    private void Open()
    {
        if (spinPane != null)
        {
            spinPane.SetActive(true);
        }
    }
    public void CloseSpinPane()
    {
        if (spinPane != null)
        {
            spinPane.SetActive(false);
        }
    }
    public void OpenSkinPanelByGems()
    {
        if (gemsRequired <= DataManager.currentData.Diamond && DataManager.currentData.ListSkinOwned.Count<25)
        {
            Open();
            DataManager.currentData.Diamond -= gemsRequired;
            DataManager.SaveAll();
        }
    }
    public void OpenSkinByAds()
    {
        if (DataManager.currentData.ListSkinOwned.Count < 25)
        {
            AdsManager.Instance.ShowRewarded(Open);
        }
    }
}
