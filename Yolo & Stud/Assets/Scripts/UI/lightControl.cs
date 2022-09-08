using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class lightControl : MonoBehaviour
{
   /* //Light Intensity.
    [Header("Light Intensity")]
    public Slider intensitySlider;
    [SerializeField]
    public Light lightIntensity;*/

    //Colour Variables. 
    [Header("Light Colour")]
    public Slider[] RGBSliders;
    public Light[] Lights;
    //public Slider colourSlider; 
    //public Light lightColour; 
   

   
    void Update()
    {
        //lightIntensity.intensity  = intensitySlider.value;

        var RSlider = RGBSliders[0].value;
        var GSlider = RGBSliders[1].value;
        var BSlider = RGBSliders[2].value;

        //First create a value
        var newColorRGB = new Color(RSlider, GSlider, BSlider);
        //After change the colors
        Lights[0].color = newColorRGB;
        Lights[1].color = newColorRGB;
        Lights[2].color = newColorRGB;
        Lights[3].color = newColorRGB;
        Lights[4].color = newColorRGB;
        Lights[5].color = newColorRGB;

    }
}
