using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnObjects : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Mảng prefab để spawn
    public float spawnZone = 10f; // Bán kính spawn
    public float gapDistance = 2f; // Khoảng cách tối thiểu giữa các object
    public int maxObjects = 10; // Số lượng object tối đa
    private int countObject = 0; // Đếm số object đã spawn

    private List<Vector3> spawnedPositions = new List<Vector3>(); // Lưu vị trí đã spawn

    void Start()
    {
        int attempts = 0;
        int maxAttempts = maxObjects * 10; // Giới hạn để tránh vòng lặp vô hạn

        while (countObject < maxObjects && attempts < maxAttempts)
        {
            attempts++;

            // Chọn 1 prefab ngẫu nhiên
            int randomIndex = Random.Range(0, objectsToSpawn.Length);
            GameObject objToSpawn = objectsToSpawn[randomIndex];

            // Sinh vị trí ngẫu nhiên trong vùng spawnZone
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnZone;
            randomPosition.y = 0; // Cố định y để trên mặt đất

            // Kiểm tra khoảng cách với các vị trí đã spawn
            bool canTakePosition = true;
            foreach (Vector3 pos in spawnedPositions)
            {
                if (Vector3.Distance(randomPosition, pos) < gapDistance)
                {
                    canTakePosition = false;
                    break;
                }
            }

            // Nếu hợp lệ thì spawn và lưu vị trí
            if (canTakePosition)
            {
                Instantiate(objToSpawn, randomPosition, Quaternion.identity);
                spawnedPositions.Add(randomPosition);
                countObject++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnZone);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gapDistance);
    }
}
