using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Ads
{ 
    public interface IAdvertiseProvider 
    {
        void ShowBanner();
        void HideBanner();
        void ShowInterstitial();
    }
}