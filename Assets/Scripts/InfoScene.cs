using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(NewBanner());
    }

    IEnumerator NewBanner()
    {
        AdsManager.Instance.DestroyBanner();
        yield return new WaitForEndOfFrame();
        AdsManager.Instance.RequestBanner(GoogleMobileAds.Api.AdPosition.Top);
    }
}
