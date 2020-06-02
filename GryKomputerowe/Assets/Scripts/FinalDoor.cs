using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    Inventory inventory;
    public Transform player;
    public float angle;
    public GameObject secondHalf;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].name.Equals("Golden Key"))
            {
                if (distance <= 3f)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        if (hit.collider.GetComponent<FinalDoor>() != null && Input.GetMouseButtonDown(0))
                        {
                            inventory.Remove(inventory.items[i]);
                            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + angle, transform.localEulerAngles.z);
                            secondHalf.transform.localEulerAngles = new Vector3(secondHalf.transform.localEulerAngles.x, secondHalf.transform.localEulerAngles.y - angle, secondHalf.transform.localEulerAngles.z);
                        }
                    }
                }
            }
        }
    }
}
