using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    void Start()
    {
        if (Advertisement.isInitialized)
        {
            Debug.Log("Unity Ads đã được khởi tạo.");
        }
        else
        {
            Debug.Log("Unity Ads chưa khởi tạo.");
        }
    }

}
