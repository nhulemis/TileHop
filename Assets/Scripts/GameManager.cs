using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameStarted;
    public GameObject _prefabCube;
    public Transform _spawnPoint;

    private Queue<GameObject> _objectPools;

    public const int MIN_OBJECT = 20;
    public const int MAX_OBJECT = 100;
    [Range(MIN_OBJECT, MAX_OBJECT)]
    public int _maxObjectPool = 20;

    [SerializeField]
    Transform player;

    Vector3 offsetPlatformPos;

    public List<GameObject> ListPlaform { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }else
        {
            Instance = this;
        }

        ListPlaform = new List<GameObject>();
        _objectPools = new Queue<GameObject>();
        for (int i = 0; i < _maxObjectPool; i++)
        {
            var cube = Instantiate(_prefabCube, _spawnPoint);
            cube.SetActive(false);
            _objectPools.Enqueue(cube);
        }

        offsetPlatformPos = player.transform.position;
        BeatDetect beat = BeatDetect.Instance;
        for (int i = 0; i < 8; i++)
        {
            offsetPlatformPos.z += beat._speedMove * beat._beat;
            GameObject game = Instantiate(_prefabCube);
            game.transform.position = offsetPlatformPos;
            ListPlaform.Add(game);
        }
        _spawnPoint.position = offsetPlatformPos;
    }

    // Start is called before the first frame update
    void Start()
    {

        IsGameStarted = false;
    }

    public void StartGame()
    {
        if (IsGameStarted)
        {
            return;
        }

        BeatDetect.Instance.PlayMusic();
        IsGameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsGameStarted)
        {
            StartGame();
        }

        if (!IsGameStarted )
        {
            return;
        }

        if (BeatDetect.Instance.IsBeat)
        {
            NewCube();
        }

        if (BeatDetect.Instance.IsAudioLoudness)
        {
            //NewDeathCube();
        }
    }

    void NewCube()
    {
        var cube = _objectPools.Dequeue();
        cube.transform.position = _spawnPoint.position;
        cube.SetActive(true);
        ListPlaform.Add(cube);
        _objectPools.Enqueue(cube);
    }

    void NewDeathCube()
    {
        var cube = _objectPools.Dequeue();
        cube.transform.position = _spawnPoint.position;
        cube.SetActive(true);

        _objectPools.Enqueue(cube);
    }

}
