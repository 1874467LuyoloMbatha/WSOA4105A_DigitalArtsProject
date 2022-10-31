using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSwitch : MonoBehaviour
{
    public int selectedObject = 0; 
    // Start is called before the first frame update
    void Start()
    {
        SelectObject(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        int previouSselectedObject = selectedObject;

        //Input// 
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //scroll up
        {
            if (selectedObject >= transform.childCount - 1)
                selectedObject = 0;
            else
               selectedObject++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) //scroll down
        {
            if (selectedObject <= 0)
                selectedObject = transform. childCount - 1;
            else
                selectedObject --;
        }

        if (previouSselectedObject != selectedObject) 
        {
            SelectObject();
        }
    }

    void SelectObject() 
    {
        int i = 0; //looping  of objects 
        foreach (Transform objetctToBSwitch in transform)
        {
            if (i == selectedObject)// enable weapon switch 
                objetctToBSwitch.gameObject.SetActive(true);
            else
                objetctToBSwitch.gameObject.SetActive(false);

            i++; 
        }
    }
}
