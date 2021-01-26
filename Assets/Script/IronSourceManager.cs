using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceManager : MonoBehaviour
{
    private string appKey = "e336ada1";
    void Start()
    {

        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        Debug.Log("unity-script: unity version" + IronSource.unityVersion());

        Debug.Log("unity-script: IronSource.Agent.init");
        IronSource.Agent.init(appKey);

        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;

        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;

        IronSource.Agent.loadInterstitial();
    }

    private void InterstitialAdShowSucceededEvent()
    {
        Debug.Log("WAs shown");
    }

    void InterstitialAdReadyEvent()
    {
        Debug.Log("Yeaaa");
    }

    public void ShowInterstitial()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
    }
    void OnDestroy()
    {
        IronSourceEvents.onInterstitialAdReadyEvent -= InterstitialAdReadyEvent;

        IronSourceEvents.onInterstitialAdShowSucceededEvent -= InterstitialAdShowSucceededEvent;
    }

}
