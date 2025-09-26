using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using System.Linq; // để dùng ToDictionary

public class TaskManager : MonoBehaviour
{
    //private int count = 0;
    public int maxCountOfMission;
    public Dictionary<string, int> TaskNeeding = new Dictionary<string, int>();
    public Dictionary<string, List<GameObject>> GroupedObjects = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, int> TaskHaving = new Dictionary<string, int>();

    private bool tasksAssigned = false;

    //Nên tính toán theo level hiện tại. có lẽ nên có set tối đa.
    private void Update()
    {
        if (!tasksAssigned)
        {
            GroupTask();
            TasksAssignment();
            tasksAssigned = true; // đánh dấu đã chạy
        }
    }
    private void GroupTask()//1. nhóm các object cùng loại
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
    }

    private void TasksAssignment()//2. Phân công nhiệm vụ từ các object đã nhóm
    {
        int count = 0;
        //phân công nhiêm vụ
        foreach (var groupedObjs in GroupedObjects)
        {
            count++;
            Debug.Log($"đang phân công nhiệm vụ lần {count}");
            if (count < maxCountOfMission && count < GroupedObjects.Count)
            {
                int randum = Random.Range(0, groupedObjs.Value.Count - 1);
                string taskName = groupedObjs.Key;
                int taskCount = groupedObjs.Value.Count - randum;
                TaskNeeding[taskName] = taskCount;
            }

        }
    }

    private void TasksProgress()//3. Cập nhật tiến độ nhiệm vụ trong background
    {
        TaskHaving = TaskNeeding.Keys.ToDictionary(keySelector: k => k, elementSelector: k => 0);

    }  
    private void TaskProgress(string key)
    {
        TaskHaving[key] += 1;

    }
    public static string GetCleanName(GameObject obj)//1.2. Chuẩn hóa tên object
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
