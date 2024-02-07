using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    private GameObject playerLine;
    private bool isDoorOpen = false;
    private Vector3 positionDoorOpen;
    private Vector3 positionDoorClose;
    private GridLab gridLab;

    void Start()
    {
        playerLine = GameObject.FindGameObjectWithTag("PlayerLine");
        positionDoorClose = transform.position;
        positionDoorOpen = transform.position + new Vector3(1.0f, 0f, 0f);
    }

    public void AddGridLab(GridLab gridLab)
    {
        this.gridLab = gridLab;
    }

    void Update()
    {
        CheckIfDoorOpened();
    }

    public void CheckIfDoorOpened()
    {
        bool tmpIsOpen = true;
        bool isPlayerCloseToDoor = true; //Vector3.Distance(Camera.main.transform.position, transform.position) < 1.0f;

        if (!playerLine.GetComponent<LineManager>().isLineValid)
        {
            tmpIsOpen = false;
        }
        if (gridLab != null) {
            isPlayerCloseToDoor = gridLab.IsPlayerAtEnd();
            if (!gridLab.CheckPathValididy())
            {
                tmpIsOpen = false;
            }
        }
        if (tmpIsOpen && !isDoorOpen && isPlayerCloseToDoor)
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
