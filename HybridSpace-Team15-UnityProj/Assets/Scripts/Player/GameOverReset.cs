using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverReset : MonoBehaviour {

	public float threshold;

	void Update()
	{
		if(transform.position.y < threshold)
		{
			GetComponent<Rigidbody>().isKinematic = true; // remove momentum
			ResetLevel();
			GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	void ResetLevel()
	{
		// reset player
		GetComponent<PlayerMovement>().activeSimulation = false;
		transform.position = GameManager.instance.activeCheckpoint.playerPosition;

		GameManager.instance.ResetLevel();
	}

}
