using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    public float waitTime = 16.5f;
  
    void Start()
    {
        StartCoroutine(waitForIntro());
        
    }
    IEnumerator waitForIntro() 
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);  
    }
}
