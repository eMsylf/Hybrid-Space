using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpComponent : MonoBehaviour {

	public int upStrength;
	public int leftStrength;

	void OnCollisionEnter(Collision collider)
	{
		if (collider.transform.name == "Player")
		{
			float speed = collider.transform.GetComponent<PlayerMovement>().speed;
			collider.transform.GetComponent<Rigidbody>().AddForce(new Vector3(-1 * leftStrength, 1 * upStrength, 0), ForceMode.Impulse);
		}
	}

}
