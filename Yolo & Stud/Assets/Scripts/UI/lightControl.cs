using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class lightControl : MonoBehaviour
{
    //Light Intensity.
    [Header("Light Intensity")]
    public Slider intensitySlider;
    public Light lightIntensity;

    //Colour Variables. 
    [Header("Light Colour")]
    public Slider colourSlider; 
    public Light lightColour; 
    void Start()
    {
        
    }

   
    void Update()
    {
        lightIntensity.intensity  = intensitySlider.value;
        
        
       
        
    }
}
