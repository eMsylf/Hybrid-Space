using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimation : MonoBehaviour
{

	public float minScale, maxScale, speed;
	Transform colliderObject;

	float divider;

	// Use this for initialization
	void Start()
	{
		divider = 2.0f / (maxScale - minScale);

		// quick workaround so we don't have to replace all the end point prefabs
		colliderObject = transform.GetChild(0);
		colliderObject.parent = transform.parent;
	}

	// Update is called once per frame
	void Update()
	{

		float scale = ((Mathf.Sin(Time.time * speed) + 1) / divider) + minScale;

		transform.localScale = Vector3.one * scale;
	}
}