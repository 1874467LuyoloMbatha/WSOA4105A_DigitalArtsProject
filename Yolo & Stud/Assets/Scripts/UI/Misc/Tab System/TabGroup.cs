using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public enum ButtonType { sprite, colour}
    public enum TypeOfTab { Swapping, Uniform}

    public enum TabOption { Default, SameScreen}

    [Header("Type Of Tab")]
    [SerializeField] TypeOfTab typeOfTab;
	[Tooltip("The default allows the tab buttons to be switched off after being clicked")]
	[SerializeField] TabOption option;

    [Header("External Scripts + Current Button + Switching GameObjects")]
    [Tooltip("All buttons will live under this list")]
    [SerializeField] List<TabButtons> tabButtons;
    [Tooltip("Stores a list of game objects so that the buttons activate and deactivate them")]
    [SerializeField] List<GameObject> objsToSwap;
    [SerializeField] TabButtons selectedButton;

    [Header("Type Of Buttons")]
    [SerializeField] ButtonType typeOfButton;

    [Header("Button State: Sprite")]
    [SerializeField] Sprite tabIdle;
    [SerializeField] Sprite tabHover, tabSelected;

    [Header("Button State: Colour")]
    [SerializeField] Color colorIdle;
    [SerializeField] Color colorHover, colorSelected;

    [Header("Saving/Loading Index")]
    [SerializeField] int saveLoadIndex;
    [SerializeField] int mainBackButtonIndex;


    public void Subscribe(TabButtons btn)
	{
        if(tabButtons == null)
            tabButtons = new List<TabButtons>();

        tabButtons.Add(btn);
	}
   
    public void OnTabEnter(TabButtons btn)
	{
        ResetTabs();

        //Checks if button is already selected
        if (selectedButton == null || btn != selectedButton)
        { 
            if (typeOfButton == ButtonType.sprite)
                btn.bg.sprite = tabHover;
            if (typeOfButton == ButtonType.colour)
                btn.bg.color = colorHover; 
        }
    }

    public void OnTabSelected(TabButtons btn)
    {
        if (selectedButton != null)
            selectedButton.DeSelect();

        if (!btn.isBackButton)
            selectedButton = btn;

        if (selectedButton != null)
            selectedButton.Select();

        ResetTabs();

        if (typeOfButton == ButtonType.sprite)
        {
            // btn.bg.sprite = tabSelected;
            // btn.buttonLine.sprite = tabSelected;
            btn.buttonLine.gameObject.SetActive(true);
        }
        if (typeOfButton == ButtonType.colour)
            btn.bg.color = colorSelected;

        //Swicthing Objects
        if (typeOfTab == TypeOfTab.Swapping)
        {
            if (!btn.isBackButton)
            {
                int index = btn.transform.GetSiblingIndex();
                for (int i = 0; i < objsToSwap.Count; i++)
                {
                    if (i == index)
                        objsToSwap[i].SetActive(true);
                    else
                        objsToSwap[i].SetActive(false);

                }
            }
            else if (btn.isBackButton && btn.typeOfBackButton == TabButtons.BackButtonType.Uniform)
            {
                for (int i = 0; i < objsToSwap.Count; i++)
                {
                    objsToSwap[i].SetActive(false);
                }
                selectedButton = null;
            }

            if (btn.typeOfBackButton == TabButtons.BackButtonType.Switch && btn.isBackButton)
            {
                for (int i = 0; i < objsToSwap.Count; i++)
                {
                    if (i == saveLoadIndex || i == mainBackButtonIndex)
                        objsToSwap[i].SetActive(true);
                    else
                        objsToSwap[i].SetActive(false);
                }

                selectedButton = null;
            }

        }
        else if (typeOfTab == TypeOfTab.Uniform)
            selectedButton = null;


        //Swicth off all the tab buttons
        if (option == TabOption.Default )
        {
            for (int i = 0; i < tabButtons.Count; i++)
            {
				if (!btn.isBackButton)
                    tabButtons[i].gameObject.SetActive(false);

				if (tabButtons[i].isBackButton)
                    tabButtons[i].gameObject.SetActive(true);
            }
        }

        if (btn.typeOfBackButton == TabButtons.BackButtonType.Uniform && btn.isBackButton)
        {
            if (option == TabOption.Default)
            {
                for (int i = 0; i < tabButtons.Count; i++)
                {
                    tabButtons[i].gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnTabExit(TabButtons btn)
	{
        ResetTabs();
    }

    public void ResetTabs()
	{
		foreach (TabButtons btn in tabButtons)
		{
            if (selectedButton != null && btn == selectedButton)
                continue;

            if (typeOfButton == ButtonType.sprite)
            {
                btn.bg.sprite = tabIdle;
                btn.buttonLine.gameObject.SetActive(false);
            }
            if (typeOfButton == ButtonType.colour)
                btn.bg.color = colorIdle;
		}
	}
}
