using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour {

	public Camera cam;
	public float newZ;

	public void DoResize()
	{
		float oldDistance = (gameObject.transform.position - cam.transform.position).magnitude;
		Vector3 newBoxPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, newZ);

		float newDistance = (newBoxPos - cam.transform.position).magnitude;
		float newSize = (newDistance/oldDistance);

		// REPOSITIONING
		Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
		Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, newZ));
		
		transform.position = worldPos;
		transform.localScale = Vector3.one * newSize;

	}

}
