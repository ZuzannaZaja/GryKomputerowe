using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item item;
    public float radius = 3f;
    public Transform player;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioManager>().GetComponent<AudioSource>();
    }
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
        audioSource.Play();
        Destroy(gameObject);
        Inventory.instance.Add(item);
    }
}
