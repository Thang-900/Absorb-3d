using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SetSkinState : MonoBehaviour
{
    public bool isOwned = false;
    public bool isSelected = false;
    public SetSkinState[] setAllSkinStates;
    public GameObject tick;
    public GameObject cover;
    public Button Button;
    private void Start()
    {
        setAllSkinStates = FindObjectsOfType<SetSkinState>();
        tick = transform.Find("Tick").gameObject;
        cover = transform.Find("CoverImage").gameObject;
        Button = GetComponent<Button>();
        Button.enabled = false;
        setSkin(); SetStatus();
    }
    private void OnEnable()
    {
        setAllSkinStates = FindObjectsOfType<SetSkinState>();
        SetStatus();
        setSkin();
    }
    private void SetStatus()
    {
        if (DataManager.currentData.ListSkinOwned.Contains(name))
        {
            isOwned = true;
        }
        else
        {
            isOwned = false;

        }
        if(DataManager.currentData.SelectedSkin.ToString() == name)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }
    }
    public void setSkin()
    {
        if (tick != null && cover != null)
        {
            if (isOwned)
            {
                cover.SetActive(false);
                if (isSelected)
                {
                    tick.SetActive(true);
                }
                else
                {
                    tick.SetActive(false);
                }
                Button.enabled = true;
            }
            else
            {
                cover.SetActive(true);
                tick.SetActive(false);
                Button.enabled = false;
            }
        }
    }
    public void SelectSkin()
    {
        if (isOwned)
        {
            isSelected = true;
            DataManager.currentData.SelectedSkin = name;
            DataManager.SaveAll();
            foreach (SetSkinState skin in setAllSkinStates)
            {
                if (skin != this)
                {
                    skin.UnSelectSkin();
                }
            }
            setSkin();
        }
    }
    public void UnSelectSkin()
    {
        if (isOwned)
        {
            isSelected = false;
            setSkin();
        }
    }
    public void SetOwnSkin()
    {
        Button.enabled = true;
        isOwned = true;
        setSkin();
    }
    public void SetUnOwnSkin()
    {
        Button.enabled = false;
        isOwned = false;
        isSelected = false;
        setSkin();
    }
}
