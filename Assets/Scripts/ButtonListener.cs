using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListener : MonoBehaviour
{
   

    public void OnButtonClick()
    {
        Debug.Log("Nhu Le Coding");
        GameManager.Instance.StartGame();
    }

    public void OnSelectChanged(Vector2 vector)
    {
        //Debug.Log("ABC: " + vector.x + " - " + vector.y);
    }

    public void OnItemSelected(GameObject go)
    {
        Debug.Log("Name: " + go.name);

    }
}
