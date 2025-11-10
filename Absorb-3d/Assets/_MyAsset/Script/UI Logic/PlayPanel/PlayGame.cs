using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainScene"); 
    }
}
