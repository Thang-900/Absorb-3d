using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TaskState : MonoBehaviour
{
    public string[] need;
    public string[] have;
    private List<string> taskManager = new List<string>();
    TaskHave taskHave;
    TaskManager taskNeed;
    bool runTaskRelease = false;
    public Text taskComment;
    string comments="";
    // Start is called before the first frame update
    void Start()
    {
        taskHave = GameObject.Find("TaskManager").GetComponent<TaskHave>();
        taskNeed = GameObject.Find("TaskManager").GetComponent<TaskManager>();
    }
    
    
    // Update is called once per frame
    void LateUpdate()
    {
        //hàm dưới chạy một lần

        //// có tên của nhiệm vụ -  cần xử lý hiển thị số 
        //// làm sao để có nhiều nhiệm vụ đê hiển thị ra màn hình?
        //// Nếu need == have thì xóa nhiệm vụ đang hiển thi?
        if(!runTaskRelease)
        {
            taskRelease();
            updateTask();
            runTaskRelease = true;
        }
        taskComment.text = comments;
    }
    private void taskRelease()
    {
        need = taskNeed.taskNeeding.Keys.ToArray();
        have = taskHave.TaskHaving.Keys.ToArray();
        foreach (var ne in need)
        {
            Debug.Log($" need có {ne}");
        }
        foreach (var ne in have)
        {
            Debug.Log($" have có {ne}");
        }

        foreach (string task in need)
        {
            //foreach (string haveTask in have)
            //{
            //    if (task == haveTask)
            //    {
                    taskManager.Add(task);
                    Debug.Log($"need và have có chung task: {task}");
            //    }
            //}
        }
    }
    //// chạy hàm bên dưới mỗi khi một object biến mất
    public void updateTask()
    {
        comments = "";
        foreach (var task in taskManager)
        {
            if (taskHave.TaskHaving[task] >= taskNeed.taskNeeding[task])
            {
                Debug.Log($"hoàn thành nhiệm vụ: {task}");
                //taskHave.TaskHaving.Remove(task);
                //taskNeed.TaskNeeding.Remove(task);
                //taskManager.Remove(task);
            }
            else
            {
                Debug.Log($"còn thiếu {taskNeed.taskNeeding[task] - taskHave.TaskHaving[task]} ở nhiệm vụ {task}");
                comments += $"{task} cần: {taskNeed.taskNeeding[task]} có: {taskHave.TaskHaving[task]}\n";
                Debug.Log("222 " + comments);
            }
        }
    }
}
