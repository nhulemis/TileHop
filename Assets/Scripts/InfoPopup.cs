using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPopup : MonoBehaviour
{
    public static InfoPopup Instance;

    public string songName;
    public float bpm;
    public AudioClip audio;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
