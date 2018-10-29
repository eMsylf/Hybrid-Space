using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		WebCamDevice[] devices = WebCamTexture.devices;
		for( int i = 0 ; i < devices.Length ; i++ )
   			Debug.Log(devices[i].name);

		WebCamTexture webcam = new WebCamTexture(devices[0].name);
		GetComponent<MeshRenderer>().material.mainTexture = webcam;
		webcam.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
