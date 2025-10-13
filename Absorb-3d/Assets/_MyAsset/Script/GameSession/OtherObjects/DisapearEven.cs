using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearEven : MonoBehaviour
{
    private LevelUp levelUp;
    private TaskManager TaskManager;
    private SetBonusText setText;
    private GoldBonus goldBonus;

    public float experienceGain = 100f; // Amount of experience to gain on release
    public int goldGain = 100;
    private void Start()
    {
        goldGain = 100;
    }
    void Awake()
    {
        setText = GameObject.Find("SetBonusText").GetComponent<SetBonusText>();
        levelUp = GameObject.Find("Ground").GetComponent<LevelUp>();
        TaskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        goldBonus = GameObject.Find("GameJsonManager").GetComponent<GoldBonus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= -1f)
        {
            string textExperience = "+" + experienceGain.ToString() + " EXP";
            levelUp.GainExperience(experienceGain); // Assuming you want to gain 100 experience on release
            setText.callText(textExperience);// goi màu ch? khác nhau cho d?p
            TaskManager.TaskProgress(gameObject);// update task progress in system 
            TaskManager.ReleaseTask(); // Show task proress for phayer
            GoldBonus.AddGoldBonus(goldGain); // Add gold bonus 
            Destroy(gameObject);
        }
    }
}
