using UnityEngine;
using Vuforia;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

	public float speed;
	public bool active;
	public GameObject platformPrefab;
	public Camera cam;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// only move player when colliding with platform
	void OnCollisionStay(Collision c)
	{
		if (active)
		{
			rb.AddForce(Vector3.left * speed);
			Debug.Log("Should move");
		}
	}

	public void EnableMovement()
	{
		GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
		foreach(GameObject platform in platforms)
		{
			// only get rendered platforms
			if (!platform.GetComponent<MeshRenderer>().enabled) continue;

			Vector3 pos = platform.transform.position;
			Quaternion rot = platform.transform.rotation;
			GameObject platform_instance = (GameObject)Instantiate(platformPrefab, pos, rot);
			SetScaleAndPosition(ref platform_instance);

			Destroy(platform);
		}
		
		
		TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
		active = true;
		rb.AddForce(Vector3.left * speed);
	}

	private void SetScaleAndPosition(ref GameObject obj)
	{
		float oldDistance = (obj.transform.position - cam.transform.position).magnitude;
		Vector3 newBoxPos = new Vector3(obj.transform.position.x, obj.transform.position.y, 19);

		float newDistance = (newBoxPos - cam.transform.position).magnitude;
		float newSize = (newDistance/oldDistance);

		// REPOSITIONING
		Vector3 screenPos = cam.WorldToScreenPoint(obj.transform.position);
		Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 19));
		
		obj.transform.position = worldPos;
		obj.transform.localScale = Vector3.one * newSize;
		obj.transform.rotation = Quaternion.identity;
	}
	
}
