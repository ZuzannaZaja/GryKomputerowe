using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Viewable viewable;
    private GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        viewable = GetComponent<Viewable>();
        key = gameObject.transform.GetChild(0).gameObject;     
    }

    // Update is called once per frame
    void Update()
    {
        if(!viewable.isSelected && key != null)
        {
            key.SetActive(false);
        }
        if(viewable.isSelected && key != null)
        {
            key.SetActive(true);
        }
    }
}
