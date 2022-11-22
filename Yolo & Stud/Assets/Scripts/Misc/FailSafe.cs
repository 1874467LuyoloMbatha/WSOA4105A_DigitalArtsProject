using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FailSafe : MonoBehaviour
{

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			GameManager.Instance.ResetPlayer();
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/