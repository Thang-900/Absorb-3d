using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealseBonusText : MonoBehaviour
{
    private SetBonusText setText;
    public string textBonus;
    private LevelUp levelUp;
    private TaskHave taskHave;
    TaskState taskState;
    public float experienceGain = 100f; // Amount of experience to gain on release

    void Awake()
    {
        setText = GameObject.Find("SetBonusText").GetComponent<SetBonusText>();
        levelUp = GameObject.Find("Ground").GetComponent<LevelUp>();
        taskHave = GameObject.Find("TaskManager").GetComponent<TaskHave>();
        taskState = GameObject.Find("TaskManager").GetComponent<TaskState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= -1.5f)
        {
            levelUp.GainExperience(experienceGain); // Assuming you want to gain 100 experience on release
            setText.callText(textBonus);// goi m�u ch? kh�c nhau cho d?p
            taskHave.taskProgress(gameObject);// g?i d? c?p nh?t l?i s? lu?ng d� ho�n th�nh
            taskState.updateTask(); // g?i d? c?p nh?t nhi?m v?
            Destroy(gameObject); // h?y v?t th?
        }
    }
}
