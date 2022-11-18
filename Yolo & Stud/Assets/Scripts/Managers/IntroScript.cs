using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [Header("Unity Handles")]
	[SerializeField] GameObject introPlayer;
	[SerializeField] GameObject greetingCard;
    [SerializeField] Text greetingText, playerName;

    [Header("Floats")]
    public float waitTime = 16.5f;
    public float secondWaitTime = 2.3f;

    [Header("Generic Elements")]
    [SerializeField] string greetingPlayerPrefs = "PlayerName";
    [SerializeField] string[] greetingProperties;
    void Start()
    {
        if(greetingCard != null)
            greetingCard.SetActive(false);

        StartCoroutine(waitForIntro());
        
    }
    IEnumerator waitForIntro() 
    {
        yield return new WaitForSeconds(waitTime);
      
        if(introPlayer != null)
			introPlayer.SetActive(false);

		yield return new WaitForSeconds(.25f);
		if (greetingCard != null)
        {
            greetingCard.SetActive(true);
            
            if (PlayerPrefs.HasKey(greetingPlayerPrefs))
            {
                playerName.text = PlayerPrefs.GetString(greetingPlayerPrefs);
            }
            else
                playerName.text = "Friend";

            int rend = Random.Range(0, greetingProperties.Length - 1);
            greetingText.text = greetingProperties[rend];

			StartCoroutine(WaitForGreeting());
		}
        else
            Debug.LogError("ASSIGN CARD BRUH!");


        //SceneManager.LoadScene(1);  
    }

    IEnumerator WaitForGreeting()
    {
        yield return new WaitForSeconds(secondWaitTime);
        SceneManager.LoadSceneAsync(1);
		//SceneManager.LoadScene(1);
	}
}
