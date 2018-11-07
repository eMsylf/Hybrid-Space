using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : MonoBehaviour {

	public void StopMoving()
	{
		GetComponent<PlayerMovement>().activeSimulation = false;
	}
}
