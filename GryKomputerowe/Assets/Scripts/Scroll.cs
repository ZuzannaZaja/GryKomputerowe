using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private Renderer rend;
    private RotateView rotateView;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        rend = GetComponent<Renderer>();
        rotateView = Camera.main.GetComponent<RotateView>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 0.5f)) - rend.bounds.center;
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
        rotateView.isFocused = true;
        rotateView.isScrollOpened = true;
        if(Input.GetMouseButtonDown(0))
        {
            rotateView.isFocused = false;
            rotateView.isScrollOpened = false;
            gameObject.SetActive(false);
        }
    }
}
