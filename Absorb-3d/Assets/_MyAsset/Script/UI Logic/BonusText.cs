using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveUpSpeed = 1f;
    public float lifeTime = 1.5f;
    private TextMeshPro textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        Destroy(gameObject, lifeTime);
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);
    }


    void Update()
    {
        transform.Translate((new Vector3(0, 0, 45)).normalized * moveUpSpeed * Time.deltaTime);
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
