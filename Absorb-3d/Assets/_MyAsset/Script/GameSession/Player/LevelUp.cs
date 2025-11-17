using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentExperience = 0f;
    public float experienceToNextLevel = 100f;
    public int currentLevel = 1;
    public GameObject showExperienceRate;
    public GameObject maxExperienceRate;
    private SetBonusText sponeLevelUpText;
    
    private void OnEnable()
    {
        currentLevel = 1;
        currentExperience = 0f;
        experienceToNextLevel = 1000f;
        gameObject.transform.localScale= Vector3.one;
        GainExperience(0);
        sponeLevelUpText = GameObject.Find("SetBonusText").GetComponent<SetBonusText>();
    }
    public void GainExperience(float amount)
    {
        currentExperience += amount;
        if (currentExperience >= experienceToNextLevel)
        {
            LevelUpp();
        }
        showExperienceRate.transform.localScale= maxExperienceRate.transform.localScale * (currentExperience / experienceToNextLevel);
    }
    private void LevelUpp()
    {
        sponeLevelUpText.callText("Level Up");
        gameObject.transform.localScale *= 1.1f; // Increase size by 10%
        currentLevel++;
        currentExperience -= experienceToNextLevel;
        experienceToNextLevel *= 1.2f; // Increase the requirement for the next level
        Debug.Log("Leveled up to level " + currentLevel + "! Next level requires " + experienceToNextLevel + " experience.");
    }
    
}
