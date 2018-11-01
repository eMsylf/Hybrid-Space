using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour {

	public Camera cam1;
	public Camera cam2;

	// Use this for initialization
	void Start () {
		cam1.enabled = true;
		cam2.enabled = false;
	}
	
	// Update is called once per frame
	public void Change()
	{
		cam1.enabled = false;
		cam2.enabled = true;
	}
}
