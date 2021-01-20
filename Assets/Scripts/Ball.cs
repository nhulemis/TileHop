using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{

    int indexPlatform;

    Vector3 zeroPos;

    float distanceNextPlatform;

    bool isDrag;
    float offsetZCoordinate;
    Vector3 offsetPosition;

    [SerializeField]
    TextMeshProUGUI text;
    int score = 0;

    float T;
    // Start is called before the first frame update
    void Start()
    {
        indexPlatform = 0;
        zeroPos = this.transform.position;
        distanceNextPlatform =Vector3.Distance (this.transform.position, GameManager.Instance.ListPlaform[indexPlatform].transform.position);
        Debug.Log(distanceNextPlatform);
        T = 0;

        text.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            return;
        }
        // y(t) = Asin(ωt+φ)
        // A = distanceNextPlatform  // 
        // φ = 0
        // ω = 2PI/T  // T = 2A
        // T =?  =>>  s =distanceNextPlatform , v = BeatDetect.Instance._bpm / 10
        // S = VT => T = (S/V)* 2 

        float V = BeatDetect.Instance._speedMove;
        float A = distanceNextPlatform;

        float y = A * Mathf.Sin(Mathf.PI * V  * T / A);
        T += Time.deltaTime;
        var newPos = zeroPos;

        Touch touch = new Touch();

#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began )
            {
                isDrag = true;
                offsetZCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
                offsetPosition = gameObject.transform.position - GetInputCoordinate(touch);
            }
            else if (touch.phase == TouchPhase.Ended )
            {
                isDrag = false;
            }
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;
            offsetZCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
            offsetPosition = gameObject.transform.position - GetInputCoordinate();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }
#endif


        if (isDrag)
        {
            newPos.x = (offsetPosition + GetInputCoordinate(touch)).x;
        }

        newPos.y += y;

        transform.position = newPos;

    }

    private Vector3 GetInputCoordinate(Touch touch = new Touch())
    {
#if UNITY_ANDROID || UNITY_IOS 
        Vector3 posInput = touch.position;
#else
        var posInput = Input.mousePosition;
#endif
        posInput.z = offsetZCoordinate;

        return Camera.main.ScreenToWorldPoint(posInput);
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        indexPlatform++;
        distanceNextPlatform = Vector3.Distance(this.transform.position, GameManager.Instance.ListPlaform[indexPlatform].transform.position);
        T = 0;
        score++;
        text.text = score.ToString();
    }
}
