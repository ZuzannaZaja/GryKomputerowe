using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpstairsDoor : MonoBehaviour
{
    private Vector3 startingRotation;
    public Transform player;
    public AudioSource lockedSource;
    // Start is called before the first frame update
    void Start()
    {
        startingRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= 3f && transform.localEulerAngles == startingRotation)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.transform.GetComponent<UpstairsDoor>() != null && Input.GetMouseButtonDown(0))
                {
                    lockedSource.Play();
                }
            }
        }

    }
}
