
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using AnotherFileBrowser.Windows;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ImportManager : Singleton<ImportManager>
{
    [Header("Generic Strings")]
    [SerializeField] string filePath = @"file://C:\";
	[SerializeField] string filePathPrefs = "CustomMusicPrefs";

	[Header("For The Music")]
	public List<AudioClip> toPopulate;
	void Start()
    {
		if (PlayerPrefs.HasKey(filePathPrefs))
		{
			SetFilePath(PlayerPrefs.GetString(filePathPrefs));
			LoadFiles();
		}
	}
	#region IENumerators to load the differemt audio files
	IEnumerator LoadWav(string url)
	{
		//UnityEngine.Debug.Log("HAO?");
		url = string.Format("file://{0}", url);
	//	UnityEngine.Debug.Log("Debugging:" + url);

		using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV))
		{
			yield return www.SendWebRequest();
			if (www.result == UnityWebRequest.Result.ConnectionError)
			{
				UnityEngine.Debug.Log(www.error);
			}
			else
			{
				toPopulate.Add(DownloadHandlerAudioClip.GetContent(www));
				UnityEngine.Debug.Log("added wav files");
				//aud.clip = DownloadHandlerAudioClip.GetContent(www);
			}
			//UnityEngine.Debug.Log(www.downloadHandler.data);
		}
	}

	IEnumerator LoadMPEG(string url)
	{
		//UnityEngine.Debug.Log("HAO?");
		url = string.Format("file://{0}", url);
		//	UnityEngine.Debug.Log("Debugging:" + url);

		using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
		{
			yield return www.SendWebRequest();
			if (www.result == UnityWebRequest.Result.ConnectionError)
			{
				UnityEngine.Debug.Log(www.error);
			}
			else
			{
				toPopulate.Add(DownloadHandlerAudioClip.GetContent(www));
				UnityEngine.Debug.Log("added mp3 files");
				//aud.clip = DownloadHandlerAudioClip.GetContent(www);
			}
			//UnityEngine.Debug.Log(www.downloadHandler.data);
		}
	}

	IEnumerator LoadOGG(string url)
	{
		//UnityEngine.Debug.Log("HAO?");
		url = string.Format("file://{0}", url);
		//	UnityEngine.Debug.Log("Debugging:" + url);

		using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS))
		{
			yield return www.SendWebRequest();
			if (www.result == UnityWebRequest.Result.ConnectionError)
			{
				UnityEngine.Debug.Log(www.error);
			}
			else
			{
				toPopulate.Add(DownloadHandlerAudioClip.GetContent(www));
				UnityEngine.Debug.Log("Added ogg files");
				//aud.clip = DownloadHandlerAudioClip.GetContent(www);
			}
			//UnityEngine.Debug.Log(www.downloadHandler.data);
		}
	}
	#endregion

	#region Public Functions To Reference
	public void LoadFiles()
	{
		if (Directory.Exists(filePath))
		{
			//AudioClip[] temp =
			foreach (var item in Directory.GetFiles(filePath))
			{
				if (item.Contains(".wav"))
				{
					UnityEngine.Debug.Log(item);
					StartCoroutine(LoadWav(item));
				}

				if (item.Contains(".mp3"))
				{
					UnityEngine.Debug.Log(item);
					StartCoroutine(LoadMPEG(item));
				}

				if (item.Contains(".ogg"))
				{
					UnityEngine.Debug.Log(item);
					StartCoroutine(LoadOGG(item));
				}
			}
		}
	}
	public void PlayAudio()
	{
		int tempClip = UnityEngine.Random.Range(0, toPopulate.Count - 1);
		//UnityEngine.Debug.Log(toPopulate.Count);
		GetComponent<AudioSource>().clip = toPopulate[tempClip];
		GetComponent<AudioSource>().Play();
	}
	public void OpenExplorer()
    {
		var bp = new BrowserProperties();
		bp.filter = "txt files (*.txt)|*.txt|All Files (*.*)|*.*";
		bp.filterIndex = 0;

		new FileBrowser().OpenFolderBrowser(bp, path =>
		{
			//Do something with path(string)
			SetFilePath(path);
			LoadFiles();
			Debug.Log(path);
		});
		//Application.OpenURL(filePath);
	}

	public void SetFilePath(string v)
	{
		filePath = v;
		PlayerPrefs.SetString(filePathPrefs, filePath);
	}
	public string GetFilePath()
    {
        return filePath;
    }
	#endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/