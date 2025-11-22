using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour,
    IUnityAdsInitializationListener,
    IUnityAdsLoadListener,
    IUnityAdsShowListener
{
    public static AdsManager Instance;

    private Action onRewardEarned;

#if UNITY_ANDROID
    private string gameId = "5987217";
    private string interstitialId = "adsAndroid_1";
    private string rewardedId = "Rewarded_Android"; // tạm – lát sẽ tạo

#endif

    private void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize với testMode = false (Dashboard sẽ điều khiển test)
        Advertisement.Initialize(gameId, false, this);
    }

    // ================== LOAD ==================
    private void LoadInterstitial()
    {
        Advertisement.Load(interstitialId, this);
    }

    private void LoadRewarded()
    {
        Advertisement.Load(rewardedId, this);
    }

    public void LoadAll()
    {
        LoadInterstitial();
        LoadRewarded();
    }

    // ================== SHOW ==================

    public void ShowInterstitial()
    {
        Advertisement.Show(interstitialId, this);
    }

    public void ShowRewarded(Action rewardCallback)
    {
        onRewardEarned = rewardCallback;
        Advertisement.Show(rewardedId, this);
    }

    // ============== CALLBACKS ==============

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads INITIALIZED");
        LoadAll();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads INIT FAILED: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"AD LOADED: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"LOAD FAILED ({placementId}): {message}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState state)
    {
        Debug.Log($"SHOW COMPLETE: {placementId}, State: {state}");

        // Reward
        if (placementId == rewardedId &&
            state == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("REWARD PLAYER");
            onRewardEarned?.Invoke();
        }

        // Load lại
        Advertisement.Load(placementId, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"SHOW FAILED ({placementId}): {message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }
}
