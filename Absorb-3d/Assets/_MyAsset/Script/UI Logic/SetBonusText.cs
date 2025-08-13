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
        Instantiate(floatingTextPrefab, spawnPosition, Quaternion.identity)
    .GetComponent<FloatingText>()
    .SetText(text);
    }
    //settext lỗi vì nó được gọi trước khi tạo ra prefab
}
