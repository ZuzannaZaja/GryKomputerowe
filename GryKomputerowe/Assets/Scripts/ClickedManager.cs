using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickedManager : MonoBehaviour
{
    public int[] correct_order = { 1, 2, 3, 4, 5 };
    public int[] input_order = { 0, 0, 0, 0, 0 };
    public GameObject door;
    public PlayerController player;
    public GameObject textReset;
    public AudioSource audioSource;

    private int counter = 0;
    // Update is called once per frame

    void Update()
    {
        ManageOrder();
        Resert();
    }

    public void Resert()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            input_order = new int[] { 0, 0, 0, 0, 0 };
            counter = 0;
            for (int i = 0; i < 12; i++)
            {
                player.letters[i].GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }

    public void ManageOrder()
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
                        player.letters[6].GetComponent<MeshRenderer>().material.color = Color.green;
                        player.letters[0].GetComponent<MeshRenderer>().material.color = Color.green;
                        player.letters[7].GetComponent<MeshRenderer>().material.color = Color.green;
                        player.letters[10].GetComponent<MeshRenderer>().material.color = Color.green;
                        player.letters[5].GetComponent<MeshRenderer>().material.color = Color.green;

                        textReset.SetActive(false);
                        audioSource.Play();
                        door.transform.localEulerAngles = new Vector3(door.transform.localEulerAngles.x, door.transform.localEulerAngles.y + 90f, door.transform.localEulerAngles.z);
                        counter = 0;
                        input_order = new int[] { 0, 0, 0, 0, 0 };
                    }
                    else if (counter == 5 && !input_order.SequenceEqual(correct_order))
                    {
                        counter = 0;
                        input_order = new int[] { 0, 0, 0, 0, 0 };
                        for (int i = 0; i < 12; i++)
                        {
                            player.letters[i].GetComponent<MeshRenderer>().material.color = Color.white;
                        }
                    }
                }
            }
        }
    }
}
