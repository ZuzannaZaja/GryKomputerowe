using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewable : MonoBehaviour
{
    public Vector3 startingPosition;
    public Quaternion startingRotation;
    public Vector3 localScaleHint;
    public bool isSelected = false;
    public bool isHint = false;
    Renderer rend;
    private RotateView rotateView;
    public float hintScale;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rotateView = Camera.main.GetComponent<RotateView>();
    }
  
    public void Select()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
        transform.position += Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane + 0.5f))-rend.bounds.center;
        isSelected = true;
        if(isHint)
        {
            localScaleHint = transform.localScale;
            transform.localScale *= hintScale;
            transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
            rotateView.isScrollOpened = true;
        }
    }

    public void Deselect()
    {
        transform.position = startingPosition;
        transform.rotation = startingRotation;
        isSelected = false;
        if (isHint)
        {
            transform.localScale = localScaleHint;
            rotateView.isScrollOpened = false;
        }
    }

}
