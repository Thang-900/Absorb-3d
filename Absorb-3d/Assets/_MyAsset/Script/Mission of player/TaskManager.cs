using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using System.Linq;
using UnityEngine.UI; // để dùng ToDictionary

public class TaskManager : MonoBehaviour
{
    public int maxCountOfMission;
    public Dictionary<string, int> taskNeeding = new Dictionary<string, int>();
    public Dictionary<string, List<GameObject>> GroupedObjects = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, int> taskFinish = new Dictionary<string, int>();

    private bool tasksAssigned = false;
    private List<string> keysOfTaskNeed = new List<string>();

    //*******************+*********************

    //Nên tính toán theo level hiện tại. có lẽ nên có set tối đa.
    private void Update()
    {
        if (!tasksAssigned)
        {
            GroupTask();
            CreatTaskNeed();
            tasksAssigned = true; // đánh dấu đã chạy
        }
    }

    //*******************+*********************


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

    private void CreatTaskNeed()//2. Phân công nhiệm vụ từ các object đã nhóm, số lương: "maxCountOfMission" + lấy các key của need để so sánh với finish
    {
        int count = 0;
        foreach (var groupedObjs in GroupedObjects)
        {
            count++;
            if (count < maxCountOfMission && count < GroupedObjects.Count)
            {
                int randum = Random.Range(0, groupedObjs.Value.Count - 1);
                string taskName = groupedObjs.Key;
                int taskCount = groupedObjs.Value.Count - randum;
                taskNeeding[taskName] = taskCount;
            }
        }
        foreach (var task in taskNeeding)
        {
            keysOfTaskNeed.Add(task.Key);
        }
    }

    //*******************+*********************


    public void CreatTaskFinish()//3. Tao dictionary theo dõi tiến độ hoàn thành nhiệm vụ
    {
        taskFinish = taskNeeding.Keys.ToDictionary(keySelector: k => k, elementSelector: k => 0);
    }

    public void TaskProgress(GameObject absortedObj)// 4.tăng taskFinish lên khi một object bị biến mất
    {
        string taskName = GetCleanName(absortedObj);
        taskFinish[taskName] += 1;
    }
    public void ReleaseTask()//5.hiển thị tiến độ hoàn thành nhiêm vụ
    {
        string comments = "";
        foreach (var task in keysOfTaskNeed)
        {
            if (taskFinish[task] >= taskNeeding[task])
            {
                Debug.Log($"hoàn thành nhiệm vụ: {task}");
                //taskHave.TaskHaving.Remove(task);
                //taskNeed.TaskNeeding.Remove(task);
                //taskManager.Remove(task);
            }
            else
            {
                comments += $"{task} cần: {taskNeeding[task]} có: {taskFinish[task]}\n";
            }
        }
    }







    //*******************+*********************
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
