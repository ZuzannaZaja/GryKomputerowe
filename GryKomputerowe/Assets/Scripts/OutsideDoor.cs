using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDoor : MonoBehaviour
{
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
                if (hit.collider.GetComponent<OutsideDoor>() != null && Input.GetMouseButtonDown(0))
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 30f, transform.localEulerAngles.z);
                }
            }
        }
        if(transform.localEulerAngles.y == 30f)
        {
            if(player.position.x >= 17)
            {
                //TODO
            }
        }
    }
}
