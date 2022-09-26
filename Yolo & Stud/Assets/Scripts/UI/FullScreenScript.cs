using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FullScreenScript : MonoBehaviour
{
    /* public void FScreen( bool is_fullScreeen)
     {
         Screen.fullScreen = is_fullScreeen;

         Debug.Log("FullScreen is " + is_fullScreeen);
     }*/

    void Update()
    {
        Screen.fullScreen = true;
    }
}
