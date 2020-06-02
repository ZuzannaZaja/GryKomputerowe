using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public GameObject dotCursor;
    public GameObject handCursor;
    public GameObject movedPillow;
    public Transform player;
    public bool isHit;

    private void Start()
    {
        isHit = false;
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
                if (hit.collider.GetComponent<Bag>() != null)
                {
                    isHit = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        isHit = false;
                        gameObject.SetActive(false);
                        movedPillow.SetActive(true);
                    }
                }
                if (hit.collider.GetComponent<Bag>() == null)
                {
                    isHit = false;
                }
            }
        }
    }
}
