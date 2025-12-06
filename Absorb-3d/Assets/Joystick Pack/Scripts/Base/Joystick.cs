using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float Horizontal => (snapX ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x);
    public float Vertical => (snapY ? SnapFloat(input.y, AxisOptions.Vertical) : input.y);
    public Vector2 Direction => new Vector2(Horizontal, Vertical);

    [SerializeField] private float handleRange = 1f;
    [SerializeField] private float deadZone = 0f;
    [SerializeField] private AxisOptions axisOptions = AxisOptions.Both;
    [SerializeField] private bool snapX = false;
    [SerializeField] private bool snapY = false;

    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;

    private Vector2 input = Vector2.zero;
    private RectTransform baseRect;
    private Canvas canvas;
    private Camera cam;

    protected virtual void Start()
    {
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        handle.anchoredPosition = Vector2.zero;
    }

    protected virtual void OnEnable()
    {
        ForceStop(); // mỗi lần enable đều đứng yên
    }

    protected virtual void OnDisable()
    {
        ForceStop(); // disable → reset
    }

    public void ForceStop()
    {
        input = Vector2.zero;

        if (handle != null)
            handle.anchoredPosition = Vector2.zero;

        if (background != null)
            background.gameObject.SetActive(false);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        cam = canvas.renderMode == RenderMode.ScreenSpaceCamera ? canvas.worldCamera : null;

        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2f;

        input = (eventData.position - position) / (radius * canvas.scaleFactor);

        HandleInput(input.magnitude, input.normalized, radius);
        handle.anchoredPosition = input * radius * handleRange;
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalized, Vector2 radius)
    {
        if (magnitude > deadZone)
        {
            input = magnitude > 1 ? normalized : input;
        }
        else input = Vector2.zero;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        ForceStop();
    }

    private float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
            return 0;
        return value > 0 ? 1 : -1;
    }
}

public enum AxisOptions { Both, Horizontal, Vertical }
