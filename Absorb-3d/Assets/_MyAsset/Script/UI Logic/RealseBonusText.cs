using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealseBonusText : MonoBehaviour
{
    private SetBonusText setText;
    public string textBonus;
    private LevelUp levelUp;
    private TaskHave taskHave;
    public float experienceGain = 100f; // Amount of experience to gain on release

    void Awake()
    {
        setText = GameObject.Find("SetBonusText").GetComponent<SetBonusText>();
        levelUp = GameObject.Find("Ground").GetComponent<LevelUp>();
        taskHave = GameObject.Find("TaskManager").GetComponent<TaskHave>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= -1.5f)
        {
            levelUp.GainExperience(experienceGain); // Assuming you want to gain 100 experience on release
            setText.callText(textBonus);
            taskHave.taskProgress(gameObject);
            Destroy(gameObject);
        }
    }
}
