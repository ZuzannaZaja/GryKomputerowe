using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDoor : MonoBehaviour
{
    public GameObject player;
    private bool shouldCheck = false;
    private Vector3 startingCamera;
    private bool shouldStop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 3f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.GetComponent<OutsideDoor>() != null && Input.GetMouseButtonDown(0))
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 30f, transform.localEulerAngles.z);
                    shouldCheck = true;
                }
            }
        }
        if(shouldCheck)
        {
            if(player.transform.position.x >= transform.position.x + 1)
            {
                startingCamera = Camera.main.transform.localEulerAngles;
                Camera.main.GetComponent<RotateView>().isFocused = true;
                Camera.main.GetComponent<RotateView>().isScrollOpened = true;
                Vector3 to = new Vector3(-10, 0, 0);

                if (!shouldStop)
                {
                    Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.rotation.eulerAngles, new Vector3(-10f,Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z), Time.deltaTime);

                }
                if (Camera.main.transform.eulerAngles.x >= 350f)
                {
                    shouldStop = true;
                }
            }
        }
    }
}
