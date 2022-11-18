using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System.Diagnostics;

[RequireComponent(typeof(AudioSource))]
public class SongNAudio : MonoBehaviour
{
    public string path = "c:/omar/song.mp3";
    private AudioSource aud = null;

    private void Start()
    {
        aud = this.GetComponent<AudioSource>();
        StartCoroutine(LoadSongCoroutine());
    }
    private IEnumerator LoadSongCoroutine()
    {
        string url = string.Format("file://{0}", path);
        //UnityWebRequest www = UnityWebRequestMultimedia(url, AudioType.MPEG);
        UnityWebRequest www = UnityWebRequest.Get(url);
		//WWW www = new WWW(url);
		yield return www.SendWebRequest();


        if (www == UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            aud.clip = NAudioPlayer.FromMp3Data(www.downloadHandler.data);
        }
        else
        {
            aud.clip = DownloadHandlerAudioClip.GetContent(www);
        }

		UnityEngine.Debug.Log(www.downloadHandler.data);
		//aud.clip = NAudioPlayer.FromMp3Data(www.bytes);
        aud.Play();
    }
}
