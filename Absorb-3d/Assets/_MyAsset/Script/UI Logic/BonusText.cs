using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveUpSpeed = 1f;
    public float lifeTime = 1.5f;
    private TextMeshPro textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        Destroy(gameObject, lifeTime);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * moveUpSpeed * Time.deltaTime);
    }

    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
