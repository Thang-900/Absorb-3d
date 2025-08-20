using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskState : MonoBehaviour
{
    public string[] need;
    public string[] have;
    private List<string> taskManager = new List<string>();
    TaskHave taskHave;
    TaskNeed taskNeed;
    // Start is called before the first frame update
    void Start()
    {
        taskHave = GameObject.Find("TaskManager").GetComponent<TaskHave>();
        taskNeed = GameObject.Find("TaskManager").GetComponent<TaskNeed>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        need = taskNeed.TaskNeeding.Keys.ToArray();
        have = taskHave.TaskHaving.Keys.ToArray();
        foreach (string task in need)
        {
            foreach (string haveTask in have)
            {
                if (task == haveTask)
                {
                    taskManager.Add(task);
                    Debug.Log($"có nhiệm vụ: {task}");
                }
            }
        }
        // có tên của nhiệm vụ -  cần xử lý hiển thị số 
        // làm sao để có nhiều nhiệm vụ đê hiển thị ra màn hình?
        // Nếu need == have thì xóa nhiệm vụ đang hiển thi?
        //
        foreach (var task in taskManager)
        {
            if (taskHave.TaskHaving[task] >= taskNeed.TaskNeeding[task])
            {
                Debug.Log($"hoàn thành nhiệm vụ: {task}");
                taskHave.TaskHaving.Remove(task);
                taskNeed.TaskNeeding.Remove(task);
                taskManager.Remove(task);
            }
            else
            {
                Debug.Log($"còn thiếu {taskNeed.TaskNeeding[task] - taskHave.TaskHaving[task]} ở nhiệm vụ {task}");
            }
        }

    }
}
