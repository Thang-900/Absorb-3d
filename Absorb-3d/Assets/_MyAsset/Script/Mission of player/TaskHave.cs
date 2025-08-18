using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // để dùng ToDictionary

public class TaskHave : MonoBehaviour
{
    public Dictionary<string, int> TaskHaving = new Dictionary<string, int>();
    private TaskNeed taskNeed;

    void Start()
    {
        taskNeed = GameObject.Find("TaskManager").GetComponent<TaskNeed>();
    }

    private void LateUpdate()
    {
        // Nếu muốn copy nguyên dictionary (key + value giống hệt)
        // TaskHaving = new Dictionary<string, int>(taskNeed.TaskNeeding);

        // Nếu chỉ muốn copy key và set value = 0
        TaskHaving = taskNeed.TaskNeeding.Keys.ToDictionary(k => k, k => 0);

    }
    public void taskProgress(GameObject absortedObj)
    {
        string taskName = TaskNeed.GetCleanName(absortedObj);
        TaskHaving[taskName] += 1;
    }
}
