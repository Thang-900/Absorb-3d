using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public int maxCountOfMission;
    public Dictionary<string, int> taskNeeding = new Dictionary<string, int>();
    public Dictionary<string, List<GameObject>> GroupedObjects = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, int> taskFinish = new Dictionary<string, int>();

    public Text taskComment;
    public DataManager dataManager;
    private bool tasksAssigned = false;
    private bool tasksCompleted = false;
    private List<string> keysOfTaskNeed = new List<string>();
    private SetTopic setTopic;

    // ==========================
    // 🔥 CHẠY MỖI KHI OBJECT ACTIVE
    // ==========================
    private void OnEnable()
    {
        ResetData();         // clear dữ liệu cũ
        InitializeTasks();   // chạy lại logic như Start()
    }

    // ==========================
    // 🔥 CHẠY MỖI KHI OBJECT BỊ TẮT (SetActive(false))
    // ==========================
    private void OnDisable()
    {
        ResetData();
    }


    // ==========================
    // Tách thành hàm riêng để tái sử dụng
    // ==========================
    private void InitializeTasks()
    {
        dataManager = FindObjectOfType<DataManager>();
        setTopic = FindObjectOfType<SetTopic>();

        GroupTask();
        CreateTaskNeed();
        CreateTaskFinish();
        ReleaseTask();

        tasksAssigned = true;
        tasksCompleted = false;
    }

    private void ResetData()
    {
        taskNeeding.Clear();
        GroupedObjects.Clear();
        taskFinish.Clear();
        keysOfTaskNeed.Clear();
        tasksAssigned = false;
        tasksCompleted = false;

        if (taskComment != null)
            taskComment.text = "";
    }



    private void Update()
    {
        TaskCompleted();
    }

    private void GroupTask()
    {
        GameObject[] currentOBJ = GameObject.FindGameObjectsWithTag("Feed");

        foreach (GameObject obj in currentOBJ)
        {
            string name = GetCleanName(obj);
            if (!GroupedObjects.ContainsKey(name))
                GroupedObjects[name] = new List<GameObject>();

            GroupedObjects[name].Add(obj);
        }
    }

    private void CreateTaskNeed()
    {
        int count = 0;
        foreach (var groupedObjs in GroupedObjects)
        {
            count++;
            if (count <= maxCountOfMission && groupedObjs.Value.Count > 0)
            {
                int randIndex = Random.Range(0, groupedObjs.Value.Count);
                int taskCount = groupedObjs.Value.Count - randIndex;
                if (taskCount <= 0) taskCount = 1;
                taskNeeding[groupedObjs.Key] = taskCount;
            }
        }
        keysOfTaskNeed = taskNeeding.Keys.ToList();
    }

    public void CreateTaskFinish()
    {
        taskFinish = taskNeeding.Keys.ToDictionary(k => k, k => 0);
    }

    public void TaskProgress(GameObject absorbedObj)
    {
        string taskName = GetCleanName(absorbedObj);
        if (!tasksAssigned || !taskFinish.ContainsKey(taskName))
            return;

        taskFinish[taskName] += 1;
        ReleaseTask();
    }

    public void ReleaseTask()
    {
        string comments = "";
        foreach (var task in keysOfTaskNeed)
        {
            if (taskFinish[task] >= taskNeeding[task])
                Debug.Log($"✅ Hoàn thành nhiệm vụ: {task}");
            else
                comments += $"{task}: cần {taskNeeding[task]}, có {taskFinish[task]}\n";
        }

        if (taskComment != null)
            taskComment.text = comments;
    }

    private void TaskCompleted()
    {
        if (tasksCompleted) return;

        foreach (var task in taskNeeding)
        {
            if (taskFinish[task.Key] < taskNeeding[task.Key])
                return;
        }

        tasksCompleted = true;

        dataManager.SaveGold();
        Debug.Log("🎉 Tất cả nhiệm vụ đã hoàn thành!");
        setTopic.SetTopicToMainMenu();
    }

    public static string GetCleanName(GameObject obj)
    {
        string rawName = obj.name;
        string noClone = rawName.Replace("Clone", "");
        string noNumbers = Regex.Replace(noClone, @"\d+", "");
        string noBrackets = Regex.Replace(noNumbers, @"[()]", "");
        return noBrackets.Trim();
    }
}
