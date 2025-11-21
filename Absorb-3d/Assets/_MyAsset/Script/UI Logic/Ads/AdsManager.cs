using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager Instance;
    public Action onRewardEarned;

#if UNITY_ANDROID
    private string gameId = "5987216";
    private string interstitialId = "Interstitial_Android";
    private string rewardedId = "Rewarded_Android";
#endif

    private void Awake()
    {
        Instance = this;
        Advertisement.Initialize(gameId, false, this);
    }

    // ------------------ LOAD ------------------
    public void LoadInterstitial()
    {
        Advertisement.Load(interstitialId, this);
    }

    public void LoadRewarded()
    {
        Advertisement.Load(rewardedId, this);
    }

    // ------------------ SHOW ------------------
    public void ShowInterstitial()
    {
        Advertisement.Show(interstitialId, this);
    }

    public void ShowRewarded()
    {
        Advertisement.Show(rewardedId, this);
    }

    // ------------------ CALLBACKS ------------------
    public void OnInitializationComplete()
    {
        Debug.Log("Ads Init Done");
        LoadInterstitial();
        LoadRewarded();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Init FAILED: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Loaded: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Load FAILED {placementId}: {message}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Show Complete: {placementId}");

        if (placementId == rewardedId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("REWARD THE PLAYER");
        }

        // Load lại sau khi xem xong
        Advertisement.Load(placementId, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Show FAILED {placementId}: {message}");
    }
    public void ShowRewarded(Action rewardCallback = null)
    {
        onRewardEarned = rewardCallback;

        Advertisement.Show(rewardedId, this);
    }

    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowClick(string placementId) { }
}
