using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ImportManager : MonoBehaviour
{
    [Header("Generic Strings")]
    [SerializeField] string filePath = @"file://C:\";
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void OpenExplorer()
    {
        Application.OpenURL(filePath);
    }
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/