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
    public AudioSource audioSource;
    private bool shouldPlay;

    private void Start()
    {
        shouldChangeRotation = false;
        wasOpened = false;
        shouldPlay = true;
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
                CloseDoor();
                shouldChangeRotation = false;
            }
        }

    }
    public void OpenDoor()
    {
        transform.localEulerAngles = openedDoor;
        wasOpened = true;
    }

    void CloseDoor()
    {
        transform.localEulerAngles = closedDoor;
        if(!audioSource.isPlaying && shouldPlay)
        {
            audioSource.Play();
            shouldPlay = false;
        }
    }
}
