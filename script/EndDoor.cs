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

        if (!playerLine.GetComponent<LineManager>().isLineValid)
        {
            tmpIsOpen = false;
        }
        if (gridLab != null && !gridLab.CheckPathValididy())
        {
            tmpIsOpen = false;
        }
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
