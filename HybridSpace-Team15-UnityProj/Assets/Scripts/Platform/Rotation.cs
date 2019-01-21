using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
	Vector3 forward; // cache forward vector

	// Use this for initialization
	void Start()
	{
		forward = new Vector3(0, 0, 1);
	}

	void LateUpdate()
	{
		//float zRot = transform.eulerAngles.z;
		//transform.rotation = Quaternion.Euler(0, 0, zRot);
		transform.forward = forward;
		transform.up = transform.parent.up;
		transform.right = transform.parent.right;
		transform.right = new Vector3(transform.right.x, transform.right.y, 0);

	}
}