using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Inventory inventory;
    public GameObject chestOpened;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        
        if(distance <= 3f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.GetComponent<Chest>() != null && Input.GetMouseButtonDown(0))
                {
                    foreach (Item item in inventory.items)
                    {
                        if(item.name.Equals("Rusty Key") && distance <= 3f)
                        {
                            chestOpened.SetActive(true);
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
            
        }
        
    }
}
