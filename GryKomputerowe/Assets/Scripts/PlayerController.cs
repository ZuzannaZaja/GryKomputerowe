using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 5f;
    private Vector3 moveDirection = Vector3.zero;
    public Camera camera;
    private Viewable lastViewed;
    private RotateView rotateView;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotateView = GetComponentInChildren<RotateView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!rotateView.isFocused)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            characterController.Move(move * speed * Time.deltaTime);
        }

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            Viewable viewable = hit.collider.GetComponent<Viewable>();
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if(viewable != null){
                lastViewed = viewable;
            }

            if (Input.GetMouseButtonDown(0) && interactable != null)
            {
                interactable.ifInteract();
            }
        }
        if(Input.GetMouseButtonDown(0) && lastViewed != null)
        {
            if(!lastViewed.isSelected && hit.collider.GetComponent<Viewable>() != null)
            {
                lastViewed.Select(); 
                rotateView.isFocused = true;
            } else
            {
                lastViewed.Deselect();
                rotateView.isFocused = false;
                lastViewed = null;               
            }
        }

        if(rotateView.isFocused)
        {
            float mouseX = Input.GetAxis("Mouse X") * 700f * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * 700f * Time.deltaTime;
            lastViewed.transform.Rotate(new Vector3(0, 0, mouseX));
        }
    }

}


