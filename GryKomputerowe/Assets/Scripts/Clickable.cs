using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public int order;
    public float radius = 4f;
    public Transform player;
    
    private ClickedManager _clickedManager;
    public GameObject letter;

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
        letter.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

}
