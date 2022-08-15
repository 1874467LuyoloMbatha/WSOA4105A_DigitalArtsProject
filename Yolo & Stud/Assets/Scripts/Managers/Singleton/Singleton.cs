using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<S> : MonoBehaviour
	where S : Component
{
	private static S instance;
	public static S Instance
	{
		get 
		{
			if(instance == null)
			{
				var objects = FindObjectsOfType(typeof(S)) as S[];
				if (objects.Length > 0)
					instance = objects[0];
				if (objects.Length > 1)
					Debug.LogError("There is more than one " + typeof(S).Name + " in the scene! Dumb Dumb.");
				
				//Casts new game object of this singleton
				if(instance == null)
				{
					GameObject obj = new GameObject();
					obj.hideFlags = HideFlags.HideAndDontSave;
					instance = obj.AddComponent<S>();
				}
			}
			return instance;
		}
	}
}

public class SingletonPersistent<S> : MonoBehaviour
	where S : Component
{
	public static S Instance { get; private set; }

	public virtual void Awake()
	{
		if (Instance == null)
		{
			Instance = this as S;
			DontDestroyOnLoad(this);
		}
		else
			Destroy(gameObject);
	}
}
