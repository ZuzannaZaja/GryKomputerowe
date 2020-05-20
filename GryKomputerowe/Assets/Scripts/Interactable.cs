using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item item;
    public float radius = 3f;
    public Transform player;

    public void ifInteract()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= radius)
        {
            PickUp();
        }
    }
    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        Destroy(gameObject);
        Inventory.instance.Add(item);
    }
}
