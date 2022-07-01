using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private void Start()
    {
        Advertisement.Initialize("4811314");
        Advertisement.AddListener(this);
    }

    public void PlayRewardedAd()
    {
        //Rewarded_Android
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
        else { Debug.Log("Ad is not ready!"); }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log(name + " ready.");
    }

    public void OnUnityAdsDidError(string message)
    { }

    public void OnUnityAdsDidStart(string placementId)
    { }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
        {
            CannonShopManager.instance.doubleCash = true;
        }
    }
}