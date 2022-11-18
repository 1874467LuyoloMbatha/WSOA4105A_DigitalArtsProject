using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AnotherFileBrowser.Windows;
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
		var bp = new BrowserProperties();
		bp.filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
		bp.filterIndex = 0;

		new FileBrowser().OpenFolderBrowser(bp, path =>
		{
			//Do something with path(string)
			Debug.Log(path);
		});
		//Application.OpenURL(filePath);
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/