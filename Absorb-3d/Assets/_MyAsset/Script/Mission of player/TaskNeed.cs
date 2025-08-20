using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class TaskNeed : MonoBehaviour
{
    //private int count = 0;
    public int countOfMission = 0;
    public int maxCountOfMission = 3;
    public Dictionary<string, int> TaskNeeding = new Dictionary<string, int>();
    public Dictionary<string, List<GameObject>> GroupedObjects = new Dictionary<string, List<GameObject>>();
    private bool tasksAssigned = false;
    public int count = 0; // đếm số lượng nhiệm vụ đã phân công, để tránh lặp lại   

    //Nên tính toán theo level hiện tại. có lẽ nên có set tối đa.
    private void Update()
    {
        if (!tasksAssigned)
        {
            taskPublish();
            tasksAssigned = true; // đánh dấu đã chạy
        }
    }
    private void taskPublish()
    {
        GameObject[] curentOBJ = GameObject.FindGameObjectsWithTag("Feed");

        // có danh sách các objects feed;
        foreach (GameObject Objs in curentOBJ)
        {
            string name = GetCleanName(Objs);
            if (!GroupedObjects.ContainsKey(name))
            {
                GroupedObjects[name] = new List<GameObject>();
            }
            GroupedObjects[name].Add(Objs);

        }

        //xem các nhóm objects

        foreach (var pair in GroupedObjects)
        {
            Debug.Log($"đang có {pair.Value.Count} ở nhóm {pair.Key}");
        }


        //phân công nhiêm vụ
        foreach (var groupedObjs in GroupedObjects)
        {
            if (countOfMission < maxCountOfMission && countOfMission < GroupedObjects.Count)
            {
                int randum = Mathf.Max(0, groupedObjs.Value.Count - 4);
                string taskName = groupedObjs.Key;
                int taskCount = groupedObjs.Value.Count - randum;
                TaskNeeding[taskName] = taskCount;
                Debug.Log($"phân công {taskName}, số lương:  {taskCount}, số lượng lặp: {countOfMission}, số lượng nhóm: {GroupedObjects.Count}");
            }
            countOfMission++;

        }
    }
    public static string GetCleanName(GameObject obj)
    {
        // Lấy tên gốc
        string rawName = obj.name;

        // Bỏ chữ "Clone"
        string noClone = rawName.Replace("Clone", "");

        // Bỏ hết số
        string noNumbers = Regex.Replace(noClone, @"\d+", "");

        // Bỏ hết dấu ngoặc ()
        string noBrackets = Regex.Replace(noNumbers, @"[()]", "");

        // Xóa khoảng trắng thừa
        return noBrackets.Trim();
    }



}
