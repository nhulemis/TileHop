using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Riser : MonoBehaviour
{
    Vector3 targetPos;

    [SerializeField]
    GameObject Fx;

    private void Awake()
    {
        Assert.IsNotNull(Fx);
    }

    // Start is called before the first frame update
    void Start()
    {
        RespawnCube();
        Fx.SetActive(false);

    }

    public void RespawnCube()
    {
        //this.gameObject.SetActive(true);
        targetPos = this.transform.position + new Vector3(0, 0, -70);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            return;
        }

        if (this.gameObject.activeSelf)
        {
            float step = BeatDetect.Instance._speedMove * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetPos , step);

            float distance = Vector3.Distance(transform.position, targetPos);
            if (distance <= 1f)
            {
                var pos = transform.position;
                pos.y += 0.2f;
                transform.position = pos;
                Fx.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Fx.SetActive(true);

       var pos =  transform.position;
        pos.y -= 0.2f;
        transform.position = pos;
    }
}
