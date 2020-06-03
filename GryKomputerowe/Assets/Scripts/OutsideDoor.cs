using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutsideDoor : MonoBehaviour
{
    public GameObject player;
    private bool shouldCheck = false;
    private Vector3 startingCamera;
    private bool shouldStop = false;
    public CanvasGroup endingCanvas;
    public GameObject inventoryCanvas;
    public GameObject dotCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 3f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.GetComponent<OutsideDoor>() != null && Input.GetMouseButtonDown(0))
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 30f, transform.localEulerAngles.z);
                    shouldCheck = true;
                }
            }
        }
        if(shouldCheck)
        {
            if(player.transform.position.x >= transform.position.x + 0.5f)
            {
                startingCamera = Camera.main.transform.localEulerAngles;
                Camera.main.GetComponent<RotateView>().isFocused = true;
                Camera.main.GetComponent<RotateView>().isScrollOpened = true;
                Vector3 to = new Vector3(-10, 0, 0);

                if(Camera.main.transform.eulerAngles.x >= 180f)
                {
                    if (!shouldStop)
                    {
                        Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, Quaternion.Euler(-6f, 90f, Camera.main.transform.eulerAngles.z), 10*Time.deltaTime);
                    }

                }

                if(Camera.main.transform.eulerAngles.x >= 0f && Camera.main.transform.eulerAngles.x < 180f)
                {
                    if (!shouldStop)
                    {
                        Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.rotation.eulerAngles, new Vector3(-6f, 90f, Camera.main.transform.eulerAngles.z), Time.deltaTime);

                    }

                }
                if(Camera.main.transform.eulerAngles.x == 354f)
                {
                    shouldStop = true;
                    StartCoroutine(Fade());
                }
            }
        }
    }
    IEnumerator Fade()
    {
        inventoryCanvas.SetActive(false);
        dotCanvas.SetActive(false);
        yield return new WaitForSeconds(1.5f); 
        endingCanvas.alpha = endingCanvas.alpha + Time.deltaTime;
        if (endingCanvas.alpha >= 1f)
        {
            endingCanvas.alpha = 1f;
        }
        yield return new WaitForSeconds(1f);
        player.GetComponent<SceneSwitcher>().BackMenu();
    }
    

}
