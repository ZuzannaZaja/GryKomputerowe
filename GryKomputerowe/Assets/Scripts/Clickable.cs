using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public int order;
    public float radius = 3f;
    public Transform player;
    
    private ClickedManager _clickedManager;

    private void Start()
    {
        _clickedManager = GetComponent<ClickedManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ifClickable()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= radius)
        {
            Click();
        }
    }

    public void Click()
    {
        //TODO: can be changing material when clicked or sth
    }

}
