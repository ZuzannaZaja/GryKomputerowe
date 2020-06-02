using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 5f;
    private Vector3 moveDirection = Vector3.zero;
    private Viewable lastViewed;
    private RotateView rotateView;
    public float gravity = -9.81f; 
    Vector3 velocity;
    public GameObject[] letters;
    Inventory inventory;
    public GameObject dotCursor;
    public GameObject handCursor;
    public GameObject textReset;
    public GameObject textDrink;
    public GameObject bag;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotateView = GetComponentInChildren<RotateView>();
        inventory = Inventory.instance;
    }

    private void FixedUpdate()
    {
        if (!rotateView.isFocused)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            characterController.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

        }
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            Viewable viewable = hit.collider.GetComponent<Viewable>();
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (bag.GetComponent<Bag>().isHit)
            {
                dotCursor.SetActive(false);
                handCursor.SetActive(true);
            }

            if (viewable != null)
            {
                float distanceViewable = Vector3.Distance(transform.position, viewable.transform.position);
                if(distanceViewable <= 3f)
                {
                    dotCursor.SetActive(false);
                    handCursor.SetActive(true);
                }
            }

            if (interactable != null)
            {
                float distanceInteractable = Vector3.Distance(transform.position, interactable.transform.position);
                if (distanceInteractable <= interactable.radius)
                {
                    dotCursor.SetActive(false);
                    handCursor.SetActive(true);
                }
            }

            if (viewable == null && interactable == null && !bag.GetComponent<Bag>().isHit)
            {
                handCursor.SetActive(false);
                dotCursor.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0) && viewable != null){
                lastViewed = viewable;
            }

            if (Input.GetMouseButtonDown(0) && interactable != null)
            {
                interactable.ifInteract();
            }
        
            if(Input.GetMouseButtonDown(0) && lastViewed != null) 
            {
                float distance = Vector3.Distance(transform.position, lastViewed.transform.position);
                if (distance <= 3f)
                {
                    if (!lastViewed.isSelected && hit.collider.GetComponent<Viewable>() != null)
                    {
                        lastViewed.Select();
                        rotateView.isFocused = true;
                    }
                    else
                    {
                        lastViewed.Deselect();
                        rotateView.isFocused = false;
                        lastViewed = null;
                    }
                } 
            }
        }

        if(rotateView.isFocused && !rotateView.isScrollOpened)
        {
            float mouseX = Input.GetAxis("Mouse X") * 700f * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * 700f * Time.deltaTime;
            lastViewed.transform.Rotate(new Vector3(0, 0, mouseX));
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Item item in inventory.items)
            {
                if(item.name.Equals("Potion"))
                {
                    foreach (GameObject letter in letters)
                    {
                        letter.SetActive(true);
                    }
                    inventory.Remove(item);
                    textDrink.SetActive(false);
                    textReset.SetActive(true);
                    break;
                }
            }

        }
    }

}


