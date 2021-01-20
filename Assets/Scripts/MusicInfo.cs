using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicInfo : MonoBehaviour
{
    [SerializeField]
    public string songName;

    [SerializeField]
    public float bpm;

    [SerializeField]
    TextMeshProUGUI songNameText;
    [SerializeField]
    TextMeshProUGUI bpmText;
    [SerializeField]
    public AudioClip audio;

    // Start is called before the first frame update
    void Start()
    {
        songNameText.text = songName;
        bpmText.text = "bpm: " +bpm.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
