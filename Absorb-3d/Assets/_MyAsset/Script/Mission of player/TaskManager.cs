using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // để dùng ToDictionary

public class TaskManager : MonoBehaviour
{
    public int maxCountOfMission;
    public Dictionary<string, int> taskNeeding = new Dictionary<string, int>();
    public Dictionary<string, List<GameObject>> GroupedObjects = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, int> taskFinish = new Dictionary<string, int>();
    public Text taskComment;
    private bool tasksAssigned = false;
    private List<string> keysOfTaskNeed = new List<string>();

    //*******************+*********************
    private void Start()
    {
        GroupTask();
        CreatTaskNeed();
        CreatTaskFinish();
        ReleaseTask();
        tasksAssigned = true; // đánh dấu đã chạy
    }
    private void Update()
    {
        TaskCompleted();
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
            if (count <= maxCountOfMission && count <= GroupedObjects.Count)
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
        if (!tasksAssigned || !taskFinish.ContainsKey(taskName))
            return;
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
            }
            else
            {
                comments += $"{task} cần: {taskNeeding[task]} có: {taskFinish[task]}\n";
            }
        }
        taskComment.text = comments;

    }
    private void TaskCompleted()
    {
        foreach (var task in taskNeeding)
        {
            if (taskFinish[task.Key] < taskNeeding[task.Key])
                return; // Chưa hoàn thành tất cả nhiệm vụ
        }
        Debug.Log("🎉 Tất cả nhiệm vụ đã hoàn thành!");
        ActionTaskCompleted();
        // Thực hiện hành động khi tất cả nhiệm vụ hoàn thành
    }
    private void ActionTaskCompleted()
    {
        DataManager.instance.SaveGold();
        DataManager.instance.SaveMapLevel();
        SceneManager.LoadScene("MenuScene");
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
