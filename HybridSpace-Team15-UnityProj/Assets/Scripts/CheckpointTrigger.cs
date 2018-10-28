using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour {

	public Camera cam;
	public Vector3 newCameraPosition;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == "Player")
		{
			TransformCamera();
		}
	}

	void TransformCamera()
	{
		cam.transform.position = newCameraPosition;
	}
}
