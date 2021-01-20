using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    private BannerView bannerView;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        DontDestroyOnLoad(this);
        RequestBanner();
    }

    public void RequestBanner(AdPosition position = AdPosition.Bottom)
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, position);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void ShowBanner()
    {
        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }
    public void DestroyBanner()
    {
        bannerView.Destroy();
    }
}
