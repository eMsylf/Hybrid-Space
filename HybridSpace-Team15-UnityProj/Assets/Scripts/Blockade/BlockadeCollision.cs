using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockadeCollision : MonoBehaviour {

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Platform")
		{
			// SHOW HUD
		}
	}
}
