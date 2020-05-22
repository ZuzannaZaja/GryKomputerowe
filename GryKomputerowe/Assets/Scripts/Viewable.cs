using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewable : MonoBehaviour
{
    public Vector3 startingPosition;
    public Quaternion startingRotation;
    public bool isSelected = false;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }
  
    public void Select()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        transform.position += Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane + 0.5f))-rend.bounds.center;
        isSelected = true;
    }

    public void Deselect()
    {
        transform.position = startingPosition;
        transform.rotation = startingRotation;
        isSelected = false;
    }

}
