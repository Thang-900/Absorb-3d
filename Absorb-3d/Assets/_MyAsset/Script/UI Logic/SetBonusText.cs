using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SetBonusText : MonoBehaviour
{
    public TextMeshPro floatingTextPrefab;
    public Transform spawn;
    public string text;
    Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = spawn.position;
    }

    // Update is called once per frame
    public void callText()
    {
        Instantiate(floatingTextPrefab, spawnPosition, Quaternion.Euler(45,0,0))
    .GetComponent<FloatingText>()
    .SetText(text);
    }
}
