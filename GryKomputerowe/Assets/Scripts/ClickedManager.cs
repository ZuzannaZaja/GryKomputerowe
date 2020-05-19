using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickedManager : MonoBehaviour
{
    public int[] correct_order = {1, 2, 3};
    public int[] input_order = {0, 0, 0};
    public Camera camera;

    private int counter = 0; 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Clickable clickable = hit.collider.GetComponent<Clickable>();
                if (clickable != null)
                {
                    clickable.ifClickable();
                    counter++;
                    input_order[counter - 1] = clickable.order;

                    if (counter == 3 && input_order.SequenceEqual(correct_order))
                    {
                        Debug.Log("You won!");
                    }
                    else if (counter == 3 && !input_order.SequenceEqual(correct_order))
                    {
                        counter = 0;
                        input_order = new int[] {0, 0, 0};
                    }
                }
            }
        }
    }
}
