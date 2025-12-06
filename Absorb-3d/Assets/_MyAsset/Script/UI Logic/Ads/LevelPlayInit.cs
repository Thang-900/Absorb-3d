using UnityEngine;
using Unity.Services.Core;
using Unity.Services.LevelPlay;

public class LevelPlayInit : MonoBehaviour
{
    async void Awake()
    {
        Debug.Log("Initializing Unity Services + LevelPlay...");
        await UnityServices.InitializeAsync();
        Debug.Log("LevelPlay Initialized!");
    }
}
