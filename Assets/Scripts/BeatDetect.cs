using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDetect : MonoBehaviour
{
    public static BeatDetect Instance { get; private set; }

    // ================ Audio Loudness detect =============
    public float _stepUpdate = 0.1f;
    private float _currentTimeUpdate = 0f;
    public float _loudness;
    private float[] _sampleLoudness;
    public float _loudnessFactor = 0.3f;
    private const int MAX_SAMPLE = 1024;
    public AudioSource _audioSource;
    public bool IsAudioLoudness { get; set; }

    public int _speedFactor;

    //================= BeatDetect BPM ===================
    public float _bpm;
    public float _speedMove;

    float _beatTimer;
    public float _beat { get; set; }
    int _beatCounter;
    public bool IsBeat { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _sampleLoudness = new float[MAX_SAMPLE];

        _speedMove = _bpm / _speedFactor;
        _beat = 60f / _bpm;
    }

    // Start is called before the first frame update
    void Start()
    {
        _bpm = InfoPopup.Instance.bpm;
        _audioSource.clip = InfoPopup.Instance.audio;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            return;
        }

        Detect();
        AudioDetect();
    }

    void Detect()
    {
        _speedMove = _bpm / _speedFactor;
        IsBeat = false;
        _beat = 60f / _bpm; // 120 b / m  60 / 120 = 0s
        _beatTimer += Time.deltaTime; // 0.01
        if (_beatTimer >= _beat)
        {
            _beatTimer -= _beat;
            IsBeat = true;
            _beatCounter++;
            //Debug.Log("Beat");
        }

    }

    void AudioDetect()
    {
        _currentTimeUpdate += Time.deltaTime;
        IsAudioLoudness = false;

        if (_currentTimeUpdate > _stepUpdate)
        {
            _currentTimeUpdate = 0;
            _loudness = 0;

            _audioSource.clip.GetData(_sampleLoudness, _audioSource.timeSamples);

            foreach (var item in _sampleLoudness)
            {
                _loudness += Mathf.Abs(item);
            }

            _loudness /= MAX_SAMPLE;

            if (_loudness > _loudnessFactor)
            {
                IsAudioLoudness = true;
            }
        }
    }

    public void PlayMusic()
    {
        _audioSource.Play();
    }
}
