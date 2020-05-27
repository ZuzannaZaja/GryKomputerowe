using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollOpen : MonoBehaviour
{
    public GameObject scrollOpened;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= 3f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.GetComponent<ScrollOpen>() != null && Input.GetMouseButtonDown(0))
                {
                    if (distance <= 3f)
                    {
                        scrollOpened.SetActive(true);
                    }
                }
            }
        }
    }
}

