using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class TaskNeed : MonoBehaviour
{
    private int count = 0;
    public int countOfMission = 0;
    public int maxCountOfMission = 3;
    public Dictionary<string, int> TaskNeeding = new Dictionary<string, int>();
    //Nên tính toán theo level hiện tại. có lẽ nên có set tối đa.
    private void Update()
    {
        GameObject[] curentOBJ = GameObject.FindGameObjectsWithTag("Feed");
        // có danh sách các objects feed;
        Dictionary<string, List<GameObject>> GroupedObjects = new Dictionary<string, List<GameObject>>();
        foreach (GameObject Objs in curentOBJ)
        {
            string name = GetCleanName(Objs);
            if (!GroupedObjects.ContainsKey(name))
            {
                GroupedObjects[name] = new List<GameObject>();
            }
            GroupedObjects[name].Add(Objs);
            count++;
        }
        if (count == curentOBJ.Length)
        {
            count++;
            foreach (var pair in GroupedObjects)
            {
                Debug.Log($"đang có {pair.Value.Count} {pair.Key}");
            }
        }
        //phân công nhiêm vụ
        foreach (var groupedObjs in GroupedObjects)
        {
            if (countOfMission < maxCountOfMission)
            {
                countOfMission++;
                int randum = Random.Range(0, groupedObjs.Value.Count - 4);
                string taskName = groupedObjs.Key;
                int taskCount = groupedObjs.Value.Count - randum;
                TaskNeeding[taskName]=taskCount;
                Debug.Log($"{taskName} cái {taskCount}");
            }
        }
        //foreach(var task in ThisTask)
        //{
        //    Debug.Log($"{task.Value} nhiệm vụ {task.Key}");
        //}
        // làm sao để đếm số lượng?

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
