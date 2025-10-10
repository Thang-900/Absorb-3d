using UnityEngine;

public class GravityZone : MonoBehaviour
{
    [Header("Cấu hình vùng hút")]
    public float pullForce = 30f;      // Lực hút
    public float maxDistance = 10f;    // Bán kính vùng hút

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        // Bỏ qua chính vật chủ (để nó không tự hút chính mình)
        if (rb == null || other.gameObject == transform.parent.gameObject)
            return;

        // Tính hướng từ vật thể bị hút đến tâm vùng hút (vật chủ)
        Vector3 center = transform.parent.position - new Vector3(0,1,0);
        Vector3 direction = center - other.transform.position;
        float distance = direction.magnitude;

        // Giảm lực theo khoảng cách
        float forceMagnitude = pullForce * (1 - (distance / maxDistance));
        forceMagnitude = Mathf.Max(forceMagnitude, 0f);

        // Thêm lực hút về trung tâm
        rb.AddForce(direction.normalized * forceMagnitude, ForceMode.Acceleration);
    }

    // Hiển thị vùng hút trong Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.2f, 0.5f, 1f, 0.3f);
        Gizmos.DrawSphere(transform.position, maxDistance);
    }
}
