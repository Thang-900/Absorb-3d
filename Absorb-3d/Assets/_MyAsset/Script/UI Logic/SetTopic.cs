using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTopic : MonoBehaviour
{
    public GameObject play;
    public GameObject MainMenu;

    public void SetTopicToPlay()
    {
        play.SetActive(true);
        MainMenu.SetActive(false);

    }
    public void SetTopicToMainMenu()
    {
        play.SetActive(false);
        MainMenu.SetActive(true);
    }
}
