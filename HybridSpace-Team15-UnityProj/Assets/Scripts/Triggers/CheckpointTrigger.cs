using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour {

	public Camera cam;
	public int checkpointID;

	void OnCollisionEnter(Collision collider)
	{
		if (collider.gameObject.name == "Player")
		{
			// colliding with current checkpoint --> do nothing
			if (GameManager.instance.activeCheckpoint.id == checkpointID)
			{
				return;
			}

			GameManager.instance.NextCheckpoint();
			collider.gameObject.GetComponent<SendMessage>().StopMoving();
			TransformCamera();
			GameManager.instance.ResetLevel();
		}
	}

	void TransformCamera()
	{
		cam.transform.position = GameManager.instance.activeCheckpoint.cameraPosition;
	}
}
