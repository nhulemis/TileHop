using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnUIListenner : MonoBehaviour
{
    public void OnButtonPauseClick() 
    {
        Debug.Log("ABC : " + this.gameObject.name);
    }


    public void OnItemChecked(MusicInfo go)
    {
        var info = InfoPopup.Instance;
        info.audio = go.audio;
        info.bpm = go.bpm;
        info.songName = go.songName;
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Main");
        AdsManager.Instance.HideBanner();
    }
}
