using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawnObjects : MonoBehaviour
{
    public int currentLevel = 1;  
    public GameObject[] objectsToSpawn; // Mảng prefab để spawn
    public float spawnZone = 10f; // Bán kính spawn
    public float gapDistance = 2f; // Khoảng cách tối thiểu giữa các object
    public int maxObjects = 10; // Số lượng object tối đa
    private int countObject = 0; // Đếm số object đã spawn
    private List<Vector3> spawnedPositions = new List<Vector3>(); // Lưu vị trí đã spawn
    private List<GameObject> spawedObjects = new List<GameObject>();
    private void Start()
    {
        sponeGameObject();
    }
    public void sponeGameObject()
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
            randomPosition.y = 5f; // Cố định y để trên mặt đất

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
                GameObject spawned = Instantiate(objToSpawn, randomPosition, Quaternion.Euler(0, 0, 0));
                spawedObjects.Add(spawned);
                spawnedPositions.Add(randomPosition);
                countObject++;
            }
        }
    }
    public void missionRelease()
    {
        // Bước 1: Gom các object cùng tên
        Dictionary<string, List<GameObject>> groupedObjects = new Dictionary<string, List<GameObject>>();
        foreach (GameObject obj in spawedObjects)
        {
            string objName = obj.name.Replace("(Clone)", "").Trim(); // loại bỏ (Clone) Unity tự thêm
            if (!groupedObjects.ContainsKey(objName))
            {
                groupedObjects[objName] = new List<GameObject>();
            }
            groupedObjects[objName].Add(obj);
        }  

        // Bước 2: Tạo nhiệm vụ ngẫu nhiên
        foreach (var kvp in groupedObjects)
        {
            string objName = kvp.Key;
            List<GameObject> objList = kvp.Value;

            // Chọn số lượng nhiệm vụ ngẫu nhiên (ví dụ từ 1 đến tổng số object của loại đó)
            int taskCount = Random.Range(1, objList.Count + 1);

            Debug.Log($"Mission: Collect {taskCount} of {objName}");
            // Ở đây bạn có thể lưu vào list nhiệm vụ hoặc trigger UI nhiệm vụ
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
