using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public List<GameObject> listSkins = new List<GameObject>();
    public List<string> listSkinID = new List<string>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var skin in listSkins)
        {
            listSkinID.Add(skin.name);
        }
    }
    private void StatusOne()
    {
        foreach(var skin in listSkins)
        {
            skin.transform.Find("tick").gameObject.SetActive(false);
        }
    }
}
