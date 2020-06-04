using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
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
    public GameObject pauseCanvas;
    public Lock padlock;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 1f;
    public GameObject PPV;

    bool isPaused;
    bool isGrounded;

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

        }
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            Viewable viewable = hit.collider.GetComponent<Viewable>();
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (hit.collider.GetComponent<CursorChange>() != null)
            {
                float distance = Vector3.Distance(transform.position, hit.collider.GetComponent<CursorChange>().transform.position);
                if(distance <= 3f)
                {
                    dotCursor.SetActive(false);
                    handCursor.SetActive(true);
                }
            }

            if (hit.collider.GetComponent<CursorChange>() == null || rotateView.isFocused)
            {
                if(hit.collider.GetComponent<Interactable>() != null)
                {
                    dotCursor.SetActive(false);
                    handCursor.SetActive(true);
                }
                else
                {
                    dotCursor.SetActive(true);
                    handCursor.SetActive(false);
                }
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
                    PPV.SetActive(true);
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1.0f;
                pauseCanvas.SetActive(false);
                dotCursor.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0.0f;
                pauseCanvas.SetActive(true);
                dotCursor.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPaused = true;
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseCanvas.SetActive(false);
        dotCursor.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }
}


