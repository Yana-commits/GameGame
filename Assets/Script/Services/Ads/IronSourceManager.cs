using Object = UnityEngine.Object;
using UnityEngine;
using Game.Data;

namespace Services.Ads
{
    public class IronSourceManager : IAdvertiseProvider
    {
        private const string appKey = "e336ada1";
        
        private static IronSourceManager instance;
        public static IAdvertiseProvider Instance => instance;

        private ApplicationPauseHandler pauseHandler; 

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            instance = new IronSourceManager();
        }

        private IronSourceManager()
        {
            var go = new GameObject(nameof(ApplicationPauseHandler));
            Object.DontDestroyOnLoad(go);
            pauseHandler = go.AddComponent<ApplicationPauseHandler>();

            IronSource.Agent.validateIntegration();
            IronSource.Agent.init(appKey);
      
            IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;

            IronSource.Agent.loadInterstitial();
            if(!UserDataController.Instance().info.advert)
                IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.TOP);
        }
        ~IronSourceManager()
        {
            IronSourceEvents.onInterstitialAdReadyEvent -= InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdShowSucceededEvent -= InterstitialAdShowSucceededEvent;
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
                IronSource.Agent.showInterstitial();
        }
     

        public void ShowBanner()
        {
            IronSource.Agent.displayBanner();
        }
        public void HideBanner()
        {
            IronSource.Agent.hideBanner();
        }

        private void OnApplicationPause(bool isPaused)
        {
            IronSource.Agent.onApplicationPause(isPaused);
        }

        private class ApplicationPauseHandler : MonoBehaviour
        {
            private void OnApplicationPause(bool isPaused)
                => instance.OnApplicationPause(isPaused);
        }
    }
}