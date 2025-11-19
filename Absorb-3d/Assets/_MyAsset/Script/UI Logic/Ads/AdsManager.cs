using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    private System.Action onRewardComplete;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        IronSource.Agent.init("2449a3b55");

        IronSourceEvents.onRewardedVideoAdRewardedEvent += OnUserRewarded;
    }

    public void ShowRewarded(System.Action rewardAction)
    {
        onRewardComplete = rewardAction;

        if (IronSource.Agent.isRewardedVideoAvailable())
            IronSource.Agent.showRewardedVideo();
        else
        {
            Debug.Log("Rewarded not available!");
        }
    }

    private void OnUserRewarded(IronSourcePlacement placement)
    {
        onRewardComplete?.Invoke();
    }

    private void OnApplicationPause(bool pause)
    {
        IronSource.Agent.onApplicationPause(pause);
    }
}
