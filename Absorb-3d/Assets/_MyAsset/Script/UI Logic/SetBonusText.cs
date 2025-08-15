using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SetBonusText : MonoBehaviour
{
    public TextMeshPro floatingTextPrefab;
    Vector3 spawnPosition;
    private Rigidbody ground;
    // Start is called before the first frame update
    private void Start()
    {
        ground = GameObject.Find("Ground").GetComponent<Rigidbody>();
    }
    private void Update()
    {
        spawnPosition = ground.position;
    }

    // Update is called once per frame
    public void callText(string text)
    {
        
        TextMeshPro newText = Instantiate(floatingTextPrefab, spawnPosition + new Vector3(Random.Range(0, 10f), Random.Range(0, 1.5f), 0), Quaternion.identity);
        newText.text = text;
        Debug.Log("goc cúa text: " + newText.transform.rotation);
        if (text == "Level Up")
        {
            newText.fontSize *= 1.5f;
            newText.transform.position += new Vector3(0, 2, 0);
            newText.color = Color.yellow; // Change color for level up text
        }
        else
        {
            // Random màu RGB trong khoảng 0–1
            newText.color = RandomBrightColor();
        }
    }
    Color RandomBrightColor()
    {
        return Color.HSVToRGB(Random.value, 1f, 1f);
    }
    //settext lỗi vì nó được gọi trước khi tạo ra prefab
}
