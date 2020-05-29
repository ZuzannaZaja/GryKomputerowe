using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickedManager : MonoBehaviour
{
    public int[] correct_order = { 1, 2, 3, 4, 5 };
    public int[] input_order = { 0, 0, 0, 0, 0 };
    //public GameObject doorLeft;
    //public GameObject doorRight;
    public GameObject door;

    private int counter = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Clickable clickable = hit.collider.GetComponent<Clickable>();
                if (clickable != null)
                {
                    clickable.ifClickable();
                    counter++;
                    input_order[counter - 1] = clickable.order;

                    if (counter == 5 && input_order.SequenceEqual(correct_order))
                    {
                        //doorLeft.transform.localEulerAngles = new Vector3(doorLeft.transform.localEulerAngles.x, doorLeft.transform.localEulerAngles.y + 90f, doorLeft.transform.localEulerAngles.z);
                        //doorRight.transform.localEulerAngles = new Vector3(doorRight.transform.localEulerAngles.x, doorRight.transform.localEulerAngles.y - 90f, doorRight.transform.localEulerAngles.z);
                        door.transform.localEulerAngles = new Vector3(door.transform.localEulerAngles.x, door.transform.localEulerAngles.y + 90f, door.transform.localEulerAngles.z);
                        counter = 0;
                        input_order = new int[] { 0, 0, 0, 0, 0 };
                    }
                    else if (counter == 5 && !input_order.SequenceEqual(correct_order))
                    {
                        counter = 0;
                        input_order = new int[] { 0, 0, 0, 0, 0 };
                    }
                }
            }
        }
    }
}
