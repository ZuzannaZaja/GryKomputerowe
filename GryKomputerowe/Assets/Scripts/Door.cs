using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform player;
    private bool shouldChangeRotation;
    private Vector3 openedDoor;
    private Vector3 closedDoor;
    private bool wasOpened;

    private void Start()
    {
        shouldChangeRotation = false;
        openedDoor = new Vector3(-90f, 0f, 0f);
        closedDoor = new Vector3(-90f, 90f, 0f);

    }
    void Update()
    {
        if (!wasOpened)
        {
            if (player.position.x > transform.position.x)
            {
                shouldChangeRotation = true;
            }

            if (shouldChangeRotation)
            {
                transform.localEulerAngles = closedDoor;
                shouldChangeRotation = false;
            }
        }

    }
    public void OpenDoor()
    {
        transform.localEulerAngles = openedDoor;
        wasOpened = true;
    }
}
