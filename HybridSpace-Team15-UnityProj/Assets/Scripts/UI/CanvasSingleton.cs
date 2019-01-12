using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// makes sure we have 1 instance of the canvas throughout the game
//  allows us to keep UI references for checkpoint scores

public class CanvasSingleton : MonoBehaviour
{
	//public static CanvasSingleton Singleton;

	// Use this for initialization
	/*void Awake()
	{
		if (Singleton == null)
		{
			Singleton = this;
		}
		else if (Singleton != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}*/
}