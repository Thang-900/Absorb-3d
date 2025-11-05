using UnityEngine;
using UnityEngine.UI;

public class ImagesChangesTransform : MonoBehaviour
{
    public Sprite newSprite_1;
    public Sprite newSprite_2;

    private Image imageComponent;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
            Debug.LogWarning("Image component missing on " + gameObject.name);
    }

    public void ImageForm_1()
    {
        if (imageComponent != null)
            imageComponent.sprite = newSprite_1;
    }

    public void ImageForm_2()
    {
        if (imageComponent != null)
            imageComponent.sprite = newSprite_2;
    }
}
