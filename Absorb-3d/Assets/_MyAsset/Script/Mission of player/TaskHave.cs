using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // để dùng ToDictionary

public class TaskHave : MonoBehaviour
{
    public Dictionary<string, int> TaskHaving = new Dictionary<string, int>();
    private TaskNeed taskNeed;
    private bool checkedTask = false;

    void Start()
    {
        taskNeed = GameObject.Find("TaskManager").GetComponent<TaskNeed>();
    }

    private void LateUpdate()
    {
        if (!checkedTask)
        {
            creat();
            checkedTask = true;
        }
    }
    private void creat() // xử lý tiến độ hoàn thành nhiệm vụ
    {

        // Nếu muốn copy nguyên dictionary (key + value giống hệt)
        // TaskHaving = new Dictionary<string, int>(taskNeed.TaskNeeding);

        // Nếu chỉ muốn copy key và set value = 0

        TaskHaving = taskNeed.GroupedObjects.Keys.ToDictionary(keySelector: k => k, elementSelector: k => 0);

        // xem danh sach cac object dang co
        foreach (var task in TaskHaving)
        {
            Debug.Log($"++Nhiệm vụ taskHave: {task.Key}, Số lượng: {task.Value}");
        }
        foreach(var task in taskNeed.TaskNeeding)
        {
            Debug.Log($"++Nhiệm vụ taskNeed: {task.Key}, Số lượng: {task.Value}");
        }

    }
    public void taskProgress(GameObject absortedObj)
    {
        string taskName = TaskNeed.GetCleanName(absortedObj);
        TaskHaving[taskName] += 1;
        Debug.Log($" +1 ở {taskName} , số lượng hiện tại:{TaskHaving[taskName]} ");
    }
}
