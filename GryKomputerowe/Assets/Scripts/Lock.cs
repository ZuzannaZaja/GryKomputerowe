using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public Transform player;
    public GameObject lockCanvas;
    private RotateView rotateView;
    private bool isViewed;
    private Renderer rend;
    public Vector3 startingPosition;
    public Quaternion startingRotation;
    private Vector3 startingLocalScale;
    public Number[] numbers;
    public string displayText;
    private bool firstNumber;
    private bool secondNumber;
    private bool thirdNumber;
    private string numbersEntered;
    public GameObject input;
    private bool wasCorrect;
    public GameObject door;
    private AudioSource audioSource;
    public AudioSource clickSource;
    public AudioSource correctSource;

    // Start is called before the first frame update
    void Start()
    {
        rotateView = Camera.main.GetComponent<RotateView>();
        isViewed = false;
        firstNumber = false;
        secondNumber = false;
        thirdNumber = false;
        rend = GetComponent<Renderer>();
        numbers = transform.GetComponentsInChildren<Number>();
        wasCorrect = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= 3f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.transform.GetComponent<Number>() != null && Input.GetMouseButtonDown(0) && isViewed)
                {
                    clickSource.Play();
                    if (!wasCorrect)
                    {
                        if (!thirdNumber && secondNumber && firstNumber)
                        {
                            Number num = hit.collider.GetComponent<Number>();
                            displayText = numbersEntered + num.whatNumber;
                            numbersEntered += num.whatNumber;
                            thirdNumber = true;
                            input.transform.GetComponent<TextMesh>().text = displayText;

                        }
                        if (!secondNumber && firstNumber)
                        {
                            Number num = hit.collider.GetComponent<Number>();
                            displayText = numbersEntered + num.whatNumber + "0";
                            numbersEntered += num.whatNumber;
                            secondNumber = true;
                            input.transform.GetComponent<TextMesh>().text = displayText;
                        }
                        if (!firstNumber)
                        {
                            Number num = hit.collider.GetComponent<Number>();
                            displayText = num.whatNumber + "00";
                            numbersEntered = num.whatNumber;
                            firstNumber = true;
                            input.transform.GetComponent<TextMesh>().text = displayText;
                        }
                        if (firstNumber && secondNumber && thirdNumber)
                            Check();
                    }
                }
                if (hit.collider.GetComponent<Lock>() == null && Input.GetMouseButtonDown(0) && isViewed && hit.collider.GetComponent<Number>() == null)
                {
                    transform.position = startingPosition;
                    transform.rotation = startingRotation;
                    transform.localScale = startingLocalScale;
                    rotateView.isScrollOpened = false;
                    isViewed = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    if (!wasCorrect)
                    {
                        Reset();
                    }
                    if (wasCorrect)
                    {
                        audioSource.Play();
                        door.GetComponent<Door>().OpenDoor();
                    }
                }
                if (hit.collider.GetComponent<Lock>() != null || hit.collider.GetComponent<Number>() != null)
                {
                    if (Input.GetMouseButtonDown(0) && !isViewed)
                    {
                        startingLocalScale = transform.localScale;
                        startingPosition = transform.position;
                        startingRotation = transform.rotation;
                        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up); 
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        transform.position += Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 0.5f)) - rend.bounds.center;
                        transform.localScale *= 4f;
                        rotateView.isScrollOpened = true;
                        isViewed = true;
                    }
                }

            }
        }
        
    }

    void Reset()
    {
        displayText = "000";
        input.transform.GetComponent<TextMesh>().text = displayText;
        numbersEntered = "";
        firstNumber = false;
        secondNumber = false;
        thirdNumber = false;
    }

    void Check()
    {
        if (displayText.Equals("392"))
        {
            input.transform.GetComponent<TextMesh>().color = Color.green;
            correctSource.Play();
            wasCorrect = true;
        }
        else
            Reset();
    }
}
