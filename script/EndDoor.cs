using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    private GameObject playerLine;
    private bool isDoorOpen = false;
    private Vector3 positionDoorOpen;
    private Vector3 positionDoorClose;

    void Start()
    {
        playerLine = GameObject.FindGameObjectWithTag("PlayerLine");
        positionDoorClose = transform.position;
        positionDoorOpen = transform.position + new Vector3(1.0f, 0f, 0f);
        Debug.Log("ADD CHECK FOR LEVEL PATH VALIDITY HERE!");
    }

    void Update()
    {
        CheckIfDoorOpened();
    }

    public void CheckIfDoorOpened()
    {
        bool tmpIsOpen = true;

        if (!playerLine.GetComponent<LineManager>().isLineValid)
        {
            tmpIsOpen = false;
        }
        //Debug.Log("ADD CHECK FOR LEVEL PATH VALIDITY HERE!");
        if (tmpIsOpen && !isDoorOpen)
        {
            DoorAnimation(true);
            isDoorOpen = true;
        } 
        else if (!tmpIsOpen && isDoorOpen)
        {
            DoorAnimation(false);
            isDoorOpen = false;
        }
    }


    public void DoorAnimation(bool open){
        Vector3 newDoorPosition = open ? positionDoorOpen : positionDoorClose;
        {
            iTween.MoveTo(
                gameObject, 
                iTween.Hash(
                    "position", newDoorPosition, 
                    "time", 2, 
                    "easetype", iTween.EaseType.easeInOutQuad));
        }
    }
}
