using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public int maxCountOfMission;
    public Dictionary<string, int> taskNeeding = new Dictionary<string, int>();
    public Dictionary<string, List<GameObject>> GroupedObjects = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, int> taskFinish = new Dictionary<string, int>();
    public Text taskComment;

    private bool tasksAssigned = false;
    private bool tasksCompleted = false; // ✅ tránh chạy nhiều lần
    private List<string> keysOfTaskNeed = new List<string>();

    public PlayerInformationManager playerInfoManager;
    public DocumentControl documentControl;
    //public bool needUpdateLevel = false;

    private void Start()
    {
        documentControl = FindObjectOfType<DocumentControl>();
        playerInfoManager = FindObjectOfType<PlayerInformationManager>();
    }
    //private void Start()
    //{
    //    playerInfoManager = FindObjectOfType<PlayerInformationManager>();
    //    documentControl = FindObjectOfType<DocumentControl>();

    //    GroupTask();
    //    CreateTaskNeed();
    //    CreateTaskFinish();
    //    ReleaseTask();
    //    tasksAssigned = true;
    //}

    //private void Update()
    //{
    //    TaskCompleted();
    //}

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
                if (taskCount <= 0) taskCount = 1; // ✅ đảm bảo ít nhất 1 task
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
        taskComment.text = comments;
    }

    private void TaskCompleted()
    {
        if (tasksCompleted) return;

        foreach (var task in taskNeeding)
        {
            if (taskFinish[task.Key] < taskNeeding[task.Key])
                return; // vẫn còn task chưa hoàn thành
        }

        tasksCompleted = true; // ✅ đánh dấu đã hoàn thành
        Debug.Log("🎉 Tất cả nhiệm vụ đã hoàn thành!");
        StartCoroutine(ActionTaskCompleted());
    }
    public void Action()
    {
        StartCoroutine(ActionTaskCompleted());
    }
    private IEnumerator ActionTaskCompleted()
    {
        string playerId = playerInfoManager.currentPlayerId;
        if (string.IsNullOrEmpty(playerId))
        {
            Debug.LogError("⚠️ Không có PlayerId, không thể lưu dữ liệu!");
            yield break;
        }

        DocumentControl.PlayerData playerData = null;
        yield return StartCoroutine(documentControl.GetDocumentById(playerId, data =>
        {
            playerData = data;
        }));

        if (playerData == null)
        {
            Debug.LogError("❌ Không lấy được dữ liệu player từ server!");
            yield break;
        }

        int newLevel = playerData.levelMap + 1;
        int newGold = GoldBonus.goldBonus + playerData.gold;

        yield return StartCoroutine(documentControl.CreateOrUpdateDocument(
            playerId, newGold, playerData.diamond, newLevel
        ));

        GoldBonus.ResetGoldBonus();
        SceneManager.LoadScene("MenuScene");
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
