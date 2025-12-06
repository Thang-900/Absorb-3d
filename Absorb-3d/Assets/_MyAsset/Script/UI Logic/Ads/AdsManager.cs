using UnityEngine;
using Unity.Services.Core;
using Unity.Services.LevelPlay;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    public string bannerAdUnitId = "2lhf295oshtpe0wz";
    public string interstitialAdUnitId = "na9pnti60cr4n8og";
    public string rewardedAdUnitId = "ox1h9iosgqbk63i1";
    public string appKey = "2449a3b55";

    private LevelPlayBannerAd banner;
    private LevelPlayInterstitialAd interstitial;
    private LevelPlayRewardedAd rewarded;

    // Callback sẽ chạy khi xem xong ads
    private Action rewardCallback;

    private async void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }

        await UnityServices.InitializeAsync();

        string userId = SystemInfo.deviceUniqueIdentifier;
        LevelPlay.Init(appKey, userId);

        // Create ads
        banner = new LevelPlayBannerAd(bannerAdUnitId);
        interstitial = new LevelPlayInterstitialAd(interstitialAdUnitId);
        rewarded = new LevelPlayRewardedAd(rewardedAdUnitId);

        // Register rewarded events
        rewarded.OnAdRewarded += OnRewardedSuccess;
        rewarded.OnAdClosed += OnRewardedClosed;

        // Load ads
        banner.LoadAd();
        interstitial.LoadAd();
        rewarded.LoadAd();
    }

    // ---------------- BANNER ----------------
    public void ShowBanner() => banner?.ShowAd();
    public void HideBanner() => banner?.HideAd();

    // ---------------- INTER ----------------
    public void ShowInterstitial()
    {
        if (interstitial != null && interstitial.IsAdReady())
            interstitial.ShowAd();
        else
            interstitial?.LoadAd();
    }

    // ---------------- REWARDED ----------------
    public void ShowRewarded(Action onRewarded)
    {
        rewardCallback = onRewarded;

        if (rewarded != null && rewarded.IsAdReady())
            rewarded.ShowAd();
        else
            rewarded?.LoadAd();
    }

    private void OnRewardedSuccess(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        Debug.Log("🎉 Rewarded Ads: SUCCESS – Running reward callback!");

        rewardCallback?.Invoke();
        rewardCallback = null;
    }

    private void OnRewardedClosed(LevelPlayAdInfo adInfo)
    {
        Debug.Log("❌ Rewarded Ads closed.");
    }
}
