using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    Inventory inventory;
    public Transform player;
    private int ingredients;
    public Item potion;
    public GameObject textUI;
    public GameObject textUIComeBack;
    public float timeStart = 5;
    public GameObject chestOpened;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        ingredients = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if(inventory.items.Count == 5 && chestOpened.activeSelf)
        {
            if (distance <= 3f)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.GetComponent<Cauldron>() != null && Input.GetMouseButtonDown(0) && distance <= 3f)
                    {
                        foreach (Item item in inventory.items)
                        {
                            if (item.name.Equals("Olive Oil"))
                            {
                                ingredients += 1;
                            }
                            if (item.name.Equals("Beat"))
                            {
                                ingredients += 1;
                            }
                            if (item.name.Equals("Feather"))
                            {
                                ingredients += 1;
                            }
                            if (item.name.Equals("Flask"))
                            {
                                ingredients += 1;
                            }
                            if (item.name.Equals("Ivy"))
                            {
                                ingredients += 1;
                            }
                        }
                        if (ingredients == 5)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                inventory.Remove(inventory.items[0]);
                            }
                            inventory.Add(potion);
                            ActivateText();
                            ingredients = 0;
                        }
                    }
                }
            }
        }
        else if (chestOpened.activeSelf)
        {
            if (distance <= 3f)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.GetComponent<Cauldron>() != null && Input.GetMouseButtonDown(0) && distance <= 3f)
                    {
                        textUIComeBack.SetActive(true);
                    }
                }
            }
        }
        
        if (textUI.activeSelf)
        {
            timeStart -= Time.deltaTime;
            if (timeStart <= 0)
            {
                textUI.SetActive(false);
                timeStart = 5;
            }
        }
        if (textUIComeBack.activeSelf)
        {
            timeStart -= Time.deltaTime;
            if (timeStart <= 0)
            {
                textUIComeBack.SetActive(false);
                timeStart = 5;
            }
        }
    }
    public void ActivateText()
    {
        textUI.SetActive(true);
    }
}
