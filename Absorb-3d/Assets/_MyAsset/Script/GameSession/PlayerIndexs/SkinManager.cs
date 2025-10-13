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

    // Update is called once per frame
    void Update()
    {
        
    }
}
