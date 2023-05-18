using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager instance;

#if UNITY_IOS
    private string gameId = "";
    private string rewardPlacementId = "";
    private string interPlacementId = "";

#elif UNITY_ANDROID
    private string gameId = "5281015";
    private string rewardPlacementId = "Rewarded_Android";
    private string interPlacementId = "Interstitial_Android";
#endif

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);

        Advertisement.Initialize(gameId, false, this);
    }

    public void ShowRewardAd()
    {
        Advertisement.Show(rewardPlacementId, this);
    }

    public void ShowInterAd()
    {
        Advertisement.Show(interPlacementId, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("ad init success");
        Advertisement.Load(rewardPlacementId, this);
        Advertisement.Load(interPlacementId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("ad init fail");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("ad load success:" + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
      
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        AudioManager.instance.bgmSource.Stop();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        TransitionManager.instance.Transition("GamePlay");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
    }
}
